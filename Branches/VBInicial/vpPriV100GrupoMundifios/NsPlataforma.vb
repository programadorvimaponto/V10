Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Platform.Services
Imports Vimaponto.Componentes.Sistema
Imports Vimaponto.PrimaveraV100
Public Class NsPlataforma
    Inherits Plataforma

    Public Overrides Sub DepoisDeAbrirEmpresa(e As ExtensibilityEventArgs)
        PriV100Api.AtualizaContexto(PSO, BSO, Aplicacao)

        AtualizaShemaBaseDados(PriV100Api.VSO.Contexto)
    End Sub


    Private Sub AtualizaShemaBaseDados(contexto As SisNeContexto)
        Dim grupoMundifiosDs As New DsFacade(contexto)
    End Sub
End Class
