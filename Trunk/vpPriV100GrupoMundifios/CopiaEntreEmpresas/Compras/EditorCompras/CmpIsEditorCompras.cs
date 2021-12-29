using CmpBE100;
using Generico;
using Microsoft.VisualBasic;
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

                CmpBE100.CmpBEDocumentoCompra DocumentoModelo = new CmpBE100.CmpBEDocumentoCompra();
                DocumentoModelo = new CmpBE100.CmpBEDocumentoCompra();
                DocumentoModelo = BSO.Compras.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual);

                if (MessageBox.Show("Pretende gerar documento na empresa do Grupo?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    return true;

                GerarDocumento_BaseCompras(DocumentoModelo, NomeEmpresaDestino, TipoDocVendasDestino, SerieVendasDestino, EntidadeDestino, ArmazemDestino);

                return true;
            }
            catch
            {
                MessageBox.Show("Erro ao Registar Documentos na empresa do Grupo", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool GerarDocumento_BaseCompras(CmpBEDocumentoCompra DocumentoBase, string BaseDadosDestino, string TipoDoc_Destino, string Serie_Destino, string Entidade_Destino, string Armazem_Destino)
        {
            try
            {
                //        'Identifica o Documento acabado de Criar (Encomenda a Cliente)
                //'IdentificarDocumento Filial_Origem, Serie_Origem, TipoDoc_Origem, NumDoc_Origem

                //'Gera Documento de Venda (encomenda a Cliente) à PERCATO
                if (!Module1.AbreEmpresa(BaseDadosDestino))
                    return false;

                VndBEDocumentoVenda DocumentoNovo_Venda = new VndBEDocumentoVenda();

                DocumentoNovo_Venda.Filial = "000";
                DocumentoNovo_Venda.Serie = Serie_Destino;
                //'DocCompra consta nos Campos de utilizador
                DocumentoNovo_Venda.Tipodoc = TipoDoc_Destino;
                //'Fornecedor
                DocumentoNovo_Venda.TipoEntidade = "C";
                //'CodFornecedor consta nos Campos de utilizador
                DocumentoNovo_Venda.Entidade = Entidade_Destino;
                //'-----> Falta passar o documento original
                //'DocumentoCompra.doc = DocumentoBase.Serie & "/" & DocumentoBase.NumDoc

                //'DocumentoNovo_Venda.Referencia = DocumentoBase.tipoDoc & " " & DocumentoBase.Serie & "/" & DocumentoBase.NumDoc

                DocumentoNovo_Venda.Referencia = DocumentoBase.NumDocExterno;
                DocumentoNovo_Venda.LocalCarga = DocumentoBase.LocalCarga;
                DocumentoNovo_Venda.LocalDescarga = DocumentoBase.LocalDescarga;
                DocumentoNovo_Venda.CamposUtil["CDU_DocumentoOrigem"].Valor = DocumentoBase.Tipodoc + " " + DocumentoBase.Serie + "/" + DocumentoBase.NumDoc;

                //'Condições
                DocumentoNovo_Venda.ModoPag = DocumentoBase.ModoPag;
                DocumentoNovo_Venda.CondPag = DocumentoBase.CondPag;
                DocumentoNovo_Venda.Moeda = DocumentoBase.Moeda;
                DocumentoNovo_Venda.ContaDomiciliacao = DocumentoBase.ContaDomiciliacao;
                DocumentoNovo_Venda.Responsavel = DocumentoBase.Responsavel;

                int preenche = 5;
                PriV100Api.BSO.Vendas.Documentos.PreencheDadosRelacionados(DocumentoNovo_Venda, ref preenche);
                //'GcpBE800.PreencheRelacaoVendas.vdDadosTodos
                //'Tem de estar depois dos dados relazionados!! porque senao sugere a data do sistema e altera a data do documento
                DocumentoNovo_Venda.DataDoc = DocumentoBase.DataDoc;
                //'se não colocar isto, nao consigo gravar documentos de uma série diferente à serie actual, praticada na data do sistema
                //'DocumentoCompra.DataIntroducao = DocumentoCompra.DataDoc
                int j;
                j = 1;
                string Artigo;
                double Quantidade;
                string Armazem;
                string Localizacao;
                for (int i = 1, loopTo = DocumentoBase.Linhas.NumItens; i <= loopTo; i++)
                {
                    // Adicionar a linha ao documento

                    if (DocumentoBase.Linhas.GetEdita(i).TipoLinha == "60")
                    {
                        if (Strings.InStr(1, DocumentoBase.Linhas.GetEdita(i).Descricao, "/N.º") == 0)
                        {
                            PriV100Api.BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoNovo_Venda, vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: DocumentoBase.Linhas.GetEdita(i).Descricao);
                            IDLinhaDocOriginal[j] = DocumentoBase.Linhas.GetEdita(i).IdLinha + ";" + DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha;
                            j = j + 1;
                        }
                    }
                    else
                    {
                        Artigo = DocumentoBase.Linhas.GetEdita(i).Artigo;
                        if (!PriV100Api.BSO.Base.Artigos.Existe(Artigo))
                        {
                            MessageBox.Show("O Artigo " + Artigo + " não existe na Empresa " + BaseDadosDestino, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        VerificaLote(Artigo, DocumentoBase.Linhas.GetEdita(i).Lote);
                        Quantidade = DocumentoBase.Linhas.GetEdita(i).Quantidade;

                        // Se o armazem dos parametros preenchido, sua esse. Caso contrário, usa o das linhas
                        Armazem = Interaction.IIf(Strings.Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Armazem).ToString();
                        Localizacao = Interaction.IIf(Strings.Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Localizacao).ToString();

                        // Armazem = DocumentoBase.Linhas(i).Armazem
                        // Localizacao = DocumentoBase.Linhas(i).Localizacao

                        PriV100Api.BSO.Vendas.Documentos.AdicionaLinha(DocumentoNovo_Venda, Artigo, ref Quantidade, ref Armazem, ref Localizacao);
                        DocumentoNovo_Venda.Linhas.GetEdita(j).Descricao = DocumentoBase.Linhas.GetEdita(i).Descricao;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).Lote = DocumentoBase.Linhas.GetEdita(i).Lote;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).Unidade = DocumentoBase.Linhas.GetEdita(i).Unidade;
                        // DocumentoNovo_Venda.Linhas(j).Armazem = DocumentoBase.Linhas(i).Armazem
                        // DocumentoNovo_Venda.Linhas(j).Localizacao = DocumentoBase.Linhas(i).Localizacao
                        DocumentoNovo_Venda.Linhas.GetEdita(j).CodIva = DocumentoBase.Linhas.GetEdita(i).CodIva;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).TaxaIva = DocumentoBase.Linhas.GetEdita(i).TaxaIva;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).PrecUnit = DocumentoBase.Linhas.GetEdita(i).PrecUnit;
                        // DocumentoNovo_Venda.Linhas(j).DescontoComercial = DocumentoBase.Linhas(i).DescontoComercial
                        // DocumentoNovo_Venda.Linhas(j).Desconto1 = DocumentoBase.Linhas(i).Desconto1
                        // DocumentoNovo_Venda.Linhas(j).Desconto2 = DocumentoBase.Linhas(i).Desconto2
                        // DocumentoNovo_Venda.Linhas(j).Desconto3 = DocumentoBase.Linhas(i).Desconto3
                        DocumentoNovo_Venda.Linhas.GetEdita(j).MovStock = DocumentoBase.Linhas.GetEdita(i).MovStock;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).DataStock = DocumentoBase.Linhas.GetEdita(i).DataStock;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).DataEntrega = DocumentoBase.Linhas.GetEdita(i).DataEntrega;

                        // Para garantir a rastreabilidade
                        // DocumentoNovo_Venda.Linhas(j).CamposUtil("CDU_IDLinhaOriginalPercato").Valor = DocumentoCompra.Linhas(j).IDLinha
                        IDLinhaDocOriginal[j] = DocumentoBase.Linhas.GetEdita(i).IdLinha + ";" + DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha;
                        j = j + 1;
                    }
                }

                PriV100Api.BSO.Vendas.Documentos.Actualiza(DocumentoNovo_Venda);
                long k;
                var loopTo1 = DocumentoNovo_Venda.Linhas.NumItens;
                for (k = 1L; k <= loopTo1; k++)
                {
                    // If DocumentoNovo_Venda.Linhas(k).TipoLinha = 60 And InStr(1, DocumentoNovo_Venda.Linhas(k).Descricao, "/N.º") > 0 Then
                    // ObjMotor.DSO.BDAPL.Execute "update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" & IDLinhaDocOriginal(k) & "' where id = '" & DocumentoNovo_Venda.Linhas(k).IdLinha & "' "
                    // Else
                    //
                    // End If

                    if (Strings.InStr(1, IDLinhaDocOriginal[k], ";") > 0 & Strings.Len(Strings.Mid(IDLinhaDocOriginal[k], Strings.InStr(1, IDLinhaDocOriginal[k], ";") + 1, Strings.Len(IDLinhaDocOriginal[k]))) > 0)
                    {
                        PriV100Api.BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" + Strings.Mid(IDLinhaDocOriginal[k], 1, Strings.InStr(1, IDLinhaDocOriginal[k], ";") - 1) + "' where  id = '" + Strings.Mid(IDLinhaDocOriginal[k], Strings.InStr(1, IDLinhaDocOriginal[k], ";") + 1, Strings.Len(IDLinhaDocOriginal[k])) + "' ");
                    }
                }

                try
                {
                    PriV100Api.BSO.DSO.ExecuteSQL(" UPDATE CabecCompras " + " SET CDU_DocumentoVendaDestino = '" + DocumentoNovo_Venda.Tipodoc + " " + DocumentoNovo_Venda.Serie + "/" + DocumentoNovo_Venda.NumDoc + "' " + " where filial = '" + DocumentoBase.Filial + "' and  TipoDoc = '" + DocumentoBase.Tipodoc + "' and Serie = '" + DocumentoBase.Serie + "' and NumDoc = " + DocumentoBase.NumDoc + " ");
                    MessageBox.Show("Foi gerado o documento de Venda " + DocumentoNovo_Venda.Tipodoc + " " + DocumentoNovo_Venda.Serie + "/" + DocumentoNovo_Venda.NumDoc + " na empresa " + BaseDadosDestino + " com a Entidade " + DocumentoNovo_Venda.Entidade, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    MessageBox.Show("Problemas na actualização do Num. documento de destino", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return false;
            }
        }

        private static void VerificaLote(string str_Artigo, string str_Lote)
        {
            if (str_Lote == "")
                return;

            if (PriV100Api.BSO.Inventario.ArtigosLotes.Existe(str_Artigo, str_Lote) == false)
            {
                InvBE100.InvBEArtigoLote ArtigoLote = new InvBE100.InvBEArtigoLote();

                StdBELista stdBE_ListaLote;
                stdBE_ListaLote = PriV100Api.BSO.Consulta(" SELECT * FROM ArtigoLote " + " WHERE Artigo = '" + str_Artigo + "' " + " AND Lote = '" + str_Lote + "' ");

                if (!stdBE_ListaLote.Vazia())
                {
                    stdBE_ListaLote.Inicio();

                    ArtigoLote.Artigo = stdBE_ListaLote.Valor("Artigo");
                    ArtigoLote.Lote = stdBE_ListaLote.Valor("Lote");
                    ArtigoLote.Descricao = stdBE_ListaLote.Valor("Descricao");
                    if (Strings.Len(stdBE_ListaLote.Valor("DataFabrico")) > 0)
                        ArtigoLote.DataFabrico = stdBE_ListaLote.Valor("DataFabrico");
                    if (Strings.Len(stdBE_ListaLote.Valor("Validade")) > 0)
                        ArtigoLote.Validade = stdBE_ListaLote.Valor("Validade");
                    ArtigoLote.Controlador = stdBE_ListaLote.Valor("Controlador");
                    ArtigoLote.Activo = stdBE_ListaLote.Valor("Activo");
                    ArtigoLote.Observacoes = stdBE_ListaLote.Valor("Observacoes");
                    // 2017-04-14
                    ArtigoLote.CamposUtil["CDU_TipoQualidade"].Valor = stdBE_ListaLote.Valor("CDU_TipoQualidade");
                    ArtigoLote.CamposUtil["CDU_Parafinado"].Valor = stdBE_ListaLote.Valor("CDU_Parafinado");
                    PriV100Api.BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote);
                }
            }
        }
    }
}