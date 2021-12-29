using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;

namespace Vatbook
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            // #################################################################################################
            // ## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
            // #################################################################################################
            if (this.DocumentoCompra.CamposUtil["CDU_Fattura_Numero"].Valor.ToString() + "" == "" | this.DocumentoCompra.CamposUtil["CDU_Fattura_Numero"].Valor.ToString() == "0")
            {
                string str;
                StdBELista lista;

                str = BSO.Compras.TabCompras.DaValorAtributo(this.DocumentoCompra.Tipodoc, "CDU_Fattura_SezionaleIVA");
                if (str + "" != "")
                {
                    lista = BSO.Consulta("select dbo.fnFattura_Num('" + str + "','c')");
                    lista.Inicio();

                    this.DocumentoCompra.CamposUtil["CDU_Fattura_Numero"] = lista.Valor(0);
                }
            }
        }
    }
}