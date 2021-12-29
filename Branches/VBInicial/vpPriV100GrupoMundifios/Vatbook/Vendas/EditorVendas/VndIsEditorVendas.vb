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
Namespace Vatbook
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Vatbook") = 1 Then

                '#################################################################################################
                '## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
                '#################################################################################################
                If Me.DocumentoVenda.CamposUtil("CDU_Fattura_Numero").Valor & "" = "" Or Me.DocumentoVenda.CamposUtil("CDU_Fattura_Numero").Valor = "0" Then
                    Dim str As String
                    Dim lista As StdBELista

                    str = BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.Tipodoc, "CDU_Fattura_SezionaleIVA")
                    If str & "" <> "" Then

                        lista = BSO.Consulta("select dbo.fnFattura_Num('" & str & "','v')")
                        lista.Inicio()

                        Me.DocumentoVenda.CamposUtil("CDU_Fattura_Numero").Valor = lista.Valor(0)

                    End If


                End If

                '#################################################################################################
                '## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
                '#################################################################################################

            End If
        End Sub

    End Class
End Namespace