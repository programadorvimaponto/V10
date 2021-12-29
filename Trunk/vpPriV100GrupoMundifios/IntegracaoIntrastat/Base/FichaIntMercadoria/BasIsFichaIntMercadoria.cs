using Generico;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;

namespace IntegracaoIntrastat
{
    public class BasIsFichaIntMercadoria : FichaIntMercadoria
    {
        private bool Updt;
        private StdBELista listEmpresas;
        private long i;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("IntegracaoIntrastat") == 1)
            {
                if (BSO.Base.IntrastatMercadoria.Existe(this.Mercadoria.Mercadoria))
                    Updt = true;
                else
                    Updt = false;
            }
        }

        public override void DepoisDeAnular(string Mercadoria, ExtensibilityEventArgs e)
        {
            base.DepoisDeAnular(Mercadoria, e);

            if (Module1.VerificaToken("IntegracaoIntrastat") == 1)
            {
                // JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" + Aplicacao.Empresa.CodEmp + "' and PRI_FichaIntMercadoria='1'");
                listEmpresas.Inicio();

                for (i = 1; i <= listEmpresas.NumLinhas(); i++)
                {
                    // Codigo atualizado a 05/06/2020 para permitir automatizar as empresas a considerar na tabela DEV_Empresas
                    BSO.DSO.ExecuteSQL("delete from PRI" + listEmpresas.Valor("Empresa") + ".dbo.IntrastatMercadoria where Mercadoria ='" + this.Mercadoria.Mercadoria + "'");
                    listEmpresas.Seguinte();
                }
            }
        }

        public override void DepoisDeGravar(string Mercadoria, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Mercadoria, e);

            if (Module1.VerificaToken("IntegracaoIntrastat") == 1)
            {
                // JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" + Aplicacao.Empresa.CodEmp + "' and PRI_FichaIntMercadoria='1'");

                listEmpresas.Inicio();

                {
                    if (Updt)
                    {
                        // Codigo atualizado a 05/06/2020 para permitir automatizar as empresas a considerar na tabela DEV_Empresas
                        for (int i = 1, loopTo = listEmpresas.NumLinhas(); i <= loopTo; i++)
                        {
                            BSO.DSO.ExecuteSQL("update PRI" + listEmpresas.Valor("Empresa") + ".dbo.IntrastatMercadoria set Descricao= '" + this.Mercadoria.Descricao + "', DescricaoInterna = '" + this.Mercadoria.DescricaoInterna + "', UnidadeSup='" + this.Mercadoria.UnidadeSup + "', UnidadeSuplementar='" + this.Mercadoria.UnidadeSuplementar + "', FactorUnidadeSup = '" + this.Mercadoria.FactorUnidadeSup + "', MassaLiquidaObrig='" + this.Mercadoria.MassaLiquidaObrig + "' where Mercadoria= '" + this.Mercadoria.Mercadoria + "'");
                            listEmpresas.Seguinte();
                        }
                    }
                    else
                    {
                        // Codigo atualizado a 05/06/2020 para permitir automatizar as empresas a considerar na tabela DEV_Empresas
                        for (int i = 1, loopTo1 = listEmpresas.NumLinhas(); i <= loopTo1; i++)
                        {
                            BSO.DSO.ExecuteSQL("insert into PRI" + listEmpresas.Valor("Empresa") + ".dbo.IntrastatMercadoria (Mercadoria, Descricao, DescricaoInterna, UnidadeSup, UnidadeSuplementar, FactorUnidadeSup, MassaLiquidaObrig) values ('" + this.Mercadoria.Mercadoria + "', '" + this.Mercadoria.Descricao + "', '" + this.Mercadoria.DescricaoInterna + "', '" + this.Mercadoria.UnidadeSup + "', '" + this.Mercadoria.UnidadeSuplementar + "', '" + this.Mercadoria.FactorUnidadeSup + "', '" + this.Mercadoria.MassaLiquidaObrig + "')");
                            listEmpresas.Seguinte();
                        }
                    }
                }
            }
        }
    }
}