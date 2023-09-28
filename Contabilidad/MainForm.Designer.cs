namespace Contabilidad
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarAsientoAperturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.respaldarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.recuperarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vistaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asientosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planDeCuentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saldosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mayorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vistaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mosaicoHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mosaicoVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.vistaToolStripMenuItem,
            this.vistaToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(704, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.toolStripMenuItem1,
            this.guardarComoToolStripMenuItem,
            this.importarAsientoAperturaToolStripMenuItem,
            this.exportarToolStripMenuItem,
            this.backupToolStripMenuItem,
            this.toolStripMenuItem2,
            this.imprimirToolStripMenuItem,
            this.toolStripMenuItem3,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(209, 6);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Enabled = false;
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar Como";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // importarAsientoAperturaToolStripMenuItem
            // 
            this.importarAsientoAperturaToolStripMenuItem.Name = "importarAsientoAperturaToolStripMenuItem";
            this.importarAsientoAperturaToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.importarAsientoAperturaToolStripMenuItem.Text = "Importar Asiento Apertura";
            this.importarAsientoAperturaToolStripMenuItem.ToolTipText = "Inserta el Asiento de Apertura de un Periodo Anterior.\r\n";
            this.importarAsientoAperturaToolStripMenuItem.Click += new System.EventHandler(this.importarAsientoAperturaToolStripMenuItem_Click);
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.Enabled = false;
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.exportarToolStripMenuItem.Text = "Exportar";
            this.exportarToolStripMenuItem.Click += new System.EventHandler(this.exportarToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.respaldarToolStripMenuItem,
            this.toolStripMenuItem5,
            this.recuperarToolStripMenuItem});
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            // 
            // respaldarToolStripMenuItem
            // 
            this.respaldarToolStripMenuItem.Name = "respaldarToolStripMenuItem";
            this.respaldarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.respaldarToolStripMenuItem.Text = "Respaldar";
            this.respaldarToolStripMenuItem.Click += new System.EventHandler(this.respaldarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(124, 6);
            // 
            // recuperarToolStripMenuItem
            // 
            this.recuperarToolStripMenuItem.Name = "recuperarToolStripMenuItem";
            this.recuperarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.recuperarToolStripMenuItem.Text = "Recuperar";
            this.recuperarToolStripMenuItem.Click += new System.EventHandler(this.recuperarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 6);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(209, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // vistaToolStripMenuItem
            // 
            this.vistaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asientosToolStripMenuItem,
            this.planDeCuentasToolStripMenuItem,
            this.saldosToolStripMenuItem,
            this.mayorToolStripMenuItem});
            this.vistaToolStripMenuItem.Name = "vistaToolStripMenuItem";
            this.vistaToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.vistaToolStripMenuItem.Text = "Gestion";
            // 
            // asientosToolStripMenuItem
            // 
            this.asientosToolStripMenuItem.Name = "asientosToolStripMenuItem";
            this.asientosToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.asientosToolStripMenuItem.Text = "Asientos";
            this.asientosToolStripMenuItem.Click += new System.EventHandler(this.asientosToolStripMenuItem_Click);
            // 
            // planDeCuentasToolStripMenuItem
            // 
            this.planDeCuentasToolStripMenuItem.Name = "planDeCuentasToolStripMenuItem";
            this.planDeCuentasToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.planDeCuentasToolStripMenuItem.Text = "Plan de Cuentas";
            this.planDeCuentasToolStripMenuItem.Click += new System.EventHandler(this.planDeCuentasToolStripMenuItem_Click);
            // 
            // saldosToolStripMenuItem
            // 
            this.saldosToolStripMenuItem.Name = "saldosToolStripMenuItem";
            this.saldosToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saldosToolStripMenuItem.Text = "Saldos";
            this.saldosToolStripMenuItem.Click += new System.EventHandler(this.saldosToolStripMenuItem_Click);
            // 
            // mayorToolStripMenuItem
            // 
            this.mayorToolStripMenuItem.Name = "mayorToolStripMenuItem";
            this.mayorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.mayorToolStripMenuItem.Text = "Mayor";
            this.mayorToolStripMenuItem.Click += new System.EventHandler(this.mayorToolStripMenuItem_Click);
            // 
            // vistaToolStripMenuItem1
            // 
            this.vistaToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mosaicoHorizontalToolStripMenuItem,
            this.mosaicoVerticalToolStripMenuItem,
            this.cascadaToolStripMenuItem,
            this.toolStripMenuItem4});
            this.vistaToolStripMenuItem1.Name = "vistaToolStripMenuItem1";
            this.vistaToolStripMenuItem1.Size = new System.Drawing.Size(66, 20);
            this.vistaToolStripMenuItem1.Text = "Ventanas";
            // 
            // mosaicoHorizontalToolStripMenuItem
            // 
            this.mosaicoHorizontalToolStripMenuItem.Name = "mosaicoHorizontalToolStripMenuItem";
            this.mosaicoHorizontalToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.mosaicoHorizontalToolStripMenuItem.Text = "Mosaico Horizontal";
            this.mosaicoHorizontalToolStripMenuItem.Click += new System.EventHandler(this.mosaicoHorizontalToolStripMenuItem_Click);
            // 
            // mosaicoVerticalToolStripMenuItem
            // 
            this.mosaicoVerticalToolStripMenuItem.Name = "mosaicoVerticalToolStripMenuItem";
            this.mosaicoVerticalToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.mosaicoVerticalToolStripMenuItem.Text = "Mosaico Vertical";
            this.mosaicoVerticalToolStripMenuItem.Click += new System.EventHandler(this.mosaicoVerticalToolStripMenuItem_Click);
            // 
            // cascadaToolStripMenuItem
            // 
            this.cascadaToolStripMenuItem.Name = "cascadaToolStripMenuItem";
            this.cascadaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.cascadaToolStripMenuItem.Text = "Cascada";
            this.cascadaToolStripMenuItem.Click += new System.EventHandler(this.cascadaToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(174, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(704, 389);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "EDM Balance v.5.3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vistaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asientosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planDeCuentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saldosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mayorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vistaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mosaicoHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mosaicoVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem importarAsientoAperturaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem respaldarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem recuperarToolStripMenuItem;
    }
}

