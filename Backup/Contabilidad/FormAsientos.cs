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
    public partial class FormAsientos : BasicForm
    {
        EDM.EDM edm;
        EDM.Empresa currentEmpresa;
        List<EDM.Entity.Asiento> Asientos;
        bool nuevoAsiento = false;
        EDM.Entity.Asiento currentAsiento;
        int numeroActual;
        long maxIdReg;
        long idRegMod;
        bool modify = false;
        bool modified = false;
        bool modificarAsientoAbierto = false;

        public FormAsientos()
        {
            InitializeComponent();
            prepareLVcolumns();
        }

        public FormAsientos(int id, string description, tableType type)
            : base(id, description, type)
        {
            InitializeComponent();
            prepareLVcolumns();
        }

        public void loadPDC(EDM.EDM edm_)
        {
            edm = edm_;
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
            
            foreach (KeyValuePair<int, string> kvp in edm.PDC.planDeCuentas)
                stringCol.Add(kvp.Value); //cbCuenta.Items.Add(kvp.Value);

            //cargo datos del combobox
            BindingSource bs = new BindingSource();
            List<string> ls = edm.PDC.planDeCuentas.Values.ToList<string>();
            ls.Sort();
            bs.DataSource = ls;
            cbCuenta.DataSource = bs;

            //cargo la lista de items para el autocomplete
            cbCuenta.AutoCompleteCustomSource = stringCol;
            cbCuenta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbCuenta.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void loadComboFechas(DateTime fInicio, DateTime fFinal)
        {
            List<DateTime> lDates = calculateDates(fInicio, fFinal);
            foreach(DateTime d in lDates)
                cbFecha.Items.Add(d.ToShortDateString());
            cbFecha.SelectedIndex = 0;
        }

        private List<DateTime> calculateDates(DateTime fInicio, DateTime fFinal)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            List<DateTime> lDates = new List<DateTime>();
            int ano = fInicio.Year;
            int mes = fInicio.Month;
            int dia = DateTime.DaysInMonth(fInicio.Year, fInicio.Month);
            DateTime fechaIng = new DateTime(fInicio.Year, fInicio.Month, dia);

            while(fechaIng< fFinal || fechaIng==fFinal)
            {
                lDates.Add(fechaIng);
                if (mes + 1 > 12)
                {
                    mes = 1;
                    ano++;
                }
                else
                    mes++;

                dia = DateTime.DaysInMonth(ano,mes);
                fechaIng = new DateTime(ano, mes, dia);
            }
            return lDates;
        }

        private void prepareLVcolumns()
        {
            lvAsientos.View = View.Details;
            lvAsientos.GridLines = true;
            lvAsientos.Columns.Add("Nro.",40);
            lvAsientos.Columns.Add("Fecha", 100);
            lvAsientos.Columns.Add("Cod.", 60);
            lvAsientos.Columns.Add("Description", 200);
            lvAsientos.Columns.Add("Debe", 100);
            lvAsientos.Columns.Add("Haber", 100);
            lvAsientos.Columns.Add("idReg", 0);

            //lvAsientos.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public void LoadDataInfo(EDM.Empresa empresa, List<EDM.Entity.Asiento> asientos)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            currentEmpresa = empresa;

            lbCliente.Text = empresa.Name;
            txtFechaInicial.Text = empresa.FechaInicio.ToShortDateString();
            txtFechaFinal.Text = empresa.FechaFinal.ToShortDateString();
            txtFechaInicial.Enabled = false;
            txtFechaFinal.Enabled = false;

            Asientos = asientos;
            refreshLvAsientos();
            numeroActual = maxNumber() + 1;
            txtAsientoActual.Text = numeroActual.ToString();
            lbDiferencia.Text = "0";
            loadComboFechas(empresa.FechaInicio, empresa.FechaFinal);
            nuevoAsiento = true;
            getMaxIdReg();
        }

        public void refreshLvAsientos()
        {
            lvAsientos.Items.Clear();
            Color color = Color.LightYellow;
            Asientos.Sort(delegate(EDM.Entity.Asiento c1, EDM.Entity.Asiento c2)
            {
                return c1.Fecha.CompareTo(c2.Fecha);
            });

            foreach (EDM.Entity.Asiento aItem in Asientos)
            {
                if (color.Name == Color.LightYellow.Name)
                    color = Color.Lavender;
                else
                    color = Color.LightYellow;
                var debSort = aItem.Registros.Where(c => c.valueType == EDM.Entity.ValueType.Debe);
                var habSort = aItem.Registros.Where(c => c.valueType == EDM.Entity.ValueType.Haber);

                foreach (EDM.Entity.Registro reg in debSort)
                {
                    ListViewItem lvItem = lvitemToAdd(ref color, aItem, reg);
                    lvAsientos.Items.Add(lvItem);
                }
                foreach (EDM.Entity.Registro reg in habSort)
                {
                    ListViewItem lvItem = lvitemToAdd(ref color, aItem, reg);
                    lvAsientos.Items.Add(lvItem);
                }
            }
        }

        private static ListViewItem lvitemToAdd(ref Color color, EDM.Entity.Asiento aItem, EDM.Entity.Registro reg)
        {
            ListViewItem lvItem = new ListViewItem();
            lvItem.BackColor = color;
            lvItem.Name = reg.idRegistro.ToString();
            lvItem.Text = aItem.Numero.ToString();
            lvItem.SubItems.Add(aItem.Fecha.ToShortDateString());
            lvItem.SubItems.Add(reg.Codigo.ToString());
            lvItem.SubItems.Add(reg.Description);
            if (reg.valueType == EDM.Entity.ValueType.Debe)
            {
                lvItem.SubItems.Add("$" + reg.Valor.ToString("N2"));
                lvItem.SubItems.Add("");
            }
            else
            {
                lvItem.SubItems.Add("");
                lvItem.SubItems.Add("$" + reg.Valor.ToString("N2"));
            }
            lvItem.SubItems.Add(reg.idRegistro.ToString());
            return lvItem;
        }

        

        #region "Botones y Controles"
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (!modify)
                ingresarRegistro();
            else
            {
                //if (modificarAsientoAbierto)
                //    actualizaAsiento(true);
                //else
                    actualizaAsiento(false);
            }
        }

        private void btnCerrarAsiento_Click(object sender, EventArgs e)
        {
            cerrarAsiento();
        }

        private void toolStripBtnEliminar_Click(object sender, EventArgs e)
        {
            if (lvAsientos.SelectedItems == null) return;
            if (currentAsiento != null && currentAsiento.isClosed == false)
            {
                if (currentAsiento.Numero != Convert.ToInt32(lvAsientos.SelectedItems[0].SubItems[0].Text))
                {
                    MessageBox.Show("Es necesario cerrar el asiento actual antes de Eliminar un registro de otro asiento.",
                        "Elimina Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    modificarAsientoAbierto = true;
                    eliminarRegistro();
                }
            }
            else
            {
                for (int i = 0; i < Asientos.Count; i++)
                {
                    if (Asientos[i].Numero == Convert.ToInt32(lvAsientos.SelectedItems[0].SubItems[0].Text))//txtAsientoActual.Text))
                    {
                        currentAsiento = Asientos[i];
                        numeroActual = currentAsiento.Numero;
                        break;
                    }
                }
                eliminarRegistro();

                nuevoAsiento = false;
                modified = true;
                modify = false;
                modificarAsientoAbierto = false;
                currentAsiento.isClosed = false;
                cbFecha.SelectedItem = currentAsiento.Fecha.ToShortDateString(); //fecha
                cbFecha.Enabled = false;
                txtAsientoActual.Text = currentAsiento.Numero.ToString();
            }
            isClosingAvailable();
            refreshLvAsientos();
        }

        private void toolStripBtnModificar_Click(object sender, EventArgs e)
        {
            if (lvAsientos.SelectedItems == null) return;

            if (currentAsiento != null && currentAsiento.isClosed == false)
            {
                if (currentAsiento.Numero != Convert.ToInt32(lvAsientos.SelectedItems[0].SubItems[0].Text))
                {
                    MessageBox.Show("Es necesario cerrar el asiento actual antes de modificar otro.",
                        "Modificación de Asiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    modificarAsientoAbierto = true;
                    modificarAsiento();
                }
            }
            else
            {
                modificarAsientoAbierto = false;
                modificarAsiento();
                for(int i=0; i<Asientos.Count;i++)
                {
                    if (Asientos[i].Numero== Convert.ToInt32(txtAsientoActual.Text))
                    {
                        currentAsiento = Asientos[i];
                        numeroActual = currentAsiento.Numero;
                        break;
                    }
                }
                currentAsiento.isClosed = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (isClosingAvailable())
            {
                currentAsiento.isClosed = true;
                numeroActual = maxNumber() + 1;
            }
            volverAlEstadoDeIngreso();
        }
        #endregion

        private void ingresarRegistro()
        {
            if (cbFecha.Text == "") { cbFecha.Focus(); return; }
            if (cbCuenta.Text == "") { cbCuenta.Focus(); return; }
            if (numImporte.Value == 0) { numImporte.Focus(); return; }
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                
                int cod=0;
                for (int i = 0; i < edm.PDC.planDeCuentas.Count; i++)
                {
                    if (edm.PDC.planDeCuentas.ElementAt(i).Value == cbCuenta.SelectedItem.ToString())
                        cod = edm.PDC.planDeCuentas.ElementAt(i).Key;
                }               
                
                if (nuevoAsiento)
                {
                    currentAsiento = new EDM.Entity.Asiento(numeroActual,
                        new List<EDM.Entity.Registro>(),
                        Convert.ToDateTime(cbFecha.Text));
                    Asientos.Add(currentAsiento);
                    cbFecha.Enabled = false;
                    nuevoAsiento = false;
                }

                //Add registro
                EDM.Entity.ValueType type = rbDebe.Checked? EDM.Entity.ValueType.Debe: EDM.Entity.ValueType.Haber;
                maxIdReg++;
                currentAsiento.Registros.Add(new EDM.Entity.Registro(maxIdReg, cod, cbCuenta.Text, type, Convert.ToDouble(numImporte.Value)));

                isClosingAvailable();
                refreshLvAsientos();
                numImporte.Value = 0;
            }
            catch 
            {
                MessageBox.Show("Se ha intentado de ingresar incorrectamente.", "Ingreso incorrecto.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCuenta.Focus();
            }
        }

        private void ingresarRegistro(int cod, string nombreCuenta, EDM.Entity.ValueType type, double numImporte)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            //Add registro
            maxIdReg++;
            currentAsiento.Registros.Add(new EDM.Entity.Registro(maxIdReg, cod, nombreCuenta, type, numImporte));

            

        }

        private void modificarAsiento()
        {
            modify = true;
            ListViewItem lvitem = lvAsientos.SelectedItems[0];
            if (lvitem.SubItems[4].Text != "")    //debe
            {
                rbDebe.Checked = true;
                rbHaber.Checked = false;
                numImporte.Value = Convert.ToDecimal(lvitem.SubItems[4].Text.Remove(0,1));
            }
            else
            {
                rbDebe.Checked = false;
                rbHaber.Checked = true;
                numImporte.Value = Convert.ToDecimal(lvitem.SubItems[5].Text.Remove(0,1));
            }
            cbCuenta.SelectedItem = lvitem.SubItems[3].Text; //Description
            cbFecha.SelectedItem = (Convert.ToDateTime(lvitem.SubItems[1].Text)).ToShortDateString(); //fecha
            cbFecha.Enabled = false;
            txtAsientoActual.Text = lvitem.SubItems[0].Text;

            idRegMod = Convert.ToInt64(lvitem.SubItems[6].Text); //idregistro
            btnIngresar.Text = "Actualizar";
            btnCancelar.Visible = true;
            btnCerrarAsiento.Enabled = false;
        }

        private void actualizaAsiento(bool actual)
        {
            int cod = 0;
            for (int i = 0; i < edm.PDC.planDeCuentas.Count; i++)
            {
                if (edm.PDC.planDeCuentas.ElementAt(i).Value == cbCuenta.SelectedItem.ToString())
                    cod = edm.PDC.planDeCuentas.ElementAt(i).Key;
            }
            foreach (EDM.Entity.Registro reg in currentAsiento.Registros)
            {
                if (reg.idRegistro == idRegMod)
                {
                    EDM.Entity.ValueType type = rbDebe.Checked ? EDM.Entity.ValueType.Debe : EDM.Entity.ValueType.Haber;
                    reg.Codigo = cod;
                    reg.Description = cbCuenta.Text;
                    reg.valueType = type;
                    reg.Valor = Convert.ToDouble(numImporte.Value);

                    //no hace falta guardar en disco xq aun no esta cerrado
                    nuevoAsiento = isClosingAvailable();
                    //if (nuevoAsiento)
                    //{
                    modify = false;
                    volverAlEstadoDeIngreso();
                    if (nuevoAsiento)
                        btnCerrarAsiento.Enabled = true;

                    //}
                    refreshLvAsientos();
                    numImporte.Value = 0;
                    modified = true;
                }
            }
        }

        private void volverAlEstadoDeIngreso()
        {
            numImporte.Value = 0;
            btnIngresar.Text = "Ingresar";
            btnCancelar.Visible = false;
            modify = false;
            modificarAsientoAbierto = false;
            getMaxIdReg();
            idRegMod = 0;
            btnCerrarAsiento.Enabled = false;
            txtAsientoActual.Text = numeroActual.ToString();
        }

        private void eliminarRegistro()
        {
            ListViewItem lvitem = lvAsientos.SelectedItems[0];
            idRegMod = Convert.ToInt64(lvitem.SubItems[6].Text); //idreg.
            for (int i = 0; i < currentAsiento.Registros.Count; i++)
            {
                if (currentAsiento.Registros[i].idRegistro == idRegMod)
                {
                    currentAsiento.Registros.RemoveAt(i);
                    break;
                }
            }
        }

        private void cerrarAsiento()
        {
            btnCerrarAsiento.Enabled = false;
            cbFecha.Enabled = true;
            nuevoAsiento = true;
            numeroActual = maxNumber() + 1;
            txtAsientoActual.Text = numeroActual.ToString();
            rbDebe.Checked = true;
            rbHaber.Checked = false;

            if (!modified)
                EDM.EmpresaArchivo.WriteAsiento(currentEmpresa.FullPath, currentAsiento, true);
            else
                EDM.EmpresaArchivo.WriteAsientos(currentEmpresa.FullPath, Asientos, currentEmpresa);

            currentAsiento.isClosed = true;
            modified = false;
        }

        private bool isClosingAvailable()
        { 
            //Verifica si se puede cerrar, si hay diferencia.
            double deb = 0;
            double hab = 0;
            foreach (EDM.Entity.Registro reg in currentAsiento.Registros)
            {
                if (reg.valueType == EDM.Entity.ValueType.Debe)
                    deb += reg.Valor;
                else
                    hab += reg.Valor;
            }
            double resultado = deb - hab;
            lbDiferencia.Text = resultado.ToString("N2");

            if (resultado != 0)
            {
                lbDiferencia.ForeColor = Color.Red;
                btnCerrarAsiento.Enabled = false;
                return false;
            }
            else
            {
                lbDiferencia.ForeColor = Color.Black;
                btnCerrarAsiento.Enabled = true;
                return true;
            }
        }

        /// <summary>
        /// Obtiene el maximo numero de Asiento hasta el momento.
        /// </summary>
        /// <returns>integer</returns>
        private int maxNumber()
        {
            int max = 0;
            for (int i = 0; i < Asientos.Count; i++)
            {
                max = Asientos[i].Numero > max ? Asientos[i].Numero : max;
            }
            return max;
        }

        /// <summary>
        /// Guarda en variable maxIdReg el maximo id almacenado en Registros.
        /// </summary>
        private void getMaxIdReg()
        {
            long max = 0;
            foreach (EDM.Entity.Asiento oAs in Asientos)
            {
                for (int i = 0; i < oAs.Registros.Count; i++)
                {
                    max = oAs.Registros[i].idRegistro > max ? oAs.Registros[i].idRegistro : max;
                }
            }
            maxIdReg = max;
        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            this.Print();
        }

        private void toolStripVistaP_Click(object sender, EventArgs e)
        {
            this.VistaPrevia();
        }

        private void toolStripBtnAsientosPredetermindados_Click(object sender, EventArgs e)
        {
            if (!nuevoAsiento)
            {
                MessageBox.Show("Atención: Ingreso de Asiento Predeterminado no disponible. Aun no se ha cerrado el Asiento actual", "Asiento Predeterminado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FormAsientosPredeterminados formPredeterminados = new FormAsientosPredeterminados(this.cbFecha, edm, numeroActual);
            formPredeterminados.ShowDialog();

            if (formPredeterminados.registrosPredeterminado.Count > 0)
            {
                int num = Convert.ToInt32(formPredeterminados.registrosPredeterminado[0].SubItems[0].Text);
                DateTime date = Convert.ToDateTime(formPredeterminados.registrosPredeterminado[0].SubItems[1].Text);
                currentAsiento = new EDM.Entity.Asiento(num,
                    new List<EDM.Entity.Registro>(),
                    date);

                Asientos.Add(currentAsiento);

                //0-Nro. 1-Fecha 2-Cod 3-Description 4-Debe 5-Haber 6-idReg
                EDM.Entity.ValueType type= EDM.Entity.ValueType.Debe;
                double valor = 0;
                foreach (ListViewItem lvi in formPredeterminados.registrosPredeterminado)
                {
                    if (lvi.SubItems[4].Text != "" && Convert.ToDouble(lvi.SubItems[4].Text) != 0)
                    {
                        type = EDM.Entity.ValueType.Debe;
                        valor = Convert.ToDouble(lvi.SubItems[4].Text);
                    }
                    if(lvi.SubItems[5].Text != "" && Convert.ToDouble(lvi.SubItems[5].Text)!=0)
                    {
                        type = EDM.Entity.ValueType.Haber;
                        valor = Convert.ToDouble(lvi.SubItems[5].Text);
                    }

                    ingresarRegistro(Convert.ToInt32(lvi.SubItems[2].Text), lvi.SubItems[3].Text, type, valor);
                }

                refreshLvAsientos();
                numeroActual++;
                txtAsientoActual.Text = numeroActual.ToString();
                EDM.EmpresaArchivo.WriteAsientos(currentEmpresa.FullPath, Asientos, currentEmpresa);
            }
        }

        private void asientoDeCierreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentAsiento != null && currentAsiento.isClosed == false)
            {
                MessageBox.Show("Es necesario cerrar el asiento actual antes de Insertar los Asientos de Cierre.",
                    "Asientos de Cierre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<EDM.Entity.Registro> patrimonioReg = new List<EDM.Entity.Registro>();
            foreach (EDM.Entity.Asiento asiento in asientos)
            {
                foreach (EDM.Entity.Registro reg in asiento.Registros)
                {
                    //var currentReg = patrimonioReg

                    //1000
                    if ( reg.Codigo>999 && reg.Codigo<2000)
                    {

                    }
                    //2000
                    if (reg.Codigo > 1999 && reg.Codigo < 3000)
                    {

                    }
                    //3000
                    if (reg.Codigo > 2999 && reg.Codigo < 4000)
                    {

                    }
                    //4000
                    if (reg.Codigo > 3999 && reg.Codigo < 5000)
                    {

                    }

                    var objSaldo = (from c in saldos
                                    where c.Id == reg.Codigo
                                    select c).First();
                    if (reg.valueType == EDM.Entity.ValueType.Debe)
                        objSaldo.AcumD = reg.Valor;
                    else
                        objSaldo.AcumH = reg.Valor;
                }

            }

                    

        }

    }
}
