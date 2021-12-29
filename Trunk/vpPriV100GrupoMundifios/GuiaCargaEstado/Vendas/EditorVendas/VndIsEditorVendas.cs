using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;

namespace GuiaCargaEstado
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            // *******************************************************************************************************************************************
            // #### Ao gravar GR coloca o estado da Guia de Carga como Expedida 01/10/2018 (JFC)                                                      ####
            // *******************************************************************************************************************************************
            int ln2;

            if (Module1.VerificaToken("GuiaCargaEstado") == 1)
            {
                if ((this.DocumentoVenda.Tipodoc == "GR"))
                {
                    for (ln2 = 1; ln2 <= this.DocumentoVenda.Linhas.NumItens; ln2++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(ln2).Artigo + "" != "")
                            BSO.DSO.ExecuteSQL("UPDATE ln SET ln.CDU_EstadoGC = '04' from linhasdoc ln inner join cabecdoc cd on cd.id=ln.idcabecdoc WHERE cd.tipodoc='GC' and cd.entidade = '" + this.DocumentoVenda.Entidade + "' and ln.artigo = '" + this.DocumentoVenda.Linhas.GetEdita(ln2).Artigo + "' and ln.lote = '" + this.DocumentoVenda.Linhas.GetEdita(ln2).Lote + "'");
                    }
                }
            }
        }
    }
}