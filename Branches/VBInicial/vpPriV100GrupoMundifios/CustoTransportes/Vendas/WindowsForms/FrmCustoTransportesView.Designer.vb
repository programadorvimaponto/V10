Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.CustomForm
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCustoTransportesView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCustoTransportesView))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TextEditCodigoFornecedor = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditFornecedor = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditFatura = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditFatura2 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCusto = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCusto2 = New DevExpress.XtraEditors.TextEdit()
        Me.MemoEditObsTransporte = New DevExpress.XtraEditors.MemoEdit()
        Me.TextEditTotal = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditIdFatura = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditIdFatura2 = New DevExpress.XtraEditors.TextEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItemGravar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItemRemover = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.TextEditCodigoFornecedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditFornecedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditFatura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditFatura2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCusto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCusto2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MemoEditObsTransporte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditIdFatura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditIdFatura2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TextEditIdFatura2)
        Me.GroupControl1.Controls.Add(Me.Label7)
        Me.GroupControl1.Controls.Add(Me.Label6)
        Me.GroupControl1.Controls.Add(Me.TextEditIdFatura)
        Me.GroupControl1.Controls.Add(Me.Label5)
        Me.GroupControl1.Controls.Add(Me.Label4)
        Me.GroupControl1.Controls.Add(Me.Label3)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.TextEditTotal)
        Me.GroupControl1.Controls.Add(Me.MemoEditObsTransporte)
        Me.GroupControl1.Controls.Add(Me.TextEditCusto2)
        Me.GroupControl1.Controls.Add(Me.TextEditCusto)
        Me.GroupControl1.Controls.Add(Me.TextEditFatura2)
        Me.GroupControl1.Controls.Add(Me.TextEditFatura)
        Me.GroupControl1.Controls.Add(Me.TextEditFornecedor)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoFornecedor)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 37)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(618, 212)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Dados"
        '
        'TextEditCodigoFornecedor
        '
        Me.TextEditCodigoFornecedor.Location = New System.Drawing.Point(158, 27)
        Me.TextEditCodigoFornecedor.Name = "TextEditCodigoFornecedor"
        Me.TextEditCodigoFornecedor.Size = New System.Drawing.Size(100, 20)
        Me.TextEditCodigoFornecedor.TabIndex = 0
        '
        'TextEditFornecedor
        '
        Me.TextEditFornecedor.Location = New System.Drawing.Point(264, 27)
        Me.TextEditFornecedor.Name = "TextEditFornecedor"
        Me.TextEditFornecedor.Size = New System.Drawing.Size(231, 20)
        Me.TextEditFornecedor.TabIndex = 1
        '
        'TextEditFatura
        '
        Me.TextEditFatura.Location = New System.Drawing.Point(158, 57)
        Me.TextEditFatura.Name = "TextEditFatura"
        Me.TextEditFatura.Size = New System.Drawing.Size(100, 20)
        Me.TextEditFatura.TabIndex = 1
        '
        'TextEditFatura2
        '
        Me.TextEditFatura2.Location = New System.Drawing.Point(158, 87)
        Me.TextEditFatura2.Name = "TextEditFatura2"
        Me.TextEditFatura2.Size = New System.Drawing.Size(100, 20)
        Me.TextEditFatura2.TabIndex = 2
        '
        'TextEditCusto
        '
        Me.TextEditCusto.Location = New System.Drawing.Point(395, 57)
        Me.TextEditCusto.Name = "TextEditCusto"
        Me.TextEditCusto.Size = New System.Drawing.Size(100, 20)
        Me.TextEditCusto.TabIndex = 3
        '
        'TextEditCusto2
        '
        Me.TextEditCusto2.Location = New System.Drawing.Point(395, 87)
        Me.TextEditCusto2.Name = "TextEditCusto2"
        Me.TextEditCusto2.Size = New System.Drawing.Size(100, 20)
        Me.TextEditCusto2.TabIndex = 4
        '
        'MemoEditObsTransporte
        '
        Me.MemoEditObsTransporte.Location = New System.Drawing.Point(53, 145)
        Me.MemoEditObsTransporte.Name = "MemoEditObsTransporte"
        Me.MemoEditObsTransporte.Size = New System.Drawing.Size(560, 62)
        Me.MemoEditObsTransporte.TabIndex = 5
        '
        'TextEditTotal
        '
        Me.TextEditTotal.Location = New System.Drawing.Point(395, 119)
        Me.TextEditTotal.Name = "TextEditTotal"
        Me.TextEditTotal.Size = New System.Drawing.Size(212, 20)
        Me.TextEditTotal.TabIndex = 6
        '
        'TextEditIdFatura
        '
        Me.TextEditIdFatura.Location = New System.Drawing.Point(5, 27)
        Me.TextEditIdFatura.Name = "TextEditIdFatura"
        Me.TextEditIdFatura.Size = New System.Drawing.Size(51, 20)
        Me.TextEditIdFatura.TabIndex = 1
        '
        'TextEditIdFatura2
        '
        Me.TextEditIdFatura2.Location = New System.Drawing.Point(5, 53)
        Me.TextEditIdFatura2.Name = "TextEditIdFatura2"
        Me.TextEditIdFatura2.Size = New System.Drawing.Size(51, 20)
        Me.TextEditIdFatura2.TabIndex = 2
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItemGravar, Me.BarButtonItemRemover, Me.BarButtonItemFechar})
        Me.BarManager1.MaxItemId = 3
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(624, 31)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 253)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(624, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 222)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(624, 31)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 222)
        '
        'Bar1
        '
        Me.Bar1.BarName = "ações"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemGravar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemRemover, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "ações"
        '
        'BarButtonItemGravar
        '
        Me.BarButtonItemGravar.Caption = "Gravar"
        Me.BarButtonItemGravar.Id = 0
        Me.BarButtonItemGravar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemGravar.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItemGravar.Name = "BarButtonItemGravar"
        '
        'BarButtonItemRemover
        '
        Me.BarButtonItemRemover.Caption = "Remover"
        Me.BarButtonItemRemover.Id = 1
        Me.BarButtonItemRemover.ImageOptions.Image = CType(resources.GetObject("BarButtonItemRemover.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItemRemover.Name = "BarButtonItemRemover"
        '
        'BarButtonItemFechar
        '
        Me.BarButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.BarButtonItemFechar.Caption = "Fechar"
        Me.BarButtonItemFechar.Id = 2
        Me.BarButtonItemFechar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemFechar.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItemFechar.Name = "BarButtonItemFechar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(90, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Fornecedor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(107, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Fatura2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(113, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Fatura"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 146)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Obs."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(296, 122)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Valor Total Fatura"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(328, 90)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "ValorTotal2"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(331, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Valor Total"
        '
        'FrmCustoTransportesView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "FrmCustoTransportesView"
        Me.Size = New System.Drawing.Size(624, 253)
        Me.Text = "FrmCustoTransportesView"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.TextEditCodigoFornecedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditFornecedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditFatura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditFatura2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCusto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCusto2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MemoEditObsTransporte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditIdFatura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditIdFatura2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TextEditIdFatura2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents TextEditIdFatura As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents TextEditTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents MemoEditObsTransporte As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents TextEditCusto2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCusto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditFatura2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditFatura As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditFornecedor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoFornecedor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItemGravar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItemRemover As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItemFechar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
End Class
