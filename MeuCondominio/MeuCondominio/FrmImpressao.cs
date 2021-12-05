using MeuCondominio.Bus;
using MeuCondominio.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace MeuCondominio
{
    public partial class FrmImpressao : Form
    {
        List<Morador> listPrint;
        PrintDocument print;
        String[] textoToPrint;

        public FrmImpressao()
        {
            InitializeComponent();
        }

        private void FrmImpressao_Load(object sender, EventArgs e)
        {

            chkVisualizaImpressao.Checked = true;
            chkVisualizaImpressao.Visible = false;
            foreach (var printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cboImpressora.Items.Add(printer);
            }
            this.Visible = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            List<Morador> ListaRecibo = new List<Morador>();
            SedexBus bus = new SedexBus();

            if (ckBPeriodo.Checked)
            {
                string inicio = string.Concat(string.Concat(dtpDataInicial.Text.Substring(6, 4)), '-', string.Concat(dtpDataInicial.Text.Substring(3, 2)), '-', string.Concat(dtpDataInicial.Text.Substring(0, 2)), " ", dtpHoraInicial.Text);
                string fim = string.Concat(string.Concat(dtpDataFinal.Text.Substring(6, 4)), '-', string.Concat(dtpDataFinal.Text.Substring(3, 2)), '-', string.Concat(dtpDataFinal.Text.Substring(0, 2)), " ", dtpHoraFinal.Text);
                ListaRecibo = bus.RetornaListaParaRecibo(inicio, fim);
            }
            else
            {
                ListaRecibo = bus.RetornaListaParaRecibo();
            }


            if (ListaRecibo.Count < 1)
            {
                lblMsgMorador.Visible = true;
                lblMsgMorador.Text = "Sem recibos para impressão selecionada!";
                return;
            }

            textoToPrint = new string[ListaRecibo.Count * 3];
            print = new ImprimirDocumento(textoToPrint);
            print.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_EndPrint);

            int x = 0;

            foreach (Morador moradorPrint in ListaRecibo)
            {
                textoToPrint[x] += ("BL:" + moradorPrint.Bloco.Remove(0,5).PadLeft(2, '0') + " Apto: " + moradorPrint.Apartamento + " Cod: " + moradorPrint.SobreNomeMorador.PadRight(13, ' ')).PadRight(37, '_');
                textoToPrint[x] += (" Nome:_____________ Data:__/__/____ Ass:_________________");
                x++;
                textoToPrint[x] += (" ").PadRight(150, ' ');
                x++;
            }

            print.PrintPage += this.Doc_PrintPage;
            
            PrintDialog dialogo = new PrintDialog();
            dialogo.Document = print;

            //  Se o usuário clicar em OK , imprime o documento
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                print.DocumentName = "Assinaturas";
                print.Print();
            }
            this.Visible = false;

            if (!bus.AlteraStatusRecibo())
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, string.Concat("void imprimirToolStripMenuItem_Click(): ", "Erro ao alterar status de Recibo impresso"));
            }

        }

        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            {
               //Bus.SedexBus bus = new Bus.SedexBus();

                //foreach (Morador morador in listPrint)
                //{
                //    // morador.ReciboImpresso = "S";
                //    //TODO: Fata criar os metodos para atualizar quando for impresso o recibo
                //    ///UPDATE SedexHistorico SET ReciboImpresso = 'S' WHERE IdSedex = 0;
                //    //bus.AtualizarRecibo(morador);
                //}
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Recupera o documento que enviou este evento.
            ImprimirDocumento doc = (ImprimirDocumento)sender;

            // Define a fonte e determina a altura da linha
            using (Font fonte = new Font("Verdana", 10))
            {
                float alturaLinha = fonte.GetHeight(e.Graphics);

                // Cria as variáveis para tratar a posição na página
                float x = 10; // e.MarginBounds.Left;
                float y = 30; // e.MarginBounds.Top;

                // Incrementa o contador de página para refletir
                // a página que esta sendo impressa
                doc.NumeroPagina += 1;

                // Imprime toda a informação que cabe na página
                // O laço termina quando a próxima linha
                // iria passar a borda da margem ou quando não
                // houve mais linhas a imprimir
                while ((y + alturaLinha) < e.MarginBounds.Bottom &&
                  doc.Offset <= doc.Texto.GetUpperBound(0))
                {
                    e.Graphics.DrawString(doc.Texto[doc.Offset], fonte,
                      Brushes.Black, x, y);

                    // move para a proxima linha
                    doc.Offset += 1;

                    // Move uma linha para baixo (proxima linha)
                    y += alturaLinha;
                }

                if (doc.Offset < doc.Texto.GetUpperBound(0))
                {
                    // Havendo ainda pelo menos mais uma página.
                    // Sinaliza o evento para disparar novamente
                    e.HasMorePages = true;
                }
                else
                {
                    // A impressão terminou
                    doc.Offset = 0;
                }
            }
        }


        public class ImprimirDocumento : PrintDocument
        {
            private string[] texto;
            private int numeroPagina;
            private int offset;

            public string[] Texto
            {
                get { return texto; }
                set { texto = value; }
            }

            public int NumeroPagina
            {
                get { return numeroPagina; }
                set { numeroPagina = value; }
            }

            public int Offset
            {
                get { return offset; }
                set { offset = value; }
            }

            public ImprimirDocumento(string[] _texto)
            {
                this.Texto = _texto;
            }
        }

        private void FrmImpressao_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}