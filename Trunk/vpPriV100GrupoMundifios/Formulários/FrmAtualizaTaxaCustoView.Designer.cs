
namespace GrupoMundifios.Formulários
{
    partial class FrmAtualizaTaxaCustoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAtualizaTaxaCustoView));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemConfirmar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkEditCustosProcesso = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditTaxaDesperdicio = new DevExpress.XtraEditors.CheckEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCustosProcesso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditTaxaDesperdicio.Properties)).BeginInit();
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
            this.barButtonItemConfirmar,
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemConfirmar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemCancelar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "ações";
            // 
            // barButtonItemConfirmar
            // 
            this.barButtonItemConfirmar.Caption = "Confirmar";
            this.barButtonItemConfirmar.Id = 0;
            this.barButtonItemConfirmar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemConfirmar.ImageOptions.Image")));
            this.barButtonItemConfirmar.Name = "barButtonItemConfirmar";
            this.barButtonItemConfirmar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemConfirmar_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(395, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 170);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(395, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 139);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(395, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 139);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkEditCustosProcesso);
            this.groupControl1.Controls.Add(this.checkEditTaxaDesperdicio);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Location = new System.Drawing.Point(3, 37);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(389, 129);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Atualiza Taxa Desperdício / Custo Processo";
            // 
            // checkEditCustosProcesso
            // 
            this.checkEditCustosProcesso.Location = new System.Drawing.Point(249, 103);
            this.checkEditCustosProcesso.MenuManager = this.barManager1;
            this.checkEditCustosProcesso.Name = "checkEditCustosProcesso";
            this.checkEditCustosProcesso.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEditCustosProcesso.Properties.Appearance.Options.UseFont = true;
            this.checkEditCustosProcesso.Properties.Caption = "Custos do processo";
            this.checkEditCustosProcesso.Size = new System.Drawing.Size(137, 20);
            this.checkEditCustosProcesso.TabIndex = 2;
            // 
            // checkEditTaxaDesperdicio
            // 
            this.checkEditTaxaDesperdicio.Location = new System.Drawing.Point(8, 103);
            this.checkEditTaxaDesperdicio.MenuManager = this.barManager1;
            this.checkEditTaxaDesperdicio.Name = "checkEditTaxaDesperdicio";
            this.checkEditTaxaDesperdicio.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEditTaxaDesperdicio.Properties.Appearance.Options.UseFont = true;
            this.checkEditTaxaDesperdicio.Properties.Caption = "Taxas de desperdício";
            this.checkEditTaxaDesperdicio.Size = new System.Drawing.Size(144, 20);
            this.checkEditTaxaDesperdicio.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(381, 80);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // FrmAtualizaTaxaCustoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmAtualizaTaxaCustoView";
            this.Size = new System.Drawing.Size(395, 170);
            this.Text = "Atualiza Taxa Custo";
            this.Load += new System.EventHandler(this.FrmAtualizaTaxaCustoView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCustosProcesso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditTaxaDesperdicio.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemConfirmar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit checkEditCustosProcesso;
        private DevExpress.XtraEditors.CheckEdit checkEditTaxaDesperdicio;
        private System.Windows.Forms.Label label1;
    }
}