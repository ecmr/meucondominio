using System;
using System.Collections.Generic;
using System.Data;
using MeuCondominio.Model;
using System.Data.SQLite;

namespace MeuCondominio.Model
{
    public class DalHelper
    {
        private static SQLiteConnection sqliteConnection;
        private static string stBanco = @"C:\Sedex Condominio\dados\Cadastro.sqlite3";

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



        #region MORADOR
        public static Morador GetMorador(string pBloco, string pApto, string NomeCompleto)
        {
            var NomeMorador = NomeCompleto.Split(' ');

            try
            {
                var query = @"SELECT DISTINCT 
                         AP.IdApartamento, AP.NomeTorre AS BLOCO, AP.Apartamento
                        ,MD.IDMORADOR, MD.NOME, MD.SOBRENOME, MD.TELEFONEFIXO, MD.CELULAR1, MD.CELULAR2, MD.EMAIL1, MD.EMAIL2
                    FROM
                        Apartamento AP
                    INNER JOIN MORADOR MD ON AP.IdApartamento = MD.IdApartamento
                    WHERE BLOCO = @BLOCO AND APARTAMENTO = @APARTAMENTO AND MD.NOME = @NOME;";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BLOCO ", pBloco));
                    cmd.Parameters.AddWithValue("@APARTAMENTO", pApto);
                    cmd.Parameters.AddWithValue("@NOME", NomeMorador[0]);
                    //cmd.Parameters.AddWithValue("@SOBRENOME", NomeMorador[1]);

                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var morador = PreencheMoradorDtReader(reader)[0];
                        if (morador.IdMorador > 0)
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
                var query = @"SELECT DISTINCT 
                         AP.IdApartamento, AP.NomeTorre AS BLOCO, AP.Apartamento
                        ,MD.IDMORADOR, MD.NOME, MD.SOBRENOME, MD.TELEFONEFIXO, MD.CELULAR1, MD.CELULAR2, MD.Email1, MD.Email2
                    FROM
                        Apartamento AP
                    INNER JOIN MORADOR MD ON AP.IdApartamento = MD.IdApartamento
                    WHERE BLOCO = @BLOCO AND APARTAMENTO = @APARTAMENTO;";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BLOCO ", pBloco));
                    cmd.Parameters.AddWithValue("@APARTAMENTO", pApto);

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
                    cmd.Parameters.AddWithValue("@SOBRENOME", pmorador.SobreNomeMorador);
                    cmd.Parameters.AddWithValue("@BLOCO", pBloco);
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
                    var query = @"INSERT INTO Morador (Nome,  Sobrenome,  TelefoneFixo,  Celular1,  Celular2,  Email1,  Email2,  IdApartamento)
                                              VALUES (@Nome, @Sobrenome, @TelefoneFixo, @Celular1, @Celular2, @Email1, @Email2, @IdApartamento)";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Nome", morador.NomeMorador);
                    cmd.Parameters.AddWithValue("@Sobrenome", morador.SobreNomeMorador);
                    cmd.Parameters.AddWithValue("@TelefoneFixo", morador.TelefoneFixo);
                    cmd.Parameters.AddWithValue("@Celular1", morador.Celular1);
                    cmd.Parameters.AddWithValue("@Celular2", morador.Celular2);
                    cmd.Parameters.AddWithValue("@Email1", morador.Email1);
                    cmd.Parameters.AddWithValue("@Email2", morador.Email2);
                    cmd.Parameters.AddWithValue("@IdApartamento", morador.IdApartamento);

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
            var query = @"UPDATE Morador
                       SET Nome = @Nome,
                           Sobrenome = @Sobrenome,
                           TelefoneFixo = @TelefoneFixo,
                           Celular1 = @Celular1,
                           Celular2 = @Celular2,
                           Email1 = @Email1,
                           Email2 = @Email2,
                           IdApartamento = @IdApartamento
                     WHERE IdMorador = @IdMorador";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (morador.IdMorador > 0)
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@IdMorador", morador.IdMorador);
                        cmd.Parameters.AddWithValue("@Nome", morador.NomeMorador);
                        cmd.Parameters.AddWithValue("@Sobrenome", morador.SobreNomeMorador);
                        cmd.Parameters.AddWithValue("@NomeMorador", morador.NomeMorador);
                        cmd.Parameters.AddWithValue("@TelefoneFixo", morador.TelefoneFixo);
                        cmd.Parameters.AddWithValue("@Celular1", morador.Celular1);
                        cmd.Parameters.AddWithValue("@Celular2", morador.Celular2);
                        cmd.Parameters.AddWithValue("@Email1", morador.Email1);
                        cmd.Parameters.AddWithValue("@Email2", morador.Email2);
                        cmd.Parameters.AddWithValue("@IdApartamento", morador.IdApartamento);

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
            var query = @"UPDATE MORADOR
                SET Celular1 = @Celular1
                WHERE
                IdMorador=@IdMorador";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (morador.IdMorador > 0)
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@IdMorador", morador.IdMorador);
                        cmd.Parameters.AddWithValue("@Celular1", morador.Celular1);

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
            DataTable dt = new DataTable();

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
                    return morador[0];
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
                var query = @"SELECT IdSedex, Sedex.IdMorador, Sedex.IdApartamento, ap.NomeTorre as Bloco, ap.Apartamento, Morador.Nome, Morador.Sobrenome, Morador.Email1, Morador.Celular1, CodigoBarraEtiqueta, CodigoQRCode, CodigoBarraEtiquetaLocal, LocalPrateleira, DataCadastro,
                                   DataEntrega, DataEnvioMensagem, Enviadosms, EnviadoZap, EnviadoTelegram, EnviadoEmail, EncomendaAlimento, EncomendaMedicamento
                              FROM 
                              Sedex 
                              inner join Morador on Sedex.IdMorador = Morador.IdMorador
                              inner join Apartamento ap on Morador.IdApartamento = ap.IdApartamento 
                            WHERE CODIGOBARRAETIQUETA IS NOT NULL
                            AND Enviadosms IS NULL
                            AND DATAENVIOMENSAGEM IS NULL";

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

        public static List<Morador> GetSedexParaAssinatura()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            var query = @"SELECT distinct MORA.IdMorador, ap.NomeTorre AS Bloco, ap.Apartamento, MORA.Nome AS NomeMorador, SED.CodigoBarraEtiqueta AS Sobrenome, MORA.Email1, MORA.Celular1, SED.DATAENVIO  
                    FROM SedexHistorico SED
                      inner join Morador MORA on SED.IdMorador = MORA.IdMorador
                      inner join Apartamento ap on MORA.IdApartamento = ap.IdApartamento 
                    WHERE 1 = 1
                    AND ReciboImpresso = 'N'
                    GROUP BY
                        ap.NomeTorre, ap.Apartamento, MORA.Nome, MORA.Email1, MORA.Celular1, SED.CodigoBarraEtiqueta 
                    ORDER BY 
                    ap.NomeTorre, AP.Apartamento;";

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;
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

        public static bool Delete(int Id)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "DELETE FROM Sedex Where IdMorador=@Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
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
                SELECT IdSedex, ap.NomeTorre, ap.Apartamento, Morador.IdMorador, Morador.Nome, Morador.Sobrenome, Morador.Celular1, Morador.Email1, DATETIME('now'), CodigoBarraEtiqueta
                FROM 
                Sedex 
                inner join Morador on Sedex.IdMorador = Morador.IdMorador
                inner join Apartamento ap on Morador.IdApartamento = ap.IdApartamento 
                WHERE 1 = 1
                AND CODIGOBARRAETIQUETA IS NOT NULL
                AND Enviadosms IS NOT NULL
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
                    var query2 = @"DELETE FROM SEDEX WHERE 1 = 1 AND CODIGOBARRAETIQUETA IS NOT NULL AND Enviadosms IS NOT NULL AND DATAENVIOMENSAGEM IS NOT NULL";

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

            var query = @"SELECT H.NomeTorre as Bloco, H.Apartamento, NomeMorador || SobreNome as NomeMorador, H.NumeroEnviado, H.EmailEnviado, H.DataEnvio, H.CodigoBarraEtiqueta 
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
                //morador.CodigoBarraEtiqueta = row.ItemArray[6].ToString();
                //morador.CodigoQRCode = row.ItemArray[7].ToString();
                //morador.CodigoBarraEtiquetaLocal = row.ItemArray[8].ToString();
                //morador.LocalPrateleira = int.Parse(row.ItemArray[9].ToString());
                //morador.DataCadastro = row.ItemArray[10].ToString();
                //morador.DataEntrega = row.ItemArray[11].ToString();
                //morador.DataEnvioMensagem = row.ItemArray[12].ToString();
                //morador.EnviadoPorSMS = row.ItemArray[13].ToString();
                //morador.EnviadoPorZAP = row.ItemArray[14].ToString();
                //morador.EnviadoPorTELEGRAM = row.ItemArray[15].ToString();
                //morador.EnviadoPorEmail1 = row.ItemArray[16].ToString();
                //morador.ReciboImpresso = row.ItemArray[17].ToString();

                moradores.Add(morador);
            }
            return moradores;
        }
        //PreencheMoradorParaEnvioMensagem

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
                    NomeTorre = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    Apartamento = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    NomeMorador = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    NumeroEnviado = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Email1Enviado = reader.IsDBNull(4) ? "" : reader.GetString(4),
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
                    ChaveSedex = int.Parse(row.ItemArray[0].ToString())
                };

                enviar.Add(envio);
            }
            return enviar;
        }

        #endregion
    }
}
