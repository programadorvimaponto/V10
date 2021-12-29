using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;

namespace Vatbook
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            // #################################################################################################
            // ## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
            // #################################################################################################
            if (this.DocumentoVenda.CamposUtil["CDU_Fattura_Numero"].Valor + "" == "" | this.DocumentoVenda.CamposUtil["CDU_Fattura_Numero"].Valor.ToString() == "0")
            {
                string str;
                StdBELista lista;

                str = BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.Tipodoc, "CDU_Fattura_SezionaleIVA");
                if (str + "" != "")
                {
                    lista = BSO.Consulta("select dbo.fnFattura_Num('" + str + "','v')");
                    lista.Inicio();

                    this.DocumentoVenda.CamposUtil["CDU_Fattura_Numero"].Valor = lista.Valor(0);
                }
            }
        }
    }
}