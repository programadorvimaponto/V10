'Imports System
'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Text
'Imports System.Threading.Tasks
'Imports Primavera.Extensibility.Sales.Editors
'Imports Primavera.Extensibility.BusinessEntities
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
'Imports Primavera.Extensibility.Constants.ExtensibilityService
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService

'Namespace ArmazemEntreposto
'    Public Class VndNsEditorVendas
'        Inherits EditorVendas

'        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
'            MyBase.TeclaPressionada(KeyCode, Shift, e)

'            If Module1.VerificaToken("ArmazemEntreposto") = 1 Then

'                If Me.LinhaActual > 0 Then
'                    If KeyCode = 68 And Me.DocumentoVenda.Tipodoc = "GR" And Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Armazem = "AEP" Then

'                        Module1.aepArtigo = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Artigo
'                        Module1.aepDocumento = Me.DocumentoVenda.Tipodoc & " " & Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie
'                        Module1.aepLote = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Lote
'                        Module1.aepArmazem = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Armazem
'                        Module1.aepDespDAU = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_DespDAU").Valor
'                        Module1.aepRegime = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_Regime").Valor
'                        Module1.aepIDlinha = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).IdLinha


'                        Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmAlteraGuiaEPView))

'                        If result.ResultCode = ExtensibilityResultCode.Ok Then

'                            Dim frm As FrmAlteraGuiaEPView = result.Result
'                            frm.ShowDialog()

'                        End If

'                    End If
'                End If

'            End If

'        End Sub

'    End Class
'End Namespace
