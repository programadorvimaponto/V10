using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;
using System.Windows.Forms;

namespace IntegracaoClientes
{
    public class BasIsFichaCliente : FichaClientes
    {
        private bool actualiza;
        private bool clienteCriadoAgora;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("IntegracaoClientes") == 1)
            {
                // ########################################################################################################################
                // ####### Verifica se existe alguma alteração no Anulado ou Tipo de Crédito e pergunta se é para atualizar nas empresas ##
                // #######    JFC 11/12/2020 - Pedido de Sofia Mendes                                                                    ##
                // ########################################################################################################################

                StdBELista listCriterios;
                string strCredito;
                actualiza = false;
                clienteCriadoAgora = false;
                strCredito = "";
                listCriterios = BSO.Consulta("select top 1 ClienteAnulado, TipoCred from Clientes where Cliente='" + this.Cliente.Cliente + "'");
                listCriterios.Inicio();

                if (listCriterios.Vazia() == false)
                {
                    if (this.Cliente.Inactivo != listCriterios.Valor("ClienteAnulado") | this.Cliente.TipoCredito != listCriterios.Valor("TipoCred"))
                    {
                        switch (this.Cliente.TipoCredito)
                        {
                            case "1":
                                {
                                    strCredito = "Por Limite";
                                    break;
                                }

                            case "2":
                                {
                                    strCredito = "Suspenso";
                                    break;
                                }
                        }
                        if (MessageBox.Show("Deseja atualizar os seguintes parâmetros em todas as empresas?, " + Strings.Chr(13) + Strings.Chr(13) + "Anulado: " + this.Cliente.Inactivo + Strings.Chr(13) + "Crédito: " + strCredito + Strings.Chr(13) + "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)

                            actualiza = true;
                    }
                }
                else
                    clienteCriadoAgora = true;
            }
        }

        private StdBELista listEmpresas;

        public override void DepoisDeGravar(string Cliente, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Cliente, e);

            if (Module1.VerificaToken("IntegracaoClientes") == 1)
            {
                if (actualiza)
                {
                    // JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                    listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" + Aplicacao.Empresa.CodEmp + "' and PRI_FichaClientes='1'");
                    listEmpresas.Inicio();

                    for (var i = 1; i <= listEmpresas.NumLinhas(); i++)
                    {
                        BSO.DSO.ExecuteSQL("update PRI" + listEmpresas.Valor("Empresa") + ".DBO.Clientes set ClienteAnulado='" + this.Cliente.Inactivo + "', TipoCred='" + this.Cliente.TipoCredito + "' where CDU_EntidadeInterna='" + this.Cliente.CamposUtil["CDU_EntidadeInterna"].Valor + "'");
                        listEmpresas.Seguinte();
                    }
                }

                // JFC 04/03/2021 Valida Cliente criado, e atualiza CDU_PrintLab
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" + Aplicacao.Empresa.CodEmp + "' and PRI_FichaClientes='1'");
                listEmpresas.Inicio();

                if (clienteCriadoAgora)
                {
                    for (var i = 1; i <= listEmpresas.NumLinhas(); i++)
                        BSO.DSO.ExecuteSQL("update c set c.CDU_PrintLab=c2.CDU_PrintLab from dbo.Clientes c inner join PRI" + listEmpresas.Valor("Empresa") + ".dbo.Clientes c2  on c2.CDU_EntidadeInterna=c.CDU_EntidadeInterna where c.CDU_EntidadeInterna='" + this.Cliente.CamposUtil["CDU_EntidadeInterna"].Valor + "'");
                }
                else
                    for (var i = 1; i <= listEmpresas.NumLinhas(); i++)
                    {
                        BSO.DSO.ExecuteSQL("update PRI" + listEmpresas.Valor("Empresa") + ".DBO.Clientes set CDU_PrintLab='" + this.Cliente.CamposUtil["CDU_PrintLab"].Valor + "' where CDU_EntidadeInterna='" + this.Cliente.CamposUtil["CDU_EntidadeInterna"].Valor + "'");
                        listEmpresas.Seguinte();
                    }
            }
        }
    }
}