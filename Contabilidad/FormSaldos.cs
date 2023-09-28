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
    public partial class FormSaldos : Contabilidad.BasicForm
    {
        string title;
        List<EDM.Entity.Asiento> asientos_;
        public FormSaldos()
        {
            InitializeComponent();
        }

        public FormSaldos(int id, string description, tableType type)
            : base(id, description, type)
        {
            InitializeComponent();
        }

        public void LoadDataInfo(EDM.Empresa empresa, List<EDM.Entity.Asiento> asientos)
        {
            asientos_ = asientos;
            lbEmpresa.Text = empresa.Name;
            lbFechas.Text = empresa.FechaInicio.ToShortDateString() +
                " a " + empresa.FechaFinal.ToShortDateString();

            title = empresa.Name + "\r\n(" + lbFechas.Text + ")";

            prepareDtGrd(asientos);
        }

        private void prepareDtGrd(List<EDM.Entity.Asiento> asientos)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            dtgrdSaldos.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "Id";
            idColumn.HeaderText = "ID";
            idColumn.SortMode = DataGridViewColumnSortMode.Automatic;

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Descripcion";

            //DataGridViewTextBoxColumn detailsColumn = new DataGridViewTextBoxColumn();
            //detailsColumn.DataPropertyName = "Details";
            //detailsColumn.HeaderText = "Detalle";

            DataGridViewTextBoxColumn acumDColumn = new DataGridViewTextBoxColumn();
            acumDColumn.DataPropertyName = "AcumD";
            acumDColumn.HeaderText = "Acumulacion Debitos";
            acumDColumn.DefaultCellStyle.Format = "N2";

            DataGridViewTextBoxColumn acumHColumn = new DataGridViewTextBoxColumn();
            acumHColumn.DataPropertyName = "AcumH";
            acumHColumn.HeaderText = "Acumulacion Creditos";
            acumHColumn.DefaultCellStyle.Format = "N2";

            DataGridViewTextBoxColumn saldoDColumn = new DataGridViewTextBoxColumn();
            saldoDColumn.DataPropertyName = "SaldoD";
            saldoDColumn.HeaderText = "Saldo Deudor";
            saldoDColumn.DefaultCellStyle.Format = "N2";

            DataGridViewTextBoxColumn saldoHColumn = new DataGridViewTextBoxColumn();
            saldoHColumn.DataPropertyName = "SaldoH";
            saldoHColumn.HeaderText = "Saldo Acreedor";
            saldoHColumn.DefaultCellStyle.Format = "N2";
            

            dtgrdSaldos.Columns.Add(idColumn);          //0
            dtgrdSaldos.Columns.Add(descriptionColumn); //1
            //dtgrdSaldos.Columns.Add(detailsColumn);     //2
            dtgrdSaldos.Columns.Add(acumDColumn);       //2
            dtgrdSaldos.Columns.Add(acumHColumn);       //3
            dtgrdSaldos.Columns.Add(saldoDColumn);      //4
            dtgrdSaldos.Columns.Add(saldoHColumn);      //5

            BindingList<EDM.Entity.Saldo> saldos = new BindingList<EDM.Entity.Saldo>();

            List<int> aux = new List<int>();
            foreach (EDM.Entity.Asiento asiento in asientos)
            {
                foreach (EDM.Entity.Registro reg in asiento.Registros)
                {
                    if (!aux.Contains(reg.Codigo))
                    {
                        saldos.Add(new EDM.Entity.Saldo(reg.Codigo, reg.Description, reg.Details));
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

            var saldosOrdenados = from c in saldos
                                  orderby c.Id
                                  select c;

            dtgrdSaldos.DataSource = saldosOrdenados.ToList();

            //resizeColumns();
            adjustColumns(true);
            colorea();

            agregarTotales();
        }

        [Obsolete]
        private void resizeColumns()
        {
            dtgrdSaldos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgrdSaldos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgrdSaldos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgrdSaldos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgrdSaldos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgrdSaldos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            for (int i = 0; i < dtgrdSaldos.Columns.Count; i++)
            {
                int colw = dtgrdSaldos.Columns[i].Width;
                dtgrdSaldos.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dtgrdSaldos.Columns[i].Width = colw;
            }
        }
        private void adjustColumns(bool autoAdjust)
        {
            int lastVisibleColumnIndex = 0;
            foreach (DataGridViewColumn col in this.dtgrdSaldos.Columns)
            {
                if (col.Visible == true)
                    lastVisibleColumnIndex = col.Index;
            }
            foreach (DataGridViewColumn col in this.dtgrdSaldos.Columns)
            {
                if (col.Index == lastVisibleColumnIndex)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                else
                    if (autoAdjust)
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    else
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            foreach (DataGridViewColumn col in this.dtgrdSaldos.Columns)
            {
                int size = col.Width;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Width = size;
            }
        }

        private void colorea()
        {
            dtgrdSaldos.EnableHeadersVisualStyles = false;
            dtgrdSaldos.ColumnHeadersDefaultCellStyle.BackColor = Color.SandyBrown;

            bool colorea = false;
            for (int i = 0; i < dtgrdSaldos.Rows.Count; i++)
            {
                if (colorea)
                {
                    dtgrdSaldos.Rows[i].DefaultCellStyle.BackColor = Color.Lavender;
                    colorea = false;
                }
                else
                {
                    dtgrdSaldos.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    colorea = true;
                }
                for(int p =2; p<6;p++)
                if (Convert.ToInt64(dtgrdSaldos.Rows[i].Cells[p].Value) == 0)
                    dtgrdSaldos.Rows[i].Cells[p].Style.ForeColor = dtgrdSaldos.Rows[i].DefaultCellStyle.BackColor;
                else
                    dtgrdSaldos.Rows[i].Cells[p].Style.ForeColor = Color.Black;
            }
        }

        private void agregarTotales()
        {
            dtgrdSaldos.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Width = dtgrdSaldos.Columns[0].Width;
            idColumn.HeaderText = "-";

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = dtgrdSaldos.Columns[1].Width;
            descriptionColumn.HeaderText = "-";

            //DataGridViewTextBoxColumn detailsColumn = new DataGridViewTextBoxColumn();    
            //detailsColumn.Width = dtgrdSaldos.Columns[2].Width;
            //detailsColumn.HeaderText = "-";

            DataGridViewTextBoxColumn acumDColumn = new DataGridViewTextBoxColumn();
            acumDColumn.Width = dtgrdSaldos.Columns[2].Width;
            acumDColumn.HeaderText = "Total Debitos";

            DataGridViewTextBoxColumn acumHColumn = new DataGridViewTextBoxColumn();
            acumHColumn.Width = dtgrdSaldos.Columns[3].Width;
            acumHColumn.HeaderText = "Total Creditos";

            DataGridViewTextBoxColumn saldoDColumn = new DataGridViewTextBoxColumn();
            saldoDColumn.Width = dtgrdSaldos.Columns[4].Width;
            saldoDColumn.HeaderText = "Total Saldo Deudor";

            DataGridViewTextBoxColumn saldoHColumn = new DataGridViewTextBoxColumn();
            saldoHColumn.Width = dtgrdSaldos.Columns[5].Width;
            saldoHColumn.HeaderText = "Total Saldo Acreedor";

            dtGrdTotales.Columns.Add(idColumn);
            dtGrdTotales.Columns.Add(descriptionColumn);
            //dtGrdTotales.Columns.Add(detailsColumn);
            dtGrdTotales.Columns.Add(acumDColumn);
            dtGrdTotales.Columns.Add(acumHColumn);
            dtGrdTotales.Columns.Add(saldoDColumn);
            dtGrdTotales.Columns.Add(saldoHColumn);

            dtGrdTotales.EnableHeadersVisualStyles = false;
            dtGrdTotales.ColumnHeadersDefaultCellStyle.BackColor = Color.SandyBrown;

            double tDebe=0;
            double tHaber=0;
            double tSDeu=0;
            double tSAcr=0;
            for (int i = 0; i < dtgrdSaldos.Rows.Count; i++)
            {
                tDebe += Convert.ToDouble(dtgrdSaldos.Rows[i].Cells[2].Value);
                tHaber += Convert.ToDouble(dtgrdSaldos.Rows[i].Cells[3].Value);
                tSDeu += Convert.ToDouble(dtgrdSaldos.Rows[i].Cells[4].Value);
                tSAcr += Convert.ToDouble(dtgrdSaldos.Rows[i].Cells[5].Value);
            }

            int n = dtGrdTotales.Rows.Add();
            dtGrdTotales.Rows[n].Cells[2].Value = "$" + tDebe.ToString("N2");
            dtGrdTotales.Rows[n].Cells[3].Value = "$" + tHaber.ToString("N2");
            dtGrdTotales.Rows[n].Cells[4].Value = "$" + tSDeu.ToString("N2");
            dtGrdTotales.Rows[n].Cells[5].Value = "$" + tSAcr.ToString("N2");
            dtGrdTotales.Rows[n].DefaultCellStyle.ForeColor = Color.DarkBlue;
            
        }

        private void dtgrdSaldos_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                for(int i=0; i<dtgrdSaldos.Columns.Count;i++)
                    dtGrdTotales.Columns[i].Width = dtgrdSaldos.Columns[i].Width;
            }
            catch
            {
            }
        }

        private void dtgrdSaldos_SizeChanged(object sender, EventArgs e)
        {
            adjustColumns(true);
        }

        private void FormSaldos_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtgrdSaldos.Columns.Clear();
            dtgrdSaldos.DataSource = null;
            dtGrdTotales.Columns.Clear();

            prepareDtGrd(asientos_);

            dtgrdSaldos.Refresh();
            dtGrdTotales.Refresh();
        }

        

    }
}
