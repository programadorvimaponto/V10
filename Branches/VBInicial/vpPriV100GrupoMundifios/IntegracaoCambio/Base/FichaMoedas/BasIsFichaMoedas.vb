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

Namespace IntegracaoCambio
    Public Class BasNsFichaMoedas
        Inherits FichaMoedas

        Dim listCambio As StdBELista
        Dim listEmpresas As StdBELista
        Dim i As Long
        Dim dataCambio As StdBELista
        Dim listMoeda As StdBELista
        Public Overrides Sub DepoisDeGravar(Moeda As String, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Moeda, e)

            If Module1.VerificaToken("IntegracaoCambio") = 1 Then

                'JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" & Aplicacao.Empresa.CodEmp & "' and PRI_FichaMoedas='1'")
                listCambio = BSO.Consulta("select top 1 * from dbo.MoedasHistorico where Moeda = '" & Moeda & "' order by Data Desc")

                listEmpresas.Inicio()
                listCambio.Inicio()

                For i = 1 To listEmpresas.NumLinhas
                    listMoeda = BSO.Consulta("select top 1 * from PRI" & listEmpresas.Valor("Empresa") & ".dbo.Moedas where Moeda='" & Moeda & "'")


                    dataCambio = BSO.Consulta("select top 1 * from PRI" & listEmpresas.Valor("Empresa") & ".dbo.MoedasHistorico where Moeda='" & Moeda & "' order by Data Desc")
                    If listMoeda.Vazia = True Then
                        MsgBox("A Moeda " & Moeda & " não existe na empresa " & listEmpresas.Valor("Empresa") & "!", vbInformation)
                    Else
                        dataCambio.Inicio()

                        If dataCambio.Vazia = True Then
                            BSO.DSO.ExecuteSQL("insert into PRI" & listEmpresas.Valor("Empresa") & ".dbo.MoedasHistorico select top 1 * from dbo.MoedasHistorico where Moeda='" & Moeda & "' order by Data Desc")
                        ElseIf listCambio.Valor("Data") > dataCambio.Valor("Data") Then
                            BSO.DSO.ExecuteSQL("insert into PRI" & listEmpresas.Valor("Empresa") & ".dbo.MoedasHistorico select top 1 * from dbo.MoedasHistorico where Moeda='" & Moeda & "' order by Data Desc")

                        End If

                    End If
                    listEmpresas.Seguinte()
                Next i

            End If
        End Sub

    End Class
End Namespace