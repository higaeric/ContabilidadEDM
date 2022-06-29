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
    public partial class FormMayor : Contabilidad.BasicForm
    {
        string description;

        public FormMayor(int id, string description, tableType type)
            : base(id, description, type)
        {
            InitializeComponent();
        }

        public void LoadDataInfo(List<EDM.Entity.Registro> registros, string pdcName)
        {
            description = pdcName;
            labelDescription.Text = pdcName;
            prepareGrid(registros);
            adjustColumns(true);
        }

        private void prepareGrid(List<EDM.Entity.Registro> registros)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            dtgrdMayor.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn nombreCuentaColumn = new DataGridViewTextBoxColumn();
            nombreCuentaColumn.DataPropertyName = "Nombre";
            nombreCuentaColumn.HeaderText = "Nombre";

            DataGridViewTextBoxColumn detailColumn = new DataGridViewTextBoxColumn();
            detailColumn.DataPropertyName = "Details";
            detailColumn.HeaderText = "Detalles";
            
            DataGridViewTextBoxColumn acumDColumn = new DataGridViewTextBoxColumn();
            acumDColumn.DataPropertyName = "Debe";
            acumDColumn.HeaderText = "Debitos";
            acumDColumn.DefaultCellStyle.Format = "N2";

            DataGridViewTextBoxColumn acumHColumn = new DataGridViewTextBoxColumn();
            acumHColumn.DataPropertyName = "Haber";
            acumHColumn.HeaderText = "Creditos";
            acumHColumn.DefaultCellStyle.Format = "N2";

            dtgrdMayor.Columns.Add(nombreCuentaColumn);
            dtgrdMayor.Columns.Add(detailColumn);
            dtgrdMayor.Columns.Add(acumDColumn);
            dtgrdMayor.Columns.Add(acumHColumn);

            BindingList<EDM.Entity.Mayor> mayorizacion = new BindingList<EDM.Entity.Mayor>();

            foreach (EDM.Entity.Registro reg in registros)
            { 
                if(reg.valueType== EDM.Entity.ValueType.Debe)
                    mayorizacion.Add(new EDM.Entity.Mayor(reg.Codigo, reg.Description, reg.Description,
                        reg.Valor,0, reg.Details));
                else
                    mayorizacion.Add(new EDM.Entity.Mayor(reg.Codigo, reg.Description, reg.Description,
                        0, reg.Valor, reg.Details));
            }

            dtgrdMayor.DataSource = mayorizacion;

            adjustColumns(true);
            agregarTotales();
        }

        private void adjustColumns(bool autoAdjust)
        {
            int partes = this.dtgrdMayor.Width / 4 - 15;
            foreach (DataGridViewColumn col in this.dtgrdMayor.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Width = partes;
            }
        }

        private void dtgrdMayor_SizeChanged(object sender, EventArgs e)
        {
            adjustColumns(true);
        }

        private void agregarTotales()
        {
            dtgrdMayor.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn nombreCuentaColumn = new DataGridViewTextBoxColumn();
            nombreCuentaColumn.Width = dtgrdMayor.Columns[0].Width;
            nombreCuentaColumn.HeaderText = "-";

            DataGridViewTextBoxColumn detailsColumn = new DataGridViewTextBoxColumn();
            detailsColumn.Width = dtgrdMayor.Columns[1].Width;
            detailsColumn.HeaderText = "-";

            DataGridViewTextBoxColumn acumDColumn = new DataGridViewTextBoxColumn();
            acumDColumn.Width = dtgrdMayor.Columns[2].Width;
            acumDColumn.HeaderText = "Total Debitos";

            DataGridViewTextBoxColumn acumHColumn = new DataGridViewTextBoxColumn();
            acumHColumn.Width = dtgrdMayor.Columns[3].Width;
            acumHColumn.HeaderText = "Total Creditos";

            dtGrdTotales.Columns.Add(nombreCuentaColumn);
            dtGrdTotales.Columns.Add(detailsColumn);
            dtGrdTotales.Columns.Add(acumDColumn);
            dtGrdTotales.Columns.Add(acumHColumn);


            dtGrdTotales.EnableHeadersVisualStyles = false;
            dtGrdTotales.ColumnHeadersDefaultCellStyle.BackColor = Color.SandyBrown;

            double tDebe = 0;
            double tHaber = 0;

            for (int i = 0; i < dtgrdMayor.Rows.Count; i++)
            {
                tDebe += Convert.ToDouble(dtgrdMayor.Rows[i].Cells[2].Value);
                tHaber += Convert.ToDouble(dtgrdMayor.Rows[i].Cells[3].Value);
            }

            int n = dtGrdTotales.Rows.Add();
            dtGrdTotales.Rows[n].Cells[2].Value = "$" + tDebe.ToString("N2");
            dtGrdTotales.Rows[n].Cells[3].Value = "$" + tHaber.ToString("N2");
            dtGrdTotales.Rows[n].DefaultCellStyle.ForeColor = Color.DarkBlue;

        }

        private void dtgrdMayor_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                for (int i = 0; i < dtgrdMayor.Columns.Count; i++)
                    dtGrdTotales.Columns[i].Width = dtgrdMayor.Columns[i].Width;
            }
            catch
            {
            }
        }
    }
}
