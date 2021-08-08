﻿using Microsoft.Data.Sqlite;
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
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Sedex(IdMorador int, Bloco Varchar(2), Apartamento Varchar(3), NomeDestinatario Varchar(100), Email Varchar(100), NumeroCelular Varchar(11), CodigoBarraEtiqueta Varchar(50), CodigoBarraEtiquetaLocal Varchar(50), LocalPrateleira Varchar(3)), DataEnvioMensagem VARCHAR(20), Enviadosms VARCHAR(1), EnviadoZap VARCHAR(1), EnviadoTelegram VARCHAR(1), EnviadoEmail VARCHAR(1)";
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
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Sedex Where CodigoBarraEtiqueta=" + CodigoBarras;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        var morador = PreencheMoradorDt(dt);
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

        public static List<Morador> GetSedexParaAssinatura()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            var query = @"SELECT * FROM SEDEX 
                        WHERE 
                        DATACADASTRO IS NOT NULL 
                        AND DATAENVIOMENSAGEM IS NOT NULL 
                        AND DATAENTREGA IS NOT NULL 
                        AND ENVIADOSMS = 'S' 
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
                    var query = @"INSERT INTO Sedex(IdMorador, Bloco , Apartamento , NomeDestinatario , Email , NumeroCelular , CodigoBarraEtiqueta , CodigoBarraEtiquetaLocal , LocalPrateleira, DataCadastro, DataEntrega, DataEnvioMensagem, Enviadosms, EnviadoZap, EnviadoTelegram, EnviadoEmail)
                                    values ((SELECT MAX(IDMORADOR)+1 FROM SEDEX), @Bloco , @Apartamento , @NomeDestinatario , @Email , @NumeroCelular , @CodigoBarraEtiqueta , @CodigoBarraEtiquetaLocal , @LocalPrateleira, @DataCadastro, @DataEntrega, @DataEnvioMensagem, @Enviadosms, @EnviadoZap, @EnviadoTelegram, @EnviadoEmail)";

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
                    cmd.Parameters.AddWithValue("@Enviadosms", morador.Enviadosms);
                    cmd.Parameters.AddWithValue("@EnviadoZap", morador.EnviadoZap);
                    cmd.Parameters.AddWithValue("@EnviadoTelegram", morador.EnviadoTelegram);
                    cmd.Parameters.AddWithValue("@EnviadoEmail", morador.EnviadoEmail);


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
                EnviadoEmail = @EnviadoEmail,
                Enviadosms = @Enviadosms,
                EnviadoTelegram = @EnviadoTelegram,
                EnviadoZap = @EnviadoZap
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
                        cmd.Parameters.AddWithValue("@Enviadosms", morador.Enviadosms);
                        cmd.Parameters.AddWithValue("@EnviadoZap", morador.EnviadoZap);
                        cmd.Parameters.AddWithValue("@EnviadoTelegram", morador.EnviadoTelegram);
                        cmd.Parameters.AddWithValue("@EnviadoEmail", morador.EnviadoEmail);

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
                morador.Enviadosms = row.ItemArray[13].ToString();
                morador.EnviadoZap = row.ItemArray[14].ToString();
                morador.EnviadoTelegram = row.ItemArray[15].ToString();
                morador.EnviadoEmail = row.ItemArray[16].ToString();

                moradores.Add(morador);
            }
            return moradores;
        }
    }
}
