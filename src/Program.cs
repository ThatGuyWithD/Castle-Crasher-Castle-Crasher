
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

static class Program
{
    static void ApplyBranding(Control control)
    {
        if (control == null) return;

        if (!String.IsNullOrEmpty(control.Text))
        {
            control.Text = control.Text
                .Replace("Crasher Unlocker V1.2", "Crasher Editor V1.2")
                .Replace("Crasher Unlocker", "Crasher Editor");
        }

        foreach (Control child in control.Controls)
            ApplyBranding(child);
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        MainForm form = new MainForm();
        ApplyBranding(form);
        form.Text = "Crasher Editor V1.2";
        Application.Run(form);
    }
}
