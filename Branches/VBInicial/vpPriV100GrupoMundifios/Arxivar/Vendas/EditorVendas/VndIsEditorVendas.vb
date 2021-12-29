Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace Arxivar
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Dim VarArxivar As Boolean

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Arxivar") = 1 Then
                VarArxivar = False

                '##########################################################################################
                '##Valida se a fatura já foi convertida para ficheiro XML         -        15/05/2019 JFC##
                '##########################################################################################
                If BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.Tipodoc, "TipoDocumento") = "4" Then

                    VarArxivar = MsgBox("Export to Arxivar?", vbYesNo)

                    If VarArxivar Then

                        Dim strExportXML As String
                        Dim listaExportXML As StdBELista

                        strExportXML = "select ExportXML from Arxivar.Arxivar.dbo.Fatture_Testata where Fatture_ID='" & Me.DocumentoVenda.Tipodoc & Me.DocumentoVenda.NumDoc & Me.DocumentoVenda.Serie & "'"

                        listaExportXML = BSO.Consulta(strExportXML)


                        If listaExportXML.Vazia = False Then
                            listaExportXML.Inicio()

                            If listaExportXML.Valor("ExportXML") = "1" Then
                                MsgBox("La fattura è già stata convertita in XML, non è possibile salvare le modifiche!", vbCritical + vbOKOnly)
                                VarArxivar = False
                            End If
                        End If
                    End If

                End If
                '##########################################################################################
                '##Valida se a fatura já foi convertida para ficheiro XML         -        15/05/2019 JFC##
                '##########################################################################################
            End If


        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("Arxivar") = 1 Then
                '##########################################################################################
                '##Upload dos dados para o Azure        -        15/05/2019 JFC                          ##
                '##########################################################################################

                If VarArxivar Then

                    Dim strAzure As String


                    BSO.DSO.ExecuteSQL("exec primunditalia.dbo.spArxivar '" & Me.DocumentoVenda.ID & "','" & Me.DocumentoVenda.Tipodoc & Me.DocumentoVenda.NumDoc & Me.DocumentoVenda.Serie & "'")



                End If


                '##########################################################################################
                '##Upload dos dados para o Azure        -        15/05/2019 JFC                          ##
                '##########################################################################################
            End If
        End Sub


    End Class
End Namespace
