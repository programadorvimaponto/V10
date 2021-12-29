'Imports System
'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Text
'Imports System.Threading.Tasks
'Imports Primavera.Extensibility.BusinessEntities
'Imports Primavera.Extensibility.CustomForm
'Imports StdBE100
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

'Public Class FrmAlteraCertificadoTransacao
'    Inherits CustomForm

'    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
'    Private components As ComponentModel.IContainer
'    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
'    Friend WithEvents BarButtonItemGravar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents BarButtonItemFechar As DevExpress.XtraBars.BarButtonItem
'    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
'    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents LookUpEditDataCert As DevExpress.XtraEditors.LookUpEdit
'    Friend WithEvents TextEditNumCert As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents Label3 As Windows.Forms.Label
'    Friend WithEvents Label2 As Windows.Forms.Label
'    Friend WithEvents Label1 As Windows.Forms.Label
'    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents CheckEditBCI As DevExpress.XtraEditors.CheckEdit
'    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
'    Friend WithEvents TextEditArmazem As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditlote As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditArtigo As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents TextEditDocumento As DevExpress.XtraEditors.TextEdit
'    Friend WithEvents LookUpEditProgramLabel As DevExpress.XtraEditors.LookUpEdit
'    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl

'    Private Sub InitializeComponent()
'        Me.components = New System.ComponentModel.Container()
'        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAlteraCertificadoTransacao))
'        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
'        Me.Bar1 = New DevExpress.XtraBars.Bar()
'        Me.BarButtonItemGravar = New DevExpress.XtraBars.BarButtonItem()
'        Me.BarButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
'        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
'        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
'        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
'        Me.TextEditArmazem = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditlote = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditArtigo = New DevExpress.XtraEditors.TextEdit()
'        Me.TextEditDocumento = New DevExpress.XtraEditors.TextEdit()
'        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
'        Me.CheckEditBCI = New DevExpress.XtraEditors.CheckEdit()
'        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
'        Me.LookUpEditDataCert = New DevExpress.XtraEditors.LookUpEdit()
'        Me.TextEditNumCert = New DevExpress.XtraEditors.TextEdit()
'        Me.Label3 = New System.Windows.Forms.Label()
'        Me.Label2 = New System.Windows.Forms.Label()
'        Me.Label1 = New System.Windows.Forms.Label()
'        Me.LookUpEditProgramLabel = New DevExpress.XtraEditors.LookUpEdit()
'        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl1.SuspendLayout()
'        CType(Me.TextEditArmazem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditlote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditArtigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl2.SuspendLayout()
'        CType(Me.CheckEditBCI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.GroupControl3.SuspendLayout()
'        CType(Me.LookUpEditDataCert.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.TextEditNumCert.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.LookUpEditProgramLabel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
'        Me.BarButtonItemGravar.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
'        Me.BarButtonItemGravar.Name = "BarButtonItemGravar"
'        '
'        'BarButtonItemFechar
'        '
'        Me.BarButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
'        Me.BarButtonItemFechar.Caption = "Fechar"
'        Me.BarButtonItemFechar.Id = 1
'        Me.BarButtonItemFechar.ImageOptions.Image = CType(resources.GetObject("BarButtonItem2.ImageOptions.Image"), System.Drawing.Image)
'        Me.BarButtonItemFechar.Name = "BarButtonItemFechar"
'        '
'        'barDockControlTop
'        '
'        Me.barDockControlTop.CausesValidation = False
'        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
'        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
'        Me.barDockControlTop.Manager = Me.BarManager1
'        Me.barDockControlTop.Size = New System.Drawing.Size(359, 31)
'        '
'        'barDockControlBottom
'        '
'        Me.barDockControlBottom.CausesValidation = False
'        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
'        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 297)
'        Me.barDockControlBottom.Manager = Me.BarManager1
'        Me.barDockControlBottom.Size = New System.Drawing.Size(359, 0)
'        '
'        'barDockControlLeft
'        '
'        Me.barDockControlLeft.CausesValidation = False
'        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
'        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
'        Me.barDockControlLeft.Manager = Me.BarManager1
'        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 266)
'        '
'        'barDockControlRight
'        '
'        Me.barDockControlRight.CausesValidation = False
'        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
'        Me.barDockControlRight.Location = New System.Drawing.Point(359, 31)
'        Me.barDockControlRight.Manager = Me.BarManager1
'        Me.barDockControlRight.Size = New System.Drawing.Size(0, 266)
'        '
'        'GroupControl1
'        '
'        Me.GroupControl1.Controls.Add(Me.TextEditArmazem)
'        Me.GroupControl1.Controls.Add(Me.TextEditlote)
'        Me.GroupControl1.Controls.Add(Me.TextEditArtigo)
'        Me.GroupControl1.Controls.Add(Me.TextEditDocumento)
'        Me.GroupControl1.Location = New System.Drawing.Point(3, 37)
'        Me.GroupControl1.Name = "GroupControl1"
'        Me.GroupControl1.Size = New System.Drawing.Size(226, 121)
'        Me.GroupControl1.TabIndex = 4
'        Me.GroupControl1.Text = "Encomenda/Artigo"
'        '
'        'TextEditArmazem
'        '
'        Me.TextEditArmazem.Location = New System.Drawing.Point(157, 92)
'        Me.TextEditArmazem.MenuManager = Me.BarManager1
'        Me.TextEditArmazem.Name = "TextEditArmazem"
'        Me.TextEditArmazem.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditArmazem.Properties.Appearance.Options.UseFont = True
'        Me.TextEditArmazem.Size = New System.Drawing.Size(64, 22)
'        Me.TextEditArmazem.TabIndex = 3
'        '
'        'TextEditlote
'        '
'        Me.TextEditlote.Location = New System.Drawing.Point(5, 92)
'        Me.TextEditlote.MenuManager = Me.BarManager1
'        Me.TextEditlote.Name = "TextEditlote"
'        Me.TextEditlote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditlote.Properties.Appearance.Options.UseFont = True
'        Me.TextEditlote.Size = New System.Drawing.Size(146, 22)
'        Me.TextEditlote.TabIndex = 2
'        '
'        'TextEditArtigo
'        '
'        Me.TextEditArtigo.Location = New System.Drawing.Point(5, 64)
'        Me.TextEditArtigo.MenuManager = Me.BarManager1
'        Me.TextEditArtigo.Name = "TextEditArtigo"
'        Me.TextEditArtigo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditArtigo.Properties.Appearance.Options.UseFont = True
'        Me.TextEditArtigo.Size = New System.Drawing.Size(216, 22)
'        Me.TextEditArtigo.TabIndex = 1
'        '
'        'TextEditDocumento
'        '
'        Me.TextEditDocumento.Location = New System.Drawing.Point(5, 34)
'        Me.TextEditDocumento.MenuManager = Me.BarManager1
'        Me.TextEditDocumento.Name = "TextEditDocumento"
'        Me.TextEditDocumento.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditDocumento.Properties.Appearance.Options.UseFont = True
'        Me.TextEditDocumento.Size = New System.Drawing.Size(216, 22)
'        Me.TextEditDocumento.TabIndex = 0
'        '
'        'GroupControl2
'        '
'        Me.GroupControl2.Controls.Add(Me.CheckEditBCI)
'        Me.GroupControl2.Location = New System.Drawing.Point(235, 37)
'        Me.GroupControl2.Name = "GroupControl2"
'        Me.GroupControl2.Size = New System.Drawing.Size(116, 121)
'        Me.GroupControl2.TabIndex = 5
'        Me.GroupControl2.Text = "Outros"
'        '
'        'CheckEditBCI
'        '
'        Me.CheckEditBCI.Location = New System.Drawing.Point(5, 36)
'        Me.CheckEditBCI.MenuManager = Me.BarManager1
'        Me.CheckEditBCI.Name = "CheckEditBCI"
'        Me.CheckEditBCI.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.CheckEditBCI.Properties.Appearance.Options.UseFont = True
'        Me.CheckEditBCI.Properties.Caption = "BCI Recebido"
'        Me.CheckEditBCI.Size = New System.Drawing.Size(100, 20)
'        Me.CheckEditBCI.TabIndex = 0
'        '
'        'GroupControl3
'        '
'        Me.GroupControl3.Controls.Add(Me.LookUpEditProgramLabel)
'        Me.GroupControl3.Controls.Add(Me.LookUpEditDataCert)
'        Me.GroupControl3.Controls.Add(Me.TextEditNumCert)
'        Me.GroupControl3.Controls.Add(Me.Label3)
'        Me.GroupControl3.Controls.Add(Me.Label2)
'        Me.GroupControl3.Controls.Add(Me.Label1)
'        Me.GroupControl3.Location = New System.Drawing.Point(3, 164)
'        Me.GroupControl3.Name = "GroupControl3"
'        Me.GroupControl3.Size = New System.Drawing.Size(348, 124)
'        Me.GroupControl3.TabIndex = 6
'        Me.GroupControl3.Text = "Dados Certificado"
'        '
'        'LookUpEditDataCert
'        '
'        Me.LookUpEditDataCert.Location = New System.Drawing.Point(120, 61)
'        Me.LookUpEditDataCert.MenuManager = Me.BarManager1
'        Me.LookUpEditDataCert.Name = "LookUpEditDataCert"
'        Me.LookUpEditDataCert.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.LookUpEditDataCert.Properties.Appearance.Options.UseFont = True
'        Me.LookUpEditDataCert.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.LookUpEditDataCert.Size = New System.Drawing.Size(223, 20)
'        Me.LookUpEditDataCert.TabIndex = 5
'        '
'        'TextEditNumCert
'        '
'        Me.TextEditNumCert.Location = New System.Drawing.Point(120, 26)
'        Me.TextEditNumCert.MenuManager = Me.BarManager1
'        Me.TextEditNumCert.Name = "TextEditNumCert"
'        Me.TextEditNumCert.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.TextEditNumCert.Properties.Appearance.Options.UseFont = True
'        Me.TextEditNumCert.Size = New System.Drawing.Size(223, 22)
'        Me.TextEditNumCert.TabIndex = 4
'        '
'        'Label3
'        '
'        Me.Label3.AutoSize = True
'        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label3.Location = New System.Drawing.Point(15, 62)
'        Me.Label3.Name = "Label3"
'        Me.Label3.Size = New System.Drawing.Size(99, 16)
'        Me.Label3.TabIndex = 2
'        Me.Label3.Text = "Data Certificado"
'        '
'        'Label2
'        '
'        Me.Label2.AutoSize = True
'        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label2.Location = New System.Drawing.Point(76, 99)
'        Me.Label2.Name = "Label2"
'        Me.Label2.Size = New System.Drawing.Size(38, 16)
'        Me.Label2.TabIndex = 1
'        Me.Label2.Text = "Label"
'        '
'        'Label1
'        '
'        Me.Label1.AutoSize = True
'        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label1.Location = New System.Drawing.Point(15, 29)
'        Me.Label1.Name = "Label1"
'        Me.Label1.Size = New System.Drawing.Size(99, 16)
'        Me.Label1.TabIndex = 0
'        Me.Label1.Text = "Num Certificado"
'        '
'        'LookUpEditProgramLabel
'        '
'        Me.LookUpEditProgramLabel.Location = New System.Drawing.Point(120, 97)
'        Me.LookUpEditProgramLabel.MenuManager = Me.BarManager1
'        Me.LookUpEditProgramLabel.Name = "LookUpEditProgramLabel"
'        Me.LookUpEditProgramLabel.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.LookUpEditProgramLabel.Properties.Appearance.Options.UseFont = True
'        Me.LookUpEditProgramLabel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
'        Me.LookUpEditProgramLabel.Size = New System.Drawing.Size(223, 20)
'        Me.LookUpEditProgramLabel.TabIndex = 6
'        '
'        'FrmAlteraCertificadoTransacao
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
'        Me.Controls.Add(Me.GroupControl3)
'        Me.Controls.Add(Me.GroupControl2)
'        Me.Controls.Add(Me.GroupControl1)
'        Me.Controls.Add(Me.barDockControlLeft)
'        Me.Controls.Add(Me.barDockControlRight)
'        Me.Controls.Add(Me.barDockControlBottom)
'        Me.Controls.Add(Me.barDockControlTop)
'        Me.Name = "FrmAlteraCertificadoTransacao"
'        Me.Size = New System.Drawing.Size(359, 297)
'        Me.Text = "Altera Certificado Transação"
'        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl1.ResumeLayout(False)
'        CType(Me.TextEditArmazem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditlote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditArtigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditDocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl2.ResumeLayout(False)
'        CType(Me.CheckEditBCI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.GroupControl3.ResumeLayout(False)
'        Me.GroupControl3.PerformLayout()
'        CType(Me.LookUpEditDataCert.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.TextEditNumCert.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.LookUpEditProgramLabel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.ResumeLayout(False)
'        Me.PerformLayout()

'    End Sub

'    Dim ListaProgramLabel As StdBELista
'    Dim SqlStringProgramLabel As String

'    Public Sub New()

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.


'        TextEditArtigo.EditValue = Module1.certArtigo
'        TextEditArmazem.EditValue = Module1.certArmazem
'        TextEditlote.EditValue = Module1.certLote
'        TextEditNumCert.EditValue = Module1.certCertificadoTransacao
'        LookUpEditDataCert.EditValue = Module1.certDataCertificado
'        TextEditDocumento.EditValue = Module1.certDocumento
'        txtIDlinha = Module1.certIDlinha
'        CheckEditBCI.EditValue = Module1.certBCI

'        If UCase(BSO.Inventario.ArtigosLotes.Edita(TextEditArtigo.EditValue, TextEditlote.EditValue).Observacoes) Like "*BCI*" Or Module1.certDescricao Like "*BCI*" Then
'            CheckEditBCI.Enabled = True
'        Else
'            CheckEditBCI.Enabled = False
'        End If

'        'Preenche combo das Program Labels
'        SqlStringProgramLabel = "SELECT * FROM TDU_CertificadosLabels ORDER BY CDU_Id ASC"

'        ListaProgramLabel = BSO.Consulta(SqlStringProgramLabel)

'        If ListaProgramLabel.Vazia = False Then

'            ListaProgramLabel.Inicio()

'            For k = 1 To ListaProgramLabel.NumLinhas
'                Dim dt As DataTable

'                dt.Rows.Add(ListaProgramLabel.Valor("CDU_Id") & " - " & ListaProgramLabel.Valor("CDU_Program") & " - " & ListaProgramLabel.Valor("CDU_Label"))

'                LookUpEditProgramLabel.Properties.DataSource = dt
'                ListaProgramLabel.Seguinte()

'            Next k

'        End If


'        'Preenche texto default da combo igual ao cert na encomenda
'        SqlStringProgramLabel = "SELECT * FROM TDU_CertificadosLabels where CDU_Id='" & Module1.certProgramLabel & "' ORDER BY CDU_Id ASC"

'        ListaProgramLabel = BSO.Consulta(SqlStringProgramLabel)

'        If ListaProgramLabel.Vazia = False Then

'            ListaProgramLabel.Inicio()



'            Me.LookUpEditProgramLabel.EditValue = ListaProgramLabel.Valor("CDU_Id") & " - " & ListaProgramLabel.Valor("CDU_Program") & " - " & ListaProgramLabel.Valor("CDU_Label")


'        End If


'    End Sub

'    Private Sub BarButtonItemGravar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemGravar.ItemClick

'        BSO.DSO.ExecuteSQL("update LinhasCompras set CDU_DataCertificadoTrans=convert(datetime,'" & LookUpEditDataCert.EditValue & "',105), CDU_NumCertificadoTrans='" & TextEditNumCert.EditValue & "', CDU_ProgramLabels='" & Strings.Left(Me.LookUpEditProgramLabel.EditValue.Text, 1) & "', CDU_BCI='" & CheckEditBCI.EditValue & "' where Id='" & Module1.certIDlinha & "'")
'        EditorCompras.DocumentoCompra.Linhas(EditorCompras.LinhaActual).CamposUtil("CDU_DataCertificadoTrans") = LookUpEditDataCert.EditValue
'        EditorCompras.DocumentoCompra.Linhas(EditorCompras.LinhaActual).CamposUtil("CDU_NumCertificadoTrans") = TextEditNumCert.EditValue
'        EditorCompras.DocumentoCompra.Linhas(EditorCompras.LinhaActual).CamposUtil("CDU_ProgramLabels") = Strings.Left(Me.LookUpEditProgramLabel.EditValue, 1)
'        EditorCompras.DocumentoCompra.Linhas(EditorCompras.LinhaActual).CamposUtil("CDU_BCI") = CheckEditBCI.EditValue

'        BSO.DSO.ExecuteSQL("exec [dbo].[spInserirCert]")

'        Me.DialogResult = DialogResult.OK
'        Me.Close()

'    End Sub

'    Private Sub BarButtonItemFechar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemFechar.ItemClick


'        Me.DialogResult = DialogResult.Cancel
'        Me.Close()

'    End Sub
'End Class