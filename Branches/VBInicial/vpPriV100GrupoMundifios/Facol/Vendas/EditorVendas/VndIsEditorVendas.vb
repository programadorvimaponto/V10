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
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService
Imports Primavera.Extensibility.Constants.ExtensibilityService

Namespace Facol
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Facol") = 1 Then

                If Me.DocumentoVenda.Tipodoc = "NEF" Then

                    If Me.DocumentoVenda.Estado = "G" Then
                        Me.DocumentoVenda.Estado = "P"
                    End If
                End If


            End If

        End Sub

        Dim VarNetTrans As Boolean
        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("Facol") = 1 Then

                'Se o documento for NET fecha o documento. Isto porque é um documento do tipo Encomenda e aparece no Mapa de Bordo. JFC
                If Me.DocumentoVenda.Tipodoc = "NET" And VarNetTrans = True Then

                    Dim StringFechaDoc As String

                    BSO.DSO.ExecuteSQL("UPDATE cds set cds.fechado='1' from CabecDocStatus cds inner join CabecDoc cd on cd.Id=cds.IdCabecDoc where cd.TipoDoc='NET' and cd.NumDoc='" & Me.DocumentoVenda.NumDoc & "' and cd.Serie='" & Me.DocumentoVenda.Serie & "'")
                    VarNetTrans = False
                End If


            End If

        End Sub


        Public Overrides Sub DepoisDeTransformar(e As ExtensibilityEventArgs)
            MyBase.DepoisDeTransformar(e)

            If Module1.VerificaToken("Facol") = 1 Then

                Dim i As Long
                If Me.DocumentoVenda.Tipodoc = "NET" Then
                    VarNetTrans = True
                End If

            End If

        End Sub

        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
            MyBase.TeclaPressionada(KeyCode, Shift, e)

            If Module1.VerificaToken("Facol") = 1 Then

                '#############################################################################
                '# NET pagas pela Facol, Formulário FrmFacolPago (JFC 19/10/2018)            #
                '#############################################################################
                'Crtl+D- Comissao Facol Pago



                If KeyCode = 68 And Me.DocumentoVenda.Tipodoc = "NET" Then

                    Module1.dsptipoDoc = Me.DocumentoVenda.Tipodoc
                    Module1.dspSerie = Me.DocumentoVenda.Serie
                    Module1.dspNumDoc = Me.DocumentoVenda.NumDoc



                    Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmFacolPagoView))

                    If result.ResultCode = ExtensibilityResultCode.Ok Then

                        Dim frm As FrmFacolPagoView = result.Result
                        frm.ShowDialog()

                    End If


                End If

                '#############################################################################
                '# NET pagas pela Facol, Formulário FrmFacolPago (JFC 19/10/2018)            #
                '#############################################################################


            End If
        End Sub


    End Class
End Namespace
