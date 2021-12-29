Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgsPublic
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdPlatBS100.StdBSTipos

Namespace InfoFioValidaLinha
    Public Class VndNsEditorVendas
        Inherits EditorVendas

        Dim MsgErro As Integer
        Dim ListaPTB As StdBELista
        Dim ListaPCU As StdBELista
        Dim ListaPCU_ArtigoArmazem As StdBELista
        Dim SqlStringPCU As String
        Dim ListaFichaLAB As StdBELista
        Dim SqlStringFichaLAB As String
        Dim ListaArtLoteRestricoes As StdBELista
        Dim SqlStringArtLoteRestricoes As String
        Dim ListaCliLevouArtLote As StdBELista
        Dim SqlStringCliLevouArtLote As String
        Public Overrides Sub ValidaLinha(NumLinha As Integer, e As ExtensibilityEventArgs)
            MyBase.ValidaLinha(NumLinha, e)

            If Module1.VerificaToken("InfoFioValidaLinha") = 1 Then

                If BSO.Inventario.ArtigosLotes.Existe(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote) = True Then

                    'Comentada dia 28/01/2014 - permitir Angelo alterar Tipo Qualidade no editor de vendas diferente do Tipo Qualidade na ficha do artigo/lote
                    Me.DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil("CDU_TipoQualidade").Valor = BSO.Inventario.ArtigosLotes.Edita(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote).CamposUtil("CDU_TipoQualidade").Valor

                    Module1.EntidadeVerifica = Me.DocumentoVenda.Entidade
                    Module1.ArtigoVerifica = Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo
                    Module1.LoteVerifica = Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote
                    Module1.PrecoUnitVerifica = Me.DocumentoVenda.Linhas.GetEdita(NumLinha).PrecUnit
                    Module1.ArmazemVerifica = Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Armazem

                    VerificaPrecoAbaixoCustoUltimo()

                    VerificaExisteFichaLaboratorio()

                    VerificaExisteRestricoesArtigoLote()

                    VerificaClenteLevouArtLote()

                End If

                If Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo & "" <> "" Then
                    If Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote & "" <> "<L01>" Then
                        If BSO.Inventario.ArtigosLotes.Existe(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote) = False Then
                            MsgErro = MsgBox("Atenção o Lote não existe!" & Chr(13) & "Artigo: " & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo & " Lote: " & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote, vbInformation + vbOKOnly)
                        End If
                    End If
                End If


                '######################################################################
                '##Verifica Alteração de Comissão  - Pedido D. Goreti -25/03/2019 JFC##
                '######################################################################
                If Me.DocumentoVenda.Tipodoc = "ECL" And Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo & "" <> "" Then

                    Dim sqlComissao As StdBELista

                    sqlComissao = BSO.Consulta("select top 1 ln.NumLinha, ln.Artigo, ln.Lote, ln.Comissao from LinhasDoc ln where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).IdLinha & "'")
                    If sqlComissao.Vazia = False Then
                        sqlComissao.Inicio()

                        If sqlComissao.Valor("Comissao") <> Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Comissao Then
                            MsgBox("ATENÇÃO COMISSÃO ALTERADA!" & Chr(13) & Chr(13) & "Linha: " & NumLinha & Chr(13) & "" +
                             " Artigo: " & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo & "" +
                             " Lote: " & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote & "" +
                             " Comissao: " & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Comissao & "" +
                             Chr(13) & Chr(13) & "" +
                            "Antes:" & Chr(13) & "" +
                             " Artigo: " & sqlComissao.Valor("Artigo") & "" +
                             " Lote: " & sqlComissao.Valor("Lote") & "" +
                             " Comissao: " & sqlComissao.Valor("Comissao") & "", vbInformation)
                        End If

                    End If


                End If
                '######################################################################
                '##Verifica Alteração de Comissão  - Pedido D. Goreti -25/03/2019 JFC##
                '######################################################################
            End If

        End Sub


        Private Function VerificaPrecoAbaixoCustoUltimo()

            '    SqlStringPCU = "SELECT     ISNULL(ROUND " _
            '                & "((SELECT     TOP (1) CASE moeda WHEN 'EUR' THEN LinhasCompras.PrecUnit ELSE isnull(CASE isnull ((SELECT     TOP 1 CDU_CambioFixo FROM TDU_CabecCustosEncomendas WHERE     (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND CDU_TipoDoc = 'ECF'), (SELECT     TOP 1 CDU_CambioFixo FROM TDU_CabecCustosEncomendas WHERE      (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND CDU_TipoDoc = 'CNT')) WHEN 0 THEN LinhasCompras.PrecUnit / (SELECT     TOP 1 Compra FROM MoedasHistorico WHERE (MoedasHistorico.moeda = CabecCompras.moeda) ORDER BY DataCambio DESC) ELSE LinhasCompras.PrecUnit / isnull " _
            '                & "((SELECT     TOP 1 CDU_CambioFixo FROM TDU_CabecCustosEncomendas WHERE     (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND CDU_TipoDoc = 'ECF'), (SELECT     TOP 1 CDU_CambioFixo FROM TDU_CabecCustosEncomendas WHERE      (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND CDU_TipoDoc = 'CNT')) END, LinhasCompras.PrecUnit / (SELECT     TOP 1 Compra FROM MoedasHistorico WHERE (MoedasHistorico.moeda = CabecCompras.moeda) ORDER BY DataCambio DESC)) END AS Expr1 FROM         dbo.LinhasCompras INNER JOIN dbo.CabecCompras ON dbo.LinhasCompras.IdCabecCompras = dbo.CabecCompras.Id INNER JOIN dbo.DocumentosCompra ON dbo.CabecCompras.TipoDoc = dbo.DocumentosCompra.Documento INNER JOIN dbo.CabecComprasStatus ON dbo.CabecCompras.Id = dbo.CabecComprasStatus.IdCabecCompras " _
            '                & "WHERE     (dbo.DocumentosCompra.TipoDocumento = 2) AND (dbo.LinhasCompras.Artigo = '" & ArtigoVerifica & "') AND (dbo.LinhasCompras.Lote = '" & LoteVerifica & "') AND (dbo.CabecComprasStatus.Anulado = 0) AND (dbo.CabecComprasStatus.Fechado = 0) ORDER BY dbo.CabecCompras.TipoDoc DESC, dbo.CabecCompras.NumDoc DESC) + ISNULL ((SELECT     TOP (1) CDU_TotalCusto FROM dbo.TDU_CabecCustosEncomendas WHERE     (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND (CDU_TipoDoc = 'ECF')), (SELECT     TOP (1) TDU_CabecCustosEncomendas_1.CDU_TotalCusto " _
            '                & "FROM          dbo.TDU_CabecCustosEncomendas AS TDU_CabecCustosEncomendas_1 INNER JOIN dbo.CabecCompras AS CabecCompras_1 ON TDU_CabecCustosEncomendas_1.CDU_NumDoc = CabecCompras_1.NumDoc AND TDU_CabecCustosEncomendas_1.CDU_TipoDoc = CabecCompras_1.TipoDoc AND TDU_CabecCustosEncomendas_1.CDU_Serie = CabecCompras_1.Serie INNER JOIN dbo.CabecComprasStatus AS CabecComprasStatus_1 ON CabecCompras_1.Id = CabecComprasStatus_1.IdCabecCompras " _
            '                & "WHERE      (TDU_CabecCustosEncomendas_1.CDU_Artigo = '" & ArtigoVerifica & "') AND (TDU_CabecCustosEncomendas_1.CDU_Lote = '" & LoteVerifica & "') AND (TDU_CabecCustosEncomendas_1.CDU_TipoDoc = 'CNT') AND (CabecComprasStatus_1.Fechado = 0) AND (CabecComprasStatus_1.Anulado = 0) ORDER BY CabecCompras_1.NumDoc DESC)), 3), (SELECT     TOP (1) dbo.LinhasSTK.CDU_PrCusto FROM          dbo.LinhasSTK INNER JOIN dbo.DocumentosStk ON dbo.LinhasSTK.TipoDoc = dbo.DocumentosStk.Documento WHERE      (dbo.LinhasSTK.Artigo = '" & ArtigoVerifica & "') AND (dbo.LinhasSTK.Lote = '" & LoteVerifica & "') AND (dbo.DocumentosStk.TipoDocumento = 0) OR (dbo.LinhasSTK.Artigo = '" & ArtigoVerifica & "') AND (dbo.LinhasSTK.Lote = '" & LoteVerifica & "') AND (dbo.DocumentosStk.TipoDocumento = 1) ORDER BY dbo.LinhasSTK.Data DESC)) AS PCusto"

            SqlStringPCU = "select dbo.VMP_IEXF_DaPrecoCusto ('" & Module1.ArtigoVerifica & "','" & Module1.LoteVerifica & "','3') as 'PCusto'"

            ListaPCU = BSO.Consulta(SqlStringPCU)

            If ListaPCU.Vazia = False Then

                ListaPCU.Inicio

                If Module1.PrecoUnitVerifica <= ListaPCU.Valor("PCusto") Then

                    If ListaPCU.Valor("PCusto") = "" Then
                        'Eduardo Sampaio 03/03/2017
                        'Por indicação do Rui Fernandes,faço como está no IE. Atualizo com o valor da tabela ArtigoArmazem caso a query acima nao retorne valores
                        ListaPCU_ArtigoArmazem = BSO.Consulta("SELECT PCMedio as PCusto  FROM ArtigoArmazem WHERE Artigo = '" & Module1.ArtigoVerifica & "' AND Armazem = '" & Module1.ArmazemVerifica & "' AND  Lote = '" & Module1.LoteVerifica & "'")
                        If ListaPCU_ArtigoArmazem.Vazia = False Then
                            ListaPCU_ArtigoArmazem.Inicio()

                            If Module1.PrecoUnitVerifica <= ListaPCU_ArtigoArmazem.Valor("PCusto") Then
                                MsgErro = MsgBox("O Artigo: " & Module1.ArtigoVerifica & ", Lote: " & Module1.LoteVerifica & " está a ser vendido abaixo do preço de custo: " & ListaPCU_ArtigoArmazem.Valor("PCusto") & " €", vbCritical + vbOKOnly)
                                Exit Function
                            Else
                                Exit Function
                            End If
                        End If
                    End If

                    MsgErro = MsgBox("O Artigo: " & Module1.ArtigoVerifica & ", Lote: " & Module1.LoteVerifica & " está a ser vendido abaixo do preço de custo: " & ListaPCU.Valor("PCusto") & " €", vbCritical + vbOKOnly)

                End If

            End If

        End Function

        Private Function VerificaExisteFichaLaboratorio()

            SqlStringFichaLAB = "SELECT * FROM [TDU_LABORATORIOLOTE] " _
                            & "WHERE CDU_RSSitFinFio!='SPECS' and cdu_codARTIGO = '" & Module1.ArtigoVerifica & "' and cdu_loteart = '" & Module1.LoteVerifica & "'"

            ListaFichaLAB = BSO.Consulta(SqlStringFichaLAB)

            If ListaFichaLAB.Vazia = True Then

                MsgErro = MsgBox("O Artigo: " & Module1.ArtigoVerifica & ", Lote: " & Module1.LoteVerifica & " não possui características técnicas", vbInformation + vbOKOnly)

            End If

        End Function


        Private Function VerificaExisteRestricoesArtigoLote()

            SqlStringArtLoteRestricoes = "SELECT * FROM [TDU_LABORATORIOLOTE] " _
                            & "WHERE cdu_codARTIGO = '" & Module1.ArtigoVerifica & "' and cdu_loteart = '" & Module1.LoteVerifica & "'"

            ListaArtLoteRestricoes = BSO.Consulta(SqlStringArtLoteRestricoes)

            If ListaArtLoteRestricoes.Vazia = False Then

                ListaArtLoteRestricoes.Inicio

                If ListaArtLoteRestricoes.Valor("CDU_RSSitFinFioObs") & "" <> "" Then

                    MsgErro = MsgBox("O Artigo: " & Module1.ArtigoVerifica & ", Lote: " & Module1.LoteVerifica & " tem restrições: " & Chr(13) & Chr(13) & ListaArtLoteRestricoes.Valor("CDU_RSSitFinFioObs"), vbInformation + vbOKOnly)

                End If

            End If

        End Function

        Private Function VerificaClenteLevouArtLote()

            SqlStringCliLevouArtLote = "SELECT dbo.CabecDoc.Entidade, dbo.LinhasDoc.Artigo, dbo.LinhasDoc.Lote " _
                                & "FROM dbo.CabecDoc INNER JOIN dbo.LinhasDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc " _
                                & "WHERE (dbo.LinhasDoc.Artigo = '" & Module1.ArtigoVerifica & "') AND (dbo.LinhasDoc.Lote = '" & Module1.LoteVerifica & "') AND (dbo.CabecDoc.Entidade = '" & Module1.EntidadeVerifica & "')"

            ListaCliLevouArtLote = BSO.Consulta(SqlStringCliLevouArtLote)

            If ListaCliLevouArtLote.Vazia = True Then

                MsgErro = MsgBox("É a primeira vez que o Cliente " & BSO.Base.Clientes.Edita(Module1.EntidadeVerifica).Nome & " leva o Artigo: " & Module1.ArtigoVerifica & ", do Lote: " & Module1.LoteVerifica, vbInformation + vbOKOnly)

            End If

        End Function


    End Class
End Namespace