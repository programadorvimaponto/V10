
namespace GrupoMundifios.Formulários
{
    partial class FrmImportaDocCBLView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportaDocCBLView));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label4 = new System.Windows.Forms.Label();
            this.spinEditNumPeriodoFinal = new DevExpress.XtraEditors.SpinEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemImportar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.spinEditNumPeriodoInicial = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditAno = new DevExpress.XtraEditors.SpinEdit();
            this.lookUpEditEmpresaOrigem = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditNumPeriodoFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditNumPeriodoInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditAno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEmpresaOrigem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.spinEditNumPeriodoFinal);
            this.groupControl1.Controls.Add(this.spinEditNumPeriodoInicial);
            this.groupControl1.Controls.Add(this.spinEditAno);
            this.groupControl1.Controls.Add(this.lookUpEditEmpresaOrigem);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Location = new System.Drawing.Point(4, 37);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(245, 113);
            this.groupControl1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "a";
            // 
            // spinEditNumPeriodoFinal
            // 
            this.spinEditNumPeriodoFinal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditNumPeriodoFinal.Location = new System.Drawing.Point(181, 86);
            this.spinEditNumPeriodoFinal.MenuManager = this.barManager1;
            this.spinEditNumPeriodoFinal.Name = "spinEditNumPeriodoFinal";
            this.spinEditNumPeriodoFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditNumPeriodoFinal.Size = new System.Drawing.Size(56, 20);
            this.spinEditNumPeriodoFinal.TabIndex = 6;
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
            this.barButtonItemImportar,
            this.barButtonItemCancelar});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "ações";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemImportar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemCancelar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "ações";
            // 
            // barButtonItemImportar
            // 
            this.barButtonItemImportar.Caption = "Importar";
            this.barButtonItemImportar.Id = 0;
            this.barButtonItemImportar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemImportar.ImageOptions.Image")));
            this.barButtonItemImportar.Name = "barButtonItemImportar";
            this.barButtonItemImportar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemImportar_ItemClick);
            // 
            // barButtonItemCancelar
            // 
            this.barButtonItemCancelar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemCancelar.Caption = "Cancelar";
            this.barButtonItemCancelar.Id = 1;
            this.barButtonItemCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancelar.ImageOptions.Image")));
            this.barButtonItemCancelar.Name = "barButtonItemCancelar";
            this.barButtonItemCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(252, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 154);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(252, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 123);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(252, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 123);
            // 
            // spinEditNumPeriodoInicial
            // 
            this.spinEditNumPeriodoInicial.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditNumPeriodoInicial.Location = new System.Drawing.Point(100, 86);
            this.spinEditNumPeriodoInicial.MenuManager = this.barManager1;
            this.spinEditNumPeriodoInicial.Name = "spinEditNumPeriodoInicial";
            this.spinEditNumPeriodoInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditNumPeriodoInicial.Size = new System.Drawing.Size(56, 20);
            this.spinEditNumPeriodoInicial.TabIndex = 5;
            // 
            // spinEditAno
            // 
            this.spinEditAno.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditAno.Location = new System.Drawing.Point(100, 56);
            this.spinEditAno.MenuManager = this.barManager1;
            this.spinEditAno.Name = "spinEditAno";
            this.spinEditAno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditAno.Size = new System.Drawing.Size(56, 20);
            this.spinEditAno.TabIndex = 4;
            // 
            // lookUpEditEmpresaOrigem
            // 
            this.lookUpEditEmpresaOrigem.Location = new System.Drawing.Point(100, 27);
            this.lookUpEditEmpresaOrigem.MenuManager = this.barManager1;
            this.lookUpEditEmpresaOrigem.Name = "lookUpEditEmpresaOrigem";
            this.lookUpEditEmpresaOrigem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditEmpresaOrigem.Properties.NullText = "";
            this.lookUpEditEmpresaOrigem.Size = new System.Drawing.Size(137, 20);
            this.lookUpEditEmpresaOrigem.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ano:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Período:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empresa Origem:";
            // 
            // FrmImportaDocCBLView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmImportaDocCBLView";
            this.Size = new System.Drawing.Size(252, 154);
            this.Text = "Importa Documentos Contabilidade";
            this.Load += new System.EventHandler(this.FrmImportaDocCBLView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditNumPeriodoFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditNumPeriodoInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditAno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEmpresaOrigem.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImportar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SpinEdit spinEditNumPeriodoFinal;
        private DevExpress.XtraEditors.SpinEdit spinEditNumPeriodoInicial;
        private DevExpress.XtraEditors.SpinEdit spinEditAno;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditEmpresaOrigem;
    }
}