using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDM.Validation
{
    public static class Registry
    {
        public static string iniPath;
        public static Validation.ValidationTransaction tr;

        public static bool passInstalationOK(string pass)
        {
            //Clave Fija
            if (pass == "guliverk")
                return true;

            //numero x fecha actual. del ultimo dia del mes.
            int mes = System.DateTime.Today.Month;
            int anio = System.DateTime.Today.Year;
            int dia = System.DateTime.DaysInMonth(anio, mes);

            string current = "E" + dia.ToString() + mes.ToString() + anio.ToString();

            if (pass == current)
                return true;
            else
                return false;
        }

        public static bool passInstalationTrial(string pass)
        {
            //numero x fecha actual. del primer dia del mes.
            int mes = System.DateTime.Today.Month;
            int anio = System.DateTime.Today.Year;
            int dia = 1;    //System.DateTime.DaysInMonth(anio, mes);

            string current = "E" + dia.ToString() + mes.ToString() + anio.ToString();

            if (pass == current)
                return true;
            else
                return false;
        }

        public static string getMacAddress()
        {
            string macAddress = "";
            bool encontrado = false;

            foreach (System.Net.NetworkInformation.NetworkInterface n in
                System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (n.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Ethernet) continue;
                if (n.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    macAddress = n.GetPhysicalAddress().ToString();
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                foreach (System.Net.NetworkInformation.NetworkInterface n in
                    System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (n.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211) continue;

                    //if (n.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    //{
                        macAddress = n.GetPhysicalAddress().ToString();
                        //encontrado = true;
                        break;
                    //}
                }
            }

            return macAddress;
        }

        public static bool ExistsMac(string macAddress)
        {
            foreach (System.Net.NetworkInformation.NetworkInterface n in
                System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if(macAddress == n.GetPhysicalAddress().ToString())
                    return true;
            }
            return false;
        }

        public static void loadLocalData()
        {
            //read ini

        }

        public static void ReadIni(string fullIniPath)
        {
            tr = Validation.ValidationIO.Leer(fullIniPath);
        }

        public static void WriteIni(string fullIniPath, string macAddress, DateTime fecha, bool isTrial, bool validYear)
        {
            Validation.ValidationIO.Escribe(fullIniPath, macAddress ,fecha, 
                Validation.ValidationTransactionType.INI, isTrial, validYear);
        }

        public static bool isOK()
        {
            //if (tr.Header["MA"].ToString() == getMacAddress())
            //    return true;
            if (ExistsMac(tr.Header["MA"].ToString()))
                return true;

            return false;
        }

        /// <summary>
        /// Retorna la cantidad de idas que tiene disponible.
        /// </summary>
        /// <returns></returns>
        public static int isTrial()
        {
            if (tr.Header.ContainsKey("TRL"))
            {
                if (tr.Header["TRL"].ToString() == "1")
                {
                    DateTime dt = Convert.ToDateTime(tr.Header["FECHA"].ToString());
                    dt = dt.AddDays(30);
                    int diference = (dt - DateTime.Today).Days;
                    return diference;
                }
                else 
                {
                    if (tr.Header["VYR"].ToString() == "0")
                    {
                        return 99999;
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(tr.Header["FECHA"].ToString());
                        dt = dt.AddDays(365); //por un año
                        int diference = (dt - DateTime.Today).Days;
                        return diference;
                    }
                }
            }
            return -1;
        }

        public static string GetLicenceType()
        {
            if (tr.Header.ContainsKey("TRL"))
            {
                if (tr.Header["TRL"].ToString() == "1")
                {
                    return "Trial";
                }
            }
            
            if (tr.Header.ContainsKey("VYR") && tr.Header.ContainsKey("TRL"))
            {
                if (tr.Header["VYR"].ToString() == "0" && tr.Header["TRL"].ToString() == "0")
                {
                    return "Admin";
                }
            }

            return "UserLicense";
        }
    }
}
