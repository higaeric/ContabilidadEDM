using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Contabilidad
{
    public partial class FormAsientos : BasicForm
    {
        Font printFont;
        Bitmap bitmap;

        private void VistaPrevia()
        {
            //System.Drawing.Printing.print
            //PrintAString(bitmap);
            BrightIdeasSoftware.ListViewPrinter printer = new BrightIdeasSoftware.ListViewPrinter();
            printer.Header = lbCliente.Text + "\r\n" + txtFechaInicial.Text + " a " + txtFechaFinal.Text;
            printer.HeaderFormat.Font = lbCliente.Font;
            printer.ListView = lvAsientos;
            printer.PrintPreview();
        }
        public void PrintExternal()
        {
            this.VistaPrevia();
        }

        private void Print()
        {
            BrightIdeasSoftware.ListViewPrinter printer = new BrightIdeasSoftware.ListViewPrinter();
            printer.Header = lbCliente.Text + "\r\n" + txtFechaInicial.Text + " a " + txtFechaFinal.Text;
            printer.HeaderFormat.Font = lbCliente.Font;
            printer.ListView = lvAsientos;
            printer.Print();
        }

        public void PrintAString(Bitmap data)
        {
            PrintDocument pd = new PrintDocument();
            printFont = lvAsientos.Font; //new Font("Arial", 12);
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            pd.DefaultPageSettings.Landscape = true;
            pd.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            bitmap = new Bitmap(this.lvAsientos.Width, this.lvAsientos.Height);
            lvAsientos.DrawToBitmap(bitmap, this.lvAsientos.ClientRectangle);
            e.Graphics.DrawImage(bitmap, new Point(70, 70));
        }

    }
}
