namespace MeuCondominio.Model
{
    public class Morador
    {
        public int IdMorador { get; set; }
        public int IdApartamento { get; set; }
        public string Bloco { get; set; }
        public string Apartamento { get; set; }
        public string NomeMorador { get; set; }
        public string SobreNomeMorador { get; set; }
        public string TelefoneFixo { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
}
