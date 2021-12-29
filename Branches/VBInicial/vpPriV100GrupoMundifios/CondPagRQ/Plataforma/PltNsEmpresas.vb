Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Platform.Services
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace CondPagRQ
    Public Class PltNsEmpresas
        Inherits Plataforma

        Public Overrides Sub DepoisDeAbrirEmpresa(e As ExtensibilityEventArgs)
            MyBase.DepoisDeAbrirEmpresa(e)

            If Module1.VerificaToken("CondPagRQ") = 1 Then


                Dim listadocs As StdBELista


                If (BSO.DSO.ExecuteSQL("SELECT * FROM [PRIEMPRE].[DBO].[MENSAGENSEMAIL] M WHERE M.ENVIADA = 0 AND M.DATA <= CONVERT(DATETIME,'" & Format(Now, "yyyy-MM-dd") & "' ,102)") = True) Then

                    MsgBox("Existem mensagens de plafond ultrapassado não enviadas!" & Chr(13) & "Por favor verificar o serviço Primavera Windows Scheduler que não está a funcionar.", vbInformation + vbOKOnly)

                End If



                '*************************************************************************************************************************************************


                'Verifica se existem documentos de venda com condição de pagamento RQ ou RM onde a data de vencimento não foi recalculada
                '*************************************************************************************************************************************************
                Dim SqlStringDocs = "SELECT dbo.CabecDoc.Id, dbo.CabecDoc.Filial, dbo.CabecDoc.TipoDoc, dbo.CabecDoc.Serie, dbo.CabecDoc.NumDoc, dbo.CabecDoc.CondPag, dbo.CondPag.CDU_RQ, dbo.CondPag.CDU_RM, dbo.CabecDoc.CDU_AlteradaDataVenc " _
                            & "FROM dbo.CabecDoc INNER JOIN dbo.CondPag ON dbo.CabecDoc.CondPag = dbo.CondPag.CondPag INNER JOIN dbo.DocumentosVenda ON dbo.CabecDoc.TipoDoc = dbo.DocumentosVenda.Documento " _
                            & "WHERE (dbo.DocumentosVenda.TipoDocumento = 4) AND (dbo.CabecDoc.CDU_AlteradaDataVenc = 0) AND (dbo.CondPag.CDU_RQ = 1) AND (dbo.CabecDoc.Data <= CONVERT(DATETIME, GETDATE()-1, 102)) OR " _
                            & "(dbo.DocumentosVenda.TipoDocumento = 4) AND (dbo.CabecDoc.CDU_AlteradaDataVenc = 0) AND (dbo.CondPag.CDU_RM = 1) AND (dbo.CabecDoc.Data <= CONVERT(DATETIME, GETDATE()-1, 102))"

                listadocs = BSO.Consulta(SqlStringDocs)

                If listadocs.Vazia = False Then
                    MsgBox("Existem documentos de venda com condição de pagamento resumo quinzenal ou resumo mensal onde a data de vencimento não foi corrigida." & Chr(13) & Chr(13) & "Deve por favor executar o quanto antes a função" & Chr(13) & "'Altera Data Vencimento RQ/RM'.", vbInformation + vbOKOnly)
                End If
                '*************************************************************************************************************************************************

            End If
            'End If
        End Sub

    End Class
End Namespace