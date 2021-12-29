using Generico;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;
using System.Windows.Forms;

namespace IntegracaoCondPag
{
    public class BasIsFichaCondsPag : FichaCondsPag
    {
        private bool UpdtDescricao;
        private bool UpdtCondPag;
        private StdBELista listEmpresas;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("IntegracaoCondPag") == 1)
            {
                // Valida se a condição de pagamento ja existe
                if (BSO.Base.CondsPagamento.Existe(this.CondPag.CondPag))
                {
                    UpdtCondPag = true;
                    // Valida se foi alterada a descrição
                    if (BSO.Base.CondsPagamento.DaValorAtributo(this.CondPag.CondPag, "Descricao") == this.CondPag.Descricao)
                        UpdtDescricao = false;
                    else

                        if (MessageBox.Show("Deseja atualizar a descrição nas restantes empresas? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        UpdtDescricao = true;
                    else
                        UpdtDescricao = false;
                }
                else
                    UpdtCondPag = false;
            }
        }

        public override void DepoisDeAnular(string CondPag, ExtensibilityEventArgs e)
        {
            base.DepoisDeAnular(CondPag, e);

            if (Module1.VerificaToken("IntegracaoCondPag") == 1)
            {
                // JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" + Aplicacao.Empresa.CodEmp + "' and PRI_FichaCondsPag='1'");
                listEmpresas.Inicio();

                for (var i = 1; i <= listEmpresas.NumLinhas(); i++)
                {
                    BSO.DSO.ExecuteSQL("delete from pri" + listEmpresas.Valor("Empresa") + ".dbo.CondPag where CondPag ='" + this.CondPag.CondPag + "'");
                    listEmpresas.Seguinte();
                }
            }
        }

        public override void DepoisDeGravar(string CondPag, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(CondPag, e);

            if (Module1.VerificaToken("IntegracaoCondPag") == 1)
            {
                // JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" + Aplicacao.Empresa.CodEmp + "' and PRI_FichaCondsPag='1'");
                listEmpresas.Inicio();

                if (UpdtCondPag)
                {
                    // Validade se existe e se quer alterar a Descriçaão
                    if (UpdtDescricao)
                    {
                        listEmpresas.Inicio();

                        for (var i = 1; i <= listEmpresas.NumLinhas(); i++)
                        {
                            BSO.DSO.ExecuteSQL("update pri" + listEmpresas.Valor("Empresa") + ".dbo.CondPag set Descricao = '" + this.CondPag.Descricao + "', Dias='" + this.CondPag.DiasVencimento + "', Desconto= '" + this.CondPag.Desconto + "',EntradaInicial= '" + this.CondPag.EntradaInicial + "', DiasEntrada= '" + this.CondPag.DiasVencimentoEntradaInicial + "', NumPrestacoes= '" + this.CondPag.NumeroPrestacoes + "', PeriodoPrestacoes= '" + this.CondPag.PeriodicidadePrestacoes + "',TipoCondicao= '" + this.CondPag.TipoCondicao + "', Clientes='" + this.CondPag.DescLiqClientes + "', Fornecedores ='" + this.CondPag.DescLiqFornecedores + "', OutrosCredores ='" + this.CondPag.DescLiqOutrosCredores + "',OutrosDevedores ='" + this.CondPag.DescLiqOutrosDevedores + "',  DescontoIncluiIVA ='" + this.CondPag.DescontoIncluiIVA + "', CDU_RQ ='" + this.CondPag.CamposUtil["CDU_RQ"].Valor + "',CDU_RM ='" + this.CondPag.CamposUtil["CDU_RM"].Valor + "' where CondPag= '" + this.CondPag.CondPag + "'");
                            listEmpresas.Seguinte();
                        }
                    }
                    else
                        for (var i = 1; i <= listEmpresas.NumLinhas(); i++)
                        {
                            BSO.DSO.ExecuteSQL("update pri" + listEmpresas.Valor("Empresa") + ".dbo.CondPag set Dias='" + this.CondPag.DiasVencimento + "', Desconto= '" + this.CondPag.Desconto + "',EntradaInicial= '" + this.CondPag.EntradaInicial + "', DiasEntrada= '" + this.CondPag.DiasVencimentoEntradaInicial + "', NumPrestacoes= '" + this.CondPag.NumeroPrestacoes + "', PeriodoPrestacoes= '" + this.CondPag.PeriodicidadePrestacoes + "',TipoCondicao= '" + this.CondPag.TipoCondicao + "', Clientes='" + this.CondPag.DescLiqClientes + "', Fornecedores ='" + this.CondPag.DescLiqFornecedores + "', OutrosCredores ='" + this.CondPag.DescLiqOutrosCredores + "',OutrosDevedores ='" + this.CondPag.DescLiqOutrosDevedores + "', DescontoIncluiIVA ='" + this.CondPag.DescontoIncluiIVA + "',CDU_RQ ='" + this.CondPag.CamposUtil["CDU_RQ"].Valor + "',CDU_RM ='" + this.CondPag.CamposUtil["CDU_RM"].Valor + "' where CondPag= '" + this.CondPag.CondPag + "'");
                            listEmpresas.Seguinte();
                        }
                }
                else
                    for (var i = 1; i <= listEmpresas.NumLinhas(); i++)
                    {
                        BSO.DSO.ExecuteSQL("insert into "
                       + "PRI" + listEmpresas.Valor("Empresa") + ".dbo.CondPag(CondPag,Descricao,Dias,Desconto,EntradaInicial,DiasEntrada,NumPrestacoes,PeriodoPrestacoes,TipoCondicao,SugereDescontosLiquidacao,Clientes,Fornecedores,OutrosCredores,OutrosDevedores,Meses30Dias,DescontoIncluiIVA,CDU_RQ,CDU_RM)values('" + this.CondPag.CondPag + "','" + this.CondPag.Descricao + "','" + this.CondPag.DiasVencimento + "', '" + this.CondPag.Desconto + "', '" + this.CondPag.EntradaInicial + "','" + this.CondPag.DiasVencimentoEntradaInicial + "','" + this.CondPag.NumeroPrestacoes + "','" + this.CondPag.PeriodicidadePrestacoes + "','" + this.CondPag.TipoCondicao + "','" + this.CondPag.SugereDescontosLiquidacao + "','" + this.CondPag.DescLiqClientes + "','" + this.CondPag.DescLiqFornecedores + "','" + this.CondPag.DescLiqOutrosCredores + "','" + this.CondPag.DescLiqOutrosDevedores + "','" + this.CondPag.Meses30Dias + "','" + this.CondPag.DescontoIncluiIVA + "','" + this.CondPag.CamposUtil["CDU_RQ"].Valor + "','" + this.CondPag.CamposUtil["CDU_RM"].Valor + "')");
                        listEmpresas.Seguinte();
                    }
            }
        }
    }
}