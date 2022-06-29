using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Contabilidad
{
    public partial class MainForm : Form
    {
        public bool CheckingRegistry()
        {
            string archivoIni = System.Windows.Forms.Application.StartupPath + 
                "\\edm.ini";
            bool isOK = false;
            EDM.Validation.Registry.iniPath = archivoIni;

            if (System.IO.File.Exists(archivoIni))
            {
                EDM.Validation.Registry.ReadIni(archivoIni);
                isOK = EDM.Validation.Registry.isOK();
                if (isOK)
                    return true;
            }
            int iteracion = 3;
            FormLicenseCode form = new FormLicenseCode();
            for (int i = 0; i < iteracion; i++)
            {
                form.ShowDialog();
                if (EDM.Validation.Registry.passInstalationOK(form.pass))
                {
                    EDM.Validation.Registry.WriteIni(archivoIni,
                        EDM.Validation.Registry.getMacAddress(),
                        DateTime.Today);
                    return true;
                }
            }
            return false;
        }


    }
}
