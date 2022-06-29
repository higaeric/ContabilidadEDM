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
    public partial class FormPlanDeCuentas : BasicForm
    {
        EDM.EDM edm;
        bool modified = false;

        public FormPlanDeCuentas(EDM.EDM edm_)
        {
            InitializeComponent();
            edm = edm_;
            load();
        }

        public FormPlanDeCuentas(int id, string description, tableType type)
            : base(id, description, type)
        {
            InitializeComponent();

        }

        private void load()
        {
            lvCuentas.Clear();
            cbFamilia.Items.Clear();

            //ListView lvCuentas
            lvCuentas.View = View.Details;
            lvCuentas.Columns.Add("Codigo",70);
            lvCuentas.Columns.Add("Descripcion",246);
            lvCuentas.MultiSelect = false;
            lvCuentas.FullRowSelect = true;
            lvCuentas.GridLines = true;

            foreach (KeyValuePair<int, string> kvp in edm.PDC.planDeCuentas)
            { 
                ListViewItem lvitem = new ListViewItem();
                lvitem.Name = kvp.Value;
                lvitem.Text = kvp.Key.ToString();
                lvitem.SubItems.Add(kvp.Value);
                lvCuentas.Items.Add(lvitem);
            }
            lvCuentas.Sorting = SortOrder.Ascending;
            lvCuentas.Sort();
            colorea();

            //ComboBox cbFamilia
            cbFamilia.Items.Add("Serie 1000");
            cbFamilia.Items.Add("Serie 2000");
            cbFamilia.Items.Add("Serie 3000");
            cbFamilia.Items.Add("Serie 4000");
            cbFamilia.SelectedIndex = 0;
        }

        private int nextID(string familia)
        {
            string actual = "";
            int mayorId = 0;
            if (familia.Contains("1000")) actual = "1";
            else if (familia.Contains("2000")) actual = "2";
            else if (familia.Contains("3000")) actual = "3";
            else if (familia.Contains("4000")) actual = "4";

            foreach (ListViewItem lvitem in lvCuentas.Items)
            {
                if (lvitem.Text.StartsWith(actual))
                    if (Convert.ToInt32(lvitem.Text) > mayorId)
                        mayorId = Convert.ToInt32(lvitem.Text);
            }
            foreach (KeyValuePair<int, string> kvp in edm.PDC.planDeCuentasInactivas)
            {
                if (kvp.Key.ToString().StartsWith(actual))
                    if (kvp.Key > mayorId)
                        mayorId = kvp.Key;
            }

            return mayorId + 1;
        }

        private void colorea()
        {
            foreach (ListViewItem lvitem in lvCuentas.Items)
            {
                if (lvitem.Text.StartsWith("1") || lvitem.Text.StartsWith("3"))
                    lvitem.BackColor = Color.LightGoldenrodYellow;
            }
        }

        #region "Controles y Eventos"
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (modified == false)
                InsertarNueavaCuenta();
            else
                guardarTodo();

            txtDescription.Text = "";
        }

        private void lvCuentas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu myContenxtMenu = new ContextMenu();
                MenuItem menuitem1 = new MenuItem("Modificar");
                MenuItem menuitem2 = new MenuItem("Eliminar");

                myContenxtMenu.MenuItems.Clear();
                myContenxtMenu.MenuItems.Add(menuitem1);
                myContenxtMenu.MenuItems.Add(menuitem2);
                myContenxtMenu.Show(lvCuentas, e.Location, LeftRightAlignment.Right);

                menuitem1.Click += new System.EventHandler(this.menuitem1_Click);
                menuitem2.Click += new System.EventHandler(this.menuitem2_Click);
            }
        }

        //Modificar
        private void menuitem1_Click(object sender, EventArgs e)
        {
            modificarCuenta();
        }
        
        //Eliminar
        private void menuitem2_Click(object sender, EventArgs e)
        {
            eliminarCuenta();
        }

        private void lvCuentas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                
            }
        }
        #endregion

        private void InsertarNueavaCuenta()
        { 
            int id = nextID(cbFamilia.Text);
            bool resultado = edm.PDC.AddCuenta(id, txtDescription.Text);
            if (!resultado)
                MessageBox.Show("No se ha podido Ingresar, hubo un problema con el archivo.", "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            load();
        }

        private void modificarCuenta()
        {
            //modified = true;
            //habilitar modificar la description:
            lvCuentas.LabelEdit = true;
            
        }

        private void eliminarCuenta()
        {
            if (DialogResult.Yes !=
                MessageBox.Show("Esta seguro que desea eliminar la cuenta", "Eliminar Cuenta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                return;

            int id = Convert.ToInt32(lvCuentas.SelectedItems[0].Text);
            edm.PDC.EliminarCuenta(id);
            load();           
        }

        private void guardarTodo()
        {
            edm.PDC.GuardarCompleto();
        }

    }
}
