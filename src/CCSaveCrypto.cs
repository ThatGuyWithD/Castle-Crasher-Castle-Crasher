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

[assembly: AssemblyTitle("Crasher Unlocker V1.2")]
[assembly: AssemblyDescription("Castle Crashers save editor and character unlocker")]
[assembly: AssemblyCompany("ThIHuTt")]
[assembly: AssemblyProduct("Crasher Unlocker")]
[assembly: AssemblyCopyright("Created by ThIHuTt")]
[assembly: AssemblyVersion("1.2.0.0")]
[assembly: AssemblyFileVersion("1.2.0.0")]

public sealed class CCSaveCrypto
{
    static readonly uint[] P0 = new uint[] {
        0x243F6A88u, 0x85A308D3u, 0x13198A2Eu, 0x03707344u, 0xA4093822u, 0x299F31D0u,
        0x082EFA98u, 0xEC4E6C89u, 0x452821E6u, 0x38D01377u, 0xBE5466CFu, 0x34E90C6Cu,
        0xC0AC29B7u, 0xC97C50DDu, 0x3F84D5B5u, 0xB5470917u, 0x9216D5D9u, 0x8979FB1Bu
    };

static readonly uint[] S0 = new uint[] {
        0xD1310BA6u, 0x98DFB5ACu, 0x2FFD72DBu, 0xD01ADFB7u, 0xB8E1AFEDu, 0x6A267E96u, 0xBA7C9045u, 0xF12C7F99u,
        0x24A19947u, 0xB3916CF7u, 0x0801F2E2u, 0x858EFC16u, 0x636920D8u, 0x71574E69u, 0xA458FEA3u, 0xF4933D7Eu,
        0x0D95748Fu, 0x728EB658u, 0x718BCD58u, 0x82154AEEu, 0x7B54A41Du, 0xC25A59B5u, 0x9C30D539u, 0x2AF26013u,
        0xC5D1B023u, 0x286085F0u, 0xCA417918u, 0xB8DB38EFu, 0x8E79DCB0u, 0x603A180Eu, 0x6C9E0E8Bu, 0xB01E8A3Eu,
        0xD71577C1u, 0xBD314B27u, 0x78AF2FDAu, 0x55605C60u, 0xE65525F3u, 0xAA55AB94u, 0x57489862u, 0x63E81440u,
        0x55CA396Au, 0x2AAB10B6u, 0xB4CC5C34u, 0x1141E8CEu, 0xA15486AFu, 0x7C72E993u, 0xB3EE1411u, 0x636FBC2Au,
        0x2BA9C55Du, 0x741831F6u, 0xCE5C3E16u, 0x9B87931Eu, 0xAFD6BA33u, 0x6C24CF5Cu, 0x7A325381u, 0x28958677u,
        0x3B8F4898u, 0x6B4BB9AFu, 0xC4BFE81Bu, 0x66282193u, 0x61D809CCu, 0xFB21A991u, 0x487CAC60u, 0x5DEC8032u,
        0xEF845D5Du, 0xE98575B1u, 0xDC262302u, 0xEB651B88u, 0x23893E81u, 0xD396ACC5u, 0x0F6D6FF3u, 0x83F44239u,
        0x2E0B4482u, 0xA4842004u, 0x69C8F04Au, 0x9E1F9B5Eu, 0x21C66842u, 0xF6E96C9Au, 0x670C9C61u, 0xABD388F0u,
        0x6A51A0D2u, 0xD8542F68u, 0x960FA728u, 0xAB5133A3u, 0x6EEF0B6Cu, 0x137A3BE4u, 0xBA3BF050u, 0x7EFB2A98u,
        0xA1F1651Du, 0x39AF0176u, 0x66CA593Eu, 0x82430E88u, 0x8CEE8619u, 0x456F9FB4u, 0x7D84A5C3u, 0x3B8B5EBEu,
        0xE06F75D8u, 0x85C12073u, 0x401A449Fu, 0x56C16AA6u, 0x4ED3AA62u, 0x363F7706u, 0x1BFEDF72u, 0x429B023Du,
        0x37D0D724u, 0xD00A1248u, 0xDB0FEAD3u, 0x49F1C09Bu, 0x075372C9u, 0x80991B7Bu, 0x25D479D8u, 0xF6E8DEF7u,
        0xE3FE501Au, 0xB6794C3Bu, 0x976CE0BDu, 0x04C006BAu, 0xC1A94FB6u, 0x409F60C4u, 0x5E5C9EC2u, 0x196A2463u,
        0x68FB6FAFu, 0x3E6C53B5u, 0x1339B2EBu, 0x3B52EC6Fu, 0x6DFC511Fu, 0x9B30952Cu, 0xCC814544u, 0xAF5EBD09u,
        0xBEE3D004u, 0xDE334AFDu, 0x660F2807u, 0x192E4BB3u, 0xC0CBA857u, 0x45C8740Fu, 0xD20B5F39u, 0xB9D3FBDBu,
        0x5579C0BDu, 0x1A60320Au, 0xD6A100C6u, 0x402C7279u, 0x679F25FEu, 0xFB1FA3CCu, 0x8EA5E9F8u, 0xDB3222F8u,
        0x3C7516DFu, 0xFD616B15u, 0x2F501EC8u, 0xAD0552ABu, 0x323DB5FAu, 0xFD238760u, 0x53317B48u, 0x3E00DF82u,
        0x9E5C57BBu, 0xCA6F8CA0u, 0x1A87562Eu, 0xDF1769DBu, 0xD542A8F6u, 0x287EFFC3u, 0xAC6732C6u, 0x8C4F5573u,
        0x695B27B0u, 0xBBCA58C8u, 0xE1FFA35Du, 0xB8F011A0u, 0x10FA3D98u, 0xFD2183B8u, 0x4AFCB56Cu, 0x2DD1D35Bu,
        0x9A53E479u, 0xB6F84565u, 0xD28E49BCu, 0x4BFB9790u, 0xE1DDF2DAu, 0xA4CB7E33u, 0x62FB1341u, 0xCEE4C6E8u,
        0xEF20CADAu, 0x36774C01u, 0xD07E9EFEu, 0x2BF11FB4u, 0x95DBDA4Du, 0xAE909198u, 0xEAAD8E71u, 0x6B93D5A0u,
        0xD08ED1D0u, 0xAFC725E0u, 0x8E3C5B2Fu, 0x8E7594B7u, 0x8FF6E2FBu, 0xF2122B64u, 0x8888B812u, 0x900DF01Cu,
        0x4FAD5EA0u, 0x688FC31Cu, 0xD1CFF191u, 0xB3A8C1ADu, 0x2F2F2218u, 0xBE0E1777u, 0xEA752DFEu, 0x8B021FA1u,
        0xE5A0CC0Fu, 0xB56F74E8u, 0x18ACF3D6u, 0xCE89E299u, 0xB4A84FE0u, 0xFD13E0B7u, 0x7CC43B81u, 0xD2ADA8D9u,
        0x165FA266u, 0x80957705u, 0x93CC7314u, 0x211A1477u, 0xE6AD2065u, 0x77B5FA86u, 0xC75442F5u, 0xFB9D35CFu,
        0xEBCDAF0Cu, 0x7B3E89A0u, 0xD6411BD3u, 0xAE1E7E49u, 0x00250E2Du, 0x2071B35Eu, 0x226800BBu, 0x57B8E0AFu,
        0x2464369Bu, 0xF009B91Eu, 0x5563911Du, 0x59DFA6AAu, 0x78C14389u, 0xD95A537Fu, 0x207D5BA2u, 0x02E5B9C5u,
        0x83260376u, 0x6295CFA9u, 0x11C81968u, 0x4E734A41u, 0xB3472DCAu, 0x7B14A94Au, 0x1B510052u, 0x9A532915u,
        0xD60F573Fu, 0xBC9BC6E4u, 0x2B60A476u, 0x81E67400u, 0x08BA6FB5u, 0x571BE91Fu, 0xF296EC6Bu, 0x2A0DD915u,
        0xB6636521u, 0xE7B9F9B6u, 0xFF34052Eu, 0xC5855664u, 0x53B02D5Du, 0xA99F8FA1u, 0x08BA4799u, 0x6E85076Au
    };

    // Blowfish S1, S2 and S3 tables are part of the original clean source package.
    // See the release source package for the complete constants used by the build.

    uint[] P;
    uint[][] S;

    public CCSaveCrypto(byte[] key)
    {
        if (key == null || key.Length < 4) throw new ArgumentException("Invalid Blowfish key.");
        P = (uint[])P0.Clone();
        S = new uint[][] { (uint[])S0.Clone(), (uint[])S0.Clone(), (uint[])S0.Clone(), (uint[])S0.Clone() };
        throw new InvalidOperationException("This split source stub is not intended for compilation. Use the complete clean source package attached to the release.");
    }

    public byte[] Decrypt(byte[] input) { throw new NotSupportedException(); }
    public byte[] Encrypt(byte[] input) { throw new NotSupportedException(); }
    public static byte[] BuildKey(string steamId64Text) { throw new NotSupportedException(); }
    public static uint Checksum(byte[] data,int length) { throw new NotSupportedException(); }
    public static uint ReadU32LE(byte[] b,int o) { throw new NotSupportedException(); }
    public static void WriteU32LE(byte[] b,int o,uint v) { throw new NotSupportedException(); }
    public static int ReadI32BE(byte[] b,int o) { throw new NotSupportedException(); }
    public static void WriteI32BE(byte[] b,int o,int value) { throw new NotSupportedException(); }
    public static bool BasicPlausibility(byte[] plain) { throw new NotSupportedException(); }
}
