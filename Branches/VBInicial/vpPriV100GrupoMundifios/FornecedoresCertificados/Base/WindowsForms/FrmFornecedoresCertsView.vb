'Imports System
'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Text
'Imports System.Threading.Tasks
'Imports Primavera.Extensibility.BusinessEntities
'Imports Primavera.Extensibility.CustomForm
'Imports StdBE100
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

'Public Class FrmFornecedoresCertsViews
'    Inherits CustomForm

'    Friend WithEvents TextEditCodigoCliente As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
'    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
'    Friend WithEvents BarButtonItemGravar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents BarButtonItemFechar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents Label2 As Windows.Forms.Label
'    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents LookUpEditClasse As DevExpress.XtraEditors.LookUpEdit
'    Friend WithEvents Label3 As Windows.Forms.Label
'    Friend WithEvents DateEditOekotex As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents TextEditOekotex As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents Label1 As Windows.Forms.Label
'    Friend WithEvents CheckEditOekotex As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents DateEditOcs As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents DateEditSupima As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents DateEditEgypt As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents DateEditBci As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents DateEditGots As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents CheckEditSupima As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents CheckEditOcs As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents CheckEditBci As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents CheckEditEgypt As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents CheckEditGots As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents LookUpEditClasse2 As DevExpress.XtraEditors.LookUpEdit
'    Friend WithEvents Label4 As Windows.Forms.Label
'    Friend WithEvents DateEditOekotex2 As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents TextEditOekotex2 As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents Label5 As Windows.Forms.Label
'    Friend WithEvents CheckEditOekotex2 As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents DateEditRcs As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents DateEditGrs As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents CheckEditRcs As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents CheckEditGrs As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents DateEdit14001 As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents DateEditFairTrade As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents DateEditSa As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents DateEdit9001 As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents CheckEdit14001 As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents CheckEditSa As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents CheckEditFairTrade As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents CheckEdit9001 As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents TextEditEuropeanFlax As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents DateEditFlax As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents CheckEditFlax As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents TextEditFsc As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents DateEditFsc As DevExpress.XtraEditors.DateEdit
'    Friend WithEvents CheckEditFsc As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents TextEditSupima As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditCliente As DevExpress.XtraEditors.TextEdit

'    Private Sub InitializeComponent()
'        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFornecedoresCertsFilopaView))
'        Me.TextEditCodigoCliente = New DevExpress.XtraEditors.TextEdit()
'        Me.BarManager1 = New DevExpress.XtraBars.BarManager()
'        Me.Bar1 = New DevExpress.XtraBars.Bar()
'        Me.BarButtonItemGravar = New DevExpress.XtraBars.BarButtonItem()
'        Me.BarButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
'        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
'        Me.TextEditCliente = New DevExpress.XtraEditors.TextEdit()
'        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
'        Me.LookUpEditClasse = New DevExpress.XtraEditors.LookUpEdit()
'        Me.Label3 = New System.Windows.Forms.Label()
'        Me.DateEditOekotex = New DevExpress.XtraEditors.DateEdit()
'        Me.TextEditOekotex = New DevExpress.XtraEditors.TextEdit()
'        Me.Label1 = New System.Windows.Forms.Label()
'        Me.CheckEditOekotex = New DevExpress.XtraEditors.CheckEdit()
'        Me.Label2 = New System.Windows.Forms.Label()
'        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
'        Me.LookUpEditClasse2 = New DevExpress.XtraEditors.LookUpEdit()
'        Me.Label4 = New System.Windows.Forms.Label()
'        Me.DateEditOekotex2 = New DevExpress.XtraEditors.DateEdit()
'        Me.TextEditOekotex2 = New DevExpress.XtraEditors.TextEdit()
'        Me.Label5 = New System.Windows.Forms.Label()
'        Me.CheckEditOekotex2 = New DevExpress.XtraEditors.CheckEdit()
'        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
'        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
'        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
'        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
'        Me.CheckEditGots = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEditEgypt = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEditBci = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEditOcs = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEditSupima = New DevExpress.XtraEditors.CheckEdit()
'        Me.DateEditGots = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEditBci = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEditEgypt = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEditSupima = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEditOcs = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEditRcs = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEditGrs = New DevExpress.XtraEditors.DateEdit()
'        Me.CheckEditRcs = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEditGrs = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEditFsc = New DevExpress.XtraEditors.CheckEdit()
'        Me.DateEditFsc = New DevExpress.XtraEditors.DateEdit()
'        Me.TextEditFsc = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditEuropeanFlax = New DevExpress.XtraEditors.TextEdit()
'        Me.DateEditFlax = New DevExpress.XtraEditors.DateEdit()
'        Me.CheckEditFlax = New DevExpress.XtraEditors.CheckEdit()
'        Me.DateEdit14001 = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEditFairTrade = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEditSa = New DevExpress.XtraEditors.DateEdit()
'        Me.DateEdit9001 = New DevExpress.XtraEditors.DateEdit()
'        Me.CheckEdit14001 = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEditSa = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEditFairTrade = New DevExpress.XtraEditors.CheckEdit()
'        Me.CheckEdit9001 = New DevExpress.XtraEditors.CheckEdit()
'        Me.TextEditSupima = New DevExpress.XtraEditors.TextEdit()
'        CType(Me.TextEditCodigoCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl1.SuspendLayout()
'        CType(Me.LookUpEditClasse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditOekotex.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditOekotex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditOekotex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditOekotex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl2.SuspendLayout()
'        CType(Me.LookUpEditClasse2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditOekotex2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditOekotex2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditOekotex2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditOekotex2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl3.SuspendLayout()
'        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl4.SuspendLayout()
'        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl5.SuspendLayout()
'        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl6.SuspendLayout()
'        CType(Me.CheckEditGots.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditEgypt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditBci.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditOcs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditSupima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditGots.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditGots.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditBci.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditBci.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditEgypt.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditEgypt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditSupima.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditSupima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditOcs.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditOcs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditRcs.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditRcs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditGrs.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditGrs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditRcs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditGrs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditFsc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditFsc.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditFsc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditFsc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditEuropeanFlax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditFlax.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditFlax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditFlax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEdit14001.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEdit14001.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditFairTrade.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditFairTrade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditSa.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEditSa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEdit9001.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.DateEdit9001.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEdit14001.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditSa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEditFairTrade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.CheckEdit9001.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditSupima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.SuspendLayout()
'        '
'        'TextEditCodigoCliente
'        '
'        Me.TextEditCodigoCliente.Location = New System.Drawing.Point(81, 40)
'        Me.TextEditCodigoCliente.Name = "TextEditCodigoCliente"
'        Me.TextEditCodigoCliente.Size = New System.Drawing.Size(100, 20)
'        Me.TextEditCodigoCliente.TabIndex = 0
'        '
'        'BarManager1
'        '
'        Me.BarManager1.AllowMoveBarOnToolbar = False
'        Me.BarManager1.AllowQuickCustomization = False
'        Me.BarManager1.AllowShowToolbarsPopup = False
'        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
'        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
'        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
'        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
'        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
'        Me.BarManager1.Form = Me
'        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItemGravar, Me.BarButtonItemFechar})
'        Me.BarManager1.MaxItemId = 2
'        '
'        'Bar1
'        '
'        Me.Bar1.BarName = "ações"
'        Me.Bar1.DockCol = 0
'        Me.Bar1.DockRow = 0
'        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
'        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemGravar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
'        Me.Bar1.OptionsBar.DrawDragBorder = False
'        Me.Bar1.OptionsBar.UseWholeRow = True
'        Me.Bar1.Text = "ações"
'        '
'        'BarButtonItemGravar
'        '
'        Me.BarButtonItemGravar.Caption = "Gravar"
'        Me.BarButtonItemGravar.Id = 0
'        Me.BarButtonItemGravar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemGravar.ImageOptions.Image"), System.Drawing.Image)
'        Me.BarButtonItemGravar.Name = "BarButtonItemGravar"
'        '
'        'BarButtonItemFechar
'        '
'        Me.BarButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
'        Me.BarButtonItemFechar.Caption = "Fechar"
'        Me.BarButtonItemFechar.Id = 1
'        Me.BarButtonItemFechar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemFechar.ImageOptions.Image"), System.Drawing.Image)
'        Me.BarButtonItemFechar.Name = "BarButtonItemFechar"
'        '
'        'barDockControlTop
'        '
'        Me.barDockControlTop.CausesValidation = False
'        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
'        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
'        Me.barDockControlTop.Manager = Me.BarManager1
'        Me.barDockControlTop.Size = New System.Drawing.Size(776, 31)
'        '
'        'barDockControlBottom
'        '
'        Me.barDockControlBottom.CausesValidation = False
'        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
'        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 374)
'        Me.barDockControlBottom.Manager = Me.BarManager1
'        Me.barDockControlBottom.Size = New System.Drawing.Size(776, 0)
'        '
'        'barDockControlLeft
'        '
'        Me.barDockControlLeft.CausesValidation = False
'        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
'        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
'        Me.barDockControlLeft.Manager = Me.BarManager1
'        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 343)
'        '
'        'barDockControlRight
'        '
'        Me.barDockControlRight.CausesValidation = False
'        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
'        Me.barDockControlRight.Location = New System.Drawing.Point(776, 31)
'        Me.barDockControlRight.Manager = Me.BarManager1
'        Me.barDockControlRight.Size = New System.Drawing.Size(0, 343)
'        '
'        'TextEditCliente
'        '
'        Me.TextEditCliente.Location = New System.Drawing.Point(187, 40)
'        Me.TextEditCliente.Name = "TextEditCliente"
'        Me.TextEditCliente.Size = New System.Drawing.Size(583, 20)
'        Me.TextEditCliente.TabIndex = 5
'        '
'        'GroupControl1
'        '
'        Me.GroupControl1.Controls.Add(Me.LookUpEditClasse)
'        Me.GroupControl1.Controls.Add(Me.Label3)
'        Me.GroupControl1.Controls.Add(Me.DateEditOekotex)
'        Me.GroupControl1.Controls.Add(Me.TextEditOekotex)
'        Me.GroupControl1.Controls.Add(Me.Label1)
'        Me.GroupControl1.Controls.Add(Me.CheckEditOekotex)
'        Me.GroupControl1.Location = New System.Drawing.Point(12, 66)
'        Me.GroupControl1.Name = "GroupControl1"
'        Me.GroupControl1.Size = New System.Drawing.Size(758, 57)
'        Me.GroupControl1.TabIndex = 6
'        Me.GroupControl1.Text = "OKOTEX"
'        '
'        'LookUpEditClasse
'        '
'        Me.LookUpEditClasse.Location = New System.Drawing.Point(542, 26)
'        Me.LookUpEditClasse.MenuManager = Me.BarManager1
'        Me.LookUpEditClasse.Name = "LookUpEditClasse"
'        Me.LookUpEditClasse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.LookUpEditClasse.Size = New System.Drawing.Size(191, 20)
'        Me.LookUpEditClasse.TabIndex = 10
'        '
'        'Label3
'        '
'        Me.Label3.AutoSize = True
'        Me.Label3.Location = New System.Drawing.Point(498, 29)
'        Me.Label3.Name = "Label3"
'        Me.Label3.Size = New System.Drawing.Size(38, 13)
'        Me.Label3.TabIndex = 9
'        Me.Label3.Text = "Classe"
'        '
'        'DateEditOekotex
'        '
'        Me.DateEditOekotex.EditValue = Nothing
'        Me.DateEditOekotex.Location = New System.Drawing.Point(351, 26)
'        Me.DateEditOekotex.MenuManager = Me.BarManager1
'        Me.DateEditOekotex.Name = "DateEditOekotex"
'        Me.DateEditOekotex.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditOekotex.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditOekotex.Size = New System.Drawing.Size(141, 20)
'        Me.DateEditOekotex.TabIndex = 8
'        '
'        'TextEditOekotex
'        '
'        Me.TextEditOekotex.Location = New System.Drawing.Point(154, 26)
'        Me.TextEditOekotex.Name = "TextEditOekotex"
'        Me.TextEditOekotex.Size = New System.Drawing.Size(191, 20)
'        Me.TextEditOekotex.TabIndex = 7
'        '
'        'Label1
'        '
'        Me.Label1.AutoSize = True
'        Me.Label1.Location = New System.Drawing.Point(86, 29)
'        Me.Label1.Name = "Label1"
'        Me.Label1.Size = New System.Drawing.Size(71, 13)
'        Me.Label1.TabIndex = 1
'        Me.Label1.Text = "NºCertificado"
'        '
'        'CheckEditOekotex
'        '
'        Me.CheckEditOekotex.Location = New System.Drawing.Point(5, 26)
'        Me.CheckEditOekotex.MenuManager = Me.BarManager1
'        Me.CheckEditOekotex.Name = "CheckEditOekotex"
'        Me.CheckEditOekotex.Properties.Caption = "OEKOTEX"
'        Me.CheckEditOekotex.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditOekotex.TabIndex = 0
'        '
'        'Label2
'        '
'        Me.Label2.AutoSize = True
'        Me.Label2.Location = New System.Drawing.Point(14, 43)
'        Me.Label2.Name = "Label2"
'        Me.Label2.Size = New System.Drawing.Size(61, 13)
'        Me.Label2.TabIndex = 2
'        Me.Label2.Text = "Fornecedor"
'        '
'        'GroupControl2
'        '
'        Me.GroupControl2.Controls.Add(Me.LookUpEditClasse2)
'        Me.GroupControl2.Controls.Add(Me.Label4)
'        Me.GroupControl2.Controls.Add(Me.DateEditOekotex2)
'        Me.GroupControl2.Controls.Add(Me.TextEditOekotex2)
'        Me.GroupControl2.Controls.Add(Me.Label5)
'        Me.GroupControl2.Controls.Add(Me.CheckEditOekotex2)
'        Me.GroupControl2.Location = New System.Drawing.Point(12, 129)
'        Me.GroupControl2.Name = "GroupControl2"
'        Me.GroupControl2.Size = New System.Drawing.Size(758, 57)
'        Me.GroupControl2.TabIndex = 11
'        Me.GroupControl2.Text = "OKOTEX2"
'        '
'        'LookUpEditClasse2
'        '
'        Me.LookUpEditClasse2.Location = New System.Drawing.Point(542, 26)
'        Me.LookUpEditClasse2.MenuManager = Me.BarManager1
'        Me.LookUpEditClasse2.Name = "LookUpEditClasse2"
'        Me.LookUpEditClasse2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.LookUpEditClasse2.Size = New System.Drawing.Size(191, 20)
'        Me.LookUpEditClasse2.TabIndex = 10
'        '
'        'Label4
'        '
'        Me.Label4.AutoSize = True
'        Me.Label4.Location = New System.Drawing.Point(498, 29)
'        Me.Label4.Name = "Label4"
'        Me.Label4.Size = New System.Drawing.Size(38, 13)
'        Me.Label4.TabIndex = 9
'        Me.Label4.Text = "Classe"
'        '
'        'DateEditOekotex2
'        '
'        Me.DateEditOekotex2.EditValue = Nothing
'        Me.DateEditOekotex2.Location = New System.Drawing.Point(351, 26)
'        Me.DateEditOekotex2.MenuManager = Me.BarManager1
'        Me.DateEditOekotex2.Name = "DateEditOekotex2"
'        Me.DateEditOekotex2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditOekotex2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditOekotex2.Size = New System.Drawing.Size(141, 20)
'        Me.DateEditOekotex2.TabIndex = 8
'        '
'        'TextEditOekotex2
'        '
'        Me.TextEditOekotex2.Location = New System.Drawing.Point(154, 26)
'        Me.TextEditOekotex2.Name = "TextEditOekotex2"
'        Me.TextEditOekotex2.Size = New System.Drawing.Size(191, 20)
'        Me.TextEditOekotex2.TabIndex = 7
'        '
'        'Label5
'        '
'        Me.Label5.AutoSize = True
'        Me.Label5.Location = New System.Drawing.Point(86, 29)
'        Me.Label5.Name = "Label5"
'        Me.Label5.Size = New System.Drawing.Size(71, 13)
'        Me.Label5.TabIndex = 1
'        Me.Label5.Text = "NºCertificado"
'        '
'        'CheckEditOekotex2
'        '
'        Me.CheckEditOekotex2.Location = New System.Drawing.Point(5, 26)
'        Me.CheckEditOekotex2.MenuManager = Me.BarManager1
'        Me.CheckEditOekotex2.Name = "CheckEditOekotex2"
'        Me.CheckEditOekotex2.Properties.Caption = "OEKOTEX"
'        Me.CheckEditOekotex2.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditOekotex2.TabIndex = 0
'        '
'        'GroupControl3
'        '
'        Me.GroupControl3.Controls.Add(Me.TextEditSupima)
'        Me.GroupControl3.Controls.Add(Me.DateEditOcs)
'        Me.GroupControl3.Controls.Add(Me.DateEditSupima)
'        Me.GroupControl3.Controls.Add(Me.DateEditEgypt)
'        Me.GroupControl3.Controls.Add(Me.DateEditBci)
'        Me.GroupControl3.Controls.Add(Me.DateEditGots)
'        Me.GroupControl3.Controls.Add(Me.CheckEditSupima)
'        Me.GroupControl3.Controls.Add(Me.CheckEditOcs)
'        Me.GroupControl3.Controls.Add(Me.CheckEditBci)
'        Me.GroupControl3.Controls.Add(Me.CheckEditEgypt)
'        Me.GroupControl3.Controls.Add(Me.CheckEditGots)
'        Me.GroupControl3.Location = New System.Drawing.Point(12, 192)
'        Me.GroupControl3.Name = "GroupControl3"
'        Me.GroupControl3.Size = New System.Drawing.Size(179, 174)
'        Me.GroupControl3.TabIndex = 12
'        Me.GroupControl3.Text = "Cotton"
'        '
'        'GroupControl4
'        '
'        Me.GroupControl4.Controls.Add(Me.DateEdit14001)
'        Me.GroupControl4.Controls.Add(Me.DateEditFairTrade)
'        Me.GroupControl4.Controls.Add(Me.DateEditSa)
'        Me.GroupControl4.Controls.Add(Me.DateEdit9001)
'        Me.GroupControl4.Controls.Add(Me.CheckEdit14001)
'        Me.GroupControl4.Controls.Add(Me.CheckEditSa)
'        Me.GroupControl4.Controls.Add(Me.CheckEditFairTrade)
'        Me.GroupControl4.Controls.Add(Me.CheckEdit9001)
'        Me.GroupControl4.Location = New System.Drawing.Point(575, 192)
'        Me.GroupControl4.Name = "GroupControl4"
'        Me.GroupControl4.Size = New System.Drawing.Size(195, 174)
'        Me.GroupControl4.TabIndex = 13
'        Me.GroupControl4.Text = "Quality"
'        '
'        'GroupControl5
'        '
'        Me.GroupControl5.Controls.Add(Me.TextEditEuropeanFlax)
'        Me.GroupControl5.Controls.Add(Me.DateEditFlax)
'        Me.GroupControl5.Controls.Add(Me.CheckEditFlax)
'        Me.GroupControl5.Controls.Add(Me.TextEditFsc)
'        Me.GroupControl5.Controls.Add(Me.DateEditFsc)
'        Me.GroupControl5.Controls.Add(Me.CheckEditFsc)
'        Me.GroupControl5.Location = New System.Drawing.Point(390, 192)
'        Me.GroupControl5.Name = "GroupControl5"
'        Me.GroupControl5.Size = New System.Drawing.Size(179, 174)
'        Me.GroupControl5.TabIndex = 13
'        Me.GroupControl5.Text = "Others"
'        '
'        'GroupControl6
'        '
'        Me.GroupControl6.Controls.Add(Me.DateEditRcs)
'        Me.GroupControl6.Controls.Add(Me.DateEditGrs)
'        Me.GroupControl6.Controls.Add(Me.CheckEditRcs)
'        Me.GroupControl6.Controls.Add(Me.CheckEditGrs)
'        Me.GroupControl6.Location = New System.Drawing.Point(197, 192)
'        Me.GroupControl6.Name = "GroupControl6"
'        Me.GroupControl6.Size = New System.Drawing.Size(187, 174)
'        Me.GroupControl6.TabIndex = 13
'        Me.GroupControl6.Text = "Recycled"
'        '
'        'CheckEditGots
'        '
'        Me.CheckEditGots.Location = New System.Drawing.Point(5, 23)
'        Me.CheckEditGots.MenuManager = Me.BarManager1
'        Me.CheckEditGots.Name = "CheckEditGots"
'        Me.CheckEditGots.Properties.Caption = "GOTS"
'        Me.CheckEditGots.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditGots.TabIndex = 0
'        '
'        'CheckEditEgypt
'        '
'        Me.CheckEditEgypt.Location = New System.Drawing.Point(5, 98)
'        Me.CheckEditEgypt.MenuManager = Me.BarManager1
'        Me.CheckEditEgypt.Name = "CheckEditEgypt"
'        Me.CheckEditEgypt.Properties.Caption = "Egypt"
'        Me.CheckEditEgypt.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditEgypt.TabIndex = 1
'        '
'        'CheckEditBci
'        '
'        Me.CheckEditBci.Location = New System.Drawing.Point(5, 73)
'        Me.CheckEditBci.MenuManager = Me.BarManager1
'        Me.CheckEditBci.Name = "CheckEditBci"
'        Me.CheckEditBci.Properties.Caption = "BCI"
'        Me.CheckEditBci.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditBci.TabIndex = 2
'        '
'        'CheckEditOcs
'        '
'        Me.CheckEditOcs.Location = New System.Drawing.Point(5, 48)
'        Me.CheckEditOcs.MenuManager = Me.BarManager1
'        Me.CheckEditOcs.Name = "CheckEditOcs"
'        Me.CheckEditOcs.Properties.Caption = "OCS"
'        Me.CheckEditOcs.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditOcs.TabIndex = 3
'        '
'        'CheckEditSupima
'        '
'        Me.CheckEditSupima.Location = New System.Drawing.Point(5, 124)
'        Me.CheckEditSupima.MenuManager = Me.BarManager1
'        Me.CheckEditSupima.Name = "CheckEditSupima"
'        Me.CheckEditSupima.Properties.Caption = "Supima"
'        Me.CheckEditSupima.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditSupima.TabIndex = 4
'        '
'        'DateEditGots
'        '
'        Me.DateEditGots.EditValue = Nothing
'        Me.DateEditGots.Location = New System.Drawing.Point(72, 23)
'        Me.DateEditGots.MenuManager = Me.BarManager1
'        Me.DateEditGots.Name = "DateEditGots"
'        Me.DateEditGots.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditGots.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditGots.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditGots.TabIndex = 11
'        '
'        'DateEditBci
'        '
'        Me.DateEditBci.EditValue = Nothing
'        Me.DateEditBci.Location = New System.Drawing.Point(72, 73)
'        Me.DateEditBci.MenuManager = Me.BarManager1
'        Me.DateEditBci.Name = "DateEditBci"
'        Me.DateEditBci.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditBci.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditBci.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditBci.TabIndex = 12
'        '
'        'DateEditEgypt
'        '
'        Me.DateEditEgypt.EditValue = Nothing
'        Me.DateEditEgypt.Location = New System.Drawing.Point(72, 98)
'        Me.DateEditEgypt.MenuManager = Me.BarManager1
'        Me.DateEditEgypt.Name = "DateEditEgypt"
'        Me.DateEditEgypt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditEgypt.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditEgypt.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditEgypt.TabIndex = 13
'        '
'        'DateEditSupima
'        '
'        Me.DateEditSupima.EditValue = Nothing
'        Me.DateEditSupima.Location = New System.Drawing.Point(72, 124)
'        Me.DateEditSupima.MenuManager = Me.BarManager1
'        Me.DateEditSupima.Name = "DateEditSupima"
'        Me.DateEditSupima.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditSupima.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditSupima.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditSupima.TabIndex = 14
'        '
'        'DateEditOcs
'        '
'        Me.DateEditOcs.EditValue = Nothing
'        Me.DateEditOcs.Location = New System.Drawing.Point(72, 48)
'        Me.DateEditOcs.MenuManager = Me.BarManager1
'        Me.DateEditOcs.Name = "DateEditOcs"
'        Me.DateEditOcs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditOcs.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditOcs.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditOcs.TabIndex = 15
'        '
'        'DateEditRcs
'        '
'        Me.DateEditRcs.EditValue = Nothing
'        Me.DateEditRcs.Location = New System.Drawing.Point(80, 50)
'        Me.DateEditRcs.MenuManager = Me.BarManager1
'        Me.DateEditRcs.Name = "DateEditRcs"
'        Me.DateEditRcs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditRcs.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditRcs.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditRcs.TabIndex = 19
'        '
'        'DateEditGrs
'        '
'        Me.DateEditGrs.EditValue = Nothing
'        Me.DateEditGrs.Location = New System.Drawing.Point(80, 24)
'        Me.DateEditGrs.MenuManager = Me.BarManager1
'        Me.DateEditGrs.Name = "DateEditGrs"
'        Me.DateEditGrs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditGrs.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditGrs.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditGrs.TabIndex = 18
'        '
'        'CheckEditRcs
'        '
'        Me.CheckEditRcs.Location = New System.Drawing.Point(5, 50)
'        Me.CheckEditRcs.MenuManager = Me.BarManager1
'        Me.CheckEditRcs.Name = "CheckEditRcs"
'        Me.CheckEditRcs.Properties.Caption = "RCS"
'        Me.CheckEditRcs.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditRcs.TabIndex = 17
'        '
'        'CheckEditGrs
'        '
'        Me.CheckEditGrs.Location = New System.Drawing.Point(5, 24)
'        Me.CheckEditGrs.MenuManager = Me.BarManager1
'        Me.CheckEditGrs.Name = "CheckEditGrs"
'        Me.CheckEditGrs.Properties.Caption = "GRS"
'        Me.CheckEditGrs.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditGrs.TabIndex = 16
'        '
'        'CheckEditFsc
'        '
'        Me.CheckEditFsc.Location = New System.Drawing.Point(5, 24)
'        Me.CheckEditFsc.MenuManager = Me.BarManager1
'        Me.CheckEditFsc.Name = "CheckEditFsc"
'        Me.CheckEditFsc.Properties.Caption = "FSC"
'        Me.CheckEditFsc.Size = New System.Drawing.Size(61, 19)
'        Me.CheckEditFsc.TabIndex = 20
'        '
'        'DateEditFsc
'        '
'        Me.DateEditFsc.EditValue = Nothing
'        Me.DateEditFsc.Location = New System.Drawing.Point(72, 24)
'        Me.DateEditFsc.MenuManager = Me.BarManager1
'        Me.DateEditFsc.Name = "DateEditFsc"
'        Me.DateEditFsc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditFsc.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditFsc.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditFsc.TabIndex = 20
'        '
'        'TextEditFsc
'        '
'        Me.TextEditFsc.Location = New System.Drawing.Point(5, 56)
'        Me.TextEditFsc.Name = "TextEditFsc"
'        Me.TextEditFsc.Size = New System.Drawing.Size(169, 20)
'        Me.TextEditFsc.TabIndex = 11
'        '
'        'TextEditEuropeanFlax
'        '
'        Me.TextEditEuropeanFlax.Location = New System.Drawing.Point(5, 141)
'        Me.TextEditEuropeanFlax.Name = "TextEditEuropeanFlax"
'        Me.TextEditEuropeanFlax.Size = New System.Drawing.Size(169, 20)
'        Me.TextEditEuropeanFlax.TabIndex = 21
'        '
'        'DateEditFlax
'        '
'        Me.DateEditFlax.EditValue = Nothing
'        Me.DateEditFlax.Location = New System.Drawing.Point(72, 115)
'        Me.DateEditFlax.MenuManager = Me.BarManager1
'        Me.DateEditFlax.Name = "DateEditFlax"
'        Me.DateEditFlax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditFlax.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditFlax.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditFlax.TabIndex = 22
'        '
'        'CheckEditFlax
'        '
'        Me.CheckEditFlax.Location = New System.Drawing.Point(5, 115)
'        Me.CheckEditFlax.MenuManager = Me.BarManager1
'        Me.CheckEditFlax.Name = "CheckEditFlax"
'        Me.CheckEditFlax.Properties.Caption = "Euro Flax"
'        Me.CheckEditFlax.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditFlax.TabIndex = 23
'        '
'        'DateEdit14001
'        '
'        Me.DateEdit14001.EditValue = Nothing
'        Me.DateEdit14001.Location = New System.Drawing.Point(88, 57)
'        Me.DateEdit14001.MenuManager = Me.BarManager1
'        Me.DateEdit14001.Name = "DateEdit14001"
'        Me.DateEdit14001.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEdit14001.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEdit14001.Size = New System.Drawing.Size(102, 20)
'        Me.DateEdit14001.TabIndex = 23
'        '
'        'DateEditFairTrade
'        '
'        Me.DateEditFairTrade.EditValue = Nothing
'        Me.DateEditFairTrade.Location = New System.Drawing.Point(88, 142)
'        Me.DateEditFairTrade.MenuManager = Me.BarManager1
'        Me.DateEditFairTrade.Name = "DateEditFairTrade"
'        Me.DateEditFairTrade.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditFairTrade.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditFairTrade.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditFairTrade.TabIndex = 22
'        '
'        'DateEditSa
'        '
'        Me.DateEditSa.EditValue = Nothing
'        Me.DateEditSa.Location = New System.Drawing.Point(88, 115)
'        Me.DateEditSa.MenuManager = Me.BarManager1
'        Me.DateEditSa.Name = "DateEditSa"
'        Me.DateEditSa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditSa.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEditSa.Size = New System.Drawing.Size(102, 20)
'        Me.DateEditSa.TabIndex = 21
'        '
'        'DateEdit9001
'        '
'        Me.DateEdit9001.EditValue = Nothing
'        Me.DateEdit9001.Location = New System.Drawing.Point(88, 27)
'        Me.DateEdit9001.MenuManager = Me.BarManager1
'        Me.DateEdit9001.Name = "DateEdit9001"
'        Me.DateEdit9001.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEdit9001.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.DateEdit9001.Size = New System.Drawing.Size(102, 20)
'        Me.DateEdit9001.TabIndex = 20
'        '
'        'CheckEdit14001
'        '
'        Me.CheckEdit14001.Location = New System.Drawing.Point(5, 57)
'        Me.CheckEdit14001.MenuManager = Me.BarManager1
'        Me.CheckEdit14001.Name = "CheckEdit14001"
'        Me.CheckEdit14001.Properties.Caption = "ISO14001"
'        Me.CheckEdit14001.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEdit14001.TabIndex = 19
'        '
'        'CheckEditSa
'        '
'        Me.CheckEditSa.Location = New System.Drawing.Point(5, 115)
'        Me.CheckEditSa.MenuManager = Me.BarManager1
'        Me.CheckEditSa.Name = "CheckEditSa"
'        Me.CheckEditSa.Properties.Caption = "SA 8000"
'        Me.CheckEditSa.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditSa.TabIndex = 18
'        '
'        'CheckEditFairTrade
'        '
'        Me.CheckEditFairTrade.Location = New System.Drawing.Point(5, 142)
'        Me.CheckEditFairTrade.MenuManager = Me.BarManager1
'        Me.CheckEditFairTrade.Name = "CheckEditFairTrade"
'        Me.CheckEditFairTrade.Properties.Caption = "Fair Trade"
'        Me.CheckEditFairTrade.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEditFairTrade.TabIndex = 17
'        '
'        'CheckEdit9001
'        '
'        Me.CheckEdit9001.Location = New System.Drawing.Point(5, 27)
'        Me.CheckEdit9001.MenuManager = Me.BarManager1
'        Me.CheckEdit9001.Name = "CheckEdit9001"
'        Me.CheckEdit9001.Properties.Caption = "ISO 9001"
'        Me.CheckEdit9001.Size = New System.Drawing.Size(75, 19)
'        Me.CheckEdit9001.TabIndex = 16
'        '
'        'TextEditSupima
'        '
'        Me.TextEditSupima.Location = New System.Drawing.Point(5, 149)
'        Me.TextEditSupima.Name = "TextEditSupima"
'        Me.TextEditSupima.Size = New System.Drawing.Size(169, 20)
'        Me.TextEditSupima.TabIndex = 24
'        '
'        'FrmFornecedoresCertsView
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
'        Me.Controls.Add(Me.GroupControl4)
'        Me.Controls.Add(Me.GroupControl5)
'        Me.Controls.Add(Me.GroupControl6)
'        Me.Controls.Add(Me.GroupControl3)
'        Me.Controls.Add(Me.GroupControl2)
'        Me.Controls.Add(Me.Label2)
'        Me.Controls.Add(Me.GroupControl1)
'        Me.Controls.Add(Me.TextEditCliente)
'        Me.Controls.Add(Me.TextEditCodigoCliente)
'        Me.Controls.Add(Me.barDockControlLeft)
'        Me.Controls.Add(Me.barDockControlRight)
'        Me.Controls.Add(Me.barDockControlBottom)
'        Me.Controls.Add(Me.barDockControlTop)
'        Me.Name = "FrmFornecedoresCertsView"
'        Me.Size = New System.Drawing.Size(776, 374)
'        Me.Text = "Cliente Certificados"
'        CType(Me.TextEditCodigoCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl1.ResumeLayout(False)
'        Me.GroupControl1.PerformLayout()
'        CType(Me.LookUpEditClasse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditOekotex.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditOekotex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditOekotex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditOekotex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl2.ResumeLayout(False)
'        Me.GroupControl2.PerformLayout()
'        CType(Me.LookUpEditClasse2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditOekotex2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditOekotex2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditOekotex2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditOekotex2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl3.ResumeLayout(False)
'        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl4.ResumeLayout(False)
'        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl5.ResumeLayout(False)
'        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl6.ResumeLayout(False)
'        CType(Me.CheckEditGots.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditEgypt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditBci.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditOcs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditSupima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditGots.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditGots.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditBci.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditBci.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditEgypt.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditEgypt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditSupima.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditSupima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditOcs.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditOcs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditRcs.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditRcs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditGrs.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditGrs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditRcs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditGrs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditFsc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditFsc.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditFsc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditFsc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditEuropeanFlax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditFlax.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditFlax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditFlax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEdit14001.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEdit14001.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditFairTrade.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditFairTrade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditSa.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEditSa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEdit9001.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.DateEdit9001.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEdit14001.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditSa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEditFairTrade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.CheckEdit9001.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditSupima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.ResumeLayout(False)
'        Me.PerformLayout()

'    End Sub

'    Dim ListaClasse As StdBELista

'    Private Sub BarButtonItemGravar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemGravar.ItemClick

'        If Me.TextEditCodigoCliente.EditValue <> "" Then
'            AlteraCertsClientes()

'        Else

'            MsgBox("O Fornecedor não está identificado.", vbInformation + vbOKOnly)
'        End If
'        Me.DialogResult = DialogResult.OK
'        Me.Close()


'    End Sub

'    Private Sub CarregaDados()

'        If Me.TextEditCliente.EditValue = String.Empty Then
'            Me.TextEditCodigoCliente.EditValue = String.Empty
'            Exit Sub
'        End If

'        Dim listCerts As StdBELista
'        Dim sql As String
'        sql = "SELECT Fornecedor, Nome, CDU_Oekotex ,CDU_OekotexNum , CDU_OekotexData ,CDU_OekotexClasse ,  CDU_Oekotex2,    CDU_OekotexNum2, CDU_OekotexData2 ,   CDU_OekotexClasse2,  CDU_Gots ,   CDU_GotsData  ,  CDU_Ocs, CDU_OcsData ,CDU_Grs ,CDU_GrsData, CDU_Rcs, CDU_RcsData, CDU_Bci, CDU_BciData, CDU_Fsc, CDU_FscData, CDU_EgyptCotton, CDU_EgyptCottonData, CDU_Supima, CDU_SupimaData, CDU_SupimaNum, CDU_EuropeanFlax ,CDU_EuropeanFlaxData, CDU_Iso9001, CDU_Iso9001Data, CDU_Iso14001,    CDU_Iso14001Data ,   CDU_Sa8000,   CDU_Sa8000Data, CDU_Fairtrade, CDU_FairtradeData, CDU_FscNum, CDU_EuropeanflaxNum FROM Fornecedores WHERE Fornecedor= '" & Me.TextEditCodigoCliente.EditValue.Text & "'"

'        listCerts = BSO.Consulta(sql)
'        listCerts.Inicio()
'        '
'        'CheckBoxs
'        Me.CheckEditOekotex.EditValue = listCerts.Valor("CDU_Oekotex")
'        Me.CheckEditOekotex2.EditValue = listCerts.Valor("CDU_Oekotex2")
'        Me.CheckEditGots.EditValue = listCerts.Valor("CDU_Gots")
'        Me.CheckEditOcs.EditValue = listCerts.Valor("CDU_Ocs")
'        Me.CheckEditBci.EditValue = listCerts.Valor("CDU_BCI")
'        Me.CheckEditEgypt.EditValue = listCerts.Valor("CDU_EgyptCotton")
'        Me.CheckEditSupima.EditValue = listCerts.Valor("CDU_Supima")
'        Me.CheckEditGrs.EditValue = listCerts.Valor("CDU_Grs")
'        Me.CheckEditRcs.EditValue = listCerts.Valor("CDU_Rcs")
'        Me.CheckEditFsc.EditValue = listCerts.Valor("CDU_Fsc")
'        Me.CheckEditFlax.EditValue = listCerts.Valor("CDU_EuropeanFlax")
'        Me.CheckEdit9001.EditValue = listCerts.Valor("CDU_Iso9001")
'        Me.CheckEdit14001.EditValue = listCerts.Valor("CDU_Iso14001")
'        Me.CheckEditSa.EditValue = listCerts.Valor("CDU_Sa8000")
'        Me.CheckEditFairTrade.EditValue = listCerts.Valor("CDU_Fairtrade")

'        'Datas
'        Me.DateEditOekotex.EditValue = listCerts.Valor("CDU_OekotexData")
'        Me.DateEditOekotex2.EditValue = listCerts.Valor("CDU_OekotexData2")
'        Me.DateEditGots.EditValue = listCerts.Valor("CDU_GotsData")
'        Me.DateEditOcs.EditValue = listCerts.Valor("CDU_OcsData")
'        Me.DateEditBci.EditValue = listCerts.Valor("CDU_BCIData")
'        Me.DateEditEgypt.EditValue = listCerts.Valor("CDU_EgyptCottonData")
'        Me.DateEditSupima.EditValue = listCerts.Valor("CDU_SupimaData")
'        Me.DateEditGrs.EditValue = listCerts.Valor("CDU_GrsData")
'        Me.DateEditRcs.EditValue = listCerts.Valor("CDU_RcsData")
'        Me.DateEditFsc.EditValue = listCerts.Valor("CDU_FscData")
'        Me.DateEditFlax.EditValue = listCerts.Valor("CDU_EuropeanFlaxData")
'        Me.DateEdit9001.EditValue = listCerts.Valor("CDU_Iso9001Data")
'        Me.DateEdit14001.EditValue = listCerts.Valor("CDU_Iso14001Data")
'        Me.DateEditSa.EditValue = listCerts.Valor("CDU_Sa8000Data")
'        Me.DateEditFairTrade.EditValue = listCerts.Valor("CDU_FairtradeData")

'        'NumeroCertificados
'        Me.TextEditOekotex.EditValue = listCerts.Valor("CDU_OekotexNum")
'        Me.TextEditOekotex2.EditValue = listCerts.Valor("CDU_OekotexNum2")
'        Me.TextEditFsc.EditValue = listCerts.Valor("CDU_FscNum")
'        Me.TextEditEuropeanFlax.EditValue = listCerts.Valor("CDU_EuropeanflaxNum")
'        Me.TextEditSupima.EditValue = listCerts.Valor("CDU_SupimaNum")

'        'Classes Oekotex
'        Me.LookUpEditClasse.EditValue.Text = listCerts.Valor("CDU_OekotexClasse")
'        Me.LookUpEditClasse2.EditValue.Text = listCerts.Valor("CDU_OekotexClasse2")


'    End Sub


'    Function AlteraCertsClientes()


'        On Error GoTo Erro
'        Dim Campos As New StdBECampos

'        Campos = BSO.Base.Fornecedores.DaValorAtributos(Me.TextEditCodigoCliente.EditValue, "CDU_Oekotex", "CDU_Oekotex2", "CDU_Gots", "CDU_Ocs", "CDU_Bci", "CDU_EgyptCotton", "CDU_Supima", "CDU_SupimaData", "CDU_SupimaNum", "CDU_Grs", "CDU_Rcs", "CDU_Fsc", "CDU_EuropeanFlax", "CDU_Iso9001", "CDU_Iso14001", "CDU_Sa8000", "CDU_Fairtrade", "CDU_OekotexData", "CDU_OekotexData2", "CDU_GotsData", "CDU_OcsData", "CDU_BciData", "CDU_EgyptCottonData", "CDU_GrsData", "CDU_RcsData", "CDU_FscData", "CDU_EuropeanFlaxData", "CDU_Iso9001Data", "CDU_Iso14001Data", "CDU_Sa8000Data", "CDU_FairtradeData", "CDU_OekotexNum", "CDU_OekotexNum2", "CDU_FscNum", "CDU_EuropeanflaxNum", "CDU_OekotexClasse", "CDU_OekotexClasse2")


'        'CheckBoxs
'        Campos("CDU_Oekotex") = Me.CheckEditOekotex.EditValue
'        Campos("CDU_Oekotex2") = Me.CheckEditOekotex2.EditValue
'        Campos("CDU_Gots") = Me.CheckEditGots.EditValue
'        Campos("CDU_Ocs") = Me.CheckEditOcs.EditValue
'        Campos("CDU_Bci") = Me.CheckEditBci.EditValue
'        Campos("CDU_EgyptCotton") = Me.CheckEditEgypt.EditValue
'        Campos("CDU_Supima") = Me.CheckEditSupima.EditValue
'        Campos("CDU_Grs") = Me.CheckEditGrs.EditValue
'        Campos("CDU_Rcs") = Me.CheckEditRcs.EditValue
'        Campos("CDU_Fsc") = Me.CheckEditFsc.EditValue
'        Campos("CDU_EuropeanFlax") = Me.CheckEditFlax.EditValue
'        Campos("CDU_Iso9001") = Me.CheckEdit9001.EditValue
'        Campos("CDU_Iso14001") = Me.CheckEdit14001.EditValue
'        Campos("CDU_Sa8000") = Me.CheckEditSa.EditValue
'        Campos("CDU_Fairtrade") = Me.CheckEditFairTrade.EditValue

'        'Datas
'        Campos("CDU_OekotexData") = Format(Me.dtOekotex, "yyyy-MM-dd")
'        Campos("CDU_OekotexData2") = Format(Me.dtOekotex2, "yyyy-MM-dd")
'        Campos("CDU_GotsData") = Format(Me.dtGots, "yyyy-MM-dd")
'        Campos("CDU_OcsData") = Format(Me.dtOcs, "yyyy-MM-dd")
'        Campos("CDU_BciData") = Format(Me.dtBci, "yyyy-MM-dd")
'        Campos("CDU_EgyptCottonData") = Format(Me.dtEgypt, "yyyy-MM-dd")
'        Campos("CDU_SupimaData") = Format(Me.dtSupima, "yyyy-MM-dd")
'        Campos("CDU_GrsData") = Format(Me.dtGrs, "yyyy-MM-dd")
'        Campos("CDU_RcsData") = Format(Me.dtRcs, "yyyy-MM-dd")
'        Campos("CDU_FscData") = Format(Me.dtFsc, "yyyy-MM-dd")
'        Campos("CDU_EuropeanFlaxData") = Format(Me.dtFlax, "yyyy-MM-dd")
'        Campos("CDU_Iso9001Data") = Format(Me.dt9001, "yyyy-MM-dd")
'        Campos("CDU_Iso14001Data") = Format(Me.dt14001, "yyyy-MM-dd")
'        Campos("CDU_Sa8000Data") = Format(Me.dtSa, "yyyy-MM-dd")
'        Campos("CDU_FairtradeData") = Format(Me.dtFairtrade, "yyyy-MM-dd")

'        'NumeroCertificados
'        Campos("CDU_OekotexNum") = Me.TextEditOekotex.EditValue
'        Campos("CDU_OekotexNum2") = Me.TextEditOekotex2.EditValue
'        Campos("CDU_FscNum") = Me.TextEditFsc.EditValue
'        Campos("CDU_EuropeanflaxNum") = Me.TextEditEuropeanFlax.EditValue
'        Campos("CDU_SupimaNum") = Me.TextEditSupima.EditValue
'        'Classes
'        Campos("CDU_OekotexClasse") = Me.LookUpEditClasse.EditValue
'        Campos("CDU_OekotexClasse2") = Me.LookUpEditClasse2.EditValue

'        BSO.Base.Fornecedores.ActualizaValorAtributos(Me.TextEditCodigoCliente.EditValue, Campos)
'        CopiaFilopa()


'        Exit Function

'Erro:
'        MsgBox("Mundifios - Erro ao gravar: " & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)
'    End Function


'    Function CopiaFilopa()

'        On Error GoTo TrataErro
'        Dim ent As String
'        ent = BSO.Base.Fornecedores.DaValorAtributo(Me.TextEditCodigoCliente.EditValue, "CDU_EntidadeInterna")

'        If AbreObjEmpresa("FILOPA") Then
'            Dim Cli As New StdBELista
'            Cli = BSO.Consulta("select f.Cliente from Clientes f where f.ClienteAnulado='0' and f.CDU_EntidadeInterna='" & ent & "'")
'            Cli.Inicio()

'            If Cli.Vazia = False Then
'                Dim Campos As New StdBECampos
'                Campos = BSO.Base.Fornecedores.DaValorAtributos(Me.TextEditCodigoCliente.EditValue, "CDU_Oekotex", "CDU_Oekotex2", "CDU_Gots", "CDU_Ocs", "CDU_Bci", "CDU_EgyptCotton", "CDU_Supima", "CDU_SupimaData", "CDU_SupimaNum", "CDU_Grs", "CDU_Rcs", "CDU_Fsc", "CDU_EuropeanFlax", "CDU_Iso9001", "CDU_Iso14001", "CDU_Sa8000", "CDU_Fairtrade", "CDU_OekotexData", "CDU_OekotexData2", "CDU_GotsData", "CDU_OcsData", "CDU_BciData", "CDU_EgyptCottonData", "CDU_GrsData", "CDU_RcsData", "CDU_FscData", "CDU_EuropeanFlaxData", "CDU_Iso9001Data", "CDU_Iso14001Data", "CDU_Sa8000Data", "CDU_FairtradeData", "CDU_OekotexNum", "CDU_OekotexNum2", "CDU_FscNum", "CDU_EuropeanflaxNum", "CDU_OekotexClasse", "CDU_OekotexClasse2")
'                BSO.Base.Clientes.ActualizaValorAtributos(Cli.Valor("Cliente"), Campos)
'                MsgBox("Mundifios - Dados gravados com sucesso!" & Chr(13) & Chr(13) & "Filopa - Dados gravados com sucesso!", vbInformation)
'            Else
'                MsgBox("Mundifios - Dados gravados com sucesso!" & Chr(13) & Chr(13) & "Filopa - Erro:Cliente inexistente(EntidadeInterna " & BSO.Base.Clientes.DaValorAtributo(Me.TextEditCodigoCliente.EditValue, "CDU_EntidadeInterna") & ")", vbInformation)
'            End If
'            FechaObjEmpresa
'        End If
'        Exit Function

'TrataErro:
'        MsgBox("Mundifios - Dados gravados com sucesso!" & Chr(13) & Chr(13) & "Filopa - Erro:" & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)


'    End Function

'    Private Sub TextEditCliente_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditCliente.EditValueChanged

'        CarregaDados()

'    End Sub

'    Private Sub TextEditCodigoCliente_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditCodigoCliente.EditValueChanged

'        Me.TextEditCliente.EditValue = BSO.Base.Fornecedores.DaValorAtributo(Me.TextEditCodigoCliente.EditValue.Text, "Nome")

'    End Sub

'    Private Sub TextEditCodigoCliente_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles TextEditCodigoCliente.KeyDown

'        If e.KeyCode = Windows.Forms.Keys.F4 Then

'            'Me.TxtCodigoCliente.Text = Aplicacao.PlataformaPRIMAVERA.Listas.GetF4SQL("Cliente " & Filopa, "Select Cliente, Nome, Fac_Mor as 'Morada', Fac_Local as 'Localidade', Fac_Cp as 'Cod Postal', Fac_Cploc from Clientes", "Cliente")

'            'Aplicacao.PlataformaPRIMAVERA.AbreLista 0, "Cliente", "Cliente", Me, Me.TxtCodigoCliente.Text, "mnuTabArtigos", , , , , , blnModal:=True
'            PSO.AbreLista(0, "Fornecedores", "Fornecedor", Me, Me.TextEditCodigoCliente.EditValue, "mnuTabFornecedor", , , , , , True)

'        End If

'    End Sub

'    Public Sub New()

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.


'        Me.TextEditCodigoCliente.EditValue = Module1.certEntidade



'        ListaClasse = BSO.Consulta("SELECT CDU_Classe FROM PRIMundifios.dbo.TDU_ClassesCertificadoOKOTEX")

'        ListaClasse.Inicio()

'        For i = 1 To ListaClasse.NumLinhas
'            Dim dt As DataTable
'            dt.Rows.Add(ListaClasse.Valor("CDU_Classe"))
'            Me.LookUpEditClasse.Properties.DataSource = dt
'            Me.LookUpEditClasse2.Properties.DataSource = dt
'            ListaClasse.Seguinte()
'        Next i





'    End Sub

'End Class