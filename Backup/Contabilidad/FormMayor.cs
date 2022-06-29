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
        int codigo;
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
            
            DataGridViewTextBoxColumn acumDColumn = new DataGridViewTextBoxColumn();
            acumDColumn.DataPropertyName = "Debe";
            acumDColumn.HeaderText = "Debitos";
            acumDColumn.DefaultCellStyle.Format = "N2";

            DataGridViewTextBoxColumn acumHColumn = new DataGridViewTextBoxColumn();
            acumHColumn.DataPropertyName = "Haber";
            acumHColumn.HeaderText = "Creditos";
            acumHColumn.DefaultCellStyle.Format = "N2";

            dtgrdMayor.Columns.Add(acumDColumn);
            dtgrdMayor.Columns.Add(acumHColumn);

            BindingList<EDM.Entity.Mayor> mayorizacion = new BindingList<EDM.Entity.Mayor>();

            foreach (EDM.Entity.Registro reg in registros)
            { 
                if(reg.valueType== EDM.Entity.ValueType.Debe)
                    mayorizacion.Add(new EDM.Entity.Mayor(reg.Codigo,reg.Description,
                        reg.Valor,0));
                else
                    mayorizacion.Add(new EDM.Entity.Mayor(reg.Codigo,reg.Description,
                        0, reg.Valor));
            }

            dtgrdMayor.DataSource = mayorizacion;
        }

        private void adjustColumns(bool autoAdjust)
        {
            //int lastVisibleColumnIndex = 0;
            //foreach (DataGridViewColumn col in this.dtgrdMayor.Columns)
            //{
            //    if (col.Visible == true)
            //        lastVisibleColumnIndex = col.Index;
            //}
            //foreach (DataGridViewColumn col in this.dtgrdMayor.Columns)
            //{
            //    if (col.Index == lastVisibleColumnIndex)
            //        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //    else
            //        if (autoAdjust)
            //            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //        else
            //            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //}

            //Como son dos columnas unicamente hago el datagrid a la mitad.
            int mitad = this.dtgrdMayor.Width / 2 - 25;
            foreach (DataGridViewColumn col in this.dtgrdMayor.Columns)
            {
                //int size = col.Width;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Width = mitad; //size;
            }
        }

        private void dtgrdMayor_SizeChanged(object sender, EventArgs e)
        {
            adjustColumns(true);
        }


    }
}
