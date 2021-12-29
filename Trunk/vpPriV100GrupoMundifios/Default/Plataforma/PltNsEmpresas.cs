using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;
using StdBE100;

namespace Default
{
    public class PltNsEmpresas : Plataforma
    {
        // *******************************************************************************************************************************************
        // #### ARMAZEM ENTREPOSTO ####
        // *******************************************************************************************************************************************
        private StdBELista ListaArmEnt;

        private string SqlStringArmEnt;
        // *******************************************************************************************************************************************
        // #### ARMAZEM ENTREPOSTO ####
        // *******************************************************************************************************************************************

        public override void DepoisDeAbrirEmpresa(ExtensibilityEventArgs e)
        {
            base.DepoisDeAbrirEmpresa(e);

            if (Module1.VerificaToken("Default") == 1)
            {
                // *******************************************************************************************************************************************
                // #### ARMAZEM ENTREPOSTO ####
                // *******************************************************************************************************************************************
                SqlStringArmEnt = "SELECT CDU_Parametro FROM TDU_Parametros WHERE CDU_Modulo = 'Entreposto'";

                ListaArmEnt = BSO.Consulta(SqlStringArmEnt);

                if (ListaArmEnt.Vazia() == false)
                    Module1.ArmEntreposto = ListaArmEnt.Valor("CDU_Parametro");
            }
        }
    }
}