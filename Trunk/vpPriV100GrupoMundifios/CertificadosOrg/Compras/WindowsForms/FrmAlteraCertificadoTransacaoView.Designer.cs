
namespace CertificadosOrg
{
    partial class FrmAlteraCertificadoTransacaoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlteraCertificadoTransacaoView));
            this.BarManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.Bar1 = new DevExpress.XtraBars.Bar();
            this.BarButtonItemGravar = new DevExpress.XtraBars.BarButtonItem();
            this.BarButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.GroupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.dateEditDataCert = new DevExpress.XtraEditors.DateEdit();
            this.LookUpEditProgramLabel = new DevExpress.XtraEditors.LookUpEdit();
            this.TextEditNumCert = new DevExpress.XtraEditors.TextEdit();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.CheckEditBCI = new DevExpress.XtraEditors.CheckEdit();
            this.GroupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.TextEditArmazem = new DevExpress.XtraEditors.TextEdit();
            this.TextEditlote = new DevExpress.XtraEditors.TextEdit();
            this.TextEditArtigo = new DevExpress.XtraEditors.TextEdit();
            this.TextEditDocumento = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.BarManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl3)).BeginInit();
            this.GroupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDataCert.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDataCert.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LookUpEditProgramLabel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditNumCert.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl2)).BeginInit();
            this.GroupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CheckEditBCI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl1)).BeginInit();
            this.GroupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditArmazem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditlote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditArtigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditDocumento.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BarManager1
            // 
            this.BarManager1.AllowMoveBarOnToolbar = false;
            this.BarManager1.AllowQuickCustomization = false;
            this.BarManager1.AllowShowToolbarsPopup = false;
            this.BarManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.Bar1});
            this.BarManager1.DockControls.Add(this.barDockControlTop);
            this.BarManager1.DockControls.Add(this.barDockControlBottom);
            this.BarManager1.DockControls.Add(this.barDockControlLeft);
            this.BarManager1.DockControls.Add(this.barDockControlRight);
            this.BarManager1.Form = this;
            this.BarManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BarButtonItemGravar,
            this.BarButtonItemFechar});
            this.BarManager1.MaxItemId = 2;
            // 
            // Bar1
            // 
            this.Bar1.BarName = "ações";
            this.Bar1.DockCol = 0;
            this.Bar1.DockRow = 0;
            this.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.Bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BarButtonItemGravar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BarButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.Bar1.OptionsBar.DrawDragBorder = false;
            this.Bar1.OptionsBar.UseWholeRow = true;
            this.Bar1.Text = "ações";
            // 
            // BarButtonItemGravar
            // 
            this.BarButtonItemGravar.Caption = "Gravar";
            this.BarButtonItemGravar.Id = 0;
            this.BarButtonItemGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BarButtonItemGravar.ImageOptions.Image")));
            this.BarButtonItemGravar.Name = "BarButtonItemGravar";
            this.BarButtonItemGravar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemGravar_ItemClick);
            // 
            // BarButtonItemFechar
            // 
            this.BarButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.BarButtonItemFechar.Caption = "Fechar";
            this.BarButtonItemFechar.Id = 1;
            this.BarButtonItemFechar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BarButtonItemFechar.ImageOptions.Image")));
            this.BarButtonItemFechar.Name = "BarButtonItemFechar";
            this.BarButtonItemFechar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemFechar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.BarManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(359, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 297);
            this.barDockControlBottom.Manager = this.BarManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(359, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.BarManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 266);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(359, 31);
            this.barDockControlRight.Manager = this.BarManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 266);
            // 
            // GroupControl3
            // 
            this.GroupControl3.Controls.Add(this.dateEditDataCert);
            this.GroupControl3.Controls.Add(this.LookUpEditProgramLabel);
            this.GroupControl3.Controls.Add(this.TextEditNumCert);
            this.GroupControl3.Controls.Add(this.Label3);
            this.GroupControl3.Controls.Add(this.Label2);
            this.GroupControl3.Controls.Add(this.Label1);
            this.GroupControl3.Location = new System.Drawing.Point(5, 165);
            this.GroupControl3.Name = "GroupControl3";
            this.GroupControl3.Size = new System.Drawing.Size(348, 124);
            this.GroupControl3.TabIndex = 9;
            this.GroupControl3.Text = "Dados Certificado";
            // 
            // dateEditDataCert
            // 
            this.dateEditDataCert.EditValue = null;
            this.dateEditDataCert.Location = new System.Drawing.Point(120, 61);
            this.dateEditDataCert.MenuManager = this.BarManager1;
            this.dateEditDataCert.Name = "dateEditDataCert";
            this.dateEditDataCert.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDataCert.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDataCert.Size = new System.Drawing.Size(223, 20);
            this.dateEditDataCert.TabIndex = 8;
            // 
            // LookUpEditProgramLabel
            // 
            this.LookUpEditProgramLabel.Location = new System.Drawing.Point(120, 97);
            this.LookUpEditProgramLabel.MenuManager = this.BarManager1;
            this.LookUpEditProgramLabel.Name = "LookUpEditProgramLabel";
            this.LookUpEditProgramLabel.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LookUpEditProgramLabel.Properties.Appearance.Options.UseFont = true;
            this.LookUpEditProgramLabel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LookUpEditProgramLabel.Properties.NullText = "";
            this.LookUpEditProgramLabel.Size = new System.Drawing.Size(223, 20);
            this.LookUpEditProgramLabel.TabIndex = 6;
            // 
            // TextEditNumCert
            // 
            this.TextEditNumCert.Location = new System.Drawing.Point(120, 26);
            this.TextEditNumCert.MenuManager = this.BarManager1;
            this.TextEditNumCert.Name = "TextEditNumCert";
            this.TextEditNumCert.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditNumCert.Properties.Appearance.Options.UseFont = true;
            this.TextEditNumCert.Size = new System.Drawing.Size(223, 22);
            this.TextEditNumCert.TabIndex = 4;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(15, 62);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(99, 16);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Data Certificado";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(76, 99);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(38, 16);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Label";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(15, 29);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(99, 16);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Num Certificado";
            // 
            // GroupControl2
            // 
            this.GroupControl2.Controls.Add(this.CheckEditBCI);
            this.GroupControl2.Location = new System.Drawing.Point(237, 38);
            this.GroupControl2.Name = "GroupControl2";
            this.GroupControl2.Size = new System.Drawing.Size(116, 121);
            this.GroupControl2.TabIndex = 8;
            this.GroupControl2.Text = "Outros";
            // 
            // CheckEditBCI
            // 
            this.CheckEditBCI.Location = new System.Drawing.Point(5, 36);
            this.CheckEditBCI.MenuManager = this.BarManager1;
            this.CheckEditBCI.Name = "CheckEditBCI";
            this.CheckEditBCI.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckEditBCI.Properties.Appearance.Options.UseFont = true;
            this.CheckEditBCI.Properties.Caption = "BCI Recebido";
            this.CheckEditBCI.Size = new System.Drawing.Size(100, 20);
            this.CheckEditBCI.TabIndex = 0;
            // 
            // GroupControl1
            // 
            this.GroupControl1.Controls.Add(this.TextEditArmazem);
            this.GroupControl1.Controls.Add(this.TextEditlote);
            this.GroupControl1.Controls.Add(this.TextEditArtigo);
            this.GroupControl1.Controls.Add(this.TextEditDocumento);
            this.GroupControl1.Location = new System.Drawing.Point(5, 38);
            this.GroupControl1.Name = "GroupControl1";
            this.GroupControl1.Size = new System.Drawing.Size(226, 121);
            this.GroupControl1.TabIndex = 7;
            this.GroupControl1.Text = "Encomenda/Artigo";
            // 
            // TextEditArmazem
            // 
            this.TextEditArmazem.Location = new System.Drawing.Point(157, 92);
            this.TextEditArmazem.MenuManager = this.BarManager1;
            this.TextEditArmazem.Name = "TextEditArmazem";
            this.TextEditArmazem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditArmazem.Properties.Appearance.Options.UseFont = true;
            this.TextEditArmazem.Size = new System.Drawing.Size(64, 22);
            this.TextEditArmazem.TabIndex = 3;
            // 
            // TextEditlote
            // 
            this.TextEditlote.Location = new System.Drawing.Point(5, 92);
            this.TextEditlote.MenuManager = this.BarManager1;
            this.TextEditlote.Name = "TextEditlote";
            this.TextEditlote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditlote.Properties.Appearance.Options.UseFont = true;
            this.TextEditlote.Size = new System.Drawing.Size(146, 22);
            this.TextEditlote.TabIndex = 2;
            // 
            // TextEditArtigo
            // 
            this.TextEditArtigo.Location = new System.Drawing.Point(5, 64);
            this.TextEditArtigo.MenuManager = this.BarManager1;
            this.TextEditArtigo.Name = "TextEditArtigo";
            this.TextEditArtigo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditArtigo.Properties.Appearance.Options.UseFont = true;
            this.TextEditArtigo.Size = new System.Drawing.Size(216, 22);
            this.TextEditArtigo.TabIndex = 1;
            // 
            // TextEditDocumento
            // 
            this.TextEditDocumento.Location = new System.Drawing.Point(5, 34);
            this.TextEditDocumento.MenuManager = this.BarManager1;
            this.TextEditDocumento.Name = "TextEditDocumento";
            this.TextEditDocumento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditDocumento.Properties.Appearance.Options.UseFont = true;
            this.TextEditDocumento.Size = new System.Drawing.Size(216, 22);
            this.TextEditDocumento.TabIndex = 0;
            // 
            // FrmAlteraCertificadoTransacaoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupControl3);
            this.Controls.Add(this.GroupControl2);
            this.Controls.Add(this.GroupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAlteraCertificadoTransacaoView";
            this.Size = new System.Drawing.Size(359, 297);
            this.Text = "Altera Certificado Transação Compras";
            this.Load += new System.EventHandler(this.FrmAlteraCertificadoTransacaoView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BarManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl3)).EndInit();
            this.GroupControl3.ResumeLayout(false);
            this.GroupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDataCert.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDataCert.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LookUpEditProgramLabel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditNumCert.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl2)).EndInit();
            this.GroupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CheckEditBCI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl1)).EndInit();
            this.GroupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TextEditArmazem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditlote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditArtigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditDocumento.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevExpress.XtraBars.BarManager BarManager1;
        internal DevExpress.XtraBars.Bar Bar1;
        internal DevExpress.XtraBars.BarButtonItem BarButtonItemGravar;
        internal DevExpress.XtraBars.BarButtonItem BarButtonItemFechar;
        internal DevExpress.XtraBars.BarDockControl barDockControlTop;
        internal DevExpress.XtraBars.BarDockControl barDockControlBottom;
        internal DevExpress.XtraBars.BarDockControl barDockControlLeft;
        internal DevExpress.XtraBars.BarDockControl barDockControlRight;
        internal DevExpress.XtraEditors.GroupControl GroupControl3;
        internal DevExpress.XtraEditors.LookUpEdit LookUpEditProgramLabel;
        internal DevExpress.XtraEditors.TextEdit TextEditNumCert;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal DevExpress.XtraEditors.GroupControl GroupControl2;
        internal DevExpress.XtraEditors.CheckEdit CheckEditBCI;
        internal DevExpress.XtraEditors.GroupControl GroupControl1;
        internal DevExpress.XtraEditors.TextEdit TextEditArmazem;
        internal DevExpress.XtraEditors.TextEdit TextEditlote;
        internal DevExpress.XtraEditors.TextEdit TextEditArtigo;
        internal DevExpress.XtraEditors.TextEdit TextEditDocumento;
        private DevExpress.XtraEditors.DateEdit dateEditDataCert;
    }
}