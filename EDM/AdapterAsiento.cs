using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDM.Entity;

namespace EDM
{
    public class AdapterAsiento
    {
        public List<Asiento> Asientos = new List<Asiento>();


        public AdapterAsiento()
        {
            Asientos = new List<Asiento>();
        }

        public void LoadAsientos(List<string> asientosFromfile)
        {
            int nAsiento = 0;
            List<Registro> registros = new List<Registro>();
            DateTime fecha = new DateTime();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            
            for(int i=0; i<asientosFromfile.Count; i++)
            {
                string[] str = asientosFromfile[i].Split('\t');
                //0:idRegistro, 1:Nro.Asiento, 2:Fecha, 3:cod, 4:Desc., 5:D/H, 6:Valor, 7:Hide, 8:Detalle 

                if (nAsiento != Convert.ToInt32(str[1]))
                {
                    if (i != 0)
                    {
                        Asientos.Add(new Asiento(nAsiento, registros, fecha));
                    }
                    nAsiento = Convert.ToInt32(str[1]);
                    fecha = Convert.ToDateTime(str[2]);
                    registros = new List<Registro>();
                }

                Entity.ValueType type = str[5]=="D"? Entity.ValueType.Debe: Entity.ValueType.Haber;

                bool ishidden = false;
                if (str.Length > 7)
                {
                    ishidden = Convert.ToBoolean(str[7]);
                }
                string detalles = "";
                if (str.Length > 8)
                {
                    detalles = str[8];
                }

                registros.Add(new Registro(Convert.ToInt32(str[0]),
                    Convert.ToInt32(str[3]), str[4], type, Convert.ToDouble(str[6]), detalles, ishidden));
            }
            //guardo el ultimo asiento.
            if(registros.Count>0)
                Asientos.Add(new Asiento(nAsiento, registros, fecha));
        }

    }
}
