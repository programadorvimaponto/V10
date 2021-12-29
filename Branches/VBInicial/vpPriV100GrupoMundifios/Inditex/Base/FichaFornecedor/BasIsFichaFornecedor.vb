Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace Inditex
    Public Class BasIsFichaFornecedor
        Inherits FichaFornecedores

        'Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
        '    MyBase.TeclaPressionada(KeyCode, Shift, e)

        '    If Module1.VerificaToken("Inditex") = 1 Then

        '        'Bruno Peixoto 02/09/2020 - CTRL+O para abrir o formumlario de Fiações Inditex
        '        If KeyCode = 79 And Me.Fornecedor.Inactivo = False And (UCase(Aplicacao.Utilizador.Utilizador) = "ANA" Or UCase(Aplicacao.Utilizador.Utilizador) = "RICARDO" Or UCase(Aplicacao.Utilizador.Utilizador) = "SUPORTE" Or UCase(Aplicacao.Utilizador.Utilizador) = "INFORMATICA") Then
        '            Module1.certFiacoes = Me.Fornecedor.Fornecedor

        '            Load FrmInditexFornecedor
        '    FrmInditexFornecedor.Show
        '        End If

        '        If KeyCode = 79 And Me.Fornecedor.Inactivo = True Then
        '            MsgBox("Fornecedor Anulado! Não é possível abrir o formulário de Fiações Inditex!", vbInformation)
        '        End If

        '    End If

        'End Sub

    End Class
End Namespace
