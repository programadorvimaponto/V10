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

Namespace DefaultToken
    Public Class PltNsEmpresas
        Inherits Plataforma



        '*******************************************************************************************************************************************
        '#### ARMAZEM ENTREPOSTO ####
        '*******************************************************************************************************************************************
        Dim ListaArmEnt As StdBELista
        Dim SqlStringArmEnt As String
        '*******************************************************************************************************************************************
        '#### ARMAZEM ENTREPOSTO ####
        '*******************************************************************************************************************************************

        Public Overrides Sub DepoisDeAbrirEmpresa(e As ExtensibilityEventArgs)
            MyBase.DepoisDeAbrirEmpresa(e)

            If Module1.VerificaToken("Default") = 1 Then
                '*******************************************************************************************************************************************
                '#### ARMAZEM ENTREPOSTO ####
                '*******************************************************************************************************************************************
                SqlStringArmEnt = "SELECT CDU_Parametro FROM TDU_Parametros WHERE CDU_Modulo = 'Entreposto'"

                ListaArmEnt = BSO.Consulta(SqlStringArmEnt)

                If ListaArmEnt.Vazia = False Then
                    Module1.ArmEntreposto = ListaArmEnt.Valor("CDU_Parametro")
                End If
                '*******************************************************************************************************************************************
                '#### ARMAZEM ENTREPOSTO ####
                '*******************************************************************************************************************************************

            End If

        End Sub

    End Class
End Namespace
