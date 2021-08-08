using System.IO;

namespace MeuCondominio.Model
{
    public class HelperModel
    {
        /// <summary>
        /// Metodo para gravar logs
        /// </summary>
        /// <param name="LogValor"></param>
        public static void GravaLog(string LogValor)
        {
            string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

            string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);

            File.WriteAllText(nomeArquivoDeLog, LogValor);
        }
    }
}
