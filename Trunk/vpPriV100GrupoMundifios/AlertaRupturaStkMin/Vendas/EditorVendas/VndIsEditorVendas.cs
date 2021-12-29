using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;

namespace AlertaRupturaStkMin
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("AlertaRupturaStkMin") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "FA" | this.DocumentoVenda.Tipodoc == "FI" | this.DocumentoVenda.Tipodoc == "FO" | this.DocumentoVenda.Tipodoc == "FIT")
                {
                    bool ExecutaSQL;
                    ExecutaSQL = false;
                    int CountLinhas;

                    for (CountLinhas = 1; CountLinhas <= this.DocumentoVenda.Linhas.NumItens; CountLinhas++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(CountLinhas).Armazem == "A11" | this.DocumentoVenda.Linhas.GetEdita(CountLinhas).Armazem == "A17")
                            ExecutaSQL = true;
                    }

                    if (ExecutaSQL == true)
                        BSO.DSO.ExecuteSQL("EXECUTE [PRIMUNDIFIOS].[DBO].[spAlertaArm11e17]");
                }
            }
        }
    }
}