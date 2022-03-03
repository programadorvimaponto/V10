
namespace ArmazemEntreposto
{
    partial class FrmAlteraGuiaAEPView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlteraGuiaAEPView));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemGravar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.textEditDocumento = new DevExpress.XtraEditors.TextEdit();
            this.textEditRegime = new DevExpress.XtraEditors.TextEdit();
            this.textEditDespDAU = new DevExpress.XtraEditors.TextEdit();
            this.textEditArm = new DevExpress.XtraEditors.TextEdit();
            this.textEditLote = new DevExpress.XtraEditors.TextEdit();
            this.textEditArtigo = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRegime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDespDAU.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditArm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditArtigo.Properties)).BeginInit();
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
            this.barButtonItemFechar});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "ações";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemGravar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // barButtonItemFechar
            // 
            this.barButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemFechar.Caption = "Fechar";
            this.barButtonItemFechar.Id = 1;
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
            this.barDockControlTop.Size = new System.Drawing.Size(400, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 112);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(400, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 81);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(400, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 81);
            // 
            // textEditDocumento
            // 
            this.textEditDocumento.Location = new System.Drawing.Point(15, 35);
            this.textEditDocumento.MenuManager = this.barManager1;
            this.textEditDocumento.Name = "textEditDocumento";
            this.textEditDocumento.Size = new System.Drawing.Size(152, 20);
            this.textEditDocumento.TabIndex = 4;
            // 
            // textEditRegime
            // 
            this.textEditRegime.Location = new System.Drawing.Point(266, 61);
            this.textEditRegime.MenuManager = this.barManager1;
            this.textEditRegime.Name = "textEditRegime";
            this.textEditRegime.Size = new System.Drawing.Size(124, 20);
            this.textEditRegime.TabIndex = 5;
            // 
            // textEditDespDAU
            // 
            this.textEditDespDAU.Location = new System.Drawing.Point(266, 35);
            this.textEditDespDAU.MenuManager = this.barManager1;
            this.textEditDespDAU.Name = "textEditDespDAU";
            this.textEditDespDAU.Size = new System.Drawing.Size(124, 20);
            this.textEditDespDAU.TabIndex = 6;
            // 
            // textEditArm
            // 
            this.textEditArm.Location = new System.Drawing.Point(121, 87);
            this.textEditArm.MenuManager = this.barManager1;
            this.textEditArm.Name = "textEditArm";
            this.textEditArm.Size = new System.Drawing.Size(46, 20);
            this.textEditArm.TabIndex = 7;
            // 
            // textEditLote
            // 
            this.textEditLote.Location = new System.Drawing.Point(15, 87);
            this.textEditLote.MenuManager = this.barManager1;
            this.textEditLote.Name = "textEditLote";
            this.textEditLote.Size = new System.Drawing.Size(100, 20);
            this.textEditLote.TabIndex = 8;
            // 
            // textEditArtigo
            // 
            this.textEditArtigo.Location = new System.Drawing.Point(15, 61);
            this.textEditArtigo.MenuManager = this.barManager1;
            this.textEditArtigo.Name = "textEditArtigo";
            this.textEditArtigo.Size = new System.Drawing.Size(152, 20);
            this.textEditArtigo.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(208, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "DespDau";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(217, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Regime";
            // 
            // FrmAlteraGuiaAEPView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textEditArtigo);
            this.Controls.Add(this.textEditLote);
            this.Controls.Add(this.textEditArm);
            this.Controls.Add(this.textEditDespDAU);
            this.Controls.Add(this.textEditRegime);
            this.Controls.Add(this.textEditDocumento);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAlteraGuiaAEPView";
            this.Size = new System.Drawing.Size(400, 112);
            this.Text = "Altera Guia AEP";
            this.Load += new System.EventHandler(this.FrmAlteraGuiaAEPView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRegime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDespDAU.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditArm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditArtigo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGravar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFechar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEditArtigo;
        private DevExpress.XtraEditors.TextEdit textEditLote;
        private DevExpress.XtraEditors.TextEdit textEditArm;
        private DevExpress.XtraEditors.TextEdit textEditDespDAU;
        private DevExpress.XtraEditors.TextEdit textEditRegime;
        private DevExpress.XtraEditors.TextEdit textEditDocumento;
    }
}