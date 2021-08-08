using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MeuCondominio.Model;


namespace MeuCondominio
{
    public partial class FrmImpressao : Form
    {
        List<Morador> listPrint;

        public FrmImpressao(List<Morador> moradores)
        {
            listPrint = moradores;
            InitializeComponent();
        }

        private void FrmImpressao_Load(object sender, EventArgs e)
        {
            foreach (var printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cboImpressora.Items.Add(printer);
            }
            this.reportViewer1.RefreshReport();
        }

        void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringBuilder textoToPrint = new StringBuilder();

            ///Paginação
            ///


            for (int i = 0; i < listPrint.Count; i++)
            {
                #region [Row 1]
                if (listPrint.Count > 1)
                {
                    textoToPrint.Append(string.Concat(("Bloco: " + listPrint[i].Bloco + " Apartamento: " + listPrint[i].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[i * 3].Bloco + " Apartamento: " + listPrint[i * 3].Apartamento, Environment.NewLine));
                    textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[i].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[i * 3].CodigoBarraEtiqueta, Environment.NewLine));
                    textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(69, ' '), "Nome: _________________________________", Environment.NewLine));
                    textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
                    textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
                    textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
                }

            }

            #region teste
            //else if (listPrint.Count == 1)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[i].Bloco + " Apartamento: " + listPrint[i].Apartamento).PadRight(74, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[i].CodigoBarraEtiqueta).PadRight(69, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(69, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' ')));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    #endregion
            //    #region [Row 2]
            //    if (listPrint.Count >= 4)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[i+2].Bloco + " Apartamento: " + listPrint[i + 2].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[i + 3].Bloco + " Apartamento: " + listPrint[i + 3].Apartamento, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[i + 2].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[i + 3].CodigoBarraEtiqueta, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(65, ' '), "Nome: _________________________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    else if (listPrint.Count == 3)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[i + 2].Bloco + " Apartamento: " + listPrint[i + 2].Apartamento).PadRight(74, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[i + 2].CodigoBarraEtiqueta).PadRight(69, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' ')));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }

            //    #endregion
            //    #region [Row 3]
            //    if (listPrint.Count >= 6)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[i + 4].Bloco + " Apartamento: " + listPrint[i + 4].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[i + 5].Bloco + " Apartamento: " + listPrint[i + 5].Apartamento, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[i + 4].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[i + 5].CodigoBarraEtiqueta, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), "Nome: _________________________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    #endregion
            //    #region [Row 4]
            //    if (listPrint.Count >= 8)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[i + 6].Bloco + " Apartamento: " + listPrint[i + 6].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[i + 7].Bloco + " Apartamento: " + listPrint[i + 7].Apartamento, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[i + 6].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[i + 7].CodigoBarraEtiqueta, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), "Nome: _________________________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    #endregion
            //    #region [Row 5]
            //    if (listPrint.Count >= 10)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[i + 8].Bloco + " Apartamento: " + listPrint[i + 8].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[i + 9].Bloco + " Apartamento: " + listPrint[i + 9].Apartamento, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[i + 8].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[i + 9].CodigoBarraEtiqueta, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), "Nome: _________________________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    #endregion
            //    #region [Row 6]
            //    if (listPrint.Count >= 12)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[10].Bloco + " Apartamento: " + listPrint[10].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[11].Bloco + " Apartamento: " + listPrint[11].Apartamento, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[10].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[11].CodigoBarraEtiqueta, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), "Nome: " + listPrint[11].NomeDestinatario, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    else if (listPrint.Count == 11)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[10].Bloco + " Apartamento: " + listPrint[10].Apartamento).PadRight(74, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[10].CodigoBarraEtiqueta).PadRight(69, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' ')));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    #endregion
            //    #region [Row 7]
            //    if (listPrint.Count >= 14)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[12].Bloco + " Apartamento: " + listPrint[12].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[13].Bloco + " Apartamento: " + listPrint[13].Apartamento, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[12].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[13].CodigoBarraEtiqueta, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), "Nome: " + listPrint[13].NomeDestinatario, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    else if (listPrint.Count == 13)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[12].Bloco + " Apartamento: " + listPrint[12].Apartamento).PadRight(74, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[12].CodigoBarraEtiqueta).PadRight(69, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' ')));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    #endregion
            //    #region [Row 8]
            //    if (listPrint.Count >= 16)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[14].Bloco + " Apartamento: " + listPrint[14].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[15].Bloco + " Apartamento: " + listPrint[15].Apartamento, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[14].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[15].CodigoBarraEtiqueta, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), "Nome: " + listPrint[15].NomeDestinatario, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    else if (listPrint.Count == 15)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[14].Bloco + " Apartamento: " + listPrint[14].Apartamento).PadRight(74, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[14].CodigoBarraEtiqueta).PadRight(69, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' ')));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    #endregion
            //    #region [Row 9]
            //    if (listPrint.Count >= 18)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[16].Bloco + " Apartamento: " + listPrint[16].Apartamento).PadRight(74, ' '), "Bloco: " + listPrint[17].Bloco + " Apartamento: " + listPrint[17].Apartamento, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[16].CodigoBarraEtiqueta).PadRight(69, ' '), "Encomenda: " + listPrint[17].CodigoBarraEtiqueta, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), "Nome: " + listPrint[17].NomeDestinatario, Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), "Recebido em: _________________", Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' '), "Ass: ____________________________"));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            //    else if (listPrint.Count == 17)
            //    {
            //        textoToPrint.Append(string.Concat(("Bloco: " + listPrint[16].Bloco + " Apartamento: " + listPrint[16].Apartamento).PadRight(74, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Encomenda: " + listPrint[16].CodigoBarraEtiqueta).PadRight(69, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Nome: _________________________________").PadRight(68, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Recebido em: _________________ ").PadRight(65, ' '), Environment.NewLine));
            //        textoToPrint.Append(string.Concat(("Ass: ____________________________").PadRight(61, ' ')));
            //        textoToPrint.Append(string.Concat(Environment.NewLine, ("").PadRight(110, '-'), Environment.NewLine));
            //    }
            #endregion

            #endregion
            //}

            var printDocument = sender as System.Drawing.Printing.PrintDocument;


            if (printDocument != null)
            {
                using (var font = new Font("Times New Roman", 14))
                using (var brush = new SolidBrush(Color.Black))
                {
                    e.Graphics.DrawString(
                        textoToPrint.ToString(),
                        font,
                        brush,
                        new RectangleF(0, 0, printDocument.DefaultPageSettings.PrintableArea.Width, printDocument.DefaultPageSettings.PrintableArea.Height));
                }
            }
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            using (var printDocument = new System.Drawing.Printing.PrintDocument())
            {

                printDocument.PrintPage += printDocument_PrintPage;
                printDocument.PrinterSettings.PrinterName = cboImpressora.SelectedItem.ToString();
                printDocument.Print();
            }
        }

    }
}
