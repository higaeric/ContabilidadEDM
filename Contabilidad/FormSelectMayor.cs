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
    public partial class FormSelectMayor : Form
    {
        public int idCuenta = 0;
        public string nombreCuenta = "";
        Dictionary<int, EDM.Entity.Cuenta> pdc;

        public FormSelectMayor(Dictionary<int, EDM.Entity.Cuenta> pdc_)
        {
            InitializeComponent();
            pdc = pdc_;
            loadCombo();
        }

        private void loadCombo()
        {
            cbCuenta.Items.Clear();
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();

            foreach (KeyValuePair<int, EDM.Entity.Cuenta> kvp in pdc)
                stringCol.Add(kvp.Value.Nombre);

            //cargo datos del combobox
            BindingSource bs = new BindingSource();
            List<EDM.Entity.Cuenta> ls = pdc.Values.ToList<EDM.Entity.Cuenta>();
            Comparison<EDM.Entity.Cuenta> compnamedel = new Comparison<EDM.Entity.Cuenta>(EDM.Entity.Cuenta.CompareIDName); //EDM.Entity.Cuenta.CompareName);
            ls.Sort(compnamedel);

            bs.DataSource = ls;
            cbCuenta.DataSource = bs;
            cbCuenta.ValueMember = "Codigo";
            cbCuenta.DisplayMember = "Nombre";
            
            //cargo la lista de items para el autocomplete
            cbCuenta.AutoCompleteCustomSource = stringCol;
            cbCuenta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbCuenta.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(cbCuenta.Text=="")return;

            idCuenta = (Int32)cbCuenta.SelectedValue;
                //(from c in pdc
                //        where c.Value == cbCuenta.SelectedItem.ToString()
                //        select c.Key).First();

            nombreCuenta = cbCuenta.Text;
            this.Hide();
        }
    }
}
