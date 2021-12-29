Imports Vimaponto.PrimaveraV100
Imports BasBE100.BasBETiposGcp
Imports StdBE100
Imports System.Windows.Forms

Public Class Mdi_GeraDocumentoCompra

    Shared IDLinhaDocOriginal(999) As String
    Private Const TipoEmpresa As Integer = 0

    Private Shared Sub FechaEmpresa()
        PriV100Api.BSO.FechaEmpresaTrabalho()
    End Sub


    Private Shared Function AbreEmpresa(ByVal Empresa As String) As Boolean

        On Error GoTo TrataErro

        PriV100Api.BSO.AbreEmpresaTrabalho(TipoEmpresa, Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual)
        AbreEmpresa = True
        Exit Function

TrataErro:
        AbreEmpresa = False
    End Function

    Public Shared Function GerarDocumento_BaseVendas(ByVal DocumentoBase As VndBE100.VndBEDocumentoVenda,
                            ByVal BaseDadosDestino As String,
                            ByVal TipoDoc_Destino As String,
                            ByVal Serie_Destino As String,
                            ByVal Entidade_Destino As String,
                            ByVal Armazem_Destino As String) As Boolean


        On Error GoTo TrataErro


        If Not AbreEmpresa(BaseDadosDestino) Then GerarDocumento_BaseVendas = False : Exit Function

        'Identifica o Documento acabado de Criar (Encomenda a Cliente)
        'IdentificarDocumentoVenda Filial_Origem, Serie_Origem, TipoDoc_Origem, NumDoc_Origem

        Dim DocumentoCompra = New CmpBE100.CmpBEDocumentoCompra

        DocumentoCompra.Filial = "000"

        'Caso a empresa de destino seja a Mixyarn, a serie é a 2020Z pois a 2020X estava a ser utilizada como Emissivel. JFC - 02/06/2020
        '    If DocumentoBase.Entidade = "2492" And DocumentoBase.Tipodoc = "ECL" Then
        '    DocumentoCompra.Serie = "2020Z"
        '    Else
        DocumentoCompra.Serie = Serie_Destino
        '    End If
        'DocCompra consta nos Campos de utilizador
        DocumentoCompra.Tipodoc = TipoDoc_Destino
        'Fornecedor
        DocumentoCompra.TipoEntidade = "F"
        'CodFornecedor consta nos Campos de utilizador
        DocumentoCompra.Entidade = Entidade_Destino

        ' documento original
        DocumentoCompra.NumDocExterno = DocumentoBase.Tipodoc & " " & DocumentoBase.Serie & "/" & DocumentoBase.NumDoc 'DocumentoBase.Referencia
        If Len(DocumentoCompra.NumDocExterno) = 0 Then DocumentoCompra.NumDocExterno = 0
        DocumentoCompra.LocalDescarga = DocumentoBase.LocalDescarga
        DocumentoCompra.LocalCarga = DocumentoBase.LocalCarga
        DocumentoCompra.CamposUtil("CDU_DocumentoOrigem").Valor = DocumentoBase.Tipodoc & " " & DocumentoBase.Serie & "/" & DocumentoBase.NumDoc
        DocumentoCompra.MoradaEntrega = DocumentoBase.MoradaEntrega


        PriV100Api.BSO.Compras.Documentos.PreencheDadosRelacionados(DocumentoCompra)

        'Carga
        DocumentoCompra.CargaDescarga.MoradaCarga = DocumentoBase.CargaDescarga.MoradaCarga
        DocumentoCompra.CargaDescarga.Morada2Carga = DocumentoBase.CargaDescarga.Morada2Carga
        DocumentoCompra.CargaDescarga.LocalidadeCarga = DocumentoBase.CargaDescarga.LocalidadeCarga
        DocumentoCompra.CargaDescarga.DistritoCarga = DocumentoBase.CargaDescarga.DistritoCarga
        DocumentoCompra.CargaDescarga.PaisCarga = DocumentoBase.CargaDescarga.PaisCarga
        DocumentoCompra.CargaDescarga.CodPostalCarga = DocumentoBase.CargaDescarga.CodPostalCarga
        DocumentoCompra.CargaDescarga.CodPostalLocalidadeCarga = DocumentoBase.CargaDescarga.CodPostalLocalidadeCarga

        'Descarga
        DocumentoCompra.CargaDescarga.MoradaEntrega = DocumentoBase.CargaDescarga.MoradaEntrega
        DocumentoCompra.CargaDescarga.Morada2Entrega = DocumentoBase.CargaDescarga.Morada2Entrega
        DocumentoCompra.CargaDescarga.LocalidadeEntrega = DocumentoBase.CargaDescarga.LocalidadeEntrega
        DocumentoCompra.CargaDescarga.DistritoEntrega = DocumentoBase.CargaDescarga.DistritoEntrega
        DocumentoCompra.CargaDescarga.PaisEntrega = DocumentoBase.CargaDescarga.PaisEntrega
        DocumentoCompra.CargaDescarga.CodPostalEntrega = DocumentoBase.CargaDescarga.CodPostalEntrega
        DocumentoCompra.CargaDescarga.CodPostalLocalidadeEntrega = DocumentoBase.CargaDescarga.CodPostalLocalidadeEntrega

        'Condições
        DocumentoCompra.ModoPag = DocumentoBase.ModoPag
        DocumentoCompra.CondPag = DocumentoBase.CondPag
        DocumentoCompra.Moeda = DocumentoBase.Moeda
        DocumentoCompra.ContaDomiciliacao = DocumentoBase.ContaDomiciliacao
        DocumentoCompra.Responsavel = DocumentoBase.Responsavel


        'Tem de estar depois dos dados relazionados!! porque senao sugere a data do sistema e altera a data do documento
        DocumentoCompra.DataDoc = DocumentoBase.DataDoc
        'se não colocar isto, nao consigo gravar documentos de uma série diferente à serie actual, praticada na data do sistema
        'DocumentoCompra.DataIntroducao = DocumentoCompra.DataDoc


        PriV100Api.BSO.Compras.Documentos.PreencheDadosRelacionados(DocumentoCompra)

        Dim i As Long
        Dim j As Long
        Dim Artigo As String
        Dim Quantidade As Double
        Dim Armazem As String
        Dim Localizacao As String

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '@1@ @2@/N.º@3@ de @4@
        PriV100Api.BSO.Compras.Documentos.AdicionaLinhaEspecial(DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, Descricao:="@1@ @2@/N.º@3@ de @4@")
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'j = 1
        j = 2
        For i = 1 To DocumentoBase.Linhas.NumItens
            'Adicionar a linha ao documento
            If DocumentoBase.Linhas.GetEdita(i).TipoLinha = 60 Then


                If InStr(1, DocumentoBase.Linhas.GetEdita(i).Descricao, "/N.º") = 0 Then

                    PriV100Api.BSO.Compras.Documentos.AdicionaLinhaEspecial(DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, Descricao:=DocumentoBase.Linhas.GetEdita(i).Descricao)
                    IDLinhaDocOriginal(j) = DocumentoBase.Linhas.GetEdita(i).IdLinha & ";" & DocumentoCompra.Linhas.GetEdita(j).IdLinha
                    j = j + 1

                End If

            Else

                Artigo = DocumentoBase.Linhas.GetEdita(i).Artigo

                VerificaLote(Artigo, DocumentoBase.Linhas.GetEdita(i).Lote)

                Quantidade = DocumentoBase.Linhas.GetEdita(i).Quantidade

                Armazem = IIf(Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Armazem)
                Localizacao = IIf(Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Localizacao)

                PriV100Api.BSO.Compras.Documentos.AdicionaLinha(DocumentoCompra, Artigo, Quantidade, Armazem, Localizacao)

                DocumentoCompra.Linhas.GetEdita(j).Descricao = DocumentoBase.Linhas.GetEdita(i).Descricao
                DocumentoCompra.Linhas.GetEdita(j).Lote = DocumentoBase.Linhas.GetEdita(i).Lote
                DocumentoCompra.Linhas.GetEdita(j).Unidade = DocumentoBase.Linhas.GetEdita(i).Unidade
                'DocumentoCompra.Linhas(j).Armazem = DocumentoBase.Linhas(i).Armazem
                'DocumentoCompra.Linhas(j).Localizacao = DocumentoBase.Linhas(i).Localizacao

                'Deixar a gestão do iva ao encargo do Primavera
                'DocumentoCompra.Linhas(j).CodIva = DocumentoBase.Linhas(i).CodIva
                'DocumentoCompra.Linhas(j).TaxaIva = DocumentoBase.Linhas(i).TaxaIva

                DocumentoCompra.Linhas.GetEdita(j).PrecUnit = DocumentoBase.Linhas.GetEdita(i).PrecUnit

                DocumentoCompra.Linhas.GetEdita(j).MovStock = DocumentoBase.Linhas.GetEdita(i).MovStock
                DocumentoCompra.Linhas.GetEdita(j).DataStock = DocumentoBase.Linhas.GetEdita(i).DataStock
                DocumentoCompra.Linhas.GetEdita(j).DataEntrega = DocumentoBase.Linhas.GetEdita(i).DataEntrega

                IDLinhaDocOriginal(j) = DocumentoBase.Linhas.GetEdita(i).IdLinha & ";" & DocumentoCompra.Linhas.GetEdita(j).IdLinha
                j = j + 1

            End If


        Next

        PriV100Api.BSO.Compras.Documentos.Actualiza(DocumentoCompra)

        Dim k As Long
        For k = 1 To DocumentoCompra.Linhas.NumItens
            '        If DocumentoCompra.Linhas(k).TipoLinha = 60 And InStr(1, DocumentoCompra.Linhas(k).Descricao, "/N.º") > 0 Then
            '            'Não faz nada!
            '        Else
            '            'Atualizar nas LinhasCompras da empresa do Grupo o Id das LinhasDoc da empresa Origem
            '            PriV100Api.BSO.DSO.BDAPL.Execute "update LinhasCompras set CDU_IDLinhaOriginalGrupo = '" & IDLinhaDocOriginal(k) & "' where id = '" & DocumentoCompra.Linhas(k).IdLinha & "' "
            '        End If

            If InStr(1, IDLinhaDocOriginal(k), ";") > 0 And Len(Mid(IDLinhaDocOriginal(k), InStr(1, IDLinhaDocOriginal(k), ";") + 1, Len(IDLinhaDocOriginal(k)))) > 0 Then
                PriV100Api.BSO.DSO.ExecuteSQL("update LinhasCompras set CDU_IDLinhaOriginalGrupo = '" & Mid(IDLinhaDocOriginal(k), 1, InStr(1, IDLinhaDocOriginal(k), ";") - 1) & "' where id = '" & Mid(IDLinhaDocOriginal(k), InStr(1, IDLinhaDocOriginal(k), ";") + 1, Len(IDLinhaDocOriginal(k))) & "' ")
            End If

        Next k



        On Error GoTo TrataErroUpdate

        'Colocar no documento origem o valor do documento acabado de criar na empresa de Grupo
        PriV100Api.BSO.DSO.ExecuteSQL(" update CabecDoc " &
                             " set CDU_DocumentoCompraDestino = '" & DocumentoCompra.Tipodoc & " " & DocumentoCompra.Serie & "/" & DocumentoCompra.NumDoc & "' " &
                              " where filial = '" & DocumentoBase.Filial & "' and  TipoDoc = '" & DocumentoBase.Tipodoc & "' and Serie = '" & DocumentoBase.Serie & "' and NumDoc = " & DocumentoBase.NumDoc & " ")


        Rastreabilidade(DocumentoBase.Filial, DocumentoBase.Serie, DocumentoBase.Tipodoc, DocumentoBase.NumDoc, BaseDadosDestino, DocumentoCompra)

        MsgBox("Foi gerado o documento de Compra " & DocumentoCompra.Tipodoc & " " & DocumentoCompra.Serie & "/" & DocumentoCompra.NumDoc & " na empresa " & BaseDadosDestino & " com a Entidade " & DocumentoCompra.Entidade, vbInformation + vbOKOnly)

        'CopiarCaractTecnicas.CopiarCaractTec(BaseDadosDestino, DocumentoCompra)

        GerarDocumento_BaseVendas = True
        Exit Function

TrataErro:
        MsgBox("Erro: " & Err.Description, vbCritical, "Gera Documento de Compra - TrataErro")
        GerarDocumento_BaseVendas = False
        Exit Function

TrataErroUpdate:
        MsgBox("Erro: " & Err.Description, vbCritical, "Gera Documento de Compra - TrataErroUpdate")
        MsgBox("Problemas na actualização do Num. documento de destino", vbCritical, "Gera Documento de Compra - TrataErroUpdate")
        GerarDocumento_BaseVendas = False
        Exit Function

    End Function


    Private Shared Function VerificaLote(ByVal str_Artigo As String, ByVal str_Lote As String)

        If str_Lote = "" Then Exit Function

        If PriV100Api.BSO.Inventario.ArtigosLotes.Existe(str_Artigo, str_Lote) = False Then

            Dim ArtigoLote As New InvBE100.InvBEArtigoLote

            Dim stdBE_ListaLote As StdBELista
            stdBE_ListaLote = PriV100Api.BSO.Consulta(" SELECT * FROM ArtigoLote " &
                                                     " WHERE Artigo = '" & str_Artigo & "' " &
                                                     " AND Lote = '" & str_Lote & "' ")

            If Not stdBE_ListaLote.Vazia Then

                stdBE_ListaLote.Inicio()

                ArtigoLote.Artigo = stdBE_ListaLote.Valor("Artigo")
                ArtigoLote.Lote = stdBE_ListaLote.Valor("Lote")
                ArtigoLote.Descricao = stdBE_ListaLote.Valor("Descricao")
                If Len(stdBE_ListaLote.Valor("DataFabrico")) > 0 Then ArtigoLote.DataFabrico = stdBE_ListaLote.Valor("DataFabrico")
                If Len(stdBE_ListaLote.Valor("Validade")) > 0 Then ArtigoLote.Validade = stdBE_ListaLote.Valor("Validade")
                ArtigoLote.Controlador = stdBE_ListaLote.Valor("Controlador")
                ArtigoLote.Activo = stdBE_ListaLote.Valor("Activo")
                ArtigoLote.Observacoes = stdBE_ListaLote.Valor("Observacoes")
                '2017-04-14
                ArtigoLote.CamposUtil("CDU_TipoQualidade").Valor = stdBE_ListaLote.Valor("CDU_TipoQualidade")
                ArtigoLote.CamposUtil("CDU_Parafinado").Valor = stdBE_ListaLote.Valor("CDU_Parafinado")
                PriV100Api.BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote)

            End If

        End If



    End Function



    Private Shared Function Rastreabilidade(ByVal Filial_FaturaCliente As String,
                                ByVal Serie_FaturaCliente As String,
                                ByVal TipoDoc_FaturaCliente As String,
                                ByVal NumDoc_FaturaCliente As Long,
                                ByVal BaseDadosDestino As String,
                                ByVal FaturaFornecedor_AcabadoGerar As CmpBE100.CmpBEDocumentoCompra) As Boolean



        Dim Str_Rastreabilidade As String
        Dim Lst_StrRastreabilidade As StdBELista

        Str_Rastreabilidade = "  SELECT CD2.TipoDoc as TipoDocEncomenda, CD2.NumDoc as NumDocEncomenda, CD2.Id as IdCabecDocEncomenda, LD2.Id as IdLinhasDocEncomenda, " &
                        " CD.TipoDoc as TipoDocFatura, CD.NumDoc as NumDocFatura , CD.Id as IdCabecDocFatura,  LD.Id as IdLinhasDocFatura , " &
                        " CDGrupo.TipoDoc as TipoDocEncomendaGrupo, CDGrupo.NumDoc as NumDocEncomendaGrupo ,  CDGrupo.Serie as SerieEncomendaGrupo , replace(CONVERT(VARCHAR(10),   CDGrupo.DataDoc, 102),'.','/') as DataDocEncomendaGrupo ,   CDGrupo.Id as IdCabecDocEncomendaGrupo,  LDGrupo.Id as IdLinhasDocEncomendaGrupo , LDGrupo.Quantidade  as QuantidadeEncomendaGrupo ," &
                        " CDGrupoVFA.TipoDoc as TipoDocFaturaGrupo, CDGrupoVFA.NumDoc as NumDocFaturaGrupo, CDGrupoVFA.Id as IdCabecDocFaturaGrupo, LDGrupoVFA.Id as IdLinhasDocFaturaGrupo " &
                        " FROM CabecDoc  CD " &
                        " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60" &
                        " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " &
                        " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " &
                        " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.LinhasCompras LDGrupo ON LDGrupo.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD2.id) + '}' " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.CabecCompras CDGrupo ON CDGrupo.id = LDGrupo.IdCabecCompras " &
                        " " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.LinhasCompras LDGrupoVFA ON LDGrupoVFA.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD.id) + '}' " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.CabecCompras CDGrupoVFA ON CDGrupoVFA.id = LDGrupoVFA.IdCabecCompras  " &
                        " " &
                        " WHERE  CD.Filial = '" & Filial_FaturaCliente & "' AND CD.TipoDoc = '" & TipoDoc_FaturaCliente & "' AND CD.serie = '" & Serie_FaturaCliente & "' AND CD.NumDoc = " & NumDoc_FaturaCliente & " " &
                        " ORDER BY LD.NumLinha "

        'CD = A
        'CD2 = B
        'CDGrupo = D

        Lst_StrRastreabilidade = PriV100Api.BSO.Consulta(Str_Rastreabilidade)

        If Lst_StrRastreabilidade.Vazia = False Then

            Lst_StrRastreabilidade.Inicio()

            'If FaturaFornecedor_AcabadoGerar.Linhas(1).TipoLinha = 60 Then '"ECL 2016/N.º1846 de 28/12/2016"
            'FaturaFornecedor_AcabadoGerar.Linhas(1).Descricao = Lst_StrRastreabilidade("NumDocEncomendaGrupo") & " " & Lst_StrRastreabilidade("SerieEncomendaGrupo") & "Nº" & Lst_StrRastreabilidade("NumDocEncomendaGrupo") & " de " & Lst_StrRastreabilidade("DataDocEncomendaGrupo")
            'End If

            CompletarComentarioRastreabilidade(False, FaturaFornecedor_AcabadoGerar, Lst_StrRastreabilidade.Valor("TipoDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("SerieEncomendaGrupo"), Lst_StrRastreabilidade.Valor("NumDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("DataDocEncomendaGrupo"))

            Do While Not Lst_StrRastreabilidade.NoFim

                Application.DoEvents()
                'IdLinhasDoc -> Fatura de Fornecedor do Grupo
                'IdLinhasDocOrigem -> Encomenda de Fornecedor

                PriV100Api.BSO.DSO.ExecuteSQL(" INSERT INTO LinhasComprasTrans " &
                             " (IdLinhasCompras,IdLinhasComprasOrigem,QuantTrans) " &
                              " VALUES( '" & Lst_StrRastreabilidade.Valor("IdLinhasDocFaturaGrupo") & "','" & Lst_StrRastreabilidade.Valor("IdLinhasDocEncomendaGrupo") & "'," & Replace(Lst_StrRastreabilidade.Valor("QuantidadeEncomendaGrupo"), ",", ".") & " ) ")

                Lst_StrRastreabilidade.Seguinte()
            Loop

        Else
            'Se não tiver rastreabilidade em cima, vê na de baixo
            Rastreabilidade_EncomendaParaProducao(Filial_FaturaCliente, Serie_FaturaCliente, TipoDoc_FaturaCliente, NumDoc_FaturaCliente, BaseDadosDestino, FaturaFornecedor_AcabadoGerar)
        End If


    End Function

    Private Shared Sub CompletarComentarioRastreabilidade(ByVal Apagar As Boolean,
                                                ByVal FaturaFornecedor_AcabadoGerar As CmpBE100.CmpBEDocumentoCompra,
                                                ByVal Tipodoc As String,
                                                ByVal Serie As String,
                                                ByVal NumDoc As Long,
                                                ByVal Data As Date)


        FaturaFornecedor_AcabadoGerar = PriV100Api.BSO.Compras.Documentos.Edita(FaturaFornecedor_AcabadoGerar.Filial, FaturaFornecedor_AcabadoGerar.Tipodoc, FaturaFornecedor_AcabadoGerar.Serie, FaturaFornecedor_AcabadoGerar.NumDoc)

        If Apagar = False Then
            If FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).TipoLinha = "60" Then
                If FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = "@1@ @2@/N.º@3@ de @4@" Then
                    FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = Replace(FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@1@", Tipodoc)
                    FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = Replace(FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@2@", Serie)
                    FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = Replace(FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@3@", NumDoc)
                    FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = Replace(FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@4@", Data)
                End If
            End If
        Else
            If FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).TipoLinha = "60" Then
                If FaturaFornecedor_AcabadoGerar.Linhas.GetEdita(1).Descricao = "@1@ @2@/N.º@3@ de @4@" Then
                    FaturaFornecedor_AcabadoGerar.Linhas.Remove(1)
                End If
            End If
        End If

        PriV100Api.BSO.Compras.Documentos.Actualiza(FaturaFornecedor_AcabadoGerar)

    End Sub


    Private Shared Function Rastreabilidade_EncomendaParaProducao(ByVal Filial_FaturaCliente As String,
                                                        ByVal Serie_FaturaCliente As String,
                                                        ByVal TipoDoc_FaturaCliente As String,
                                                        ByVal NumDoc_FaturaCliente As Long,
                                                        ByVal BaseDadosDestino As String,
                                                        ByVal FaturaFornecedor_AcabadoGerar As CmpBE100.CmpBEDocumentoCompra) As Boolean



        Dim Str_Rastreabilidade As String
        Dim Lst_StrRastreabilidade As StdBELista

        Str_Rastreabilidade = "  SELECT CD2.TipoDoc as TipoDocEncomenda, CD2.NumDoc as NumDocEncomenda, CD2.Id as IdCabecDocEncomenda, LD2.Id as IdLinhasDocEncomenda, " &
                        " CD.TipoDoc as TipoDocFatura, CD.NumDoc as NumDocFatura , CD.Id as IdCabecDocFatura,  LD.Id as IdLinhasDocFatura , " &
                        " CDGrupo.TipoDoc as TipoDocEncomendaGrupo, CDGrupo.NumDoc as NumDocEncomendaGrupo , CDGrupo.Serie as SerieEncomendaGrupo , replace(CONVERT(VARCHAR(10),   CDGrupo.DataDoc, 102),'.','/') as DataDocEncomendaGrupo , CDGrupo.Id as IdCabecDocEncomendaGrupo,  LDGrupo.Id as IdLinhasDocEncomendaGrupo , " &
                        " LDGrupo.Quantidade  as QuantidadeEncomendaGrupo, " &
                        " CDGrupoVFA.TipoDoc as TipoDocFaturaGrupo, CDGrupoVFA.NumDoc as NumDocFaturaGrupo, CDGrupoVFA.Id as IdCabecDocFaturaGrupo, LDGrupoVFA.Id as IdLinhasDocFaturaGrupo" &
                        " FROM CabecDoc  CD " &
                        " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60" &
                        " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " &
                        " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " &
                        " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.LinhasCompras LDGrupo ON '{' + convert(nvarchar(50),  LDGrupo.id) + '}'  =  LD2.CDU_IDLinhaOriginalGrupo " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.CabecCompras CDGrupo ON CDGrupo.id = LDGrupo.IdCabecCompras " &
                        " " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.LinhasCompras LDGrupoVFA ON LDGrupoVFA.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD.id) + '}' " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.CabecCompras CDGrupoVFA ON CDGrupoVFA.id = LDGrupoVFA.IdCabecCompras  " &
                        " " &
                        " WHERE  CD.Filial = '" & Filial_FaturaCliente & "' AND CD.TipoDoc = '" & TipoDoc_FaturaCliente & "' AND CD.serie = '" & Serie_FaturaCliente & "' AND CD.NumDoc = " & NumDoc_FaturaCliente & " " &
                        " ORDER BY LD.NumLinha "

        'CD = A
        'CD2 = B
        'CDGrupo = D

        Lst_StrRastreabilidade = PriV100Api.BSO.Consulta(Str_Rastreabilidade)

        If Lst_StrRastreabilidade.Vazia = False Then

            Lst_StrRastreabilidade.Inicio()

            CompletarComentarioRastreabilidade(False, FaturaFornecedor_AcabadoGerar, Lst_StrRastreabilidade.Valor("TipoDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("SerieEncomendaGrupo"), Lst_StrRastreabilidade.Valor("NumDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("DataDocEncomendaGrupo"))

            Do While Not Lst_StrRastreabilidade.NoFim

                Application.DoEvents()
                'IdLinhasDoc -> Fatura de Fornecedor do Grupo
                'IdLinhasDocOrigem -> Encomenda de Fornecedor

                PriV100Api.BSO.DSO.ExecuteSQL(" INSERT INTO LinhasComprasTrans " &
                             " (IdLinhasCompras,IdLinhasComprasOrigem,QuantTrans) " &
                              " VALUES( '" & Lst_StrRastreabilidade.Valor("IdLinhasDocFaturaGrupo") & "','" & Lst_StrRastreabilidade.Valor("IdLinhasDocEncomendaGrupo") & "'," & Replace(Lst_StrRastreabilidade.Valor("QuantidadeEncomendaGrupo"), ",", ".") & " ) ")

                Lst_StrRastreabilidade.Seguinte()
            Loop

        Else

            CompletarComentarioRastreabilidade(True, FaturaFornecedor_AcabadoGerar, "", "", 0, Now)
        End If


    End Function

End Class
