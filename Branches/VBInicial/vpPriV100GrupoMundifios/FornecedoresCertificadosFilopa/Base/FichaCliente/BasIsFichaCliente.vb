Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Constants.ExtensibilityService
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace FornecedoresCertificadosFilopa

    Public Class BasIsFichaCliente
        Inherits FichaClientes

        'Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
        '    MyBase.TeclaPressionada(KeyCode, Shift, e)

        '    If Module1.VerificaToken("FornecedoresCertificadosFilopa") = 1 Then

        '        If KeyCode = 81 And Me.Cliente.Inactivo = False Then
        '            Module1.certEntidade = Me.Cliente.Cliente


        '            Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmFornecedoresCertsFilopaView))

        '            If result.ResultCode = ExtensibilityResultCode.Ok Then

        '                Dim frm As FrmFornecedoresCertsFilopaView = result.Result
        '                frm.ShowDialog()

        '            End If
        '        End If

        '        If KeyCode = 81 And Me.Cliente.Inactivo = True Then
        '            MsgBox("Cliente Anulado! Não é possível abrir o formulário de certificados!", vbInformation)
        '        End If

        '    End If

        'End Sub

    End Class
End Namespace
