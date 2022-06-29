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
    public partial class FormAsientosPredeterminados : Form
    {
        public List<ListViewItem> registrosPredeterminado = new List<ListViewItem>();
        EDM.EDM edm;
        int numeroActual;
        private Control[] Editors;
        System.Windows.Forms.ComboBox cbFechas;

        public FormAsientosPredeterminados(System.Windows.Forms.ComboBox cbFechas_, EDM.EDM edm_, int numeroActual_)
        {
            InitializeComponent();
            edm = edm_;
            registrosPredeterminado.Clear();
            loadComboOpciones();
            prepareLVcolumns();
            cbFechas = cbFechas_;
            foreach (string str in cbFechas.Items)
                comboBox1.Items.Add(str);
            numeroActual = numeroActual_;

            listViewEx1.SubItemClicked += new ListViewEx.SubItemEventHandler(listViewEx1_SubItemClicked);
            listViewEx1.SubItemEndEditing += new ListViewEx.SubItemEndEditingEventHandler(listViewEx1_SubItemEndEditing);
        }

        private void loadComboOpciones()
        {
            cbOpciones.Items.Add("Mercaderias, Iva Debito Fiscal, Ret IVA, a Caja");
            cbOpciones.Items.Add("Caja, a Iva Debito Fiscal, a Ventas");
            cbOpciones.Items.Add("Sueldos, Cargas Sociales, a Caja");

            cbOpciones.Text = "Seleccione un Item...";
        }

        private void prepareLVcolumns()
        {
            listViewEx1.View = View.Details;
            listViewEx1.GridLines = true;
            listViewEx1.Columns.Add("Nro.", 40);
            listViewEx1.Columns.Add("Fecha", 100);
            listViewEx1.Columns.Add("Cod.", 60);
            listViewEx1.Columns.Add("Description", 200);
            listViewEx1.Columns.Add("Debe", 100);
            listViewEx1.Columns.Add("Haber", 100);
            listViewEx1.Columns.Add("idReg", 0);

            Editors = new Control[] {
                                    null,
                                    comboBox1,
                                    null,
                                    null,
                                    numericUpDown1,
                                    numericUpDown1
            };
        }

        #region "-------- Eventos -------"
        private void listViewEx1_SubItemClicked(object sender, ListViewEx.SubItemEventArgs e)
        {
            //if (e.SubItem == 3) // Password field
            //{
            //    // the current value (text) of the subitem is ****, so we have to provide
            //    // the control with the actual text (that's been saved in the item's Tag property)
            //    e.Item.SubItems[e.SubItem].Text = e.Item.Tag.ToString();
            //}

            if(e.SubItem==4 || e.SubItem==5 || e.SubItem==1)
                listViewEx1.StartEditing(Editors[e.SubItem], e.Item, e.SubItem);
        }

        private void listViewEx1_SubItemEndEditing(object sender, ListViewEx.SubItemEndEditingEventArgs e)
        {
            //if (e.SubItem == 3) // Password field
            //{
            //    if (e.Cancel)
            //    {
            //        e.DisplayText = new string(textBoxPassword.PasswordChar, e.Item.Tag.ToString().Length);
            //    }
            //    else
            //    {
            //        // in order to display a series of asterisks instead of the plain password text
            //        // (textBox.Text _gives_ plain text, after all), we have to modify what'll get
            //        // displayed and save the plain value somewhere else.
            //        string plain = e.DisplayText;
            //        e.DisplayText = new string(textBoxPassword.PasswordChar, plain.Length);
            //        e.Item.Tag = plain;
            //    }
            //}
            if (e.SubItem == 1)
            {
                string valor = e.DisplayText;
                foreach (ListViewItem item in listViewEx1.Items)
                {
                    item.SubItems[1].Text = valor;
                }
            }
        }

        //private void checkBoxDoubleClickActivation_CheckedChanged(object sender, System.EventArgs e)
        //{
        //}

        private void listViewEx1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // To show the real password (remember, the subitem's Text _is_ '*******'),
            // set the tooltip to the ListViewItem's tag (that's where the password is stored)
            //ListViewItem item;
            //int idx = listViewEx1.GetSubItemAt(e.X, e.Y, out item);
            //if (item != null && idx == 3)
            //    toolTip1.SetToolTip(listViewEx1, item.Tag.ToString());
            //else
            //    toolTip1.SetToolTip(listViewEx1, null);
        }

        private void cbOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewEx1.Items.Clear();
            ingresarRegistroPredeterminado();
        }
        #endregion

        private void ingresarRegistroPredeterminado()
        {
            if (cbOpciones.SelectedItem.ToString() == "Seleccione un Item...") { cbOpciones.Focus(); return; }

            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

                EDM.Entity.Asiento currentAsiento = new EDM.Entity.Asiento(numeroActual,
                                                        new List<EDM.Entity.Registro>(),
                                                        Convert.ToDateTime(cbFechas.Text));

                string[] cuentas = cbOpciones.Text.Split(new Char[]{ ','}, StringSplitOptions.RemoveEmptyEntries);
                ListViewItem lvi;
                int cod = 0;
                for(int i = 0; i<cuentas.Length;i++)
                {
                    string cuenta = cuentas[i].Trim();
                    bool aCredito = false;
                    if (cuenta.StartsWith("a "))
                    {
                        aCredito = true;
                        cuenta = cuenta.Remove(0, 2);
                    }
                    for (int j = 0; j < edm.PDC.planDeCuentas.Count; j++)
                    {
                        if (edm.PDC.planDeCuentas.ElementAt(j).Value.Nombre == cuenta)
                        {
                            cod = edm.PDC.planDeCuentas.ElementAt(j).Key;
                            break;
                        }
                    }

                    // Create sample ListView data.
                    lvi = new ListViewItem(currentAsiento.Numero.ToString());
                    lvi.SubItems.Add(cbFechas.Text);
                    lvi.SubItems.Add(cod.ToString());
                    lvi.SubItems.Add(cuenta);
                    if (!aCredito)
                    {
                        lvi.SubItems.Add("0");
                        lvi.SubItems.Add("");
                    }
                    else
                    {
                        lvi.SubItems.Add("");
                        lvi.SubItems.Add("0");
                    }
                    this.listViewEx1.Items.Add(lvi);
                }
            }
            catch
            {
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!isClosingAvailable())
            {
                MessageBox.Show("No se han cargado los valores o no cierra el asiento", "Error en Valores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (ListViewItem lvi in listViewEx1.Items)
            {
                registrosPredeterminado.Add(lvi);
            }
            this.Hide();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private bool isClosingAvailable()
        {
            double deb = 0;
            double hab = 0;
            foreach (ListViewItem lvi in listViewEx1.Items)
            {
                if (lvi.SubItems[4].Text!="")
                    deb = deb + Convert.ToDouble(lvi.SubItems[4].Text);
                if(lvi.SubItems[5].Text!="")
                    hab = hab + Convert.ToDouble(lvi.SubItems[5].Text);
            }
            if (deb == 0 && hab == 0)
                return false;

            double resultado = deb - hab;

            resultado = resultado < 0 ? -resultado : resultado;
            if (resultado == 0 || resultado < 0.01)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
