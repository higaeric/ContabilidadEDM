using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace EDM.Validation
{
    public static class ValidationIO
    {

        public static void Escribe(string fullPath, string name, DateTime fechaI, DateTime fechaF,
            List<string> contentStr, Validation.ValidationTransactionType type)
        {
            Validation.ValidationTransaction tr = new Validation.ValidationTransaction(type);

            tr.Header["EMPRESA"] = name;
            tr.Header["FECHAINICIO"] = fechaI.ToShortDateString();
            tr.Header["FECHAFIN"] = fechaF.ToShortDateString();

            using (System.IO.StreamWriter w = new System.IO.StreamWriter(fullPath,false))
            {
                foreach (var h in tr.Header)
                    w.WriteLine(h.Key + "=" + h.Value);

                //Valores
                foreach (string str in contentStr)
                    w.WriteLine(str);
            }
        }

        /// <summary>
        /// Usado para el Registry
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="macAddress"></param>
        /// <param name="fecha"></param>
        /// <param name="type"></param>
        public static void Escribe(string fullPath, string macAddress, DateTime fecha,
            Validation.ValidationTransactionType type)
        {
            Validation.ValidationTransaction tr = new Validation.ValidationTransaction(type);

            tr.Header["MA"] = macAddress;
            tr.Header["FECHA"] = fecha.ToShortDateString();

            using (System.IO.StreamWriter w = new System.IO.StreamWriter(fullPath, false))
            {
                foreach (var h in tr.Header)
                    w.WriteLine(h.Key + "=" + h.Value);
            }
        }

        public static void AddDataInFile(string fullPath, List<string> content)
        {
            using (System.IO.StreamWriter w = new StreamWriter(fullPath, true))
            {
                for (int i = 0; i < content.Count; i++)
                    w.WriteLine(content[i]);
            }
        }

        public static ValidationTransaction Leer(string path)
        {
            ValidationTransaction tr = new ValidationTransaction(path);
            return tr;
        }
    }
}
