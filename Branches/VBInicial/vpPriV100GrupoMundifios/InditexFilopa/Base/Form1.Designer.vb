Imports Primavera.Extensibility.CustomForm

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.barManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.bar1 = New DevExpress.XtraBars.Bar()
        Me.barButtonItemGravar = New DevExpress.XtraBars.BarButtonItem()
        Me.barButtonItemCopiarInformacao = New DevExpress.XtraBars.BarButtonItem()
        Me.barButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.memoEditFiacoesObs = New DevExpress.XtraEditors.MemoEdit()
        Me.dateEditFiacoes = New DevExpress.XtraEditors.DateEdit()
        Me.lookUpEditAprovado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lookUpEditAuditado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lookUpEditClassificacao = New DevExpress.XtraEditors.LookUpEdit()
        Me.lookUpEditPreAuditado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lookUpEditTipoIdentificacao = New DevExpress.XtraEditors.LookUpEdit()
        Me.textEditNIdentificacao = New DevExpress.XtraEditors.TextEdit()
        Me.textEditCliente = New DevExpress.XtraEditors.TextEdit()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.TextEditFacLocal = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditFacMor = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditPais = New DevExpress.XtraEditors.TextEdit()
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.memoEditFiacoesObs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateEditFiacoes.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateEditFiacoes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditAprovado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditAuditado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditClassificacao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditPreAuditado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditTipoIdentificacao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditNIdentificacao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditFacLocal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditFacMor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditPais.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'barManager1
        '
        Me.barManager1.AllowMoveBarOnToolbar = False
        Me.barManager1.AllowQuickCustomization = False
        Me.barManager1.AllowShowToolbarsPopup = False
        Me.barManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.bar1})
        Me.barManager1.DockControls.Add(Me.barDockControlTop)
        Me.barManager1.DockControls.Add(Me.barDockControlBottom)
        Me.barManager1.DockControls.Add(Me.barDockControlLeft)
        Me.barManager1.DockControls.Add(Me.barDockControlRight)
        Me.barManager1.Form = Me
        Me.barManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barButtonItemGravar, Me.barButtonItemCopiarInformacao, Me.barButtonItemFechar})
        Me.barManager1.MaxItemId = 3
        '
        'bar1
        '
        Me.bar1.BarName = "ações"
        Me.bar1.DockCol = 0
        Me.bar1.DockRow = 0
        Me.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barButtonItemGravar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barButtonItemCopiarInformacao, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.bar1.OptionsBar.DrawDragBorder = False
        Me.bar1.OptionsBar.UseWholeRow = True
        Me.bar1.Text = "ações"
        '
        'barButtonItemGravar
        '
        Me.barButtonItemGravar.Caption = "Gravar"
        Me.barButtonItemGravar.Id = 0
        Me.barButtonItemGravar.ImageOptions.Image = CType(resources.GetObject("barButtonItemGravar.ImageOptions.Image"), System.Drawing.Image)
        Me.barButtonItemGravar.Name = "barButtonItemGravar"
        '
        'barButtonItemCopiarInformacao
        '
        Me.barButtonItemCopiarInformacao.Caption = "Copiar Informação"
        Me.barButtonItemCopiarInformacao.Id = 1
        Me.barButtonItemCopiarInformacao.ImageOptions.Image = CType(resources.GetObject("barButtonItemCopiarInformacao.ImageOptions.Image"), System.Drawing.Image)
        Me.barButtonItemCopiarInformacao.Name = "barButtonItemCopiarInformacao"
        '
        'barButtonItemFechar
        '
        Me.barButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.barButtonItemFechar.Caption = "Fechar"
        Me.barButtonItemFechar.Id = 2
        Me.barButtonItemFechar.ImageOptions.Image = CType(resources.GetObject("barButtonItemFechar.ImageOptions.Image"), System.Drawing.Image)
        Me.barButtonItemFechar.Name = "barButtonItemFechar"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.barManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(521, 31)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 354)
        Me.barDockControlBottom.Manager = Me.barManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(521, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
        Me.barDockControlLeft.Manager = Me.barManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 323)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(521, 31)
        Me.barDockControlRight.Manager = Me.barManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 323)
        '
        'memoEditFiacoesObs
        '
        Me.memoEditFiacoesObs.Location = New System.Drawing.Point(8, 197)
        Me.memoEditFiacoesObs.MenuManager = Me.barManager1
        Me.memoEditFiacoesObs.Name = "memoEditFiacoesObs"
        Me.memoEditFiacoesObs.Size = New System.Drawing.Size(507, 151)
        Me.memoEditFiacoesObs.TabIndex = 60
        '
        'dateEditFiacoes
        '
        Me.dateEditFiacoes.EditValue = Nothing
        Me.dateEditFiacoes.Location = New System.Drawing.Point(295, 141)
        Me.dateEditFiacoes.MenuManager = Me.barManager1
        Me.dateEditFiacoes.Name = "dateEditFiacoes"
        Me.dateEditFiacoes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateEditFiacoes.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateEditFiacoes.Size = New System.Drawing.Size(100, 20)
        Me.dateEditFiacoes.TabIndex = 58
        '
        'lookUpEditAprovado
        '
        Me.lookUpEditAprovado.Location = New System.Drawing.Point(435, 106)
        Me.lookUpEditAprovado.MenuManager = Me.barManager1
        Me.lookUpEditAprovado.Name = "lookUpEditAprovado"
        Me.lookUpEditAprovado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditAprovado.Size = New System.Drawing.Size(80, 20)
        Me.lookUpEditAprovado.TabIndex = 57
        '
        'lookUpEditAuditado
        '
        Me.lookUpEditAuditado.Location = New System.Drawing.Point(270, 106)
        Me.lookUpEditAuditado.MenuManager = Me.barManager1
        Me.lookUpEditAuditado.Name = "lookUpEditAuditado"
        Me.lookUpEditAuditado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditAuditado.Size = New System.Drawing.Size(100, 20)
        Me.lookUpEditAuditado.TabIndex = 56
        '
        'lookUpEditClassificacao
        '
        Me.lookUpEditClassificacao.Location = New System.Drawing.Point(91, 141)
        Me.lookUpEditClassificacao.MenuManager = Me.barManager1
        Me.lookUpEditClassificacao.Name = "lookUpEditClassificacao"
        Me.lookUpEditClassificacao.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditClassificacao.Size = New System.Drawing.Size(112, 20)
        Me.lookUpEditClassificacao.TabIndex = 55
        '
        'lookUpEditPreAuditado
        '
        Me.lookUpEditPreAuditado.Location = New System.Drawing.Point(91, 106)
        Me.lookUpEditPreAuditado.MenuManager = Me.barManager1
        Me.lookUpEditPreAuditado.Name = "lookUpEditPreAuditado"
        Me.lookUpEditPreAuditado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditPreAuditado.Size = New System.Drawing.Size(112, 20)
        Me.lookUpEditPreAuditado.TabIndex = 54
        '
        'lookUpEditTipoIdentificacao
        '
        Me.lookUpEditTipoIdentificacao.Location = New System.Drawing.Point(313, 70)
        Me.lookUpEditTipoIdentificacao.MenuManager = Me.barManager1
        Me.lookUpEditTipoIdentificacao.Name = "lookUpEditTipoIdentificacao"
        Me.lookUpEditTipoIdentificacao.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditTipoIdentificacao.Size = New System.Drawing.Size(202, 20)
        Me.lookUpEditTipoIdentificacao.TabIndex = 53
        '
        'textEditNIdentificacao
        '
        Me.textEditNIdentificacao.Location = New System.Drawing.Point(91, 70)
        Me.textEditNIdentificacao.MenuManager = Me.barManager1
        Me.textEditNIdentificacao.Name = "textEditNIdentificacao"
        Me.textEditNIdentificacao.Size = New System.Drawing.Size(112, 20)
        Me.textEditNIdentificacao.TabIndex = 52
        '
        'textEditCliente
        '
        Me.textEditCliente.Location = New System.Drawing.Point(218, 38)
        Me.textEditCliente.MenuManager = Me.barManager1
        Me.textEditCliente.Name = "textEditCliente"
        Me.textEditCliente.Size = New System.Drawing.Size(297, 20)
        Me.textEditCliente.TabIndex = 51
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(5, 178)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(70, 13)
        Me.label9.TabIndex = 50
        Me.label9.Text = "Observações"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(215, 144)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(74, 13)
        Me.label8.TabIndex = 49
        Me.label8.Text = "Data Auditoria"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(376, 109)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(53, 13)
        Me.label7.TabIndex = 48
        Me.label7.Text = "Aprovado"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(215, 109)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(49, 13)
        Me.label6.TabIndex = 47
        Me.label6.Text = "Auditado"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(16, 144)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(69, 13)
        Me.label5.TabIndex = 46
        Me.label5.Text = "Classificação"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(17, 109)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(68, 13)
        Me.label4.TabIndex = 45
        Me.label4.Text = "Pré-Auditado"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(215, 73)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(92, 13)
        Me.label3.TabIndex = 44
        Me.label3.Text = "Tipo Identificação"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(5, 73)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(80, 13)
        Me.label2.TabIndex = 43
        Me.label2.Text = "NºIdentificação"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(24, 41)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(39, 13)
        Me.label1.TabIndex = 42
        Me.label1.Text = "Cliente"
        '
        'TextEditFacLocal
        '
        Me.TextEditFacLocal.Location = New System.Drawing.Point(435, 171)
        Me.TextEditFacLocal.MenuManager = Me.barManager1
        Me.TextEditFacLocal.Name = "TextEditFacLocal"
        Me.TextEditFacLocal.Size = New System.Drawing.Size(80, 20)
        Me.TextEditFacLocal.TabIndex = 61
        Me.TextEditFacLocal.Visible = False
        '
        'TextEditFacMor
        '
        Me.TextEditFacMor.Location = New System.Drawing.Point(435, 171)
        Me.TextEditFacMor.MenuManager = Me.barManager1
        Me.TextEditFacMor.Name = "TextEditFacMor"
        Me.TextEditFacMor.Size = New System.Drawing.Size(80, 20)
        Me.TextEditFacMor.TabIndex = 62
        Me.TextEditFacMor.Visible = False
        '
        'TextEditPais
        '
        Me.TextEditPais.Location = New System.Drawing.Point(435, 171)
        Me.TextEditPais.MenuManager = Me.barManager1
        Me.TextEditPais.Name = "TextEditPais"
        Me.TextEditPais.Size = New System.Drawing.Size(80, 20)
        Me.TextEditPais.TabIndex = 63
        Me.TextEditPais.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TextEditPais)
        Me.Controls.Add(Me.TextEditFacMor)
        Me.Controls.Add(Me.TextEditFacLocal)
        Me.Controls.Add(Me.memoEditFiacoesObs)
        Me.Controls.Add(Me.dateEditFiacoes)
        Me.Controls.Add(Me.lookUpEditAprovado)
        Me.Controls.Add(Me.lookUpEditAuditado)
        Me.Controls.Add(Me.lookUpEditClassificacao)
        Me.Controls.Add(Me.lookUpEditPreAuditado)
        Me.Controls.Add(Me.lookUpEditTipoIdentificacao)
        Me.Controls.Add(Me.textEditNIdentificacao)
        Me.Controls.Add(Me.textEditCliente)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "Form1"
        Me.Size = New System.Drawing.Size(521, 354)
        Me.Text = "Form1"
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.memoEditFiacoesObs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateEditFiacoes.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateEditFiacoes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditAprovado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditAuditado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditClassificacao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditPreAuditado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditTipoIdentificacao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditNIdentificacao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditFacLocal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditFacMor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditPais.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents barManager1 As DevExpress.XtraBars.BarManager
    Private WithEvents bar1 As DevExpress.XtraBars.Bar
    Private WithEvents barButtonItemGravar As DevExpress.XtraBars.BarButtonItem
    Private WithEvents barButtonItemCopiarInformacao As DevExpress.XtraBars.BarButtonItem
    Private WithEvents barButtonItemFechar As DevExpress.XtraBars.BarButtonItem
    Private WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Private WithEvents memoEditFiacoesObs As DevExpress.XtraEditors.MemoEdit
    Private WithEvents dateEditFiacoes As DevExpress.XtraEditors.DateEdit
    Private WithEvents lookUpEditAprovado As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents lookUpEditAuditado As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents lookUpEditClassificacao As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents lookUpEditPreAuditado As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents lookUpEditTipoIdentificacao As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents textEditNIdentificacao As DevExpress.XtraEditors.TextEdit
    Private WithEvents textEditCliente As DevExpress.XtraEditors.TextEdit
    Private WithEvents label9 As Windows.Forms.Label
    Private WithEvents label8 As Windows.Forms.Label
    Private WithEvents label7 As Windows.Forms.Label
    Private WithEvents label6 As Windows.Forms.Label
    Private WithEvents label5 As Windows.Forms.Label
    Private WithEvents label4 As Windows.Forms.Label
    Private WithEvents label3 As Windows.Forms.Label
    Private WithEvents label2 As Windows.Forms.Label
    Private WithEvents label1 As Windows.Forms.Label
    Friend WithEvents TextEditFacLocal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditPais As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditFacMor As DevExpress.XtraEditors.TextEdit
End Class
