Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace InditexFilopa
    Public Class BasIsFichaCliente
        Inherits FichaClientes

        'Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
        '    MyBase.TeclaPressionada(KeyCode, Shift, e)

        '    'Bruno Peixoto 27/04/2020 - CTRL+O para abrir o formumlario de Fiações Inditex
        '    If KeyCode = 79 And Me.Cliente.Inactivo = False Then
        '        Module1.certFiacoes = Me.Cliente.Cliente

        '        Load FrmInditexCliente
        'FrmInditexCliente.Show
        '    End If

        '    If KeyCode = 79 And Me.Cliente.Inactivo = True Then
        '        MsgBox("Cliente Anulado! Não é possível abrir o formulário de Fiações Inditex!", vbInformation)
        '    End If

        'End Sub

    End Class
End Namespace