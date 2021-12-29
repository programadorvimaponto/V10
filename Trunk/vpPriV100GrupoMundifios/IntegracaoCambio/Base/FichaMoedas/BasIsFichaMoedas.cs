using Generico;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;
using System.Windows.Forms;

namespace IntegracaoCambio
{
    public class BasIsFichaMoedas : FichaMoedas
    {
        private StdBELista listCambio;
        private StdBELista listEmpresas;
        private int i;
        private StdBELista dataCambio;
        private StdBELista listMoeda;

        public override void DepoisDeGravar(string Moeda, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Moeda, e);

            if (Module1.VerificaToken("IntegracaoCambio") == 1)
            {
                // JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" + Aplicacao.Empresa.CodEmp + "' and PRI_FichaMoedas='1'");
                listCambio = BSO.Consulta("select top 1 * from dbo.MoedasHistorico where Moeda = '" + Moeda + "' order by Data Desc");

                listEmpresas.Inicio();
                listCambio.Inicio();

                for (i = 1; i <= listEmpresas.NumLinhas(); i++)
                {
                    listMoeda = BSO.Consulta("select top 1 * from PRI" + listEmpresas.Valor("Empresa") + ".dbo.Moedas where Moeda='" + Moeda + "'");

                    dataCambio = BSO.Consulta("select top 1 * from PRI" + listEmpresas.Valor("Empresa") + ".dbo.MoedasHistorico where Moeda='" + Moeda + "' order by Data Desc");
                    if (listMoeda.Vazia() == true)
                        MessageBox.Show("A Moeda " + Moeda + " não existe na empresa " + listEmpresas.Valor("Empresa") + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        dataCambio.Inicio();

                        if (dataCambio.Vazia() == true)
                            BSO.DSO.ExecuteSQL("insert into PRI" + listEmpresas.Valor("Empresa") + ".dbo.MoedasHistorico select top 1 * from dbo.MoedasHistorico where Moeda='" + Moeda + "' order by Data Desc");
                        else if (listCambio.Valor("Data") > dataCambio.Valor("Data"))
                            BSO.DSO.ExecuteSQL("insert into PRI" + listEmpresas.Valor("Empresa") + ".dbo.MoedasHistorico select top 1 * from dbo.MoedasHistorico where Moeda='" + Moeda + "' order by Data Desc");
                    }
                    listEmpresas.Seguinte();
                }
            }
        }
    }
}