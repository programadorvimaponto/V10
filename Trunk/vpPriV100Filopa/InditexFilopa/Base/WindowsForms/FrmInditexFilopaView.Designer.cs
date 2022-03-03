
namespace InditexFilopa
{
    partial class FrmInditexFilopaView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInditexFilopaView));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemGravar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCopiarInformacao = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.TextEditPais = new DevExpress.XtraEditors.TextEdit();
            this.TextEditFacMor = new DevExpress.XtraEditors.TextEdit();
            this.TextEditFacLocal = new DevExpress.XtraEditors.TextEdit();
            this.memoEditFiacoesObs = new DevExpress.XtraEditors.MemoEdit();
            this.dateEditFiacoes = new DevExpress.XtraEditors.DateEdit();
            this.lookUpEditAprovado = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditAuditado = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditClassificacao = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditPreAuditado = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditTipoIdentificacao = new DevExpress.XtraEditors.LookUpEdit();
            this.textEditNIdentificacao = new DevExpress.XtraEditors.TextEdit();
            this.textEditCliente = new DevExpress.XtraEditors.TextEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textEditCodigoCliente = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditPais.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditFacMor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditFacLocal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditFiacoesObs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFiacoes.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFiacoes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAprovado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAuditado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditClassificacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditPreAuditado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTipoIdentificacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNIdentificacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCodigoCliente.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemGravar,
            this.barButtonItemCopiarInformacao,
            this.barButtonItemFechar});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "ações";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemGravar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemCopiarInformacao, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "ações";
            // 
            // barButtonItemGravar
            // 
            this.barButtonItemGravar.Caption = "Gravar";
            this.barButtonItemGravar.Id = 0;
            this.barButtonItemGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemGravar.ImageOptions.Image")));
            this.barButtonItemGravar.Name = "barButtonItemGravar";
            this.barButtonItemGravar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGravar_ItemClick);
            // 
            // barButtonItemCopiarInformacao
            // 
            this.barButtonItemCopiarInformacao.Caption = "Copiar Informação";
            this.barButtonItemCopiarInformacao.Id = 1;
            this.barButtonItemCopiarInformacao.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemCopiarInformacao.ImageOptions.Image")));
            this.barButtonItemCopiarInformacao.Name = "barButtonItemCopiarInformacao";
            this.barButtonItemCopiarInformacao.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCopiarInformacao_ItemClick);
            // 
            // barButtonItemFechar
            // 
            this.barButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemFechar.Caption = "Fechar";
            this.barButtonItemFechar.Id = 2;
            this.barButtonItemFechar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemFechar.ImageOptions.Image")));
            this.barButtonItemFechar.Name = "barButtonItemFechar";
            this.barButtonItemFechar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemFechar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(521, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 353);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(521, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 322);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(521, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 322);
            // 
            // TextEditPais
            // 
            this.TextEditPais.Location = new System.Drawing.Point(436, 172);
            this.TextEditPais.MenuManager = this.barManager1;
            this.TextEditPais.Name = "TextEditPais";
            this.TextEditPais.Size = new System.Drawing.Size(80, 20);
            this.TextEditPais.TabIndex = 84;
            this.TextEditPais.Visible = false;
            // 
            // TextEditFacMor
            // 
            this.TextEditFacMor.Location = new System.Drawing.Point(436, 172);
            this.TextEditFacMor.MenuManager = this.barManager1;
            this.TextEditFacMor.Name = "TextEditFacMor";
            this.TextEditFacMor.Size = new System.Drawing.Size(80, 20);
            this.TextEditFacMor.TabIndex = 83;
            this.TextEditFacMor.Visible = false;
            // 
            // TextEditFacLocal
            // 
            this.TextEditFacLocal.Location = new System.Drawing.Point(436, 172);
            this.TextEditFacLocal.MenuManager = this.barManager1;
            this.TextEditFacLocal.Name = "TextEditFacLocal";
            this.TextEditFacLocal.Size = new System.Drawing.Size(80, 20);
            this.TextEditFacLocal.TabIndex = 82;
            this.TextEditFacLocal.Visible = false;
            // 
            // memoEditFiacoesObs
            // 
            this.memoEditFiacoesObs.Location = new System.Drawing.Point(9, 198);
            this.memoEditFiacoesObs.MenuManager = this.barManager1;
            this.memoEditFiacoesObs.Name = "memoEditFiacoesObs";
            this.memoEditFiacoesObs.Size = new System.Drawing.Size(507, 151);
            this.memoEditFiacoesObs.TabIndex = 81;
            // 
            // dateEditFiacoes
            // 
            this.dateEditFiacoes.EditValue = null;
            this.dateEditFiacoes.Location = new System.Drawing.Point(296, 142);
            this.dateEditFiacoes.MenuManager = this.barManager1;
            this.dateEditFiacoes.Name = "dateEditFiacoes";
            this.dateEditFiacoes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFiacoes.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFiacoes.Size = new System.Drawing.Size(100, 20);
            this.dateEditFiacoes.TabIndex = 80;
            // 
            // lookUpEditAprovado
            // 
            this.lookUpEditAprovado.Location = new System.Drawing.Point(436, 107);
            this.lookUpEditAprovado.MenuManager = this.barManager1;
            this.lookUpEditAprovado.Name = "lookUpEditAprovado";
            this.lookUpEditAprovado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAprovado.Properties.NullText = "";
            this.lookUpEditAprovado.Size = new System.Drawing.Size(80, 20);
            this.lookUpEditAprovado.TabIndex = 79;
            // 
            // lookUpEditAuditado
            // 
            this.lookUpEditAuditado.Location = new System.Drawing.Point(271, 107);
            this.lookUpEditAuditado.MenuManager = this.barManager1;
            this.lookUpEditAuditado.Name = "lookUpEditAuditado";
            this.lookUpEditAuditado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAuditado.Properties.NullText = "";
            this.lookUpEditAuditado.Size = new System.Drawing.Size(100, 20);
            this.lookUpEditAuditado.TabIndex = 78;
            // 
            // lookUpEditClassificacao
            // 
            this.lookUpEditClassificacao.Location = new System.Drawing.Point(92, 142);
            this.lookUpEditClassificacao.MenuManager = this.barManager1;
            this.lookUpEditClassificacao.Name = "lookUpEditClassificacao";
            this.lookUpEditClassificacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditClassificacao.Properties.NullText = "";
            this.lookUpEditClassificacao.Size = new System.Drawing.Size(112, 20);
            this.lookUpEditClassificacao.TabIndex = 77;
            // 
            // lookUpEditPreAuditado
            // 
            this.lookUpEditPreAuditado.Location = new System.Drawing.Point(92, 107);
            this.lookUpEditPreAuditado.MenuManager = this.barManager1;
            this.lookUpEditPreAuditado.Name = "lookUpEditPreAuditado";
            this.lookUpEditPreAuditado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditPreAuditado.Properties.NullText = "";
            this.lookUpEditPreAuditado.Size = new System.Drawing.Size(112, 20);
            this.lookUpEditPreAuditado.TabIndex = 76;
            // 
            // lookUpEditTipoIdentificacao
            // 
            this.lookUpEditTipoIdentificacao.Location = new System.Drawing.Point(314, 71);
            this.lookUpEditTipoIdentificacao.MenuManager = this.barManager1;
            this.lookUpEditTipoIdentificacao.Name = "lookUpEditTipoIdentificacao";
            this.lookUpEditTipoIdentificacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTipoIdentificacao.Properties.NullText = "";
            this.lookUpEditTipoIdentificacao.Size = new System.Drawing.Size(202, 20);
            this.lookUpEditTipoIdentificacao.TabIndex = 75;
            // 
            // textEditNIdentificacao
            // 
            this.textEditNIdentificacao.Location = new System.Drawing.Point(92, 71);
            this.textEditNIdentificacao.MenuManager = this.barManager1;
            this.textEditNIdentificacao.Name = "textEditNIdentificacao";
            this.textEditNIdentificacao.Size = new System.Drawing.Size(112, 20);
            this.textEditNIdentificacao.TabIndex = 74;
            // 
            // textEditCliente
            // 
            this.textEditCliente.Location = new System.Drawing.Point(219, 39);
            this.textEditCliente.MenuManager = this.barManager1;
            this.textEditCliente.Name = "textEditCliente";
            this.textEditCliente.Size = new System.Drawing.Size(297, 20);
            this.textEditCliente.TabIndex = 73;
            this.textEditCliente.EditValueChanged += new System.EventHandler(this.textEditCliente_EditValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 179);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 72;
            this.label9.Text = "Observações";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(216, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 71;
            this.label8.Text = "Data Auditoria";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(377, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "Aprovado";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(216, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "Auditado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 68;
            this.label5.Text = "Classificação";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 67;
            this.label4.Text = "Pré-Auditado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "Tipo Identificação";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "NºIdentificação";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Cliente";
            // 
            // textEditCodigoCliente
            // 
            this.textEditCodigoCliente.Location = new System.Drawing.Point(92, 39);
            this.textEditCodigoCliente.MenuManager = this.barManager1;
            this.textEditCodigoCliente.Name = "textEditCodigoCliente";
            this.textEditCodigoCliente.Size = new System.Drawing.Size(112, 20);
            this.textEditCodigoCliente.TabIndex = 89;
            this.textEditCodigoCliente.EditValueChanged += new System.EventHandler(this.textEditCodigoCliente_EditValueChanged);
            this.textEditCodigoCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditCodigoCliente_KeyDown);
            // 
            // FrmInditexFilopaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textEditCodigoCliente);
            this.Controls.Add(this.TextEditPais);
            this.Controls.Add(this.TextEditFacMor);
            this.Controls.Add(this.TextEditFacLocal);
            this.Controls.Add(this.memoEditFiacoesObs);
            this.Controls.Add(this.dateEditFiacoes);
            this.Controls.Add(this.lookUpEditAprovado);
            this.Controls.Add(this.lookUpEditAuditado);
            this.Controls.Add(this.lookUpEditClassificacao);
            this.Controls.Add(this.lookUpEditPreAuditado);
            this.Controls.Add(this.lookUpEditTipoIdentificacao);
            this.Controls.Add(this.textEditNIdentificacao);
            this.Controls.Add(this.textEditCliente);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInditexFilopaView";
            this.Size = new System.Drawing.Size(521, 353);
            this.Text = "Inditex Filopa";
            this.Load += new System.EventHandler(this.FrmInditexFilopaView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditPais.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditFacMor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditFacLocal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditFiacoesObs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFiacoes.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFiacoes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAprovado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAuditado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditClassificacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditPreAuditado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTipoIdentificacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNIdentificacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCodigoCliente.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGravar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCopiarInformacao;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFechar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        internal DevExpress.XtraEditors.TextEdit TextEditPais;
        internal DevExpress.XtraEditors.TextEdit TextEditFacMor;
        internal DevExpress.XtraEditors.TextEdit TextEditFacLocal;
        private DevExpress.XtraEditors.MemoEdit memoEditFiacoesObs;
        private DevExpress.XtraEditors.DateEdit dateEditFiacoes;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditAprovado;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditAuditado;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditClassificacao;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditPreAuditado;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditTipoIdentificacao;
        private DevExpress.XtraEditors.TextEdit textEditNIdentificacao;
        private DevExpress.XtraEditors.TextEdit textEditCliente;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEditCodigoCliente;
    }
}