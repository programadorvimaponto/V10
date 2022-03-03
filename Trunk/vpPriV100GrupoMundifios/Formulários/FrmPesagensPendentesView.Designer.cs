
namespace GrupoMundifios.Formulários
{
    partial class FrmPesagensPendentesView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPesagensPendentesView));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemAtualizar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemApagar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.vmpGridControlPesagens = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridControl();
            this.vmpGridViewPesagens = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridView();
            this.dateEditFim = new DevExpress.XtraEditors.DateEdit();
            this.dateEditInicio = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlPesagens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewPesagens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFim.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInicio.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInicio.Properties)).BeginInit();
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
            this.barButtonItemAtualizar,
            this.barButtonItemApagar,
            this.barButtonItemCancelar});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "ações";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemAtualizar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemApagar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemCancelar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "ações";
            // 
            // barButtonItemAtualizar
            // 
            this.barButtonItemAtualizar.Caption = "Atualizar";
            this.barButtonItemAtualizar.Id = 0;
            this.barButtonItemAtualizar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemAtualizar.ImageOptions.Image")));
            this.barButtonItemAtualizar.Name = "barButtonItemAtualizar";
            this.barButtonItemAtualizar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAtualizar_ItemClick);
            // 
            // barButtonItemApagar
            // 
            this.barButtonItemApagar.Caption = "Apagar";
            this.barButtonItemApagar.Id = 1;
            this.barButtonItemApagar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemApagar.ImageOptions.Image")));
            this.barButtonItemApagar.Name = "barButtonItemApagar";
            this.barButtonItemApagar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemApagar_ItemClick);
            // 
            // barButtonItemCancelar
            // 
            this.barButtonItemCancelar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemCancelar.Caption = "Cancelar";
            this.barButtonItemCancelar.Id = 2;
            this.barButtonItemCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancelar.ImageOptions.Image")));
            this.barButtonItemCancelar.Name = "barButtonItemCancelar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1021, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 669);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1021, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 638);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1021, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 638);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.vmpGridControlPesagens);
            this.groupControl1.Controls.Add(this.dateEditFim);
            this.groupControl1.Controls.Add(this.dateEditInicio);
            this.groupControl1.Location = new System.Drawing.Point(3, 35);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1015, 631);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Pesagens Pendentes";
            // 
            // vmpGridControlPesagens
            // 
            this.vmpGridControlPesagens.Location = new System.Drawing.Point(5, 49);
            this.vmpGridControlPesagens.MainView = this.vmpGridViewPesagens;
            this.vmpGridControlPesagens.MenuManager = this.barManager1;
            this.vmpGridControlPesagens.Name = "vmpGridControlPesagens";
            this.vmpGridControlPesagens.ShowOnlyPredefinedDetails = true;
            this.vmpGridControlPesagens.Size = new System.Drawing.Size(1005, 577);
            this.vmpGridControlPesagens.TabIndex = 2;
            this.vmpGridControlPesagens.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.vmpGridViewPesagens});
            // 
            // vmpGridViewPesagens
            // 
            this.vmpGridViewPesagens.CampoChave = null;
            this.vmpGridViewPesagens.CampoValor = null;
            this.vmpGridViewPesagens.ConfiguracaoForm = null;
            this.vmpGridViewPesagens.ConfiguracaoParam = null;
            this.vmpGridViewPesagens.GridControl = this.vmpGridControlPesagens;
            this.vmpGridViewPesagens.Name = "vmpGridViewPesagens";
            this.vmpGridViewPesagens.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.vmpGridViewPesagens.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.vmpGridViewPesagens_FocusedRowChanged);
            // 
            // dateEditFim
            // 
            this.dateEditFim.EditValue = null;
            this.dateEditFim.Location = new System.Drawing.Point(910, 23);
            this.dateEditFim.MenuManager = this.barManager1;
            this.dateEditFim.Name = "dateEditFim";
            this.dateEditFim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFim.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFim.Size = new System.Drawing.Size(100, 20);
            this.dateEditFim.TabIndex = 1;
            // 
            // dateEditInicio
            // 
            this.dateEditInicio.EditValue = null;
            this.dateEditInicio.Location = new System.Drawing.Point(5, 23);
            this.dateEditInicio.MenuManager = this.barManager1;
            this.dateEditInicio.Name = "dateEditInicio";
            this.dateEditInicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditInicio.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditInicio.Size = new System.Drawing.Size(100, 20);
            this.dateEditInicio.TabIndex = 0;
            // 
            // FrmPesagensPendentesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmPesagensPendentesView";
            this.Size = new System.Drawing.Size(1021, 669);
            this.Text = "Pesagens Pendentes";
            this.Load += new System.EventHandler(this.FrmPesagensPendentesView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlPesagens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewPesagens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFim.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInicio.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInicio.Properties)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem barButtonItemAtualizar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemApagar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancelar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.DateEdit dateEditFim;
        private DevExpress.XtraEditors.DateEdit dateEditInicio;
        private Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridControl vmpGridControlPesagens;
        private Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridView vmpGridViewPesagens;
    }
}