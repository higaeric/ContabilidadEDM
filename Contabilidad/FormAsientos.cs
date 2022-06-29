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
        bool existeAsientoDeCierre = false;

        OrdenColumna OrdenActual = OrdenColumna.Fecha_Asc;

        #region "Eventos"
        private void FormAsientos_Load(object sender, EventArgs e)
        {
        }

        private void FormAsientos_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.checkBoxFechaMensual = checkBoxFechaMensual.Checked;
            Properties.Settings.Default.AsientosSpliterDistance1 = this.splitContainer1.SplitterDistance;
            Properties.Settings.Default.Save();
        }

        private void FormAsientos_Shown(object sender, EventArgs e)
        {
            //splitContainer1.SplitterDistance = 175;
            this.checkBoxFechaMensual.Checked = Properties.Settings.Default.checkBoxFechaMensual;
            this.splitContainer1.SplitterDistance = Properties.Settings.Default.AsientosSpliterDistance1;

            this.Refresh();
        }

        private void cbCuenta_Click(object sender, EventArgs e)
        {
            if (MainForm.pdcChanged)
            {
                //Actualizo el combo de plan de cuentas.
                loadPDC(edm);
                MainForm.pdcChanged = false;
            }
        }

        private void FormAsientos_Enter(object sender, EventArgs e)
        {
            if (MainForm.pdcChanged && edm !=null)
            {
                //Actualizo el combo de plan de cuentas.
                loadPDC(edm);
                MainForm.pdcChanged = false;
            }
        }
        #endregion

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
            
            foreach (KeyValuePair<int, EDM.Entity.Cuenta> kvp in edm.PDC.planDeCuentas)
                stringCol.Add(kvp.Value.Nombre); //cbCuenta.Items.Add(kvp.Value);

            //cargo datos del combobox
            BindingSource bs = new BindingSource();
            List<EDM.Entity.Cuenta> ls = edm.PDC.planDeCuentas.Values.ToList(); //.ToList<string>();

            Comparison<EDM.Entity.Cuenta> compnamedel = new Comparison<EDM.Entity.Cuenta>(EDM.Entity.Cuenta.CompareName);
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
            lvAsientos.Columns.Add("NRO","Nro.",40);        //0
            lvAsientos.Columns.Add("FECHA", "Fecha", 100);  //1
            lvAsientos.Columns.Add("COD","Cod.", 60);       //2
            lvAsientos.Columns.Add("DESCRIPTION", "Description", 200);  //3
            lvAsientos.Columns.Add("DETALLE", "Detalle", 150);          //4

            lvAsientos.Columns.Add("DEBE", "Debe", 100);    //5
            lvAsientos.Columns.Add("HABER", "Haber", 100);  //6
            lvAsientos.Columns.Add("IDREG", "idReg", 0);    //7

            lvAsientos.Columns.Add("HIDDEN","Hidden", 0);

            
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
            numeroActual = nextNumber();
            txtAsientoActual.Text = numeroActual.ToString();
            lbDiferencia.Text = "0";
            loadComboFechas(empresa.FechaInicio, empresa.FechaFinal);
            nuevoAsiento = true;
            getMaxIdReg();
        }

        /// <summary>
        /// Limpia el ListView de Asientos.
        /// Colorea por asiento.
        /// Recorre los asientos y los escribe en forma ordenada: primero los Debe y luego los Haberes.
        /// En el caso del Asiento de cierre se escribe sin el ordenamiento de Debe/Haber.
        /// </summary>
        public void refreshLvAsientos(long idRegistro = 0)
        {
            lvAsientos.Items.Clear();
            Color color = Color.LightYellow;

            Asientos.Sort(delegate(EDM.Entity.Asiento c1, EDM.Entity.Asiento c2)
            {
                //Antes ordenaba primero por Fecha y luego Numero
                //int result = c1.Fecha.CompareTo(c2.Fecha);
                //return result != 0 ? result : c1.Numero.CompareTo(c2.Numero);

                int result = 0;
                if(OrdenActual== OrdenColumna.Fecha_Asc)
                {
                    result = c1.Fecha.CompareTo(c2.Fecha);
                }
                else if (OrdenActual == OrdenColumna.Numero_Asc)
                { 
                    
                }
                    
                return result!=0? result: c1.Numero.CompareTo(c2.Numero);
            });

            foreach (EDM.Entity.Asiento aItem in Asientos)
            {
                //---- Actualizo la descripcion. -----
                foreach (EDM.Entity.Registro reg in aItem.Registros)
                {
                    if(edm.PDC.planDeCuentas.ContainsKey(reg.Codigo))
                    {
                        reg.Description = edm.PDC.planDeCuentas[reg.Codigo].Nombre;
                    }
                }
                //------------------------------------

                if (color.Name == Color.LightYellow.Name)
                    color = Color.Lavender;
                else
                    color = Color.LightYellow;

                if (aItem.Numero != 0)
                {
                    //Ordenamiento y coloreo de Asientos Std.
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

            var AsientoCierre = Asientos.FirstOrDefault(c => c.Numero == 0);
            if (AsientoCierre != null)
            {
                //El Asiento de Cierre:
                existeAsientoDeCierre = true;

                color = Color.PaleGreen;
                var resultEjercDH = AsientoCierre.Registros
                    .Where(c => c.valueType == EDM.Entity.ValueType.Debe && c.Codigo > 2999).Union(
                    AsientoCierre.Registros.Where(c => c.valueType == EDM.Entity.ValueType.Haber && c.Codigo > 2999));

                for (int i = 0; i < resultEjercDH.Count(); i++)
                {
                    ListViewItem lvItem = lvitemToAdd(ref color, AsientoCierre, resultEjercDH.ElementAt(i));
                    lvAsientos.Items.Add(lvItem);
                }

                color = Color.Gold;
                var resultNoAsigcDH = AsientoCierre.Registros
                    .Where(c => c.valueType == EDM.Entity.ValueType.Debe && c.Codigo < 3000).Union(
                    AsientoCierre.Registros.Where(c => c.valueType == EDM.Entity.ValueType.Haber && c.Codigo < 3000));

                for (int i = 0; i < resultNoAsigcDH.Count(); i++)
                {
                    ListViewItem lvItem = lvitemToAdd(ref color, AsientoCierre, resultNoAsigcDH.ElementAt(i));
                    lvAsientos.Items.Add(lvItem);
                }
            }

            

            //Focus segun el idRegistro. Si es cero no lo hace
            if (idRegistro != 0)
            {
                for (int i = 0; i < lvAsientos.Items.Count; i++)
                {
                    if (lvAsientos.Items[i].SubItems[7].Text == idRegistro.ToString())//idregistro
                    {
                        
                        lvAsientos.Items[i].Selected = true;
                        //lvAsientos.Items[i].Focused = true;
                        lvAsientos.Items[i].EnsureVisible();
                        lvAsientos.Select();
                        return;
                    }
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
            lvItem.SubItems.Add(reg.Details);

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

            lvItem.SubItems.Add(reg.isHidden.ToString());
            
            return lvItem;
        }

        

        #region "Botones y Controles"
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (!ContinuarSiAsientoDeCierreExiste())
                return;

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

        private void toolStripBtnEliminarRow_Click(object sender, EventArgs e)
        {
            if (lvAsientos.SelectedItems == null || lvAsientos.SelectedItems.Count == 0) return;
            if (lvAsientos.SelectedItems[0].SubItems[0].Text == "0")
            {
                MessageBox.Show("No esta permitido borrar registros del Asiento de Cierre", "Eliminar registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ContinuarSiAsientoDeCierreExiste())
                return;

            if (MessageBox.Show("Esta seguro que desea eliminar el Registro seleccionado?", "Elimnar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                != DialogResult.Yes)
            {
                return;
            }

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
                dateTimePicker1.Text = currentAsiento.Fecha.ToShortDateString(); //fecha
                dateTimePicker1.Enabled = false;
                checkBoxFechaMensual.Enabled = false;

                txtAsientoActual.Text = currentAsiento.Numero.ToString();
            }
            isClosingAvailable();
            refreshLvAsientos();
        }

        private void toolStripBtnEliminar_Click(object sender, EventArgs e)
        {
            if (lvAsientos.SelectedItems == null || lvAsientos.SelectedItems.Count == 0) return;

            string asientoNumeroStr = "Nro. " + lvAsientos.SelectedItems[0].SubItems[0].Text;
            if(asientoNumeroStr == "0")
                asientoNumeroStr = "de Cierre";

            if (currentAsiento != null && currentAsiento.isClosed == false)
            {
                MessageBox.Show("No se puede eliminar un asiento entero si aun no se ha cerrado el asiento actual.\r\nPrimero debe cerrar Asiento.", "Eliminar Asiento.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Esta seguro que desea eliminar el Asiento " + asientoNumeroStr + " completo?", "Eliminar el asiento completo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes)
            {
                return;
            }

            eliminarAsiento();
            refreshLvAsientos();
        }

        private void toolStripBtnModificar_Click(object sender, EventArgs e)
        {
            if (lvAsientos.SelectedItems == null || lvAsientos.SelectedItems.Count==0) return;
            if (lvAsientos.SelectedItems[0].SubItems[0].Text == "0")
            {
                MessageBox.Show("No esta permitido modificar registros del Asiento de Cierre", "Modificar registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ContinuarSiAsientoDeCierreExiste())
                return;

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
                numeroActual = nextNumber();

                cbFecha.Enabled = true;
                dateTimePicker1.Enabled = true;
                checkBoxFechaMensual.Enabled = true;
            }
            volverAlEstadoDeIngreso();
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
            if (!ContinuarSiAsientoDeCierreExiste())
                return;

            ingresarAsientosPredeterminados();
        }

        private void asientoDeCierreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ContinuarSiAsientoDeCierreExiste())
                return;
            ingresarAsientoDeCierre();
        }
        #endregion

        private void ingresarRegistro()
        {
            if (cbFecha.Text == "" && checkBoxFechaMensual.Checked) { cbFecha.Focus(); return; }
            if (dateTimePicker1.Text == "" && !checkBoxFechaMensual.Checked) { dateTimePicker1.Focus(); return; }
            if (cbCuenta.Text == "") { cbCuenta.Focus(); return; }
            if (string.IsNullOrEmpty(txtImporte.Text) || Convert.ToDouble(txtImporte.Text) == 0) { txtImporte.Focus(); return; }
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                
                int cod=0;
                for (int i = 0; i < edm.PDC.planDeCuentas.Count; i++)
                {
                    if (edm.PDC.planDeCuentas.ElementAt(i).Value.Nombre == cbCuenta.SelectedItem.ToString())
                        cod = edm.PDC.planDeCuentas.ElementAt(i).Key;
                }               
                
                if (nuevoAsiento)
                {
                    string currentFecha = "";

                    if(checkBoxFechaMensual.Checked )
                    {
                        currentFecha = cbFecha.Text;
                    }
                    else
                    {
                        try
                        {
                            currentFecha = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                            Convert.ToDateTime(currentFecha);   //Para verificar la fecha. si es erroneo va al catch
                        }
                        catch
                        {
                            DateTime dt;
                            DateTime.TryParse(dateTimePicker1.Value.ToShortDateString(),out dt);
                            string s = dt.ToString("dd/MM/yyyy");     
                        }              
                    }

                    currentAsiento = new EDM.Entity.Asiento(numeroActual,
                        new List<EDM.Entity.Registro>(),
                        Convert.ToDateTime(currentFecha)); //cbFecha.Text));
                    Asientos.Add(currentAsiento);
                    cbFecha.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    nuevoAsiento = false;
                    checkBoxFechaMensual.Enabled = false;
                }

                //Add registro
                EDM.Entity.ValueType type = rbDebe.Checked? EDM.Entity.ValueType.Debe: EDM.Entity.ValueType.Haber;
                maxIdReg++;

                currentAsiento.Registros.Add(new EDM.Entity.Registro(maxIdReg, cod, cbCuenta.Text, type, Convert.ToDouble(txtImporte.Text), 
                    txtDetalle.Text, checkBoxOculto.Checked)); //numImporte.Value)));

                isClosingAvailable();
                refreshLvAsientos(maxIdReg);
                txtImporte.Text = "0.00"; //numImporte.Value = 0;
                txtDetalle.Text = "";
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
            if (lvitem.SubItems[5].Text != "")    //debe, Antes era el 4
            {
                rbDebe.Checked = true;
                rbHaber.Checked = false;
                txtImporte.Text = lvitem.SubItems[5].Text.Remove(0, 1); //numImporte.Value = Convert.ToDecimal(lvitem.SubItems[4].Text.Remove(0,1));
            }
            else
            {
                rbDebe.Checked = false;
                rbHaber.Checked = true;
                txtImporte.Text = lvitem.SubItems[6].Text.Remove(0, 1); //numImporte.Value = Convert.ToDecimal(lvitem.SubItems[5].Text.Remove(0,1));
            }
            cbCuenta.SelectedItem = lvitem.SubItems[3].Text; //3 Description
            if (cbCuenta.SelectedItem.ToString() != lvitem.SubItems[3].Text)
            {
                //Si dejaron espacios en el plan de cuentas, puede no tomarlo.
                //Entonces busco x proximidad, eliminando los espacios.
                string aux = "";
                string itemBuscar = lvitem.SubItems[3].Text.Trim(); //3 Description
                for (int i = 0; i < cbCuenta.Items.Count; i++)
                {
                    aux = cbCuenta.Items[i].ToString().Trim();
                    if (aux == itemBuscar)
                    {
                        cbCuenta.SelectedIndex = i;
                        break;
                    }
                }
            }
            
            txtDetalle.Text = lvitem.SubItems[4].Text;
            

            cbFecha.SelectedItem = (Convert.ToDateTime(lvitem.SubItems[1].Text)).ToShortDateString(); //fecha
            dateTimePicker1.Text = (Convert.ToDateTime(lvitem.SubItems[1].Text)).ToShortDateString(); //fecha

            cbFecha.Enabled = false;
            dateTimePicker1.Enabled = false;
            checkBoxFechaMensual.Enabled = false;

            txtAsientoActual.Text = lvitem.SubItems[0].Text;

            idRegMod = Convert.ToInt64(lvitem.SubItems[7].Text); //idregistro
            btnIngresar.Text = "Actualizar";
            btnCancelar.Visible = true;
            btnCerrarAsiento.Enabled = false;
        }

        private void actualizaAsiento(bool actual)
        {
            int cod = 0;
            //for (int i = 0; i < edm.PDC.planDeCuentas.Count; i++)
            //{
                //if (edm.PDC.planDeCuentas.ElementAt(i).Value == cbCuenta.SelectedItem.ToString())
                    //cod = edm.PDC.planDeCuentas.ElementAt(i).Key;
            //}
            cod = (Int32)cbCuenta.SelectedValue;

            foreach (EDM.Entity.Registro reg in currentAsiento.Registros)
            {
                if (reg.idRegistro == idRegMod)
                {
                    EDM.Entity.ValueType type = rbDebe.Checked ? EDM.Entity.ValueType.Debe : EDM.Entity.ValueType.Haber;
                    reg.Codigo = cod;
                    reg.Description = cbCuenta.Text;
                    reg.valueType = type;
                    reg.Valor = Convert.ToDouble(txtImporte.Text);  //numImporte.Value);
                    reg.Details = txtDetalle.Text;
                    reg.isHidden = checkBoxOculto.Checked;

                    //no hace falta guardar en disco xq aun no esta cerrado
                    nuevoAsiento = isClosingAvailable();
                    //if (nuevoAsiento)
                    //{
                    modify = false;
                    volverAlEstadoDeIngreso();
                    if (nuevoAsiento)
                        btnCerrarAsiento.Enabled = true;

                    //}
                    refreshLvAsientos(reg.idRegistro);
                    txtImporte.Text = "0.00";   //numImporte.Value = 0;
                    modified = true;
                }
            }
        }

        private void volverAlEstadoDeIngreso()
        {
            txtImporte.Text = "0.00";   //numImporte.Value = 0;
            btnIngresar.Text = "Ingresar";
            btnCancelar.Visible = false;
            modify = false;
            modificarAsientoAbierto = false;
            getMaxIdReg();
            idRegMod = 0;
            btnCerrarAsiento.Enabled = false;
            txtAsientoActual.Text = numeroActual.ToString();
            txtDetalle.Text = "";
        }

        private void eliminarRegistro()
        {
            ListViewItem lvitem = lvAsientos.SelectedItems[0];
            idRegMod = Convert.ToInt64(lvitem.SubItems[7].Text); //idreg.
            for (int i = 0; i < currentAsiento.Registros.Count; i++)
            {
                if (currentAsiento.Registros[i].idRegistro == idRegMod)
                {
                    currentAsiento.Registros.RemoveAt(i);
                    break;
                }
            }
        }

        private void eliminarAsiento()
        {
            ListViewItem lvitem = lvAsientos.SelectedItems[0];
            int numAsiento = Convert.ToInt32(lvitem.SubItems[0].Text);

            var asientoEliminar = Asientos.FirstOrDefault(c => c.Numero == numAsiento);


            for (int i = asientoEliminar.Registros.Count; i > 0; i= i-1)
            {
                asientoEliminar.Registros.RemoveAt(i-1);
            }

            EDM.EmpresaArchivo.WriteAsientos(currentEmpresa.FullPath, Asientos, currentEmpresa);
            refreshLvAsientos();
        }

        private void cerrarAsiento()
        {
            btnCerrarAsiento.Enabled = false;
            cbFecha.Enabled = true;
            dateTimePicker1.Enabled = true;
            checkBoxFechaMensual.Enabled = true;
            nuevoAsiento = true;
            numeroActual = nextNumber();
            txtAsientoActual.Text = numeroActual.ToString();
            rbDebe.Checked = true;
            rbHaber.Checked = false;

            if (!modified)
                EDM.EmpresaArchivo.WriteAsiento(currentEmpresa.FullPath, currentAsiento, true);
            else
                EDM.EmpresaArchivo.WriteAsientos(currentEmpresa.FullPath, Asientos, currentEmpresa);

            currentAsiento.isClosed = true;
            modified = false;
            txtDetalle.Text = "";
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

            resultado = resultado < 0 ? -resultado : resultado;
            if (resultado == 0 || resultado< 0.01)
            {
                lbDiferencia.ForeColor = Color.Black;
                btnCerrarAsiento.Enabled = true;
                return true;
            }
            else
            {
                lbDiferencia.ForeColor = Color.Red;
                btnCerrarAsiento.Enabled = false;
                return false;
            }
        }

        /// <summary>
        /// Obtiene el proximo numero de Asiento hasta el momento.
        /// </summary>
        /// <returns>integer</returns>
        private int nextNumber()
        {
            //24-12-2014 ahora buscara el proximo numero disponible
            ////obtiene el maximo
            //int max = 0;
            //for (int i = 0; i < Asientos.Count; i++)
            //{
            //    max = Asientos[i].Numero > max ? Asientos[i].Numero : max;
            //}
            //return max;
            
            for (int i = 0; i < Asientos.Count; i++)
            {
                bool numIntemedio = Asientos.Exists(c => c.Numero == i + 1);
                if (!numIntemedio)
                    return i + 1;
            }
            return Asientos.Count +1;
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

        private void ingresarAsientosPredeterminados()
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

                //0-Nro. 1-Fecha 2-Cod 3-Description 4-Detalle 5-Debe 6-Haber 7-idReg
                EDM.Entity.ValueType type = EDM.Entity.ValueType.Debe;
                double valor = 0;
                foreach (ListViewItem lvi in formPredeterminados.registrosPredeterminado)
                {
                    if (lvi.SubItems[4].Text != "" && Convert.ToDouble(lvi.SubItems[4].Text) != 0)  //debe
                    {
                        type = EDM.Entity.ValueType.Debe;
                        valor = Convert.ToDouble(lvi.SubItems[4].Text);
                    }
                    if (lvi.SubItems[5].Text != "" && Convert.ToDouble(lvi.SubItems[5].Text) != 0)  //haber
                    {
                        type = EDM.Entity.ValueType.Haber;
                        valor = Convert.ToDouble(lvi.SubItems[5].Text);
                    }

                    ingresarRegistro(Convert.ToInt32(lvi.SubItems[2].Text), lvi.SubItems[3].Text, type, valor);
                }

                refreshLvAsientos(maxIdReg);
                numeroActual++;
                txtAsientoActual.Text = numeroActual.ToString();
                EDM.EmpresaArchivo.WriteAsientos(currentEmpresa.FullPath, Asientos, currentEmpresa);
                currentAsiento.isClosed = true;
            }
        }

        private void ingresarAsientoDeCierre()
        {
            if (currentAsiento != null && currentAsiento.isClosed == false)
            {
                MessageBox.Show("Es necesario cerrar el asiento actual antes de Insertar los Asientos de Cierre.",
                    "Asientos de Cierre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Esta seguro de Generar el Asiento de Cierre?", "Asiento de Cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=
                System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            List<EDM.Entity.Saldo> saldos = new List<EDM.Entity.Saldo>();
            List<int> aux = new List<int>();
            foreach (EDM.Entity.Asiento asiento in Asientos)
            {
                foreach (EDM.Entity.Registro reg in asiento.Registros)
                {
                    if (!aux.Contains(reg.Codigo))
                    {
                        saldos.Add(new EDM.Entity.Saldo(reg.Codigo, reg.Description));
                        aux.Add(reg.Codigo);
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

            //----- Calculo del "Resultado del Ejercicio" ---------
            var saldosResultEjerc = (from c in saldos
                                     where c.Id > 2999
                                     orderby c.Id
                                     select c).ToList();

            double valorResultadoEjercicio = 0;
            for (int i = 0; i < saldosResultEjerc.Count; i++)
            {
                valorResultadoEjercicio += saldosResultEjerc[i].SaldoH - saldosResultEjerc[i].SaldoD;
            }
            EDM.Entity.Saldo sResultEjerc = new EDM.Entity.Saldo(9999, "Resultado del Ejercicio");
            if (valorResultadoEjercicio < 0)
                sResultEjerc.AcumD = valorResultadoEjercicio;
            else
                sResultEjerc.AcumH = -valorResultadoEjercicio;
            saldos.Add(sResultEjerc);
            saldosResultEjerc.Add(sResultEjerc);
            //------------------------------------------------------

            //----------- Calculo de Resultados no asignados -------
            //Primero lo busco por el ID y nombre conocido
            var regResultNoAsign = saldos.FirstOrDefault(c => c.Id==2006 && c.Description=="Resultados No Asignados");
            if(regResultNoAsign==null)  //En caso de que lo hayan modificado lo busco x nombre
                regResultNoAsign = saldos.FirstOrDefault(c => EDM.EDM.ComputeLevenshteinDistance(c.Description.ToLower(), "resultados no asignados") < 2);

            if (regResultNoAsign != null)
            {
                //lo modifico
                if (valorResultadoEjercicio < 0)
                    regResultNoAsign.AcumD = -valorResultadoEjercicio;
                else
                    regResultNoAsign.AcumH = valorResultadoEjercicio;
            }
            else
            {
                //lo agrego
                //busco el  plan de cuentas
                var pdcResultNoAsign = edm.PDC.planDeCuentas.FirstOrDefault(c => (c.Key == 2006 && c.Value.Nombre == "Resultados No Asignados")
                    ||EDM.EDM.ComputeLevenshteinDistance(c.Value.Nombre.ToLower(), "resultados no asignados") < 2);

                EDM.Entity.Saldo ss = new EDM.Entity.Saldo(pdcResultNoAsign.Key, pdcResultNoAsign.Value.Nombre);
                if (valorResultadoEjercicio < 0)
                    ss.AcumD = -valorResultadoEjercicio;
                else
                    ss.AcumH = valorResultadoEjercicio;
                saldos.Add(ss);
            }
            //------------------------------------------------------

            //------ Armado del asiento -------
            List<EDM.Entity.Registro> patrimonioReg = new List<EDM.Entity.Registro>();
            
            for (int i = 0; i < saldos.Count; i++)
            {
                int cod = saldos[i].Id;
                string descrip = saldos[i].Description;
                EDM.Entity.ValueType type;
                double valor = 0;
                if (saldos[i].SaldoD > 0)
                {
                    type = EDM.Entity.ValueType.Haber;
                    valor = saldos[i].SaldoD;
                }
                else
                {
                    type = EDM.Entity.ValueType.Debe;
                    valor = saldos[i].SaldoH;
                }

                maxIdReg++;
                patrimonioReg.Add(new EDM.Entity.Registro(maxIdReg, cod, descrip, type, valor));
            }

            //El asiento Nro 0 es el Asiento de Cierre
            Asientos.Add(new EDM.Entity.Asiento(0,patrimonioReg,DateTime.Today));
            EDM.EmpresaArchivo.WriteAsientos(currentEmpresa.FullPath, Asientos, currentEmpresa);
            refreshLvAsientos(maxIdReg);
        }

        /// <summary>
        /// Verifica si ya existe el asiento de cierre
        /// </summary>
        /// <returns></returns>
        private bool ContinuarSiAsientoDeCierreExiste()
        {
            if (existeAsientoDeCierre)
            {
                if (MessageBox.Show("Ya existe el Asiento de Cierre.\r\n"+
                    "\r\nPara realizar cambios en la tabla de Asientos, se eliminará dicho Asiento de Cierre existente. Luego tendrá que volver a generarlo.\r\n\r\n"+
                    " ¿Desea Continuar?", "Asiento de Cierre Existente", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) !=
                    System.Windows.Forms.DialogResult.Yes)
                {
                    return false;
                }
                //elimina el asiento de cierre existente
                int indexToRemove = Asientos.FindIndex(c => c.Numero == 0);
                Asientos.RemoveAt(indexToRemove);
                existeAsientoDeCierre = false;
                EDM.EmpresaArchivo.WriteAsientos(currentEmpresa.FullPath, Asientos, currentEmpresa);
            }
            return true;
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }


            bool IsDec = false;
            //int nroDec = 0;
            txtImporte.Text = String.Format("{0:0,0.00}", txtImporte.Text);

            for (int i = 0; i < txtImporte.Text.Length; i++)
            {
                if (txtImporte.Text[i] == '.')
                    IsDec = true;

                //if (IsDec && nroDec++ >= 2)
                //{
                //    e.Handled = true;
                //    return;
                //}
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtImporte_TextChanged(object sender, EventArgs e)
        {
            int nroDec = 0;
            int cantChar = 0;
            bool IsDec = false;

            if (txtImporte.Text.StartsWith("."))
                txtImporte.Text = 0 + txtImporte.Text;

            for (int i = 0; i < txtImporte.Text.Length; i++)
            {
                if (txtImporte.Text[i] == '.')
                    IsDec = true;

                if (IsDec)
                {
                    nroDec++;
                }

                if (IsDec && cantChar==0)
                    cantChar = i;
            }
            if (nroDec > 2)
                txtImporte.Text = txtImporte.Text.Substring(0, cantChar + 3);
        }

        private void checkBoxFechaMensual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFechaMensual.Checked)
            {
                cbFecha.Visible = true;
                dateTimePicker1.Visible = false;
            }
            else
            {
                cbFecha.Visible = false;
                dateTimePicker1.Visible = true;
            }
        }

        private void lvAsientos_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                OrdenActual = OrdenColumna.Numero_Asc;
                lvAsientos.SetSortIcon(0, SortOrder.Ascending);
            }
            else if (e.Column == 1)
            {
                OrdenActual = OrdenColumna.Fecha_Asc;
                lvAsientos.SetSortIcon(1, SortOrder.Ascending);
            }
            refreshLvAsientos();
        }







    }

    public enum OrdenColumna
    { 
        Numero_Asc = 1,
        Numero_Des = 2,
        Fecha_Asc = 3,
        Fecha_Des = 4
    }
}
