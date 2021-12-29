Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Threading.Tasks
Imports Vimaponto.Componentes.Sistema

Public Class DsFacade
    Inherits SisDsServicoDados

    Public Overrides ReadOnly Property NivelBaseDados As Integer
        Get
            Return 2
        End Get
    End Property

    Public Overrides ReadOnly Property AssemblyComScripts As Assembly
        Get
            Return Assembly.GetExecutingAssembly()
        End Get
    End Property

    Public Overrides ReadOnly Property IdentificadorPacote As String
        Get
            Return "GrupoMundifios"
        End Get
    End Property

    Public Sub New(ByRef contexto As SisNeContexto)
        MyBase.New(contexto)

    End Sub
End Class