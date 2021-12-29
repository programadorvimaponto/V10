Imports Primavera.Extensibility.CustomForm
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Public Class FrmFacolPagoView
    Inherits CustomForm

    Private Sub BarButtonItemAplicar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemAplicar.ItemClick

        BSO.DSO.ExecuteSQL("update CabecDoc set CDU_ComissaoFacolPago='" & CheckEditFaturadoFacol.EditValue & "' where TipoDoc='" & Module1.dsptipoDoc & "' and NumDoc='" & Module1.dspNumDoc & "' and Serie='" & Module1.dspSerie & "'")
        BSO.DSO.ExecuteSQL("update CabecDoc set CDU_ComissaoAgentePaga='" & CheckEditPagoAgente.EditValue & "' where TipoDoc='" & Module1.dsptipoDoc & "' and NumDoc='" & Module1.dspNumDoc & "' and Serie='" & Module1.dspSerie & "'")


        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub FrmFacolPagoView_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

        DaValores()

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DaValores()


    End Sub


    Private Sub DaValores()

        Dim lista As StdBELista
        Dim sql As String

        sql = "select isnull(CDU_ComissaoFacolPago,0) as R, isnull(CDU_ComissaoAgentePaga,0) as A from CabecDoc where TipoDoc='" & Module1.dsptipoDoc & "' and NumDoc='" & Module1.dspNumDoc & "' and Serie='" & Module1.dspSerie & "'"
        lista = BSO.Consulta(sql)

        lista.Inicio()


        Module1.dspDisputa = lista.Valor("R")


        CheckEditFaturadoFacol.EditValue = Module1.dspDisputa
        CheckEditPagoAgente.EditValue = lista.Valor("A")

    End Sub

    Private Sub BarButtonItemFechar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemFechar.ItemClick

        Me.DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub
End Class