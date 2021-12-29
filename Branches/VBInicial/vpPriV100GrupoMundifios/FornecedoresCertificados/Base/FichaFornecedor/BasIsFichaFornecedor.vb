'Imports Primavera.Extensibility.Base.Editors
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
'Imports Primavera.Extensibility.Constants.ExtensibilityService
'Imports StdBE100
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico


'Namespace FornecedoresCertificados
'    Public Class BasIsFichaFornecedor
'        Inherits FichaFornecedores

'        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
'            MyBase.TeclaPressionada(KeyCode, Shift, e)

'            If Module1.VerificaToken("FornecedoresCertificados") = 1 Then
'                '    
'                'Crtl + Q JFC 04/11/2019
'                If KeyCode = 81 And Me.Fornecedor.Inactivo = False Then
'                    Module1.certEntidade = Me.Fornecedor.Fornecedor


'                    Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmFornecedoresCertsViews))

'                    If result.ResultCode = ExtensibilityResultCode.Ok Then

'                        Dim frm As FrmFornecedoresCertsViews = result.Result
'                        frm.ShowDialog()

'                    End If

'                End If

'                If KeyCode = 81 And Me.Fornecedor.Inactivo = True Then
'                    MsgBox("Fornecedor Anulado! Não é possível abrir o formulário de certificados!", vbInformation)
'                End If

'            End If
'        End Sub

'    End Class
'End Namespace
