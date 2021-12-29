
namespace Inditex
{
    partial class FrmInditex
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInditex));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemGravar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCopiaInformacao = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textEditFornecedor = new DevExpress.XtraEditors.TextEdit();
            this.textEditNIdentificacao = new DevExpress.XtraEditors.TextEdit();
            this.lookUpEditTipoIdentificacao = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditPreAuditado = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditClassificacao = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditAuditado = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditAprovado = new DevExpress.XtraEditors.LookUpEdit();
            this.dateEditFiacoes = new DevExpress.XtraEditors.DateEdit();
            this.memoEditCpLoc = new DevExpress.XtraEditors.MemoEdit();
            this.memoEditFiacoesObs = new DevExpress.XtraEditors.MemoEdit();
            this.textEditCodigoFornecedor = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFornecedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNIdentificacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTipoIdentificacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditPreAuditado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditClassificacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAuditado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAprovado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFiacoes.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFiacoes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditCpLoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditFiacoesObs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCodigoFornecedor.Properties)).BeginInit();
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
            this.barButtonItemCopiaInformacao,
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemCopiaInformacao, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // 
            // barButtonItemCopiaInformacao
            // 
            this.barButtonItemCopiaInformacao.Caption = "Copiar Informação";
            this.barButtonItemCopiaInformacao.Id = 1;
            this.barButtonItemCopiaInformacao.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemCopiaInformacao.ImageOptions.Image")));
            this.barButtonItemCopiaInformacao.Name = "barButtonItemCopiaInformacao";
            // 
            // barButtonItemFechar
            // 
            this.barButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemFechar.Caption = "Fechar";
            this.barButtonItemFechar.Id = 2;
            this.barButtonItemFechar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemFechar.ImageOptions.Image")));
            this.barButtonItemFechar.Name = "barButtonItemFechar";
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
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 377);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(521, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 346);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(521, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 346);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Fornecedor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "NºIdentificação";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tipo Identificação";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pré-Auditado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Classificação";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Auditado";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(376, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Aprovado";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Data Auditoria";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Observações";
            // 
            // textEditFornecedor
            // 
            this.textEditFornecedor.Location = new System.Drawing.Point(218, 43);
            this.textEditFornecedor.MenuManager = this.barManager1;
            this.textEditFornecedor.Name = "textEditFornecedor";
            this.textEditFornecedor.Size = new System.Drawing.Size(297, 20);
            this.textEditFornecedor.TabIndex = 13;
            // 
            // textEditNIdentificacao
            // 
            this.textEditNIdentificacao.Location = new System.Drawing.Point(91, 75);
            this.textEditNIdentificacao.MenuManager = this.barManager1;
            this.textEditNIdentificacao.Name = "textEditNIdentificacao";
            this.textEditNIdentificacao.Size = new System.Drawing.Size(112, 20);
            this.textEditNIdentificacao.TabIndex = 14;
            // 
            // lookUpEditTipoIdentificacao
            // 
            this.lookUpEditTipoIdentificacao.Location = new System.Drawing.Point(313, 75);
            this.lookUpEditTipoIdentificacao.MenuManager = this.barManager1;
            this.lookUpEditTipoIdentificacao.Name = "lookUpEditTipoIdentificacao";
            this.lookUpEditTipoIdentificacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTipoIdentificacao.Properties.NullText = "";
            this.lookUpEditTipoIdentificacao.Size = new System.Drawing.Size(202, 20);
            this.lookUpEditTipoIdentificacao.TabIndex = 15;
            // 
            // lookUpEditPreAuditado
            // 
            this.lookUpEditPreAuditado.Location = new System.Drawing.Point(91, 111);
            this.lookUpEditPreAuditado.MenuManager = this.barManager1;
            this.lookUpEditPreAuditado.Name = "lookUpEditPreAuditado";
            this.lookUpEditPreAuditado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditPreAuditado.Properties.NullText = "";
            this.lookUpEditPreAuditado.Size = new System.Drawing.Size(112, 20);
            this.lookUpEditPreAuditado.TabIndex = 16;
            // 
            // lookUpEditClassificacao
            // 
            this.lookUpEditClassificacao.Location = new System.Drawing.Point(91, 146);
            this.lookUpEditClassificacao.MenuManager = this.barManager1;
            this.lookUpEditClassificacao.Name = "lookUpEditClassificacao";
            this.lookUpEditClassificacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditClassificacao.Properties.NullText = "";
            this.lookUpEditClassificacao.Size = new System.Drawing.Size(112, 20);
            this.lookUpEditClassificacao.TabIndex = 17;
            // 
            // lookUpEditAuditado
            // 
            this.lookUpEditAuditado.Location = new System.Drawing.Point(270, 111);
            this.lookUpEditAuditado.MenuManager = this.barManager1;
            this.lookUpEditAuditado.Name = "lookUpEditAuditado";
            this.lookUpEditAuditado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAuditado.Properties.NullText = "";
            this.lookUpEditAuditado.Size = new System.Drawing.Size(100, 20);
            this.lookUpEditAuditado.TabIndex = 18;
            // 
            // lookUpEditAprovado
            // 
            this.lookUpEditAprovado.Location = new System.Drawing.Point(435, 111);
            this.lookUpEditAprovado.MenuManager = this.barManager1;
            this.lookUpEditAprovado.Name = "lookUpEditAprovado";
            this.lookUpEditAprovado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAprovado.Properties.NullText = "";
            this.lookUpEditAprovado.Size = new System.Drawing.Size(80, 20);
            this.lookUpEditAprovado.TabIndex = 19;
            // 
            // dateEditFiacoes
            // 
            this.dateEditFiacoes.EditValue = null;
            this.dateEditFiacoes.Location = new System.Drawing.Point(295, 146);
            this.dateEditFiacoes.MenuManager = this.barManager1;
            this.dateEditFiacoes.Name = "dateEditFiacoes";
            this.dateEditFiacoes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFiacoes.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFiacoes.Size = new System.Drawing.Size(100, 20);
            this.dateEditFiacoes.TabIndex = 20;
            // 
            // memoEditCpLoc
            // 
            this.memoEditCpLoc.Location = new System.Drawing.Point(408, 163);
            this.memoEditCpLoc.MenuManager = this.barManager1;
            this.memoEditCpLoc.Name = "memoEditCpLoc";
            this.memoEditCpLoc.Size = new System.Drawing.Size(107, 33);
            this.memoEditCpLoc.TabIndex = 21;
            // 
            // memoEditFiacoesObs
            // 
            this.memoEditFiacoesObs.Location = new System.Drawing.Point(8, 223);
            this.memoEditFiacoesObs.MenuManager = this.barManager1;
            this.memoEditFiacoesObs.Name = "memoEditFiacoesObs";
            this.memoEditFiacoesObs.Size = new System.Drawing.Size(507, 151);
            this.memoEditFiacoesObs.TabIndex = 22;
            // 
            // textEditCodigoFornecedor
            // 
            this.textEditCodigoFornecedor.Location = new System.Drawing.Point(91, 43);
            this.textEditCodigoFornecedor.MenuManager = this.barManager1;
            this.textEditCodigoFornecedor.Name = "textEditCodigoFornecedor";
            this.textEditCodigoFornecedor.Size = new System.Drawing.Size(112, 20);
            this.textEditCodigoFornecedor.TabIndex = 27;
            // 
            // FrmInditex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textEditCodigoFornecedor);
            this.Controls.Add(this.memoEditFiacoesObs);
            this.Controls.Add(this.memoEditCpLoc);
            this.Controls.Add(this.dateEditFiacoes);
            this.Controls.Add(this.lookUpEditAprovado);
            this.Controls.Add(this.lookUpEditAuditado);
            this.Controls.Add(this.lookUpEditClassificacao);
            this.Controls.Add(this.lookUpEditPreAuditado);
            this.Controls.Add(this.lookUpEditTipoIdentificacao);
            this.Controls.Add(this.textEditNIdentificacao);
            this.Controls.Add(this.textEditFornecedor);
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
            this.Name = "FrmInditex";
            this.Size = new System.Drawing.Size(521, 377);
            this.Text = "FrmInditex";
            this.Load += new System.EventHandler(this.FrmInditex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFornecedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNIdentificacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTipoIdentificacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditPreAuditado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditClassificacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAuditado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAprovado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFiacoes.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFiacoes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditCpLoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditFiacoesObs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCodigoFornecedor.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGravar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCopiaInformacao;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFechar;
        private DevExpress.XtraEditors.MemoEdit memoEditFiacoesObs;
        private DevExpress.XtraEditors.MemoEdit memoEditCpLoc;
        private DevExpress.XtraEditors.DateEdit dateEditFiacoes;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditAprovado;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditAuditado;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditClassificacao;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditPreAuditado;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditTipoIdentificacao;
        private DevExpress.XtraEditors.TextEdit textEditNIdentificacao;
        private DevExpress.XtraEditors.TextEdit textEditFornecedor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private PriTextBoxF4100.PriTextBoxF4 TxtCodigoCliente;
        private DevExpress.XtraEditors.TextEdit textEditCodigoFornecedor;
    }
}