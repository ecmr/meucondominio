using MeuCondominio.Model;
using MeuCondominio.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace MeuCondominio.Bus
{
    public class SedexBus
    {
        public Morador Consultar(int idMorador)
        {
            return DalHelper.GetSedex(idMorador);
            //return DalHelper.GetCliente(idMorador);
        }

        public Morador ConsultarMorador(int idMorador)
        {
            return DalHelper.GetMorador(idMorador);
        }

        public Morador Consultar(string CodigoBarras)
        {
            return DalHelper.GetSedex(CodigoBarras);
            //return DalHelper.GetCliente(CodigoBarras);
        }
        public List<Morador> GetMoradores()
        {
            return DalHelper.GetMorador();
        }

        public List<Morador> GetMoradoresAtuais()
        {
            return DalHelper.GetMoradorAtuais();
        }

        public List<Morador> Moradores(string pBloco, string pApto)
        {
            return DalHelper.GetMorador(pBloco, pApto);
        }

        public Morador Consultar(string pBloco, string pApto, string pNomeMorador)
        {
            return DalHelper.GetMorador(pBloco, pApto, pNomeMorador);
        }

        public List<Morador> Consultar(Morador morador, string pBloco, string pApto)
        {
            return DalHelper.GetMorador(morador, pBloco, pApto);
        }

        public Apartamento RetornaApartamento(string pBloco, string pApto)
        {
            return DalHelper.GetApartamento(pBloco, pApto);
        }

        public Apartamento RetornaApartamentoFiltroExcel(string pBloco, string pApto)
        {
            return DalHelper.GetApartamentoFiltroExcel(pBloco, pApto);
        }

        public List<Morador> GetMoradores(Morador morador, string bloco, string apartameto)
        {
            return DalHelper.GetMorador(morador, bloco, apartameto);
        }

        public List<SmsEnvio> RetornaListaParaEnvioSms()
        {
            return DalHelper.GetSedexParaEnvioSms();
        }

        public List<Morador> RetornaListaParaRecibo()
        {
            return DalHelper.GetSedexParaAssinatura();
        }

        public bool Adicionar(Morador morador)
        { //ao incluir um novo morador deu erro aqui
            Apartamento ap = DalHelper.GetApartamento(morador.Bloco, morador.Apartamento);
            morador.IdApartamento = ap.IdApartamento;
            if (string.IsNullOrEmpty(morador.SobreNomeMorador))
                morador.SobreNomeMorador = PegaSobreNome(morador.NomeMorador);
            if (string.IsNullOrEmpty(morador.NomeMorador))
                morador.NomeMorador = PegaPrimeiroNome(morador.NomeMorador);
            
            return DalHelper.Add(morador);
        }

        /// <summary>
        /// Responsável por criar um moraor novo e adiconar uma encomenda para o mesmo
        /// </summary>
        /// <param name="morador"></param>
        /// <param name="sedex"></param>
        /// <returns></returns>
        public bool Adicionar(Morador morador, Sedex sedex)
        {
            bool sucesso = false;

            morador.SobreNomeMorador = PegaSobreNome(morador.NomeMorador).Trim();
            morador.NomeMorador = PegaPrimeiroNome(morador.NomeMorador).Trim();
             

            if (morador.IdMorador < 1)
            {
                morador.IdApartamento = DalHelper.GetApartamento(morador.Bloco, morador.Apartamento).IdApartamento;
                if (DalHelper.Add(morador))
                    morador = DalHelper.GetMorador(morador, morador.Bloco, morador.Apartamento)[0];
            }
            if (!string.IsNullOrEmpty(sedex.CodigoBarraEtiqueta))
                sucesso = DalHelper.Add(sedex);

            return sucesso;
        }

        /// <summary>
        /// TODO: FALTA
        /// </summary>
        /// <param name="sedex"></param>
        /// <returns></returns>
        public bool RegistrarEmvioSms(int ChaveSedex)
        {
            return DalHelper.RegistraEnvioMensagem(ChaveSedex);
        }

        public bool RegistrarEmvioEmail(int ChaveSedex)
        {
            return DalHelper.RegistraEnvioEmail(ChaveSedex);
        }

        public bool AtualizarMorador(Morador morador)
        {
            return DalHelper.AtualizaMorador(morador);
        }

        public bool AtualizarTelefone(Morador morador)
        {
            return DalHelper.UpdateTelefone(morador);
        }

        public bool Excluir(int idMorador)
        {
            return DalHelper.Delete(idMorador);
        }

        /// <summary>
        /// Novo
        /// </summary>
        /// <param name="idMorador"></param>
        /// <param name="bloco"></param>
        /// <param name="apartamento"></param>
        /// <returns></returns>
        public List<SedexHistorico> GetHistoricoPorMorador(int idMorador)
        {
            return DalHelper.GetHistoricoPorMorador(idMorador);
        }

        /// <summary>
        /// Excluir, substituido pelo GetHistoricoPorMorador(int idMorador)
        /// </summary>
        /// <param name="idMorador"></param>
        /// <param name="bloco"></param>
        /// <param name="apartamento"></param>
        /// <returns></returns>
        public List<Morador> GetHistoricoPorApartamento(string bloco, string apartamento)
        {
            return DalHelper.GetHistoricoPorApartamento(string.Concat("BLOCO ", bloco), apartamento);
        }

        #region Histórico
        public bool EnviarSmsParaHistorico()
        {
            return DalHelper.EnviaSedexParaHistorico();
        }
        public bool AdicionarHistorico(Morador morador, Sedex sedex)
        {
            return DalHelper.AddHistorico(morador, sedex);
        }

        public void ExcluiHistoricoVelho()
        {
            DalHelper.LimpaHistorico();
        }
        #endregion

        public bool AlteraStatusRecibo()
        {
            return DalHelper.AtualizaReciboImpresso();
        }

        #region Metodos gerais
        private string PegaPrimeiroNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return "";

            string[] nomeCompleto = nome.Split(' ');
            return nomeCompleto[0];
        }

        private string PegaSobreNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return "";

            string[] nomeCompleto = nome.Split(' ');

            string SobreNome = "";

            for (int i = 1; i < nomeCompleto.Length; i++)
            {
                SobreNome += string.Concat(nomeCompleto[i], " ");
            }

            return SobreNome;
        }
        #endregion

    }
}
