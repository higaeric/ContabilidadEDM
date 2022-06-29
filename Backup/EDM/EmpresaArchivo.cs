using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDM.Validation;
using System.IO;

namespace EDM
{
    public static class EmpresaArchivo
    {

        public static bool NewFile(Empresa empresa)
        { 
            try
            {
                ValidationIO.Escribe(empresa.FullPath, empresa.Name, empresa.FechaInicio, empresa.FechaFinal,
                    new List<string>(), ValidationTransactionType.EMPRESA);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void prepareToString(Entity.Asiento asiento, List<string> regData)
        {
            //0:idRegistro, 1:Nro.Asiento, 2:Fecha, 3:cod, 4:Desc., 5:D/H, 6:Valor 
            string data = "";
            foreach (Entity.Registro reg in asiento.Registros)
            {
                data = reg.idRegistro.ToString() + "\t" +
                    asiento.Numero.ToString() + "\t" +
                    asiento.Fecha.ToShortDateString() + "\t" +
                    reg.Codigo.ToString() + "\t" +
                    reg.Description + "\t";
                if (reg.valueType == global::EDM.Entity.ValueType.Debe)
                    data += "D" + "\t";
                else
                    data += "H" + "\t";
                data += reg.Valor.ToString();

                regData.Add(data);
            }
        }

        public static void WriteAsiento(string fullPath, Entity.Asiento asiento, bool addIntoFile)
        {
            List<string> regData = new List<string>();
            prepareToString(asiento, regData);
            if (addIntoFile)
                Validation.ValidationIO.AddDataInFile(fullPath, regData);
        }
        
        /// <summary>
        /// Borra el archivo actual.
        /// Escribe nuevamente el archivo.
        /// </summary>
        /// <param name="fullPath">string path</param>
        /// <param name="asientos">Lista de Asientos</param>
        /// <param name="empresa">Obj. Empresa</param>
        public static void WriteAsientos(string fullPath, List<Entity.Asiento> asientos, Empresa empresa)
        {
            if(File.Exists(fullPath))
                File.Delete(fullPath);

            List<string> regData = new List<string>();
            foreach (Entity.Asiento asiento in asientos)
                prepareToString(asiento, regData);
            Validation.ValidationIO.Escribe(fullPath, empresa.Name, empresa.FechaInicio,
                empresa.FechaFinal, regData, ValidationTransactionType.EMPRESA);
        }

        public static void ModifyFile()
        {
            
        }

        public static void LoadFile()
        { 
            
        }


        /// <summary>
        /// Busca todos los nombres de los archivos
        /// Empresa Periodo.
        /// </summary>
        /// <returns>Listado de Archivos</returns>
        public static List<string> GetFiles()
        {

            return new List<string>();
        }

        public static string GetStdName(string name, DateTime fInicio, DateTime fFinal)
        {
            string nuevoNombre;
            nuevoNombre = name.TrimEnd();
            nuevoNombre += "_";
            nuevoNombre += fInicio.ToShortDateString().Replace('/','-');
            nuevoNombre += "_";
            nuevoNombre += fFinal.ToShortDateString().Replace('/', '-');
            nuevoNombre += ".erc";
            return nuevoNombre;
        }

        public static void unTanslateStdName(string fileName, out string name, out DateTime fInicio, out DateTime fFinal)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string[] parts = fileName.Split('_');
            //3 partes: 0: name, 1: fechaInicio, 2: fechaFinal
            name = parts[0];
            fInicio = Convert.ToDateTime(parts[1].Replace('-', '/'));
            fFinal = Convert.ToDateTime(parts[2].Replace('-', '/'));
        }

        public static string GetNextBackupNameAvailable(string fullPath)
        {
            string directorio = Path.GetDirectoryName(fullPath);
            string name = Path.GetFileNameWithoutExtension(fullPath);
            int num =1;
            string posibleNombre = directorio + "\\" + name + ".bk" + num.ToString();

            while (File.Exists(posibleNombre))
            {
                num++;
                posibleNombre = directorio + "\\" + name + ".bk" + num.ToString();
            }
            return posibleNombre;
        }

    }

    public class Empresa
    { 
        public string Name;
        public DateTime FechaInicio;
        public DateTime FechaFinal;
        public string FileName;
        public string FullPath;

        public Empresa(string name, DateTime fInicio, DateTime fFinal, string fullPath)
        {
            Name = name;
            FechaInicio = fInicio;
            FechaFinal = fFinal;
            FileName = EmpresaArchivo.GetStdName(name, fInicio, fFinal);
            FullPath = fullPath;
        }

    }
}
