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
            //numero x fecha actual. del ultimo dia del mes.
            int mes = System.DateTime.Today.Month;
            int anio = System.DateTime.Today.Year;
            int dia = System.DateTime.DaysInMonth(anio, mes);

            string current = dia.ToString() + mes.ToString() + anio.ToString();

            if (pass == current)
                return true;
            else
                return false;
        }

        public static string getMacAddress()
        {
            string macAddress = "";
            foreach (System.Net.NetworkInformation.NetworkInterface n in
                System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (n.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Ethernet) continue;
                if (n.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    macAddress = n.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddress;
        }

        public static void loadLocalData()
        {
            //read ini

        }

        public static void ReadIni(string fullIniPath)
        {
            tr = Validation.ValidationIO.Leer(fullIniPath);
        }

        public static void WriteIni(string fullIniPath, string macAddress, DateTime fecha)
        {
            Validation.ValidationIO.Escribe(fullIniPath, macAddress ,fecha, 
                Validation.ValidationTransactionType.INI);
        }

        public static bool isOK()
        {
            if (tr.Header["MA"].ToString() == getMacAddress())
                return true;
            return false;
        }

    }
}
