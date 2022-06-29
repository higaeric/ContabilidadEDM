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
            FormAddEmpresa f = new FormAddEmpresa();
            f.ShowDialog();
            if (f.EmpresaActual == null) return;

            empresa = f.EmpresaActual;
            this.menuItemEnable();
            f.Dispose();
            f = null;

            this.BackgroundImageLayout = ImageLayout.Center;
            this.BackgroundImage = generateImageFromString(empresa.Name);
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
            FormSelectMayor fselect = new FormSelectMayor(edm.PDC.planDeCuentas);
            fselect.ShowDialog();
            int seleccionado = fselect.idCuenta;
            if (seleccionado == 0) return;

            //busco x codigo
            List<EDM.Entity.Registro> res = new List<EDM.Entity.Registro>();
            foreach (EDM.Entity.Asiento asien in adapterAsiento.Asientos)
                foreach (EDM.Entity.Registro reg in asien.Registros)
                    if (reg.Codigo == seleccionado)
                        res.Add(reg);

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
            int fontSize = 48;
            int height = 200;
            int with = 600;

            Bitmap objBitmap = new Bitmap(with, height);
            Graphics objGraphics = Graphics.FromImage(objBitmap);

            Font objFont = new Font(fontName, fontSize);
            PointF objPoint = new PointF(5f, 5f);

            SolidBrush objBrushForeColor = new SolidBrush(fontColor);
            SolidBrush objBrushBackColor = new SolidBrush(backColor);

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


        private bool EscribirArchivoBinario()
        {
            //FileStream fs = null;
            //BinaryWriter bw = null;
            //try
            //{
            //    if (Comprimir == false)
            //    {
            //        fs = new FileStream(NombreArchivo, FileMode.Create);
            //        bw = new BinaryWriter(fs);
            //        bw.Write(Contenido);
            //        exito = true;
            //    }
            //    else
            //    {
            //        fs = new FileStream(NombreArchivo, FileMode.OpenOrCreate);
            //        //fs = new FileStream(NombreArchivo, FileMode.CreateNew);
            //        System.IO.Compresion.GZipStream cmp = new GZipStream(fs, CompressionMode.Compress);
            //        bw = new BinaryWriter(cmp);
            //        bw.Write(Contenido);
            //        exito = true;
            //    }


            //}
            //catch (IOException ioexep1)
            //{
            //    Nucleo.ManejarErrorIO(ioexep1);
            //    exito = false;
            //}
            //finally
            //{
            //    bw.Flush();
            //    bw.Close();
            //    fs.Close();
            //}
            //if (exito == true) return true;
            return false;

        }
        //private byte[] LeerArvhivoBinario()
        //{
        //    if (ExisteArchivo(NombreArchivo, true) == false) return new byte[0];

        //    bool exito = false;
        //    byte[] Contenido = null;

        //    if (Comprimido == true)
        //    {
        //        FileStream fs1 = null;
        //        try
        //        {
        //            fs1 = new FileStream(NombreArchivo, FileMode.Open);
        //            GZipStream dcmp = new GZipStream(fs1,
        //            CompressionMode.Decompress);
        //            Contenido = ReadFully(dcmp, 0);
        //            exito = true;

        //        }
        //        catch (IOException ioexep1)
        //        {
        //            Nucleo.ManejarErrorIO(ioexep1);
        //        }
        //        catch (Exception exep1)
        //        {
        //            Nucleo.ManejarErrorApp(exep1);
        //        }
        //    }
        //    else
        //    {
        //        FileStream fs1 = null;
        //        try
        //        {
        //            fs1 = new FileStream(NombreArchivo, FileMode.Open, FileAccess.Read);
        //            Contenido = ReadFully(fs1, 0);
        //            exito = true;
        //        }
        //        catch (IOException ioexep1)
        //        {
        //            Nucleo.ManejarErrorIO(ioexep1);
        //        }
        //        catch (Exception exep1)
        //        {
        //            Nucleo.ManejarErrorApp(exep1);
        //        }
        //        finally
        //        {
        //            if (fs1 != null) fs1.Close();
        //        }
        //    }

        //    if (exito == false) return new byte[0];
        //    return Contenido;
        //}
        //private void zipeando()
        //{
        //    string tipo = "";
        //    string filenameNew = filename;
        //    if (type == ExtensionFile.MI)
        //    {
        //        tipo = " - MI.zip";
        //        filenameNew += ".mi";
        //    }
        //    foreach (ZipFile zip in zipList)
        //    {
        //        if (zip.Name.Contains(tipo))
        //        {
        //            if (zip.ContainsEntry(filenameNew))
        //                zip.RemoveEntry(filenameNew);

        //            zip.AddEntry(filenameNew, contenido);
        //        }
        //    }
        //}
        //public static ZipFile zipPrepare(ExtensionFile type, string DestinationFolder, string zipName, int maxSegmentSizeMB)
        //{
        //    string zipPath = DestinationFolder + "\\" + zipName;
        //    string filenameWithExtension = "";

        //    nameExtension(type, ref zipPath, ref filenameWithExtension);

        //    ZipFile zip = new ZipFile();
        //    if (!System.IO.File.Exists(zipPath))
        //    {
        //        if (maxSegmentSizeMB != 0)
        //            zip.MaxOutputSegmentSize = maxSegmentSizeMB * 1000 * 1024;

        //        zip.Name = zipPath;
        //    }
        //    else
        //        zip = Ionic.Zip.ZipFile.Read(zipPath);

        //    return zip;
        //}
    }
}
