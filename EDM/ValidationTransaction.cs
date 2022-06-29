
namespace EDM.Validation
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [Flags]
    public enum ValidationTransactionType
    {
        UNDEFINED,
        EMPRESA,
        INI
    }


    public class ValidationTransaction
    {
        public ValidationTransactionType Type = ValidationTransactionType.UNDEFINED;
        public string Manufacturer, Release, Build, Server;

        public Dictionary<string, string> Header = new Dictionary<string, string>();
        public List<string> Body = new List<string>();
        public string Description;

        public bool InError = false;
        public List<string> ErrorLog = new List<string>();

        public string Name
        {
            get { return Type.ToString(); }
        }

        public ValidationTransaction(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                bool passedHeader = false;
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();
                    if (line.Trim() != "")
                    {
                        if (!passedHeader)
                        {
                            string[] kvp = line.Split('=');
                            if (kvp[0] == "DATA")
                            {
                                passedHeader = true;
                            }
                            else if (kvp[0] == "TRANS")
                            {
                                try
                                {
                                    Type = (ValidationTransactionType)Enum.Parse(typeof(ValidationTransactionType), kvp[1]);
                                }
                                catch
                                {
                                    InError = true;
                                    ErrorLog.Add("Unsupported TRANS code " + kvp[1]);
                                }
                            }
                            else
                            {
                                if (!Header.ContainsKey(kvp[0]))
                                {
                                    Header.Add(kvp[0], kvp[1]);
                                }
                                else
                                {
                                    InError = true;
                                    ErrorLog.Add("Duplicated tag " + kvp[0]);
                                }
                            }
                        }
                        else
                        {
                            Body.Add(line);
                        }
                    }
                }

                if (Type == ValidationTransactionType.UNDEFINED)
                {
                    InError = true;
                    ErrorLog.Add("Undefined TRANS code");
                }
                if (Body.Count == 0)
                {
                    InError = true;
                    ErrorLog.Add("Empty transaction body");
                }
            }
        }

        public ValidationTransaction(ValidationTransactionType t)
        {
            Type = t;
            //Server = Welcome.CurrentServer;
            //Manufacturer = Welcome.CurrentManufacturer;
            //Release = Welcome.CurrentRelease;
            //Build = Welcome.CurrentBuild;

            // Prepare Header
            switch (t)
            {
                case ValidationTransactionType.EMPRESA:
                    Description = "Creacion de archivo Empresa Periodo";
                    Header = new Dictionary<string, string>
                    {
                        { "TRANS", t.ToString()},
                        { "MSG", Description},
                        { "START", ""},
                        { "EMPRESA", ""},
                        { "FECHAINICIO", ""},
                        { "FECHAFIN", ""},
                        {"TRL", ""}
                    };
                    break;
                case ValidationTransactionType.INI:
                    Description = "Archivo inicial";
                    Header = new Dictionary<string, string>
                    {
                        {"TRANS", t.ToString()},
                        {"MSG", Description},
                        {"MA", ""},
                        {"FECHA",""},
                        {"TRL",""}
                    };
                    break;
            }
            Header.Add("DATA", "");
        }


    }
}
