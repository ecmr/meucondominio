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



        #region Consulta do combo bloco/apto e preenchimento de objetos

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

        public static List<Morador> GetMoradores(string pBloco, string pApto)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
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
                    //cmd.CommandText = "SELECT * FROM SEDEX WHERE BLOCO = '" + pBloco + "' AND APARTAMENTO = '" + pApto + "' AND CODIGOBARRAETIQUETA IS NULL AND CODIGOQRCODE IS NULL AND CODIGOBARRAETIQUETALOCAL IS NULL AND LOCALPRATELEIRA = 0";
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@BLOCO", string.Concat("BLOCO ", pBloco));
                    cmd.Parameters.AddWithValue("@APARTAMENTO", pApto);

                    //da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    //da.Fill(dt);
                    //return PreencheMoradorDt(dt);

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
        /// NOVO
        /// </summary>
        /// <param name="pBloco"></param>
        /// <param name="pApartamento"></param>
        /// <returns></returns>
        public static List<SedexHistorico> GetHistoricoPorMorador(int IdMorador)
        {
            List<SedexHistorico> HistoricoSedex;

            var query = @"SELECT H.NomeTorre as Bloco, H.Apartamento, NomeMorador || SobreNome as NomeMorador, H.NumeroEnviado, H.EmailEnviado, H.DataEnvio 
                FROM 
                    SedexHistorico H
                WHERE H.IdMorador = @IdMorador;";

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
                    DataEnvio = reader.IsDBNull(5) ? "" : reader.GetString(5)
                };

                historico.Add(historicoMorador);
            }
            return historico;
        }
        #endregion







        public static Morador GetCliente(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Sedex Where IdMorador=" + id;
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

        public static Morador GetCliente(string CodigoBarras)
        {
            try
            {
                var query = @"SELECT * FROM Sedex Where CodigoBarraEtiqueta=@CodigoBarraEtiqueta AND ReciboImpresso = 'N'";

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

        public static List<Morador> GetClientes()
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



        public static List<Morador> GetClientes(string pBloco, string pApto, string pNomeMorador)
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

        public static List<Morador> GetClientes(Morador pmorador)
        {
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
                    //cmd.Parameters.AddWithValue("@CODIGOBARRAETIQUETA", pmorador.CodigoBarraEtiqueta);
                    //cmd.Parameters.AddWithValue("@LOCALPRATELEIRA", pmorador.LocalPrateleira);
                    //cmd.Parameters.AddWithValue("@RECIBOIMPRESSO", pmorador.ReciboImpresso);

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

        public static List<Morador> GetSedexParaEnvioSms()
        {
            try
            {
                List<Morador> listaMoradores;
                var query = @"SELECT IdMorador, Bloco, Apartamento, NomeMorador, Email1, Celular1, 
                    CodigoBarraEtiqueta, CodigoBarraEtiquetaLocal, CodigoQRCode, 
                    LocalPrateleira, DataCadastro, DataEnvioMensagem, DataEntrega, 
                    EnviadoPorEmail1, EnviadoPorSMS, EnviadoPorTELEGRAM, EnviadoPorZAP, ReciboImpresso 
                    FROM SEDEX 
                    WHERE
                    CODIGOBARRAETIQUETA IS NOT NULL
                    AND ENVIADOPORSMS IS NULL
                    AND DATAENVIOMENSAGEM IS NULL";

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = DbConnection();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaMoradores = PreencheMoradorDtReader(reader);
                        if (listaMoradores.Count > 0)
                            return listaMoradores;
                    }
                    return new List<Morador>();
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

            var query = @"SELECT distinct IdMorador, Bloco, Apartamento, NomeMorador, Email1, Celular1, CodigoBarraEtiqueta, CodigoQRCode, CodigoBarraEtiquetaLocal, LocalPrateleira, DataCadastro, 
                            DataEntrega, DataEnvioMensagem, EnviadoPorSMS, EnviadoPorEmail1, EnviadoPorZAP, EnviadoPorTELEGRAM, ReciboImpresso
                            FROM SEDEX 
                            WHERE 
                            DATACADASTRO IS NOT NULL 
                            AND DATAENVIOMENSAGEM IS NOT NULL 
                            AND EnviadoPorSMS = 'S'
                            AND ReciboImpresso = 'N'
                            GROUP BY
                                Bloco, Apartamento, NomeMorador, Email1, Celular1, CodigoBarraEtiqueta, CodigoQRCode, CodigoBarraEtiquetaLocal, LocalPrateleira, DataCadastro, 
                                DataEntrega, DataEnvioMensagem, EnviadoPorSMS, EnviadoPorEmail1, EnviadoPorZAP, EnviadoPorTELEGRAM, ReciboImpresso
                            ORDER BY 
                            Bloco, Apartamento;";

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

        public static bool Add(Morador morador)
        {
            bool sucesso = false;
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    var query = @"INSERT INTO Sedex(IdMorador, Bloco , Apartamento , NomeMorador , Email1 , Celular1 , CodigoBarraEtiqueta , CodigoBarraEtiquetaLocal , LocalPrateleira, DataCadastro, DataEntrega, DataEnvioMensagem, EnviadoPorSMS, EnviadoPorZAP, EnviadoPorTELEGRAM, EnviadoPorEmail1, ReciboImpresso)
                                    values ((SELECT MAX(IDMORADOR)+1 FROM SEDEX), @Bloco , @Apartamento , @NomeMorador , @Email1 , @Celular1 , @CodigoBarraEtiqueta , @CodigoBarraEtiquetaLocal , @LocalPrateleira, @DataCadastro, @DataEntrega, @DataEnvioMensagem, @EnviadoPorSMS, @EnviadoPorZAP, @EnviadoPorTELEGRAM, @EnviadoPorEmail1, @ReciboImpresso)";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Bloco", morador.Bloco);
                    cmd.Parameters.AddWithValue("@Apartamento", morador.Apartamento);
                    cmd.Parameters.AddWithValue("@NomeMorador", morador.NomeMorador);
                    cmd.Parameters.AddWithValue("@Email1", morador.Email1);
                    cmd.Parameters.AddWithValue("@Celular1", morador.Celular1);
                    //cmd.Parameters.AddWithValue("@CodigoBarraEtiqueta", morador.CodigoBarraEtiqueta);
                    //cmd.Parameters.AddWithValue("@CodigoBarraEtiquetaLocal", morador.CodigoBarraEtiquetaLocal);
                    //cmd.Parameters.AddWithValue("@LocalPrateleira", morador.LocalPrateleira);
                    //cmd.Parameters.AddWithValue("@DataCadastro", morador.DataCadastro);
                    //cmd.Parameters.AddWithValue("@DataEntrega", morador.DataEntrega);
                    //cmd.Parameters.AddWithValue("@DataEnvioMensagem", morador.DataEnvioMensagem);
                    //cmd.Parameters.AddWithValue("@EnviadoPorSMS", morador.EnviadoPorSMS);
                    //cmd.Parameters.AddWithValue("@EnviadoPorZAP", morador.EnviadoPorZAP);
                    //cmd.Parameters.AddWithValue("@EnviadoPorTELEGRAM", morador.EnviadoPorTELEGRAM);
                    //cmd.Parameters.AddWithValue("@EnviadoPorEmail1", morador.EnviadoPorEmail1);
                    //cmd.Parameters.AddWithValue("@ReciboImpresso", morador.ReciboImpresso);

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

        public static bool Update(Morador morador)
        {
            var query = @"Update Sedex Set 
                Bloco = @Bloco,
                Apartamento = @Apartamento,
                NomeMorador = @NomeMorador,
                Email1 = @Email1,
                Celular1 = @Celular1,
                CodigoBarraEtiqueta = @CodigoBarraEtiqueta,
                CodigoQRCode = @CodigoQRCode,
                CodigoBarraEtiquetaLocal = @CodigoBarraEtiquetaLocal,
                LocalPrateleira = @LocalPrateleira,
                DataCadastro = @DataCadastro,
                DataEntrega = @DataEntrega,
                DataEnvioMensagem = @DataEnvioMensagem,
                EnviadoPorEmail1 = @EnviadoPorEmail1,
                EnviadoPorSMS = @EnviadoPorSMS,
                EnviadoPorTELEGRAM = @EnviadoPorTELEGRAM,
                EnviadoPorZAP = @EnviadoPorZAP,
                ReciboImpresso = @ReciboImpresso
            Where IdMorador = @IdMorador";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (morador.IdMorador > 0)
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@IdMorador", morador.IdMorador);
                        cmd.Parameters.AddWithValue("@Bloco", morador.Bloco);
                        cmd.Parameters.AddWithValue("@Apartamento", morador.Apartamento);
                        cmd.Parameters.AddWithValue("@NomeMorador", morador.NomeMorador);
                        cmd.Parameters.AddWithValue("@Email1", morador.Email1);
                        cmd.Parameters.AddWithValue("@Celular1", morador.Celular1);
                        //cmd.Parameters.AddWithValue("@CodigoBarraEtiqueta", morador.CodigoBarraEtiqueta);
                        //cmd.Parameters.AddWithValue("@CodigoQRCode", morador.CodigoQRCode);
                        //cmd.Parameters.AddWithValue("@CodigoBarraEtiquetaLocal", morador.CodigoBarraEtiquetaLocal);
                        //cmd.Parameters.AddWithValue("@LocalPrateleira", morador.LocalPrateleira);
                        //cmd.Parameters.AddWithValue("@DataCadastro", morador.DataCadastro);
                        //cmd.Parameters.AddWithValue("@DataEntrega", morador.DataEntrega);
                        //cmd.Parameters.AddWithValue("@DataEnvioMensagem", morador.DataEnvioMensagem);
                        //cmd.Parameters.AddWithValue("@EnviadoPorSMS", morador.EnviadoPorSMS);
                        //cmd.Parameters.AddWithValue("@EnviadoPorZAP", morador.EnviadoPorZAP);
                        //cmd.Parameters.AddWithValue("@EnviadoPorTELEGRAM", morador.EnviadoPorTELEGRAM);
                        //cmd.Parameters.AddWithValue("@EnviadoPorEmail1", morador.EnviadoPorEmail1);
                        //cmd.Parameters.AddWithValue("@ReciboImpresso", morador.ReciboImpresso);

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
            var query = @"UPDATE SEDEX
                SET Celular1 = @Celular1
                WHERE
                BLOCO = @Bloco
                AND APARTAMENTO = @Apartamento
                AND NomeMorador = @NomeMorador";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (morador.IdMorador > 0)
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@Bloco", morador.Bloco);
                        cmd.Parameters.AddWithValue("@Apartamento", morador.Apartamento);
                        cmd.Parameters.AddWithValue("@NomeMorador", morador.NomeMorador);
                        cmd.Parameters.AddWithValue("@Celular1", morador.Celular1);

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
                morador.Email1 = row.ItemArray[4].ToString();
                morador.Celular1 = row.ItemArray[5].ToString();
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
    }
}
