'Imports System
'Imports System.Collections.Generic
'Imports System.ComponentModel
'Imports System.Linq
'Imports System.Text
'Imports System.Threading.Tasks
'Imports Primavera.Extensibility.BusinessEntities
'Imports Primavera.Extensibility.CustomForm
'Imports StdBE100
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

'Public Class frmalteracertificadotransacao2view
'    Inherits CustomForm

'    Friend WithEvents barmanager1 As DevExpress.XtraBars.BarManager
'    Private components As ComponentModel.IContainer
'    Friend WithEvents bar1 As DevExpress.XtraBars.Bar
'    Friend WithEvents barbuttonitemaplicar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents barbuttonitemclear As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents barbuttonitemfechar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents bardockcontroltop As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents bardockcontrolbottom As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents bardockcontrolleft As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents bardockcontrolright As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents label4 As Windows.Forms.Label
'    Friend WithEvents label3 As Windows.Forms.Label
'    Friend WithEvents label2 As Windows.Forms.Label
'    Friend WithEvents texteditqtd3 As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents label1 As Windows.Forms.Label
'    Friend WithEvents texteditdescricao As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents texteditobs As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents texteditartigo As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents texteditqtd1 As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents texteditqtd2 As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents texteditlote As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents texteditdocumento As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents checkeditbciemitido As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents checkeditcertificadoemitido As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents checkeditemitircertificado As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents lookupedittrans2 As DevExpress.XtraEditors.LookUpEdit
'    Friend WithEvents lookupedittrans3 As DevExpress.XtraEditors.LookUpEdit
'    Friend WithEvents lookupedittrans1 As DevExpress.XtraEditors.LookUpEdit
'    Friend WithEvents label5 As Windows.Forms.Label
'    Friend WithEvents label6 As Windows.Forms.Label
'    Friend WithEvents label7 As Windows.Forms.Label
'    Friend WithEvents barbuttonitematualizar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents barcheckitem1 As DevExpress.XtraBars.BarCheckItem

'    Private Sub initializecomponent()
'        Me.components = New System.ComponentModel.Container()
'        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmalteracertificadotransacao2view))
'        Me.barmanager1 = New DevExpress.XtraBars.BarManager(Me.components)
'        Me.bar1 = New DevExpress.XtraBars.Bar()
'        Me.barbuttonitemaplicar = New DevExpress.XtraBars.BarButtonItem()
'        Me.barbuttonitemclear = New DevExpress.XtraBars.BarButtonItem()
'        Me.barbuttonitemfechar = New DevExpress.XtraBars.BarButtonItem()
'        Me.barbuttonitematualizar = New DevExpress.XtraBars.BarButtonItem()
'        Me.bardockcontroltop = New DevExpress.XtraBars.BarDockControl()
'        Me.bardockcontrolbottom = New DevExpress.XtraBars.BarDockControl()
'        Me.bardockcontrolleft = New DevExpress.XtraBars.BarDockControl()
'        Me.bardockcontrolright = New DevExpress.XtraBars.BarDockControl()
'        Me.barcheckitem1 = New DevExpress.XtraBars.BarCheckItem()
'        Me.checkeditemitircertificado = New DevExpress.XtraEditors.CheckEdit()
'        Me.checkeditcertificadoemitido = New DevExpress.XtraEditors.CheckEdit()
'        Me.checkeditbciemitido = New DevExpress.XtraEditors.CheckEdit()
'        Me.texteditdocumento = New DevExpress.XtraEditors.TextEdit()
'        Me.texteditlote = New DevExpress.XtraEditors.TextEdit()
'        Me.texteditqtd2 = New DevExpress.XtraEditors.TextEdit()
'        Me.texteditqtd1 = New DevExpress.XtraEditors.TextEdit()
'        Me.texteditartigo = New DevExpress.XtraEditors.TextEdit()
'        Me.texteditobs = New DevExpress.XtraEditors.TextEdit()
'        Me.texteditdescricao = New DevExpress.XtraEditors.TextEdit()
'        Me.label1 = New System.Windows.Forms.Label()
'        Me.texteditqtd3 = New DevExpress.XtraEditors.TextEdit()
'        Me.label2 = New System.Windows.Forms.Label()
'        Me.label3 = New System.Windows.Forms.Label()
'        Me.label4 = New System.Windows.Forms.Label()
'        Me.label5 = New System.Windows.Forms.Label()
'        Me.label6 = New System.Windows.Forms.Label()
'        Me.label7 = New System.Windows.Forms.Label()
'        Me.lookupedittrans1 = New DevExpress.XtraEditors.LookUpEdit()
'        Me.lookupedittrans3 = New DevExpress.XtraEditors.LookUpEdit()
'        Me.lookupedittrans2 = New DevExpress.XtraEditors.LookUpEdit()
'        CType(Me.barmanager1, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.checkeditemitircertificado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.checkeditcertificadoemitido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.checkeditbciemitido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.texteditdocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.texteditlote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.texteditqtd2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.texteditqtd1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.texteditartigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.texteditobs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.texteditdescricao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.texteditqtd3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.lookupedittrans1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.lookupedittrans3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.lookupedittrans2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.SuspendLayout()
'        '
'        'barmanager1
'        '
'        Me.barmanager1.AllowMoveBarOnToolbar = False
'        Me.barmanager1.AllowQuickCustomization = False
'        Me.barmanager1.AllowShowToolbarsPopup = False
'        Me.barmanager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.bar1})
'        Me.barmanager1.DockControls.Add(Me.bardockcontroltop)
'        Me.barmanager1.DockControls.Add(Me.bardockcontrolbottom)
'        Me.barmanager1.DockControls.Add(Me.bardockcontrolleft)
'        Me.barmanager1.DockControls.Add(Me.bardockcontrolright)
'        Me.barmanager1.Form = Me
'        Me.barmanager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barcheckitem1, Me.barbuttonitemaplicar, Me.barbuttonitemclear, Me.barbuttonitemfechar, Me.barbuttonitematualizar})
'        Me.barmanager1.MaxItemId = 5
'        '
'        'bar1
'        '
'        Me.bar1.BarName = "ações"
'        Me.bar1.DockCol = 0
'        Me.bar1.DockRow = 0
'        Me.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
'        Me.bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbuttonitemaplicar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbuttonitemclear, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbuttonitemfechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbuttonitematualizar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
'        Me.bar1.OptionsBar.DrawDragBorder = False
'        Me.bar1.OptionsBar.UseWholeRow = True
'        Me.bar1.Text = "ações"
'        '
'        'barbuttonitemaplicar
'        '
'        Me.barbuttonitemaplicar.Caption = "aplicar"
'        Me.barbuttonitemaplicar.Id = 1
'        Me.barbuttonitemaplicar.ImageOptions.Image = CType(resources.GetObject("barbuttonitemaplicar.imageoptions.image"), System.Drawing.Image)
'        Me.barbuttonitemaplicar.Name = "barbuttonitemaplicar"
'        '
'        'barbuttonitemclear
'        '
'        Me.barbuttonitemclear.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
'        Me.barbuttonitemclear.Caption = "clear"
'        Me.barbuttonitemclear.Id = 2
'        Me.barbuttonitemclear.ImageOptions.Image = CType(resources.GetObject("barbuttonitemclear.imageoptions.image"), System.Drawing.Image)
'        Me.barbuttonitemclear.Name = "barbuttonitemclear"
'        '
'        'barbuttonitemfechar
'        '
'        Me.barbuttonitemfechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
'        Me.barbuttonitemfechar.Caption = "fechar"
'        Me.barbuttonitemfechar.Id = 3
'        Me.barbuttonitemfechar.ImageOptions.Image = CType(resources.GetObject("barbuttonitem3.imageoptions.image"), System.Drawing.Image)
'        Me.barbuttonitemfechar.Name = "barbuttonitemfechar"
'        '
'        'barbuttonitematualizar
'        '
'        Me.barbuttonitematualizar.Caption = "atualizar lista"
'        Me.barbuttonitematualizar.Id = 4
'        Me.barbuttonitematualizar.ImageOptions.Image = CType(resources.GetObject("barbuttonitematualizar.imageoptions.image"), System.Drawing.Image)
'        Me.barbuttonitematualizar.Name = "barbuttonitematualizar"
'        '
'        'bardockcontroltop
'        '
'        Me.bardockcontroltop.CausesValidation = False
'        Me.bardockcontroltop.Dock = System.Windows.Forms.DockStyle.Top
'        Me.bardockcontroltop.Location = New System.Drawing.Point(0, 0)
'        Me.bardockcontroltop.Manager = Me.barmanager1
'        Me.bardockcontroltop.Size = New System.Drawing.Size(571, 31)
'        '
'        'bardockcontrolbottom
'        '
'        Me.bardockcontrolbottom.CausesValidation = False
'        Me.bardockcontrolbottom.Dock = System.Windows.Forms.DockStyle.Bottom
'        Me.bardockcontrolbottom.Location = New System.Drawing.Point(0, 303)
'        Me.bardockcontrolbottom.Manager = Me.barmanager1
'        Me.bardockcontrolbottom.Size = New System.Drawing.Size(571, 0)
'        '
'        'bardockcontrolleft
'        '
'        Me.bardockcontrolleft.CausesValidation = False
'        Me.bardockcontrolleft.Dock = System.Windows.Forms.DockStyle.Left
'        Me.bardockcontrolleft.Location = New System.Drawing.Point(0, 31)
'        Me.bardockcontrolleft.Manager = Me.barmanager1
'        Me.bardockcontrolleft.Size = New System.Drawing.Size(0, 272)
'        '
'        'bardockcontrolright
'        '
'        Me.bardockcontrolright.CausesValidation = False
'        Me.bardockcontrolright.Dock = System.Windows.Forms.DockStyle.Right
'        Me.bardockcontrolright.Location = New System.Drawing.Point(571, 31)
'        Me.bardockcontrolright.Manager = Me.barmanager1
'        Me.bardockcontrolright.Size = New System.Drawing.Size(0, 272)
'        '
'        'barcheckitem1
'        '
'        Me.barcheckitem1.Caption = "barcheckitem1"
'        Me.barcheckitem1.Id = 0
'        Me.barcheckitem1.Name = "barcheckitem1"
'        '
'        'checkeditemitircertificado
'        '
'        Me.checkeditemitircertificado.Location = New System.Drawing.Point(100, 37)
'        Me.checkeditemitircertificado.MenuManager = Me.barmanager1
'        Me.checkeditemitircertificado.Name = "checkeditemitircertificado"
'        Me.checkeditemitircertificado.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.checkeditemitircertificado.Properties.Appearance.Options.UseFont = True
'        Me.checkeditemitircertificado.Properties.Caption = "emitir cert."
'        Me.checkeditemitircertificado.Size = New System.Drawing.Size(103, 20)
'        Me.checkeditemitircertificado.TabIndex = 4
'        '
'        'checkeditcertificadoemitido
'        '
'        Me.checkeditcertificadoemitido.Location = New System.Drawing.Point(341, 37)
'        Me.checkeditcertificadoemitido.MenuManager = Me.barmanager1
'        Me.checkeditcertificadoemitido.Name = "checkeditcertificadoemitido"
'        Me.checkeditcertificadoemitido.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.checkeditcertificadoemitido.Properties.Appearance.Options.UseFont = True
'        Me.checkeditcertificadoemitido.Properties.Caption = "cert.emitido"
'        Me.checkeditcertificadoemitido.Size = New System.Drawing.Size(117, 20)
'        Me.checkeditcertificadoemitido.TabIndex = 5
'        '
'        'checkeditbciemitido
'        '
'        Me.checkeditbciemitido.Location = New System.Drawing.Point(218, 37)
'        Me.checkeditbciemitido.MenuManager = Me.barmanager1
'        Me.checkeditbciemitido.Name = "checkeditbciemitido"
'        Me.checkeditbciemitido.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.checkeditbciemitido.Properties.Appearance.Options.UseFont = True
'        Me.checkeditbciemitido.Properties.Caption = "bci emitido"
'        Me.checkeditbciemitido.Size = New System.Drawing.Size(117, 20)
'        Me.checkeditbciemitido.TabIndex = 6
'        '
'        'texteditdocumento
'        '
'        Me.texteditdocumento.Location = New System.Drawing.Point(12, 74)
'        Me.texteditdocumento.MenuManager = Me.barmanager1
'        Me.texteditdocumento.Name = "texteditdocumento"
'        Me.texteditdocumento.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.texteditdocumento.Properties.Appearance.Options.UseFont = True
'        Me.texteditdocumento.Size = New System.Drawing.Size(245, 22)
'        Me.texteditdocumento.TabIndex = 7
'        '
'        'texteditlote
'        '
'        Me.texteditlote.Location = New System.Drawing.Point(180, 122)
'        Me.texteditlote.MenuManager = Me.barmanager1
'        Me.texteditlote.Name = "texteditlote"
'        Me.texteditlote.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.texteditlote.Properties.Appearance.Options.UseFont = True
'        Me.texteditlote.Size = New System.Drawing.Size(77, 22)
'        Me.texteditlote.TabIndex = 8
'        '
'        'texteditqtd2
'        '
'        Me.texteditqtd2.Location = New System.Drawing.Point(360, 208)
'        Me.texteditqtd2.MenuManager = Me.barmanager1
'        Me.texteditqtd2.Name = "texteditqtd2"
'        Me.texteditqtd2.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.texteditqtd2.Properties.Appearance.Options.UseFont = True
'        Me.texteditqtd2.Size = New System.Drawing.Size(162, 22)
'        Me.texteditqtd2.TabIndex = 9
'        '
'        'texteditqtd1
'        '
'        Me.texteditqtd1.Location = New System.Drawing.Point(360, 170)
'        Me.texteditqtd1.MenuManager = Me.barmanager1
'        Me.texteditqtd1.Name = "texteditqtd1"
'        Me.texteditqtd1.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.texteditqtd1.Properties.Appearance.Options.UseFont = True
'        Me.texteditqtd1.Size = New System.Drawing.Size(162, 22)
'        Me.texteditqtd1.TabIndex = 10
'        '
'        'texteditartigo
'        '
'        Me.texteditartigo.Location = New System.Drawing.Point(12, 122)
'        Me.texteditartigo.MenuManager = Me.barmanager1
'        Me.texteditartigo.Name = "texteditartigo"
'        Me.texteditartigo.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.texteditartigo.Properties.Appearance.Options.UseFont = True
'        Me.texteditartigo.Size = New System.Drawing.Size(162, 22)
'        Me.texteditartigo.TabIndex = 11
'        '
'        'texteditobs
'        '
'        Me.texteditobs.Location = New System.Drawing.Point(310, 74)
'        Me.texteditobs.MenuManager = Me.barmanager1
'        Me.texteditobs.Name = "texteditobs"
'        Me.texteditobs.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.texteditobs.Properties.Appearance.Options.UseFont = True
'        Me.texteditobs.Size = New System.Drawing.Size(245, 22)
'        Me.texteditobs.TabIndex = 12
'        '
'        'texteditdescricao
'        '
'        Me.texteditdescricao.Location = New System.Drawing.Point(263, 122)
'        Me.texteditdescricao.MenuManager = Me.barmanager1
'        Me.texteditdescricao.Name = "texteditdescricao"
'        Me.texteditdescricao.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.texteditdescricao.Properties.Appearance.Options.UseFont = True
'        Me.texteditdescricao.Size = New System.Drawing.Size(292, 22)
'        Me.texteditdescricao.TabIndex = 13
'        '
'        'label1
'        '
'        Me.label1.AutoSize = True
'        Me.label1.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.label1.Location = New System.Drawing.Point(270, 77)
'        Me.label1.Name = "label1"
'        Me.label1.Size = New System.Drawing.Size(34, 16)
'        Me.label1.TabIndex = 14
'        Me.label1.Text = "obs."
'        '
'        'texteditqtd3
'        '
'        Me.texteditqtd3.Location = New System.Drawing.Point(360, 245)
'        Me.texteditqtd3.MenuManager = Me.barmanager1
'        Me.texteditqtd3.Name = "texteditqtd3"
'        Me.texteditqtd3.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.texteditqtd3.Properties.Appearance.Options.UseFont = True
'        Me.texteditqtd3.Size = New System.Drawing.Size(162, 22)
'        Me.texteditqtd3.TabIndex = 15
'        '
'        'label2
'        '
'        Me.label2.AutoSize = True
'        Me.label2.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.label2.Location = New System.Drawing.Point(319, 248)
'        Me.label2.Name = "label2"
'        Me.label2.Size = New System.Drawing.Size(35, 16)
'        Me.label2.TabIndex = 16
'        Me.label2.Text = "qtd3"
'        '
'        'label3
'        '
'        Me.label3.AutoSize = True
'        Me.label3.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.label3.Location = New System.Drawing.Point(319, 211)
'        Me.label3.Name = "label3"
'        Me.label3.Size = New System.Drawing.Size(35, 16)
'        Me.label3.TabIndex = 17
'        Me.label3.Text = "qtd2"
'        '
'        'label4
'        '
'        Me.label4.AutoSize = True
'        Me.label4.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.label4.Location = New System.Drawing.Point(319, 173)
'        Me.label4.Name = "label4"
'        Me.label4.Size = New System.Drawing.Size(35, 16)
'        Me.label4.TabIndex = 18
'        Me.label4.Text = "qtd1"
'        '
'        'label5
'        '
'        Me.label5.AutoSize = True
'        Me.label5.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.label5.Location = New System.Drawing.Point(9, 176)
'        Me.label5.Name = "label5"
'        Me.label5.Size = New System.Drawing.Size(39, 16)
'        Me.label5.TabIndex = 21
'        Me.label5.Text = "cert1"
'        '
'        'label6
'        '
'        Me.label6.AutoSize = True
'        Me.label6.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.label6.Location = New System.Drawing.Point(9, 211)
'        Me.label6.Name = "label6"
'        Me.label6.Size = New System.Drawing.Size(39, 16)
'        Me.label6.TabIndex = 20
'        Me.label6.Text = "cert2"
'        '
'        'label7
'        '
'        Me.label7.AutoSize = True
'        Me.label7.Font = New System.Drawing.Font("tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.label7.Location = New System.Drawing.Point(9, 248)
'        Me.label7.Name = "label7"
'        Me.label7.Size = New System.Drawing.Size(39, 16)
'        Me.label7.TabIndex = 19
'        Me.label7.Text = "cert3"
'        '
'        'lookupedittrans1
'        '
'        Me.lookupedittrans1.Location = New System.Drawing.Point(54, 171)
'        Me.lookupedittrans1.MenuManager = Me.barmanager1
'        Me.lookupedittrans1.Name = "lookupedittrans1"
'        Me.lookupedittrans1.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.lookupedittrans1.Properties.Appearance.Options.UseFont = True
'        Me.lookupedittrans1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.lookupedittrans1.Size = New System.Drawing.Size(250, 20)
'        Me.lookupedittrans1.TabIndex = 22
'        '
'        'lookupedittrans3
'        '
'        Me.lookupedittrans3.Location = New System.Drawing.Point(54, 246)
'        Me.lookupedittrans3.MenuManager = Me.barmanager1
'        Me.lookupedittrans3.Name = "lookupedittrans3"
'        Me.lookupedittrans3.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.lookupedittrans3.Properties.Appearance.Options.UseFont = True
'        Me.lookupedittrans3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.lookupedittrans3.Size = New System.Drawing.Size(250, 20)
'        Me.lookupedittrans3.TabIndex = 23
'        '
'        'lookupedittrans2
'        '
'        Me.lookupedittrans2.Location = New System.Drawing.Point(54, 209)
'        Me.lookupedittrans2.MenuManager = Me.barmanager1
'        Me.lookupedittrans2.Name = "lookupedittrans2"
'        Me.lookupedittrans2.Properties.Appearance.Font = New System.Drawing.Font("tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.lookupedittrans2.Properties.Appearance.Options.UseFont = True
'        Me.lookupedittrans2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.lookupedittrans2.Size = New System.Drawing.Size(250, 20)
'        Me.lookupedittrans2.TabIndex = 24
'        '
'        'frmalteracertificadotransacao2view
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
'        Me.Controls.Add(Me.lookupedittrans2)
'        Me.Controls.Add(Me.lookupedittrans3)
'        Me.Controls.Add(Me.lookupedittrans1)
'        Me.Controls.Add(Me.label5)
'        Me.Controls.Add(Me.label6)
'        Me.Controls.Add(Me.label7)
'        Me.Controls.Add(Me.label4)
'        Me.Controls.Add(Me.label3)
'        Me.Controls.Add(Me.label2)
'        Me.Controls.Add(Me.texteditqtd3)
'        Me.Controls.Add(Me.label1)
'        Me.Controls.Add(Me.texteditdescricao)
'        Me.Controls.Add(Me.texteditobs)
'        Me.Controls.Add(Me.texteditartigo)
'        Me.Controls.Add(Me.texteditqtd1)
'        Me.Controls.Add(Me.texteditqtd2)
'        Me.Controls.Add(Me.texteditlote)
'        Me.Controls.Add(Me.texteditdocumento)
'        Me.Controls.Add(Me.checkeditbciemitido)
'        Me.Controls.Add(Me.checkeditcertificadoemitido)
'        Me.Controls.Add(Me.checkeditemitircertificado)
'        Me.Controls.Add(Me.bardockcontrolleft)
'        Me.Controls.Add(Me.bardockcontrolright)
'        Me.Controls.Add(Me.bardockcontrolbottom)
'        Me.Controls.Add(Me.bardockcontroltop)
'        Me.Name = "frmalteracertificadotransacao2view"
'        Me.Size = New System.Drawing.Size(571, 303)
'        Me.Text = "altera certificado transacao 2"
'        CType(Me.barmanager1, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.checkeditemitircertificado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.checkeditcertificadoemitido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.checkeditbciemitido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.texteditdocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.texteditlote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.texteditqtd2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.texteditqtd1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.texteditartigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.texteditobs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.texteditdescricao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.texteditqtd3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.lookupedittrans1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.lookupedittrans3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.lookupedittrans2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.ResumeLayout(False)
'        Me.PerformLayout()

'    End Sub

'    Dim listacert As StdBELista
'    Dim sqlcert As String

'    Private Sub barbuttonitemaplicar_itemclick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbuttonitemaplicar.ItemClick


'        BSO.DSO.ExecuteSQL("update linhasdoc set cdu_certificadoemitido='" & checkeditcertificadoemitido.EditValue & "', cdu_bciemitido='" & checkeditbciemitido.EditValue & "', cdu_emitircertificado='" & checkeditemitircertificado.EditValue & "', cdu_numcertificadotrans='" & lookupedittrans1.EditValue & "', cdu_numcertificadotrans2='" & lookupedittrans2.EditValue & "', cdu_numcertificadotrans3='" & lookupedittrans3.EditValue & "', cdu_qtdcertificadotrans='" & Replace(texteditqtd1.EditValue, ",", ".") & "', cdu_qtdcertificadotrans2='" & Replace(texteditqtd2.EditValue, ",", ".") & "', cdu_qtdcertificadotrans3='" & Replace(texteditqtd3.EditValue, ",", ".") & "', cdu_obscertificadotrans='" & (texteditobs.EditValue) & "' where id='" & certidlinha & "'")


'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_emitircertificado") = checkeditemitircertificado.EditValue
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_certificadoemitido") = checkeditcertificadoemitido.EditValue
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_bciemitido") = checkeditbciemitido.EditValue
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_numcertificadotrans") = lookupedittrans1.EditValue
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_numcertificadotrans2") = lookupedittrans2.EditValue
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_numcertificadotrans3") = lookupedittrans3.EditValue
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_qtdcertificadotrans") = Replace(texteditqtd1.EditValue, ".", ",")
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_qtdcertificadotrans2") = Replace(texteditqtd2.EditValue, ".", ",")
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_qtdcertificadotrans3") = Replace(texteditqtd3.EditValue, ".", ",")
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_obscertificadotrans") = texteditobs.EditValue


'        Me.DialogResult = DialogResult.OK
'        Me.Close()

'    End Sub

'    Private Sub barbuttonitematualizar_itemclick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbuttonitematualizar.ItemClick

'        BSO.DSO.ExecuteSQL("exec [dbo].[spinserircert]")

'    End Sub

'    Private Sub barbuttonitemclear_itemclick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbuttonitemclear.ItemClick

'        BSO.DSO.ExecuteSQL("update linhasdoc set cdu_certificadoemitido='', cdu_emitircertificado='', cdu_bciemitido='', cdu_numcertificadotrans='', cdu_numcertificadotrans2='', cdu_numcertificadotrans3='', cdu_qtdcertificadotrans='0', cdu_qtdcertificadotrans2='0', cdu_qtdcertificadotrans3='0', cdu_obscertificadotrans='' where id='" & certidlinha & "'")

'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_numcertificadotrans") = ""
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_numcertificadotrans2") = ""
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_numcertificadotrans3") = ""
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_qtdcertificadotrans") = "0"
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_qtdcertificadotrans2") = "0"
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_qtdcertificadotrans3") = "0"
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_certificadoemitido") = ""
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_emitircertificado") = ""
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_bciemitido") = ""
'        editorvendas.documentovenda.linhas(editorvendas.linhaactual).camposutil("cdu_obscertificadotrans") = ""

'        checkeditemitircertificado.EditValue = False
'        checkeditcertificadoemitido.EditValue = False
'        checkeditbciemitido.EditValue = False
'        lookupedittrans1.EditValue = ""
'        lookupedittrans2.EditValue = ""
'        lookupedittrans3.EditValue = ""
'        texteditqtd1.EditValue = 0
'        texteditqtd2.EditValue = 0
'        texteditqtd3.EditValue = 0
'        texteditobs.EditValue = ""

'    End Sub

'    Private Sub texteditqtd1_editvaluechanged(sender As Object, e As EventArgs) Handles texteditqtd1.EditValueChanged

'        If Not IsNumeric(texteditqtd1.EditValue) Then
'            MsgBox("only numbers allowed")
'            texteditqtd1.EditValue = "0"
'        End If

'    End Sub

'    Private Sub texteditqtd2_editvaluechanged(sender As Object, e As EventArgs) Handles texteditqtd2.EditValueChanged

'        If Not IsNumeric(texteditqtd2.EditValue) Then
'            MsgBox("only numbers allowed")
'            texteditqtd2.EditValue = "0"
'        End If

'    End Sub

'    Private Sub texteditqtd3_editvaluechanged(sender As Object, e As EventArgs) Handles texteditqtd3.EditValueChanged

'        If Not IsNumeric(texteditqtd3.EditValue) Then
'            MsgBox("only numbers allowed")
'            texteditqtd3.EditValue = "0"
'        End If

'    End Sub

'    Public Sub New()

'        ' this call is required by the designer.
'        initializecomponent()

'        ' add any initialization after the initializecomponent() call.

'        texteditartigo.EditValue = Module1.certArtigo
'        texteditlote.EditValue = Module1.certLote
'        lookupedittrans1.EditValue = Module1.certCertificadoTransacao
'        lookupedittrans2.EditValue = Module1.certCertificadoTransacao2
'        lookupedittrans3.EditValue = Module1.certCertificadoTransacao3
'        texteditqtd1.EditValue = Module1.certQtdTransacao
'        texteditqtd2.EditValue = Module1.certQtdTransacao2
'        texteditqtd3.EditValue = Module1.certQtdTransacao3
'        texteditdocumento.EditValue = Module1.certDocumento
'        txtidlinha = Module1.certIDlinha
'        checkeditcertificadoemitido.EditValue = Module1.certEmitido
'        checkeditemitircertificado.EditValue = Module1.certEmitir
'        texteditdescricao.EditValue = Module1.certDescricao
'        texteditobs.EditValue = Module1.certObs
'        checkeditbciemitido.EditValue = Module1.certBCIEmitido

'        If UCase(BSO.Inventario.ArtigosLotes.Edita(texteditartigo.EditValue, texteditlote.EditValue).Observacoes) Like "*bci*" Or Module1.certDescricao Like "*bci*" Then
'            checkeditbciemitido.Enabled = True
'        Else
'            checkeditbciemitido.Enabled = False
'        End If

'        carregacombos()


'    End Sub

'    Private Function carregacombos()

'        listacert = BSO.Consulta("select [cert] from [tdu_saldocertificadostrans] where [disp] > 0 order by artigo desc, lote desc")

'        If listacert.Vazia = False Then

'            listacert.Inicio()

'            For k = 1 To listacert.NumLinhas

'                Dim dt As New DataTable

'                dt.Rows.Add(listacert.Valor("cert"))

'                lookupedittrans1.Properties.DataSource = dt
'                lookupedittrans2.Properties.DataSource = dt
'                lookupedittrans3.Properties.DataSource = dt


'                listacert.Seguinte()

'            Next k

'        End If
'    End Function

'    Private Sub barbuttonitemfechar_itemclick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbuttonitemfechar.ItemClick

'        Me.DialogResult = DialogResult.Cancel
'        Me.Close()

'    End Sub
'End Class