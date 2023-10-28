namespace Contabilidad
{
    partial class FormAsientos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAsientos));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtFechaFinal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFechaInicial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCliente = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbDiferencia = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAsientoActual = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCodigo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.checkBoxFechaMensual = new System.Windows.Forms.CheckBox();
            this.checkBoxOculto = new System.Windows.Forms.CheckBox();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.numImporte = new System.Windows.Forms.NumericUpDown();
            this.btnCerrarAsiento = new System.Windows.Forms.Button();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.cbCuenta = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbHaber = new System.Windows.Forms.RadioButton();
            this.cbFecha = new System.Windows.Forms.ComboBox();
            this.rbDebe = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnEliminarRow = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripExportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripVistaP = new System.Windows.Forms.ToolStripButton();
            this.toolStripPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnAsientosPredetermindados = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.asientoDeCierreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvAsientos = new System.Windows.Forms.ListView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numImporte)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 165;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.lvAsientos);
            this.splitContainer1.Size = new System.Drawing.Size(992, 546);
            this.splitContainer1.SplitterDistance = 175;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtFechaFinal);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.txtFechaInicial);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.lbCliente);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(992, 175);
            this.splitContainer2.SplitterDistance = 230;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtFechaFinal
            // 
            this.txtFechaFinal.Location = new System.Drawing.Point(120, 81);
            this.txtFechaFinal.Name = "txtFechaFinal";
            this.txtFechaFinal.Size = new System.Drawing.Size(79, 20);
            this.txtFechaFinal.TabIndex = 4;
            this.txtFechaFinal.Text = "31/12/2012";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "a";
            // 
            // txtFechaInicial
            // 
            this.txtFechaInicial.Location = new System.Drawing.Point(16, 81);
            this.txtFechaInicial.Name = "txtFechaInicial";
            this.txtFechaInicial.Size = new System.Drawing.Size(79, 20);
            this.txtFechaInicial.TabIndex = 2;
            this.txtFechaInicial.Text = "01/01/2012";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Periodo:";
            // 
            // lbCliente
            // 
            this.lbCliente.AutoSize = true;
            this.lbCliente.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCliente.ForeColor = System.Drawing.Color.Blue;
            this.lbCliente.Location = new System.Drawing.Point(12, 9);
            this.lbCliente.Name = "lbCliente";
            this.lbCliente.Size = new System.Drawing.Size(83, 22);
            this.lbCliente.TabIndex = 0;
            this.lbCliente.Text = "lbCliente";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbDiferencia);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtAsientoActual);
            this.groupBox2.Location = new System.Drawing.Point(600, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(150, 161);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Info";
            // 
            // lbDiferencia
            // 
            this.lbDiferencia.AutoSize = true;
            this.lbDiferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDiferencia.Location = new System.Drawing.Point(24, 84);
            this.lbDiferencia.Name = "lbDiferencia";
            this.lbDiferencia.Size = new System.Drawing.Size(93, 20);
            this.lbDiferencia.TabIndex = 11;
            this.lbDiferencia.Text = "lbDiferencia";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Diferencia:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Asiento Nº";
            // 
            // txtAsientoActual
            // 
            this.txtAsientoActual.Enabled = false;
            this.txtAsientoActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAsientoActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtAsientoActual.Location = new System.Drawing.Point(69, 13);
            this.txtAsientoActual.Name = "txtAsientoActual";
            this.txtAsientoActual.Size = new System.Drawing.Size(47, 29);
            this.txtAsientoActual.TabIndex = 8;
            this.txtAsientoActual.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCodigo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.checkBoxFechaMensual);
            this.groupBox1.Controls.Add(this.checkBoxOculto);
            this.groupBox1.Controls.Add(this.txtDetalle);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtImporte);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.numImporte);
            this.groupBox1.Controls.Add(this.btnCerrarAsiento);
            this.groupBox1.Controls.Add(this.btnIngresar);
            this.groupBox1.Controls.Add(this.cbCuenta);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rbHaber);
            this.groupBox1.Controls.Add(this.cbFecha);
            this.groupBox1.Controls.Add(this.rbDebe);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(591, 161);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // cbCodigo
            // 
            this.cbCodigo.FormattingEnabled = true;
            this.cbCodigo.Location = new System.Drawing.Point(19, 35);
            this.cbCodigo.Name = "cbCodigo";
            this.cbCodigo.Size = new System.Drawing.Size(83, 21);
            this.cbCodigo.TabIndex = 18;
            this.cbCodigo.SelectedIndexChanged += new System.EventHandler(this.cbCodigo_SelectedIndexChanged);
            this.cbCodigo.Click += new System.EventHandler(this.cbCodigo_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Codigo";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(19, 84);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(132, 20);
            this.dateTimePicker1.TabIndex = 5;
            this.dateTimePicker1.Visible = false;
            // 
            // checkBoxFechaMensual
            // 
            this.checkBoxFechaMensual.AutoSize = true;
            this.checkBoxFechaMensual.Checked = true;
            this.checkBoxFechaMensual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFechaMensual.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFechaMensual.Location = new System.Drawing.Point(167, 85);
            this.checkBoxFechaMensual.Name = "checkBoxFechaMensual";
            this.checkBoxFechaMensual.Size = new System.Drawing.Size(60, 16);
            this.checkBoxFechaMensual.TabIndex = 6;
            this.checkBoxFechaMensual.Text = "Mensual";
            this.checkBoxFechaMensual.UseVisualStyleBackColor = true;
            this.checkBoxFechaMensual.CheckedChanged += new System.EventHandler(this.checkBoxFechaMensual_CheckedChanged);
            // 
            // checkBoxOculto
            // 
            this.checkBoxOculto.AutoSize = true;
            this.checkBoxOculto.Location = new System.Drawing.Point(422, 127);
            this.checkBoxOculto.Name = "checkBoxOculto";
            this.checkBoxOculto.Size = new System.Drawing.Size(57, 17);
            this.checkBoxOculto.TabIndex = 17;
            this.checkBoxOculto.Text = "Oculto";
            this.checkBoxOculto.UseVisualStyleBackColor = true;
            this.checkBoxOculto.Visible = false;
            // 
            // txtDetalle
            // 
            this.txtDetalle.Location = new System.Drawing.Point(19, 127);
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.Size = new System.Drawing.Size(386, 20);
            this.txtDetalle.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Detalle Adicional";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(364, 35);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(122, 20);
            this.txtImporte.TabIndex = 1;
            this.txtImporte.Text = "0.00";
            this.txtImporte.TextChanged += new System.EventHandler(this.txtImporte_TextChanged);
            this.txtImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporte_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(503, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(82, 22);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // numImporte
            // 
            this.numImporte.DecimalPlaces = 2;
            this.numImporte.Location = new System.Drawing.Point(414, 50);
            this.numImporte.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numImporte.Name = "numImporte";
            this.numImporte.Size = new System.Drawing.Size(83, 20);
            this.numImporte.TabIndex = 14;
            this.numImporte.ThousandsSeparator = true;
            this.numImporte.Visible = false;
            // 
            // btnCerrarAsiento
            // 
            this.btnCerrarAsiento.Enabled = false;
            this.btnCerrarAsiento.Location = new System.Drawing.Point(503, 86);
            this.btnCerrarAsiento.Name = "btnCerrarAsiento";
            this.btnCerrarAsiento.Size = new System.Drawing.Size(82, 23);
            this.btnCerrarAsiento.TabIndex = 7;
            this.btnCerrarAsiento.Text = "Cerrar Asiento";
            this.btnCerrarAsiento.UseVisualStyleBackColor = true;
            this.btnCerrarAsiento.Click += new System.EventHandler(this.btnCerrarAsiento_Click);
            // 
            // btnIngresar
            // 
            this.btnIngresar.Location = new System.Drawing.Point(503, 36);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(82, 44);
            this.btnIngresar.TabIndex = 6;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // cbCuenta
            // 
            this.cbCuenta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCuenta.FormattingEnabled = true;
            this.cbCuenta.Location = new System.Drawing.Point(108, 35);
            this.cbCuenta.Name = "cbCuenta";
            this.cbCuenta.Size = new System.Drawing.Size(250, 21);
            this.cbCuenta.TabIndex = 0;
            this.cbCuenta.SelectedIndexChanged += new System.EventHandler(this.cbCuenta_SelectedIndexChanged);
            this.cbCuenta.Click += new System.EventHandler(this.cbCuenta_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Importe [$]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Cuenta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fecha";
            // 
            // rbHaber
            // 
            this.rbHaber.AutoSize = true;
            this.rbHaber.Location = new System.Drawing.Point(283, 87);
            this.rbHaber.Name = "rbHaber";
            this.rbHaber.Size = new System.Drawing.Size(62, 17);
            this.rbHaber.TabIndex = 4;
            this.rbHaber.Text = "HABER";
            this.rbHaber.UseVisualStyleBackColor = true;
            // 
            // cbFecha
            // 
            this.cbFecha.FormattingEnabled = true;
            this.cbFecha.Location = new System.Drawing.Point(19, 83);
            this.cbFecha.Name = "cbFecha";
            this.cbFecha.Size = new System.Drawing.Size(132, 21);
            this.cbFecha.TabIndex = 2;
            // 
            // rbDebe
            // 
            this.rbDebe.AutoSize = true;
            this.rbDebe.Checked = true;
            this.rbDebe.Location = new System.Drawing.Point(283, 68);
            this.rbDebe.Name = "rbDebe";
            this.rbDebe.Size = new System.Drawing.Size(54, 17);
            this.rbDebe.TabIndex = 3;
            this.rbDebe.TabStop = true;
            this.rbDebe.Text = "DEBE";
            this.rbDebe.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnEliminarRow,
            this.toolStripBtnEliminar,
            this.toolStripBtnModificar,
            this.toolStripSeparator1,
            this.toolStripExportExcel,
            this.toolStripVistaP,
            this.toolStripPrint,
            this.toolStripSeparator2,
            this.toolStripBtnAsientosPredetermindados,
            this.toolStripSeparator3,
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(41, 363);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnEliminarRow
            // 
            this.toolStripBtnEliminarRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnEliminarRow.Image = global::Contabilidad.Properties.Resources.row_delete32;
            this.toolStripBtnEliminarRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnEliminarRow.Name = "toolStripBtnEliminarRow";
            this.toolStripBtnEliminarRow.Size = new System.Drawing.Size(38, 28);
            this.toolStripBtnEliminarRow.Text = "toolStripButton1";
            this.toolStripBtnEliminarRow.ToolTipText = "Elimina solo la linea seleccionada del Asiento";
            this.toolStripBtnEliminarRow.Click += new System.EventHandler(this.toolStripBtnEliminarRow_Click);
            // 
            // toolStripBtnEliminar
            // 
            this.toolStripBtnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnEliminar.Image")));
            this.toolStripBtnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnEliminar.Name = "toolStripBtnEliminar";
            this.toolStripBtnEliminar.Size = new System.Drawing.Size(38, 28);
            this.toolStripBtnEliminar.Text = "Eliminar";
            this.toolStripBtnEliminar.ToolTipText = "Eliminar el Asiento completo.";
            this.toolStripBtnEliminar.Click += new System.EventHandler(this.toolStripBtnEliminar_Click);
            // 
            // toolStripBtnModificar
            // 
            this.toolStripBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnModificar.Image")));
            this.toolStripBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnModificar.Name = "toolStripBtnModificar";
            this.toolStripBtnModificar.Size = new System.Drawing.Size(38, 28);
            this.toolStripBtnModificar.Text = "Modificar";
            this.toolStripBtnModificar.Click += new System.EventHandler(this.toolStripBtnModificar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(38, 6);
            // 
            // toolStripExportExcel
            // 
            this.toolStripExportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExportExcel.Image")));
            this.toolStripExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExportExcel.Name = "toolStripExportExcel";
            this.toolStripExportExcel.Size = new System.Drawing.Size(38, 28);
            this.toolStripExportExcel.Text = "ExportExcel";
            this.toolStripExportExcel.Click += new System.EventHandler(this.toolStripExportExcel_Click);
            // 
            // toolStripVistaP
            // 
            this.toolStripVistaP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripVistaP.Image = ((System.Drawing.Image)(resources.GetObject("toolStripVistaP.Image")));
            this.toolStripVistaP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripVistaP.Name = "toolStripVistaP";
            this.toolStripVistaP.Size = new System.Drawing.Size(38, 28);
            this.toolStripVistaP.Text = "Vista Preliminar";
            this.toolStripVistaP.Click += new System.EventHandler(this.toolStripVistaP_Click);
            // 
            // toolStripPrint
            // 
            this.toolStripPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPrint.Image")));
            this.toolStripPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPrint.Name = "toolStripPrint";
            this.toolStripPrint.Size = new System.Drawing.Size(38, 28);
            this.toolStripPrint.Text = "Imprimir";
            this.toolStripPrint.Click += new System.EventHandler(this.toolStripPrint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(38, 6);
            // 
            // toolStripBtnAsientosPredetermindados
            // 
            this.toolStripBtnAsientosPredetermindados.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnAsientosPredetermindados.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnAsientosPredetermindados.Image")));
            this.toolStripBtnAsientosPredetermindados.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnAsientosPredetermindados.Name = "toolStripBtnAsientosPredetermindados";
            this.toolStripBtnAsientosPredetermindados.Size = new System.Drawing.Size(38, 28);
            this.toolStripBtnAsientosPredetermindados.Text = "toolStripButton1";
            this.toolStripBtnAsientosPredetermindados.ToolTipText = "Asientos Predeterminados";
            this.toolStripBtnAsientosPredetermindados.Click += new System.EventHandler(this.toolStripBtnAsientosPredetermindados_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(38, 6);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asientoDeCierreToolStripMenuItem});
            this.toolStripSplitButton1.Image = global::Contabilidad.Properties.Resources.List;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(38, 28);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // asientoDeCierreToolStripMenuItem
            // 
            this.asientoDeCierreToolStripMenuItem.Name = "asientoDeCierreToolStripMenuItem";
            this.asientoDeCierreToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.asientoDeCierreToolStripMenuItem.Text = "Asiento de Cierre";
            this.asientoDeCierreToolStripMenuItem.Click += new System.EventHandler(this.asientoDeCierreToolStripMenuItem_Click);
            // 
            // lvAsientos
            // 
            this.lvAsientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAsientos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvAsientos.FullRowSelect = true;
            this.lvAsientos.HideSelection = false;
            this.lvAsientos.Location = new System.Drawing.Point(44, 0);
            this.lvAsientos.Name = "lvAsientos";
            this.lvAsientos.Size = new System.Drawing.Size(944, 365);
            this.lvAsientos.TabIndex = 0;
            this.lvAsientos.UseCompatibleStateImageBehavior = false;
            this.lvAsientos.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvAsientos_ColumnClick);
            // 
            // FormAsientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 546);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormAsientos";
            this.Text = "FormAsientos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAsientos_FormClosing);
            this.Load += new System.EventHandler(this.FormAsientos_Load);
            this.Shown += new System.EventHandler(this.FormAsientos_Shown);
            this.Enter += new System.EventHandler(this.FormAsientos_Enter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numImporte)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCliente;
        private System.Windows.Forms.TextBox txtFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFechaInicial;
        private System.Windows.Forms.ListView lvAsientos;
        private System.Windows.Forms.ComboBox cbFecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCuenta;
        private System.Windows.Forms.RadioButton rbHaber;
        private System.Windows.Forms.RadioButton rbDebe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAsientoActual;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCerrarAsiento;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbDiferencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnEliminar;
        private System.Windows.Forms.ToolStripButton toolStripBtnModificar;
        private System.Windows.Forms.NumericUpDown numImporte;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripPrint;
        private System.Windows.Forms.ToolStripButton toolStripVistaP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripBtnAsientosPredetermindados;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem asientoDeCierreToolStripMenuItem;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxOculto;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox checkBoxFechaMensual;
        private System.Windows.Forms.ToolStripButton toolStripBtnEliminarRow;
        private System.Windows.Forms.ToolStripButton toolStripExportExcel;
        private System.Windows.Forms.ComboBox cbCodigo;
        private System.Windows.Forms.Label label9;
    }
}