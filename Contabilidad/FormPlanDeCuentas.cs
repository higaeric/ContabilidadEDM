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
            //lvCuentas.Clear();
            cbFamilia.Items.Clear();

            ////ListView lvCuentas
            //lvCuentas.View = View.Details;
            //lvCuentas.Columns.Add("Codigo",70);
            //lvCuentas.Columns.Add("Descripcion",246);
            //lvCuentas.MultiSelect = false;
            //lvCuentas.FullRowSelect = true;
            //lvCuentas.GridLines = true;

            //foreach (KeyValuePair<int, EDM.Entity.Cuenta> kvp in edm.PDC.planDeCuentas)
            //{ 
            //    ListViewItem lvitem = new ListViewItem();
            //    lvitem.Name = kvp.Value.Nombre;
            //    lvitem.Text = kvp.Key.ToString();
            //    lvitem.SubItems.Add(kvp.Value.Nombre);
            //    lvCuentas.Items.Add(lvitem);
            //}
            //lvCuentas.Sorting = SortOrder.Ascending;
            //lvCuentas.Sort();
            //coloreaListView();

            //ComboBox cbFamilia
            cbFamilia.Items.Add("Serie 1000");
            cbFamilia.Items.Add("Serie 2000");
            cbFamilia.Items.Add("Serie 3000");
            cbFamilia.Items.Add("Serie 4000");
            cbFamilia.SelectedIndex = 0;

            loadComboCuentaPadre();

            loadTreevieCuentas();
        }

        private void loadTreevieCuentas()
        {
            tvCuentas.Nodes.Clear();
            tvCuentas.Nodes.Add("Cuentas","Cuentas");
            tvCuentas.Nodes[0].Nodes.Add("1000", "Serie 1000");
            tvCuentas.Nodes[0].Nodes.Add("2000", "Serie 2000");
            tvCuentas.Nodes[0].Nodes.Add("3000", "Serie 3000");
            tvCuentas.Nodes[0].Nodes.Add("4000", "Serie 4000");

            //--------------- Busca la serie 1000 y que no sea Subcuenta.--------------
            var s1 = (from c in edm.PDC.planDeCuentas.Values
                      where (c.Codigo > 999 && c.Codigo < 2000)  && c.CuentaPadre<1000
                      select c).OrderBy(k => k.Nombre);
            
            foreach (EDM.Entity.Cuenta oItem in s1)
            {
                tvCuentas.Nodes[0].Nodes["1000"].Nodes.Add(oItem.Codigo.ToString(), "[" + oItem.Codigo + "] " + oItem.Nombre);
            }

            //--------------- Busca la serie 2000 y que no sea Subcuenta.--------------
            var s2 = (from c in edm.PDC.planDeCuentas.Values
                      where (c.Codigo > 1999 && c.Codigo < 3000) && c.CuentaPadre < 1000
                      select c).OrderBy(k => k.Nombre);

            foreach (EDM.Entity.Cuenta oItem in s2)
            {
                tvCuentas.Nodes[0].Nodes["2000"].Nodes.Add(oItem.Codigo.ToString(), "[" + oItem.Codigo + "] " + oItem.Nombre);
            }

            //--------------- Busca la serie 3000 y que no sea Subcuenta.--------------
            var s3 = (from c in edm.PDC.planDeCuentas.Values
                      where (c.Codigo > 2999 && c.Codigo < 4000) && c.CuentaPadre<1000
                      select c).OrderBy(k => k.Nombre);

            foreach (EDM.Entity.Cuenta oItem in s3)
            {
                tvCuentas.Nodes[0].Nodes["3000"].Nodes.Add(oItem.Codigo.ToString(), "[" + oItem.Codigo + "] " + oItem.Nombre);
            }

            //--------------- Busca la serie 4000 y que no sea Subcuenta.--------------
            var s4 = (from c in edm.PDC.planDeCuentas.Values
                      where (c.Codigo > 3999) && c.CuentaPadre < 1000
                      select c).OrderBy(k => k.Nombre);

            foreach (EDM.Entity.Cuenta oItem in s4)
            {
                tvCuentas.Nodes[0].Nodes["4000"].Nodes.Add(oItem.Codigo.ToString(), "[" + oItem.Codigo + "] " + oItem.Nombre);
            }

            //-------------------------- Busca Subcuenta.------------------------------
            var ss = (from c in edm.PDC.planDeCuentas.Values
                      where c.CuentaPadre > 999
                      select c).OrderBy(k => k.Nombre);
            foreach (EDM.Entity.Cuenta oItem in ss)
            {
                TreeNode tnode = tvCuentas.Nodes.Find(oItem.CuentaPadre.ToString(), true).First();
                tnode.Nodes.Add(oItem.Codigo.ToString(), "[" + oItem.Codigo + "] " + oItem.Nombre);
            }

            tvCuentas.ExpandAll();
        }

        private void loadComboCuentaPadre()
        {
            List<EDM.Entity.Cuenta> cuentaList = new List<EDM.Entity.Cuenta>();

            foreach (EDM.Entity.Cuenta oCuenta in edm.PDC.planDeCuentas.Values)
            {
                //Cargo unicamente las cuentas padres.
                if(oCuenta.CuentaPadre==0)
                    cuentaList.Add(oCuenta);
            }

            cbCuentaPadre.DisplayMember = "Codigo_Nombre";
            cbCuentaPadre.ValueMember = "Codigo";
            cbCuentaPadre.DataSource = cuentaList;
        }

        private int nextID(string familia)
        {
            string actual = "";
            int mayorId = 0;
            if (familia.Contains("1000")) actual = "1";
            else if (familia.Contains("2000")) actual = "2";
            else if (familia.Contains("3000")) actual = "3";
            else if (familia.Contains("4000")) actual = "4";

            //foreach (ListViewItem lvitem in lvCuentas.Items)
            //{
            //    if (lvitem.Text.StartsWith(actual))
            //        if (Convert.ToInt32(lvitem.Text) > mayorId)
            //            mayorId = Convert.ToInt32(lvitem.Text);
            //}

            var posibleMayor = (from c in edm.PDC.planDeCuentas
                                where c.Key.ToString().StartsWith(actual)
                                select c.Value.Codigo).ToArray();

            if (posibleMayor.Length > 0)
                mayorId = posibleMayor.Max();

            foreach (KeyValuePair<int, EDM.Entity.Cuenta> kvp in edm.PDC.planDeCuentasInactivas)
            {
                if (kvp.Key.ToString().StartsWith(actual))
                    if (kvp.Key > mayorId)
                        mayorId = kvp.Key;
            }

            if (mayorId == 0)
            {
                switch (actual)
                { 
                    case "1":
                        mayorId = 1000;
                        break;
                    case "2":
                        mayorId = 2000;
                        break;
                    case "3":
                        mayorId = 3000;
                        break;
                    case "4":
                        mayorId = 4000;
                        break;
                }
            }

            return mayorId + 1;
        }

        private void coloreaListView()
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
            int seleccionar = 0;
            if (modified == false)
                seleccionar = InsertarNueavaCuenta();
            else
                guardarTodo();

            MainForm.pdcChanged = true;

            txtDescription.Text = "";

            try
            {
                TreeNode tnode = tvCuentas.Nodes.Find(seleccionar.ToString(), true).First();
                tvCuentas.SelectedNode = tnode;
            }
            catch { }
        }

        private void tvCuentas_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void tvCuentas_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                tvCuentas.SelectedNode = e.Node;

                if (e.Node.Name == "Cuentas" || e.Node.Name == "1000" || e.Node.Name == "2000" ||
                    e.Node.Name == "3000" || e.Node.Name == "4000")
                    return;

                ContextMenu myContenxtMenu = new ContextMenu();
                MenuItem menuitem1 = new MenuItem("Modificar");
                MenuItem menuitem2 = new MenuItem("Eliminar");

                myContenxtMenu.MenuItems.Clear();
                myContenxtMenu.MenuItems.Add(menuitem1);
                myContenxtMenu.MenuItems.Add(menuitem2);
                myContenxtMenu.Show(tvCuentas, e.Location, LeftRightAlignment.Right);

                menuitem1.Click += new System.EventHandler(this.menuitem1_Click);
                menuitem2.Click += new System.EventHandler(this.menuitem2_Click);
            }
        }

        private void lvCuentas_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    ContextMenu myContenxtMenu = new ContextMenu();
            //    MenuItem menuitem1 = new MenuItem("Modificar");
            //    MenuItem menuitem2 = new MenuItem("Eliminar");

            //    myContenxtMenu.MenuItems.Clear();
            //    myContenxtMenu.MenuItems.Add(menuitem1);
            //    myContenxtMenu.MenuItems.Add(menuitem2);
            //    myContenxtMenu.Show(lvCuentas, e.Location, LeftRightAlignment.Right);

            //    menuitem1.Click += new System.EventHandler(this.menuitem1_Click);
            //    menuitem2.Click += new System.EventHandler(this.menuitem2_Click);
            //}
        }

        //Modificar

        private void tvCuentas_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', ',', '!' }) == -1)
                    {
                        // Stop editing without canceling the label change.
                        e.Node.EndEdit(false);
                        modificarCuenta(e.Node.Name, e.Label);
                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        e.CancelEdit = true;
                        MessageBox.Show("Existe caracteres invalidos.\n" +
                           "Los caracteres invalidos son: '@', ',', '!'",
                           "Modificacion de Cuenta");
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    /* Cancel the label edit action, inform the user, and 
                       place the node in edit mode again. */
                    e.CancelEdit = true;
                    //MessageBox.Show("Invalid tree node label.\nThe label cannot be blank",
                    //   "Node Label Edit");
                    e.Node.BeginEdit();
                }
            }
        }


        private void menuitem1_Click(object sender, EventArgs e)
        {
            modificarCuentaDisponible();
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

        private int InsertarNueavaCuenta()
        { 
            int id = nextID(cbFamilia.Text);
            int idPadre = 0;
            if(checkBoxSubCuenta.Checked)
            {
                idPadre = (Int32)cbCuentaPadre.SelectedValue;
            }

            bool resultado = edm.PDC.AddCuenta(id, txtDescription.Text, idPadre);
            if (!resultado)
                MessageBox.Show("No se ha podido Ingresar, hubo un problema con el archivo.", "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            load();
            
            return id;
        }

        private void modificarCuentaDisponible()
        {
            //lvCuentas.LabelEdit = true;

            TreeNode mySelectedNode = tvCuentas.SelectedNode;

            if (mySelectedNode != null && mySelectedNode.Parent != null)
            {
                tvCuentas.SelectedNode = mySelectedNode;
                tvCuentas.LabelEdit = true;
                if (!mySelectedNode.IsEditing)
                {
                    mySelectedNode.BeginEdit();
                }
            }
        }

        private void modificarCuenta(string idNode, string text)
        {
            int id = Convert.ToInt32(idNode);   //tvCuentas.SelectedNode.Name);
            EDM.Entity.Cuenta oCuenta = edm.PDC.planDeCuentas[id];
            edm.PDC.ModificarCuenta(id, text, oCuenta.CuentaPadre);
        }

        private void eliminarCuenta()
        {
            if (DialogResult.Yes !=
                MessageBox.Show("Esta seguro que desea eliminar la cuenta", "Eliminar Cuenta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                return;

            int id = Convert.ToInt32(tvCuentas.SelectedNode.Name); //lvCuentas.SelectedItems[0].Text);
            edm.PDC.EliminarCuenta(id);

            var subcuentas = from c in edm.PDC.planDeCuentas
                             where c.Value.CuentaPadre == id
                             select c.Value;
            foreach (EDM.Entity.Cuenta sub in subcuentas)
                sub.CuentaPadre = 0;

            load();           
        }

        private void guardarTodo()
        {
            edm.PDC.GuardarCompleto();
        }

        private void checkBoxSubCuenta_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSubCuenta.Checked)
            {
                cbCuentaPadre.Enabled = true;
                cbFamilia.Enabled = false;
                cambiarComboFamiliaSegunComboPadre();
            }
            else
            {
                cbCuentaPadre.Enabled = false;
                cbFamilia.Enabled = true;
            }
        }

        private void cbCuentaPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambiarComboFamiliaSegunComboPadre();
        }

        private void cambiarComboFamiliaSegunComboPadre()
        {
            try
            {
                EDM.Entity.Cuenta currentCuenta = (EDM.Entity.Cuenta)cbCuentaPadre.SelectedItem;
                int serie = currentCuenta.Codigo / 1000;

                cbFamilia.SelectedIndex = serie - 1;
            }
            catch { }
        }




    }
}
