using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace MeuCondominio
{
    public partial class FrmConfiguracoes : Form
    {
        public string pValorTxtSms;
        private int TotalCaracteres = 100;

        public FrmConfiguracoes()
        {
            InitializeComponent();
        }

        private void PreenchimentoSms(object sender, EventArgs e)
        {
            if (txtMensagemSms.Text.Length >= 100)
            {
                lblCaracteres.Text = "Caracteres restantes: " + (TotalCaracteres - txtMensagemSms.Text.Length).ToString();
                return;
            }
            
            lblCaracteres.Text = "Caracteres restantes: " + (TotalCaracteres - txtMensagemSms.Text.Length).ToString();

            txtFinal.Text = "Cond. Resid. Aricanduva!" + Environment.NewLine;
            txtFinal.Text += "Ola {NomeMorador}" + Environment.NewLine;
            txtFinal.Text += txtMensagemSms.Text + Environment.NewLine;
            txtFinal.Text += "Att: Administracao!";
            txtFinal.Refresh();
            pValorTxtSms += txtMensagemSms.ToString();
        }

        private void FrmConfiguracoes_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarNoXml();
            this.Close();
        }

        private void SalvarNoXml()
        {
            var caminho = System.Environment.CurrentDirectory;

            using (DataSet dsResultado = new DataSet())
            {
                dsResultado.ReadXml(CaminhoDadosXML(caminho) + @"\Configuracao.xml");
                if (dsResultado.Tables.Count > 0)
                    dsResultado.Tables.RemoveAt(0);

                {
                    XmlTextWriter writer = new XmlTextWriter(CaminhoDadosXML(caminho) + @"\Configuracao.xml", System.Text.Encoding.UTF8);

                    writer.WriteStartDocument(true);
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    writer.WriteStartElement("ValorMensagemSMS");
                    writer.WriteStartElement("valorMensagem");
                    writer.WriteString(txtMensagemSms.Text.Trim());
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                    dsResultado.ReadXml(CaminhoDadosXML(caminho) + @"\Configuracao.xml");
                }
            }
        }

        private void FrmConfiguracoes_Load(object sender, EventArgs e)
        {
            LerXml();
        }

        private void LerXml()
        {
            var caminho = System.Environment.CurrentDirectory;

           
            XDocument doc = XDocument.Load((CaminhoDadosXML(caminho) + @"\Configuracao.xml"));
            var prods = from p in doc.Descendants("ValorMensagemSMS")
                        select new
                        {
                            ValorMensagem = p.Element("valorMensagem").Value
                        };
            foreach (var p in prods)
            {
                txtFinal.Text = String.Concat("Cond. Resid. Aricanduva!", Environment.NewLine, "Ola {Morador}", Environment.NewLine, p.ValorMensagem, Environment.NewLine, "Att: Administracao!");
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
    }
}
