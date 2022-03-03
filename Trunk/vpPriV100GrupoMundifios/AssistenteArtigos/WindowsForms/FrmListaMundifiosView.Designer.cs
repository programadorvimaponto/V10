
namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    partial class frmListaMundifiosView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaMundifiosView));
            this.barManagerListaMundifios = new DevExpress.XtraBars.BarManager(this.components);
            this.barTopoListaMundifios = new DevExpress.XtraBars.Bar();
            this.barButtonItemConfirmar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barBaixoListaMundifios = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.vmpGridControlListaMundifios = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridControl();
            this.vmpGridViewListaMundifios = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridView();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerListaMundifios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlListaMundifios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewListaMundifios)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagerListaMundifios
            // 
            this.barManagerListaMundifios.AllowMoveBarOnToolbar = false;
            this.barManagerListaMundifios.AllowQuickCustomization = false;
            this.barManagerListaMundifios.AllowShowToolbarsPopup = false;
            this.barManagerListaMundifios.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTopoListaMundifios,
            this.barBaixoListaMundifios});
            this.barManagerListaMundifios.DockControls.Add(this.barDockControlTop);
            this.barManagerListaMundifios.DockControls.Add(this.barDockControlBottom);
            this.barManagerListaMundifios.DockControls.Add(this.barDockControlLeft);
            this.barManagerListaMundifios.DockControls.Add(this.barDockControlRight);
            this.barManagerListaMundifios.Form = this;
            this.barManagerListaMundifios.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemConfirmar,
            this.barButtonItemFechar});
            this.barManagerListaMundifios.MaxItemId = 2;
            this.barManagerListaMundifios.StatusBar = this.barBaixoListaMundifios;
            // 
            // barTopoListaMundifios
            // 
            this.barTopoListaMundifios.BarName = "Tools";
            this.barTopoListaMundifios.DockCol = 0;
            this.barTopoListaMundifios.DockRow = 0;
            this.barTopoListaMundifios.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTopoListaMundifios.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemConfirmar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTopoListaMundifios.OptionsBar.DrawDragBorder = false;
            this.barTopoListaMundifios.OptionsBar.UseWholeRow = true;
            this.barTopoListaMundifios.Text = "Tools";
            // 
            // barButtonItemConfirmar
            // 
            this.barButtonItemConfirmar.Caption = "Confirmar";
            this.barButtonItemConfirmar.Id = 0;
            this.barButtonItemConfirmar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemConfirmar.ImageOptions.Image")));
            this.barButtonItemConfirmar.Name = "barButtonItemConfirmar";
            this.barButtonItemConfirmar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemConfirmar_ItemClick);
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
            // barBaixoListaMundifios
            // 
            this.barBaixoListaMundifios.BarName = "Status bar";
            this.barBaixoListaMundifios.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barBaixoListaMundifios.DockCol = 0;
            this.barBaixoListaMundifios.DockRow = 0;
            this.barBaixoListaMundifios.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barBaixoListaMundifios.OptionsBar.AllowQuickCustomization = false;
            this.barBaixoListaMundifios.OptionsBar.DrawDragBorder = false;
            this.barBaixoListaMundifios.OptionsBar.UseWholeRow = true;
            this.barBaixoListaMundifios.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManagerListaMundifios;
            this.barDockControlTop.Size = new System.Drawing.Size(800, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 427);
            this.barDockControlBottom.Manager = this.barManagerListaMundifios;
            this.barDockControlBottom.Size = new System.Drawing.Size(800, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManagerListaMundifios;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 396);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(800, 31);
            this.barDockControlRight.Manager = this.barManagerListaMundifios;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 396);
            // 
            // vmpGridControlListaMundifios
            // 
            this.vmpGridControlListaMundifios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmpGridControlListaMundifios.Location = new System.Drawing.Point(0, 31);
            this.vmpGridControlListaMundifios.MainView = this.vmpGridViewListaMundifios;
            this.vmpGridControlListaMundifios.MenuManager = this.barManagerListaMundifios;
            this.vmpGridControlListaMundifios.Name = "vmpGridControlListaMundifios";
            this.vmpGridControlListaMundifios.ShowOnlyPredefinedDetails = true;
            this.vmpGridControlListaMundifios.Size = new System.Drawing.Size(800, 396);
            this.vmpGridControlListaMundifios.TabIndex = 4;
            this.vmpGridControlListaMundifios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.vmpGridViewListaMundifios});
            // 
            // vmpGridViewListaMundifios
            // 
            this.vmpGridViewListaMundifios.CampoChave = null;
            this.vmpGridViewListaMundifios.CampoValor = null;
            this.vmpGridViewListaMundifios.ConfiguracaoForm = null;
            this.vmpGridViewListaMundifios.ConfiguracaoParam = null;
            this.vmpGridViewListaMundifios.GridControl = this.vmpGridControlListaMundifios;
            this.vmpGridViewListaMundifios.Name = "vmpGridViewListaMundifios";
            this.vmpGridViewListaMundifios.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.vmpGridViewListaMundifios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vmpGridViewListaMundifios_KeyDown);
            this.vmpGridViewListaMundifios.DoubleClick += new System.EventHandler(this.vmpGridViewListaMundifios_DoubleClick);
            // 
            // frmListaMundifiosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vmpGridControlListaMundifios);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmListaMundifiosView";
            this.Size = new System.Drawing.Size(800, 450);
            this.Text = "Lista Mundifios";
            ((System.ComponentModel.ISupportInitialize)(this.barManagerListaMundifios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlListaMundifios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewListaMundifios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerListaMundifios;
        private DevExpress.XtraBars.Bar barTopoListaMundifios;
        private DevExpress.XtraBars.BarButtonItem barButtonItemConfirmar;
        private DevExpress.XtraBars.Bar barBaixoListaMundifios;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private Componentes.Sdk.Controlos.VmpGrid.VmpGridControl vmpGridControlListaMundifios;
        private Componentes.Sdk.Controlos.VmpGrid.VmpGridView vmpGridViewListaMundifios;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFechar;
    }
}