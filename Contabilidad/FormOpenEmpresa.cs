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
    public partial class FormOpenEmpresa : Form
    {
        public EDM.Empresa EmpresaActual;
        public string ArchivoSeleccionado;
        EDM.EDM edm;

        public FormOpenEmpresa(EDM.EDM edm_)
        {
            edm = edm_;
            InitializeComponent();
            loadFiles();
        }

        public FormOpenEmpresa(EDM.EDM edm_, EDM.Empresa empresaCargada)
        {
            edm = edm_;
            InitializeComponent();
            loadFiles(empresaCargada.Name, empresaCargada.FullPath);
        }
      
        private void loadFiles()
        {
            lvArchivos.Clear();
            lvArchivos.View = View.Details;
            lvArchivos.Columns.Add("Nombre", 160);
            lvArchivos.Columns.Add("Fecha Inicio", 70);
            lvArchivos.Columns.Add("Fecha Final", 70);
            lvArchivos.MultiSelect = false;
            lvArchivos.FullRowSelect = true;
            lvArchivos.GridLines = true;

            //Cargar items.
            string[] archivos = System.IO.Directory.GetFiles(
                System.Windows.Forms.Application.StartupPath + "\\Data",
                "*.erc", System.IO.SearchOption.TopDirectoryOnly);
            foreach (string str in archivos)
            {
                string name = "";
                DateTime fInicio;
                DateTime fFinal;
                EDM.EmpresaArchivo.unTanslateStdName(System.IO.Path.GetFileNameWithoutExtension(str),
                    out name, out fInicio, out fFinal);

                ListViewItem item = new ListViewItem();
                item.Name = str;
                item.Text = name;
                item.SubItems.Add(fInicio.ToShortDateString());
                item.SubItems.Add(fFinal.ToShortDateString());
                lvArchivos.Items.Add(item);
            }

            lvArchivos.Sorting = SortOrder.Ascending;
            lvArchivos.Sort();
        }

        /// <summary>
        /// Carga para seleccionar periodos anteriores.
        /// No utilizarlo para el inicial
        /// </summary>
        /// <param name="search"></param>
        private void loadFiles(string search, string pathActual)
        {
            lvArchivos.Clear();
            lvArchivos.View = View.Details;
            lvArchivos.Columns.Add("Nombre", 160);
            lvArchivos.Columns.Add("Fecha Inicio", 70);
            lvArchivos.Columns.Add("Fecha Final", 70);
            lvArchivos.MultiSelect = false;
            lvArchivos.FullRowSelect = true;
            lvArchivos.GridLines = true;

            //Cargar items.
            string[] archivos = System.IO.Directory.GetFiles(
                System.Windows.Forms.Application.StartupPath + "\\Data",
                "*.erc", System.IO.SearchOption.TopDirectoryOnly);
            foreach (string str in archivos)
            {
                if (str.ToLower() == pathActual.ToLower())  //Sisgnifica q es el mismo archivo actual.
                    continue;

                string name = "";
                DateTime fInicio;
                DateTime fFinal;
                EDM.EmpresaArchivo.unTanslateStdName(System.IO.Path.GetFileNameWithoutExtension(str),
                    out name, out fInicio, out fFinal);
                
                
                string nameComp =name.Trim().ToLower();
                string searchComp = search.Trim().ToLower();
                if (nameComp != searchComp)
                {
                    int comparacion =EDM.EDM.ComputeLevenshteinDistance(nameComp, searchComp);
                    if (comparacion > 3)
                        continue;
                }
                
                ListViewItem item = new ListViewItem();
                item.Name = str;
                item.Text = name;
                item.SubItems.Add(fInicio.ToShortDateString());
                item.SubItems.Add(fFinal.ToShortDateString());
                lvArchivos.Items.Add(item);
                
            }

            lvArchivos.Sorting = SortOrder.Ascending;
            lvArchivos.Sort();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            abrirSeleccionado();
        }

        private void lvArchivos_DoubleClick(object sender, EventArgs e)
        {
            abrirSeleccionado();
        }

        private void abrirSeleccionado()
        { 
            if (lvArchivos.SelectedItems == null || lvArchivos.SelectedItems.Count == 0)
                return;
            ListViewItem l = lvArchivos.SelectedItems[0];
            ArchivoSeleccionado = l.Name;
            EmpresaActual = new EDM.Empresa(l.Text, Convert.ToDateTime(l.SubItems[1].Text), 
                Convert.ToDateTime(l.SubItems[2].Text), l.Name);

            this.Hide();           
        }
    }
}
