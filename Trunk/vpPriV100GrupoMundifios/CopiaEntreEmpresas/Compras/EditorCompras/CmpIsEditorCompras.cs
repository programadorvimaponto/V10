using CmpBE100;
using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Attributes;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;
using System;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100;
using VndBE100;
using static BasBE100.BasBETiposGcp;

namespace CopiaEntreEmpresas
{
    public class CmpIsEditorCompras : EditorCompras
    {
        private static string[] IDLinhaDocOriginal = new string[1000];

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                // EduSamp
                if (!BSO.Compras.Documentos.Existe(this.DocumentoCompra.Filial, this.DocumentoCompra.Tipodoc, this.DocumentoCompra.Serie, this.DocumentoCompra.NumDoc))
                {
                    this.DocumentoCompra.CamposUtil["CDU_DocumentoVendaDestino"].Valor = "";
                    this.DocumentoCompra.CamposUtil["CDU_DocumentoCompraDestino"].Valor = "";
                }
            }
        }
        [Order(130)]
        //Deixar os outros eventos similares fazerem o que têm a fazer antes de passar para o CopiaEntreEmpresas
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
                // EduSamp
                RegistarDocumentosEmpresaGrupo(Filial, Tipo, Serie, NumDoc);
        }
        
        private string NomeEmpresaDestino;

        private bool RegistarDocumentosEmpresaGrupo(string Filial_Atual, string TipoDoc_Atual, string Serie_Atual, int NumDoc_Atual)
        {
            try
            {
                string Mensagem;

                if (Strings.Len(DocumentoCompra.CamposUtil["CDU_DocumentoVendaDestino"].Valor) > 0)
                {
                    Mensagem = "O Documento atual já tinha dado origem ao(s) seguinte(s) documento(s) na empresa de Grupo: " + Strings.Chr(13) + Strings.Chr(13) + "" + this.DocumentoCompra.CamposUtil["CDU_DocumentoVendaDestino"].Valor + "" + Strings.Chr(13) + "" + Strings.Chr(13) + Strings.Chr(13) + "Caso tenha efetuado altearções, deverá replicar manualmente na empresa de Grupo.";
                    MessageBox.Show(Mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                if (Strings.Len(DocumentoCompra.CamposUtil["CDU_DocumentoOrigem"].Valor) > 0)
                {
                    Mensagem = "O Documento atual já tinha sido gerado através do seguinte documento na empresa de Grupo: " + Strings.Chr(13) + Strings.Chr(13) + "" + DocumentoCompra.CamposUtil["CDU_DocumentoOrigem"].Valor + "" + Strings.Chr(13) + "" + Strings.Chr(13) + Strings.Chr(13) + "Não irá gerar nenhum documento na empresa do Grupo.";
                    MessageBox.Show(Mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                if (Strings.Right(DocumentoCompra.Serie, 1) == "x" || Strings.Right(DocumentoCompra.Serie, 1) == "X")
                    return true;

                NomeEmpresaDestino = Strings.UCase(BSO.Base.Fornecedores.Edita(DocumentoCompra.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "");

                if (Strings.Len(NomeEmpresaDestino) == 0)
                {
                    return true;
                }

                string EntidadeDestino;
                EntidadeDestino = BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).CamposUtil["CDU_CodigoFornecedorGrupo"].Valor + "";

                if (Strings.Len(EntidadeDestino) == 0)
                {
                    MessageBox.Show("O campo de utilizador 'Cód. Fornecedor Grupo' da entidade do Grupo '" + DocumentoCompra.Entidade + "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                string TipoDocVendasDestino;
                TipoDocVendasDestino = Strings.UCase(BSO.Compras.TabCompras.Edita(TipoDoc_Atual).CamposUtil["CDU_TipoDocVendasDestino"].Valor + "");



                if (Strings.Len(TipoDocVendasDestino) == 0)
                    return true;

                string SerieVendasDestino;
                SerieVendasDestino = Strings.UCase(BSO.Compras.TabCompras.Edita(TipoDoc_Atual).CamposUtil["CDU_SerieVendasDestino"].Valor + "");
                if (Strings.Len(SerieVendasDestino) == 0)
                {
                    MessageBox.Show("O campo de utilizador 'Serie Vendas Destino' do Documento '" + TipoDoc_Atual + "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return false;
                }

                string ArmazemDestino;

                // Identificar o armazem do parametro
                ArmazemDestino = BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).CamposUtil["CDU_ArmazemGrupo"].Valor + "";

                var DocumentoModelo = new CmpBE100.CmpBEDocumentoCompra();
                DocumentoModelo = new CmpBE100.CmpBEDocumentoCompra();
                DocumentoModelo = BSO.Compras.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual);

                if (MessageBox.Show("Pretende gerar documento na empresa do Grupo?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    return true;

                Mdi_GeraDocumentoVenda.GerarDocumento_BaseCompras(DocumentoModelo, NomeEmpresaDestino, TipoDocVendasDestino, SerieVendasDestino, EntidadeDestino, ArmazemDestino);

                return true;
            }
            catch
            {
                MessageBox.Show("Erro ao Registar Documentos na empresa do Grupo", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

     
    }
}