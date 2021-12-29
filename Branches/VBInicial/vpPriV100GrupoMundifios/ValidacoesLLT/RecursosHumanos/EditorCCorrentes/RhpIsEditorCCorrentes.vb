Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.PayablesReceivables.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports BasBE100
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace ValidacoesLLT
    Public Class RhpIsEditorCCorrentes
        Inherits EditorCCorrentes

        Public Overrides Sub AntesDeGravar(TDocumento As BasBETiposGcp.TE_DocCCorrentes, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(TDocumento, Cancel, e)


            If Module1.VerificaToken("ValidacoesLLT") = 1 Then

                If Me.DocumentoLiquidacao.Tipodoc = "LLT" And BSO.Base.Clientes.DaValorAtributo(Me.DocumentoLiquidacao.Entidade, "TipoMercado") <> 0 Then
                    MsgBox("A liquidação por letra só pode ser efectuada para clientes nacionais.", vbCritical + vbOKOnly)
                    Cancel = True
                End If

                If Me.DocumentoLiquidacao.Tipodoc = "LLR" And BSO.Base.Clientes.DaValorAtributo(Me.DocumentoLiquidacao.Entidade, "TipoMercado") = 0 Then
                    MsgBox("A liquidação por remessa só pode ser efectuada para clientes intracomunitários e outros mercados.", vbCritical + vbOKOnly)
                    Cancel = True
                End If
            End If

        End Sub

    End Class
End Namespace