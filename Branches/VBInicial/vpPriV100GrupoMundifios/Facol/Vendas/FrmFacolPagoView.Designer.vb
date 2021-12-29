Imports Primavera.Extensibility.CustomForm
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFacolPagoView
    Inherits CustomForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFacolPagoView))
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItemAplicar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
        Me.CheckEditFaturadoFacol = New DevExpress.XtraEditors.CheckEdit()
        Me.CheckEditPagoAgente = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEditFaturadoFacol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEditPagoAgente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(266, 31)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 89)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(266, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 58)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(266, 31)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 58)
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
        'CheckEditFaturadoFacol
        '
        Me.CheckEditFaturadoFacol.Location = New System.Drawing.Point(89, 37)
        Me.CheckEditFaturadoFacol.MenuManager = Me.BarManager1
        Me.CheckEditFaturadoFacol.Name = "CheckEditFaturadoFacol"
        Me.CheckEditFaturadoFacol.Properties.Caption = "Faturado Facol"
        Me.CheckEditFaturadoFacol.Size = New System.Drawing.Size(93, 19)
        Me.CheckEditFaturadoFacol.TabIndex = 4
        '
        'CheckEditPagoAgente
        '
        Me.CheckEditPagoAgente.Location = New System.Drawing.Point(89, 62)
        Me.CheckEditPagoAgente.MenuManager = Me.BarManager1
        Me.CheckEditPagoAgente.Name = "CheckEditPagoAgente"
        Me.CheckEditPagoAgente.Properties.Caption = "Pago Agente"
        Me.CheckEditPagoAgente.Size = New System.Drawing.Size(93, 19)
        Me.CheckEditPagoAgente.TabIndex = 5
        '
        'FrmFacolPagoView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckEditPagoAgente)
        Me.Controls.Add(Me.CheckEditFaturadoFacol)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "FrmFacolPagoView"
        Me.Size = New System.Drawing.Size(266, 89)
        Me.Text = "Comissao Facol"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEditFaturadoFacol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEditPagoAgente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItemAplicar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItemFechar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents CheckEditPagoAgente As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CheckEditFaturadoFacol As DevExpress.XtraEditors.CheckEdit
End Class
