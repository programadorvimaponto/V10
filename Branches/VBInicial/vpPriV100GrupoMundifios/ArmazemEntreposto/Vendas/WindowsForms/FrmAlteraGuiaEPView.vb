'Imports System
'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Text
'Imports System.Threading.Tasks
'Imports Primavera.Extensibility.BusinessEntities
'Imports Primavera.Extensibility.CustomForm
'Imports Primavera.Extensibility.Sales.Editors
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

'Public Class FrmAlteraGuiaEPView
'    Inherits CustomForm

'    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
'    Private components As ComponentModel.IContainer
'    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
'    Friend WithEvents BarButtonItemGravar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents BarButtonItemFechar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents Label2 As Windows.Forms.Label
'    Friend WithEvents Label1 As Windows.Forms.Label
'    Friend WithEvents TextEditRegime As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditDespDau As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditArtigo As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditLote As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditArmazem As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditDocumento As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl

'    Private Sub InitializeComponent()
'        Me.components = New System.ComponentModel.Container()
'        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAlteraGuiaEPView))
'        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
'        Me.Bar1 = New DevExpress.XtraBars.Bar()
'        Me.BarButtonItemGravar = New DevExpress.XtraBars.BarButtonItem()
'        Me.BarButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
'        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
'        Me.TextEditDocumento = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditArmazem = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditLote = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditArtigo = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditDespDau = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditRegime = New DevExpress.XtraEditors.TextEdit()
'        Me.Label1 = New System.Windows.Forms.Label()
'        Me.Label2 = New System.Windows.Forms.Label()
'        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditArmazem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditArtigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditDespDau.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditRegime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.SuspendLayout()
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
'        Me.barDockControlTop.Size = New System.Drawing.Size(420, 31)
'        '
'        'barDockControlBottom
'        '
'        Me.barDockControlBottom.CausesValidation = False
'        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
'        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 162)
'        Me.barDockControlBottom.Manager = Me.BarManager1
'        Me.barDockControlBottom.Size = New System.Drawing.Size(420, 0)
'        '
'        'barDockControlLeft
'        '
'        Me.barDockControlLeft.CausesValidation = False
'        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
'        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
'        Me.barDockControlLeft.Manager = Me.BarManager1
'        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 131)
'        '
'        'barDockControlRight
'        '
'        Me.barDockControlRight.CausesValidation = False
'        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
'        Me.barDockControlRight.Location = New System.Drawing.Point(420, 31)
'        Me.barDockControlRight.Manager = Me.BarManager1
'        Me.barDockControlRight.Size = New System.Drawing.Size(0, 131)
'        '
'        'TextEditDocumento
'        '
'        Me.TextEditDocumento.Location = New System.Drawing.Point(12, 46)
'        Me.TextEditDocumento.MenuManager = Me.BarManager1
'        Me.TextEditDocumento.Name = "TextEditDocumento"
'        Me.TextEditDocumento.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditDocumento.Properties.Appearance.Options.UseFont = True
'        Me.TextEditDocumento.Size = New System.Drawing.Size(149, 26)
'        Me.TextEditDocumento.TabIndex = 4
'        '
'        'TextEditArmazem
'        '
'        Me.TextEditArmazem.Location = New System.Drawing.Point(114, 129)
'        Me.TextEditArmazem.MenuManager = Me.BarManager1
'        Me.TextEditArmazem.Name = "TextEditArmazem"
'        Me.TextEditArmazem.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditArmazem.Properties.Appearance.Options.UseFont = True
'        Me.TextEditArmazem.Size = New System.Drawing.Size(47, 26)
'        Me.TextEditArmazem.TabIndex = 5
'        '
'        'TextEditLote
'        '
'        Me.TextEditLote.Location = New System.Drawing.Point(12, 129)
'        Me.TextEditLote.MenuManager = Me.BarManager1
'        Me.TextEditLote.Name = "TextEditLote"
'        Me.TextEditLote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditLote.Properties.Appearance.Options.UseFont = True
'        Me.TextEditLote.Size = New System.Drawing.Size(96, 26)
'        Me.TextEditLote.TabIndex = 6
'        '
'        'TextEditArtigo
'        '
'        Me.TextEditArtigo.Location = New System.Drawing.Point(12, 88)
'        Me.TextEditArtigo.MenuManager = Me.BarManager1
'        Me.TextEditArtigo.Name = "TextEditArtigo"
'        Me.TextEditArtigo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditArtigo.Properties.Appearance.Options.UseFont = True
'        Me.TextEditArtigo.Size = New System.Drawing.Size(149, 26)
'        Me.TextEditArtigo.TabIndex = 7
'        '
'        'TextEditDespDau
'        '
'        Me.TextEditDespDau.Location = New System.Drawing.Point(252, 46)
'        Me.TextEditDespDau.MenuManager = Me.BarManager1
'        Me.TextEditDespDau.Name = "TextEditDespDau"
'        Me.TextEditDespDau.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditDespDau.Properties.Appearance.Options.UseFont = True
'        Me.TextEditDespDau.Size = New System.Drawing.Size(149, 26)
'        Me.TextEditDespDau.TabIndex = 8
'        '
'        'TextEditRegime
'        '
'        Me.TextEditRegime.Location = New System.Drawing.Point(252, 88)
'        Me.TextEditRegime.MenuManager = Me.BarManager1
'        Me.TextEditRegime.Name = "TextEditRegime"
'        Me.TextEditRegime.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditRegime.Properties.Appearance.Options.UseFont = True
'        Me.TextEditRegime.Size = New System.Drawing.Size(149, 26)
'        Me.TextEditRegime.TabIndex = 9
'        '
'        'Label1
'        '
'        Me.Label1.AutoSize = True
'        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label1.Location = New System.Drawing.Point(186, 52)
'        Me.Label1.Name = "Label1"
'        Me.Label1.Size = New System.Drawing.Size(60, 16)
'        Me.Label1.TabIndex = 10
'        Me.Label1.Text = "DespDAU"
'        '
'        'Label2
'        '
'        Me.Label2.AutoSize = True
'        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label2.Location = New System.Drawing.Point(195, 94)
'        Me.Label2.Name = "Label2"
'        Me.Label2.Size = New System.Drawing.Size(51, 16)
'        Me.Label2.TabIndex = 11
'        Me.Label2.Text = "Regime"
'        '
'        'FrmAlteraGuiaEPView
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
'        Me.Controls.Add(Me.Label2)
'        Me.Controls.Add(Me.Label1)
'        Me.Controls.Add(Me.TextEditRegime)
'        Me.Controls.Add(Me.TextEditDespDau)
'        Me.Controls.Add(Me.TextEditArtigo)
'        Me.Controls.Add(Me.TextEditLote)
'        Me.Controls.Add(Me.TextEditArmazem)
'        Me.Controls.Add(Me.TextEditDocumento)
'        Me.Controls.Add(Me.barDockControlLeft)
'        Me.Controls.Add(Me.barDockControlRight)
'        Me.Controls.Add(Me.barDockControlBottom)
'        Me.Controls.Add(Me.barDockControlTop)
'        Me.Name = "FrmAlteraGuiaEPView"
'        Me.Size = New System.Drawing.Size(420, 162)
'        Me.Text = "Altera Guia EP"
'        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditDocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditArmazem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditArtigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditDespDau.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditRegime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.ResumeLayout(False)
'        Me.PerformLayout()

'    End Sub

'    Private Sub BarButtonItemGravar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemGravar.ItemClick

'        BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_Regime='" & TextEditRegime.EditValue & "', CDU_DespDAU='" & TextEditDespDau.EditValue & "' where Id='" & Module1.aepIDlinha & "'")
'        EditorVendas.DocumentoVenda.Linhas.GetEdita(EditorVendas.LinhaActual).CamposUtil("CDU_Regime").Valor = TextEditRegime.EditValue
'        EditorVendas.DocumentoVenda.Linhas.GetEdita(EditorVendas.LinhaActual).CamposUtil("CDU_DespDAU").Valor = TextEditDespDau.EditValue

'        Me.DialogResult = DialogResult.OK
'        Me.Close()

'    End Sub

'    Public Sub New()

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.

'        TextEditArtigo.EditValue = Module1.aepArtigo
'        TextEditArmazem.EditValue = Module1.aepArmazem
'        TextEditLote.EditValue = Module1.aepLote
'        TextEditDespDau.EditValue = Module1.aepDespDAU
'        TextEditRegime.EditValue = Module1.aepRegime
'        TextEditDocumento.EditValue = Module1.aepDocumento
'        txtIDlinha = Module1.aepIDlinha


'    End Sub

'    Private Sub BarButtonItemFechar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemFechar.ItemClick

'        Me.DialogResult = DialogResult.Cancel
'        Me.Close()

'    End Sub
'End Class