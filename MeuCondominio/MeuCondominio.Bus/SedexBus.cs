using MeuCondominio.Model;
using MeuCondominio.Model;
using System.Collections.Generic;
using System.Data;

namespace MeuCondominio.Bus
{
    public class SedexBus
    {
        public Morador Consultar(int idMorador)
        {
            return DalHelper.GetCliente(idMorador);
        }

        public Morador Consultar(string CodigoBarras)
        {
            return DalHelper.GetCliente(CodigoBarras);
        }

        public List<Morador> Moradores(string pBloco, string pApto)
        {
            return DalHelper.GetMoradores(pBloco, pApto);
        }

        public Morador Consultar(string pBloco, string pApto, string pNomeMorador)
        {
            return DalHelper.GetMorador(pBloco, pApto, pNomeMorador);
        }

        public List<Morador> Consultar(Morador morador)
        {
            return DalHelper.GetClientes(morador);
        }

        public List<Morador> RetornaListaParaEnvioSms()
        {
            return DalHelper.GetSedexParaEnvioSms();
        }

        public List<Morador> RetornaListaParaRecibo()
        {
            return DalHelper.GetSedexParaAssinatura();
        }


        public bool Adicionar(Morador morador)
        {
            Apartamento ap = DalHelper.GetApartamento(morador.Bloco, morador.Apartamento);
            morador.IdApartamento = ap.IdApartamento;
            return DalHelper.Add(morador);
        }

        public bool Adicionar(Morador morador, Sedex sedex)
        {
            return DalHelper.Add(morador, sedex);
        }

        public bool Atualizar()
        {
            return DalHelper.Update();
        }

        public bool AtualizarMorador(Morador morador)
        {
            return DalHelper.Update();
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
        public bool AdicionarHistorico(Morador morador, Sedex sedex)
        {
            return DalHelper.AddHistorico(morador, sedex);
        }
        #endregion



    }
}
