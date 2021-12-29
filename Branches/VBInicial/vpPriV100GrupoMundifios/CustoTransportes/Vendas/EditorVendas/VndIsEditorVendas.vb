Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgsPublic
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdPlatBS100.StdBSTipos
Imports Primavera.Extensibility.Constants.ExtensibilityService
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService

Namespace CustoTransportes
    Public Class VndNsVendas
        Inherits EditorVendas

        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
            MyBase.TeclaPressionada(KeyCode, Shift, e)


            If Module1.VerificaToken("CustoTransportes") = 1 Then

                '#################################################################################################
                '# Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (JFC 13/05/2019) #
                '#################################################################################################

                'JFC 18/12/2019 Ctrl + W - Custo Transportes
                If KeyCode = 87 And BSO.Vendas.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).TipoDocumento = 3 Then

                    Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmCustoTransportesView))

                    If result.ResultCode = ExtensibilityResultCode.Ok Then

                        Dim frm As FrmCustoTransportesView = result.Result
                        frm.ShowDialog()

                    End If

                End If
            End If

        End Sub

    End Class
End Namespace