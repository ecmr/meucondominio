using ClosedXML.Excel;
using MeuCondominio.Bus;
using MeuCondominio.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MeuCondominio
{
    public partial class FrmGestaoSedex : Form
    {
        List<Morador> moradoresBlocos = new List<Morador>();
        private int IdMoradorSedex;
        private string sDataCadastro;
        private string sDataEntrega;
        private string sDataEnvioMensagem;

        private static string sLocalExcel = @"C:\Sedex Condominio\Excel\";

        public FrmGestaoSedex()
        {
            InitializeComponent();
            this.cboApto.GotFocus += CboApto_GotFocus;
            ckbSms.Checked = true;
            timer1.Enabled = false;

            cboBloco.Items.Add("01");
            cboBloco.Items.Add("02");
            cboBloco.Items.Add("03");
            cboBloco.Items.Add("04");
            cboBloco.Items.Add("05");
            cboBloco.Items.Add("06");
            cboBloco.Items.Add("07");
            cboBloco.Items.Add("08");
            cboBloco.Items.Add("09");
            cboBloco.Items.Add("10");
            cboBloco.Items.Add("11");
            cboBloco.Items.Add("12");
            cboBloco.Items.Add("13");


            cboApto.Items.Add("11");
            cboApto.Items.Add("12");
            cboApto.Items.Add("13");
            cboApto.Items.Add("14");
            cboApto.Items.Add("21");
            cboApto.Items.Add("22");
            cboApto.Items.Add("23");
            cboApto.Items.Add("24");
            cboApto.Items.Add("31");
            cboApto.Items.Add("32");
            cboApto.Items.Add("33");
            cboApto.Items.Add("34");
            cboApto.Items.Add("41");
            cboApto.Items.Add("42");
            cboApto.Items.Add("43");
            cboApto.Items.Add("44");
            cboApto.Items.Add("51");
            cboApto.Items.Add("52");
            cboApto.Items.Add("53");
            cboApto.Items.Add("54");
            cboApto.Items.Add("61");
            cboApto.Items.Add("62");
            cboApto.Items.Add("63");
            cboApto.Items.Add("64");
            cboApto.Items.Add("71");
            cboApto.Items.Add("72");
            cboApto.Items.Add("73");
            cboApto.Items.Add("74");
            cboApto.Items.Add("81");
            cboApto.Items.Add("82");
            cboApto.Items.Add("83");
            cboApto.Items.Add("84");
            cboApto.Items.Add("91");
            cboApto.Items.Add("92");
            cboApto.Items.Add("93");
            cboApto.Items.Add("94");
            cboApto.Items.Add("101");
            cboApto.Items.Add("102");
            cboApto.Items.Add("103");
            cboApto.Items.Add("104");

            // CarregarBlocos();
        }

        #region Eventos
        private void cnsultarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar();
        }
        private void CarregarBlocos()
        {
            lblMsgMorador.Text = "Carregando arquivo...";
            lblMsgMorador.Visible = true;
            CarregaBloco01();
            CarregaBloco02();
            CarregaBloco03();
            CarregaBloco04();
            CarregaBloco05();
            CarregaBloco06();
            CarregaBloco07();
            CarregaBloco08();
            CarregaBloco09();
            CarregaBloco10();
            CarregaBloco11();
            CarregaBloco12();
            CarregaBloco13();
            lblMsgMorador.Text = "Carregamento completo!";
            timer1.Enabled = true;
        }
        private void carregarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregarBlocos();
            MeuCondominio.Model.HelperModel.GravaLog("Carregar excel");
        }
        private void CarregaBloco01()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(2); //Bloco01

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "01";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (bus.Adicionar(morador))
                            continue;
                        HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }
        }
        private void CarregaBloco02()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(3); //Bloco02

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "02";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }
        }
        private void CarregaBloco03()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(4); //Bloco03

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "03";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }
        }
        private void CarregaBloco04()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(5); //Bloco01

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "04";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }
        }
        private void CarregaBloco05()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(6); //Bloco01

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "05";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);

                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }
        }
        private void CarregaBloco06()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(7); //Bloco06

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "06";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void CarregaBloco07()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(8); //Bloco08

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "07";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void CarregaBloco08()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(9); //Bloco08

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "08";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void CarregaBloco09()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(10); //Bloco09

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "09";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);

                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void CarregaBloco10()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(11); //Bloco10

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "10";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);

                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void CarregaBloco11()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(12); //Bloco11

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "11";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);

                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void CarregaBloco12()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(13); //Bloco12

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "12";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void CarregaBloco13()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(14); //Bloco13

            try
            {
                for (int i = 2; i < 180; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        var cApto = row.Cell(2); // APTO
                        object valueApto = cApto.Value;

                        var cNomeMorador = row.Cell(3); // Morador
                        object valueNome = cNomeMorador.Value;

                        var cCelularMorador = row.Cell(4); // Celular
                        object valueCelular = SomenteNumeros(cCelularMorador.Value.ToString());

                        var cEmailMorador = row.Cell(6); // email
                        object valueEmail = cEmailMorador.Value;

                        morador.Bloco = "13";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeDestinatario = valueNome.ToString();
                        morador.NumeroCelular = valueCelular.ToString();
                        morador.email = valueEmail.ToString();

                        SedexBus bus = new SedexBus();
                        if (!bus.Adicionar(morador))
                            HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);

                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void cboApto_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewMoradoresApto.Items.Clear();

            for (int i = 0; i < moradoresBlocos.Count; i++)
            {
                int bloco = int.Parse(moradoresBlocos[i].Bloco);
                int cBloco = int.Parse(cboBloco.Text);
                int apto = int.Parse(moradoresBlocos[i].Apartamento);
                int cApto = int.Parse(cboApto.Text);

                if (bloco.Equals(cBloco) && apto.Equals(cApto))
                {
                    listViewMoradoresApto.Items.Add(moradoresBlocos[i].NomeDestinatario);
                }
            }
        }
        private void cboBlocoKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (char.IsDigit(e.KeyChar) || (!string.IsNullOrEmpty(e.KeyChar.ToString())))
                {
                    if (string.Concat(cboBloco.Text, char.GetNumericValue(e.KeyChar)).Length < 2)
                        return;
                }

                int sBloco = int.Parse(e.KeyChar.ToString());

                if (((sBloco >= 1) && (sBloco <= 13)) || e.KeyChar == 13)
                {
                    //LimparTela();
                    cboBloco.Text += sBloco; //sBloco.ToString().PadLeft(2, '0');
                    cboApto.Focus();
                }
                else
                    return;

                cboBloco.Select(cboBloco.Text.Length, 0);
            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }

        }
        private void CboApto_GotFocus(Object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(cboBloco.Text) && (cboBloco.Text.Length > 2)))
            {
                cboBloco.Text = cboBloco.Text.Remove(0, 1);
            }
        }
        private void CarregaNomes(object sender, KeyPressEventArgs e)
        {
            try
            {
                lstHistorico.Items.Clear();

                if (string.IsNullOrEmpty(e.KeyChar.ToString()) || (string.IsNullOrEmpty(cboApto.Text)))
                    return;

                string sApto = cboApto.Text;

                if ((int.Parse(sApto) > 10) && (int.Parse(sApto) <= 104))
                {
                    listViewMoradoresApto.Items.Clear();

                    string sBloco = cboBloco.Text;

                    SedexBus bus = new SedexBus();
                    var MoradoreList = bus.Consultar(sBloco, sApto);

                    foreach (Morador morador in MoradoreList)
                    {
                        listViewMoradoresApto.Items.Add(morador.NomeDestinatario);
                    }
                    CarregaHistorico(cboBloco.Text, cboApto.Text);
                }
            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }
        }
        private void listViewMoradoresApto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (listViewMoradoresApto.SelectedItems.Count < 1)
                return;

            string nomeMorador = listViewMoradoresApto.SelectedItems[0].Text;
            string bloco = cboBloco.Text;
            string apto = cboApto.Text.Substring(0, 1) == "0" ? cboApto.Text.Substring(1, 2) : cboApto.Text;

            SedexBus bus = new SedexBus();
            List<Morador> queryMoradores = bus.Consultar(bloco, apto, nomeMorador);

            foreach (Morador morador in queryMoradores)
            {
                txtNomeMoraador.Text = morador.NomeDestinatario;
                txtCelular.Text = morador.NumeroCelular;
                txtEmail.Text = morador.email;
                IdMoradorSedex = morador.IdMorador;
            }
            if (IdMoradorSedex > 0)
                btnExcluir.Enabled = true;
            else
                btnExcluir.Enabled = false;

            Cursor.Current = Cursors.Default;
        }

        private void CarregaHistorico(string Bloco, string Apartamento)
        {
            lstHistorico.Items.Clear();
            SedexBus sedexBus = new SedexBus();
            List<Morador> moradores = sedexBus.GetHistoricoPorApartamento(Bloco, Apartamento);

            foreach (Morador morador in moradores)
            {
                ListViewItem item = new ListViewItem(morador.Bloco);
                item.SubItems.Add(morador.Apartamento);
                item.SubItems.Add(morador.NomeDestinatario);
                item.SubItems.Add(morador.NumeroCelular);
                item.SubItems.Add(morador.DataEnvioMensagem);
                item.SubItems.Add(morador.DataEntrega);
                lstHistorico.Items.Add(item);
            }
        }

        private void AtualizaMorador()
        {
            Morador morador = new Morador();
            morador.IdMorador = IdMoradorSedex;
            morador.NomeDestinatario = txtNomeMoraador.Text;
            morador.Bloco = cboBloco.Text;
            morador.Apartamento = cboApto.Text;
            txtCelular.Mask = "";
            morador.NumeroCelular = SomenteNumeros(txtCelular.Text);
            txtCelular.Mask = "(99) 00000-0000";
            morador.email = txtEmail.Text;
            morador.ReciboImpresso = "N";

            SedexBus bus = new SedexBus();

            bool sucesso = false;

            if (IdMoradorSedex < 1)
                sucesso = bus.Adicionar(morador);
            else
            sucesso = bus.Atualizar(morador);
            
            if (sucesso)
            {
                lblMsgMorador.Text = "Registro atualizado com sucesso!";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
            }
            else
            {
                lblMsgMorador.Text = "Falha ao atualizar o registro";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
            }
        }
        private bool ValidarCamposPreGravar()
        {
            if ((string.IsNullOrEmpty(txtCodBarras.Text) ||
                (string.IsNullOrEmpty(txtQrcode.Text)) ||
                (string.IsNullOrEmpty(txtEtiquetaLocal.Text))) &&
                (string.IsNullOrEmpty(cboBloco.Text)) &&
                (string.IsNullOrEmpty(cboApto.Text)) &&
                (string.IsNullOrEmpty(txtNomeMoraador.Text)))
                return false;
            if (!CelularValido())
                return false;

            return true;
        }
        private void LimparMensagem(object sender, EventArgs e)
        {
            LimparTela();
            timer1.Enabled = false;
        }
        private void CarregarTelaPosConsulta(Morador morador)
        {
            sDataCadastro = morador.DataCadastro;
            sDataEntrega = morador.DataEntrega;
            sDataEnvioMensagem = morador.DataEnvioMensagem;
            IdMoradorSedex = morador.IdMorador;
            cboBloco.Text = morador.Bloco;
            cboApto.Text = morador.Apartamento;
            txtCelular.Text = morador.NumeroCelular;
            txtCodBarras.Text = morador.CodigoBarraEtiqueta;
            txtEmail.Text = morador.email;
            txtEtiquetaLocal.Text = morador.CodigoBarraEtiquetaLocal;
            txtNomeMoraador.Text = morador.NomeDestinatario;
            txtPrateleira.Text = morador.LocalPrateleira.ToString();
            txtQrcode.Text = morador.CodigoQRCode;

            if (!string.IsNullOrEmpty(morador.DataEntrega))
            {
                lblMsgMorador.Text = string.Concat("Entregue em: ", morador.DataEntrega, Environment.NewLine, "Enviado por: ");
                if (morador.EnviadoPorSMS == "S")
                    lblMsgMorador.Text += " -SMS- ";
                if (morador.EnviadoPorZAP == "S")
                    lblMsgMorador.Text += " -WhatsApp- ";
                if (morador.EnviadoPorTELEGRAM == "S")
                    lblMsgMorador.Text += " -Telegram- ";
                if (morador.EnviadoPorEMAIL == "S")
                    lblMsgMorador.Text += " -E-Mail- ";
            }
            if (!string.IsNullOrEmpty(morador.DataEnvioMensagem))
            {
                lblMsgMorador.Text = string.Concat("Enviado em: ", morador.DataEnvioMensagem, Environment.NewLine, "Enviado por: ");
                if (morador.EnviadoPorSMS == "S")
                    lblMsgMorador.Text += " -SMS- ";
                if (morador.EnviadoPorZAP == "S")
                    lblMsgMorador.Text += " -WhatsApp- ";
                if (morador.EnviadoPorTELEGRAM == "S")
                    lblMsgMorador.Text += " -Telegram- ";
                if (morador.EnviadoPorEMAIL == "S")
                    lblMsgMorador.Text += " -E-Mail- ";
            }
        }
        private void LimparTela()
        {
            lblMsgMorador.Text = "";
            IdMoradorSedex = 0;
            cboBloco.Text = string.Empty;
            cboApto.Text = string.Empty;
            listViewMoradoresApto.Items.Clear();
            txtCelular.Text = string.Empty;
            txtCodBarras.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtEtiquetaLocal.Text = string.Empty;
            txtNomeMoraador.Text = string.Empty;
            txtPrateleira.Text = string.Empty;
            txtQrcode.Text = string.Empty;
            ckbEntregue.Checked = false;
        }
        private void VerificarBanco(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            Morador morador = ConsultarBase(txtCodBarras.Text);
            if (morador.IdMorador > 0)
            {
                CarregarTelaPosConsulta(morador);
            }
        }
        private void ImprimirComprovanteEntrega()
        {
            //TODO: Imprimir comprovante em impressora termica
        }
        #endregion

        #region CRUD
        private void Salvar()
        {
            Morador morador = new Morador();
            morador.NomeDestinatario = txtNomeMoraador.Text;
            morador.Bloco = cboBloco.Text;
            morador.Apartamento = cboApto.Text;
            txtCelular.Mask = "";
            morador.NumeroCelular = SomenteNumeros(txtCelular.Text);
            txtCelular.Mask = "(99) 00000-0000";
            morador.email = txtEmail.Text;
            morador.CodigoBarraEtiqueta = txtCodBarras.Text;
            morador.CodigoBarraEtiquetaLocal = txtEtiquetaLocal.Text;
            int iPrateleira = string.IsNullOrEmpty(txtPrateleira.Text) ? 0 : int.Parse(txtPrateleira.Text);
            morador.LocalPrateleira = iPrateleira;
            morador.DataCadastro = string.Concat(DateTime.Now.Day.ToString(), "/", DateTime.Now.Month.ToString(), "/", DateTime.Now.Year.ToString(), " ", DateTime.Now.Hour.ToString(), ":", DateTime.Now.Minute.ToString());
            morador.ReciboImpresso = "N";

            SedexBus bus = new SedexBus();

            bool sucesso = false;

            morador.IdMorador = IdMoradorSedex;
            sucesso = bus.AtualizarTelefone(morador);
            morador.IdMorador = 0;
            sucesso = bus.Adicionar(morador);

            if (!sucesso)
            {
                lblMsgMorador.Text = "Erro ao salvar registro!";
            }
            else
            {
                lblMsgMorador.Text = "Registro salvo com sucesso!";
            }
            lblMsgMorador.Visible = true;

        }
        private void RegistrarEntrega()
        {
            Morador morador = new Morador();
            morador.IdMorador = IdMoradorSedex;
            morador.NomeDestinatario = txtNomeMoraador.Text;
            morador.Bloco = cboBloco.Text;
            morador.Apartamento = cboApto.Text;
            morador.NumeroCelular = SomenteNumeros(txtCelular.Text);
            morador.email = txtEmail.Text;
            morador.CodigoBarraEtiqueta = txtCodBarras.Text;
            morador.CodigoBarraEtiquetaLocal = txtEtiquetaLocal.Text;
            int iPrateleira = string.IsNullOrEmpty(txtPrateleira.Text) ? 0 : int.Parse(txtPrateleira.Text);
            morador.LocalPrateleira = iPrateleira;
            morador.DataCadastro = sDataCadastro;
            morador.DataEntrega = string.Concat(DateTime.Now.Day.ToString(), "/", DateTime.Now.Month.ToString(), "/", DateTime.Now.Year.ToString(), " ", DateTime.Now.Hour.ToString(), ":", DateTime.Now.Minute.ToString());
            morador.DataEnvioMensagem = sDataEnvioMensagem;
            morador.EnviadoPorSMS = ckbSms.Checked == true ? "S" : "N";
            morador.EnviadoPorZAP = ckbZap.Checked == true ? "S" : "N";
            morador.EnviadoPorTELEGRAM = "N";
            morador.EnviadoPorEMAIL = ckbMail.Checked == true ? "S" : "N";

            SedexBus bus = new SedexBus();
            if (bus.Atualizar(morador))
            {
                lblMsgMorador.Text = "Sedex entregue!";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
            }
        }
        private Morador ConsultarBase(string CodigoBarras)
        {
            SedexBus bus = new SedexBus();
            return bus.Consultar(CodigoBarras);
            // List<Morador> queryMoradores = moradoresBlocos.Where(ap => ap.Apartamento.Equals(apto)).ToList();
        }
        #endregion

        private void ValidarCelular(object sender, EventArgs e)
        {
            if (CelularValido())
            {
                lblMsgMorador.Text = "";
                lblMsgMorador.Visible = false;
            }
        }

        private bool CelularValido()
        {
            if (txtCelular.Text.Length < 14)
            {
                lblMsgMorador.Text = "Telefone incompleto!";
                lblMsgMorador.Visible = true;
                //txtCelular.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void txtCodBarras_TextChanged(object sender, EventArgs e)
        {

        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reciboDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Morador> ListaRecibo = new List<Morador>();
            SedexBus bus = new SedexBus();
            ListaRecibo = bus.RetornaListaParaRecibo();

            FrmImpressao frmImpressao = new FrmImpressao(ListaRecibo);
            frmImpressao.StartPosition = FormStartPosition.CenterScreen;
            frmImpressao.ShowDialog();
        }

        private void FrmGestaoSedex_Load(object sender, EventArgs e)
        {

        }

        private void cboBloco_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public string SomenteNumeros(string texto)
        {
            Regex re = new Regex("[0-9]");
            StringBuilder s = new StringBuilder();

            foreach (Match m in re.Matches(texto))
            {
                s.Append(m.Value);
            }
            return s.ToString();
        }

        private void btnEnviarSms_Click(object sender, EventArgs e)
        {
            ///TODO: LISTAR TUDO COM DATAEMVIADO NULL
            SedexBus sedexBus = new SedexBus();
            List<Morador> listSms = sedexBus.RetornaListaParaEnvioSms();

            foreach ( Morador morador in listSms)
            {
                morador.DataEnvioMensagem = string.Concat(DateTime.Now.Day.ToString(), "/", DateTime.Now.Month.ToString(), "/", DateTime.Now.Year.ToString(), " ", DateTime.Now.Hour.ToString(), ":", DateTime.Now.Minute.ToString());
                morador.EnviadoPorSMS = "S";

                if (EnvioMensagem.EnvioSmsDev(morador))
                {
                    if (sedexBus.Atualizar(morador))
                    {
                        lblMsgMorador.Visible = true;
                        lblMsgMorador.Text = $"enviado para {morador.NomeDestinatario} com sucesso!";
                        lblMsgMorador.Refresh();
                    }
                }
            }
            lblMsgMorador.Text = $"Enviado para {listSms.Count} moradores com sucesso!";
            timer1.Enabled = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposPreGravar())
            {
                lblMsgMorador.Text = "Selecione um morador e coloque um codigo de barras!";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
                return;
            }

            if (string.IsNullOrEmpty(txtCodBarras.Text) && (!ckbEntregue.Checked))
            {
                DialogResult result = MessageBox.Show("Não tem encomenda para registrar, deseja apenas atualiar os dados do morador?", "ATENÇÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    AtualizaMorador();
            }
            else if (ckbEntregue.Checked)
            {
                RegistrarEntrega();
            }
            else
            {
                Salvar();
                timer1.Enabled = true;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (IdMoradorSedex < 1)
            {
                lblMsgMorador.Text = "Selecione um morador para exclui-lo!";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
                return;
            }

            SedexBus sedexBus = new SedexBus();

            Morador morador = sedexBus.Consultar(IdMoradorSedex);

            DialogResult result;

            if (morador.IdMorador < 1)
            {
                result = MessageBox.Show("Deseja realmente excluir o registro selecionado?", "ATENÇÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                result = MessageBox.Show($"Deseja realmente excluir {morador.NomeDestinatario} ?", "ATENÇÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (result == DialogResult.No)
                return;

            if (sedexBus.Excluir(IdMoradorSedex))
            {
                lblMsgMorador.Text = "Morador excluído com sucesso!";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
                btnExcluir.Enabled = false;
                cboBloco.Focus();
                return;
            }
            else
            {
                lblMsgMorador.Text = "Falha ao exluir o morador, avise o administrador do sistema!";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
                btnExcluir.Enabled = false;
                cboBloco.Focus();
                return;
            }
            
        }
    }
}