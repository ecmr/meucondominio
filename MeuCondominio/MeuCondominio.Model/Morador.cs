namespace MeuCondominio.Model
{
    public class Morador
    {
        public int IdMorador { get; set; }
        public string Bloco { get; set; }
        public string Apartamento { get; set; }
        public string NomeDestinatario { get; set; }
        public string email { get; set; }
        public string NumeroCelular { get; set; }
        public string CodigoBarraEtiqueta { get; set; }
        public string CodigoQRCode { get; set; }
        public string CodigoBarraEtiquetaLocal { get; set; }
        public int LocalPrateleira { get; set; }
        public int EncomendaAlimento { get; set; }
        public int EncomendaMedicamento { get; set; }
        public string DataCadastro { get; set; }
        public string DataEntrega { get; set; }
        public string DataEnvioMensagem { get; set; }
        public string Enviadosms { get; set; }
        public string EnviadoZap { get; set; }
        public string EnviadoTelegram { get; set; }
        public string EnviadoEmail { get; set; }
    }
}
