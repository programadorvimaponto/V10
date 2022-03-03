using CmpBE100;
using ErpBS100;
using Microsoft.VisualBasic;
using StdBE100;
using System;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100;
using VndBE100;
using static BasBE100.BasBETiposGcp;

namespace Generico
{
    internal class Mdi_GeraDocumentoVenda
    {
        private static string[] IDLinhaDocOriginal = new string[1000];

        private static VndBEDocumentoVenda DocumentoNovo_Venda = new VndBEDocumentoVenda();

        public static bool GerarDocumento_BaseVendas(VndBE100.VndBEDocumentoVenda DocumentoBase, string BaseDadosDestino, string TipoDoc_Destino, string Serie_Destino, string Entidade_Destino, string Armazem_Destino, double ValorASomarArtigo)
        {
            try
            {
                if (!Module1.AbreEmpresa(BaseDadosDestino))
                    return false;

                DocumentoNovo_Venda.Filial = "000";
                DocumentoNovo_Venda.Serie = Serie_Destino;
                DocumentoNovo_Venda.Tipodoc = TipoDoc_Destino;
                DocumentoNovo_Venda.TipoEntidade = "C";
                DocumentoNovo_Venda.Entidade = Entidade_Destino;
                DocumentoNovo_Venda.Referencia = DocumentoBase.Referencia;
                DocumentoNovo_Venda.LocalCarga = DocumentoBase.LocalCarga;
                DocumentoNovo_Venda.LocalDescarga = DocumentoBase.LocalDescarga;
                DocumentoNovo_Venda.CamposUtil["CDU_DocumentoOrigem"].Valor = DocumentoBase.Tipodoc + " " + DocumentoBase.Serie + "/" + DocumentoBase.NumDoc;

                int preenche = 5;
                Module1.emp.Vendas.Documentos.PreencheDadosRelacionados(DocumentoNovo_Venda, ref preenche);

                DocumentoNovo_Venda.Moeda = DocumentoBase.Moeda;

                if (DocumentoBase.Entidade == "1207")
                {
                    DocumentoNovo_Venda.CargaDescarga.MoradaCarga = "Rua Comendador Manuel Gonçalves nº 25";
                    DocumentoNovo_Venda.CargaDescarga.Morada2Carga = "";
                    DocumentoNovo_Venda.CargaDescarga.LocalidadeCarga = "S Cosme do Vale";
                    DocumentoNovo_Venda.CargaDescarga.DistritoCarga = "03";
                    DocumentoNovo_Venda.CargaDescarga.PaisCarga = "PT";
                    DocumentoNovo_Venda.CargaDescarga.CodPostalCarga = "4770-583";
                    DocumentoNovo_Venda.CargaDescarga.CodPostalLocalidadeCarga = "V N Famalicão";
                }
                else
                {
                    DocumentoNovo_Venda.CargaDescarga.MoradaCarga = DocumentoBase.CargaDescarga.MoradaCarga;
                    DocumentoNovo_Venda.CargaDescarga.Morada2Carga = DocumentoBase.CargaDescarga.Morada2Carga;
                    DocumentoNovo_Venda.CargaDescarga.LocalidadeCarga = DocumentoBase.CargaDescarga.LocalidadeCarga;
                    DocumentoNovo_Venda.CargaDescarga.DistritoCarga = DocumentoBase.CargaDescarga.DistritoCarga;
                    DocumentoNovo_Venda.CargaDescarga.PaisCarga = DocumentoBase.CargaDescarga.PaisCarga;
                    DocumentoNovo_Venda.CargaDescarga.CodPostalCarga = DocumentoBase.CargaDescarga.CodPostalCarga;
                    DocumentoNovo_Venda.CargaDescarga.CodPostalLocalidadeCarga = DocumentoBase.CargaDescarga.CodPostalLocalidadeCarga;
                }

                DocumentoNovo_Venda.CargaDescarga.MoradaEntrega = DocumentoBase.CargaDescarga.MoradaEntrega;
                DocumentoNovo_Venda.CargaDescarga.Morada2Entrega = DocumentoBase.CargaDescarga.Morada2Entrega;
                DocumentoNovo_Venda.CargaDescarga.LocalidadeEntrega = DocumentoBase.CargaDescarga.LocalidadeEntrega;
                DocumentoNovo_Venda.CargaDescarga.DistritoEntrega = DocumentoBase.CargaDescarga.DistritoEntrega;
                DocumentoNovo_Venda.CargaDescarga.CodPostalEntrega = DocumentoBase.CargaDescarga.CodPostalEntrega;
                DocumentoNovo_Venda.CargaDescarga.CodPostalLocalidadeEntrega = DocumentoBase.CargaDescarga.CodPostalLocalidadeEntrega;

                if (TipoDoc_Destino == "GR" && DocumentoBase.Tipodoc == "GR")
                {
                    string TipoDocOrigem = "";
                    string SerieOrigem = "";
                    int NumDocOrigem = 0;

                    // JFC 03/09/2019
                    string TipoDocFinal = "";
                    string SerieFinal = "";
                    int NumDocFinal = 0;

                    string Str_MoradaDescarga;
                    StdBELista Lst_MoradaDescarga;

                    Str_MoradaDescarga = "SELECT top 1 CD2.TipoDoc as TipoDocEncomenda,  " + "       CD2.NumDoc as NumDocEncomenda, " + "       CD2.serie as SerieEncomenda " + " FROM CabecDoc  CD " + " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60 " + " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " + " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " + " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " + " WHERE  CD.Filial = '000' AND CD.TipoDoc = '" + DocumentoBase.Tipodoc + "' AND CD.serie = '" + DocumentoBase.Serie + "' AND CD.NumDoc = " + DocumentoBase.NumDoc + " " + " ORDER BY LD.NumLinha";

                    Lst_MoradaDescarga = PriV100Api.BSO.Consulta(Str_MoradaDescarga);

                    Lst_MoradaDescarga.Inicio();

                    while (!Lst_MoradaDescarga.NoFim())
                    {
                        Application.DoEvents();

                        TipoDocOrigem = Lst_MoradaDescarga.Valor("TipoDocEncomenda");
                        SerieOrigem = Lst_MoradaDescarga.Valor("SerieEncomenda");
                        NumDocOrigem = Lst_MoradaDescarga.Valor("NumDocEncomenda");

                        Lst_MoradaDescarga.Seguinte();
                    }

                    Str_MoradaDescarga = "select cd.CDU_DocumentoVendaDestino as 'Str' " + "from CabecDoc cd " + "where cd.TipoDoc='" + TipoDocOrigem + "' and cd.NumDoc='" + NumDocOrigem + "' and cd.Serie='" + SerieOrigem + "'";

                    Lst_MoradaDescarga = PriV100Api.BSO.Consulta(Str_MoradaDescarga);

                    Lst_MoradaDescarga.Inicio();

                    while (!Lst_MoradaDescarga.NoFim())
                    {
                        Application.DoEvents();
                        int PosBarra;
                        int PosEsp;

                        // JFC 04/09/2019 - Identificar a posição da barra e do espaço (TipoDoc Serie/NumDoc)
                        PosBarra = Strings.InStr(1, Lst_MoradaDescarga.Valor("Str"), "/", Constants.vbTextCompare);
                        PosEsp = Strings.InStr(1, Lst_MoradaDescarga.Valor("Str"), " ", Constants.vbTextCompare);

                        TipoDocFinal = Strings.Left(Lst_MoradaDescarga.Valor("Str"), PosEsp - 1);
                        SerieFinal = Strings.Mid(Lst_MoradaDescarga.Valor("Str"), PosEsp + 1, PosBarra - PosEsp - 1);
                        NumDocFinal = Strings.Mid(Lst_MoradaDescarga.Valor("Str"), PosBarra + 1);

                        Lst_MoradaDescarga.Seguinte();
                    }

                    VndBEDocumentoVenda DocumentoBase_Encomenda = new VndBEDocumentoVenda();

                    DocumentoBase_Encomenda = Module1.emp.Vendas.Documentos.Edita("000", TipoDocFinal, SerieFinal, NumDocFinal);

                    DocumentoNovo_Venda.CargaDescarga.MoradaEntrega = Interaction.IIf(Strings.Len(DocumentoBase_Encomenda.CargaDescarga.MoradaEntrega) > 0, DocumentoBase_Encomenda.CargaDescarga.MoradaEntrega, " . ").ToString();
                    DocumentoNovo_Venda.CargaDescarga.Morada2Entrega = DocumentoBase_Encomenda.CargaDescarga.Morada2Entrega;
                    DocumentoNovo_Venda.CargaDescarga.LocalidadeEntrega = Interaction.IIf(Strings.Len(DocumentoBase_Encomenda.CargaDescarga.LocalidadeEntrega) > 0, DocumentoBase_Encomenda.CargaDescarga.LocalidadeEntrega, " . ").ToString();
                    DocumentoNovo_Venda.CargaDescarga.DistritoEntrega = DocumentoBase_Encomenda.CargaDescarga.DistritoEntrega;
                    DocumentoNovo_Venda.CargaDescarga.PaisEntrega = DocumentoBase_Encomenda.CargaDescarga.PaisEntrega;
                    DocumentoNovo_Venda.CargaDescarga.CodPostalEntrega = Interaction.IIf(Strings.Len(DocumentoBase_Encomenda.CargaDescarga.CodPostalEntrega) > 0, DocumentoBase_Encomenda.CargaDescarga.CodPostalEntrega, "0000-000").ToString();
                    DocumentoNovo_Venda.CargaDescarga.CodPostalLocalidadeEntrega = Interaction.IIf(Strings.Len(DocumentoBase_Encomenda.CargaDescarga.CodPostalLocalidadeEntrega) > 0, DocumentoBase_Encomenda.CargaDescarga.CodPostalLocalidadeEntrega, " . ").ToString();

                    DocumentoNovo_Venda.ModoExp = DocumentoBase.ModoExp;
                    DocumentoNovo_Venda.Matricula = DocumentoBase.Matricula;
                    DocumentoNovo_Venda.CamposUtil["CDU_NumCarga"].Valor = DocumentoBase.CamposUtil["CDU_NumCarga"].Valor;
                    DocumentoNovo_Venda.LocalDescarga = Interaction.IIf(Strings.Len(DocumentoBase.LocalDescarga) > 0, DocumentoBase.LocalDescarga, " . ").ToString();

                    DocumentoNovo_Venda.ModoPag = DocumentoBase_Encomenda.ModoPag;
                    DocumentoNovo_Venda.CondPag = DocumentoBase_Encomenda.CondPag;
                    DocumentoNovo_Venda.Moeda = DocumentoBase_Encomenda.Moeda;
                    DocumentoNovo_Venda.ContaDomiciliacao = DocumentoBase_Encomenda.ContaDomiciliacao;
                    DocumentoNovo_Venda.Responsavel = DocumentoBase_Encomenda.Responsavel;

                    if (DocumentoBase_Encomenda.MoradaAlternativaEntrega != "")
                    {
                        DocumentoNovo_Venda.MoradaAlternativaEntrega = DocumentoBase_Encomenda.MoradaAlternativaEntrega;
                        DocumentoNovo_Venda.UtilizaMoradaAlternativaEntreg = true;
                    }
                }
                else
                {
                    if (DocumentoBase.CamposUtil["CDU_MoradaAlternativa"].Valor + "" != "")
                    {
                        DocumentoNovo_Venda.MoradaAlternativaEntrega = DocumentoBase.CamposUtil["CDU_MoradaAlternativa"].Valor + "";
                        // #13/11/2020 - JFC Pais de descarga não estava assumir correctamente nas Encomendas (principalmente quando o clientes era estrangeiro e o local de descarga era PT)
                        DocumentoNovo_Venda.CargaDescarga.PaisEntrega = Module1.emp.Base.MoradasAlternativas.DaValorAtributo("C", DocumentoNovo_Venda.Entidade, DocumentoBase.CamposUtil["CDU_MoradaAlternativa"].Valor.ToString(), "Pais");
                        DocumentoNovo_Venda.CargaDescarga.MoradaEntrega = Module1.emp.Base.MoradasAlternativas.Edita("C", DocumentoNovo_Venda.Entidade, DocumentoBase.CamposUtil["CDU_MoradaAlternativa"].Valor.ToString()).Morada;
                        DocumentoNovo_Venda.UtilizaMoradaAlternativaEntreg = true;
                    }
                }
                DocumentoNovo_Venda.DataDoc = DocumentoBase.DataDoc;
                int i;
                int j = 2;

                string Artigo;
                double Quantidade;
                string Armazem;
                string Localizacao;

                Module1.emp.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoNovo_Venda, BasBE100.BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: "@1@ @2@/N.º@3@ de @4@");

                for (i = 1; i <= DocumentoBase.Linhas.NumItens; i++)
                {
                    if (DocumentoBase.Linhas.GetEdita(i).TipoLinha == "60")
                    {
                        if (Strings.InStr(1, DocumentoBase.Linhas.GetEdita(i).Descricao, "/N.º") == 0)
                        {
                            Module1.emp.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoNovo_Venda, BasBE100.BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: DocumentoBase.Linhas.GetEdita(i).Descricao);
                            IDLinhaDocOriginal[j] = DocumentoBase.Linhas.GetEdita(i).IdLinha + ";" + DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha;
                            j = j + 1;
                        }
                    }
                    else
                    {
                        Artigo = DocumentoBase.Linhas.GetEdita(i).Artigo;
                        if (!Module1.emp.Base.Artigos.Existe(Artigo))
                        {
                            MessageBox.Show("O Artigo " + Artigo + " não existe na Empresa " + BaseDadosDestino, "Artigo não existente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        VerificaLote(Artigo, DocumentoBase.Linhas.GetEdita(i).Lote);
                        Quantidade = DocumentoBase.Linhas.GetEdita(i).Quantidade;

                        // Se o armazem dos parametros preenchido, sua esse. Caso contrário, usa o das linhas
                        Armazem = Interaction.IIf(Strings.Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Armazem).ToString();
                        Localizacao = Interaction.IIf(Strings.Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Localizacao).ToString();

                        // Armazem = DocumentoBase.Linhas(i).Armazem
                        // Localizacao = DocumentoBase.Linhas(i).Localizacao

                        Module1.emp.Vendas.Documentos.AdicionaLinha(DocumentoNovo_Venda, Artigo, ref Quantidade, ref Armazem, ref Localizacao);
                        DocumentoNovo_Venda.Linhas.GetEdita(j).Descricao = DocumentoBase.Linhas.GetEdita(i).Descricao;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).Lote = DocumentoBase.Linhas.GetEdita(i).Lote;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).Unidade = DocumentoBase.Linhas.GetEdita(i).Unidade;
                        // DocumentoNovo_Venda.Linhas(j).Armazem = DocumentoBase.Linhas(i).Armazem
                        // DocumentoNovo_Venda.Linhas(j).Localizacao = DocumentoBase.Linhas(i).Localizacao

                        // Deixar a gestão do iva ao encargo do Primavera
                        // DocumentoNovo_Venda.Linhas(j).CodIva = DocumentoBase.Linhas(i).CodIva
                        // DocumentoNovo_Venda.Linhas(j).TaxaIva = DocumentoBase.Linhas(i).TaxaIva

                        DocumentoNovo_Venda.Linhas.GetEdita(j).PrecUnit = DocumentoBase.Linhas.GetEdita(i).PrecUnit + ValorASomarArtigo;
                        // DocumentoNovo_Venda.Linhas(j).DescontoComercial = DocumentoBase.Linhas(i).DescontoComercial
                        // DocumentoNovo_Venda.Linhas(j).Desconto1 = DocumentoBase.Linhas(i).Desconto1
                        // DocumentoNovo_Venda.Linhas(j).Desconto2 = DocumentoBase.Linhas(i).Desconto2
                        // DocumentoNovo_Venda.Linhas(j).Desconto3 = DocumentoBase.Linhas(i).Desconto3
                        DocumentoNovo_Venda.Linhas.GetEdita(j).MovStock = DocumentoBase.Linhas.GetEdita(i).MovStock;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).DataStock = DocumentoBase.Linhas.GetEdita(i).DataStock;
                        DocumentoNovo_Venda.Linhas.GetEdita(j).DataEntrega = DocumentoBase.Linhas.GetEdita(i).DataEntrega;

                        // JFC 06/11/2017 Preço Base para analise de comições mercado externo
                        DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil["CDU_PrecoBase"].Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil["CDU_PrecoBase"].Valor;

                        // JFC 07/11/2017 Não estava a sair nas Guias o Tipo Qualidade. Acrescentei esta linha.
                        DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil["CDU_TipoQualidade"].Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor + "";

                        // JFC 28/02/2019 CDU_Cambio nas Encomendas a Cliente.
                        DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil["CDU_Cambio"].Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil["CDU_Cambio"].Valor;

                        // JFC 11/09/2020 CDU_PrecTab nas Encomendas a Cliente.
                        DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil["CDU_PrecTab"].Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil["CDU_PrecTab"].Valor;

                        // Email JC sex 12/05/2017 09:38
                        if (Strings.UCase(BaseDadosDestino) == "INOVAFIL")
                        {
                            DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil["CDU_DataEntregaCliente"].Valor = DocumentoBase.Linhas.GetEdita(i).DataEntrega;
                        }

                        // 20170921
                        if (TipoDoc_Destino == "GR" & DocumentoBase.Tipodoc == "GR")
                        {
                            DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil["CDU_Observacoes"].Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil["CDU_Observacoes"].Valor + "";
                        }

                        // Para garantir a rastreabilidade
                        // DocumentoNovo_Venda.Linhas(j).CamposUtil("CDU_IDLinhaOriginalPercato").Valor = DocumentoCompra.Linhas(j).IDLinha
                        IDLinhaDocOriginal[j] = DocumentoBase.Linhas.GetEdita(i).IdLinha + ";" + DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha;
                        j = j + 1;
                    }
                }

                Module1.emp.Vendas.Documentos.Actualiza(DocumentoNovo_Venda);

                if (DocumentoNovo_Venda.Tipodoc == "GR" & DateAndTime.DateDiff("d", DocumentoNovo_Venda.DataDoc, DocumentoNovo_Venda.DataVenc) < 9L)
                {
                    string VarCliente;
                    string VarFrom;
                    string VarTo;
                    string VarAssunto;
                    string VarTextoInicialMsg;
                    string VarMensagem;
                    string VarUtilizador;
                    VarCliente = DocumentoNovo_Venda.Entidade;
                    VarFrom = "";
                    VarTo = "informatica@mundifios.pt; tesouraria.clientes@mundifios.pt; faturacao@mundifios.pt";
                    if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
                    {
                        VarTextoInicialMsg = "Bom dia,";
                    }
                    else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
                    {
                        VarTextoInicialMsg = "Boa tarde,";
                    }
                    else
                    {
                        VarTextoInicialMsg = "Boa noite,";
                    }

                    Module1.AbreEmpresa(BaseDadosDestino);
                    VarAssunto = "Emitir Fatura: " + DocumentoNovo_Venda.Tipodoc + " " + Strings.Format(DocumentoNovo_Venda.NumDoc, "####") + "/" + DocumentoNovo_Venda.Serie + " (" + Strings.Replace(PriV100Api.BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + ")";
                    VarUtilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                    VarMensagem = VarTextoInicialMsg + '\r' + '\r' + '\r' + "Foi emitido uma Guia com prazo de pagamento inferior ou igual a 8 dias, por favor emita a respetiva fatura:" + '\r' + '\r' + "" + "Empresa:                         " + BaseDadosDestino + '\r' + "" + "Utilizador:                      " + VarUtilizador + '\r' + '\r' + "" + "Cliente:                         " + VarCliente + " - " + Strings.Replace(PriV100Api.BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + '\r' + "" + "Documento:                       " + DocumentoNovo_Venda.Tipodoc + " " + Strings.Format(DocumentoNovo_Venda.NumDoc, "#,###") + "/" + DocumentoNovo_Venda.Serie + '\r' + '\r' + "" + "Data Vencimento:                 " + DocumentoNovo_Venda.DataVenc + '\r' + '\r' + "" + "Local Descarga:                  " + DocumentoNovo_Venda.LocalDescarga + '\r' + "" + "Morada Entrega:                  " + Strings.Replace(DocumentoNovo_Venda.MoradaEntrega, "'", "") + '\r' + '\r' + "" + "Cumprimentos";

                    Module1.FechaEmpresa();

                    PriV100Api.BSO.DSO.ExecuteSQL("INSERT INTO priempre.dbo.MENSAGENSEMAIL ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateAndTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
                }

                for (int k = 1; k <= DocumentoNovo_Venda.Linhas.NumItens; k++)
                {
                    if (Strings.InStr(1, IDLinhaDocOriginal[k], ";") > 0 & Strings.Len(Strings.Mid(IDLinhaDocOriginal[k], Strings.InStr(1, IDLinhaDocOriginal[k], ";") + 1, Strings.Len(IDLinhaDocOriginal[k]))) > 0)
                    {
                        Module1.emp.DSO.ExecuteSQL("update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" + Strings.Mid(IDLinhaDocOriginal[k], 1, Strings.InStr(1, IDLinhaDocOriginal[k], ";") - 1) + "' where  id = '" + Strings.Mid(IDLinhaDocOriginal[k], Strings.InStr(1, IDLinhaDocOriginal[k], ";") + 1, Strings.Len(IDLinhaDocOriginal[k])) + "' ");
                    }
                }

                try
                {
                    PriV100Api.BSO.DSO.ExecuteSQL(" UPDATE CabecDoc " + " SET CDU_DocumentoVendaDestino = '" + DocumentoNovo_Venda.Tipodoc + " " + DocumentoNovo_Venda.Serie + "/" + DocumentoNovo_Venda.NumDoc + "' " + " where filial = '" + DocumentoBase.Filial + "' and  TipoDoc = '" + DocumentoBase.Tipodoc + "' and Serie = '" + DocumentoBase.Serie + "' and NumDoc = " + DocumentoBase.NumDoc + " ");

                    // 20170921
                    Rastreabilidade(DocumentoBase.Filial, DocumentoBase.Serie, DocumentoBase.Tipodoc, DocumentoBase.NumDoc, BaseDadosDestino, DocumentoNovo_Venda);
                    MessageBox.Show("Foi gerado o documento de Venda " + DocumentoNovo_Venda.Tipodoc + " " + DocumentoNovo_Venda.Serie + "/" + DocumentoNovo_Venda.NumDoc + " na empresa " + BaseDadosDestino + " com a Entidade " + DocumentoNovo_Venda.Entidade, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
                catch
                {
                    MessageBox.Show("Problemas na actualização do Num. documento de destino", "Gera Documento Base Vendas - TrataErroUpdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.ToString(), "Encomenda de Cliente ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void VerificaLote(string str_Artigo, string str_Lote)
        {
            if (str_Lote == string.Empty)
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
                    if (Strings.Len(stdBE_ListaLote.Valor("DataFabrico")) > 0) ArtigoLote.DataFabrico = stdBE_ListaLote.Valor("DataFabrico");
                    if (Strings.Len(stdBE_ListaLote.Valor("Validade")) > 0) ArtigoLote.Validade = stdBE_ListaLote.Valor("Validade");
                    ArtigoLote.Controlador = stdBE_ListaLote.Valor("Controlador");
                    ArtigoLote.Activo = stdBE_ListaLote.Valor("Activo");
                    ArtigoLote.Observacoes = stdBE_ListaLote.Valor("Observacoes");
                    ArtigoLote.CamposUtil["CDU_TipoQualidade"].Valor = stdBE_ListaLote.Valor("CDU_TipoQualidade");
                    ArtigoLote.CamposUtil["CDU_Parafinado"].Valor = stdBE_ListaLote.Valor("CDU_Parafinado");
                    Module1.emp.Inventario.ArtigosLotes.Actualiza(ArtigoLote);
                }
            }
        }

        private static bool Rastreabilidade(string Filial_FaturaCliente, string Serie_FaturaCliente, string TipoDoc_FaturaCliente, long NumDoc_FaturaCliente, string BaseDadosDestino, VndBEDocumentoVenda FaturaClienteFinal_AcabadoGerar)
        {
            string Str_Rastreabilidade;
            StdBELista Lst_StrRastreabilidade;
            string ErroComunica = "";
            Str_Rastreabilidade = "  SELECT CD2.TipoDoc as TipoDocEncomenda, CD2.NumDoc as NumDocEncomenda, CD2.Id as IdCabecDocEncomenda, LD2.Id as IdLinhasDocEncomenda, " + " CD.TipoDoc as TipoDocFatura, CD.NumDoc as NumDocFatura , CD.Id as IdCabecDocFatura,  LD.Id as IdLinhasDocFatura , " + " CDGrupo.TipoDoc as TipoDocEncomendaGrupo, CDGrupo.NumDoc as NumDocEncomendaGrupo ,  CDGrupo.Serie as SerieEncomendaGrupo , replace(CONVERT(VARCHAR(10),   CDGrupo.Data, 102),'.','/') as DataDocEncomendaGrupo ,   CDGrupo.Id as IdCabecDocEncomendaGrupo,  LDGrupo.Id as IdLinhasDocEncomendaGrupo , LDGrupo.Quantidade  as QuantidadeEncomendaGrupo ," + " CDGrupoVFA.TipoDoc as TipoDocFaturaGrupo, CDGrupoVFA.NumDoc as NumDocFaturaGrupo, CDGrupoVFA.Id as IdCabecDocFaturaGrupo, LDGrupoVFA.Id as IdLinhasDocFaturaGrupo " + " FROM CabecDoc  CD " + " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60" + " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " + " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " + " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.LinhasDoc LDGrupo ON LDGrupo.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD2.id) + '}' " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.CabecDoc CDGrupo ON CDGrupo.id = LDGrupo.IdCabecDoc " + " " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.LinhasDoc LDGrupoVFA ON LDGrupoVFA.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD.id) + '}' " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.CabecDoc CDGrupoVFA ON CDGrupoVFA.id = LDGrupoVFA.IdCabecDoc  " + " " + " WHERE CDGrupo.TipoDoc not in ('FP') and CD.Filial = '" + Filial_FaturaCliente + "' AND CD.TipoDoc = '" + TipoDoc_FaturaCliente + "' AND CD.serie = '" + Serie_FaturaCliente + "' AND CD.NumDoc = " + NumDoc_FaturaCliente + " " + " ORDER BY LD.NumLinha ";

            Lst_StrRastreabilidade = PriV100Api.BSO.Consulta(Str_Rastreabilidade);

            if (Lst_StrRastreabilidade.Vazia() == false)
            {
                Lst_StrRastreabilidade.Inicio();

                CompletarComentarioRastreabilidade(false, FaturaClienteFinal_AcabadoGerar, Lst_StrRastreabilidade.Valor("TipoDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("SerieEncomendaGrupo"), Lst_StrRastreabilidade.Valor("NumDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("DataDocEncomendaGrupo"));

                while (!Lst_StrRastreabilidade.NoFim())
                {
                    Application.DoEvents();
                    // IdLinhasDoc -> Fatura de Fornecedor do Grupo
                    // IdLinhasDocOrigem -> Encomenda de Fornecedor

                    Module1.emp.DSO.ExecuteSQL(" INSERT INTO LinhasDocTrans " + " (IdLinhasDoc,IdLinhasDocOrigem,QuantTrans) " + " VALUES( '" + Lst_StrRastreabilidade.Valor("IdLinhasDocFaturaGrupo") + "','" + Lst_StrRastreabilidade.Valor("IdLinhasDocEncomendaGrupo") + "'," + Strings.Replace(Lst_StrRastreabilidade.Valor("QuantidadeEncomendaGrupo"), ",", ".") + " ) ");
                    Lst_StrRastreabilidade.Seguinte();
                }

                if (PriV100Api.BSO.Base.Series.Edita("V", DocumentoNovo_Venda.Tipodoc, DocumentoNovo_Venda.Serie).TipoComunicacao == 2)
                    PriV100Api.BSO.Internos.Documentos.ATComunicaDocumentoId(DocumentoNovo_Venda.ID, "V", ref ErroComunica);

                if (ErroComunica != "")
                    MessageBox.Show(ErroComunica, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                CompletarComentarioRastreabilidade(true, FaturaClienteFinal_AcabadoGerar, "", "", 0, DateAndTime.Now);

            //vba não retornava nada. Após conversa com um colega, definimos que retornava falso porque a função só é chamada e não é feita nenhuma validação sobre o estado dela
            return false;
        }

        private static void CompletarComentarioRastreabilidade(bool Apagar, VndBEDocumentoVenda FaturaClienteFinal_AcabadoGerar, string Tipodoc, string Serie, long NumDoc, DateTime Data)
        {
            FaturaClienteFinal_AcabadoGerar = Module1.emp.Vendas.Documentos.Edita(FaturaClienteFinal_AcabadoGerar.Filial, FaturaClienteFinal_AcabadoGerar.Tipodoc, FaturaClienteFinal_AcabadoGerar.Serie, FaturaClienteFinal_AcabadoGerar.NumDoc);

            if (Apagar == false)
            {
                if (FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).TipoLinha == "60")
                {
                    if (FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao == "@1@ @2@/N.º@3@ de @4@")
                    {
                        FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = Strings.Replace(FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@1@", Tipodoc);
                        FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = Strings.Replace(FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@2@", Serie);
                        FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = Strings.Replace(FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@3@", NumDoc.ToString());
                        FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = Strings.Replace(FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@4@", Data.ToString());
                    }
                }
            }
            else
            {
                if (FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).TipoLinha == "60")
                {
                    if (FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao == "@1@ @2@/N.º@3@ de @4@")
                    {
                        FaturaClienteFinal_AcabadoGerar.Linhas.Remove(1);
                    }
                }
            }

            Module1.emp.Vendas.Documentos.Actualiza(FaturaClienteFinal_AcabadoGerar);
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

                //Condições
                DocumentoNovo_Venda.ModoPag = DocumentoBase.ModoPag;
                DocumentoNovo_Venda.CondPag = DocumentoBase.CondPag;
                DocumentoNovo_Venda.Moeda = DocumentoBase.Moeda;
                DocumentoNovo_Venda.ContaDomiciliacao = DocumentoBase.ContaDomiciliacao;
                DocumentoNovo_Venda.Responsavel = DocumentoBase.Responsavel;
                int preenche = 5;   
                Module1.emp.Vendas.Documentos.PreencheDadosRelacionados(DocumentoNovo_Venda,ref preenche);
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
                            Module1.emp.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoNovo_Venda, vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: DocumentoBase.Linhas.GetEdita(i).Descricao);
                            IDLinhaDocOriginal[j] = DocumentoBase.Linhas.GetEdita(i).IdLinha + ";" + DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha;
                            j = j + 1;
                        }
                    }
                    else
                    {
                        Artigo = DocumentoBase.Linhas.GetEdita(i).Artigo;
                        if (!Module1.emp.Base.Artigos.Existe(Artigo))
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

                        Module1.emp.Vendas.Documentos.AdicionaLinha(DocumentoNovo_Venda, Artigo, ref Quantidade, ref Armazem, ref Localizacao);
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

                Module1.emp.Vendas.Documentos.Actualiza(DocumentoNovo_Venda);
                int k;
                var loopTo1 = DocumentoNovo_Venda.Linhas.NumItens;
                for (k = 1; k <= loopTo1; k++)
                {
                    // If DocumentoNovo_Venda.Linhas(k).TipoLinha = 60 And InStr(1, DocumentoNovo_Venda.Linhas(k).Descricao, "/N.º") > 0 Then
                    // ObjMotor.DSO.BDAPL.Execute "update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" & IDLinhaDocOriginal(k) & "' where id = '" & DocumentoNovo_Venda.Linhas(k).IdLinha & "' "
                    // Else
                    //
                    // End If

                    if (Strings.InStr(1, IDLinhaDocOriginal[k], ";") > 0 & Strings.Len(Strings.Mid(IDLinhaDocOriginal[k], Strings.InStr(1, IDLinhaDocOriginal[k], ";") + 1, Strings.Len(IDLinhaDocOriginal[k]))) > 0)
                    {
                        Module1.emp.DSO.ExecuteSQL("update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" + Strings.Mid(IDLinhaDocOriginal[k], 1, Strings.InStr(1, IDLinhaDocOriginal[k], ";") - 1) + "' where  id = '" + Strings.Mid(IDLinhaDocOriginal[k], Strings.InStr(1, IDLinhaDocOriginal[k], ";") + 1, Strings.Len(IDLinhaDocOriginal[k])) + "' ");
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
                MessageBox.Show("Erro: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

      

    }
}