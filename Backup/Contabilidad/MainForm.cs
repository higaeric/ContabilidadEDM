﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Contabilidad
{
    public partial class MainForm : Form
    {
        public EDM.EDM edm;
        Dictionary<int, BasicForm> childForms;
        public EDM.Empresa empresa;
        public EDM.AdapterAsiento adapterAsiento;

        public MainForm()
        {
            InitializeComponent();
            childForms = new Dictionary<int, BasicForm>();
            edm = new EDM.EDM(System.Windows.Forms.Application.StartupPath);
            menuItemDisable();

            if (!CheckingRegistry())
            {
                this.Close();
                this.Dispose();
            }
        }

        private void menuItemDisable()
        {
            this.guardarComoToolStripMenuItem.Enabled = false;
            this.guardarComoToolStripMenuItem.Enabled = false;
            this.imprimirToolStripMenuItem.Enabled = false;
            this.exportarToolStripMenuItem.Enabled = false;

            this.asientosToolStripMenuItem.Enabled = false;
            this.saldosToolStripMenuItem.Enabled = false;
            this.mayorToolStripMenuItem.Enabled = false;
        }

        private void menuItemEnable()
        {
            this.guardarComoToolStripMenuItem.Enabled = true;
            this.guardarComoToolStripMenuItem.Enabled = true;
            this.imprimirToolStripMenuItem.Enabled = true;
            this.exportarToolStripMenuItem.Enabled = true;

            this.asientosToolStripMenuItem.Enabled = true;
            this.saldosToolStripMenuItem.Enabled = true;
            this.mayorToolStripMenuItem.Enabled = true;
        }

        private void mensajeProximamente(string msg)
        {
            MessageBox.Show(msg, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void asientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowAsientos();
        }

        private void planDeCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowPlanDeCuentas();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNuevaEmpresa();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOpenEmpresa();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void saldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowSaldos();
        }

        private void mayorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMayor();
        }

        private void mosaicoHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mosaicoVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mensajeProximamente("En construccion, Funcion aun no disponible.");
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mensajeProximamente("En construccion, Funcion aun no disponible.");
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //mensajeProximamente("En construccion, si desea imprimir realizarlo directamente en el icono de la ventana.");
            var f = childForms.FirstOrDefault(c => c.Value.ContainsFocus);
            if (f.Value == null) return;

            switch (f.Value.type)
            { 
                case tableType.Asientos:
                    var tmp = (FormAsientos)f.Value;
                    tmp.PrintExternal();
                    break;
                case tableType.Saldos:
                    var tmp2 = (FormSaldos)f.Value;
                    tmp2.PrintExternal();
                    break;
                case tableType.Mayor:
                    var tmp3 = (FormMayor)f.Value;
                    tmp3.PrintExternal();
                    break;
            }

        }
    }
}
