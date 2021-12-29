Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace CopiaEntreEmpresas
    Public Class CmpIsFichaTabDocCompras
        Inherits FichaTabDocCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then

                If Not ValidacaoCamposUtilVenda() Then Cancel = True

            End If

        End Sub

        Private Function ValidacaoCamposUtilVenda() As Boolean

            On Error GoTo TrataErro

            Dim DocVendaDestino As String
            Dim SerieVendaDestino As String

            Me.Documento.CamposUtil("CDU_TipoDocVendasDestino").Valor = UCase(Me.Documento.CamposUtil("CDU_TipoDocVendasDestino").Valor & "")

            Me.Documento.CamposUtil("CDU_SerieVendasDestino").Valor = UCase(Me.Documento.CamposUtil("CDU_SerieVendasDestino").Valor & "")

            DocVendaDestino = UCase(Me.Documento.CamposUtil("CDU_TipoDocVendasDestino").Valor & "")

            'Se campo da Base de dados vazia não avisa nada e sai
            If Len(DocVendaDestino) > 0 Then

                If Me.Documento.TipoDocumento <> BSO.Vendas.TabVendas.DaValorAtributo(UCase(Me.Documento.CamposUtil("CDU_TipoDocVendasDestino").Valor & ""), "TipoDocumento") Then
                    MsgBox("O tipo de documento de Venda configurado não é permitido.", vbCritical, "Campos de utilizador Doc. Venda incompletos")
                    ValidacaoCamposUtilVenda = False
                    Exit Function
                End If


                SerieVendaDestino = UCase(Me.Documento.CamposUtil("CDU_SerieVendasDestino").Valor & "")

                If Len(SerieVendaDestino) > 0 Then

                    ValidacaoCamposUtilVenda = True
                    Exit Function

                Else

                    MsgBox("Série não preenchida para o Documento de Compra " & DocVendaDestino & ".", vbCritical, "Campos de utilizador Doc. Venda incompletos")
                    ValidacaoCamposUtilVenda = False
                    Exit Function
                End If


            Else

                'Se não tiver documento preenchido, apago a serie caso exista
                Me.Documento.CamposUtil("CDU_SerieVendasDestino").Valor = ""
                ValidacaoCamposUtilVenda = True
                Exit Function
            End If


TrataErro:
            MsgBox("Erro: " & Err.Description, vbCritical, "Erro nos campos de utilizador Doc. Venda ")
            ValidacaoCamposUtilVenda = False

        End Function

    End Class
End Namespace