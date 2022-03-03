using Generico;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;

namespace HistoricoPlafond
{
    public class BasIsFichaCliente : FichaClientes
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("HistoricoPlafond") == 1)
            {
                // #################################################################################################
                // ####### Atualiza a tabela TDU_HistoricoPlafond - JFC 12-02-2019                        ##########
                // #################################################################################################

                StdBELista HistoricoPlafond;
                double PlafondSeguradora;
                double PlafondSolicitado;
                double PlafondAdicional;

                PlafondSeguradora = 0;
                PlafondSolicitado = 0;
                PlafondAdicional = 0;   

                HistoricoPlafond = BSO.Consulta("select top 1 Data, PlafondSeguradora, PlafondSolicitado, PlafondAdicional from TDU_HistoricoPlafond where Entidade='" + this.Cliente.Cliente + "' and Empresa='Mundifios' order by Data desc");

                if (HistoricoPlafond.Vazia())
                    BSO.DSO.ExecuteSQL("INSERT INTO [PRIMUNDIFIOS].[DBO].[TDU_HistoricoPlafond] values ('Mundifios', getdate(),'" + this.Cliente.Cliente + "','" + this.Cliente.Nome + "', '" + this.Cliente.CamposUtil["CDU_PlafondSeguradora"].Valor + "','" + this.Cliente.CamposUtil["CDU_PlafondExtra"].Valor + "','" + this.Cliente.CamposUtil["CDU_PlafondAdicional"].Valor + "')");
                else
                {
                    HistoricoPlafond.Inicio();

                    PlafondSeguradora = HistoricoPlafond.Valor("PlafondSeguradora");
                    PlafondSolicitado = HistoricoPlafond.Valor("PlafondSolicitado");
                    PlafondAdicional = HistoricoPlafond.Valor("PlafondAdicional");

                    if (double.Parse(Cliente.CamposUtil["CDU_PlafondSeguradora"].Valor.ToString()) != PlafondSeguradora | double.Parse(Cliente.CamposUtil["CDU_PlafondExtra"].Valor.ToString()) != PlafondSolicitado | double.Parse(Cliente.CamposUtil["CDU_PlafondAdicional"].Valor.ToString()) != PlafondAdicional)
                        BSO.DSO.ExecuteSQL("INSERT INTO [PRIMUNDIFIOS].[DBO].[TDU_HistoricoPlafond] values ('Mundifios', getdate(),'" + this.Cliente.Cliente + "','" + this.Cliente.Nome + "', '" + this.Cliente.CamposUtil["CDU_PlafondSeguradora"].Valor + "','" + this.Cliente.CamposUtil["CDU_PlafondExtra"].Valor + "','" + this.Cliente.CamposUtil["CDU_PlafondAdicional"].Valor + "')");
                }
            }
        }
    }
}