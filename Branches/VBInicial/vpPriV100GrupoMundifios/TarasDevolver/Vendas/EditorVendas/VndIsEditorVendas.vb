Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports IntBE100
Imports VndBE100

Namespace TarasDevolver
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        '        Public Overrides Sub DepoisDeDuplicar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
        '            MyBase.DepoisDeDuplicar(Filial, Tipo, Serie, NumDoc, e)

        '            If Module1.VerificaToken("TarasDevolver") = 1 Then

        '                '#### TARAS A DEVOLVER ####
        '                '*******************************************************************************************************************************************
        '                Me.DocumentoVenda.CamposUtil("CDU_NumDocStock").Valor = 0
        '                Me.DocumentoVenda.CamposUtil("CDU_NumDocSaidaStock").Valor = 0

        '                '*******************************************************************************************************************************************
        '                '#### TARAS A DEVOLVER ####
        '                '*******************************************************************************************************************************************
        '            End If

        '        End Sub

        '        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
        '            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)


        '            If Module1.VerificaToken("TarasDevolver") = 1 Then

        '                '*******************************************************************************************************************************************
        '                '#### TARAS A DEVOLVER ####
        '                '*******************************************************************************************************************************************
        '                If (Me.DocumentoVenda.Tipodoc = "GR" Or Me.DocumentoVenda.Tipodoc = "GT") And (Me.DocumentoVenda.CamposUtil("CDU_NumDocStock").Valor = 0 And Me.DocumentoVenda.CamposUtil("CDU_NumDocSaidaStock").Valor = 0) Then

        '                    'EduSamp
        '                    GeraDocumentosStock(Me.DocumentoVenda.CamposUtil("CDU_NumDocStock").Valor,
        '                                Me.DocumentoVenda.CamposUtil("CDU_NumDocSaidaStock").Valor,
        '                                Me.DocumentoVenda.CamposUtil("CDU_ConesCartao").Valor,
        '                                Me.DocumentoVenda.CamposUtil("CDU_ConesPlastico").Valor,
        '                                Me.DocumentoVenda.CamposUtil("CDU_TubosCartao").Valor,
        '                                Me.DocumentoVenda.CamposUtil("CDU_TubosPlastico").Valor,
        '                                Me.DocumentoVenda.CamposUtil("CDU_PaletesCartao").Valor,
        '                                Me.DocumentoVenda.CamposUtil("CDU_PaletesPlastico").Valor,
        '                                Me.DocumentoVenda.CamposUtil("CDU_SeparadoresCartao").Valor)

        '                End If
        '                '*******************************************************************************************************************************************
        '                '#### TARAS A DEVOLVER ####
        '                '*******************************************************************************************************************************************

        '            End If

        '        End Sub

        '        '*******************************************************************************************************************************************
        '        '#### TARAS A DEVOLVER ####
        '        '*******************************************************************************************************************************************
        '        'EduSamp 03/01/2017
        '        Function GeraDocumentosStock(ByVal v_NumDocStk As Long,
        '                                    ByVal v_NumDocSaidaStk As Long,
        '                                    ByVal v_ConesCartao As Integer,
        '                                    ByVal v_ConesPlastico As Integer,
        '                                    ByVal v_TubosCartao As Integer,
        '                                    ByVal v_TubosPlastico As Integer,
        '                                    ByVal v_PaletesMadeira As Integer,
        '                                    ByVal v_PaletesPlastico As Integer,
        '                                    ByVal v_SeparadoresCartao As Integer)

        '            Dim i As Long
        '            Dim k As Long


        '            Dim DocStk As GcpBEDocumentoStock
        '            Dim DocStk_Saida As GcpBEDocumentoStock 'Documento de saída de stock, para quando a checkbox está a false

        '            Dim LinhaStk As GcpBELinhaDocumentoStock
        '            Dim LinhaStk_Saida As GcpBELinhaDocumentoStock

        '            Dim v_Artigo As String
        '            Dim v_Armazem As String
        '            Dim v_Localizacao As String
        '            Dim v_Lote As String
        '            Dim v_LoteOrigem As String
        '            Dim v_Quantidade As Double
        '            Dim v_PrecoUnit As Double

        '            Dim Devolver As Boolean

        '            If v_NumDocStk = 0 Then
        '                Set DocStk = New GcpBEDocumentoStock
        '            Else
        '                Set DocStk = BSO.Comercial.Stocks.Edita("000", "S", "TTR", Year(Me.DocumentoVenda.DataDoc), v_NumDocStk)
        '                DocStk.Linhas.RemoveTodos
        '            End If

        '            'EduSamp 03/01/2017
        '            If v_NumDocSaidaStk = 0 Then
        '                Set DocStk_Saida = New GcpBEDocumentoStock
        '            Else
        '                Set DocStk_Saida = BSO.Comercial.Stocks.Edita("000", "S", "SS", Year(Me.DocumentoVenda.DataDoc), v_NumDocSaidaStk)
        '                DocStk.Linhas.RemoveTodos
        '            End If

        '            'Transferência de Armazém
        '            Set LinhaStk = New GcpBELinhaDocumentoStock

        '            DocStk.Filial = "000"
        '            DocStk.Tipodoc = "TTR"
        '            DocStk.Serie = Year(Me.DocumentoVenda.DataDoc)
        '            DocStk.DataDoc = Format(Me.DocumentoVenda.DataDoc & " " & Me.DocumentoVenda.HoraCarga, "dd-MM-yyyy hh:mm:ss")
        '            DocStk.Modulo = "S"
        '            DocStk.Moeda = "EUR"
        '            DocStk.cambio = 1
        '            DocStk.CambioMAlt = 1
        '            DocStk.CambioMBase = 1
        '            DocStk.Utilizador = BSO.Contexto.UtilizadorActual
        '            DocStk.TipoEntidade = "C"
        '            DocStk.Entidade = Me.DocumentoVenda.Entidade

        '            v_Armazem = "TR"
        '            v_Localizacao = "TR"
        '            v_LoteOrigem = "0001"
        '            v_Lote = Me.DocumentoVenda.Entidade

        '            DocStk.ArmazemOrigem = v_Armazem
        '            'Linha nº 1 de texto
        '            Set LinhaStk = New GcpBELinhaDocumentoStock
        '            LinhaStk.TipoLinha = "60"
        '            LinhaStk.Descricao = "Referente à " & BSO.Comercial.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).Descricao & " " & Me.DocumentoVenda.Tipodoc & " " & Me.DocumentoVenda.Serie & "/" & Me.DocumentoVenda.NumDoc
        '            LinhaStk.lote = "<L01>"
        '            LinhaStk.LoteOrigem = "<L01>"
        '            DocStk.Linhas.Insere LinhaStk

        '            'Linha nº 2 de texto
        '            Set LinhaStk = New GcpBELinhaDocumentoStock
        '            LinhaStk.TipoLinha = "60"
        '            LinhaStk.Descricao = ""
        '            LinhaStk.lote = "<L01>"
        '            LinhaStk.LoteOrigem = "<L01>"
        '            DocStk.Linhas.Insere LinhaStk


        '            '********************************************************************************************************************************************************************************************************
        '            'Se o total de devoluções de taras for diferente de 7
        '            DocStk_Saida.Filial = "000"
        '            DocStk_Saida.Tipodoc = "SS"
        '            DocStk_Saida.Serie = Year(Me.DocumentoVenda.DataDoc)
        '            DocStk_Saida.DataDoc = Format(Me.DocumentoVenda.DataDoc & " " & Me.DocumentoVenda.HoraCarga, "dd-MM-yyyy hh:mm:ss")
        '            DocStk_Saida.Modulo = "S"
        '            DocStk_Saida.Moeda = "EUR"
        '            DocStk_Saida.cambio = 1
        '            DocStk_Saida.CambioMAlt = 1
        '            DocStk_Saida.CambioMBase = 1
        '            DocStk_Saida.Utilizador = BSO.Contexto.UtilizadorActual
        '            DocStk_Saida.TipoEntidade = "C"
        '            DocStk_Saida.Entidade = Me.DocumentoVenda.Entidade

        '            DocStk_Saida.ArmazemOrigem = v_Armazem
        '            'Linha nº 1 de texto
        '            Set LinhaStk_Saida = New GcpBELinhaDocumentoStock
        '            LinhaStk_Saida.TipoLinha = "60"
        '            LinhaStk_Saida.Descricao = "Referente à " & BSO.Comercial.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).Descricao & " " & Me.DocumentoVenda.Tipodoc & " " & Me.DocumentoVenda.Serie & "/" & Me.DocumentoVenda.NumDoc
        '            LinhaStk_Saida.lote = "<L01>"
        '            LinhaStk_Saida.LoteOrigem = "<L01>"
        '            DocStk_Saida.Linhas.Insere LinhaStk_Saida

        '            'Linha nº 2 de texto
        '            Set LinhaStk_Saida = New GcpBELinhaDocumentoStock
        '            LinhaStk_Saida.TipoLinha = "60"
        '            LinhaStk_Saida.Descricao = ""
        '            LinhaStk_Saida.lote = "<L01>"
        '            LinhaStk_Saida.LoteOrigem = "<L01>"
        '            DocStk_Saida.Linhas.Insere LinhaStk_Saida

        '            '********************************************************************************************************************************************************************************************************
        '                    For i = 1 To 7

        '                Select Case i

        '                    Case 1
        '                        v_Artigo = "TRCO001"
        '                        v_Quantidade = Me.DocumentoVenda.CamposUtil("CDU_ConesCartao").Valor
        '                        Devolver = Me.DocumentoVenda.CamposUtil("CDU_DevolverConesCartao").Valor
        '                    Case 2
        '                        v_Artigo = "TRCO002"
        '                        v_Quantidade = Me.DocumentoVenda.CamposUtil("CDU_ConesPlastico").Valor
        '                        Devolver = Me.DocumentoVenda.CamposUtil("CDU_DevolverConesPlastico").Valor
        '                    Case 3
        '                        v_Artigo = "TRTU001"
        '                        v_Quantidade = Me.DocumentoVenda.CamposUtil("CDU_TubosCartao").Valor
        '                        Devolver = Me.DocumentoVenda.CamposUtil("CDU_DevolverTubosCartao").Valor
        '                    Case 4
        '                        v_Artigo = "TRTU002"
        '                        v_Quantidade = Me.DocumentoVenda.CamposUtil("CDU_TubosPlastico").Valor
        '                        Devolver = Me.DocumentoVenda.CamposUtil("CDU_DevolverTubosPlastico").Valor
        '                    Case 5
        '                        v_Artigo = "TRPA001"
        '                        v_Quantidade = Me.DocumentoVenda.CamposUtil("CDU_PaletesMadeira").Valor
        '                        Devolver = Me.DocumentoVenda.CamposUtil("CDU_DevolverPaletesMadeira").Valor
        '                    Case 6
        '                        v_Artigo = "TRPA002"
        '                        v_Quantidade = Me.DocumentoVenda.CamposUtil("CDU_PaletesPlastico").Valor
        '                        Devolver = Me.DocumentoVenda.CamposUtil("CDU_DevolverPaletesPlastico").Valor
        '                    Case 7
        '                        v_Artigo = "TRSE001"
        '                        v_Quantidade = Me.DocumentoVenda.CamposUtil("CDU_SeparadoresCartao").Valor
        '                        Devolver = Me.DocumentoVenda.CamposUtil("CDU_DevolverSeparadoresCartao").Valor
        '                End Select

        '                If v_Quantidade = 0 Then
        '                    GoTo ProximaTara
        '                End If

        '                v_PrecoUnit = 0


        '                'Criar lote caso não exista
        '                If BSO.Comercial.ArtigosLotes.Existe(v_Artigo, v_Lote) = False Then

        '                    Dim v_NovoLote As GcpBEArtigoLote
        '                    Set v_NovoLote = New GcpBEArtigoLote
        '                    v_NovoLote.Artigo = v_Artigo
        '                    v_NovoLote.lote = v_Lote
        '                    v_NovoLote.Descricao = Left(Me.DocumentoVenda.Nome, 30)
        '                    v_NovoLote.Activo = True
        '                    BSO.Comercial.ArtigosLotes.actualiza v_NovoLote

        '                End If

        '                If Devolver = True Then
        '                    BSO.Comercial.Stocks.AdicionaLinha DocStk, v_Artigo, "", v_Quantidade, v_Armazem, 0, 0, v_Lote, v_Localizacao
        '                    DocStk.Linhas.GetEdita(DocStk.Linhas.NumItens).LoteOrigem = v_LoteOrigem
        '                    DocStk.Linhas.GetEdita(DocStk.Linhas.NumItens).LocalizacaoOrigem = v_Localizacao
        '                    DocStk.Linhas.GetEdita(DocStk.Linhas.NumItens).DataStock = Format(Me.DocumentoVenda.DataDoc & " " & Me.DocumentoVenda.HoraCarga, "dd-MM-yyyy hh:mm:ss")
        '                Else
        '                    BSO.Comercial.Stocks.AdicionaLinha DocStk_Saida, v_Artigo, "", v_Quantidade, v_Armazem, 0, 0, v_Lote, v_Localizacao
        '                    'DocStk_Saida.Linhas(DocStk.Linhas.NumItens).LoteOrigem = v_LoteOrigem
        '                    'DocStk_Saida.Linhas(DocStk.Linhas.NumItens).LocalizacaoOrigem = v_Localizacao
        '                    DocStk_Saida.Linhas.GetEdita(DocStk.Linhas.NumItens).DataStock = Format(Me.DocumentoVenda.DataDoc & " " & Me.DocumentoVenda.HoraCarga, "dd-MM-yyyy hh:mm:ss")
        '                End If


        'ProximaTara:

        '            Next i

        '            If v_NumDocStk = 0 And DocStk.Linhas.NumItens >= 3 Then
        '                BSO.Comercial.Stocks.actualiza DocStk
        '                BSO.Comercial.Vendas.ActualizaValorAtributo Me.DocumentoVenda.Filial, Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc, "CDU_NumDocStock", DocStk.NumDoc
        '            ElseIf v_NumDocStk <> 0 Then
        '                BSO.Comercial.Stocks.actualiza DocStk
        '            End If

        '            If v_NumDocSaidaStk = 0 And DocStk_Saida.Linhas.NumItens >= 3 Then
        '                BSO.Comercial.Stocks.actualiza DocStk_Saida
        '                BSO.Comercial.Vendas.ActualizaValorAtributo Me.DocumentoVenda.Filial, Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc, "CDU_NumDocSaidaStock", DocStk_Saida.NumDoc
        '            ElseIf v_NumDocSaidaStk <> 0 Then
        '                BSO.Comercial.Stocks.actualiza DocStk_Saida
        '            End If

        '        End Function


        '        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
        '            MyBase.TeclaPressionada(KeyCode, Shift, e)


        '            If Module1.VerificaToken("TarasDevolver") = 1 Then

        '                '*******************************************************************************************************************************************
        '                '#### TARAS A DEVOLVER ####
        '                '*******************************************************************************************************************************************
        '                'Alt+T
        '                If KeyCode = 84 And (Me.DocumentoVenda.Tipodoc = "GR" Or Me.DocumentoVenda.Tipodoc = "GT") Then


        '                    Module1.ConesCartao = Me.DocumentoVenda.CamposUtil("CDU_ConesCartao").Valor
        '                    Module1.ConesPlastico = Me.DocumentoVenda.CamposUtil("CDU_ConesPlastico").Valor
        '                    Module1.TubosCartao = Me.DocumentoVenda.CamposUtil("CDU_TubosCartao").Valor
        '                    Module1.TubosPlastico = Me.DocumentoVenda.CamposUtil("CDU_TubosPlastico").Valor
        '                    Module1.PaletesMadeira = Me.DocumentoVenda.CamposUtil("CDU_PaletesMadeira").Valor
        '                    Module1.PaletesPlastico = Me.DocumentoVenda.CamposUtil("CDU_PaletesPlastico").Valor
        '                    Module1.SeparadoresCartao = Me.DocumentoVenda.CamposUtil("CDU_SeparadoresCartao").Valor

        '                    'EduSamp 03/01/2017
        '                    Module1.Devolver_ConesCartao = Me.DocumentoVenda.CamposUtil("CDU_DevolverConesCartao").Valor
        '                    Module1.Devolver_ConesPlastico = Me.DocumentoVenda.CamposUtil("CDU_DevolverConesPlastico").Valor
        '                    Module1.Devolver_TubosCartao = Me.DocumentoVenda.CamposUtil("CDU_DevolverTubosCartao").Valor
        '                    Module1.Devolver_TubosPlastico = Me.DocumentoVenda.CamposUtil("CDU_DevolverTubosPlastico").Valor
        '                    Module1.Devolver_PaletesMadeira = Me.DocumentoVenda.CamposUtil("CDU_DevolverPaletesMadeira").Valor
        '                    Module1.Devolver_PaletesPlastico = Me.DocumentoVenda.CamposUtil("CDU_DevolverPaletesPlastico").Valor
        '                    Module1.Devolver_SeparadoresCartao = Me.DocumentoVenda.CamposUtil("CDU_DevolverSeparadoresCartao").Valor


        '                    Load FrmTarasDevolver
        '                FrmTarasDevolver.Show

        '                    Me.DocumentoVenda.CamposUtil("CDU_ConesCartao").Valor = Module1.ConesCartao
        '                    Me.DocumentoVenda.CamposUtil("CDU_ConesPlastico").Valor = Module1.ConesPlastico
        '                    Me.DocumentoVenda.CamposUtil("CDU_TubosCartao").Valor = Module1.TubosCartao
        '                    Me.DocumentoVenda.CamposUtil("CDU_TubosPlastico").Valor = Module1.TubosPlastico
        '                    Me.DocumentoVenda.CamposUtil("CDU_PaletesMadeira").Valor = Module1.PaletesMadeira
        '                    Me.DocumentoVenda.CamposUtil("CDU_PaletesPlastico").Valor = Module1.PaletesPlastico
        '                    Me.DocumentoVenda.CamposUtil("CDU_SeparadoresCartao").Valor = Module1.SeparadoresCartao

        '                    'EduSamp 03/01/2017
        '                    Me.DocumentoVenda.CamposUtil("CDU_DevolverConesCartao").Valor = Module1.Devolver_ConesCartao
        '                    Me.DocumentoVenda.CamposUtil("CDU_DevolverConesPlastico").Valor = Module1.Devolver_ConesPlastico
        '                    Me.DocumentoVenda.CamposUtil("CDU_DevolverTubosCartao").Valor = Module1.Devolver_TubosCartao
        '                    Me.DocumentoVenda.CamposUtil("CDU_DevolverTubosPlastico").Valor = Module1.Devolver_TubosPlastico
        '                    Me.DocumentoVenda.CamposUtil("CDU_DevolverPaletesMadeira").Valor = Module1.Devolver_PaletesMadeira
        '                    Me.DocumentoVenda.CamposUtil("CDU_DevolverPaletesPlastico").Valor = Module1.Devolver_PaletesPlastico
        '                    Me.DocumentoVenda.CamposUtil("CDU_DevolverSeparadoresCartao").Valor = Module1.Devolver_SeparadoresCartao

        '                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens

        '                        If i > Me.DocumentoVenda.Linhas.NumItens Then
        '                            Exit For
        '                        End If

        '                        If Me.DocumentoVenda.Linhas.GetEdita(i).Descricao = "Taras a Devolver:" _
        '                    Or Right(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao, 14) = "Cone(s) Cartão" _
        '                    Or Right(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao, 16) = "Cone(s) Plástico" _
        '                    Or Right(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao, 14) = "Tubo(s) Cartão" _
        '                    Or Right(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao, 16) = "Tubo(s) Plástico" _
        '                    Or Right(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao, 17) = "Palete(s) Madeira" _
        '                    Or Right(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao, 18) = "Palete(s) Plástico" _
        '                    Or Right(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao, 20) = "Separador(es) Cartão" Then

        '                            Me.DocumentoVenda.Linhas.Remove(i)
        '                            i = i - 1

        '                        End If

        '                    Next i

        '                    If Module1.TotalTaras > 0 And Module1.TotalTaras_a_Devolver > 0 Then

        '                        For i = 0 To 7

        '                            Dim LinhaDoc As New VndBELinhaDocumentoVenda

        '                            LinhaDoc = New VndBELinhaDocumentoVenda

        '                            LinhaDoc.TipoLinha = "60"
        '                            LinhaDoc.Lote = "<L01>"

        '                            Select Case i

        '                                Case 0
        '                                    LinhaDoc.Descricao = "Taras a Devolver:"
        '                                Case 1
        '                                    If Me.DocumentoVenda.CamposUtil("CDU_ConesCartao").Valor = 0 Or Me.DocumentoVenda.CamposUtil("CDU_DevolverConesCartao").Valor = False Then
        '                                        GoTo ProximaLinha
        '                                    Else
        '                                        LinhaDoc.Descricao = Me.DocumentoVenda.CamposUtil("CDU_ConesCartao").Valor & " Cone(s) Cartão"
        '                                    End If
        '                                Case 2
        '                                    If Me.DocumentoVenda.CamposUtil("CDU_ConesPlastico").Valor = 0 Or Me.DocumentoVenda.CamposUtil("CDU_DevolverConesPlastico").Valor = False Then
        '                                        GoTo ProximaLinha
        '                                    Else
        '                                        LinhaDoc.Descricao = Me.DocumentoVenda.CamposUtil("CDU_ConesPlastico").Valor & " Cone(s) Plástico"
        '                                    End If
        '                                Case 3
        '                                    If Me.DocumentoVenda.CamposUtil("CDU_TubosCartao").Valor = 0 Or Me.DocumentoVenda.CamposUtil("CDU_DevolverTubosCartao").Valor = False Then
        '                                        GoTo ProximaLinha
        '                                    Else
        '                                        LinhaDoc.Descricao = Me.DocumentoVenda.CamposUtil("CDU_TubosCartao").Valor & " Tubo(s) Cartão"
        '                                    End If
        '                                Case 4
        '                                    If Me.DocumentoVenda.CamposUtil("CDU_TubosPlastico").Valor = 0 Or Me.DocumentoVenda.CamposUtil("CDU_DevolverTubosPlastico").Valor = False Then
        '                                        GoTo ProximaLinha
        '                                    Else
        '                                        LinhaDoc.Descricao = Me.DocumentoVenda.CamposUtil("CDU_TubosPlastico").Valor & " Tubo(s) Plástico"
        '                                    End If
        '                                Case 5
        '                                    If Me.DocumentoVenda.CamposUtil("CDU_PaletesMadeira").Valor = 0 Or Me.DocumentoVenda.CamposUtil("CDU_DevolverPaletesMadeira").Valor = False Then
        '                                        GoTo ProximaLinha
        '                                    Else
        '                                        LinhaDoc.Descricao = Me.DocumentoVenda.CamposUtil("CDU_PaletesMadeira").Valor & " Palete(s) Madeira"
        '                                    End If
        '                                Case 6
        '                                    If Me.DocumentoVenda.CamposUtil("CDU_PaletesPlastico").Valor = 0 Or Me.DocumentoVenda.CamposUtil("CDU_DevolverPaletesPlastico").Valor = False Then
        '                                        GoTo ProximaLinha
        '                                    Else
        '                                        LinhaDoc.Descricao = Me.DocumentoVenda.CamposUtil("CDU_PaletesPlastico").Valor & " Palete(s) Plástico"
        '                                    End If
        '                                Case 7
        '                                    If Me.DocumentoVenda.CamposUtil("CDU_SeparadoresCartao").Valor = 0 Or Me.DocumentoVenda.CamposUtil("CDU_DevolverSeparadoresCartao").Valor = False Then
        '                                        GoTo ProximaLinha
        '                                    Else
        '                                        LinhaDoc.Descricao = Me.DocumentoVenda.CamposUtil("CDU_SeparadoresCartao").Valor & " Separador(es) Cartão"
        '                                    End If

        '                            End Select

        '                            Me.DocumentoVenda.Linhas.Insere(LinhaDoc)

        'ProximaLinha:

        '                        Next i

        '                    End If

        '                End If
        '                '*******************************************************************************************************************************************
        '                '#### TARAS A DEVOLVER ####
        '                '*******************************************************************************************************************************************

        '            End If

        '        End Sub



    End Class
End Namespace
