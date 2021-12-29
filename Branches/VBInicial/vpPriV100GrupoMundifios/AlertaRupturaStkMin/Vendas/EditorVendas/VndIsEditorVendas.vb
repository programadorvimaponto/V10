Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace AlertaRupturaStkMin
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("AlertaRupturaStkMin") = 1 Then


                If Me.DocumentoVenda.Tipodoc = "FA" Or Me.DocumentoVenda.Tipodoc = "FI" Or Me.DocumentoVenda.Tipodoc = "FO" Or Me.DocumentoVenda.Tipodoc = "FIT" Then

                    Dim ExecutaSQL As Boolean
                    ExecutaSQL = False
                    Dim CountLinhas As Long

                    For CountLinhas = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(CountLinhas).Armazem = "A11" Or Me.DocumentoVenda.Linhas.GetEdita(CountLinhas).Armazem = "A17" Then
                            ExecutaSQL = True
                        End If
                    Next CountLinhas

                    If ExecutaSQL = True Then



                        BSO.DSO.ExecuteSQL("EXECUTE [PRIMUNDIFIOS].[DBO].[spAlertaArm11e17]")



                    End If


                End If

            End If
        End Sub

    End Class
End Namespace
