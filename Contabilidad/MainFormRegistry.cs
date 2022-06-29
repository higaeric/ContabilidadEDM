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
            string archivoIni = System.Windows.Forms.Application.StartupPath + "\\edm.ini";

            bool isOK = false;
            EDM.Validation.Registry.iniPath = archivoIni;

            if (System.IO.File.Exists(archivoIni))
            {
                EDM.Validation.Registry.ReadIni(archivoIni);
                isOK = EDM.Validation.Registry.isOK();
                if (isOK)
                {
                    int cantDiasd = EDM.Validation.Registry.isTrial();
                    if (cantDiasd == 99999)
                    {
                        return true;
                    }
                    else 
                    {
                        if (cantDiasd < 0)
                        {
                            //mensaje caducado
                            MessageBox.Show("Version Caducada\r\n\r\nContactarse con del Desarrollador para obtener la version final. Muchas Gracias.", "Version de Prueba",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                        }
                        else if (cantDiasd <15)
                        { 
                            //mensaje quedan 
                            MessageBox.Show("Version de prueba.\r\nRestan " + cantDiasd.ToString() + " días.\r\n\r\nContactarse con el Desarrollador del programa.", "Version de Prueba",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return true;
                        }
                        else if (cantDiasd > 14)
                        {
                            return true;
                        }
                    }
                }
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
                        DateTime.Today, false);
                    return true;
                }

                if (EDM.Validation.Registry.passInstalationTrial(form.pass)) 
                {
                    EDM.Validation.Registry.WriteIni(archivoIni,
                        EDM.Validation.Registry.getMacAddress(),
                        DateTime.Today, true);
                    return true;
                }
            }
            return false;
        }


    }
}
