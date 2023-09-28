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
    public partial class MainForm : Form
    {
        private int addForm(tableType type_)
        {
            int formID = childForms.Count;

            if(type_== tableType.Asientos)
            {
                FormAsientos f = new FormAsientos(formID,"Asientos",tableType.Asientos);
                f.MdiParent = this;
                f.Text = f.formDescription;
                childForms.Add(f.formID, f);
                f.Show();
            }
            else if (type_ == tableType.Saldos)
            {
                FormSaldos f = new FormSaldos(formID, "Saldos", tableType.Saldos);
                f.MdiParent = this;
                f.Text = f.formDescription;
                childForms.Add(f.formID, f);
                f.Show();
            }
            else if (type_ == tableType.Mayor)
            {
                FormMayor f = new FormMayor(formID, "Mayor", tableType.Mayor);
                f.MdiParent = this;
                f.Text = f.formDescription;
                childForms.Add(f.formID, f);
                f.Show();

            }
            SetMDIList();
            return formID;
        }

        public void SetMDIList()
        {
            // Create the MenuItem to be used to display an MDI list.
            MenuItem menuItem1 = new MenuItem();
            // Set this menu item to be used as an MDI list.
            menuItem1.MdiList = true;
        }

        private bool FindChildForm(tableType type_)
        {
            for (int i = 0; i < childForms.Count; i++)
            {
                if (childForms.ElementAt(i).Value.type == type_ &&
                    childForms.ElementAt(i).Value.IsDisposed==false)
                {
                    childForms.ElementAt(i).Value.Focus();
                    return true;
                }
            }
            return false;
        }
        private void closeAllChildForms()
        {
            foreach (KeyValuePair<int, BasicForm> kvp in childForms)
            {
                kvp.Value.Close();
                kvp.Value.Dispose();
            }
            childForms.Clear();
        }
        #region "Show Forms"
        private void ShowNuevaEmpresa()
        {
            closeAllChildForms();
            FormAddEmpresa f = new FormAddEmpresa(edm);
            f.ShowDialog();
            if (f.EmpresaActual == null) return;

            empresa = f.EmpresaActual;
            string periodoAnteriorPath = f.PeriodoAnteriorFullPath;
            this.menuItemEnable();
            f.Dispose();
            f = null;

            LoadAsientosInFile();
            
            this.BackgroundImageLayout = ImageLayout.Center;
            this.BackgroundImage = generateImageFromString(empresa.Name);

            //Si se selecciono un periodo anterior Carga el asiento de apertura
            if (periodoAnteriorPath!=null && periodoAnteriorPath != "")
            {
                LoadAsientoAperturaDelPeriodoAnterior(periodoAnteriorPath);
                ShowAsientos();
            }
        }

        private void ShowOpenEmpresa()
        {
            closeAllChildForms();
            FormOpenEmpresa form = new FormOpenEmpresa(edm);
            form.ShowDialog();
            if (form.EmpresaActual == null) return;
            empresa = form.EmpresaActual;
            this.menuItemEnable();
            form.Dispose();
            form = null;

            //Cargar archivo. DATA asientos.
            LoadAsientosInFile();

            this.BackgroundImageLayout = ImageLayout.Center;
            this.BackgroundImage = generateImageFromString(empresa.Name);
        }

        private void ShowAsientos()
        {
            //Find Existing ChildForm
            if (FindChildForm(tableType.Asientos))
                return;

            int formid = addForm(tableType.Asientos);
            FormAsientos form = (FormAsientos)this.childForms[formid];
            form.loadPDC(edm);
            form.LoadDataInfo(this.empresa, this.adapterAsiento.Asientos);
            form.WindowState = FormWindowState.Maximized;
            
            return;
        }

        private void ShowSaldos()
        {
            //Find Existing ChildForm
            if (FindChildForm(tableType.Saldos))
                return;

            int formid = addForm(tableType.Saldos);
            FormSaldos form = (FormSaldos)this.childForms[formid];
            form.LoadDataInfo(this.empresa, this.adapterAsiento.Asientos);
            form.WindowState = FormWindowState.Maximized;
            return;
        }

        private void ShowMayor()
        {
            //delta cuentas, solo las utilizadas en los asientos.
            Dictionary<int, EDM.Entity.Cuenta> planCuentasFiltrado = new Dictionary<int, EDM.Entity.Cuenta>();
            {
                List<int> idCuentasUtilizadas = new List<int>();
                foreach (EDM.Entity.Asiento asien in adapterAsiento.Asientos)
                {
                    idCuentasUtilizadas.AddRange(asien.Registros.Select(x => x.Codigo));
                }

                if(idCuentasUtilizadas.Count==0)
                {
                    MessageBox.Show("No hay cuentas seleccionadas en Asientos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                idCuentasUtilizadas = idCuentasUtilizadas.Distinct().ToList();
                planCuentasFiltrado = edm.PDC.planDeCuentas.Where(x => idCuentasUtilizadas.Contains(x.Key)).ToDictionary(t=> t.Key, t=>t.Value);
            }

            FormSelectMayor fselect = new FormSelectMayor(planCuentasFiltrado);
            fselect.ShowDialog();
            int seleccionado = fselect.idCuenta;
            if (seleccionado == 0) return;

            //Agrego el id seleccionado y las subcuentas.
            List<int> idsMayorizar = new List<int>();
            idsMayorizar.Add(seleccionado);
            var subId = from c in edm.PDC.planDeCuentas
                        where c.Value.CuentaPadre == seleccionado
                        select c.Value.Codigo;
            idsMayorizar.AddRange(subId);


            //busco x codigo
            List<EDM.Entity.RegistroMayor> res = new List<EDM.Entity.RegistroMayor>();

            foreach (int oneId in idsMayorizar)
            {
                foreach (EDM.Entity.Asiento asien in adapterAsiento.Asientos)
                    foreach (EDM.Entity.Registro reg in asien.Registros)
                        if (reg.Codigo == oneId)
                            res.Add(new EDM.Entity.RegistroMayor(asien.Numero, asien.Fecha, reg.idRegistro, 
                                reg.Codigo, reg.Description, reg.valueType, 
                                reg.Valor, reg.Details, reg.isHidden));
            }

            int formid = addForm(tableType.Mayor);
            FormMayor form = (FormMayor)this.childForms[formid];
            
            form.LoadDataInfo(res, fselect.nombreCuenta);
            form.WindowState = FormWindowState.Maximized;
            return;
        }

        private void ShowPlanDeCuentas()
        {
            FormPlanDeCuentas form = new FormPlanDeCuentas(edm);
            form.ShowDialog();
        }

        #endregion

        public static Bitmap generateImageFromString(string strDisplay)
        {
            Color fontColor = Color.Gold; //.SlateBlue;
            Color backColor = Color.Transparent;
            string fontName = "Stylus BT"; //"Comic Sans MS";
            int fontSize = 32;
            int height = 200;
            int with = 600;

            Bitmap objBitmap = new Bitmap(with, height);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            //objGraphics = Graphics.FromImage(Image.FromFile("c:\\temp\\fondo.jpg"));

            Font objFont = new Font(fontName, fontSize);
            PointF objPoint = new PointF(5f, 5f);

            SolidBrush objBrushForeColor = new SolidBrush(fontColor);
            SolidBrush objBrushBackColor = new SolidBrush(backColor);

            //objGraphics.DrawImage(Image.FromFile("c:\\temp\\fondo.jpg"), 0, 0);

            objGraphics.FillRectangle(objBrushBackColor, 0, 0, with, height);
            objGraphics.DrawString(strDisplay, objFont, objBrushForeColor, objPoint);


            return objBitmap;
        }


        private void LoadAsientosInFile()
        {
            EDM.Validation.ValidationTransaction tr = EDM.Validation.ValidationIO.Leer(empresa.FullPath);

            adapterAsiento = new EDM.AdapterAsiento();
            adapterAsiento.LoadAsientos(tr.Body);
        }

        /// <summary>
        /// Carga temporalmente el periodo anterior seleccionado, y genera el Asiento de apertura.
        /// La misma lo guarda en el adapterAsiento.
        /// </summary>
        /// <param name="periodoAnteriorPath"></param>
        private bool LoadAsientoAperturaDelPeriodoAnterior(string periodoAnteriorPath)
        {
            EDM.Validation.ValidationTransaction tr = EDM.Validation.ValidationIO.Leer(periodoAnteriorPath);
            EDM.AdapterAsiento auxAdaptAsiento = new EDM.AdapterAsiento();
            auxAdaptAsiento.LoadAsientos(tr.Body);
            EDM.Entity.Asiento asientoCierreAnterior = auxAdaptAsiento.Asientos.FirstOrDefault(c => c.Numero == 0);

            if (asientoCierreAnterior == null)
            {
                MessageBox.Show("El Periodo anterior no contiene el Asiento de Cierre", "Periodo Anterior", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //----------- x las dudas verifico q si hay asientos cargados. y obtengo el maxidReg ----------
            long max = 0;
            int numAsiento=0;
            foreach (EDM.Entity.Asiento oAs in adapterAsiento.Asientos)
            {
                numAsiento = oAs.Numero>numAsiento? oAs.Numero:numAsiento;

                for (int i = 0; i < oAs.Registros.Count; i++)
                {
                    max = oAs.Registros[i].idRegistro > max ? oAs.Registros[i].idRegistro : max;
                }
            }

            //------------------ Carga para el asiento de Apertura -----------------
            List<EDM.Entity.Registro> regsApertura = new List<EDM.Entity.Registro>();
            foreach (EDM.Entity.Registro reg in asientoCierreAnterior.Registros)
            {
                if (reg.Codigo > 2999)  //Necesito tomar la 2da parte (Resultados no asignados) que son los del 1000 y 2000
                    continue;
                int cod = reg.Codigo;
                string descrp = reg.Description;
                double valor = reg.Valor;
                EDM.Entity.ValueType type = reg.valueType == EDM.Entity.ValueType.Debe? EDM.Entity.ValueType.Haber: EDM.Entity.ValueType.Debe;
                max++;

                regsApertura.Add(new EDM.Entity.Registro(max, cod, descrp, type, valor));
            }
            numAsiento++;
            EDM.Entity.Asiento asientoApertura =new EDM.Entity.Asiento(numAsiento, regsApertura, DateTime.Today);
            adapterAsiento.Asientos.Add(asientoApertura);

            EDM.EmpresaArchivo.WriteAsiento(empresa.FullPath, asientoApertura, true);
            return true;
        }

        private void importAsientoApertura()
        {
            FormOpenEmpresa form = new FormOpenEmpresa(edm, empresa);
            form.ShowDialog();
            if (form.EmpresaActual == null) return;

            string PeriodoAnteriorFullPath = form.EmpresaActual.FullPath;
            form.Dispose();
            form = null;

            bool result = LoadAsientoAperturaDelPeriodoAnterior(PeriodoAnteriorFullPath);

            closeAllChildForms();
            ShowAsientos();

        }

        private bool backup()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Procedimiento de Respaldo de Datos.";
            sfd.FileName = "EdmBalanceData.bak";
            sfd.Filter = "*.bak|*.bak";
            sfd.CheckFileExists = false;

            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            
            //Zipear todos los archivos a .bak
            string filename = sfd.FileName;
            string folderdata = EDM.EDM.programPath + "\\Data";
            try
            {
                EDM.EDM.BackupProcess(filename, folderdata);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool recoveryFromBackup()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Procedimiento para Restaurar Datos.";
            ofd.FileName = "EdmBalanceData.bak";
            ofd.Filter = "*.bak|*.bak";
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;
                        
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            string folderdata = EDM.EDM.programPath + "\\Data";
            try
            {
                EDM.EDM.RestoreProcess(ofd.FileName, folderdata);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
