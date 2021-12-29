Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.CustomForm
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Public Class FrmAlteraCertificadoTransacaoFilopaView
    Inherits CustomForm

    'Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    'Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    'Friend WithEvents BarButtonItemAplicar As DevExpress.XtraBars.BarButtonItem
    'Friend WithEvents BarButtonItemFechar As DevExpress.XtraBars.BarButtonItem
    'Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    'Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    'Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    'Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    'Friend WithEvents BarButtonItemClear As DevExpress.XtraBars.BarButtonItem
    'Friend WithEvents Label1 As Windows.Forms.Label
    'Friend WithEvents CheckEditCertificadoEmitido As DevExpress.XtraEditors.CheckEdit
    'Friend WithEvents TextEditCancelado As DevExpress.XtraEditors.TextEdit
    'Friend WithEvents TextEditArtigo As DevExpress.XtraEditors.TextEdit
    'Friend WithEvents TextEditDescricao As DevExpress.XtraEditors.TextEdit
    'Friend WithEvents TextEditDocumento As DevExpress.XtraEditors.TextEdit
    'Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem

    'Private Sub InitializeComponent()
    '    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAlteraCertificadoTransacaoFilopaView))
    '    Me.BarManager1 = New DevExpress.XtraBars.BarManager()
    '    Me.Bar1 = New DevExpress.XtraBars.Bar()
    '    Me.BarButtonItemAplicar = New DevExpress.XtraBars.BarButtonItem()
    '    Me.BarButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
    '    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
    '    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
    '    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
    '    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
    '    Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
    '    Me.BarButtonItemClear = New DevExpress.XtraBars.BarButtonItem()
    '    Me.TextEditDocumento = New DevExpress.XtraEditors.TextEdit()
    '    Me.TextEditDescricao = New DevExpress.XtraEditors.TextEdit()
    '    Me.TextEditArtigo = New DevExpress.XtraEditors.TextEdit()
    '    Me.TextEditCancelado = New DevExpress.XtraEditors.TextEdit()
    '    Me.CheckEditCertificadoEmitido = New DevExpress.XtraEditors.CheckEdit()
    '    Me.Label1 = New System.Windows.Forms.Label()
    '    CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    '    CType(Me.TextEditDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    '    CType(Me.TextEditDescricao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    '    CType(Me.TextEditArtigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    '    CType(Me.TextEditCancelado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    '    CType(Me.CheckEditCertificadoEmitido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    '    Me.SuspendLayout()
    '    '
    '    'BarManager1
    '    '
    '    Me.BarManager1.AllowMoveBarOnToolbar = False
    '    Me.BarManager1.AllowQuickCustomization = False
    '    Me.BarManager1.AllowShowToolbarsPopup = False
    '    Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
    '    Me.BarManager1.DockControls.Add(Me.barDockControlTop)
    '    Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
    '    Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
    '    Me.BarManager1.DockControls.Add(Me.barDockControlRight)
    '    Me.BarManager1.Form = Me
    '    Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItemAplicar, Me.BarButtonItemFechar, Me.BarButtonItem1, Me.BarButtonItemClear})
    '    Me.BarManager1.MaxItemId = 5
    '    '
    '    'Bar1
    '    '
    '    Me.Bar1.BarName = "ações"
    '    Me.Bar1.DockCol = 0
    '    Me.Bar1.DockRow = 0
    '    Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    '    Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemAplicar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemClear, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
    '    Me.Bar1.OptionsBar.DrawDragBorder = False
    '    Me.Bar1.OptionsBar.UseWholeRow = True
    '    Me.Bar1.Text = "ações"
    '    '
    '    'BarButtonItemAplicar
    '    '
    '    Me.BarButtonItemAplicar.Caption = "Aplicar"
    '    Me.BarButtonItemAplicar.Id = 0
    '    Me.BarButtonItemAplicar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemAplicar.ImageOptions.Image"), System.Drawing.Image)
    '    Me.BarButtonItemAplicar.Name = "BarButtonItemAplicar"
    '    '
    '    'BarButtonItemFechar
    '    '
    '    Me.BarButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
    '    Me.BarButtonItemFechar.Caption = "Fechar"
    '    Me.BarButtonItemFechar.Id = 1
    '    Me.BarButtonItemFechar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemFechar.ImageOptions.Image"), System.Drawing.Image)
    '    Me.BarButtonItemFechar.Name = "BarButtonItemFechar"
    '    '
    '    'barDockControlTop
    '    '
    '    Me.barDockControlTop.CausesValidation = False
    '    Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
    '    Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
    '    Me.barDockControlTop.Manager = Me.BarManager1
    '    Me.barDockControlTop.Size = New System.Drawing.Size(534, 31)
    '    '
    '    'barDockControlBottom
    '    '
    '    Me.barDockControlBottom.CausesValidation = False
    '    Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.barDockControlBottom.Location = New System.Drawing.Point(0, 167)
    '    Me.barDockControlBottom.Manager = Me.BarManager1
    '    Me.barDockControlBottom.Size = New System.Drawing.Size(534, 0)
    '    '
    '    'barDockControlLeft
    '    '
    '    Me.barDockControlLeft.CausesValidation = False
    '    Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
    '    Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
    '    Me.barDockControlLeft.Manager = Me.BarManager1
    '    Me.barDockControlLeft.Size = New System.Drawing.Size(0, 136)
    '    '
    '    'barDockControlRight
    '    '
    '    Me.barDockControlRight.CausesValidation = False
    '    Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
    '    Me.barDockControlRight.Location = New System.Drawing.Point(534, 31)
    '    Me.barDockControlRight.Manager = Me.BarManager1
    '    Me.barDockControlRight.Size = New System.Drawing.Size(0, 136)
    '    '
    '    'BarButtonItem1
    '    '
    '    Me.BarButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
    '    Me.BarButtonItem1.Caption = "Clear"
    '    Me.BarButtonItem1.Id = 3
    '    Me.BarButtonItem1.Name = "BarButtonItem1"
    '    '
    '    'BarButtonItemClear
    '    '
    '    Me.BarButtonItemClear.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
    '    Me.BarButtonItemClear.Caption = "Clear"
    '    Me.BarButtonItemClear.Id = 4
    '    Me.BarButtonItemClear.ImageOptions.Image = CType(resources.GetObject("BarButtonItemClear.ImageOptions.Image"), System.Drawing.Image)
    '    Me.BarButtonItemClear.Name = "BarButtonItemClear"
    '    '
    '    'TextEditDocumento
    '    '
    '    Me.TextEditDocumento.Location = New System.Drawing.Point(3, 52)
    '    Me.TextEditDocumento.MenuManager = Me.BarManager1
    '    Me.TextEditDocumento.Name = "TextEditDocumento"
    '    Me.TextEditDocumento.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.TextEditDocumento.Properties.Appearance.Options.UseFont = True
    '    Me.TextEditDocumento.Size = New System.Drawing.Size(244, 22)
    '    Me.TextEditDocumento.TabIndex = 4
    '    '
    '    'TextEditDescricao
    '    '
    '    Me.TextEditDescricao.Location = New System.Drawing.Point(147, 92)
    '    Me.TextEditDescricao.MenuManager = Me.BarManager1
    '    Me.TextEditDescricao.Name = "TextEditDescricao"
    '    Me.TextEditDescricao.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.TextEditDescricao.Properties.Appearance.Options.UseFont = True
    '    Me.TextEditDescricao.Size = New System.Drawing.Size(368, 22)
    '    Me.TextEditDescricao.TabIndex = 5
    '    '
    '    'TextEditArtigo
    '    '
    '    Me.TextEditArtigo.Location = New System.Drawing.Point(3, 92)
    '    Me.TextEditArtigo.MenuManager = Me.BarManager1
    '    Me.TextEditArtigo.Name = "TextEditArtigo"
    '    Me.TextEditArtigo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.TextEditArtigo.Properties.Appearance.Options.UseFont = True
    '    Me.TextEditArtigo.Size = New System.Drawing.Size(138, 22)
    '    Me.TextEditArtigo.TabIndex = 6
    '    '
    '    'TextEditCancelado
    '    '
    '    Me.TextEditCancelado.Location = New System.Drawing.Point(147, 133)
    '    Me.TextEditCancelado.MenuManager = Me.BarManager1
    '    Me.TextEditCancelado.Name = "TextEditCancelado"
    '    Me.TextEditCancelado.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.TextEditCancelado.Properties.Appearance.Options.UseFont = True
    '    Me.TextEditCancelado.Size = New System.Drawing.Size(368, 22)
    '    Me.TextEditCancelado.TabIndex = 7
    '    '
    '    'CheckEditCertificadoEmitido
    '    '
    '    Me.CheckEditCertificadoEmitido.Location = New System.Drawing.Point(253, 53)
    '    Me.CheckEditCertificadoEmitido.MenuManager = Me.BarManager1
    '    Me.CheckEditCertificadoEmitido.Name = "CheckEditCertificadoEmitido"
    '    Me.CheckEditCertificadoEmitido.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.CheckEditCertificadoEmitido.Properties.Appearance.Options.UseFont = True
    '    Me.CheckEditCertificadoEmitido.Properties.Caption = "Certificado"
    '    Me.CheckEditCertificadoEmitido.Size = New System.Drawing.Size(103, 20)
    '    Me.CheckEditCertificadoEmitido.TabIndex = 8
    '    '
    '    'Label1
    '    '
    '    Me.Label1.AutoSize = True
    '    Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.Label1.Location = New System.Drawing.Point(9, 136)
    '    Me.Label1.Name = "Label1"
    '    Me.Label1.Size = New System.Drawing.Size(132, 16)
    '    Me.Label1.TabIndex = 9
    '    Me.Label1.Text = "Certificado Cancelado"
    '    '
    '    'FrmAlteraCertificadoTransacao2
    '    '
    '    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    '    Me.Controls.Add(Me.Label1)
    '    Me.Controls.Add(Me.CheckEditCertificadoEmitido)
    '    Me.Controls.Add(Me.TextEditCancelado)
    '    Me.Controls.Add(Me.TextEditArtigo)
    '    Me.Controls.Add(Me.TextEditDescricao)
    '    Me.Controls.Add(Me.TextEditDocumento)
    '    Me.Controls.Add(Me.barDockControlLeft)
    '    Me.Controls.Add(Me.barDockControlRight)
    '    Me.Controls.Add(Me.barDockControlBottom)
    '    Me.Controls.Add(Me.barDockControlTop)
    '    Me.Name = "FrmAlteraCertificadoTransacao2"
    '    Me.Size = New System.Drawing.Size(534, 167)
    '    Me.Text = "Certificados de Transação"
    '    CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    '    CType(Me.TextEditDocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    '    CType(Me.TextEditDescricao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    '    CType(Me.TextEditArtigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    '    CType(Me.TextEditCancelado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    '    CType(Me.CheckEditCertificadoEmitido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    '    Me.ResumeLayout(False)
    '    Me.PerformLayout()

    'End Sub


    'Dim ListaCert As StdBELista
    'Dim SqlCert As String

    'Private Sub BarButtonItemClear_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemClear.ItemClick

    '    BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoRecebido='0' where Id='" & Module1.certIDlinha & "'")
    '    'Acrescentado dia 27/01/2021 - Bruno
    '    BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoCancelado=' ' where Id='" & Module1.certIDlinha & "'")


    '    EditorVendas.DocumentoVenda.Linhas(EditorVendas.LinhaActual).CamposUtil("CDU_CertificadoRecebido") = "0"
    '    'Acrescentado dia 27/01/2021 - Bruno
    '    EditorVendas.DocumentoVenda.Linhas(EditorVendas.LinhaActual).CamposUtil("CDU_CertificadoCancelado") = " "

    '    CheckEditCertificadoEmitido.EditValue = False


    'End Sub

    'Private Sub BarButtonItemAplicar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemAplicar.ItemClick

    '    BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoRecebido='" & CheckEditCertificadoEmitido.EditValue & "' where Id='" & Module1.certIDlinha & "'")
    '    'Acrescentado dia 27/01/2021 - Bruno
    '    BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoCancelado='" & TextEditCancelado.EditValue & "' where Id='" & Module1.certIDlinha & "'")



    '    EditorVendas.DocumentoVenda.Linhas(EditorVendas.LinhaActual).CamposUtil("CDU_CertificadoRecebido") = CheckEditCertificadoEmitido.EditValue
    '    'Acrescentado dia 27/01/2021 - Bruno
    '    EditorVendas.DocumentoVenda.Linhas(EditorVendas.LinhaActual).CamposUtil("CDU_CertificadoCancelado") = TextEditCancelado.EditValue


    '    Me.DialogResult = DialogResult.OK
    '    Me.Close()

    'End Sub

    'Public Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.
    '    TextEditArtigo.EditValue = Module1.certArtigo
    '    'txtLote = certLote
    '    TextEditDocumento.EditValue = Module1.certDocumento
    '    txtIDlinha = Module1.certIDlinha
    '    CheckEditCertificadoEmitido.EditValue = Module1.certEmitido
    '    TextEditDescricao.EditValue = Module1.certDescricao
    '    'Acrescentado dia 27/01/2021 - Bruno
    '    TextEditCancelado.EditValue = Module1.certCancelado


    'End Sub

End Class