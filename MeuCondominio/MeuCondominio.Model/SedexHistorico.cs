namespace MeuCondominio.Model
{
    public class SedexHistorico
    {
        public int IdSedexHistorico { get; set; }
        public string NomeTorre { get; set; }
        public string Apartamento { get; set; }
        public int idMorador { get; set; }
        public string NomeMorador { get; set; }
        public string NumeroEnviado { get; set; }
        public string Email1Enviado { get; set; }
        public string DataEnvio { get; set; }
        public string CodigoBarraEtiqueta { get; set; }
    }
}
