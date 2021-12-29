using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using static BasBE100.BasBETiposGcp;

namespace DyStar
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("DyStar") == 1)
            {
                // #######################################################################################################################################################
                // #Adiciona na ultima linha de artigos um coment�rio se os fornecedore forem DyStar ou TCC. Bruno - 08/08/2019, tem de ser no fim de todas as condi��es #
                // #######################################################################################################################################################

                if (((this.DocumentoCompra.Entidade == "1809") | (this.DocumentoCompra.Entidade == "1812")) & (this.DocumentoCompra.Tipodoc == "ECF"))
                {
                    if (!BSO.Compras.Documentos.Existe(this.DocumentoCompra.Filial, this.DocumentoCompra.Tipodoc, this.DocumentoCompra.Serie, this.DocumentoCompra.NumDoc))
                        BSO.Compras.Documentos.AdicionaLinhaEspecial(this.DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, 0, "Os artigos inclu�dos nesta ECF devem ser entregues na sua embalagem original, com o r�tulo original, incluindo o nome do produto, o nome do fabricante/distribuidor e n�mero do lote do produto qu�mico.");
                }
            }
        }
    }
}