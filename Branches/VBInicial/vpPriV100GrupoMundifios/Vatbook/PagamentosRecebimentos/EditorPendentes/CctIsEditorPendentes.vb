Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.PayablesReceivables.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace Vatbook
    Public Class CctIsEditorPendentes
        Inherits EditorPendentes

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Vatbook") = 1 Then

                '#################################################################################################
                '## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
                '#################################################################################################
                If Me.DocumentoPendente.CamposUtil("CDU_Fattura_Numero").Valor & "" = "" Or Me.DocumentoPendente.CamposUtil("CDU_Fattura_Numero").Valor = "0" Then
                    Dim str As String
                    Dim lista As StdBELista
                    str = BSO.PagamentosRecebimentos.TabCCorrentes.DaValorAtributo(Me.DocumentoPendente.Tipodoc, "CDU_Fattura_SezionaleIVA")
                    If str & "" <> "" Then

                        lista = BSO.Consulta("select dbo.fnFattura_Num('" & str & "','c')")
                        lista.Inicio()

                        If lista.Valor(0) & "" <> "" Then

                            Me.DocumentoPendente.CamposUtil("CDU_Fattura_Numero").Valor = lista.Valor(0)
                        Else
                            Me.DocumentoPendente.CamposUtil("CDU_Fattura_Numero").Valor = 1
                        End If

                    End If


                End If

                '#################################################################################################
                '## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
                '#################################################################################################

            End If

        End Sub


    End Class
End Namespace