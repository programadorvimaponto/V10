using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.PayablesReceivables.Editors;
using StdBE100;

namespace Vatbook
{
    public class CctIsEditorPendentes : EditorPendentes
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            // #################################################################################################
            // ## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
            // #################################################################################################
            if (this.DocumentoPendente.CamposUtil["CDU_Fattura_Numero"].Valor + "" == "" | this.DocumentoPendente.CamposUtil["CDU_Fattura_Numero"].Valor == "0")
            {
                string str;
                StdBELista lista;
                str = BSO.PagamentosRecebimentos.TabCCorrentes.DaValorAtributo(this.DocumentoPendente.Tipodoc, "CDU_Fattura_SezionaleIVA");
                if (str + "" != "")
                {
                    lista = BSO.Consulta("select dbo.fnFattura_Num('" + str + "','c')");
                    lista.Inicio();

                    if (lista.Valor(0) + "" != "")
                        this.DocumentoPendente.CamposUtil["CDU_Fattura_Numero"].Valor = lista.Valor(0);
                    else
                        this.DocumentoPendente.CamposUtil["CDU_Fattura_Numero"].Valor = 1;
                }
            }
        }
    }
}