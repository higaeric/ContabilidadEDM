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
    public partial class FormAddEmpresa : Form
    {
        private bool isModify = false;
        public EDM.Empresa EmpresaActual;

        public FormAddEmpresa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor para el caso de Modificacion Fecha de Empresa.
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="fechaI"></param>
        /// <param name="fechaF"></param>
        public FormAddEmpresa(string empresa, string fechaI, string fechaF)
        {
            isModify = true;

            txtEmpresa.Text = empresa;
            txtEmpresa.Enabled = false;

            txtInicio.Text = fechaI;
            txtFinal.Text = fechaF;
            btnAceptar.Text = "Modificar";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DateTime finicio;
            DateTime ffinal;

            if (txtEmpresa.Text == "")
                return;

            //Validar textbox Fechas
            if (!validarFecha(txtInicio.Text))
            {
                MessageBox.Show("Error en la Sintaxis de Fecha de Inicio.\r\n\r\nEj: dd/mm/aaaa", "Error en Fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInicio.Focus();
                return;
            }
            if (!validarFecha(txtFinal.Text))
            {
                MessageBox.Show("Error en la Sintaxis de Fecha Final.\r\n\r\nEj: dd/mm/aaaa", "Error en Fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFinal.Focus();
                return;
            }
            finicio = Convert.ToDateTime(txtInicio.Text);
            ffinal = Convert.ToDateTime(txtFinal.Text);
            if (finicio > ffinal || finicio==ffinal)
            {
                MessageBox.Show("La fecha de inicio es posterior o igual al Final", "Error en Fechas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtInicio.Focus();
                return;
            }

            string path = Application.StartupPath + "\\Data\\" + EDM.EmpresaArchivo.GetStdName(txtEmpresa.Text, finicio, ffinal);
            if (System.IO.File.Exists(path))
            {
                if (MessageBox.Show("Empresa-Periodo Existente.\r\n\r\n¿Desea Reemplazarla?", "Nuevo Archivo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string backupExistente = EDM.EmpresaArchivo.GetNextBackupNameAvailable(path);
                    System.IO.File.Move(path, backupExistente); //Renombro el archivo existente
                }
                else
                {
                    txtEmpresa.Focus();
                    return;
                }
            }

            EmpresaActual = new EDM.Empresa(txtEmpresa.Text, finicio, ffinal, path);

            bool resultado = EDM.EmpresaArchivo.NewFile(EmpresaActual);
            if (!resultado)
            {
                MessageBox.Show("Problema al generar el nuevo archivo", "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmpresaActual = null;
            }
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private bool validarFecha(string text)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                //DateTime dd = DateTime.TryParseExact(text,"dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture,
                DateTime dd = DateTime.Parse(text);
                //DateTime d = Convert.ToDateTime(text); //
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region "KeyPress Validation"
        private void txtEmpresa_KeyUp(object sender, KeyEventArgs e)
        {
        }
        private void txtInicio_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
        private void txtFinal_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            //No se permite caracteres especiales: _@"\/¿?<>
            if (char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar) ||
                e.KeyChar == '.' || e.KeyChar == '-' || e.KeyChar == '&' ||
                e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == '/' ||
                e.KeyChar == '[' || e.KeyChar == ']' || e.KeyChar == '+' ||
                char.IsWhiteSpace(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;        
        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            numericoParaFecha(ref e);
        }

        private void txtFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            numericoParaFecha(ref e);
        }

        private void numericoParaFecha(ref KeyPressEventArgs e)
        { 
            //solo Numeros, barra y guion!!!
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '/' || e.KeyChar == '-')
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        #endregion





    }
}
