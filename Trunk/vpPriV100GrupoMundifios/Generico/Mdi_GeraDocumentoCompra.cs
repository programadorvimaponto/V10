using Microsoft.VisualBasic;
using StdBE100;
using System;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100;
using static BasBE100.BasBETiposGcp;

namespace Generico
{
    internal class Mdi_GeraDocumentoCompra
    {
        private static string[] IDLinhaDocOriginal = new string[1000];

        public static bool GerarDocumento_BaseVendas(VndBE100.VndBEDocumentoVenda DocumentoBase, string BaseDadosDestino, string TipoDoc_Destino, string Serie_Destino, string Entidade_Destino, string Armazem_Destino)
        {
            try
            {
                if (!Module1.AbreEmpresa(BaseDadosDestino))
                {
                    return false;
                }

                // Identifica o Documento acabado de Criar (Encomenda a Cliente)
                // IdentificarDocumentoVenda Filial_Origem, Serie_Origem, TipoDoc_Origem, NumDoc_Origem

                var DocumentoCompra = new CmpBE100.CmpBEDocumentoCompra();
                DocumentoCompra.Filial = "000";

                // Caso a empresa de destino seja a Mixyarn, a serie é a 2020Z pois a 2020X estava a ser utilizada como Emissivel. JFC - 02/06/2020
                // If DocumentoBase.Entidade = "2492" And DocumentoBase.Tipodoc = "ECL" Then
                // DocumentoCompra.Serie = "2020Z"
                // Else
                DocumentoCompra.Serie = Serie_Destino;
                // End If
                // DocCompra consta nos Campos de utilizador
                DocumentoCompra.Tipodoc = TipoDoc_Destino;
                // Fornecedor
                DocumentoCompra.TipoEntidade = "F";
                // CodFornecedor consta nos Campos de utilizador
                DocumentoCompra.Entidade = Entidade_Destino;

                // documento original
                DocumentoCompra.NumDocExterno = DocumentoBase.Tipodoc + " " + DocumentoBase.Serie + "/" + DocumentoBase.NumDoc; // DocumentoBase.Referencia
                if (Strings.Len(DocumentoCompra.NumDocExterno) == 0)
                    DocumentoCompra.NumDocExterno = "0";
                DocumentoCompra.LocalDescarga = DocumentoBase.LocalDescarga;
                DocumentoCompra.LocalCarga = DocumentoBase.LocalCarga;
                DocumentoCompra.CamposUtil["CDU_DocumentoOrigem"].Valor = DocumentoBase.Tipodoc + " " + DocumentoBase.Serie + "/" + DocumentoBase.NumDoc;
                DocumentoCompra.MoradaEntrega = DocumentoBase.MoradaEntrega;
                Module1.emp.Compras.Documentos.PreencheDadosRelacionados(DocumentoCompra);

                // Carga
                DocumentoCompra.CargaDescarga.MoradaCarga = DocumentoBase.CargaDescarga.MoradaCarga;
                DocumentoCompra.CargaDescarga.Morada2Carga = DocumentoBase.CargaDescarga.Morada2Carga;
                DocumentoCompra.CargaDescarga.LocalidadeCarga = DocumentoBase.CargaDescarga.LocalidadeCarga;
                DocumentoCompra.CargaDescarga.DistritoCarga = DocumentoBase.CargaDescarga.DistritoCarga;
                DocumentoCompra.CargaDescarga.PaisCarga = DocumentoBase.CargaDescarga.PaisCarga;
                DocumentoCompra.CargaDescarga.CodPostalCarga = DocumentoBase.CargaDescarga.CodPostalCarga;
                DocumentoCompra.CargaDescarga.CodPostalLocalidadeCarga = DocumentoBase.CargaDescarga.CodPostalLocalidadeCarga;

                // Descarga
                DocumentoCompra.CargaDescarga.MoradaEntrega = DocumentoBase.CargaDescarga.MoradaEntrega;
                DocumentoCompra.CargaDescarga.Morada2Entrega = DocumentoBase.CargaDescarga.Morada2Entrega;
                DocumentoCompra.CargaDescarga.LocalidadeEntrega = DocumentoBase.CargaDescarga.LocalidadeEntrega;
                DocumentoCompra.CargaDescarga.DistritoEntrega = DocumentoBase.CargaDescarga.DistritoEntrega;
                DocumentoCompra.CargaDescarga.PaisEntrega = DocumentoBase.CargaDescarga.PaisEntrega;
                DocumentoCompra.CargaDescarga.CodPostalEntrega = DocumentoBase.CargaDescarga.CodPostalEntrega;
                DocumentoCompra.CargaDescarga.CodPostalLocalidadeEntrega = DocumentoBase.CargaDescarga.CodPostalLocalidadeEntrega;

                // Condições
                DocumentoCompra.ModoPag = DocumentoBase.ModoPag;
                DocumentoCompra.CondPag = DocumentoBase.CondPag;
                DocumentoCompra.Moeda = DocumentoBase.Moeda;
                DocumentoCompra.ContaDomiciliacao = DocumentoBase.ContaDomiciliacao;
                DocumentoCompra.Responsavel = DocumentoBase.Responsavel;

                // Tem de estar depois dos dados relazionados!! porque senao sugere a data do sistema e altera a data do documento
                DocumentoCompra.DataDoc = DocumentoBase.DataDoc;
                // se não colocar isto, nao consigo gravar documentos de uma série diferente à serie actual, praticada na data do sistema
                // DocumentoCompra.DataIntroducao = DocumentoCompra.DataDoc

                Module1.emp.Compras.Documentos.PreencheDadosRelacionados(DocumentoCompra);
                int i;
                int j;
                string Artigo;
                double Quantidade;
                string Armazem;
                string Localizacao;

                // ''''''''''''''''''''''''''''''''''''''''''''''''''''''
                // @1@ @2@/N.º@3@ de @4@
                Module1.emp.Compras.Documentos.AdicionaLinhaEspecial(DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, Descricao: "@1@ @2@/N.º@3@ de @4@");
                // ''''''''''''''''''''''''''''''''''''''''''''''''''''''
                // j = 1
                j = 2;
                var loopTo = DocumentoBase.Linhas.NumItens;
                for (i = 1; i <= loopTo; i++)
                {
                    // Adicionar a linha ao documento
                    if (DocumentoBase.Linhas.GetEdita(i).TipoLinha == "60")
                    {
                        if (Strings.InStr(1, DocumentoBase.Linhas.GetEdita(i).Descricao, "/N.º") == 0)
                        {
                            Module1.emp.Compras.Documentos.AdicionaLinhaEspecial(DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, Descricao: DocumentoBase.Linhas.GetEdita(i).Descricao);
                            IDLinhaDocOriginal[j] = DocumentoBase.Linhas.GetEdita(i).IdLinha + ";" + DocumentoCompra.Linhas.GetEdita(j).IdLinha;
                            j = j + 1;
                        }
                    }
                    else
                    {
                        Artigo = DocumentoBase.Linhas.GetEdita(i).Artigo;
                        VerificaLote(Artigo, DocumentoBase.Linhas.GetEdita(i).Lote);
                        Quantidade = DocumentoBase.Linhas.GetEdita(i).Quantidade;
                        Armazem = Interaction.IIf(Strings.Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Armazem).ToString();
                        Localizacao = Interaction.IIf(Strings.Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Localizacao).ToString();
                        Module1.emp.Compras.Documentos.AdicionaLinha(DocumentoCompra, Artigo, ref Quantidade, ref Armazem, ref Localizacao);
                        DocumentoCompra.Linhas.GetEdita(j).Descricao = DocumentoBase.Linhas.GetEdita(i).Descricao;
                        DocumentoCompra.Linhas.GetEdita(j).Lote = DocumentoBase.Linhas.GetEdita(i).Lote;
                        DocumentoCompra.Linhas.GetEdita(j).Unidade = DocumentoBase.Linhas.GetEdita(i).Unidade;
                        // DocumentoCompra.Linhas(j).Armazem = DocumentoBase.Linhas(i).Armazem
                        // DocumentoCompra.Linhas(j).Localizacao = DocumentoBase.Linhas(i).Localizacao

                        // Deixar a gestão do iva ao encargo do Primavera
                        // DocumentoCompra.Linhas(j).CodIva = DocumentoBase.Linhas(i).CodIva
                        // DocumentoCompra.Linhas(j).TaxaIva = DocumentoBase.Linhas(i).TaxaIva

                        DocumentoCompra.Linhas.GetEdita(j).PrecUnit = DocumentoBase.Linhas.GetEdita(i).PrecUnit;
                        DocumentoCompra.Linhas.GetEdita(j).MovStock = DocumentoBase.Linhas.GetEdita(i).MovStock;
                        DocumentoCompra.Linhas.GetEdita(j).DataStock = DocumentoBase.Linhas.GetEdita(i).DataStock;
                        DocumentoCompra.Linhas.GetEdita(j).DataEntrega = DocumentoBase.Linhas.GetEdita(i).DataEntrega;
                        IDLinhaDocOriginal[j] = DocumentoBase.Linhas.GetEdita(i).IdLinha + ";" + DocumentoCompra.Linhas.GetEdita(j).IdLinha;
                        j = j + 1;
                    }
                }

                Module1.emp.Compras.Documentos.Actualiza(DocumentoCompra);
                long k;
                var loopTo1 = DocumentoCompra.Linhas.NumItens;
                for (k = 1L; k <= loopTo1; k++)
                {
                    // If DocumentoCompra.Linhas(k).TipoLinha = 60 And InStr(1, DocumentoCompra.Linhas(k).Descricao, "/N.º") > 0 Then
                    // 'Não faz nada!
                    // Else
                    // 'Atualizar nas LinhasCompras da empresa do Grupo o Id das LinhasDoc da empresa Origem
                    // PriV100Api.BSO.DSO.BDAPL.Execute "update LinhasCompras set CDU_IDLinhaOriginalGrupo = '" & IDLinhaDocOriginal(k) & "' where id = '" & DocumentoCompra.Linhas(k).IdLinha & "' "
                    // End If

                    if (Strings.InStr(1, IDLinhaDocOriginal[k], ";") > 0 & Strings.Len(Strings.Mid(IDLinhaDocOriginal[k], Strings.InStr(1, IDLinhaDocOriginal[k], ";") + 1, Strings.Len(IDLinhaDocOriginal[k]))) > 0)
                    {
                        Module1.emp.DSO.ExecuteSQL("update LinhasCompras set CDU_IDLinhaOriginalGrupo = '" + Strings.Mid(IDLinhaDocOriginal[k], 1, Strings.InStr(1, IDLinhaDocOriginal[k], ";") - 1) + "' where id = '" + Strings.Mid(IDLinhaDocOriginal[k], Strings.InStr(1, IDLinhaDocOriginal[k], ";") + 1, Strings.Len(IDLinhaDocOriginal[k])) + "' ");
                    }
                }

                try
                {
                    // Colocar no documento origem o valor do documento acabado de criar na empresa de Grupo
                    PriV100Api.BSO.DSO.ExecuteSQL(" update CabecDoc " + " set CDU_DocumentoCompraDestino = '" + DocumentoCompra.Tipodoc + " " + DocumentoCompra.Serie + "/" + DocumentoCompra.NumDoc + "' " + " where filial = '" + DocumentoBase.Filial + "' and  TipoDoc = '" + DocumentoBase.Tipodoc + "' and Serie = '" + DocumentoBase.Serie + "' and NumDoc = " + DocumentoBase.NumDoc + " ");

                    Rastreabilidade(DocumentoBase.Filial, DocumentoBase.Serie, DocumentoBase.Tipodoc, DocumentoBase.NumDoc, BaseDadosDestino, DocumentoCompra);
                    MessageBox.Show("Foi gerado o documento de Compra " + DocumentoCompra.Tipodoc + " " + DocumentoCompra.Serie + "/" + DocumentoCompra.NumDoc + " na empresa " + BaseDadosDestino + " com a Entidade " + DocumentoCompra.Entidade, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // CopiarCaractTecnicas.CopiarCaractTec(BaseDadosDestino, DocumentoCompra)

                    return true;
                }
                catch
                {
                    MessageBox.Show("Problemas na actualização do Num. documento de destino", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private static void VerificaLote(string str_Artigo, string str_Lote)
        {
            if (str_Lote == "")
                return;

            if (Module1.emp.Inventario.ArtigosLotes.Existe(str_Artigo, str_Lote) == false)
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
                    Module1.emp.Inventario.ArtigosLotes.Actualiza(ArtigoLote);
                }
            }
        }

        private static bool Rastreabilidade(string Filial_FaturaCliente, string Serie_FaturaCliente, string TipoDoc_FaturaCliente, long NumDoc_FaturaCliente, string BaseDadosDestino, CmpBE100.CmpBEDocumentoCompra FaturaFornecedor_AcabadoGerar)
        {
            string Str_Rastreabilidade;
            StdBELista Lst_StrRastreabilidade;

            Str_Rastreabilidade = "  SELECT CD2.TipoDoc as TipoDocEncomenda, CD2.NumDoc as NumDocEncomenda, CD2.Id as IdCabecDocEncomenda, LD2.Id as IdLinhasDocEncomenda, " +
                "" + " CD.TipoDoc as TipoDocFatura, CD.NumDoc as NumDocFatura , CD.Id as IdCabecDocFatura,  LD.Id as IdLinhasDocFatura , " +
                "" + " CDGrupo.TipoDoc as TipoDocEncomendaGrupo, CDGrupo.NumDoc as NumDocEncomendaGrupo ,  " +
                "CDGrupo.Serie as SerieEncomendaGrupo , replace(CONVERT(VARCHAR(10),   " +
                "CDGrupo.DataDoc, 102),'.','/') as DataDocEncomendaGrupo ,   " +
                "CDGrupo.Id as IdCabecDocEncomendaGrupo,  " +
                "LDGrupo.Id as IdLinhasDocEncomendaGrupo , LDGrupo.Quantidade  as QuantidadeEncomendaGrupo ,"
                + " CDGrupoVFA.TipoDoc as TipoDocFaturaGrupo, CDGrupoVFA.NumDoc as NumDocFaturaGrupo, CDGrupoVFA.Id as IdCabecDocFaturaGrupo, LDGrupoVFA.Id as IdLinhasDocFaturaGrupo "
                + " FROM CabecDoc  CD " + " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60" + " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id "
                + " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " + " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " + " INNER JOIN PRI" + BaseDadosDestino
                + ".dbo.LinhasCompras LDGrupo ON LDGrupo.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD2.id) + '}' " + " INNER JOIN PRI" + BaseDadosDestino
                + ".dbo.CabecCompras CDGrupo ON CDGrupo.id = LDGrupo.IdCabecCompras " + " " + " INNER JOIN PRI" + BaseDadosDestino
                + ".dbo.LinhasCompras LDGrupoVFA ON LDGrupoVFA.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD.id) + '}' "
                + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.CabecCompras CDGrupoVFA ON CDGrupoVFA.id = LDGrupoVFA.IdCabecCompras  " + " "
                + " WHERE  CD.Filial = '" + Filial_FaturaCliente + "' AND CD.TipoDoc = '" + TipoDoc_FaturaCliente + "' AND CD.serie = '"
                + Serie_FaturaCliente + "' AND CD.NumDoc = " + NumDoc_FaturaCliente + " " + " ORDER BY LD.NumLinha ";

            // CD = A
            // CD2 = B
            // CDGrupo = D

            Lst_StrRastreabilidade = PriV100Api.BSO.Consulta(Str_Rastreabilidade);

            if (Lst_StrRastreabilidade.Vazia() == false)
            {
                Lst_StrRastreabilidade.Inicio();

                // If FaturaFornecedor_AcabadoGerar.Linhas(1).TipoLinha = 60 Then '"ECL 2016/N.º1846 de 28/12/2016"
                // FaturaFornecedor_AcabadoGerar.Linhas(1).Descricao = Lst_StrRastreabilidade("NumDocEncomendaGrupo") & " " & Lst_StrRastreabilidade("SerieEncomendaGrupo") & "Nº" & Lst_StrRastreabilidade("NumDocEncomendaGrupo") & " de " & Lst_StrRastreabilidade("DataDocEncomendaGrupo")
                // End If

                CompletarComentarioRastreabilidade(false, FaturaFornecedor_AcabadoGerar, Lst_StrRastreabilidade.Valor("TipoDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("SerieEncomendaGrupo"), Lst_StrRastreabilidade.Valor("NumDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("DataDocEncomendaGrupo"));

                while (!Lst_StrRastreabilidade.NoFim())
                {
                    Application.DoEvents();
                    // IdLinhasDoc -> Fatura de Fornecedor do Grupo
                    // IdLinhasDocOrigem -> Encomenda de Fornecedor

                    Module1.emp.DSO.ExecuteSQL(" INSERT INTO LinhasComprasTrans " + " (IdLinhasCompras,IdLinhasComprasOrigem,QuantTrans) " + " VALUES( '" + Lst_StrRastreabilidade.Valor("IdLinhasDocFaturaGrupo") + "','" + Lst_StrRastreabilidade.Valor("IdLinhasDocEncomendaGrupo") + "'," + Strings.Replace(Lst_StrRastreabilidade.Valor("QuantidadeEncomendaGrupo"), ",", ".") + " ) ");

                    Lst_StrRastreabilidade.Seguinte();
                }
            }
            else
                // Se não tiver rastreabilidade em cima, vê na de baixo
                Rastreabilidade_EncomendaParaProducao(Filial_FaturaCliente, Serie_FaturaCliente, TipoDoc_FaturaCliente, NumDoc_FaturaCliente, BaseDadosDestino, FaturaFornecedor_AcabadoGerar);

            //vba não retornava nada. Após conversa com um colega, definimos que retornava falso porque a função só é chamada e não é feita nenhuma validação sobre o estado dela
            return false;
        }

        private static void CompletarComentarioRastreabilidade(bool Apagar, CmpBE100.CmpBEDocumentoCompra FaturaFornecedor_AcabadoGerar, string Tipodoc, string Serie, long NumDoc, DateTime Data)
        {
            FaturaFornecedor_AcabadoGerar = Module1.emp.Compras.Documentos.Edita(FaturaFornecedor_AcabadoGerar.Filial, FaturaFornecedor_AcabadoGerar.Tipodoc, FaturaFornecedor_AcabadoGerar.Serie, FaturaFornecedor_AcabadoGerar.NumDoc);

            if (Apagar == false)
            {
                if (FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).TipoLinha == "60")
                {
                    if (FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao == "@1@ @2@/N.º@3@ de @4@")
                    {
                        FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = Strings.Replace(FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@1@", Tipodoc);
                        FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = Strings.Replace(FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@2@", Serie);
                        FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = Strings.Replace(FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@3@", NumDoc.ToString());
                        FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = Strings.Replace(FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@4@", Data.ToString());
                    }
                }
            }
            else if (FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).TipoLinha == "60")
            {
                if (FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao == "@1@ @2@/N.º@3@ de @4@")
                    FaturaFornecedor_AcabadoGerar.Linhas.Remove(1);
            }

            Module1.emp.Compras.Documentos.Actualiza(FaturaFornecedor_AcabadoGerar);
        }

        private static bool Rastreabilidade_EncomendaParaProducao(string Filial_FaturaCliente, string Serie_FaturaCliente, string TipoDoc_FaturaCliente, long NumDoc_FaturaCliente, string BaseDadosDestino, CmpBE100.CmpBEDocumentoCompra FaturaFornecedor_AcabadoGerar)
        {
            string Str_Rastreabilidade;
            StdBELista Lst_StrRastreabilidade;

            Str_Rastreabilidade = "  SELECT CD2.TipoDoc as TipoDocEncomenda, CD2.NumDoc as NumDocEncomenda, CD2.Id as IdCabecDocEncomenda, LD2.Id as IdLinhasDocEncomenda, " + " CD.TipoDoc as TipoDocFatura, CD.NumDoc as NumDocFatura , CD.Id as IdCabecDocFatura,  LD.Id as IdLinhasDocFatura , " + " CDGrupo.TipoDoc as TipoDocEncomendaGrupo, CDGrupo.NumDoc as NumDocEncomendaGrupo , CDGrupo.Serie as SerieEncomendaGrupo , replace(CONVERT(VARCHAR(10),   CDGrupo.DataDoc, 102),'.','/') as DataDocEncomendaGrupo , CDGrupo.Id as IdCabecDocEncomendaGrupo,  LDGrupo.Id as IdLinhasDocEncomendaGrupo , " + " LDGrupo.Quantidade  as QuantidadeEncomendaGrupo, " + " CDGrupoVFA.TipoDoc as TipoDocFaturaGrupo, CDGrupoVFA.NumDoc as NumDocFaturaGrupo, CDGrupoVFA.Id as IdCabecDocFaturaGrupo, LDGrupoVFA.Id as IdLinhasDocFaturaGrupo" + " FROM CabecDoc  CD " + " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60" + " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " + " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " + " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.LinhasCompras LDGrupo ON '{' + convert(nvarchar(50),  LDGrupo.id) + '}'  =  LD2.CDU_IDLinhaOriginalGrupo " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.CabecCompras CDGrupo ON CDGrupo.id = LDGrupo.IdCabecCompras " + " " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.LinhasCompras LDGrupoVFA ON LDGrupoVFA.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD.id) + '}' " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.CabecCompras CDGrupoVFA ON CDGrupoVFA.id = LDGrupoVFA.IdCabecCompras  " + " " + " WHERE  CD.Filial = '" + Filial_FaturaCliente + "' AND CD.TipoDoc = '" + TipoDoc_FaturaCliente + "' AND CD.serie = '" + Serie_FaturaCliente + "' AND CD.NumDoc = " + NumDoc_FaturaCliente + " " + " ORDER BY LD.NumLinha ";

            // CD = A
            // CD2 = B
            // CDGrupo = D

            Lst_StrRastreabilidade = PriV100Api.BSO.Consulta(Str_Rastreabilidade);

            if (Lst_StrRastreabilidade.Vazia() == false)
            {
                Lst_StrRastreabilidade.Inicio();

                CompletarComentarioRastreabilidade(false, FaturaFornecedor_AcabadoGerar, Lst_StrRastreabilidade.Valor("TipoDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("SerieEncomendaGrupo"), Lst_StrRastreabilidade.Valor("NumDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("DataDocEncomendaGrupo"));

                while (!Lst_StrRastreabilidade.NoFim())
                {
                    Application.DoEvents();
                    // IdLinhasDoc -> Fatura de Fornecedor do Grupo
                    // IdLinhasDocOrigem -> Encomenda de Fornecedor

                    Module1.emp.DSO.ExecuteSQL(" INSERT INTO LinhasComprasTrans " + " (IdLinhasCompras,IdLinhasComprasOrigem,QuantTrans) " + " VALUES( '" + Lst_StrRastreabilidade.Valor("IdLinhasDocFaturaGrupo") + "','" + Lst_StrRastreabilidade.Valor("IdLinhasDocEncomendaGrupo") + "'," + Strings.Replace(Lst_StrRastreabilidade.Valor("QuantidadeEncomendaGrupo"), ",", ".") + " ) ");

                    Lst_StrRastreabilidade.Seguinte();
                }
            }
            else
                CompletarComentarioRastreabilidade(true, FaturaFornecedor_AcabadoGerar, "", "", 0, DateTime.Now);

            //vba não retornava nada. Após conversa com um colega, definimos que retornava falso porque a função só é chamada e não é feita nenhuma validação sobre o estado dela
            return false;
        }
    }
}