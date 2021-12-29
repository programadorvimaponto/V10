Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace IntegracaoIntrastat
    Public Class BasIsFichaIntMercadoria
        Inherits FichaIntMercadoria

        Dim Updt As Boolean
        Dim listEmpresas As StdBELista
        Dim i As Long
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("IntegracaoIntrastat") = 1 Then


                If BSO.Base.IntrastatMercadoria.Existe(Me.Mercadoria.Mercadoria) Then
                    Updt = True
                Else
                    Updt = False
                End If

            End If
        End Sub

        Public Overrides Sub DepoisDeAnular(Mercadoria As String, e As ExtensibilityEventArgs)
            MyBase.DepoisDeAnular(Mercadoria, e)

            If Module1.VerificaToken("IntegracaoIntrastat") = 1 Then

                'JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" & Aplicacao.Empresa.CodEmp & "' and PRI_FichaIntMercadoria='1'")
                listEmpresas.Inicio

                For i = 1 To listEmpresas.NumLinhas
                    'Codigo atualizado a 05/06/2020 para permitir automatizar as empresas a considerar na tabela DEV_Empresas
                    BSO.DSO.ExecuteSQL("delete from PRI" & listEmpresas.Valor("Empresa") & ".dbo.IntrastatMercadoria where Mercadoria ='" & Me.Mercadoria.Mercadoria & "'")
                    listEmpresas.Seguinte()
                Next i

            End If

        End Sub

        Public Overrides Sub DepoisDeGravar(Mercadoria As String, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Mercadoria, e)

            If Module1.VerificaToken("IntegracaoIntrastat") = 1 Then

                'JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" & Aplicacao.Empresa.CodEmp & "' and PRI_FichaIntMercadoria='1'")

                listEmpresas.Inicio()

            End If

        End Sub


    End Class
End Namespace