
namespace Filopa
{
    partial class FrmAlteraCertificadoTransacaoFilopaView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlteraCertificadoTransacaoFilopaView));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemAplicar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemClear = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.Label1 = new System.Windows.Forms.Label();
            this.CheckEditCertificadoEmitido = new DevExpress.XtraEditors.CheckEdit();
            this.TextEditCancelado = new DevExpress.XtraEditors.TextEdit();
            this.TextEditArtigo = new DevExpress.XtraEditors.TextEdit();
            this.TextEditDescricao = new DevExpress.XtraEditors.TextEdit();
            this.TextEditDocumento = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckEditCertificadoEmitido.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditCancelado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditArtigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditDocumento.Properties)).BeginInit();
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
            this.barButtonItemAplicar,
            this.barButtonItemClear,
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemAplicar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemClear, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "ações";
            // 
            // barButtonItemAplicar
            // 
            this.barButtonItemAplicar.Caption = "Aplicar";
            this.barButtonItemAplicar.Id = 0;
            this.barButtonItemAplicar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemAplicar.ImageOptions.Image")));
            this.barButtonItemAplicar.Name = "barButtonItemAplicar";
            this.barButtonItemAplicar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAplicar_ItemClick);
            // 
            // barButtonItemClear
            // 
            this.barButtonItemClear.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemClear.Caption = "Clear";
            this.barButtonItemClear.Id = 1;
            this.barButtonItemClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemClear.ImageOptions.Image")));
            this.barButtonItemClear.Name = "barButtonItemClear";
            this.barButtonItemClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemClear_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(534, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 155);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(534, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 124);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(534, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 124);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(17, 125);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(132, 16);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "Certificado Cancelado";
            // 
            // CheckEditCertificadoEmitido
            // 
            this.CheckEditCertificadoEmitido.Location = new System.Drawing.Point(261, 42);
            this.CheckEditCertificadoEmitido.MenuManager = this.barManager1;
            this.CheckEditCertificadoEmitido.Name = "CheckEditCertificadoEmitido";
            this.CheckEditCertificadoEmitido.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckEditCertificadoEmitido.Properties.Appearance.Options.UseFont = true;
            this.CheckEditCertificadoEmitido.Properties.Caption = "Certificado";
            this.CheckEditCertificadoEmitido.Size = new System.Drawing.Size(103, 20);
            this.CheckEditCertificadoEmitido.TabIndex = 14;
            // 
            // TextEditCancelado
            // 
            this.TextEditCancelado.Location = new System.Drawing.Point(155, 122);
            this.TextEditCancelado.MenuManager = this.barManager1;
            this.TextEditCancelado.Name = "TextEditCancelado";
            this.TextEditCancelado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditCancelado.Properties.Appearance.Options.UseFont = true;
            this.TextEditCancelado.Size = new System.Drawing.Size(368, 22);
            this.TextEditCancelado.TabIndex = 13;
            // 
            // TextEditArtigo
            // 
            this.TextEditArtigo.Location = new System.Drawing.Point(11, 81);
            this.TextEditArtigo.MenuManager = this.barManager1;
            this.TextEditArtigo.Name = "TextEditArtigo";
            this.TextEditArtigo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditArtigo.Properties.Appearance.Options.UseFont = true;
            this.TextEditArtigo.Size = new System.Drawing.Size(138, 22);
            this.TextEditArtigo.TabIndex = 12;
            // 
            // TextEditDescricao
            // 
            this.TextEditDescricao.Location = new System.Drawing.Point(155, 81);
            this.TextEditDescricao.MenuManager = this.barManager1;
            this.TextEditDescricao.Name = "TextEditDescricao";
            this.TextEditDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditDescricao.Properties.Appearance.Options.UseFont = true;
            this.TextEditDescricao.Size = new System.Drawing.Size(368, 22);
            this.TextEditDescricao.TabIndex = 11;
            // 
            // TextEditDocumento
            // 
            this.TextEditDocumento.Location = new System.Drawing.Point(11, 41);
            this.TextEditDocumento.MenuManager = this.barManager1;
            this.TextEditDocumento.Name = "TextEditDocumento";
            this.TextEditDocumento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditDocumento.Properties.Appearance.Options.UseFont = true;
            this.TextEditDocumento.Size = new System.Drawing.Size(244, 22);
            this.TextEditDocumento.TabIndex = 10;
            // 
            // FrmAlteraCertificadoTransacaoFilopaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.CheckEditCertificadoEmitido);
            this.Controls.Add(this.TextEditCancelado);
            this.Controls.Add(this.TextEditArtigo);
            this.Controls.Add(this.TextEditDescricao);
            this.Controls.Add(this.TextEditDocumento);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAlteraCertificadoTransacaoFilopaView";
            this.Size = new System.Drawing.Size(534, 155);
            this.Text = "Altera Certificado Transação";
            this.Load += new System.EventHandler(this.FrmAlteraCertificadoTransacaoFilopaView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckEditCertificadoEmitido.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditCancelado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditArtigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditDocumento.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAplicar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemClear;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFechar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        internal System.Windows.Forms.Label Label1;
        internal DevExpress.XtraEditors.CheckEdit CheckEditCertificadoEmitido;
        internal DevExpress.XtraEditors.TextEdit TextEditCancelado;
        internal DevExpress.XtraEditors.TextEdit TextEditArtigo;
        internal DevExpress.XtraEditors.TextEdit TextEditDescricao;
        internal DevExpress.XtraEditors.TextEdit TextEditDocumento;
    }
}