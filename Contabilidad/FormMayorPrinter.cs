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
    public partial class FormMayor : BasicForm
    {
        DataGridViewPrinter MyDataGridViewPrinter;

        public void PrintExternal()
        {
            VistaPrevia();
        }

        // The printing setup function
        private bool SetupThePrinting(DataGridView dtGrid, string name)
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            //if (MyPrintDialog.ShowDialog() != DialogResult.OK)
            //    return false;

            printDocument1.DocumentName = name + " Report";
            printDocument1.PrinterSettings =
                                MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings =
            MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Margins =
                             new Margins(40, 40, 40, 40);

            if (MessageBox.Show("¿Desea centrar el reporte en la pagina?",
                "Centrado", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                MyDataGridViewPrinter = new DataGridViewPrinter(dtGrid,
                printDocument1, true, true, name, new Font("Tahoma", 16,
                FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            else
                MyDataGridViewPrinter = new DataGridViewPrinter(dtGrid,
                printDocument1, false, true, name, new Font("Tahoma", 16,
                FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting(this.dtgrdMayor, description))
                printDocument1.Print();
        }

        private void btnVistaPreliminar_Click(object sender, EventArgs e)
        {
            VistaPrevia();
        }

        private void VistaPrevia()
        {
            if (SetupThePrinting(this.dtgrdMayor, description))
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = printDocument1;
                MyPrintPreviewDialog.ShowDialog();
            }

            //BrightIdeasSoftware.ListViewPrinter printer = new BrightIdeasSoftware.ListViewPrinter();
            //printer.Header = labelDescription.Text;
            //printer.HeaderFormat.Font = labelDescription.Font;
            //printer.ListView = dtgrdMayor;


            //printer.PrintPreview();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }


    }
}
