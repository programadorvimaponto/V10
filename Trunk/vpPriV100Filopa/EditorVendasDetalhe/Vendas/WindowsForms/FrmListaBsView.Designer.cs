
namespace EditorVendasDetalhe
{
    partial class FrmListaBsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaBsView));
            this.barManagerListaBs = new DevExpress.XtraBars.BarManager(this.components);
            this.barListaBs = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItemConfirmar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.vmpGridControlListaBs = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridControl();
            this.vmpGridViewListaBs = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridView();
            this.barStaticItemTotalRegistos = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barHeaderItemTotalRegistos = new DevExpress.XtraBars.BarHeaderItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerListaBs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlListaBs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewListaBs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagerListaBs
            // 
            this.barManagerListaBs.AllowMoveBarOnToolbar = false;
            this.barManagerListaBs.AllowQuickCustomization = false;
            this.barManagerListaBs.AllowShowToolbarsPopup = false;
            this.barManagerListaBs.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barListaBs,
            this.bar3});
            this.barManagerListaBs.DockControls.Add(this.barDockControlTop);
            this.barManagerListaBs.DockControls.Add(this.barDockControlBottom);
            this.barManagerListaBs.DockControls.Add(this.barDockControlLeft);
            this.barManagerListaBs.DockControls.Add(this.barDockControlRight);
            this.barManagerListaBs.Form = this;
            this.barManagerListaBs.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemConfirmar,
            this.barButtonItemCancelar,
            this.barStaticItemTotalRegistos,
            this.barEditItem1,
            this.barHeaderItemTotalRegistos});
            this.barManagerListaBs.MaxItemId = 5;
            this.barManagerListaBs.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.barManagerListaBs.StatusBar = this.bar3;
            // 
            // barListaBs
            // 
            this.barListaBs.BarName = "Tools";
            this.barListaBs.DockCol = 0;
            this.barListaBs.DockRow = 0;
            this.barListaBs.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barListaBs.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemConfirmar),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCancelar)});
            this.barListaBs.OptionsBar.DrawDragBorder = false;
            this.barListaBs.OptionsBar.UseWholeRow = true;
            this.barListaBs.Text = "Tools";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barHeaderItemTotalRegistos)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManagerListaBs;
            this.barDockControlTop.Size = new System.Drawing.Size(800, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 425);
            this.barDockControlBottom.Manager = this.barManagerListaBs;
            this.barDockControlBottom.Size = new System.Drawing.Size(800, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Manager = this.barManagerListaBs;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 396);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(800, 29);
            this.barDockControlRight.Manager = this.barManagerListaBs;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 396);
            // 
            // barButtonItemConfirmar
            // 
            this.barButtonItemConfirmar.Caption = "Confirmar";
            this.barButtonItemConfirmar.Id = 0;
            this.barButtonItemConfirmar.Name = "barButtonItemConfirmar";
            this.barButtonItemConfirmar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemConfirmar_ItemClick);
            // 
            // barButtonItemCancelar
            // 
            this.barButtonItemCancelar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemCancelar.Caption = "Cancelar";
            this.barButtonItemCancelar.Id = 1;
            this.barButtonItemCancelar.Name = "barButtonItemCancelar";
            this.barButtonItemCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCancelar_ItemClick);
            // 
            // vmpGridControlListaBs
            // 
            this.vmpGridControlListaBs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmpGridControlListaBs.Location = new System.Drawing.Point(0, 29);
            this.vmpGridControlListaBs.MainView = this.vmpGridViewListaBs;
            this.vmpGridControlListaBs.MenuManager = this.barManagerListaBs;
            this.vmpGridControlListaBs.Name = "vmpGridControlListaBs";
            this.vmpGridControlListaBs.ShowOnlyPredefinedDetails = true;
            this.vmpGridControlListaBs.Size = new System.Drawing.Size(800, 396);
            this.vmpGridControlListaBs.TabIndex = 4;
            this.vmpGridControlListaBs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.vmpGridViewListaBs});
            this.vmpGridControlListaBs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vmpGridControlListaBs_KeyDown);
            // 
            // vmpGridViewListaBs
            // 
            this.vmpGridViewListaBs.CampoChave = null;
            this.vmpGridViewListaBs.CampoValor = null;
            this.vmpGridViewListaBs.ColunasAcumuladas = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("vmpGridViewListaBs.ColunasAcumuladas")));
            this.vmpGridViewListaBs.ColunasInteracao = ((System.Collections.Generic.List<string>)(resources.GetObject("vmpGridViewListaBs.ColunasInteracao")));
            this.vmpGridViewListaBs.ColunasNaoPersonalizaveis = ((System.Collections.Generic.List<string>)(resources.GetObject("vmpGridViewListaBs.ColunasNaoPersonalizaveis")));
            this.vmpGridViewListaBs.ConfiguracaoForm = null;
            this.vmpGridViewListaBs.ConfiguracaoParam = null;
            this.vmpGridViewListaBs.GridControl = this.vmpGridControlListaBs;
            this.vmpGridViewListaBs.Name = "vmpGridViewListaBs";
            this.vmpGridViewListaBs.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            // 
            // barStaticItemTotalRegistos
            // 
            this.barStaticItemTotalRegistos.Caption = "LblTotalRegistos";
            this.barStaticItemTotalRegistos.Id = 2;
            this.barStaticItemTotalRegistos.Name = "barStaticItemTotalRegistos";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "TotalRegistos";
            this.barEditItem1.Edit = this.repositoryItemTextEdit1;
            this.barEditItem1.Id = 3;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // barHeaderItemTotalRegistos
            // 
            this.barHeaderItemTotalRegistos.Caption = "LblText";
            this.barHeaderItemTotalRegistos.Id = 4;
            this.barHeaderItemTotalRegistos.Name = "barHeaderItemTotalRegistos";
            // 
            // FrmListaBsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vmpGridControlListaBs);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmListaBsView";
            this.Size = new System.Drawing.Size(800, 450);
            this.Text = "FrmListaBsView";
            ((System.ComponentModel.ISupportInitialize)(this.barManagerListaBs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlListaBs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewListaBs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerListaBs;
        private DevExpress.XtraBars.Bar barListaBs;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItemConfirmar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancelar;
        private DevExpress.XtraBars.BarStaticItem barStaticItemTotalRegistos;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItemTotalRegistos;
        public Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridControl vmpGridControlListaBs;
        public Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridView vmpGridViewListaBs;
    }
}