using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace EDM
{
    public class PlanDeCuentas
    {
        private string planDeCuentasFileName = "PlandeCuentas.dat";
        public Dictionary<int, string> planDeCuentas;
        public Dictionary<int, string> planDeCuentasInactivas;

        public PlanDeCuentas()
        {
            planDeCuentas = new Dictionary<int, string>();
            planDeCuentasInactivas = new Dictionary<int, string>();
            Load();
        }

        private void Load()
        { 
            string pdcPath = EDM.programPath + "\\Data\\"+ planDeCuentasFileName;
            using (StreamReader sr = new StreamReader(pdcPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        string[] strline = line.Split('\t'); //2 columnas: 0=codigo 1=Cuenta Descripcion 2=Activo
                        if (strline[1] != "")
                        {
                            if (strline[2] == "1")
                            {
                                if (!planDeCuentas.ContainsKey(Convert.ToInt32(strline[0])))
                                    planDeCuentas.Add(Convert.ToInt32(strline[0]), strline[1]);
                            }
                            else if (strline[2]=="0")
                                planDeCuentasInactivas.Add(Convert.ToInt32(strline[0]), strline[1]);
                        }
                    }
                }
            }
        }

        public string GetCuenta(int id)
        {
            return planDeCuentas[id];
        }

        public bool AddCuenta(int id, string description)
        {
            try
            {
                if (!planDeCuentas.ContainsKey(id))
                    planDeCuentas.Add(id, description);
                
                //Escribir en Archivo
                string pdcPath = EDM.programPath + "\\Data\\"+ planDeCuentasFileName;
                using (StreamWriter sw = new StreamWriter(pdcPath, true))
                {
                    sw.WriteLine(id.ToString()+ "\t" + description + "\t" + "1");
                }
            }
            catch
            {                
                return false;
            }
            return true;
        }

        public void EliminarCuenta(int id)
        {
            planDeCuentasInactivas.Add(id, planDeCuentas[id].ToString());
            planDeCuentas.Remove(id);
            GuardarCompleto();
        }

        public bool ModificarCuenta(int id, string description)
        {
            try
            {
                planDeCuentas[id] = description;
                GuardarCompleto();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool GuardarCompleto()
        { 
             try
            {
                string pdcPath = EDM.programPath + "\\Data\\" + planDeCuentasFileName;
                using (StreamWriter sw = new StreamWriter(pdcPath, false))
                {
                    foreach (KeyValuePair<int, string> kvp in planDeCuentas)
                        sw.WriteLine(kvp.Key.ToString() + "\t" + kvp.Value + "\t" + "1");
                    foreach (KeyValuePair<int, string> kvp in planDeCuentasInactivas)
                        sw.WriteLine(kvp.Key.ToString() + "\t" + kvp.Value + "\t" + "0");
                }
            }
            catch
            {
                return false;
            }
            return true;           
        }
    }
}
