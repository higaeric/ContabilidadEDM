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
        Dictionary<int, string> pdc;

        public FormSelectMayor(Dictionary<int, string> pdc_)
        {
            InitializeComponent();
            pdc = pdc_;
            loadCombo();
        }

        private void loadCombo()
        {
            cbCuenta.Items.Clear();
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();

            foreach (KeyValuePair<int, string> kvp in pdc)
                stringCol.Add(kvp.Value);

            //cargo datos del combobox
            BindingSource bs = new BindingSource();
            List<string> ls = pdc.Values.ToList<string>();
            ls.Sort();
            bs.DataSource = ls;
            cbCuenta.DataSource = bs;
            
            //cargo la lista de items para el autocomplete
            cbCuenta.AutoCompleteCustomSource = stringCol;
            cbCuenta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbCuenta.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(cbCuenta.Text=="")return;

            idCuenta = (from c in pdc
                        where c.Value == cbCuenta.SelectedItem.ToString()
                        select c.Key).First();

            nombreCuenta = cbCuenta.Text;
            this.Hide();
        }
    }
}
