namespace MeuCondominio.Model
{
    public class Sedex
    {
        public int IdSedex { get; set; }
        public int IdMorador { get; set; }
        public int IdApartamento { get; set; }
        public string CodigoBarraEtiqueta { get; set; }
        public string CodigoQRCode { get; set; }
        public string CodigoBarraEtiquetaLocal { get; set; }
        public int LocalPrateleira { get; set; }
        public string DataCadastro { get; set; }
        public string DataEntrega { get; set; }
        public string DataEnvioMensagem { get; set; }
        public string EnviadoPorSms { get; set; }
        public string EnviadoPorWhatsApp { get; set; }
        public string EnviadoPorTelegram { get; set; }
        public string EnviadoPorEmail1 { get; set; }
        public string ReciboImpresso { get; set; }
        public string EncomendaAlimento { get; set; }
        public string EncomendaMedicamento { get; set; }
    }
}
