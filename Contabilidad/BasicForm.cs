using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Contabilidad
{
    public partial class BasicForm : Form
    {
        public int formID;
        public string formDescription;
        public tableType type;

        public BasicForm()
        {
            //InitializeComponent();
            formDescription = "NONE";
            type = tableType.Otro;
            formID = 9999;
        }
        public BasicForm(int ID, string description, tableType type_)
        {
            formID = ID;
            formDescription = description;
            type = type_;

            InitializeComponent();
        }


    }

    public enum tableType
    { 
        PlanDeCuenta,
        Asientos,
        Mayor,
        Saldos,
        Otro
    }
}
