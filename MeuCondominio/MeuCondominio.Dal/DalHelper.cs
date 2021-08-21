using System;
using System.Collections.Generic;
using System.Data;
using MeuCondominio.Model;
using System.Data.SQLite;

namespace MeuCondominio.Dal
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
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Sedex(IdMorador int, Bloco Varchar(2), Apartamento Varchar(3), NomeDestinatario Varchar(100), Email Varchar(100), NumeroCelular Varchar(11), CodigoBarraEtiqueta Varchar(50), CodigoBarraEtiquetaLocal Varchar(50), LocalPrateleira Varchar(3)), DataEnvioMensagem VARCHAR(20), EnviadoPorSMS VARCHAR(1), EnviadoPorZAP VARCHAR(1), EnviadoPorTELEGRAM VARCHAR(1), EnviadoPorEMAIL VARCHAR(1)";
                    cmd.ExecuteNonQuery();
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

        public static List<Morador> GetClientes(string pBloco, string pApto)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM SEDEX WHERE BLOCO = '" + pBloco + "' AND APARTAMENTO = '" + pApto + "' AND CODIGOBARRAETIQUETA IS NULL AND CODIGOQRCODE IS NULL AND CODIGOBARRAETIQUETALOCAL IS NULL AND LOCALPRATELEIRA = 0";
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
                    cmd.CommandText = "SELECT * FROM SEDEX WHERE BLOCO = '" + pBloco + "' AND APARTAMENTO = '" + pApto + "'  AND NOMEDESTINATARIO = '" + pNomeMorador + "' AND CODIGOBARRAETIQUETA IS NULL AND CODIGOQRCODE IS NULL AND CODIGOBARRAETIQUETALOCAL IS NULL AND LOCALPRATELEIRA = 0";
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

        public static List<Morador> GetSedexParaEnvioSms()
        {
            try
            {
                List<Morador> listaMoradores;
                var query = @"SELECT IdMorador, Bloco, Apartamento, NomeDestinatario, Email, NumeroCelular, 
                    CodigoBarraEtiqueta, CodigoBarraEtiquetaLocal, CodigoQRCode, 
                    LocalPrateleira, DataCadastro, DataEnvioMensagem, DataEntrega, 
                    EnviadoPorEMAIL, EnviadoPorSMS, EnviadoPorTELEGRAM, EnviadoPorZAP, ReciboImpresso 
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

            var query = @"SELECT * FROM SEDEX 
                        WHERE 
                        DATACADASTRO IS NOT NULL 
                        AND DATAENVIOMENSAGEM IS NOT NULL 
                        AND EnviadoPorSMS = 'S'
                        AND ReciboImpresso = 'N'
                        ORDER BY 
                        Bloco, 
                        Apartamento;";

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
                    var query = @"INSERT INTO Sedex(IdMorador, Bloco , Apartamento , NomeDestinatario , Email , NumeroCelular , CodigoBarraEtiqueta , CodigoBarraEtiquetaLocal , LocalPrateleira, DataCadastro, DataEntrega, DataEnvioMensagem, EnviadoPorSMS, EnviadoPorZAP, EnviadoPorTELEGRAM, EnviadoPorEMAIL, ReciboImpresso)
                                    values ((SELECT MAX(IDMORADOR)+1 FROM SEDEX), @Bloco , @Apartamento , @NomeDestinatario , @Email , @NumeroCelular , @CodigoBarraEtiqueta , @CodigoBarraEtiquetaLocal , @LocalPrateleira, @DataCadastro, @DataEntrega, @DataEnvioMensagem, @EnviadoPorSMS, @EnviadoPorZAP, @EnviadoPorTELEGRAM, @EnviadoPorEMAIL, @ReciboImpresso)";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Bloco", morador.Bloco);
                    cmd.Parameters.AddWithValue("@Apartamento", morador.Apartamento);
                    cmd.Parameters.AddWithValue("@NomeDestinatario", morador.NomeDestinatario);
                    cmd.Parameters.AddWithValue("@Email", morador.email);
                    cmd.Parameters.AddWithValue("@NumeroCelular", morador.NumeroCelular);
                    cmd.Parameters.AddWithValue("@CodigoBarraEtiqueta", morador.CodigoBarraEtiqueta);
                    cmd.Parameters.AddWithValue("@CodigoBarraEtiquetaLocal", morador.CodigoBarraEtiquetaLocal);
                    cmd.Parameters.AddWithValue("@LocalPrateleira", morador.LocalPrateleira);
                    cmd.Parameters.AddWithValue("@DataCadastro", morador.DataCadastro);
                    cmd.Parameters.AddWithValue("@DataEntrega", morador.DataEntrega);
                    cmd.Parameters.AddWithValue("@DataEnvioMensagem", morador.DataEnvioMensagem);
                    cmd.Parameters.AddWithValue("@EnviadoPorSMS", morador.EnviadoPorSMS);
                    cmd.Parameters.AddWithValue("@EnviadoPorZAP", morador.EnviadoPorZAP);
                    cmd.Parameters.AddWithValue("@EnviadoPorTELEGRAM", morador.EnviadoPorTELEGRAM);
                    cmd.Parameters.AddWithValue("@EnviadoPorEMAIL", morador.EnviadoPorEMAIL);
                    cmd.Parameters.AddWithValue("@ReciboImpresso", morador.ReciboImpresso);

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
                NomeDestinatario = @NomeDestinatario,
                Email = @Email,
                NumeroCelular = @NumeroCelular,
                CodigoBarraEtiqueta = @CodigoBarraEtiqueta,
                CodigoQRCode = @CodigoQRCode,
                CodigoBarraEtiquetaLocal = @CodigoBarraEtiquetaLocal,
                LocalPrateleira = @LocalPrateleira,
                DataCadastro = @DataCadastro,
                DataEntrega = @DataEntrega,
                DataEnvioMensagem = @DataEnvioMensagem,
                EnviadoPorEMAIL = @EnviadoPorEMAIL,
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
                        cmd.Parameters.AddWithValue("@NomeDestinatario", morador.NomeDestinatario);
                        cmd.Parameters.AddWithValue("@email", morador.email);
                        cmd.Parameters.AddWithValue("@NumeroCelular", morador.NumeroCelular);
                        cmd.Parameters.AddWithValue("@CodigoBarraEtiqueta", morador.CodigoBarraEtiqueta);
                        cmd.Parameters.AddWithValue("@CodigoQRCode", morador.CodigoQRCode);
                        cmd.Parameters.AddWithValue("@CodigoBarraEtiquetaLocal", morador.CodigoBarraEtiquetaLocal);
                        cmd.Parameters.AddWithValue("@LocalPrateleira", morador.LocalPrateleira);
                        cmd.Parameters.AddWithValue("@DataCadastro", morador.DataCadastro);
                        cmd.Parameters.AddWithValue("@DataEntrega", morador.DataEntrega);
                        cmd.Parameters.AddWithValue("@DataEnvioMensagem", morador.DataEnvioMensagem);
                        cmd.Parameters.AddWithValue("@EnviadoPorSMS", morador.EnviadoPorSMS);
                        cmd.Parameters.AddWithValue("@EnviadoPorZAP", morador.EnviadoPorZAP);
                        cmd.Parameters.AddWithValue("@EnviadoPorTELEGRAM", morador.EnviadoPorTELEGRAM);
                        cmd.Parameters.AddWithValue("@EnviadoPorEMAIL", morador.EnviadoPorEMAIL);
                        cmd.Parameters.AddWithValue("@ReciboImpresso", morador.ReciboImpresso);

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
                SET NUMEROCELULAR = @NumeroCelular
                WHERE
                BLOCO = @Bloco
                AND APARTAMENTO = @Apartamento
                AND NOMEDESTINATARIO = @NomeDestinatario";

            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (morador.IdMorador > 0)
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@Bloco", morador.Bloco);
                        cmd.Parameters.AddWithValue("@Apartamento", morador.Apartamento);
                        cmd.Parameters.AddWithValue("@NomeDestinatario", morador.NomeDestinatario);
                        cmd.Parameters.AddWithValue("@NumeroCelular", morador.NumeroCelular);

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

            var query = @"Select IDMORADOR, BLOCO, APARTAMENTO, NOMEDESTINATARIO, EMAIL, NUMEROCELULAR, CODIGOBARRAETIQUETA, CODIGOQRCODE, CODIGOBARRAETIQUETA, LOCALPRATELEIRA,
                DATACADASTRO, DATAENTREGA, DATAENVIOMENSAGEM, ENVIADOPORSMS, ENVIADOPORZAP, ENVIADOPORTELEGRAM, ENVIADOPOREMAIL, RECIBOIMPRESSO
                FROM SEDEX
                WHERE
                    1 = 1
                AND BLOCO = @BLOCO
                AND APARTAMENTO = @APARTAMENTO
                AND ENVIADOPORSMS = 'S'
                AND DATAENVIOMENSAGEM IS NOT NULL";

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


        private static List<Morador> PreencheMoradorDtReader(SQLiteDataReader reader)
        {
            List<Morador> moradores = new List<Morador>();

            while (reader.Read())
            {
                Morador morador = new Morador();

                morador.IdMorador = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                morador.Bloco = reader.IsDBNull(1) ? "" : reader.GetString(1);
                morador.Apartamento = reader.IsDBNull(2) ? "" : reader.GetString(2);
                morador.NomeDestinatario = reader.IsDBNull(3) ? "" : reader.GetString(3);
                morador.email = reader.IsDBNull(4) ? "" : reader.GetString(4);
                morador.NumeroCelular = reader.IsDBNull(5) ? "" : reader.GetString(5);
                morador.CodigoBarraEtiqueta = reader.IsDBNull(6) ? "" : reader.GetString(6);
                morador.CodigoQRCode = reader.IsDBNull(7) ? "" : reader.GetString(7);
                morador.CodigoBarraEtiquetaLocal = reader.IsDBNull(8) ? "" : reader.GetString(8);
                morador.LocalPrateleira = reader.IsDBNull(9) ? 0 : int.Parse(reader.GetString(9));
                morador.DataCadastro = reader.IsDBNull(10) ? "" : reader.GetString(10);
                morador.DataEntrega = reader.IsDBNull(11) ? "" : reader.GetString(11);
                morador.DataEnvioMensagem = reader.IsDBNull(12) ? "" : reader.GetString(12);
                morador.EnviadoPorSMS = reader.IsDBNull(13) ? "" : reader.GetString(13);
                morador.EnviadoPorZAP = reader.IsDBNull(14) ? "" : reader.GetString(14);
                morador.EnviadoPorTELEGRAM = reader.IsDBNull(15) ? "" : reader.GetString(15);
                morador.EnviadoPorEMAIL = reader.IsDBNull(16) ? "" : reader.GetString(16);
                morador.ReciboImpresso = reader.IsDBNull(17) ? "" : reader.GetString(17);
                moradores.Add(morador);
            }
            return moradores;
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
                morador.NomeDestinatario = row.ItemArray[3].ToString();
                morador.email = row.ItemArray[4].ToString();
                morador.NumeroCelular = row.ItemArray[5].ToString();
                morador.CodigoBarraEtiqueta = row.ItemArray[6].ToString();
                morador.CodigoQRCode = row.ItemArray[7].ToString();
                morador.CodigoBarraEtiquetaLocal = row.ItemArray[8].ToString();
                morador.LocalPrateleira = int.Parse(row.ItemArray[9].ToString());
                morador.DataCadastro = row.ItemArray[10].ToString();
                morador.DataEntrega = row.ItemArray[11].ToString();
                morador.DataEnvioMensagem = row.ItemArray[12].ToString();
                morador.EnviadoPorSMS = row.ItemArray[13].ToString();
                morador.EnviadoPorZAP = row.ItemArray[14].ToString();
                morador.EnviadoPorTELEGRAM = row.ItemArray[15].ToString();
                morador.EnviadoPorEMAIL = row.ItemArray[16].ToString();
                morador.ReciboImpresso = row.ItemArray[17].ToString();

                moradores.Add(morador);
            }
            return moradores;
        }
    }
}
