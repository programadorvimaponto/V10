Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace CopiaEntreEmpresas
    Public Class VndIsFichaTabDocVendas
        Inherits FichaTabDocVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)
            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then

                If Not ValidacaoCamposUtilVenda Then Cancel = True

                If Cancel = False Then If Not ValidacaoCamposUtilCompra() Then Cancel = True

            End If

        End Sub


        '#edusamp
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

                    MsgBox("Série não preenchida para o Documento de Venda " & DocVendaDestino & ".", vbCritical, "Campos de utilizador Doc. Venda incompletos")
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


        '#edusamp
        Private Function ValidacaoCamposUtilCompra() As Boolean

            On Error GoTo TrataErro

            Dim DocCompraDestino As String
            Dim SerieCompraDestino As String

            Me.Documento.CamposUtil("CDU_TipoDocComprasDestino").Valor = UCase(Me.Documento.CamposUtil("CDU_TipoDocComprasDestino").Valor & "")
            Me.Documento.CamposUtil("CDU_SerieComprasDestino").Valor = UCase(Me.Documento.CamposUtil("CDU_SerieComprasDestino").Valor & "")

            DocCompraDestino = UCase(Me.Documento.CamposUtil("CDU_TipoDocComprasDestino").Valor & "")

            'Se campo da Base de dados vazia não avisa nada e sai
            If Len(DocCompraDestino) > 0 Then

                If Me.Documento.TipoDocumento <> BSO.Compras.TabCompras.DaValorAtributo(UCase(Me.Documento.CamposUtil("CDU_TipoDocComprasDestino").Valor & ""), "TipoDocumento") Then
                    MsgBox("O tipo de documento de Compra configurado não é permitido.", vbCritical, "Campos de utilizador Doc. Venda incompletos")
                    ValidacaoCamposUtilCompra = False
                    Exit Function
                End If

                SerieCompraDestino = UCase(Me.Documento.CamposUtil("CDU_SerieComprasDestino").Valor & "")

                If Len(SerieCompraDestino) > 0 Then

                    ValidacaoCamposUtilCompra = True
                    Exit Function

                Else

                    MsgBox("Série não preenchida para o Documento de Compra " & DocCompraDestino & ".", vbCritical, "Campos de utilizador Doc. Compra incompletos")
                    ValidacaoCamposUtilCompra = False
                    Exit Function
                End If

            Else

                'Se não tiver documento preenchido, apago a serie caso exista
                Me.Documento.CamposUtil("CDU_SerieComprasDestino").Valor = ""
                ValidacaoCamposUtilCompra = True
                Exit Function
            End If

TrataErro:
            MsgBox("Erro: " & Err.Description, vbCritical, "Erro nos campos de utilizador Doc. Compra ")
            ValidacaoCamposUtilCompra = False

        End Function



    End Class
End Namespace