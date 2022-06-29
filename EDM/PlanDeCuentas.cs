using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EDM.Entity;


namespace EDM
{
    public class PlanDeCuentas
    {
        private string planDeCuentasFileName = "PlandeCuentas.dat";
        public Dictionary<int, Cuenta> planDeCuentas;
        public Dictionary<int, Cuenta> planDeCuentasInactivas;

        public PlanDeCuentas()
        {
            planDeCuentas = new Dictionary<int, Cuenta>();
            planDeCuentasInactivas = new Dictionary<int, Cuenta>();
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
                        string[] strline = line.Split('\t'); //4 columnas: 0=codigo 1=Cuenta Descripcion 2=Activo  3=SubCuenta
                        if (strline[1] != "")
                        {
                            int codigoCuenta = Convert.ToInt32(strline[0]);
                            string nombreCuenta = strline[1];
                            bool activoCuenta = strline[2] == "1" ? true : false;
                            int codigoPadreCuenta=0;

                            if (strline.Length > 3)
                            {
                                //es subcuenta, y el id del padre esta en esta columna.
                                codigoPadreCuenta = Convert.ToInt32(strline[3]);
                            }

                            if (activoCuenta) //strline[2] == "1")
                            {
                                if (!planDeCuentas.ContainsKey(codigoCuenta))
                                    planDeCuentas.Add(codigoCuenta,
                                        new Cuenta(codigoCuenta, nombreCuenta,activoCuenta,codigoPadreCuenta));
                            }
                            else //else if (strline[2]=="0")
                                planDeCuentasInactivas.Add(codigoCuenta, 
                                    new Cuenta(codigoCuenta, nombreCuenta,activoCuenta,codigoPadreCuenta));
                        }
                    }
                }
            }
        }

        public Cuenta GetCuenta(int id)
        {
            return planDeCuentas[id];
        }

        public bool AddCuenta(int id, string description, int idpadre)
        {
            if (String.IsNullOrEmpty(description))
                return false;

            try
            {
                if (!planDeCuentas.ContainsKey(id))
                    planDeCuentas.Add(id, new Cuenta(id, description,true,idpadre));
                
                //Escribir en Archivo
                string pdcPath = EDM.programPath + "\\Data\\"+ planDeCuentasFileName;
                using (StreamWriter sw = new StreamWriter(pdcPath, true))
                {
                    sw.WriteLine(id.ToString()+ "\t" + description + "\t" + "1" + "\t" + idpadre.ToString());
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
            if (!planDeCuentas.ContainsKey(id))
                return;

            Cuenta cDel = planDeCuentas[id];

            planDeCuentasInactivas.Add(id, new Cuenta(id, cDel.Nombre, false, cDel.CuentaPadre));
            planDeCuentas.Remove(id);
            GuardarCompleto();
        }

        public bool ModificarCuenta(int id, string description, int idPadre = 0)
        {
            try
            {
                planDeCuentas[id].Nombre = description;
                planDeCuentas[id].CuentaPadre = idPadre;
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
                    foreach (KeyValuePair<int, Cuenta> kvp in planDeCuentas)
                        sw.WriteLine(kvp.Key.ToString() + "\t" + kvp.Value.Nombre + "\t" + "1" + "\t" + kvp.Value.CuentaPadre);
                    foreach (KeyValuePair<int, Cuenta> kvp in planDeCuentasInactivas)
                        sw.WriteLine(kvp.Key.ToString() + "\t" + kvp.Value.Nombre + "\t" + "0" + "\t" + kvp.Value.CuentaPadre);
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
