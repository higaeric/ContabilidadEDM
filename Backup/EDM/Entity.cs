using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDM.Entity
{
    public class Asiento
    {
        public int Numero;
        public List<Registro> Registros;
        public DateTime Fecha;

        public bool isClosed = false;
        

        public Asiento(int numero, List<Registro> registros, DateTime fecha)
        {
            Numero = numero;
            Registros = registros;
            Fecha = fecha;
        }

        public void Open()
        {
            isClosed = false;
        }

        /// <summary>
        /// Calcula si exite Diferencia entre el Debe y el Haber
        /// </summary>
        /// <returns></returns>
        public double GetDiferencce()
        {
            double valorD = 0;
            double valorH = 0;
            foreach (Registro r in Registros)
            {
                if (r.valueType == ValueType.Debe)
                    valorD += r.Valor;
                else
                    valorH += r.Valor;
            }
            return valorH - valorD;
        }

        /// <summary>
        /// Recorre los registros y verifica si es posible cerrar el asiento
        /// </summary>
        /// <returns></returns>
        public bool allowClose()
        {
            if(GetDiferencce()!= 0)
                return false;
            return true;
        }

        public bool CloseAsiento()
        {
            this.isClosed = true;
            return true;
        }

        public Asiento Clone()
        {
            List<Registro> lRegistros = new List<Registro>();
            foreach (Registro reg in this.Registros)
                lRegistros.Add(new Registro(reg.idRegistro, reg.Codigo,
                    reg.Description, reg.valueType, reg.Valor));
            
            Asiento cloned = new Asiento(this.Numero, lRegistros, this.Fecha);
            cloned.isClosed = this.isClosed;
            return cloned;
        }
    }
    
    public class Registro
    {
        public long idRegistro = 0;
        public int Codigo = 0;
        public string Description = "";
        public ValueType valueType = ValueType.Debe;
        public double Valor = 0;

        public Registro(long id, int codigo, string description, ValueType type_, double valor)
        {
            idRegistro = id;
            Codigo = codigo;
            Description = description;
            valueType = type_;
            Valor = valor;
        }
    }

    public class Mayor
    {
        public int codigo;
        public string description;
        private double debe;
        private double haber;

        public double Debe 
        {
            get { return debe; }
            set { debe = value; }
        }

        public double Haber
        {
            get { return haber; }
            set { haber = value; }
        }

        public Mayor(int cod, string descr, double deb, double hab)
        {
            codigo = cod;
            description = descr;
            debe = deb;
            haber = hab;
        }
    }

    public enum ValueType
    {
        Debe,
        Haber
    }

    public class Saldo
    {
        private int id;
        private string description;
        private double acumD;
        private double acumH;
        private double saldoD;
        private double saldoH;
        private double totalD;
        private double totalH;
        //private double totalSaldoD;
        //private double totalSaldoH;

        public int Id
        {
            get { return id; }
        }
        public string Description
        {
            get { return description; }
        }
        public double AcumD
        {
            get { return acumD; }
            set { acumD += value; }
        }
        public double AcumH
        {
            get { return acumH; }
            set { acumH += value; }
        }
        public double SaldoD
        {
            get
            {
                if (acumD - acumH > 0)
                    return acumD - acumH;
                else
                    return 0;
            }
        }
        public double SaldoH
        {
            get
            {
                if (acumH - acumD > 0)
                    return acumH - acumD;
                else
                    return 0;
            }
        }


        public Saldo(int id_, string description_)
        {
            id = id_;
            description = description_;
            acumD = 0;
            acumH = 0;
            saldoD = 0;
            saldoH = 0;
        }
    }
}
