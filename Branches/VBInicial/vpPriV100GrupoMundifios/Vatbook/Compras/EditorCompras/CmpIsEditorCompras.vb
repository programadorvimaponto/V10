Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace Vatbook

    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Vatbook") = 1 Then

                '#################################################################################################
                '## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
                '#################################################################################################
                If Me.DocumentoCompra.CamposUtil("CDU_Fattura_Numero").Valor & "" = "" Or Me.DocumentoCompra.CamposUtil("CDU_Fattura_Numero").Valor = "0" Then
                    Dim str As String
                    Dim lista As StdBELista

                    str = BSO.Compras.TabCompras.DaValorAtributo(Me.DocumentoCompra.Tipodoc, "CDU_Fattura_SezionaleIVA")
                    If str & "" <> "" Then

                        lista = BSO.Consulta("select dbo.fnFattura_Num('" & str & "','c')")
                        lista.Inicio()

                        Me.DocumentoCompra.CamposUtil("CDU_Fattura_Numero") = lista.Valor(0)

                    End If


                End If

                '#################################################################################################
                '## Preenchimento automatico do CDU_Fattura_Numero para efeitos de Vatbook (JFC - 11/07/2019)
                '#################################################################################################

            End If

        End Sub

    End Class
End Namespace