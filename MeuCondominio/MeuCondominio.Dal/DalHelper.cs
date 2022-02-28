using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace MeuCondominio.Model
{
    public class DalHelper
    {
        private static SQLiteConnection sqliteConnection;
        private static string stBanco = @"C:\Sedex Condominio\dados\Cadastro.sqlite3";
        private static string strBancoAtual = @"C:\Users\edine\Desktop\Meu Condominio BKP\dados\Cadastro.sqlite3";

        public DalHelper()
        { }

        private static SQLiteConnection DbConnection()
        {
            try
            {
                sqliteConnection = new SQLiteConnection(string.Concat("Data Source=", stBanco, "; Version=3;"));
                sqliteConnection.Open();
            }
            catch (Exception ex)
            {
                HelperModel.GravaLog(string.Concat("Erro DbConnection: ", ex.Message));
            }

            return sqliteConnection;
        }

        private static SQLiteConnection DbConnection(string banco)
        {
            try
            {
                sqliteConnection = new SQLiteConnection(string.Concat("Data Source=", banco, "; Version=3;"));
                sqliteConnection.Open();
            }
            catch (Exception ex)
            {
                HelperModel.GravaLog(string.Concat("Erro DbConnection: ", ex.Message));
            }

            return sqliteConnection;
        }

        public static void CriarBancoSQLite()
        {
            try
            {
               // sqliteConnection.CreateFile(stBanco);
            }
            catch
            {
                throw;
            }
        }

        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Sedex(IdMorador int, Bloco Varchar(2), Apartamento Varchar(3), NomeMorador Varchar(100), Email1 Varchar(100), Celular1 Varchar(11), CodigoBarraEtiqueta Varchar(50), CodigoBarraEtiquetaLocal Varchar(50), LocalPrateleira Varchar(3)), DataEnvioMensagem VARCHAR(20), EnviadoPorSMS VARCHAR(1), EnviadoPorZAP VARCHAR(1), EnviadoPorTELEGRAM VARCHAR(1), EnviadoPorEmail1 VARCHAR(1)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region ACADEMIA
        /// <summary>
        /// Grava registro de moradores cadastrados na academia
        /// </summary>
        /// <param name="academia"></param>
        /// <returns></returns>
         public static bool AdicionarMoradorCatraca(Academia academia)
        {
            bool sucesso = false;
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    var query = @"INSERT INTO ACADEMIA (MATRICULA, NOME, BLOCO, APARTAMENTO) VALUES (@MATRICULA, @NOME, @BLOCO, @APARTAMENTO );";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@MATRICULA", academia.Matricula);
                    cmd.Parameters.AddWithValue("@NOME", academia.Nome);
                    cmd.Parameters.AddWithValue("@BLOCO", academia.Bloco);
                    cmd.Parameters.AddWithValue("@APARTAMENTO", academia.Apartamento);

                    cmd.ExecuteNonQuery();

                    sucesso = true;
                }

                return sucesso;
            }
            catch (Exception ex)
            {
                MeuCondominio.Model.HelperModel.GravaLog(string.Concat("Erro método AdicionarMoradorCatraca: ", ex.Message));
                return sucesso;
            }
        }


        /// <summary>
        /// Adiciona eventos da catraca
        /// </summary>
        /// <param name="pMatricula"></param>
        /// <param name="pEntrada"></param>
        /// <param name="pSaida"></param>
        /// <returns></returns>
        public static bool AdicionarEvento(Catraca evento)
        {
            bool sucesso = false;
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    var query = @"INSERT INTO CATRACA (MATRICULA, REGISTRO) VALUES (@MATRICULA, @REGISTRO);";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@MATRICULA", evento.Matricula);
                    cmd.Parameters.AddWithValue("@REGISTRO", evento.Registro);

                    cmd.ExecuteNonQuery();

                    sucesso = true;
                }

                return sucesso;
            }
            catch (Exception ex)
            {
                MeuCondominio.Model.HelperModel.GravaLog(string.Concat("Erro método AdicionarEvento: ", ex.Message));
                return sucesso;
            }
        }



        /// <summary>
        /// Lista eventos da catraca
        /// </summary>
        /// <returns></returns>
        public static List<AcademiaEvento> GetEventosCatraca(Catraca pPvento)
        {
            try
            {
                var query = @"SELECT 
                                BLOCO, APARTAMENTO, NOME, A.MATRICULA, REGISTRO
                            FROM 
                                ACADEMIA A INNER JOIN
                                CATRACA C ON C.MATRICULA = A.MATRICULA
                            WHERE 1 = 1 ";

                if (!string.IsNullOrEmpty(pPvento.Matricula))
                    query += @" AND A.MATRICULA = @MATRICULA";
                if (pPvento.Registro > DateTime.Parse("01/01/0001 00:00:00"))
                {
                    query += @" AND DATE(substr(REGISTRO,1,4) || '-' || substr(REGISTRO,6,2) || '-' || substr(REGISTRO,9,2)) BETWEEN DATE(@REGISTRO) AND DATE(@REGISTRO)";
                }

                query += " ORDER BY A.MATRICULA";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    if (!string.IsNullOrEmpty(pPvento.Matricula))
                        cmd.Parameters.AddWithValue("@MATRICULA", pPvento.Matricula);
                    if (pPvento.Registro > DateTime.Parse("01/01/0001 00:00:00"))
                        cmd.Parameters.AddWithValue("@REGISTRO", pPvento.Registro.ToString("yyyy-MM-dd"));


                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<AcademiaEvento> eventos = new List<AcademiaEvento>();

                        while (reader.Read())
                        {
                            AcademiaEvento evento = new AcademiaEvento
                            {
                                Bloco = reader.IsDBNull(0) ? "0" : reader.GetString(0),
                                Apartamento = reader.IsDBNull(1) ? "0" : reader.GetString(1),
                                 Nome = reader.IsDBNull(2) ? "0" : reader.GetString(2),
                                Matricula = reader.IsDBNull(3) ? "0" : reader.GetString(3),
                                Registro = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
                            };

                            eventos.Add(evento);
                        }
                        return eventos;
                    }
                    return new List<AcademiaEvento>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region MORADOR
        /// <summary>
        /// Retorna um morador pelo id
        /// </summary>
        /// <param name="idMorador"></param>
        /// <returns></returns>
        public static Morador GetMorador(int idMorador)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Morador Where IdMorador=" + idMorador;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);

                    List<Morador> moradores = new List<Morador>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Morador morador = new Morador();

                        morador.IdMorador = int.Parse(row.ItemArray[0].ToString());
                        morador.NomeMorador = row.ItemArray[1].ToString();
                        morador.SobreNomeMorador = row.ItemArray[2].ToString();
                        morador.TelefoneFixo = row.ItemArray[3].ToString();
                        morador.Celular1 = row.ItemArray[4].ToString();
                        morador.Email1 = row.ItemArray[5].ToString();
                        moradores.Add(morador);
                    }

                    if (moradores.Count > 0)
                        return moradores[0];
                    else
                        return new Morador();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static List<Morador> GetMoradorAtuais()
        {
            try
            {
                var query = @"SELECT IdMorador, UPPER(Nome), UPPER(Sobrenome), TelefoneFixo, Celular1, Celular2, Email1, Email2, IdApartamento FROM Morador;";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<Morador> moradores = new List<Morador>();

                        while (reader.Read())
                        {
                            Morador morador = new Morador
                            {
                                IdMorador = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                NomeMorador = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                SobreNomeMorador = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                TelefoneFixo = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Celular1 = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                Celular2 = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                Email1 = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Email2 = reader.IsDBNull(7) ? "" : reader.GetString(7)
                            };

                            moradores.Add(morador);
                        }
                        return moradores;
                    }
                    return new List<Morador>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Morador> GetMorador()
        {
            try
            {
                var query = @"SELECT Bloco, Apartamento, UPPER(NomeDestinatario) AS NomeDestinatario, Email, NumeroCelular FROM Sedex;";

                using (var cmd = DbConnection(strBancoAtual).CreateCommand())
                {
                    cmd.CommandText = query;
                    //cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<Morador> moradores = new List<Morador>();

                        while (reader.Read())
                        {
                            Morador morador = new Morador
                            {
                                Bloco = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                Apartamento = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                NomeMorador = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Email1 = reader.IsDBNull(2) ? "" : reader.GetString(3),
                                Celular1 = reader.IsDBNull(4) ? "" : reader.GetString(4)
                            };

                            moradores.Add(morador);
                        }
                        return moradores;
                    }
                    return new List<Morador>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Morador GetMorador(string pBloco, string pApto, string NomeCompleto)
        {
            var NomeMorador = NomeCompleto.Split(' ');
            var Sobrenome = string.Empty;

            for (int i = 1; i < NomeMorador.Length; i++)
            {
                Sobrenome += string.Concat(NomeMorador[i], " ");
            }
            Sobrenome = Sobrenome.Trim();


            try
            {
                //var query = @"SELECT DISTINCT 
                //         AP.IdApartamento, AP.NomeTorre AS BLOCO, AP.Apartamento
                //        ,MD.IDMORADOR, MD.NOME, MD.SOBRENOME, MD.TELEFONEFIXO, MD.CELULAR1, MD.CELULAR2, MD.EMAIL1, MD.EMAIL2
                //    FROM
                //        Apartamento AP
                //    INNER JOIN MORADOR MD ON AP.IdApartamento = MD.IdApartamento
                //    WHERE BLOCO = @BLOCO AND APARTAMENTO = @APARTAMENTO AND TRIM(UPPER(MD.NOME)) = TRIM(UPPER(@NOME));";
                var query = @"SELECT IDCONDOMINIO, BLOCO, APTO, NOME, EMAIL, CELULAR FROM CONDOMINIO WHERE BLOCO = @BLOCO AND APTO = @APTO AND NOME = @NOME;";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@BLOCO", pBloco);
                    //cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BLOCO ", pBloco));
                    cmd.Parameters.AddWithValue("@APTO", pApto);
                    cmd.Parameters.AddWithValue("@NOME", NomeCompleto.Trim()); // NomeMorador[0]
                    //cmd.Parameters.AddWithValue("@SOBRENOME", Sobrenome.Trim());

                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        //var morador = PreencheMoradorBanReader(reader)[0];
                        var morador = PreencheMoradorPorAptoDtReader(reader)[0];
                        //if (morador.IdMorador > 0)
                        if (!string.IsNullOrEmpty(morador.NomeMorador))
                            return morador;
                    }
                    return new Morador();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Morador> GetMorador(string pBloco, string pApto)
        {
            
            try
            {
                //var query = @"SELECT DISTINCT 
                //         MD.IDMORADOR, MD.NOME, MD.SOBRENOME, MD.TELEFONEFIXO, MD.CELULAR1, MD.CELULAR2, MD.Email1, MD.Email2 
                //        ,AP.IdApartamento, AP.NomeTorre AS BLOCO, AP.Apartamento
                //    FROM
                //        Apartamento AP
                //    INNER JOIN MORADOR MD ON AP.IdApartamento = MD.IdApartamento
                //    WHERE BLOCO = @BLOCO AND APARTAMENTO = @APARTAMENTO;";

                var query = @"SELECT IDCONDOMINIO, BLOCO, APTO, NOME, EMAIL, CELULAR FROM CONDOMINIO WHERE BLOCO = @BLOCO AND APTO = @APTO;";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    //cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BLOCO ", pBloco));
                    cmd.Parameters.AddWithValue("@BLOCO", pBloco);
                    cmd.Parameters.AddWithValue("@APTO", pApto);

                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var morador = PreencheMoradorPorAptoDtReader(reader); //PreencheMoradorDtReader(reader);
                        if (morador.Count > 0)
                            return morador;
                    }
                   
                    return new List<Morador>();
                }
            }
            catch (Exception ex)
            {
                string nomeArquivoDeLog = @"C:\AdCon\Log\ErroLog.txt";

                string diretorio = Path.GetDirectoryName(nomeArquivoDeLog);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllText(nomeArquivoDeLog, string.Concat(GetMorador(pBloco, pApto), " >>>> ", ex.Message, Environment.NewLine, ex.InnerException));
                return new List<Morador>();
            }
        }

        public static List<Morador> GetMorador(Morador pmorador, string pBloco, string pApartamento)
        {
 
            try
            {
                var query = @"SELECT IdMorador, Nome, Sobrenome, TelefoneFixo, Celular1, Celular2, Email1, Email2, MORADOR.IdApartamento
                        FROM Morador
                        INNER JOIN APARTAMENTO ON MORADOR.IdApartamento = APARTAMENTO.IdApartamento
                        WHERE 
                            upper(NOME) = upper(@NOME) 
                        AND upper(SOBRENOME) = upper(@SOBRENOME) 
                        AND upper(APARTAMENTO.NomeTorre) = upper(@BLOCO) 
                        AND APARTAMENTO.Apartamento = @APARTAMENTO";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@NOME", pmorador.NomeMorador);
                    cmd.Parameters.AddWithValue("@SOBRENOME", pmorador.SobreNomeMorador.Trim());
                    cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BLOCO ", pBloco));
                    cmd.Parameters.AddWithValue("@APARTAMENTO", pApartamento);

                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var morador = PreencheMoradorDtReader(reader);
                        if (morador.Count > 0)
                            return morador;
                    }
                    return new List<Morador>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Cria morador com dados basicos, como por exemplo: quando carregado dados de uma planilha
        /// </summary>
        /// <param name="morador"></param>
        /// <returns></returns>
        public static bool Add(Morador morador)
        {
            bool sucesso = false;
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    //var query = @"INSERT INTO Morador (Nome,  Sobrenome,  TelefoneFixo,  Celular1,  Celular2,  Email1,  Email2,  IdApartamento)
                    //                          VALUES (@Nome, @Sobrenome, @TelefoneFixo, @Celular1, @Celular2, @Email1, @Email2, @IdApartamento)";

                    var query = @"INSERT INTO CONDOMINIO (BLOCO,  APTO,  NOME,  EMAIL,  CELULAR)
                                                 VALUES (@BLOCO, @APTO, @NOME, @EMAIL, @CELULAR);";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BL-", morador.Bloco));
                    cmd.Parameters.AddWithValue("@APTO", morador.Apartamento);
                    cmd.Parameters.AddWithValue("@NOME", morador.NomeMorador);
                    cmd.Parameters.AddWithValue("@EMAIL", morador.Email1);
                    cmd.Parameters.AddWithValue("@CELULAR", morador.Celular1);

                    cmd.ExecuteNonQuery();

                    sucesso = true;
                }

                return sucesso;
            }
            catch (Exception ex)
            {
                MeuCondominio.Model.HelperModel.GravaLog(string.Concat("Erro métodp Add: ", ex.Message));
                return sucesso;
            }
        }

        /// <summary>
        /// Atualiza morador
        /// </summary>
        /// <param name="morador"></param>
        /// <returns></returns>
        public static bool AtualizaMorador(Morador morador)
        {
            #region
            //var query = @"UPDATE Morador
            //           SET Nome = @Nome,
            //               Sobrenome = @Sobrenome,
            //               TelefoneFixo = @TelefoneFixo,
            //               Celular1 = @Celular1,
            //               Celular2 = @Celular2,
            //               Email1 = @Email1,
            //               Email2 = @Email2,
            //               IdApartamento = @IdApartamento
            //         WHERE IdMorador = @IdMorador";
            #endregion

            var query = @"UPDATE CONDOMINIO SET 
                        BLOCO = @BLOCO,
                        APTO = @APTO,
                        NOME = @NOME,
                        EMAIL = @EMAIL,
                        CELULAR = @CELULAR 
                        WHERE IDCONDOMINIO = @IDCONDOMINIO;";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (morador.IdMorador > 0)
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BL-", morador.Bloco));
                        cmd.Parameters.AddWithValue("@APTO", morador.Apartamento);
                        cmd.Parameters.AddWithValue("@NOME", morador.NomeMorador);
                        cmd.Parameters.AddWithValue("@CELULAR", morador.Celular1);
                        cmd.Parameters.AddWithValue("@EMAIL", morador.Email1);

                        cmd.Parameters.AddWithValue("@IDCONDOMINIO", morador.IdMorador);

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                    return false;
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static bool UpdateTelefone(Morador morador)
        {
            //var query = @"UPDATE MORADOR
            //    SET Celular1 = @Celular1
            //    WHERE
            //    IdMorador=@IdMorador";

            var query = @"UPDATE CONDOMINIO SET CELULAR = @CELULAR WHERE IDCONDOMINIO = @IDCONDOMINIO;
";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (morador.IdMorador > 0)
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@IDCONDOMINIO", morador.IdMorador);
                        cmd.Parameters.AddWithValue("@CELULAR", morador.Celular1);

                        var x = cmd.ExecuteNonQuery();

                        return x == 1 ? true : false;
                    }
                    return false;
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region APARTAMENTO
        public static Apartamento GetApartamento(string pBloco, string pApartametno)
        {
            var query = @"SELECT IdApartamento, NomeTorre, Apartamento FROM Apartamento AP WHERE NOMETORRE = @BLOCO AND Apartamento = @APARTAMENTO";

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BLOCO ", pBloco));
                    cmd.Parameters.AddWithValue("@APARTAMENTO", pApartametno);

                    cmd.Connection = DbConnection();

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Apartamento apto = new Apartamento()
                            {
                                IdApartamento = rdr.GetInt32(0),
                                NomeTorre = rdr.GetString(1),
                                NumeroApartamento = rdr.GetString(2)
                            };
                            return apto;
                        }                        
                    }
                }
                    return new Apartamento();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }








        }

        public static Apartamento GetApartamentoFiltroExcel(string pBloco, string pApartametno)
        {
            var query = @"SELECT IdApartamento, NomeTorre, Apartamento
                            FROM Apartamento 
                            WHERE substring(NomeTorre,7,2) = @BLOCO
                            AND (('0' || Apartamento) = @APARTAMENTO OR Apartamento = @APARTAMENTO)";

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@BLOCO", pBloco);
                    cmd.Parameters.AddWithValue("@APARTAMENTO", pApartametno);

                    cmd.Connection = DbConnection();

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Apartamento apto = new Apartamento()
                            {
                                IdApartamento = rdr.GetInt32(0),
                                NomeTorre = rdr.GetString(1),
                                NumeroApartamento = rdr.GetString(2)
                            };
                            return apto;
                        }
                    }
                }
                return new Apartamento();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }








        }


        #endregion

        #region SEDEX

        /// <summary>
        /// Add Sedex
        /// </summary>
        /// <param name="sedex"></param>
        /// <returns></returns>
        public static bool Add(Sedex sedex)
        {

            //TODO: ARRUMAR ESTE INSERT
            bool sucesso = false;

            var query = @"INSERT INTO Sedex (IdMorador,  IdApartamento,  CodigoBarraEtiqueta,  CodigoQRCode,  CodigoBarraEtiquetaLocal,  LocalPrateleira,  DataCadastro,  DataEntrega,  DataEnvioMensagem,  Enviadosms,  EnviadoZap,  EnviadoTelegram,  EnviadoEmail)
                                    VALUES (@IdMorador, @IdApartamento, @CodigoBarraEtiqueta, @CodigoQRCode, @CodigoBarraEtiquetaLocal, @LocalPrateleira, @DataCadastro, @DataEntrega, @DataEnvioMensagem, @Enviadosms, @EnviadoZap, @EnviadoTelegram, @EnviadoEmail)";

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@IdMorador", sedex.IdMorador);
                    cmd.Parameters.AddWithValue("@IdApartamento", sedex.IdApartamento);
                    cmd.Parameters.AddWithValue("@CodigoBarraEtiqueta", sedex.CodigoBarraEtiqueta);
                    cmd.Parameters.AddWithValue("@CodigoQRCode", sedex.CodigoBarraEtiqueta);
                    cmd.Parameters.AddWithValue("@CodigoBarraEtiquetaLocal", sedex.CodigoBarraEtiquetaLocal);
                    cmd.Parameters.AddWithValue("@LocalPrateleira", sedex.LocalPrateleira);
                    cmd.Parameters.AddWithValue("@DataCadastro", sedex.DataCadastro);
                    cmd.Parameters.AddWithValue("@DataEntrega", sedex.DataEntrega);
                    cmd.Parameters.AddWithValue("@DataEnvioMensagem", sedex.DataEnvioMensagem);
                    cmd.Parameters.AddWithValue("@EnviadoSms", sedex.EnviadoPorSms);
                    cmd.Parameters.AddWithValue("@EnviadoZap", sedex.EnviadoPorWhatsApp);
                    cmd.Parameters.AddWithValue("@EnviadoTelegram", sedex.EnviadoPorTelegram);
                    cmd.Parameters.AddWithValue("@EnviadoEmail", sedex.EnviadoPorEmail1);


                    //cmd.Parameters.AddWithValue("@ReciboImpresso", sedex.ReciboImpresso);

                    cmd.ExecuteNonQuery();

                    sucesso = true;
                }

                return sucesso;
            }
            catch (Exception ex)
            {
                MeuCondominio.Model.HelperModel.GravaLog(string.Concat("Erro métodp Add: ", ex.Message));
                return sucesso;
            }
        }

        public static Morador GetSedex(int idMorador)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Sedex Where IdMorador=" + idMorador;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);

                    var morador = PreencheMoradorDt(dt);
                    if (morador.Count > 0)
                        return morador[0];
                    else
                        return new Morador();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Morador GetSedex(string CodigoBarras)
        {
            try
            {
                var query = @"SELECT * FROM Sedex Where CodigoBarraEtiqueta=@CodigoBarraEtiqueta AND Enviadosms = 'N'";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@CodigoBarraEtiqueta", CodigoBarras);
                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var morador = PreencheMoradorDtReader(reader);
                        if (morador.Count > 0)
                            return morador[0];
                    }
                    return new Morador();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Morador> GetSedex()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Sedex";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return PreencheMoradorDt(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Morador> GetSedex(string pBloco, string pApto, string pNomeMorador)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM SEDEX WHERE BLOCO = '" + pBloco + "' AND APARTAMENTO = '" + pApto + "'  AND NomeMorador = '" + pNomeMorador + "' AND CODIGOBARRAETIQUETA IS NULL AND CODIGOQRCODE IS NULL AND CODIGOBARRAETIQUETALOCAL IS NULL AND LOCALPRATELEIRA = 0";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return PreencheMoradorDt(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Morador> GetSedex(Morador pmorador)
        {
            //TODO: VER ERRO AQUI
            try
            {
                var query = @"SELECT IDMORADOR, BLOCO, APARTAMENTO, NomeMorador, Email1, Celular1, CODIGOBARRAETIQUETA, CODIGOQRCODE, CODIGOBARRAETIQUETALOCAL, 
                        LOCALPRATELEIRA, DATACADASTRO, DATAENTREGA, DATAENVIOMENSAGEM, ENVIADOPORSMS, ENVIADOPOREmail1, ENVIADOPORTELEGRAM, ENVIADOPORZAP, RECIBOIMPRESSO
                    FROM SEDEX 
                    WHERE 
                        1 = 1    
                     AND BLOCO 						= @BLOCO
                    AND APARTAMENTO 				= @APARTAMENTO
                    AND NomeMorador 			= @NomeMorador
                    AND Email1 						= @Email1
                    AND Celular1 				= @Celular1
                    AND CODIGOBARRAETIQUETA 		= @CODIGOBARRAETIQUETA
                    AND LOCALPRATELEIRA 			= @LOCALPRATELEIRA
                    AND RECIBOIMPRESSO 				= @RECIBOIMPRESSO
                    ";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@BLOCO", pmorador.Bloco);
                    cmd.Parameters.AddWithValue("@APARTAMENTO", pmorador.Apartamento);
                    cmd.Parameters.AddWithValue("@NomeMorador", pmorador.NomeMorador);
                    cmd.Parameters.AddWithValue("@Email1", pmorador.Email1);
                    cmd.Parameters.AddWithValue("@Celular1", pmorador.Celular1);

                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var morador = PreencheMoradorDtReader(reader);
                        if (morador.Count > 0)
                            return morador;
                    }
                    return new List<Morador>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<SmsEnvio> GetSedexParaEnvioSms()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                #region
                //var query = @"SELECT IdSedex, Sedex.IdMorador, Sedex.IdApartamento, ap.NomeTorre as Bloco, ap.Apartamento, Morador.Nome, Morador.Sobrenome, Morador.Email1, Morador.Celular1, CodigoBarraEtiqueta, CodigoQRCode, CodigoBarraEtiquetaLocal, LocalPrateleira, DataCadastro,
                //                   DataEntrega, DataEnvioMensagem, Enviadosms, EnviadoZap, EnviadoTelegram, EnviadoEmail, EncomendaAlimento, EncomendaMedicamento
                //              FROM 
                //              Sedex 
                //              inner join Morador on Sedex.IdMorador = Morador.IdMorador
                //              inner join Apartamento ap on Morador.IdApartamento = ap.IdApartamento 
                //            WHERE CODIGOBARRAETIQUETA IS NOT NULL
                //            AND Enviadosms IS NULL
                //            AND DATAENVIOMENSAGEM IS NULL";
                #endregion

                var query = @"SELECT 
                        IdSedex, Sedex.IdMorador, Sedex.IdApartamento, CONDOMINIO.BLOCO, CONDOMINIO.APTO AS Apartamento, CONDOMINIO.Nome, NULL AS Sobrenome, CONDOMINIO.EMAIL AS Email1, CONDOMINIO.CELULAR AS Celular1, CodigoBarraEtiqueta, CodigoQRCode, CodigoBarraEtiquetaLocal, LocalPrateleira, DataCadastro,
                           DataEntrega, DataEnvioMensagem, Enviadosms, EnviadoZap, EnviadoTelegram, EnviadoEmail, EncomendaAlimento, EncomendaMedicamento
                        FROM 
                        Sedex 
                        INNER JOIN CONDOMINIO ON SEDEX.IdMorador = CONDOMINIO.IDCONDOMINIO
                        WHERE CODIGOBARRAETIQUETA IS NOT NULL
                        AND EnviadoTelegram IS NULL
                        AND Enviadosms IS NULL
                        AND EnviadoEmail IS NULL";


                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return PreencheMoradorParaEnvioMensagem(dt);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Morador> GetSedexParaAssinatura(string pDataInicio="", string pDataFim="")
        {
            DataTable dt = new DataTable();

            var query = @"SELECT distinct MORA.IdMorador, ap.NomeTorre AS Bloco, ap.Apartamento, MORA.Nome AS NomeMorador, SED.CodigoBarraEtiqueta AS Sobrenome, MORA.Email1, MORA.Celular1, SED.DATAENVIO  
                    FROM SedexHistorico SED
                      inner join Morador MORA on SED.IdMorador = MORA.IdMorador
                      inner join Apartamento ap on MORA.IdApartamento = ap.IdApartamento 
                    WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(pDataInicio) && (!string.IsNullOrEmpty(pDataFim)))
                query += @" AND sed.dataenvio BETWEEN @DTINICIO AND @DTFIM";
            else
                query += @" AND ReciboImpresso = 'N' ";


            query += @" GROUP BY
                       ap.NomeTorre, ap.Apartamento, MORA.Nome, MORA.Email1, MORA.Celular1, SED.CodigoBarraEtiqueta 
                   ORDER BY 
                   ap.NomeTorre, AP.Apartamento;";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = query;

                    if (!string.IsNullOrEmpty(pDataInicio) && (!string.IsNullOrEmpty(pDataFim)))
                    {
                        cmd.Parameters.AddWithValue("@DTINICIO", pDataInicio);
                        cmd.Parameters.AddWithValue("@DTFIM", pDataFim);
                    }

                    cmd.Parameters.AddWithValue("@DTINICIO", pDataInicio);
                    cmd.Parameters.AddWithValue("@DTFIM", pDataFim);

                    SQLiteDataReader dr = cmd.ExecuteReader();

                    List<Morador> moradores = new List<Morador>();

                    while(dr.Read())
                    {
                        Morador morador = new Morador();

                        morador.IdMorador = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                        morador.Bloco = dr.IsDBNull(1) ? "" : dr.GetString(1);
                        morador.Apartamento = dr.IsDBNull(2) ? "" : dr.GetString(2);
                        morador.NomeMorador = dr.IsDBNull(3) ? "" : dr.GetString(3);
                        morador.SobreNomeMorador = dr.IsDBNull(4) ? "" : dr.GetString(4);
                        morador.Email1 = dr.IsDBNull(5) ? "" : dr.GetString(5);
                        morador.Celular1 = dr.IsDBNull(6) ? "" : dr.GetString(6);
                        moradores.Add(morador);
                    }
                    return moradores;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Delete(int Id)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = @"DELETE FROM CONDOMINIO WHERE IDCONDOMINIO = @IDCONDOMINIO;";
                    cmd.Parameters.AddWithValue("@IDCONDOMINIO", Id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool RegistraEnvioMensagem(int IdSedex)
        {
            var rowsAffected = 0;
            try
            {
                var query = @"UPDATE SEDEX SET Enviadosms = 'S', DATAENVIOMENSAGEM = DATETIME('NOW') WHERE IdSedex = @IdSedex";
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdSedex", IdSedex);
                    rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool RegistraEnvioTelegram(int IdSedex)
        {
            var rowsAffected = 0;
            try
            {
                var query = @"UPDATE SEDEX SET EnviadoTelegram = 'S', DATAENVIOMENSAGEM = DATETIME('NOW') WHERE IdSedex = @IdSedex";
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdSedex", IdSedex);
                    rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Registra envio de e-mail
        /// </summary>
        /// <param name="IdSedex"></param>
        /// <returns></returns>
        public static bool RegistraEnvioEmail(int IdSedex)
        {
            var rowsAffected = 0;
            try
            {
                var query = @"UPDATE SEDEX SET EnviadoEmail = 'S', DATAENVIOMENSAGEM = DATETIME('NOW') WHERE IdSedex = @IdSedex";

                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdSedex", IdSedex);
                    rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region HISTORICOSEDEX
        public static bool AddHistorico(Morador morador, Sedex sedex)
        {
            bool sucesso = false;
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    var query = @"INSERT INTO SedexHistorico (NomeTorre, Apartamento, IdMorador, NomeMorador, SobreNome, NumeroEnviado, EmailEnviado, DataEnvio)
                    VALUES (@NomeTorre, @Apartamento, @IdMorador, @NomeMorador, @SobreNome, @NumeroEnviado, @EmailEnviado, @DataEnvio)";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@NomeTorre", morador.Bloco);
                    cmd.Parameters.AddWithValue("@Apartamento", morador.Apartamento);
                    cmd.Parameters.AddWithValue("@IdMorador", sedex.IdMorador);
                    cmd.Parameters.AddWithValue("@NomeMorador", morador.NomeMorador);
                    cmd.Parameters.AddWithValue("@SobreNome", morador.SobreNomeMorador);
                    cmd.Parameters.AddWithValue("@NumeroEnviado", morador.Celular1);
                    cmd.Parameters.AddWithValue("@EmailEnviado", morador.Email1);
                    cmd.Parameters.AddWithValue("@DataEnvio", sedex.DataEnvioMensagem);

                    cmd.ExecuteNonQuery();

                    sucesso = true;
                }

                return sucesso;
            }
            catch (Exception ex)
            {
                MeuCondominio.Model.HelperModel.GravaLog(string.Concat("Erro métodp Add: ", ex.Message));
                return sucesso;
            }
        }

        public static bool EnviaSedexParaHistorico()
        {
            int resultado = 0;

            var query = @"INSERT INTO SedexHistorico (IdSedex, NomeTorre, Apartamento, IdMorador, NomeMorador, SobreNome, NumeroEnviado, EmailEnviado, DataEnvio,CodigoBarraEtiqueta)
                        SELECT IdSedex, CONDOMINIO.BLOCO, CONDOMINIO.APTO, CONDOMINIO.IDCONDOMINIO, CONDOMINIO.NOME, NULL AS Sobrenome, CONDOMINIO.CELULAR, CONDOMINIO.EMAIL, DATETIME('now'), CodigoBarraEtiqueta
                        FROM 
                        Sedex 
                        INNER JOIN CONDOMINIO ON SEDEX.IdMorador = CONDOMINIO.IDCONDOMINIO
                        WHERE 1 = 1
                        AND CODIGOBARRAETIQUETA IS NOT NULL
                        AND EnviadoTelegram IS NOT NULL
                        OR Enviadosms IS NOT NULL
                        OR EnviadoEmail IS NOT NULL
                        AND DATAENVIOMENSAGEM IS NOT NULL";

            try
            {

                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;

                    resultado = cmd.ExecuteNonQuery();

                }
                if (resultado > 0)
                {
                    var query2 = @"DELETE FROM SEDEX WHERE 1 = 1 AND CODIGOBARRAETIQUETA IS NOT NULL AND EnviadoTelegram IS NOT NULL OR Enviadosms IS NOT NULL OR EnviadoEmail IS NOT NULL AND DATAENVIOMENSAGEM IS NOT NULL";

                    try
                    {

                        using (var cmd = new SQLiteCommand(DbConnection()))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = query2;

                            resultado = cmd.ExecuteNonQuery();

                        }
                        if (resultado > 0)
                            return true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                    return false;

                if (resultado > 0) return true; else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Morador> GetHistoricoPorApartamento(string pBloco, string pApartamento)
        {
            List<Morador> HistoricoSedex;

            //var query = @"Select IDMORADOR, BLOCO, APARTAMENTO, NomeMorador, Email1, Celular1, CODIGOBARRAETIQUETA, CODIGOQRCODE, CODIGOBARRAETIQUETA, LOCALPRATELEIRA,
            //    DATACADASTRO, DATAENTREGA, DATAENVIOMENSAGEM, ENVIADOPORSMS, ENVIADOPORZAP, ENVIADOPORTELEGRAM, ENVIADOPOREmail1, RECIBOIMPRESSO
            //    FROM SEDEX
            //    WHERE
            //        1 = 1
            //    AND BLOCO = @BLOCO
            //    AND APARTAMENTO = @APARTAMENTO
            //    AND ENVIADOPORSMS = 'S'
            //    AND DATAENVIOMENSAGEM IS NOT NULL";

            var query = @"SELECT H.NomeTorre as Bloco, H.Apartamento, H.NomeMorador, H.SobreNome, H.NumeroEnviado, H.EmailEnviado, H.DataEnvio 
                FROM 
                    SedexHistorico H
                WHERE H.IdMorador = 1;";

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@BLOCO", pBloco);
                    cmd.Parameters.AddWithValue("@APARTAMENTO", pApartamento);
                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        HistoricoSedex = PreencheMoradorDtReader(reader);
                        if (HistoricoSedex.Count > 0)
                            return HistoricoSedex;
                    }
                    return new List<Morador>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// NOVO
        /// </summary>
        /// <param name="pBloco"></param>
        /// <param name="pApartamento"></param>
        /// <returns></returns>
        public static List<SedexHistorico> GetHistoricoPorMorador(int IdMorador)
        {
            List<SedexHistorico> HistoricoSedex;

            var query = @"SELECT H.NomeTorre as Bloco, H.Apartamento, H.NomeMorador, H.EmailEnviado, H.NumeroEnviado, H.DataEnvio, H.CodigoBarraEtiqueta 
                FROM 
                    SedexHistorico H
                WHERE H.ReciboImpresso = 'S' AND H.IdMorador = @IdMorador;";

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdMorador", IdMorador);

                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        HistoricoSedex = PreencheHistoricoMorador(reader);
                        if (HistoricoSedex.Count > 0)
                            return HistoricoSedex;
                    }
                    return new List<SedexHistorico>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void LimpaHistorico()
        {
            var query = @"DELETE FROM SedexHistorico WHERE IdSedexHistorico IN
                (SELECT IdSedexHistorico FROM SedexHistorico WHERE DATETIME(DataEnvio) < date('now', 'start of month', '-1 month', '0 day'));";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = query;

                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region RECIBO
        public static bool AtualizaReciboImpresso()
        {
           var query = @"UPDATE SedexHistorico SET ReciboImpresso = 'S' WHERE ReciboImpresso = 'N';";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = query;
                    
                    cmd.ExecuteNonQuery();

                    return true;
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PREENCHEOBJETOS
        private static List<Morador> PreencheMoradorDt(DataTable data)
        {
            List<Morador> moradores = new List<Morador>();

            foreach (DataRow row in data.Rows)
            {
                Morador morador = new Morador();

                morador.IdMorador = int.Parse(row.ItemArray[0].ToString());
                morador.Bloco = row.ItemArray[1].ToString();
                morador.Apartamento = row.ItemArray[2].ToString();
                morador.NomeMorador = row.ItemArray[3].ToString();
                morador.SobreNomeMorador = row.ItemArray[4].ToString();
                morador.Email1 = row.ItemArray[5].ToString();
                morador.Celular1 = row.ItemArray[6].ToString();
                moradores.Add(morador);
            }
            return moradores;
        }

        private static List<Apartamento> PreencheApartamentoReader(SQLiteDataReader reader)
        {
            List<Apartamento> apartamentos = new List<Apartamento>();

            while (reader.Read())
            {
                Apartamento ap = new Apartamento()
                {
                    IdApartamento = int.Parse(reader.IsDBNull(0) ? "" : reader.GetString(0)),
                    NomeTorre = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    NumeroApartamento = reader.IsDBNull(2) ? "" : reader.GetString(2)
                };
                apartamentos.Add(ap);
            }
            return apartamentos;
        }

        private static List<Apartamento> PreencheApartamentoDt(DataTable data)
        {
            List<Apartamento> apartamentos = new List<Apartamento>();

            foreach (DataRow row in data.Rows)
            {
                Apartamento ap = new Apartamento()
                {
                    IdApartamento = int.Parse(row.ItemArray[0].ToString()),
                    NomeTorre = row.ItemArray[1].ToString(),
                    NumeroApartamento = row.ItemArray[2].ToString()
                };

                apartamentos.Add(ap);
            }
            return apartamentos;
        }
        private static List<SedexHistorico> PreencheHistoricoMorador(SQLiteDataReader reader)
        {
            List<SedexHistorico> historico = new List<SedexHistorico>();

            while (reader.Read())
            {
                SedexHistorico historicoMorador = new SedexHistorico
                {
                    NomeTorre = reader.IsDBNull(0) ? "" : reader.GetString(0).Replace("BLOCO",""),
                    Apartamento = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    NomeMorador = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Email1Enviado = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    NumeroEnviado = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    DataEnvio = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    CodigoBarraEtiqueta = reader.IsDBNull(6) ? "" : reader.GetString(6)
                };

                historico.Add(historicoMorador);
            }
            return historico;
        }

        /// <summary>
        /// Preenche objeto Morador para carregar tela pos selecionar bloco e apartamento
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Morador> PreencheMoradorDtReader(SQLiteDataReader reader)
        {
            List<Morador> moradores = new List<Morador>();

            while (reader.Read())
            {
                Morador morador = new Morador
                {
                    IdMorador = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    NomeMorador = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    SobreNomeMorador = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    TelefoneFixo = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Celular1 = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Celular2 = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    Email1 = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    Email2 = reader.IsDBNull(7) ? "" : reader.GetString(7),
                    IdApartamento = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                    Bloco = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Apartamento = reader.IsDBNull(5) ? "" : reader.GetString(5)
                    

                };

                moradores.Add(morador);
            }
            return moradores;
        }

        /// <summary>
        /// Preenche objeto Morador para carregar tela pos selecionar bloco e apartamento
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Morador> PreencheMoradorPorAptoDtReader(SQLiteDataReader reader)
        {
            List<Morador> moradores = new List<Morador>();

            while (reader.Read())
            {
                Morador morador = new Morador
                {
                    IdMorador = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    Bloco = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Apartamento = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    NomeMorador = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Email1 = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Celular1 = reader.IsDBNull(5) ? "" : reader.GetString(5)
                };

                moradores.Add(morador);
            }
            return moradores;
        }




        /// <summary>
        /// Preenche Moradores ao consultar com bloco apartamento e nome
        /// Ban: bloco, apartamento, nome 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Morador> PreencheMoradorBanReader(SQLiteDataReader reader)
        {
            List<Morador> moradores = new List<Morador>();

            while (reader.Read())
            {
                Morador morador = new Morador
                {
                    IdApartamento = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    Bloco = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Apartamento = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    IdMorador = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                    NomeMorador = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    SobreNomeMorador = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    TelefoneFixo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    Celular1 = reader.IsDBNull(7) ? "" : reader.GetString(7),
                    Celular2 = reader.IsDBNull(8) ? "" : reader.GetString(8),
                    Email1 = reader.IsDBNull(9) ? "" : reader.GetString(9),
                    Email2 = reader.IsDBNull(10) ? "" : reader.GetString(10)
                };

                moradores.Add(morador);
            }
            return moradores;
        }

        private static List<SmsEnvio> PreencheMoradorParaEnvioMensagem(DataTable data)
        {
            List<SmsEnvio> enviar = new List<SmsEnvio>();

            foreach (DataRow row in data.Rows)
            {
                SmsEnvio envio = new SmsEnvio
                {
                    NomeMorador = row.ItemArray[5].ToString(),
                    SobreNomeMorador = row.ItemArray[6].ToString(),
                    Celular1 = row.ItemArray[8].ToString(),
                    Email1 = row.ItemArray[7].ToString(),
                    ChaveSedex = int.Parse(row.ItemArray[0].ToString()),
                    CodigoBarras = row.ItemArray[9].ToString()
                };

                enviar.Add(envio);
            }
            return enviar;
        }

        #endregion
    }
}
