Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.CustomForm
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace EmDisputa
    Public Class FrmEmDisputaView
        Inherits CustomForm

        Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
        Private components As ComponentModel.IContainer
        Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
        Friend WithEvents BarButtonItemAplicar As DevExpress.XtraBars.BarButtonItem
        Friend WithEvents BarButtonItemFechar As DevExpress.XtraBars.BarButtonItem
        Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
        Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
        Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
        Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
        Friend WithEvents CheckEditFaturaDisputa As DevExpress.XtraEditors.CheckEdit

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEmDisputaView))
            Me.CheckEditFaturaDisputa = New DevExpress.XtraEditors.CheckEdit()
            Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
            Me.Bar1 = New DevExpress.XtraBars.Bar()
            Me.BarButtonItemAplicar = New DevExpress.XtraBars.BarButtonItem()
            Me.BarButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
            Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
            Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
            Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
            Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
            CType(Me.CheckEditFaturaDisputa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CheckEditFaturaDisputa
            '
            Me.CheckEditFaturaDisputa.Location = New System.Drawing.Point(80, 40)
            Me.CheckEditFaturaDisputa.Name = "CheckEditFaturaDisputa"
            Me.CheckEditFaturaDisputa.Properties.Caption = "Fatura em Disputa"
            Me.CheckEditFaturaDisputa.Size = New System.Drawing.Size(114, 19)
            Me.CheckEditFaturaDisputa.TabIndex = 0
            '
            'BarManager1
            '
            Me.BarManager1.AllowMoveBarOnToolbar = False
            Me.BarManager1.AllowQuickCustomization = False
            Me.BarManager1.AllowShowToolbarsPopup = False
            Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
            Me.BarManager1.DockControls.Add(Me.barDockControlTop)
            Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
            Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
            Me.BarManager1.DockControls.Add(Me.barDockControlRight)
            Me.BarManager1.Form = Me
            Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItemAplicar, Me.BarButtonItemFechar})
            Me.BarManager1.MaxItemId = 2
            '
            'Bar1
            '
            Me.Bar1.BarName = "ações"
            Me.Bar1.DockCol = 0
            Me.Bar1.DockRow = 0
            Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
            Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemAplicar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
            Me.Bar1.OptionsBar.DrawDragBorder = False
            Me.Bar1.OptionsBar.UseWholeRow = True
            Me.Bar1.Text = "ações"
            '
            'BarButtonItemAplicar
            '
            Me.BarButtonItemAplicar.Caption = "Aplicar"
            Me.BarButtonItemAplicar.Id = 0
            Me.BarButtonItemAplicar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemAplicar.ImageOptions.Image"), System.Drawing.Image)
            Me.BarButtonItemAplicar.Name = "BarButtonItemAplicar"
            '
            'BarButtonItemFechar
            '
            Me.BarButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
            Me.BarButtonItemFechar.Caption = "Fechar"
            Me.BarButtonItemFechar.Id = 1
            Me.BarButtonItemFechar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemFechar.ImageOptions.Image"), System.Drawing.Image)
            Me.BarButtonItemFechar.Name = "BarButtonItemFechar"
            '
            'barDockControlTop
            '
            Me.barDockControlTop.CausesValidation = False
            Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
            Me.barDockControlTop.Manager = Me.BarManager1
            Me.barDockControlTop.Size = New System.Drawing.Size(285, 31)
            '
            'barDockControlBottom
            '
            Me.barDockControlBottom.CausesValidation = False
            Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.barDockControlBottom.Location = New System.Drawing.Point(0, 71)
            Me.barDockControlBottom.Manager = Me.BarManager1
            Me.barDockControlBottom.Size = New System.Drawing.Size(285, 0)
            '
            'barDockControlLeft
            '
            Me.barDockControlLeft.CausesValidation = False
            Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
            Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
            Me.barDockControlLeft.Manager = Me.BarManager1
            Me.barDockControlLeft.Size = New System.Drawing.Size(0, 40)
            '
            'barDockControlRight
            '
            Me.barDockControlRight.CausesValidation = False
            Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
            Me.barDockControlRight.Location = New System.Drawing.Point(285, 31)
            Me.barDockControlRight.Manager = Me.BarManager1
            Me.barDockControlRight.Size = New System.Drawing.Size(0, 40)
            '
            'FrmEmDisputaView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.Controls.Add(Me.CheckEditFaturaDisputa)
            Me.Controls.Add(Me.barDockControlLeft)
            Me.Controls.Add(Me.barDockControlRight)
            Me.Controls.Add(Me.barDockControlBottom)
            Me.Controls.Add(Me.barDockControlTop)
            Me.Name = "FrmEmDisputaView"
            Me.Size = New System.Drawing.Size(285, 71)
            Me.Text = "Em Disputa"
            CType(Me.CheckEditFaturaDisputa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Private Sub BarButtonItemAplicar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemAplicar.ItemClick


            BSO.DSO.ExecuteSQL("update Historico set CDU_EmDisputa='" & CheckEditFaturaDisputa.EditValue & "' where TipoDoc='" & Module1.dsptipoDoc & "' and NumDocInt='" & Module1.dspNumDoc & "' and Serie='" & Module1.dspSerie & "'")

            Me.DialogResult = DialogResult.OK
            Me.Close()

        End Sub

        Private Sub FrmEmDisputaView_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

            Dim lista As StdBELista
            Dim sql As String

            sql = "select isnull(CDU_EmDisputa,0) as R from Historico where TipoDoc='" & Module1.dsptipoDoc & "' and NumDocInt='" & Module1.dspNumDoc & "' and Serie='" & Module1.dspSerie & "'"
            lista = BSO.Consulta(sql)

            lista.Inicio()


            Module1.dspDisputa = lista.Valor("R")


            CheckEditFaturaDisputa.EditValue = Module1.dspDisputa

        End Sub

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

            Dim lista As StdBELista
            Dim sql As String

            sql = "select isnull(CDU_EmDisputa,0) as R from Historico where TipoDoc='" & Module1.dsptipoDoc & "' and NumDocInt='" & Module1.dspNumDoc & "' and Serie='" & Module1.dspSerie & "'"
            lista = BSO.Consulta(sql)

            lista.Inicio()

            'If lista("R") & "" = "" Then
            'dspDisputa = False
            'Else
            Module1.dspDisputa = lista.Valor("R")
            'End If

            CheckEditFaturaDisputa.EditValue = Module1.dspDisputa

        End Sub

        Private Sub BarButtonItemFechar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemFechar.ItemClick

            Me.DialogResult = DialogResult.Cancel
            Me.Close()

        End Sub
    End Class
End Namespace