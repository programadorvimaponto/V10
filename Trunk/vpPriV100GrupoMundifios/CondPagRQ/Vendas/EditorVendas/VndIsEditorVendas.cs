using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;

namespace CondPagRQ
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("CondPagRQ") == 1)
            {
                if (this.DocumentoVenda.CondPag + "" != "")
                {
                    if ((bool)BSO.Base.CondsPagamento.Edita(this.DocumentoVenda.CondPag).CamposUtil["CDU_RQ"].Valor == true | (bool)BSO.Base.CondsPagamento.Edita(this.DocumentoVenda.CondPag).CamposUtil["CDU_RM"].Valor == true)
                        this.DocumentoVenda.DataVenc = Module1.NovaDataVencimento(this.DocumentoVenda.DataDoc, this.DocumentoVenda.CondPag, this.DocumentoVenda.TipoEntidade, this.DocumentoVenda.Entidade);
                }
            }
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            if (Module1.VerificaToken("CondPagRQ") == 1)
            {
                if (this.DocumentoVenda.CondPag + "" != "")
                {
                    if ((bool)BSO.Base.CondsPagamento.Edita(this.DocumentoVenda.CondPag).CamposUtil["CDU_RQ"].Valor == true | (bool)BSO.Base.CondsPagamento.Edita(this.DocumentoVenda.CondPag).CamposUtil["CDU_RM"].Valor == true)
                        this.DocumentoVenda.DataVenc = Module1.NovaDataVencimento(this.DocumentoVenda.DataDoc, this.DocumentoVenda.CondPag, this.DocumentoVenda.TipoEntidade, this.DocumentoVenda.Entidade);
                }
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("CondPagRQ") == 1)
            {
                if (this.DocumentoVenda.CondPag + "" != "")
                {
                    if ((bool)BSO.Base.CondsPagamento.Edita(this.DocumentoVenda.CondPag).CamposUtil["CDU_RQ"].Valor == true | (bool)BSO.Base.CondsPagamento.Edita(this.DocumentoVenda.CondPag).CamposUtil["CDU_RM"].Valor == true)
                        BSO.DSO.ExecuteSQL("UPDATE CabecDoc SET CDU_AlteradaDataVenc = 1 WHERE Id = '" + this.DocumentoVenda.ID + "'");
                }
            }
        }
    }
}