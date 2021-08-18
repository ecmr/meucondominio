using MeuCondominio.Dal;
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

        public List<Morador> Consultar(string pBloco, string pApto)
        {
            return DalHelper.GetClientes(pBloco, pApto);
        }

        public List<Morador> Consultar(string pBloco, string pApto, string pNomeMorador)
        {
            return DalHelper.GetClientes(pBloco, pApto, pNomeMorador);
        }

        public List<Morador> RetornaListaParaRecibo()
        {
            return DalHelper.GetSedexParaAssinatura();
        }

        public bool Adicionar(Morador morador)
        {
            return DalHelper.Add(morador);
        }

        public bool Atualizar(Morador morador)
        {
            return DalHelper.Update(morador);
        }

        public bool AtualizarTelefone(Morador morador)
        {
            return DalHelper.UpdateTelefone(morador);
        }

        public bool Excluir(int idMorador)
        {
            return DalHelper.Delete(idMorador);
        }

    }
}
