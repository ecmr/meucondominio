using HumanAPIClient.Model;
using HumanAPIClient.Service;
using MeuCondominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Telegram.Bot;
using TeleSharp.TL;
using TLSharp.Core;

// WTelegramClient exemplos
using TL;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

using Microsoft.VisualBasic;
using System.Windows.Forms;



namespace MeuCondominio
{

    public class MoradorSms
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
    }

    public class EnvioMensagem
    {
        readonly static TelegramClient clientT = new TelegramClient(8106364, "d1934a983b83df5e690abf9a52fe2d0a");
        static string PHONE_CODE = string.Empty;

        #region Envio SMS Zenvia



        static HttpClient client = new HttpClient();

        static async Task<Uri> EnvioZenviaSmsAsync(MoradorSms sms)
        {
            var jsonSms = new JavaScriptSerializer().Serialize(sms);

            using (HttpClient client = new HttpClient())
            {
                StringContent queryString = new StringContent(jsonSms, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri("https://api.zenvia.com/v2/channels/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZWRpbmVpLm1lbmV6ZXNAZ21haWwuY29tOkRjWkAyM05KRjI2Mm14Yw==");
                var response = await client.PostAsJsonAsync(string.Concat("sms/messages"), queryString);


                if (response.IsSuccessStatusCode)
                    return response.Headers.Location;
                else
                    return response.Headers.Location;
            }


            return null;

            #region

            //var jsonSms = new JavaScriptSerializer().Serialize(sms);

            //HttpResponseMessage response = await client.PostAsJsonAsync("sms/messages", jsonSms);

            //if (response.IsSuccessStatusCode)
            //    return response.Headers.Location;
            //else
            //    return response.Headers.Location;

            ////var resp = response.EnsureSuccessStatusCode();

            //// return URI of the created resource.
            ////return response.Headers.Location;

            #endregion


        }

        public static async Task<bool> ZenviaEnvioSms(MoradorSms sms)
        {
            client.BaseAddress = new Uri("https://api.zenvia.com/v2/channels/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-API-TOKEN", "1Ww0DECpl_Q3GqMMSXIo0EUGI3FL52G_ccrs");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            

            try
            {
                // Create a new SMS
                MoradorSms moradorSms = new MoradorSms {From = sms.From, To = sms.To, Text = sms.Text};

                var url = await EnvioZenviaSmsAsync(moradorSms);
                return true;
            } 
            catch (Exception e)
            {
                return false;
            }
        }

        public static string EnvioZenvia(MoradorSms sms)
        {
            MultipleSending multiCliente = new MultipleSending("edinei.menezes@gmail.com", "DcZ@23NJF262mxc");

            SimpleSending simleCliente = new SimpleSending("edinei.menezes@gmail.com", "DcZ@23NJF262mxc");
            
            
            SimpleMessage mensagem = new SimpleMessage();
            mensagem.To = sms.To;
            mensagem.Message = sms.Text;
            mensagem.Schedule = "25/01/2022 10:00:00";

            List<String> retornos = simleCliente.send(mensagem);

            return "Enviado";

           
        }





        #endregion

        #region Envio SMS DEV
        /// <summary>
        /// 
        /// </summary>
        /// <param name="morador"></param>
        public static bool EnvioSmsDev(SmsEnvio enviarPara, string pMensagem, string pKey)
        {
            SimpleSending cliente = new SimpleSending("cd009438-db18-46a5-a078-45a8f51cd435", "DcZ@23NJF262mxc");

            SimpleMessage mensagem = new SimpleMessage();
            mensagem.To = enviarPara.Celular1;
            mensagem.Message = pMensagem;
            mensagem.Schedule = "08/01/2022 13:00:00";

            List<String> retornos = cliente.send(mensagem);

            var xcxcx = "";

            #region

            ////string sHost = "https://api.smsdev.com.br/v1/send?key=";
            ////string sKey = string.Empty;

            //////Chave Edinei - meu celular
            ////if (pKey == "Desenvolvedor")
            ////    sKey = "I6GJF7FHCF6O1J8Z4M0V7SNR75MN6STOQ4NGSNFAWH8Z1OH6FHD5ZL7QC36L0962KU5T6NA038U9G5YMN0T6E0F28U2SWMDGBET8CS7JQ8FGR5DZ3ZSEAL5SO8M39NGF";
            ////else if (pKey == "Administração")
            ////    sKey = "GYU1CWX2T3JUTGUJ7XT6OSE0NORTYAM7L4UF447UG9QQYDIA7X2LR5Y3NMVA7JAE8D9XL1DN7KWN6WAULTFPGKA9QQPBOH9PN0OKFWYEHZ9X29Y8FB0O4KNZ4Z99INCV";

            //string[] nomeMorador = enviarPara.NomeMorador.Split(' ');

            //if (string.IsNullOrEmpty(pMensagem))
            //    pMensagem = "Seu Sedex chegou e já está disponível para retirada na Adm até as 18 horas, Sabado até 12 horas!";

            ////string sFone = string.Concat("&type=9&number=", enviarPara.Celular1);

            ////string sMsg = string.Concat("&msg=", pMensagem);

            //// Create a request using a URL that can receive a post.
            //// "https://api.smsdev.com.br/v1/send?key=I6GJF7FHCF6O1J8Z4M0V7SNR75MN6STOQ4NGSNFAWH8Z1OH6FHD5ZL7QC36L0962KU5T6NA038U9G5YMN0T6E0F28U2SWMDGBET8CS7JQ8FGR5DZ3ZSEAL5SO8M39NGF&type=9&number=11969410446&msg=Teste"
            //WebRequest request = WebRequest.Create(string.Concat(sHost, sKey, sFone, sMsg));
            //// Set the Method property of the request to POST.
            //request.Method = "POST";

            //// Create POST data and convert it to a byte array.
            //string postData = "This is a test that posts this string to a Web server.";
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //// Set the ContentType property of the WebRequest.
            //request.ContentType = "application/x-www-form-urlencoded";
            //// Set the ContentLength property of the WebRequest.
            //request.ContentLength = byteArray.Length;

            //// Get the request stream.
            //Stream dataStream = request.GetRequestStream();
            //// Write the data to the request stream.
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //// Close the Stream object.
            //dataStream.Close();

            //// Get the response.
            //WebResponse response = request.GetResponse();

            //string responseFromServer;

            //// Get the stream containing content returned by the server.
            //// The using block ensures the stream is automatically closed.
            //using (dataStream = response.GetResponseStream())
            //{
            //    // Open the stream using a StreamReader for easy access.
            //    StreamReader reader = new StreamReader(dataStream);
            //    // Read the content.
            //    responseFromServer = reader.ReadToEnd();
            //}

            //var retornoJson = JsonConvert.DeserializeObject<JSonSmsDev>(responseFromServer);

            //if (retornoJson.situacao == "OK")
            //{
            //    response.Close();
            //    return true;
            //}
            //else if (retornoJson.situacao == "ERRO")
            //{
            //    response.Close();
            //    return false;
            //}
            //return false;

            #endregion

            return true;
        }
        #endregion


        #region 
        //private ws _ws;
        //private readonly Subject<Notification> rxSubject = new();
        delegate void SetTextMessage(string message);
        delegate void SetChangeMode(bool value);
        #endregion

        public static void EnvioSms(Morador morador)
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // 
            // string accountSid = "ACbb8f1afad635225025b0b9265e3a3416"; // Environment.GetEnvironmentVariable("ACbb8f1afad635225025b0b9265e3a3416");
            // string authToken = "4466dbaca9b1489f1c70c1909f4e52b8"; // Environment.GetEnvironmentVariable("4466dbaca9b1489f1c70c1909f4e52b8");

            //var accountSid = "AC34558a86426edfb17d4cb820906433fd"; //"AC34558a86426edfb17d4cb820906433fd";
            //var authToken = "c0666589bad1628ad246efd285a3747a"; // "2cc547527fefd6bf53bdb75437a4530f";

            //TwilioClient.Init(accountSid, authToken);

            //var message = MessageResource.Create(
            //    body: "Condominio Residencial Aricanduva. Seu sedex chegou! Já pode vir buscar.",
            //    from: new Twilio.Types.PhoneNumber("+18178544536"),
            //    to: new Twilio.Types.PhoneNumber("+5511947971165")
            //);

            ////string.Concat("+55", morador.Celular1)
            //Console.WriteLine(message.Sid);
        }
        public static void EnvioSmsTeste(Morador morador)
        {
            string toPhoneNumber = "+5511" + morador.Celular1;
            string login = "SeuLogin";
            string password = "SuaSenha";
            string compression = "assunto";
            string body = "Condominio Residencial Aricanduva. Seu sedex chegou! Já pode vir buscar.";

            try
            {
                try
                {
                    MailMessage Message = new MailMessage();
                    Message.From = new MailAddress(login + "@ipipi.com");
                    Message.Headers.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                    Message.Headers.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", login);
                    Message.Headers.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
                    Message.Subject = compression;
                    Message.Body = body;

                    try
                    {
                        System.Net.Mail.SmtpClient smtpClient = new SmtpClient("ipipi.com");
                        smtpClient.Send(Message);
                    }
                    catch (Exception ehttp)
                    {
                        Console.WriteLine("{0}", ehttp.Message);
                        Console.WriteLine("Here is the full error message output");
                        Console.Write("{0}", ehttp.ToString());
                    }
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Unknown Exception occurred {0}", e.Message);
                Console.WriteLine("Here is the Full Message output");
                Console.WriteLine("{0}", e.ToString());
            }
        }
        public static void EnvioZap(Morador morador)
        {
            #region
            //var accountSid = "AC34558a86426edfb17d4cb820906433fd"; //"AC34558a86426edfb17d4cb820906433fd";
            //var authToken = "c0666589bad1628ad246efd285a3747a"; // "2cc547527fefd6bf53bdb75437a4530f";
            //TwilioClient.Init(accountSid, authToken);

            //var messageOptions = new CreateMessageOptions(
            //    new PhoneNumber("whatsapp:+5511947971165")); // 55" + morador.Celular1));
            //messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            //messageOptions.Body = "Condominio Residencial Aricanduva. Sua camiseta do TIMÃO chegou! Já pode vir buscar.";

            //var message = MessageResource.Create(messageOptions);
            //Console.WriteLine(message.Body);
            #endregion
        }
        public static void EnvioEmail1(string paraqualemail, string assunto, string mensagem)
        {
            var fromAddress = new MailAddress("residencialaricanduva.boletos@gmail.com", "Condomínio Residencial Aricanduva");
            var toAddress = new MailAddress(paraqualemail, "Adm");
            const string fromPassword = "rlectipgdqbpjbgm";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = assunto,
                Body = mensagem
            })
            {
                smtp.Send(message);
            }
        }

        #region TELEGRAN

        private static async Task Connect()
        {
            await clientT.ConnectAsync();
        }

        #region envio direto ao chat
        // envio para chat do condominio 
        private static readonly TelegramBotClient Bot = new TelegramBotClient("5107933535:AAHV6ZTyWOFBFBKB5kVkNWOEouXwE5UxX1k");

        // envio para chat do condominio
        private static async void Bot_SendMessage(string id, string pesan)
        {
            await Bot.SendTextMessageAsync(
                chatId: id,
                text: pesan);
        }
        // envio para chat do condominio
        public static void EnviarMensagemParaChatTelegram(string Mensagem)
        {
            Bot_SendMessage("1506046544", Mensagem);
        }

        #endregion
        public static async void EnviarMensagemParaContatoTelegram(string sTelefone, string sMensagem)
        {


            Connect().Wait(3000);

            //For authentication you need to run following code
            // var hash = await client.SendCodeRequestAsync("+5511969410446");

            var hash = "d1934a983b83df5e690abf9a52fe2d0a"; //"d10bd30e1cfda9b219";



            try
            {



                PHONE_CODE =  "s72507"; // you can change code in debugger

                var user = await clientT.MakeAuthAsync("+5511969410446", hash,  PHONE_CODE);


                //You can call any method on authenticated user. For example, let's send message to a friend by his phone number:
                //get available contacts
                var result = await clientT.GetContactsAsync();

                //find recipient in contacts
                var user2 = result.Users
                    .Where(x => x.GetType() == typeof(TLUser))
                    .Cast<TLUser>()
                    .FirstOrDefault(x => x.Phone == sTelefone);
                // x.FirstName == "Anna Clara"); //+5511963198516
                // "5511963198516"


                sMensagem = @"Cond. Resid. Aricanduva!" + Environment.NewLine;
                sMensagem += "Olá " + user2.FirstName + Environment.NewLine;
                sMensagem += "Seu Sedex chegou e está disponível para retirada na Adminstração de segunda a sexta das 9 as 18 horas e sábado das 9 as 12 horas! " + Environment.NewLine;
                sMensagem += "Att: Administracao!";

                //send message
                var s = await clientT.SendMessageAsync(new TLInputPeerUser() { UserId = user2.Id }, sMensagem);
            }
            catch (System.InvalidOperationException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region WTELEGRAMCLIENT
        //private readonly ManualResetEventSlim _codeReady = new ManualResetEventSlim(false);
        //private static WTelegram.Client _client;
        //private User _user;

        ////Label da tela original, criei uma string para representar aqui
        //private static string linkLabel = "https://my.telegram.org/apps";

        //public static void WTelegramEnvio()
        //{
        //    WTelegram.Helpers.Log = (l, s) => Debug.WriteLine(s);

        //    _client = new WTelegram.Client(Config);
        //    _user = await _client.LoginUserIfNeeded();




        //    _client?.Dispose();
        //    Properties.Settings.Default.Save();
        //}
        //private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    Process.Start(((LinkLabel)sender).Tag as string);
        //}

        //string Config(string what)
        //{
        //    switch (what)
        //    {
        //        case "api_id": return "8106364"; //textBoxApiID.Text;
        //        case "api_hash": return "d1934a983b83df5e690abf9a52fe2d0a"; // textBoxApiHash.Text;
        //        case "phone_number": return "+11969410446"; //textBoxPhone.Text;
        //        case "verification_code":
        //        case "password":
        //            BeginInvoke(new Action(() => CodeNeeded(what.Replace('_', ' '))));
        //            _codeReady.Reset();
        //            _codeReady.Wait();
        //            return textBoxCode.Text;
        //        default: return null;
        //    };
        //}

        //private void CodeNeeded(string what)
        //{
        //    labelCode.Text = what + ':';
        //    textBoxCode.Text = "";
        //    labelCode.Visible = textBoxCode.Visible = buttonSendCode.Visible = true;
        //    textBoxCode.Focus();
        //    listBox.Items.Add($"A {what} is required...");
        //}

        #endregion

    }
    public class JSonSmsDev
    {
        public string situacao { get; set; }
        public string codigo { get; set; }
        public string id { get; set; }
        public string descricao { get; set; }
    }





}
