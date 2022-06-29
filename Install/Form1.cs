using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using IWshRuntimeLibrary; 


namespace Install
{
    public partial class FormInstall : Form
    {
        public FormInstall()
        {
            InitializeComponent();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            //x86
            //string edmPath32 = ProgramFilesx86();
            //edmPath32 = Path.Combine(edmPath32, "EDM");

            //if(Directory.Exists(edmPath32))
            //{
            //    existEDMfolder = true;
            //    edmPath = edmPath32;
            //}

            string edmPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "EDM");
            string edmFilePath = Path.Combine(edmPath, "EDM Balance.exe");

            //edmPath = @"c:\temp\EDM";

            if (!Directory.Exists(edmPath))
            {
                Directory.CreateDirectory(edmPath);
            }


            DirectoryInfo targetTemp = new DirectoryInfo(edmPath);
            DirectoryInfo source = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            //Complete Install
            CopyFilesRecursively(source, targetTemp);
                
            //create shortcut
            string DesktopPathName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "EDM Balance" + ".lnk"); 
            //string shortcutPathName = Path.Combine(targetTemp.ToString(), "EDM Balance.exe");
            try 
            {
                string shortcutTarget = System.IO.Path.Combine(targetTemp.ToString(), "EDM Balance.exe"); 
                WshShell myShell = new WshShell();
                WshShortcut myShortcut = (WshShortcut)myShell.CreateShortcut(DesktopPathName); 
                myShortcut.TargetPath = shortcutTarget; //The exe file this shortcut executes when double clicked 
                myShortcut.IconLocation = shortcutTarget + ",0"; //Sets the icon of the shortcut to the exe`s icon 
                myShortcut.WorkingDirectory = targetTemp.ToString(); //The working directory for the exe 
                myShortcut.Arguments = ""; //The arguments used when executing the exe 
                myShortcut.Save(); //Creates the shortcut 
            } 
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message); 
            }

            MessageBox.Show("Ha finalizado la instalación.");
            System.Windows.Forms.Application.Exit();
        }

        private static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                    CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
                
            foreach (FileInfo file in source.GetFiles())
            {
                if (file.Name != "PlandeCuentas.dat")
                {
                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                }
                else
                {
                    if(!System.IO.File.Exists(Path.Combine(target.FullName, file.Name)))
                    {
                        file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                    }
                }
            }
        }
    }
}
