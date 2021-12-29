Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.CustomForm
Imports StdBE100

Public Class FrmClientesView
    Inherits CustomForm


    Public EmpresaDestino As String
    Public PRIEmpresaDestino As String
    Public Armazem As String

    Public Shared Cliente As String
    Friend WithEvents LabelTitulo As Windows.Forms.Label
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents TextEditDescricaoCliente As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoIdioma As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditDescricaoLocalDescarga As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoLocalidadeEntrega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoPostalEntrega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoMoradaEntrega2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoMoradaEntrega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoLocalDescarga As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditDescricaoArmazem As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoArmazem As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoCliente As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodigoMargem As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditDistritoEntrega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditCodPostalLocalidadeEntrega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Private components As ComponentModel.IContainer
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItemConfirmar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItemFechar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl


    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmClientesView))
        Me.LabelTitulo = New System.Windows.Forms.Label()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TextEditCodigoMargem = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditDistritoEntrega = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodPostalLocalidadeEntrega = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditDescricaoCliente = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodigoIdioma = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditDescricaoLocalDescarga = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodigoLocalidadeEntrega = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodigoPostalEntrega = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodigoMoradaEntrega2 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodigoMoradaEntrega = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodigoLocalDescarga = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditDescricaoArmazem = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodigoArmazem = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditCodigoCliente = New DevExpress.XtraEditors.TextEdit()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItemConfirmar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItemFechar = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.TextEditCodigoMargem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditDistritoEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodPostalLocalidadeEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditDescricaoCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodigoIdioma.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditDescricaoLocalDescarga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodigoLocalidadeEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodigoPostalEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodigoMoradaEntrega2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodigoMoradaEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodigoLocalDescarga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditDescricaoArmazem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodigoArmazem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditCodigoCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelTitulo
        '
        Me.LabelTitulo.AutoSize = True
        Me.LabelTitulo.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTitulo.Location = New System.Drawing.Point(193, 34)
        Me.LabelTitulo.Name = "LabelTitulo"
        Me.LabelTitulo.Size = New System.Drawing.Size(83, 23)
        Me.LabelTitulo.TabIndex = 0
        Me.LabelTitulo.Text = "Empresa"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoMargem)
        Me.GroupControl1.Controls.Add(Me.TextEditDistritoEntrega)
        Me.GroupControl1.Controls.Add(Me.TextEditCodPostalLocalidadeEntrega)
        Me.GroupControl1.Controls.Add(Me.TextEditDescricaoCliente)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoIdioma)
        Me.GroupControl1.Controls.Add(Me.TextEditDescricaoLocalDescarga)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoLocalidadeEntrega)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoPostalEntrega)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoMoradaEntrega2)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoMoradaEntrega)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoLocalDescarga)
        Me.GroupControl1.Controls.Add(Me.TextEditDescricaoArmazem)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoArmazem)
        Me.GroupControl1.Controls.Add(Me.TextEditCodigoCliente)
        Me.GroupControl1.Controls.Add(Me.Label4)
        Me.GroupControl1.Controls.Add(Me.Label3)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 60)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(484, 319)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Geral"
        '
        'TextEditCodigoMargem
        '
        Me.TextEditCodigoMargem.Location = New System.Drawing.Point(105, 288)
        Me.TextEditCodigoMargem.Name = "TextEditCodigoMargem"
        Me.TextEditCodigoMargem.Size = New System.Drawing.Size(85, 20)
        Me.TextEditCodigoMargem.TabIndex = 17
        '
        'TextEditDistritoEntrega
        '
        Me.TextEditDistritoEntrega.Location = New System.Drawing.Point(394, 290)
        Me.TextEditDistritoEntrega.Name = "TextEditDistritoEntrega"
        Me.TextEditDistritoEntrega.Size = New System.Drawing.Size(85, 20)
        Me.TextEditDistritoEntrega.TabIndex = 16
        '
        'TextEditCodPostalLocalidadeEntrega
        '
        Me.TextEditCodPostalLocalidadeEntrega.Location = New System.Drawing.Point(294, 250)
        Me.TextEditCodPostalLocalidadeEntrega.Name = "TextEditCodPostalLocalidadeEntrega"
        Me.TextEditCodPostalLocalidadeEntrega.Size = New System.Drawing.Size(185, 20)
        Me.TextEditCodPostalLocalidadeEntrega.TabIndex = 15
        '
        'TextEditDescricaoCliente
        '
        Me.TextEditDescricaoCliente.Location = New System.Drawing.Point(211, 29)
        Me.TextEditDescricaoCliente.Name = "TextEditDescricaoCliente"
        Me.TextEditDescricaoCliente.Size = New System.Drawing.Size(192, 20)
        Me.TextEditDescricaoCliente.TabIndex = 14
        '
        'TextEditCodigoIdioma
        '
        Me.TextEditCodigoIdioma.Location = New System.Drawing.Point(409, 29)
        Me.TextEditCodigoIdioma.Name = "TextEditCodigoIdioma"
        Me.TextEditCodigoIdioma.Size = New System.Drawing.Size(70, 20)
        Me.TextEditCodigoIdioma.TabIndex = 13
        '
        'TextEditDescricaoLocalDescarga
        '
        Me.TextEditDescricaoLocalDescarga.Location = New System.Drawing.Point(211, 107)
        Me.TextEditDescricaoLocalDescarga.Name = "TextEditDescricaoLocalDescarga"
        Me.TextEditDescricaoLocalDescarga.Size = New System.Drawing.Size(268, 20)
        Me.TextEditDescricaoLocalDescarga.TabIndex = 12
        '
        'TextEditCodigoLocalidadeEntrega
        '
        Me.TextEditCodigoLocalidadeEntrega.Location = New System.Drawing.Point(105, 215)
        Me.TextEditCodigoLocalidadeEntrega.Name = "TextEditCodigoLocalidadeEntrega"
        Me.TextEditCodigoLocalidadeEntrega.Size = New System.Drawing.Size(374, 20)
        Me.TextEditCodigoLocalidadeEntrega.TabIndex = 11
        '
        'TextEditCodigoPostalEntrega
        '
        Me.TextEditCodigoPostalEntrega.Location = New System.Drawing.Point(105, 250)
        Me.TextEditCodigoPostalEntrega.Name = "TextEditCodigoPostalEntrega"
        Me.TextEditCodigoPostalEntrega.Size = New System.Drawing.Size(185, 20)
        Me.TextEditCodigoPostalEntrega.TabIndex = 10
        '
        'TextEditCodigoMoradaEntrega2
        '
        Me.TextEditCodigoMoradaEntrega2.Location = New System.Drawing.Point(105, 180)
        Me.TextEditCodigoMoradaEntrega2.Name = "TextEditCodigoMoradaEntrega2"
        Me.TextEditCodigoMoradaEntrega2.Size = New System.Drawing.Size(374, 20)
        Me.TextEditCodigoMoradaEntrega2.TabIndex = 9
        '
        'TextEditCodigoMoradaEntrega
        '
        Me.TextEditCodigoMoradaEntrega.Location = New System.Drawing.Point(105, 144)
        Me.TextEditCodigoMoradaEntrega.Name = "TextEditCodigoMoradaEntrega"
        Me.TextEditCodigoMoradaEntrega.Size = New System.Drawing.Size(374, 20)
        Me.TextEditCodigoMoradaEntrega.TabIndex = 8
        '
        'TextEditCodigoLocalDescarga
        '
        Me.TextEditCodigoLocalDescarga.Location = New System.Drawing.Point(105, 107)
        Me.TextEditCodigoLocalDescarga.Name = "TextEditCodigoLocalDescarga"
        Me.TextEditCodigoLocalDescarga.Size = New System.Drawing.Size(100, 20)
        Me.TextEditCodigoLocalDescarga.TabIndex = 7
        '
        'TextEditDescricaoArmazem
        '
        Me.TextEditDescricaoArmazem.Location = New System.Drawing.Point(211, 69)
        Me.TextEditDescricaoArmazem.Name = "TextEditDescricaoArmazem"
        Me.TextEditDescricaoArmazem.Size = New System.Drawing.Size(268, 20)
        Me.TextEditDescricaoArmazem.TabIndex = 6
        '
        'TextEditCodigoArmazem
        '
        Me.TextEditCodigoArmazem.Location = New System.Drawing.Point(105, 69)
        Me.TextEditCodigoArmazem.Name = "TextEditCodigoArmazem"
        Me.TextEditCodigoArmazem.Size = New System.Drawing.Size(100, 20)
        Me.TextEditCodigoArmazem.TabIndex = 5
        '
        'TextEditCodigoCliente
        '
        Me.TextEditCodigoCliente.Location = New System.Drawing.Point(105, 29)
        Me.TextEditCodigoCliente.Name = "TextEditCodigoCliente"
        Me.TextEditCodigoCliente.Size = New System.Drawing.Size(100, 20)
        Me.TextEditCodigoCliente.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(44, 291)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Margem"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Local Descarga"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(36, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Armazém"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(52, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cliente"
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItemConfirmar, Me.BarButtonItemFechar})
        Me.BarManager1.MaxItemId = 2
        '
        'Bar1
        '
        Me.Bar1.BarName = "ações"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemConfirmar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "ações"
        '
        'BarButtonItemConfirmar
        '
        Me.BarButtonItemConfirmar.Caption = "Confirmar"
        Me.BarButtonItemConfirmar.Id = 0
        Me.BarButtonItemConfirmar.ImageOptions.Image = CType(resources.GetObject("BarButtonItemConfirmar.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItemConfirmar.Name = "BarButtonItemConfirmar"
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
        Me.barDockControlTop.Size = New System.Drawing.Size(490, 31)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 389)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(490, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 358)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(490, 31)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 358)
        '
        'FrmClientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.LabelTitulo)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "FrmClientes"
        Me.Size = New System.Drawing.Size(490, 389)
        Me.Text = "Dados Clientes"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.TextEditCodigoMargem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditDistritoEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodPostalLocalidadeEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditDescricaoCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodigoIdioma.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditDescricaoLocalDescarga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodigoLocalidadeEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodigoPostalEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodigoMoradaEntrega2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodigoMoradaEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodigoLocalDescarga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditDescricaoArmazem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodigoArmazem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditCodigoCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub FrmClientes_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

        PRIEmpresaDestino = "PRI" & EmpresaDestino
        LabelTitulo.Text = "Clientes da Empresa " & EmpresaDestino

    End Sub

    Public Function ValidarCampos() As Boolean

        ActualizaDescricaoClientes()
        ActualizaDescricaoArmazem()
        ActualizaDescricaoLocalDescarga()

        If Len(Me.TextEditDescricaoCliente.EditValue) = 0 Then
            MsgBox("Cliente inválido", vbCritical, "Atenção")
            ValidarCampos = False
            Exit Function
        End If

        If Len(Me.TextEditDescricaoArmazem.EditValue) = 0 Then
            MsgBox("Armazém inválido", vbCritical, "Atenção")
            ValidarCampos = False
            Exit Function
        End If

        If Len(Me.TextEditDescricaoLocalDescarga.EditValue) = 0 And Len(Me.TextEditCodigoLocalDescarga.EditValue) > 0 Then
            MsgBox("Local Descarga inválido", vbCritical, "Atenção")
            ValidarCampos = False
            Exit Function
        End If

        ValidarCampos = True

    End Function

    Private Sub BarButtonItemFechar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemFechar.ItemClick

        Me.DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub

    Private Sub BarButtonItemConfirmar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemConfirmar.ItemClick

        If Not ValidarCampos() Then Exit Sub

        Me.DialogResult = DialogResult.OK

        Me.Close()

    End Sub

    Private Sub TextEditCodigoCliente_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditCodigoCliente.EditValueChanged

        ActualizaDescricaoClientes()

    End Sub

    Private Sub TextEditCodigoArmazem_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditCodigoArmazem.EditValueChanged

        ActualizaDescricaoArmazem()

    End Sub

    Private Sub TextEditCodigoLocalDescarga_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditCodigoLocalDescarga.EditValueChanged

        ActualizaDescricaoLocalDescarga()

    End Sub

    Private Sub TextEditCodigoCliente_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles TextEditCodigoCliente.KeyDown

        If e.KeyCode = Windows.Forms.Keys.E.F4 Then
            InvocarListaClientes()
        End If

    End Sub

    Private Sub TextEditCodigoArmazem_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles TextEditCodigoArmazem.KeyDown

        If e.KeyCode = Windows.Forms.Keys.E.F4 Then
            InvocarListaArmazens()
        End If

    End Sub

    Private Sub TextEditCodigoLocalDescarga_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles TextEditCodigoLocalDescarga.KeyDown

        If e.KeyCode = Windows.Forms.Keys.E.F4 Then
            InvocarListaLocalDescarga()
        End If

    End Sub


    'Funções*******************


    '*InvocarListas*
    Private Sub InvocarListaClientes()

        'Não faz validação do cliente
        If Not Validacoes(False) Then Exit Sub

        'A abertura da lista pode ser modal ou não modal
        Me.TextEditCodigoCliente.EditValue = PSO.Listas.GetF4SQL("Empresa " & EmpresaDestino, "Select * from " & PRIEmpresaDestino & ".dbo.Clientes  where ClienteAnulado = 0", "Cliente")
        'Aplicacao.PlataformaPRIMAVERA.AbreLista 0, "Clientes", "Cliente", Me, ctrF4, "mnuTabClientes", , , , blnModal:=True
    End Sub

    Private Sub InvocarListaArmazens()

        'Não faz validação do cliente
        If Not Validacoes(False) Then Exit Sub

        'A abertura da lista pode ser modal ou não modal
        Me.TextEditCodigoArmazem.EditValue = PSO.Listas.GetF4SQL("Armazéns da Empresa " & EmpresaDestino, "Select * from " & PRIEmpresaDestino & ".dbo.Armazens " & " Where BloqueioEntradas = 0", "Armazem")
        'Aplicacao.PlataformaPRIMAVERA.AbreLista 0, "Clientes", "Cliente", Me, ctrF4, "mnuTabClientes", , , , blnModal:=True
    End Sub

    Private Sub InvocarListaLocalDescarga()

        'Faz validação do cliente
        If Not Validacoes(True) Then Exit Sub

        'A abertura da lista pode ser modal ou não modal
        Dim Titulo As String
        Titulo = "Locais de Descarga do Cliente " & Me.TextEditCodigoCliente.EditValue & " - " & Me.TextEditDescricaoCliente.EditValue

        Me.TextEditCodigoLocalDescarga.EditValue = PSO.Listas.GetF4SQL(Titulo, "Select * from " & PRIEmpresaDestino & ".dbo.MoradasAlternativasClientes  where cliente = '" & Me.TextEditCodigoCliente.EditValue & "' ", " MoradaAlternativa")
        'Aplicacao.PlataformaPRIMAVERA.AbreLista 0, "Clientes", "Cliente", Me, ctrF4, "mnuTabClientes", , , , blnModal:=True
    End Sub
    '*InvocarListas*


    '*ActualizaDescrições*
    Public Function ActualizaDescricaoClientes() As Boolean


        Dim stdBE_Lista As StdBELista

        If Len(Me.TextEditCodigoCliente.EditValue) = 0 Then
            Me.TextEditDescricaoCliente.EditValue = ""
            Me.TextEditCodigoIdioma.EditValue = ""
            ActualizaDescricaoClientes = False
            Exit Function
        End If

        stdBE_Lista = BSO.Consulta("SELECT Nome, Idioma FROM " & PRIEmpresaDestino & ".dbo.Clientes WHERE Cliente= '" & Me.TextEditCodigoCliente.EditValue & "'")

        If Not stdBE_Lista.Vazia Then
            stdBE_Lista.Inicio()
            Me.TextEditDescricaoCliente.EditValue = stdBE_Lista.Valor("Nome")
            Me.TextEditCodigoIdioma.EditValue = stdBE_Lista.Valor("Idioma")
            ActualizaDescricaoClientes = True
        Else
            Me.TextEditDescricaoCliente.EditValue = ""
            Me.TextEditCodigoIdioma.EditValue = ""
            ActualizaDescricaoClientes = False
        End If


    End Function

    Public Function ActualizaDescricaoArmazem() As Boolean

        Dim stdBE_Lista As StdBELista

        If Len(Me.TextEditCodigoArmazem.EditValue) = 0 Then ActualizaDescricaoArmazem = False : Exit Function

        stdBE_Lista = BSO.Consulta("SELECT Descricao FROM " & PRIEmpresaDestino & ".dbo.Armazens  WHERE Armazem= '" & Me.TextEditCodigoArmazem.EditValue & "'")

        If Not stdBE_Lista.Vazia Then
            stdBE_Lista.Inicio()
            Me.TextEditDescricaoArmazem.EditValue = stdBE_Lista.Valor("Descricao")
            ActualizaDescricaoArmazem = True
        Else
            Me.TextEditDescricaoArmazem.EditValue = ""
            ActualizaDescricaoArmazem = False
        End If

    End Function

    Public Function ActualizaDescricaoLocalDescarga() As Boolean

        Dim stdBE_Lista As StdBELista

        If Len(Me.TextEditCodigoLocalDescarga.EditValue) = 0 Then ActualizaDescricaoLocalDescarga = False : Exit Function

        stdBE_Lista = BSO.Consulta(" SELECT * from " & PRIEmpresaDestino & ".dbo.MoradasAlternativasClientes " &
                                                          " WHERE Cliente= '" & Me.TextEditCodigoCliente.EditValue & "'" &
                                                          " AND MoradaAlternativa = '" & Me.TextEditCodigoLocalDescarga.EditValue & "'")

        If Not stdBE_Lista.Vazia Then
            stdBE_Lista.Inicio()
            Me.TextEditDescricaoLocalDescarga.EditValue = stdBE_Lista.Valor("Morada") & " (" & stdBE_Lista.Valor("Localidade") & ")"
            Me.TextEditCodigoMoradaEntrega.EditValue = stdBE_Lista.Valor("Morada")
            Me.TextEditCodigoMoradaEntrega2.EditValue = stdBE_Lista.Valor("Morada2")
            Me.TextEditCodigoLocalidadeEntrega.EditValue = stdBE_Lista.Valor("Localidade")
            Me.TextEditCodigoPostalEntrega.EditValue = stdBE_Lista.Valor("Cp")
            Me.TextEditCodPostalLocalidadeEntrega.EditValue = stdBE_Lista.Valor("CpLocalidade")
            Me.TextEditDistritoEntrega.EditValue = stdBE_Lista.Valor("Distrito")

            ActualizaDescricaoLocalDescarga = True
        Else

            Me.TextEditDescricaoLocalDescarga.EditValue = ""
            Me.TextEditCodigoMoradaEntrega.EditValue = ""
            Me.TextEditCodigoMoradaEntrega2.EditValue = ""
            Me.TextEditCodigoLocalidadeEntrega.EditValue = ""
            Me.TextEditCodigoPostalEntrega.EditValue = ""
            Me.TextEditCodPostalLocalidadeEntrega.EditValue = ""
            Me.TextEditDistritoEntrega.EditValue = ""
            ActualizaDescricaoLocalDescarga = False
        End If

    End Function
    '*ActualizaDescrições*



    'Funções*******************

    'Validações********************
    Private Function Validacoes(ByVal ValidaCodigoCliente As Boolean) As Boolean

        If Len(PRIEmpresaDestino) <= 3 Then
            MsgBox("A empresa de estido não está preenchida", vbCritical, "Atenção")
            Validacoes = False
            Exit Function
        End If

        If ValidaCodigoCliente = True Then
            If Len(Me.TextEditDescricaoCliente.EditValue) = 0 Then
                MsgBox("O Cliente final não está preenchido", vbCritical, "Atenção")
                Validacoes = False
                Exit Function
            End If
        End If

        Validacoes = True

    End Function

End Class