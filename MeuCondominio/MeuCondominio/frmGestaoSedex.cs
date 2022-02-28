using ClosedXML.Excel;
using MeuCondominio.Bus;
using MeuCondominio.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TeleSharp.TL;
using TL;
using TLSharp.Core;
using TLSharp.Core.Exceptions;

namespace MeuCondominio
{
    public partial class FrmGestaoSedex : Form
    {
        List<Morador> moradoresBlocos = new List<Morador>();
        private int IdMoradorSedex;
        private int idApartamentomorador;
        private string sDataCadastro;
        private string sDataEntrega;
        private string sDataEnvioMensagem;
        private string sMensagemParaSms;
        private string sMensagemParaEmail;
        private string hash;
        private string code;

        #region WTelegramClient
        private readonly ManualResetEventSlim _codeReady = new ManualResetEventSlim();
        private WTelegram.Client _client;
        private User _user;
        private string _userId;
        #endregion


        #region testeTelegram
        private string NumberToSendMessage { get; set; }
        private string NumberToAuthenticate { get; set; }
        private string CodeToAuthenticate { get; set; }
        private string PasswordToAuthenticate { get; set; }
        private string NotRegisteredNumberToSignUp { get; set; }
        private string UserNameToSendMessage { get; set; }
        private string NumberToGetUserFull { get; set; }
        private string NumberToAddToChat { get; set; }
        private string ApiHash { get; set; }
        private int ApiId { get; set; }
        class Assert
        {
            static internal void IsNotNull(object obj)
            {
                IsNotNullHanlder(obj);
            }

            static internal void IsTrue(bool cond)
            {
                IsTrueHandler(cond);
            }
        }

        internal static Action<object> IsNotNullHanlder;
        internal static Action<bool> IsTrueHandler;

        protected void Init(Action<object> notNullHandler, Action<bool> trueHandler)
        {
            IsNotNullHanlder = notNullHandler;
            IsTrueHandler = trueHandler;

            // Setup your API settings and phone numbers in app.config
            GatherTestConfiguration();
        }

        private TelegramClient NewClient()
        {
            try
            {
                return new TelegramClient(ApiId, ApiHash);
            }
            catch (MissingApiConfigurationException ex)
            {
                throw new Exception($"Please add your API settings to the `app.config` file. (More info: {MissingApiConfigurationException.InfoUrl})",
                                    ex);
            }
        }

        private void GatherTestConfiguration()
        {
            string appConfigMsgWarning = "{0} not configured in app.config! Some tests may fail.";

            ApiHash = ConfigurationManager.AppSettings[nameof(ApiHash)];
            if (string.IsNullOrEmpty(ApiHash))
                Debug.WriteLine(appConfigMsgWarning, nameof(ApiHash));

            var apiId = ConfigurationManager.AppSettings[nameof(ApiId)];
            if (string.IsNullOrEmpty(apiId))
                Debug.WriteLine(appConfigMsgWarning, nameof(ApiId));
            else
                ApiId = int.Parse(apiId);

            NumberToAuthenticate = ConfigurationManager.AppSettings[nameof(NumberToAuthenticate)];
            if (string.IsNullOrEmpty(NumberToAuthenticate))
                Debug.WriteLine(appConfigMsgWarning, nameof(NumberToAuthenticate));

            CodeToAuthenticate = ConfigurationManager.AppSettings[nameof(CodeToAuthenticate)];
            if (string.IsNullOrEmpty(CodeToAuthenticate))
                Debug.WriteLine(appConfigMsgWarning, nameof(CodeToAuthenticate));

            PasswordToAuthenticate = ConfigurationManager.AppSettings[nameof(PasswordToAuthenticate)];
            if (string.IsNullOrEmpty(PasswordToAuthenticate))
                Debug.WriteLine(appConfigMsgWarning, nameof(PasswordToAuthenticate));

            NotRegisteredNumberToSignUp = ConfigurationManager.AppSettings[nameof(NotRegisteredNumberToSignUp)];
            if (string.IsNullOrEmpty(NotRegisteredNumberToSignUp))
                Debug.WriteLine(appConfigMsgWarning, nameof(NotRegisteredNumberToSignUp));

            UserNameToSendMessage = ConfigurationManager.AppSettings[nameof(UserNameToSendMessage)];
            if (string.IsNullOrEmpty(UserNameToSendMessage))
                Debug.WriteLine(appConfigMsgWarning, nameof(UserNameToSendMessage));

            NumberToGetUserFull = ConfigurationManager.AppSettings[nameof(NumberToGetUserFull)];
            if (string.IsNullOrEmpty(NumberToGetUserFull))
                Debug.WriteLine(appConfigMsgWarning, nameof(NumberToGetUserFull));

            NumberToAddToChat = ConfigurationManager.AppSettings[nameof(NumberToAddToChat)];
            if (string.IsNullOrEmpty(NumberToAddToChat))
                Debug.WriteLine(appConfigMsgWarning, nameof(NumberToAddToChat));
        }

        public virtual async Task AuthUser()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync(NumberToAuthenticate);
            var code = CodeToAuthenticate; // you can change code in debugger too

            if (String.IsNullOrWhiteSpace(code))
            {
                throw new Exception("CodeToAuthenticate is empty in the app.config file, fill it with the code you just got now by SMS/Telegram");
            }

            TLUser user = null;
            try
            {
                user = await client.MakeAuthAsync(NumberToAuthenticate, hash, code);
            }
            catch (CloudPasswordNeededException ex)
            {
                var passwordSetting = await client.GetPasswordSetting();
                var password = PasswordToAuthenticate;

                user = await client.MakeAuthWithPasswordAsync(passwordSetting, password);
            }
            catch (InvalidPhoneCodeException ex)
            {
                throw new Exception("CodeToAuthenticate is wrong in the app.config file, fill it with the code you just got now by SMS/Telegram",
                                    ex);
            }
            Assert.IsNotNull(user);
            Assert.IsTrue(client.IsUserAuthorized());
        }

        public virtual async Task SendMessageTest()
        {
            NumberToSendMessage = ConfigurationManager.AppSettings[nameof(NumberToSendMessage)];
            if (string.IsNullOrWhiteSpace(NumberToSendMessage))
                throw new Exception($"Please fill the '{nameof(NumberToSendMessage)}' setting in app.config file first");

            // this is because the contacts in the address come without the "+" prefix
            var normalizedNumber = NumberToSendMessage.StartsWith("+") ?
                NumberToSendMessage.Substring(1, NumberToSendMessage.Length - 1) :
                NumberToSendMessage;

            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == normalizedNumber);

            if (user == null)
            {
                throw new System.Exception("Number was not found in Contacts List of user: " + NumberToSendMessage);
            }

            await client.SendTypingAsync(new TLInputPeerUser() { UserId = user.Id });
            Thread.Sleep(3000);
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "TEST");
        }
        #endregion

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

            #region WTelegramClient
            WTelegram.Helpers.Log = (l, s) => Debug.WriteLine(s);

            grpBoxConfigTelegram.Visible = false;
            this.Height = 706;
            Point point = new Point(20, 410);
            groupBox3.Location = point;


            //this.Height = 778;
            //this.grpBoxConfigTelegram.Visible = true;
            //Point point = new Point(20, 510);
            //groupBox3.Location = point;
            //this.Refresh();
            #endregion

            // CarregarBlocos();
            Cursor.Current = Cursors.Default;
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
            #region
            //lblMsgMorador.Text = "Importando moradores, aguarde...";
            //lblMsgMorador.Visible = true;
            //timer1.Enabled = true;
            //lblMsgMorador.Refresh();

            //SedexBus sedexBus = new SedexBus();
            //List<Morador> moradores = sedexBus.GetMoradores();

            //foreach (Morador morador in moradores)
            //{
            //    morador.SobreNomeMorador = PegaSobreNome(morador.NomeMorador).Trim();
            //    morador.NomeMorador = PegaPrimeiroNome(morador.NomeMorador).Trim();
            //    morador.IdApartamento = sedexBus.RetornaApartamento(morador.Bloco, morador.Apartamento).IdApartamento;
            //    sedexBus.Adicionar(morador);
            //    lblMsgMorador.Text = string.Concat("Morador ", morador.NomeMorador, " adicionado...");
            //    lblMsgMorador.Refresh();
            //}
            //lblMsgMorador.Text = string.Concat(moradores.Count, " ", " adicionados com sucesso.");
            //lblMsgMorador.Refresh();
            //return;
            #endregion

            Cursor.Current = Cursors.WaitCursor;
            lblMsgMorador.Text = "Atualizando moradores, aguarde...";
            lblMsgMorador.Visible = true;
            timer1.Enabled = true;
            lblMsgMorador.Refresh();

            UnirTabelas();

            Cursor.Current = Cursors.Default;
            lblMsgMorador.Text = "moradores atualizados...";
            lblMsgMorador.Visible = true;
            timer1.Enabled = true;
            lblMsgMorador.Refresh();



            //CarregarBlocos();
           // CarregaExcel();
            MeuCondominio.Model.HelperModel.GravaLog("Carregar excel");
        }

        private void UnirTabelas()
        {
            SedexBus sedexBus = new SedexBus();
            List<Morador> moradoresAntigos = sedexBus.GetMoradores();
            List<Morador> moradoresAtuais = sedexBus.GetMoradoresAtuais();



            foreach (Morador moradorAntigo in moradoresAntigos)
            {
                string sobreNomeAntigo = PegaSobreNome(moradorAntigo.NomeMorador.Trim());
                string nomeAntigo = PegaPrimeiroNome(moradorAntigo.NomeMorador.Trim());

                foreach (Morador moradorAtual in moradoresAtuais)
                {
                    if ((moradorAtual.NomeMorador == nomeAntigo) && (moradorAtual.SobreNomeMorador.Trim() == sobreNomeAntigo.Trim()))
                    {
                        moradorAtual.Celular1 = moradorAntigo.Celular1;
                        bool x = sedexBus.AtualizarMorador(moradorAtual);
                        lblMsgMorador.Text = string.Concat("Morador ", moradorAtual.NomeMorador, " Atualizado!");
                        lblMsgMorador.Visible = true;
                        timer1.Enabled = true;
                        lblMsgMorador.Refresh();
                    }                    
                }
            }







        }

        private void CarregaExcel()
        {
            lblMsgMorador.Text = "Importando moradores, aguarde...";
            lblMsgMorador.Visible = true;
            timer1.Enabled = true;
            lblMsgMorador.Refresh();

            Cursor.Current = Cursors.WaitCursor;

            string fileName = sLocalExcel + "Contatos.xlsx";
            var workbook = new XLWorkbook(fileName);


            var ws1 = workbook.Worksheet("Moradores");

            try
            {
                int parar = 0;

                for (int i = 2; i < 660; i++)
                {
                    Morador morador = new Morador();
                    var row = ws1.Row(i);

                    if (!row.IsEmpty())
                    {
                        string[] cUnidade = row.Cell(1).Value.ToString().Split(' '); // unidade APTO BLOCO

                        object valueApto = null;
                        object valueBloco = null;

                        if ((cUnidade.Length > 0) && (cUnidade.Length < 2))
                        {
                            valueApto = cUnidade[0];
                        }
                        else if ((cUnidade.Length > 1) && (cUnidade.Length < 3))
                        {
                            valueApto = cUnidade[0];
                            valueBloco = cUnidade[1];
                        }
                        else if ((cUnidade.Length > 0) && (cUnidade.Length < 4))
                        {
                            valueApto = cUnidade[0];
                            valueBloco = string.Concat(cUnidade[1], " ", cUnidade[2]);
                        }
                        else if ((cUnidade.Length > 3) && (cUnidade.Length < 5))
                        {
                            valueApto = cUnidade[0];
                            valueBloco = string.Concat(cUnidade[1], " ", cUnidade[2], " ", cUnidade[3]);
                        }
                        else if ((cUnidade.Length > 4) && (cUnidade.Length < 6))
                        {
                            valueApto = cUnidade[0];
                            valueBloco = string.Concat(cUnidade[1], " ", cUnidade[2], " ", cUnidade[3], " ", cUnidade[4]);
                        }



                        if (!string.IsNullOrEmpty(cUnidade[1]))
                            valueBloco = cUnidade[1];

                        string[] cNomeCompleto = row.Cell(2).Value.ToString().Split(' '); // Morador
                        object valueNome = cNomeCompleto[0];

                        object valueSobreNome = string.Empty;

                        for (int k = 1; k < cNomeCompleto.Length; k++)
                        {
                            valueSobreNome += string.Concat(cNomeCompleto[k], " ");
                        }

                        var cTipo = row.Cell(3); // Tipo: Proprietario/Residente
                        object valueTipoMorador = cTipo.Value;

                        var cTelefoneFixo = row.Cell(4); // Telefone Fixo
                        object valueTelFixo = SomenteNumeros(cTelefoneFixo.Value.ToString());

                        var cTelCelular = row.Cell(5); // Telefone celular
                        object valueTelCelula = SomenteNumeros(cTelCelular.Value.ToString());

                        string[] cEmails = row.Cell(13).Value.ToString().Split(';'); // email
                        object valueEmail = cEmails;


                        morador.Apartamento = valueApto.ToString();
                        morador.Bloco = valueBloco.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.SobreNomeMorador = valueSobreNome.ToString();
                        morador.TelefoneFixo = valueTelFixo.ToString();
                        morador.Celular1 = valueTelCelula.ToString();
                        morador.Email1 = cEmails[0].ToLower();

                        for (int m = 1; m < cEmails.Length; m++)
                        {
                            morador.Email2 = cEmails[m].ToLower();
                        }

                        SedexBus bus = new SedexBus();

                        Apartamento apMorador = new Model.Apartamento();

                        //if (morador.Apartamento.Length > 3)
                        //{ apMorador.IdApartamento = 0; }
                        //else
                            
                        apMorador = bus.RetornaApartamentoFiltroExcel(morador.Bloco, morador.Apartamento);

                        if (apMorador.IdApartamento > 0)
                            morador.IdApartamento = apMorador.IdApartamento;


                        if (bus.Adicionar(morador))
                            continue;
                        HelperModel.GravaLog("Erro ao carregar bloco02, morador: " + morador.Bloco + "-" + morador.Apartamento);
                    }
                }
                Cursor.Current = Cursors.Default;
                lblMsgMorador.Text = "Moradores importados com sucesso!";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
                lblMsgMorador.Refresh();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                lblMsgMorador.Text = "Ocorreu um erro ao importar moradores!";
                lblMsgMorador.Visible = true;
                timer1.Enabled = true;
                lblMsgMorador.Refresh();

                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, ex.Message);
            }
        }
        private void CarregaBloco01()
        {
            string fileName = sLocalExcel + "cadastro  moradores 09 11 20 ATUALIZADO.xlsx";
            var workbook = new XLWorkbook(fileName);
            
            
            var ws1 = workbook.Worksheet(2); // Bloco01

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "01";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "02";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "03";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "04";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "05";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "06";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "07";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "08";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "09";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "10";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "11";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "12";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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

                        var cEmail1Morador = row.Cell(6); // Email1
                        object valueEmail1 = cEmail1Morador.Value;

                        morador.Bloco = "13";
                        morador.Apartamento = valueApto.ToString();
                        morador.NomeMorador = valueNome.ToString();
                        morador.Celular1 = valueCelular.ToString();
                        morador.Email1 = valueEmail1.ToString();

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
                    listViewMoradoresApto.Items.Add(moradoresBlocos[i].NomeMorador);
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

                if (((sBloco >= 0) && (sBloco <= 13)) || e.KeyChar == 13)
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
                listViewMoradoresApto.Items.Clear();
                listViewMoradoresApto.Refresh();
                txtNomeMoraador.Text = "";
                txtCelular.Text = "";
                txtEmail1.Text = "";
                txtCodBarras.Text = "";
                IdMoradorSedex = 0;

                if (string.IsNullOrEmpty(e.KeyChar.ToString()) || (string.IsNullOrEmpty(cboApto.Text)))
                    return;

                string sApto = cboApto.Text;

                if ((int.Parse(sApto) > 0) && ((int.Parse(sApto) > 10) && (int.Parse(sApto) <= 104)))
                {
                    listViewMoradoresApto.Items.Clear();

                    string sBloco = cboBloco.Text;

                    SedexBus bus = new SedexBus();
                    var moradores = bus.Moradores(sBloco, sApto);
                    foreach (Morador morador in moradores)
                    {
                        listViewMoradoresApto.Items.Add(string.Concat(morador.NomeMorador));
                        //listViewMoradoresApto.Items.Add(string.Concat(morador.NomeMorador, " ", morador.SobreNomeMorador));
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
        private void listViewMoradoresApto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            listViewMoradoresApto.Refresh();

            if (listViewMoradoresApto.SelectedItems.Count < 1)
                return;

            string nomeMorador = listViewMoradoresApto.SelectedItems[0].Text;
            string bloco = cboBloco.Text;

            string apto = cboApto.Text;

            if ((int.Parse(apto) > 0) && (int.Parse(apto) < 5))
                apto = apto.PadLeft(2, '0');
            else
                apto = cboApto.Text.Substring(0, 1) == "0" ? cboApto.Text.Substring(1, 2) : cboApto.Text;

            SedexBus bus = new SedexBus();
            Morador morador = bus.Consultar(bloco, apto, nomeMorador);

            if (morador.IdMorador > 0)            
            {
                txtNomeMoraador.Text = string.Concat(morador.NomeMorador, " ", morador.SobreNomeMorador);
                txtCelular.Text = morador.Celular1;
                txtEmail1.Text = morador.Email1;
                IdMoradorSedex = morador.IdMorador;
                idApartamentomorador = morador.IdApartamento;

                btnExcluir.Enabled = true;
                
                CarregaHistorico(IdMoradorSedex);
            }
            else
                btnExcluir.Enabled = false;


            

            Cursor.Current = Cursors.Default;
            listViewMoradoresApto.Refresh();
        }

        private void CarregaHistorico(int IdMorador)
        {
            lstHistorico.Items.Clear();
            SedexBus sedexBus = new SedexBus();
            List<SedexHistorico> historicoMorador = sedexBus.GetHistoricoPorMorador(IdMoradorSedex);


            for (int i = 0; i < historicoMorador.Count; i++)
            {
                var data = historicoMorador[i].DataEnvio.Split('-');
                var diaHora = data[2].Split(' ');
                var telefone = string.Concat(historicoMorador[i].NumeroEnviado.Substring(0, 2), "-");
                telefone += string.Concat(historicoMorador[i].NumeroEnviado.Substring(2, 5), "-");
                telefone += string.Concat(historicoMorador[i].NumeroEnviado.Substring(7, 4));

                ListViewItem item = new ListViewItem(historicoMorador[i].NomeTorre);
                item.SubItems.Add(historicoMorador[i].Apartamento);
                item.SubItems.Add(historicoMorador[i].NomeMorador);
                item.SubItems.Add(historicoMorador[i].Email1Enviado);
                item.SubItems.Add(telefone);
                item.SubItems.Add(string.Concat(diaHora[0], "/", data[1], "/", data[0], " ", diaHora[1]));
                item.SubItems.Add(historicoMorador[i].CodigoBarraEtiqueta);

                lstHistorico.Items.Add(item);
            }
        }

        private void AtualizaMorador()
        {
            Morador morador = new Morador();
            morador.IdMorador = IdMoradorSedex;
            morador.IdApartamento = idApartamentomorador;
            // morador.SobreNomeMorador = PegaSobreNome(txtNomeMoraador.Text.Trim());
            morador.NomeMorador = txtNomeMoraador.Text.Trim().ToUpper(); //PegaPrimeiroNome(txtNomeMoraador.Text.Trim());
            morador.Bloco = cboBloco.Text;
            morador.Apartamento = cboApto.Text;
            txtCelular.Mask = "";
            morador.Celular1 = SomenteNumeros(txtCelular.Text);
            txtCelular.Mask = "(99) 00000-0000";
            morador.Email1 = txtEmail1.Text;
            //morador.ReciboImpresso = "N";

            SedexBus bus = new SedexBus();

            bool sucesso = false;

            if (IdMoradorSedex < 1)
            {
                //morador.DataCadastro = string.Concat(DateTime.Now.Day.ToString(), "/", DateTime.Now.Month.ToString(), "/", DateTime.Now.Year.ToString(), " ", DateTime.Now.Hour.ToString(), ":", DateTime.Now.Minute.ToString());
                sucesso = bus.Adicionar(morador);
            }
            else
                sucesso = bus.AtualizarMorador(morador);
            
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
            //sDataCadastro = morador.DataCadastro;
            //sDataEntrega = morador.DataEntrega;
            //sDataEnvioMensagem = morador.DataEnvioMensagem;
            IdMoradorSedex = morador.IdMorador;
            cboBloco.Text = morador.Bloco;
            cboApto.Text = morador.Apartamento;
            txtCelular.Text = morador.Celular1;
            //txtCodBarras.Text = morador.CodigoBarraEtiqueta;
            txtEmail1.Text = morador.Email1;
            //txtEtiquetaLocal.Text = morador.CodigoBarraEtiquetaLocal;
            //txtNomeMoraador.Text = morador.NomeMorador;
            //txtPrateleira.Text = morador.LocalPrateleira.ToString();
            //txtQrcode.Text = morador.CodigoQRCode;

            //if (!string.IsNullOrEmpty(morador.DataEntrega))
            //{
            //    lblMsgMorador.Text = string.Concat("Entregue em: ", morador.DataEntrega, Environment.NewLine, "Enviado por: ");
            //    if (morador.EnviadoPorSMS == "S")
            //        lblMsgMorador.Text += " -SMS- ";
            //    if (morador.EnviadoPorZAP == "S")
            //        lblMsgMorador.Text += " -WhatsApp- ";
            //    if (morador.EnviadoPorTELEGRAM == "S")
            //        lblMsgMorador.Text += " -Telegram- ";
            //    if (morador.EnviadoPorEmail1 == "S")
            //        lblMsgMorador.Text += " -E-Mail- ";
            //}
            //if (!string.IsNullOrEmpty(morador.DataEnvioMensagem))
            //{
            //    lblMsgMorador.Text = string.Concat("Enviado em: ", morador.DataEnvioMensagem, Environment.NewLine, "Enviado por: ");
            //    if (morador.EnviadoPorSMS == "S")
            //        lblMsgMorador.Text += " -SMS- ";
            //    if (morador.EnviadoPorZAP == "S")
            //        lblMsgMorador.Text += " -WhatsApp- ";
            //    if (morador.EnviadoPorTELEGRAM == "S")
            //        lblMsgMorador.Text += " -Telegram- ";
            //    if (morador.EnviadoPorEmail1 == "S")
            //        lblMsgMorador.Text += " -E-Mail- ";
            //}
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
            txtEmail1.Text = string.Empty;
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
            txtCelular.Mask = "";

            Morador morador = new Morador()
            {
                IdMorador = IdMoradorSedex,
                NomeMorador = txtNomeMoraador.Text,
                Bloco = cboBloco.Text,
                Apartamento = cboApto.Text,
                Celular1 = SomenteNumeros(txtCelular.Text),
                Email1 = txtEmail1.Text
            };

            txtCelular.Mask = "(99) 00000-0000";

            SedexBus bus = new SedexBus();

            bool sucesso = false;

            sucesso = bus.AtualizarTelefone(morador);

            if (morador.IdMorador < 1)
            {
                morador.IdMorador = 0;
            }

            if (!string.IsNullOrEmpty(txtCodBarras.Text))
            {
                Sedex sedex = new Sedex()
                {
                    IdMorador = morador.IdMorador,
                    IdApartamento = morador.IdApartamento,
                    CodigoBarraEtiqueta = txtCodBarras.Text,
                    CodigoBarraEtiquetaLocal = txtEtiquetaLocal.Text,
                    LocalPrateleira = string.IsNullOrEmpty(txtPrateleira.Text) ? 0 : int.Parse(txtPrateleira.Text),
                    DataCadastro = string.Concat(DateTime.Now.Day.ToString(), "/", DateTime.Now.Month.ToString(), "/", DateTime.Now.Year.ToString(), " ", DateTime.Now.Hour.ToString(), ":", DateTime.Now.Minute.ToString()),
                    ReciboImpresso = "N"

                };
                sucesso = bus.Adicionar(morador, sedex);
            }
            else
            {
                sucesso = bus.Adicionar(morador);
            }

            if (sucesso)
            {
                lblMsgMorador.Text = "Registro* salvo com sucesso!";
                lblMsgMorador.Visible = true;
                lstHistorico.Items.Clear();
                lstHistorico.Refresh();
                return;
            }
            else
            {
                lblMsgMorador.Text = "Erro ao salvar registro!";
            }
            lblMsgMorador.Visible = true;
        }

        //private void RegistrarEntrega()
        //{
        //    Morador morador = new Morador();
        //    morador.IdMorador = IdMoradorSedex;
        //    morador.NomeMorador = txtNomeMoraador.Text;
        //    morador.Bloco = cboBloco.Text;
        //    morador.Apartamento = cboApto.Text;
        //    morador.Celular1 = SomenteNumeros(txtCelular.Text);
        //    morador.Email1 = txtEmail1.Text;
        //    int iPrateleira = string.IsNullOrEmpty(txtPrateleira.Text) ? 0 : int.Parse(txtPrateleira.Text);

        //    SedexBus bus = new SedexBus();
        //    if (bus.Atualizar(morador))
        //    {
        //        lblMsgMorador.Text = "Sedex entregue!";
        //        lblMsgMorador.Visible = true;
        //        timer1.Enabled = true;
        //    }
        //}

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
            FrmImpressao frmImpressao = new FrmImpressao();
            frmImpressao.StartPosition = FormStartPosition.CenterScreen;
            frmImpressao.ShowDialog();
        }

        private void reciboDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Morador> ListaRecibo = new List<Morador>();
            SedexBus bus = new SedexBus();


            ListaRecibo = bus.RetornaListaParaRecibo();

            FrmImpressao frmImpressao = new FrmImpressao();
            frmImpressao.StartPosition = FormStartPosition.CenterScreen;
            frmImpressao.ShowDialog();
        }

        private void FrmGestaoSedex_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
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

        private void LerXml(string sParametro)
        {
            var caminho = System.Environment.CurrentDirectory;
            XDocument doc = XDocument.Load((CaminhoDadosXML(caminho) + @"\Configuracao.xml"));

            if (sParametro == "ConfigTelegram")
            { 
                try
                {
                    DataSet dsResultado = new DataSet();
                    dsResultado.ReadXml(CaminhoDadosXML(caminho) + @"\Configuracao.xml");
                    if (dsResultado.Tables.Count != 0)
                    {
                        if (dsResultado.Tables[1].Rows.Count > 0)
                        {
                            code = dsResultado.Tables[1].Rows[0].ItemArray[0].ToString();
                            hash = dsResultado.Tables[1].Rows[0].ItemArray[1].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #region

            if (sParametro == "valorMensagem")
            {
                var prods = from p in doc.Descendants("ValorMensagemSMS")
                            select new
                            {
                                ValorMensagem = p.Element("valorMensagem").Value
                            };
                foreach (var p in prods)
                {
                    sMensagemParaSms = String.Concat("Cond. Resid. Aricanduva!", Environment.NewLine, "Olá {Morador}", Environment.NewLine, p.ValorMensagem, Environment.NewLine, "Att: ADM!");
                }


                var prod = from p in doc.Descendants("ValorMensagemEmail")
                            select new
                            {
                                ValorMensagemMail = p.Element("valorMensagemMail").Value

                            };
                foreach (var p in prod)
                {
                    sMensagemParaEmail = String.Concat("Olá {Morador}", Environment.NewLine, p.ValorMensagemMail, Environment.NewLine, "Att: Administração!");
                }
            }

            #endregion

        }

        private void GravarXml()
        {
            var caminho = System.Environment.CurrentDirectory;

            try
            {
                using (DataSet dsResultado = new DataSet())
                {
                    dsResultado.ReadXml(CaminhoDadosXML(caminho) + @"\Configuracao.xml");
                    if (dsResultado.Tables.Count == 0)
                    {
                        //cria uma instância do Produto e atribui valores às propriedades
                        XmlTextWriter writer = new XmlTextWriter(CaminhoDadosXML(caminho) + @"\Configuracao.xml", System.Text.Encoding.UTF8);
                        
                        writer.WriteStartDocument(true);
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 2;
                        
                        writer.WriteStartElement("Config");
                        writer.WriteStartElement("ConfigTelegram");
                        writer.WriteStartElement("codigo");
                        writer.WriteString(code);
                        writer.WriteEndElement();
                        
                        writer.WriteStartElement("hash");
                        writer.WriteString(hash);
                        writer.WriteEndElement();
                        
                        writer.Close();
                        dsResultado.ReadXml(CaminhoDadosXML(caminho) + @"\Configuracao.xml");
                    }
                    //else
                    //{
                    //    //inclui os dados no DataSet
                    //    dsResultado.Tables[0].Rows.Add(dsResultado.Tables[0].NewRow());
                    //    dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["Codigo"] = txtCodigoProduto.Text;
                    //    dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["Nome"] = txtNomeProduto.Text.ToUpper();
                    //    dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["Preco"] = txtPreco.Text;
                    //    dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["Estoque"] = txtEstoque.Text;
                    //    dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["Descricao"] = txtDescricao.Text;
                    //    dsResultado.AcceptChanges();
                    //    //--  Escreve para o arquivo XML final usando o método Write
                    //    dsResultado.WriteXml(CaminhoDadosXML(caminho) + @"Dados\Produtos.xml", XmlWriteMode.IgnoreSchema);
                    //}
                    MessageBox.Show("Dados salvos com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        public static string CaminhoDadosXML(string caminho)
        {
            if (caminho.IndexOf("\\bin\\Debug") != 0)
            {
                caminho = caminho.Replace("\\bin\\Debug", "");
            }
            return caminho;
        }

        private void RemoveDatTelegram()
        {
            if (File.Exists(@"C:\repos\meucondominio\MeuCondominio\MeuCondominio\bin\Debug\session.dat"))
                File.Delete(@"C:\repos\meucondominio\MeuCondominio\MeuCondominio\bin\Debug\session.dat");
        }


        private void btnEnviarSms_Click(object sender, EventArgs e)
        {

            RemoveDatTelegram();

            if ((!rdbAdminstracao.Checked) && (!rdbDesenvolvedor.Checked))
            {
                MessageBox.Show("Selecione qual chave sedex que deseja usar!");
                return;
            }

            if (buttonLogin.Enabled)
                LogarTelegram();

            this.btnEnviarSms.Enabled = false;
           
            string pChaveDesenvi = rdbAdminstracao.Checked ? "Administração" : "Desenvolvedor";
            
            SedexBus sedexBus = new SedexBus();
            List<SmsEnvio> listSms = sedexBus.RetornaListaParaEnvioSms();

            LerXml("valorMensagem");

            foreach ( SmsEnvio enviarTelegram in listSms)
            {
                var nome = enviarTelegram.NomeMorador.Split(' ');
                var mensagemMorador = sMensagemParaSms.Replace("{Morador}", nome[0]);
                EnvioWTelegramClient(enviarTelegram);
                lblMsgMorador.Visible = true;
                lblMsgMorador.Text = $"Telegram enviado para {enviarTelegram.NomeMorador} com sucesso!";
                lblMsgMorador.Refresh();
                sedexBus.RegistraEnvioTelegram(enviarTelegram.ChaveSedex);
            }

            foreach (SmsEnvio enviarSms in listSms)
            {
                var nome = enviarSms.NomeMorador.Split(' ');
                var mensagemMorador = sMensagemParaSms.Replace("{Morador}", nome[0]);

                //var retorno = (EnvioMensagem.ZenviaEnvioSms(new MoradorSms() { From = "5511969410446", To = enviarSms.Celular1, Text = mensagemMorador })).Wait(300);
                var retorno = (EnvioMensagem.ZenviaEnvioSms(new MoradorSms() { From = "5511947971165", To = enviarSms.Celular1, Text = mensagemMorador })).Wait(300);

                if (retorno)
                {
                    lblMsgMorador.Visible = true;
                    lblMsgMorador.Text = $"Sms enviado para {enviarSms.NomeMorador} com sucesso!";
                    lblMsgMorador.Refresh();
                    sedexBus.RegistrarEmvioSms(enviarSms.ChaveSedex);
                }
            }


            foreach (SmsEnvio enviarEmail in listSms)
            {
                string PrimeiroNome = PegaPrimeiroNome(enviarEmail.NomeMorador);

                var mensagemMorador = sMensagemParaEmail.Replace("{Morador}", PrimeiroNome);

                mensagemMorador = mensagemMorador.Replace("{codigoSedex}", enviarEmail.CodigoBarras);  

                if (!string.IsNullOrEmpty(enviarEmail.Email1))
                {
                    EnvioMensagem.EnvioEmail1(enviarEmail.Email1, "Seu Sedex chegou!", mensagemMorador);

                    lblMsgMorador.Visible = true;
                    lblMsgMorador.Text = $"E-mail enviado para {enviarEmail.NomeMorador} com sucesso!";
                    lblMsgMorador.Refresh();
                    sedexBus.RegistrarEmvioEmail(enviarEmail.ChaveSedex);
                }
            }


            if (sedexBus.EnviarSmsParaHistorico())
            {
                sedexBus.ExcluiHistoricoVelho();   
            }
            this.btnEnviarSms.Enabled = true;
            timer1.Enabled = true;
            return;
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

            Morador morador = sedexBus.ConsultarMorador(IdMoradorSedex);

            DialogResult result;

            if (morador.IdMorador < 1)
            {
                result = MessageBox.Show("Deseja realmente excluir o registro selecionado?", "ATENÇÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                result = MessageBox.Show($"Deseja realmente excluir {morador.NomeMorador} ?", "ATENÇÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void mensagemSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConfiguracoes frmConfiguracoes = new FrmConfiguracoes();
            frmConfiguracoes.Show();
            // sMensagemParaSms
        }

        private void testeTelegramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sTelefone = SomenteNumeros(txtCelular.Text);

            this.TesteTelegram(sTelefone, txtCodBarras.Text, txtCodBarras.Text);
        }


        #region TELEGRAN
        public async void TesteTelegram(string sTelefone, string sCodigoBarras, string sMensagem)
        {

            var client = NewClient();

            //client.SendMessageAsync();

            //TelegramClient client;
            var store = new FileSessionStore();

            /// Edinei
            ///client = new TelegramClient(8106364, "d1934a983b83df5e690abf9a52fe2d0a", store);

            /// Adiministração
            client = new TelegramClient(8311909, "b96f58813637f9f406579636d2db8519", store);



            //string hash = "";
            //string code = "";
            LerXml("ConfigTelegram");

            await client.ConnectAsync();

            if (!client.IsUserAuthorized())
            {
                hash = await client.SendCodeRequestAsync("+5511947971165");

                if (InputBox("Chave para Telegram", "Informe o código que recebeu no telegram", ref code) == DialogResult.OK)
                {
                    GravarXml();
                }
            }

            // Anna 11963198516
            try
            {
                //For authentication you need to run following code
                //var hash = await client.SendCodeRequestAsync("+5511969410446");

                //string code = "";
                //if (InputBox("Chave para Telegram", "Informe o código que recebeu no telegram", ref code) == DialogResult.OK)
                //{
                //    //                if (string.IsNullOrEmpty(code))
                //    //                    lblMsgMorador.Text = "";
                //}
                //code = codeHash; // "61968"; // you can change code in debugger

                var user = await client.MakeAuthAsync("+5511947971165", hash, code);

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("AUTH_RESTART"))
                    this.TesteTelegram(sTelefone, txtCodBarras.Text, txtCodBarras.Text);
            }


            //You can call any method on authenticated user. For example, let's send message to a friend by his phone number:
            //get available contacts
            var result = await client.GetContactsAsync();


            string foneEnvio = SomenteNumeros(txtCelular.Text);
            foneEnvio = string.Concat("55", foneEnvio);


            string[] contatos = new string[] { "5511969410446", "5511976845889", "5511963198516" };




            for (int i = 0; i < contatos.Length; i++)
            {

                //find recipient in contacts
                var user2 = result.Users
                    .Where(x => x.GetType() == typeof(TLUser))
                    .Cast<TLUser>()
                    .FirstOrDefault(x => x.Phone == contatos[i]); //foneEnvio //x.FirstName == "Anna Clara"); //+5511963198516


                sMensagem = @"Cond. Resid. Aricanduva!" + Environment.NewLine;
                sMensagem += "Olá " + user2.FirstName + Environment.NewLine;
                sMensagem += "Seu Sedex Código: [" + sCodigoBarras + "] chegou e está disponível para retirada na Adminstração de segunda a sexta das 9 as 18 horas e sábado das 9 as 12 horas! " + Environment.NewLine;
                sMensagem += "Att: Administracao!";

                //send message
                var s = await client.SendMessageAsync(new TLInputPeerUser() { UserId = user2.Id }, sMensagem);

            }



        }
        #endregion

        #region InputBox
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        #endregion
        private string PegaPrimeiroNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return "";

            string[] nomeCompleto = nome.Split(' ');
            return nomeCompleto[0];
        }

        private string PegaSobreNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return "";

            string[] nomeCompleto = nome.Split(' ');

            string SobreNome = "";

            for (int i = 1; i < nomeCompleto.Length; i++)
            {
                SobreNome += string.Concat(nomeCompleto[i], " ");
            }

            return SobreNome;
        }

        private void relatórioAcademiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRelatorioAcademia frmRelatorioAcademia = new FrmRelatorioAcademia();
            frmRelatorioAcademia.Show();
        }


        #region WTelegramCliente
        private void FrmGestaoSedex_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client?.Dispose();
            Properties.Settings.Default.Save();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel)sender).Tag as string);
        }
        
        private async void EnvioWTelegramClient(SmsEnvio enviar)
        {
            //Connexão
            _client = new WTelegram.Client(Config);

            _user = await _client.LoginUserIfNeeded();
            lblMsgMorador.Text = ($"Você está conectado como: {_user}");
            timer1.Enabled = true;

            if (textBoxCode.Visible)
            {
                this.Height = 778;
                this.grpBoxConfigTelegram.Visible = true;
                Point point = new Point(20, 510);
                groupBox3.Location = point;
                this.Refresh();
            }
            else
            {
                this.Height = 706;
                this.grpBoxConfigTelegram.Visible = false;
                Point point = new Point(20, 410);
                groupBox3.Location = point;
                this.Refresh();
            }

            var result = await _client.Contacts_GetContacts(_client.GetHashCode());

            foreach (User user in result.users.Values)
            {
                if (user.phone == String.Concat("55", enviar.Celular1.Trim()))
                {
                    _user = user;
                    break;
                }
            }

            enviar.CodigoBarras = string.IsNullOrEmpty(enviar.CodigoBarras.ToString()) ? "X" : enviar.CodigoBarras;

            string msg2 = @"Cond. Resid. Aricanduva!" + Environment.NewLine;
            msg2 += "Olá " + _user.first_name + Environment.NewLine;
            msg2 += "Seu Sedex de código " + enviar.CodigoBarras + " chegou e está disponível para retirada na Adminstração de segunda a sexta das 9 as 18 horas e sábado das 9 as 12 horas! " + Environment.NewLine;
            msg2 += "Att: Administracao!";

            await _client.SendMessageAsync(new InputPeerUser() { user_id = _user.id }, msg2);
        }

        string Config(string what)
        {
            switch (what)
            {
                case "api_id": return textBoxApiID.Text;
                case "api_hash": return textBoxApiHash.Text;
                case "phone_number": return textBoxPhone.Text;
                case "verification_code":
                case "password":
                    BeginInvoke(new Action(() => CodeNeeded(what.Replace('_', ' '))));
                    _codeReady.Reset();
                    _codeReady.Wait();
                    return textBoxCode.Text;
                default: return null;
            };
        }
        private void CodeNeeded(string what)
        {
            labelCode.Text = what + ':';
            textBoxCode.Text = "";
            labelCode.Visible = textBoxCode.Visible = buttonSendCode.Visible = true;
            textBoxCode.Focus();
            lblMsgMorador.Text = ($"Uma {what} é requerida...");
            lblMsgMorador.Visible = true;
        }
        #endregion

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LogarTelegram();
        }

        private async void LogarTelegram()
        {
            lblConexaoTelegram.Text = String.Concat($"Conectando & logando", Environment.NewLine, " no servidor Telegram...");
            _client = new WTelegram.Client(Config);
            _user = await _client.LoginUserIfNeeded();
            if (_user.IsActive)
                lblConexaoTelegram.Text = String.Concat($"Conectado ao telegram como: ", Environment.NewLine, _user.username);
            else
            {
                lblConexaoTelegram.Text = String.Concat($"Erro na conexão com telegram");
                lblConexaoTelegram.ForeColor = Color.Red;
                MostrarConfigTelegram(true);
                buttonLogin.Enabled = true;
                return;
            }
            buttonLogin.Enabled = false;
        }

        private void MostrarConfigTelegram(Boolean mostrar)
        {
            if (!mostrar)
            {
                WTelegram.Helpers.Log = (l, s) => Debug.WriteLine(s);
                grpBoxConfigTelegram.Visible = false;
                this.Height = 706;
                Point point = new Point(20, 410);
                groupBox3.Location = point;
            }
            else
            {
                this.Height = 778;
                this.grpBoxConfigTelegram.Visible = true;
                Point point = new Point(20, 510);
                groupBox3.Location = point;
                this.Refresh();
            }
        }

        private void buttonSendCode_Click(object sender, EventArgs e)
        {
            labelCode.Visible = textBoxCode.Visible = buttonSendCode.Visible = false;
            _codeReady.Set();
        }
    }
}