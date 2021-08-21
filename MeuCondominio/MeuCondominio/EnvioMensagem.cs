﻿using MeuCondominio.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MeuCondominio
{
    public class EnvioMensagem
    {

        #region Envio SMS DEV
        /// <summary>
        /// 
        /// </summary>
        /// <param name="morador"></param>
        public static bool EnvioSmsDev(Morador morador)
        {
            string sHost = "https://api.smsdev.com.br/v1/send?key=";
            string sKey = "I6GJF7FHCF6O1J8Z4M0V7SNR75MN6STOQ4NGSNFAWH8Z1OH6FHD5ZL7QC36L0962KU5T6NA038U9G5YMN0T6E0F28U2SWMDGBET8CS7JQ8FGR5DZ3ZSEAL5SO8M39NGF";
            string sFone = string.Concat("&type=9&number=", morador.NumeroCelular);
            string sMsg = string.Concat("&msg=Cond. Resid. Aricanduva!",
                Environment.NewLine, " Olá ", morador.NomeDestinatario, Environment.NewLine,
                " Seu Sedex chegou e já está disponível para retirada na Adm até as 18 horas, Sabado até 12 horas!", Environment.NewLine,
                "Att: Administração!");

            // Create a request using a URL that can receive a post.
            // "https://api.smsdev.com.br/v1/send?key=I6GJF7FHCF6O1J8Z4M0V7SNR75MN6STOQ4NGSNFAWH8Z1OH6FHD5ZL7QC36L0962KU5T6NA038U9G5YMN0T6E0F28U2SWMDGBET8CS7JQ8FGR5DZ3ZSEAL5SO8M39NGF&type=9&number=11969410446&msg=Teste"
            WebRequest request = WebRequest.Create(string.Concat(sHost, sKey, sFone, sMsg));
            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();

            string responseFromServer;

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
            }

            var retornoJson = JsonConvert.DeserializeObject<JSonSmsDev>(responseFromServer);

            if (retornoJson.situacao == "OK")
            {
                response.Close();
                return true;
            }
            else if (retornoJson.situacao == "ERRO")
            {
                response.Close();
                return false;
            }
            return false;
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

            ////string.Concat("+55", morador.NumeroCelular)
            //Console.WriteLine(message.Sid);
        }
        public static void EnvioSmsTeste(Morador morador)
        {
            string toPhoneNumber = "+5511" + morador.NumeroCelular;
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
            //    new PhoneNumber("whatsapp:+5511947971165")); // 55" + morador.NumeroCelular));
            //messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            //messageOptions.Body = "Condominio Residencial Aricanduva. Sua camiseta do TIMÃO chegou! Já pode vir buscar.";

            //var message = MessageResource.Create(messageOptions);
            //Console.WriteLine(message.Body);
            #endregion
        }
        public static void EnvioEmail()
        { }
    }
    public class JSonSmsDev
    {
        public string situacao { get; set; }
        public string codigo { get; set; }
        public string id { get; set; }
        public string descricao { get; set; }
    }

}