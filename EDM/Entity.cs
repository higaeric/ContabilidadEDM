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
        public double GetDiference()
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
            if(GetDiference()!= 0)
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
        public string Details = "";
        public bool isHidden = false;

        public Registro(long id, int codigo, string description, ValueType type_, double valor, string details_ = "", bool isHidden_ = false)
        {
            idRegistro = id;
            Codigo = codigo;
            Description = description;
            valueType = type_;
            Valor = valor;
            Details = details_;
            isHidden = isHidden_;
        }
    }

    public class Mayor
    {
        public int numAsiento;
        public DateTime fecha;
        public int codigo;
        public string nombre;
        public string description;
        private string details;
        private double debe;
        private double haber;

        public int NumAsiento
        {
            get { return numAsiento; }
            set { numAsiento = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Details
        {
            get { return details; }
            set { details = value; }
        }

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

        public Mayor(int numAsiento_, DateTime fecha_, int cod, string nombreCuenta_, string descr, double deb, double hab, string detail_ = "")
        {
            numAsiento = numAsiento_;
            fecha = fecha_;
            codigo = cod;
            nombre = nombreCuenta_;
            description = descr;
            debe = deb;
            haber = hab;
            details = detail_;
        }
    }

    /// <summary>
    /// Para visualizar los datos adicionales en la tabla Mayor.
    /// </summary>
    public class RegistroMayor: Registro
    {
        public int NumeroAsiento;
        public DateTime Fecha;

        public RegistroMayor(int numAsiento_, DateTime fecha_, long id, int codigo, string description, ValueType type_, double valor, string details_ = "", bool isHidden_ = false): base(id, codigo, description,type_, valor, details_,isHidden_)
        {
            NumeroAsiento = numAsiento_;
            Fecha = fecha_;
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
        private string detalle;
        private double acumD;
        private double acumH;
        private double saldoD;
        private double saldoH;
        //private double totalD;
        //private double totalH;
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
        public string Detalle
        {
            get { return detalle; }
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


        public Saldo(int id_, string description_, string detalle_ = "")
        {
            id = id_;
            description = description_;
            detalle = detalle_;
            acumD = 0;
            acumH = 0;
            saldoD = 0;
            saldoH = 0;
        }
    }

    public class Cuenta
    {
        private int codigo_ = 0;
        private string nombre_ = "";
        private bool activo_ = true;
        private int cuentaPadre_ = 0;

        public int Codigo { get { return codigo_; } set { codigo_ = value; } }
        public string Nombre { get { return nombre_; } set { nombre_ = value; } }
        public bool Activo { get { return activo_; } set { activo_ = value; } }
        public int CuentaPadre { get { return cuentaPadre_; } set { cuentaPadre_ = value; } }
        public string Codigo_Nombre { get { return codigo_.ToString() + " - " + nombre_; } }

        public Cuenta(int codigo, string nombre, bool activo, int cuentaPadre)
        {
            codigo_ = codigo;
            nombre_ = nombre;
            activo_ = activo;
            cuentaPadre_ = cuentaPadre;
        }

        public override string ToString()
        {
            return Nombre; //base.ToString();
        }



        public static int CompareName(Cuenta c1, Cuenta c2)
        {
            return c1.nombre_.CompareTo(c2.nombre_);
        }

        public static int CompareIDName(Cuenta c1, Cuenta c2)
        {
            return c1.Codigo_Nombre.CompareTo(c2.Codigo_Nombre);
        }

    }
}
