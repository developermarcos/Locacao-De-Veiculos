using FluentAssertions;
using Locadora.Dominio.ModuloGrupoDeVeiculo;
using Locadora.Dominio.ModuloPlanoCobranca;
using Locadora.Infra.Orm.ModuloGrupoVeiculo;
using Locadora.Infra.Orm.ModuloPlanoCobranca;
using Locadora.Test.Infra.Orm.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Locadora.Test.Infra.Orm.ModuloPlanoCobranca
{
    [TestClass]
    public class RepositorioPlanoCobrancaOrmTest : RepositorioBaseOrmTest
    {
        private RepositorioPlanoCobranca repositorioPlanoCobranca;
        private RepositorioGrupoVeiculo repositorioGrupoVeiculo;

        public RepositorioPlanoCobrancaOrmTest()
        {
            repositorioGrupoVeiculo = new RepositorioGrupoVeiculo(contextoDadosOrm);
            repositorioPlanoCobranca = new RepositorioPlanoCobranca(contextoDadosOrm);
        }

        private GrupoVeiculo GerarGrupoVeiculo()
        {
            GrupoVeiculo grupoVeiculo = new GrupoVeiculo();

            grupoVeiculo.Nome = "Esportivo";
            repositorioGrupoVeiculo.Inserir(grupoVeiculo);
            contextoDadosOrm.GravarDados();

            return grupoVeiculo;
        }

        private PlanoCobranca GerarPlanoCobranca()
        {
            PlanoCobranca novoPlanoCobranca = new PlanoCobranca();

            novoPlanoCobranca.DiarioDiaria = 2;
            novoPlanoCobranca.DiarioPorKm = 3;
            novoPlanoCobranca.LivreDiaria = 4;
            novoPlanoCobranca.ControladoDiaria = 5;
            novoPlanoCobranca.ControladoPorKm = 6;
            novoPlanoCobranca.ControladoLimiteKm = 7;

            return novoPlanoCobranca;
        }

        [TestMethod]
        public void Deve_Inserir_Novo_PlanoCobranca()
        {
            //arrange
            var novoPlanoCobranca = GerarPlanoCobranca();
            var grupoVeiculo = GerarGrupoVeiculo();
            novoPlanoCobranca.GrupoVeiculo = grupoVeiculo;

            //Action
            repositorioPlanoCobranca.Inserir(novoPlanoCobranca);
            contextoDadosOrm.GravarDados();

            //Assert
            var planoInserido = repositorioPlanoCobranca.SelecionarPorId(novoPlanoCobranca.Id);
            planoInserido.Should().NotBeNull();
        }
    }
}
