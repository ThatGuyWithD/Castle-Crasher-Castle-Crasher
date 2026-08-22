using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

public sealed partial class MainForm : Form
{
    static SaveSession OpenSpecificSave(string path, ulong accountId)
    {
        byte[] enc=File.ReadAllBytes(path);
        if((enc.Length%8)!=0)throw new InvalidDataException("Encrypted save length is not a Blowfish block multiple.");
        ulong steamId64=SteamIdBase+accountId;
        byte[] key=CCSaveCrypto.BuildKey(steamId64.ToString(CultureInfo.InvariantCulture));
        CCSaveCrypto bf=new CCSaveCrypto(key);
        byte[] plain=bf.Decrypt(enc);
        if(plain.Length<0x40+CharacterRecordCount*0x30+4)throw new InvalidDataException("Save is shorter than the expected Castle Crashers structure.");
        uint stored=CCSaveCrypto.ReadU32LE(plain,plain.Length-4);
        uint calc=CCSaveCrypto.Checksum(plain,plain.Length-4);
        if(stored!=calc)throw new InvalidDataException("Save checksum is invalid.");
        if(!CCSaveCrypto.BasicPlausibility(plain))throw new InvalidDataException("Save data failed the plausibility check.");
        SaveSession ss=new SaveSession();ss.Path=path;ss.AccountId=accountId;ss.SteamId64=steamId64;ss.Plain=plain;ss.Crypto=bf;ss.Modified=File.GetLastWriteTime(path);return ss;
    }

    static IEnumerable<string> FindSaveCandidates(){return GetCandidates().Select(delegate(SaveCandidate x){return x.Path;});}

    sealed class SaveCandidate{public string Path;public ulong AccountId;public ulong SteamId64;public DateTime Modified;}

    static List<SaveCandidate> GetCandidates()
    {
        List<SaveCandidate> hits=new List<SaveCandidate>();
        HashSet<string> roots=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        string v;
        v=ReadRegistryString(Registry.CurrentUser,@"Software\Valve\Steam","SteamPath");if(!String.IsNullOrEmpty(v))roots.Add(v);
        v=ReadRegistryString(Registry.LocalMachine,@"SOFTWARE\WOW6432Node\Valve\Steam","InstallPath");if(!String.IsNullOrEmpty(v))roots.Add(v);
        string pf86=Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),pf=Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        if(!String.IsNullOrEmpty(pf86))roots.Add(Path.Combine(pf86,"Steam"));
        if(!String.IsNullOrEmpty(pf))roots.Add(Path.Combine(pf,"Steam"));
        foreach(string root in roots)
        {
            try
            {
                string userdata=Path.Combine(root,"userdata");
                if(!Directory.Exists(userdata))continue;
                foreach(string dir in Directory.GetDirectories(userdata))
                {
                    string name=Path.GetFileName(dir);ulong account;
                    if(!UInt64.TryParse(name,NumberStyles.None,CultureInfo.InvariantCulture,out account))continue;
                    string path=Path.Combine(dir,AppId.ToString(CultureInfo.InvariantCulture),"remote","cc_save.dat");
                    if(!File.Exists(path))continue;
                    SaveCandidate c=new SaveCandidate();c.Path=path;c.AccountId=account;c.SteamId64=SteamIdBase+account;c.Modified=File.GetLastWriteTime(path);hits.Add(c);
                }
            }
            catch{}
        }
        hits.Sort(delegate(SaveCandidate a,SaveCandidate b){return b.Modified.CompareTo(a.Modified);});
        return hits;
    }

    static string ReadRegistryString(RegistryKey hive,string subKey,string valueName)
    {
        try
        {
            using(RegistryKey k=hive.OpenSubKey(subKey))
            {
                if(k==null)return null;
                object o=k.GetValue(valueName);
                return o==null?null:Convert.ToString(o,CultureInfo.InvariantCulture);
            }
        }
        catch{return null;}
    }
}
