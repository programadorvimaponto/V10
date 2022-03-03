
namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    partial class frmListaPropriedadesMundifiosView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaPropriedadesMundifiosView));
            this.barManagerListaPropriedadesMundifios = new DevExpress.XtraBars.BarManager(this.components);
            this.barTopoListaPropriedadesMundifios = new DevExpress.XtraBars.Bar();
            this.barButtonItemConfirmar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.vmpGridControlListaPropriedadesMundifios = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridControl();
            this.vmpGridViewListaPropriedadesMundifios = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridView();
            this.dsPropriedadesMundifios = new Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource.DsPropriedadesMundifios();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerListaPropriedadesMundifios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlListaPropriedadesMundifios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewListaPropriedadesMundifios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPropriedadesMundifios)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagerListaPropriedadesMundifios
            // 
            this.barManagerListaPropriedadesMundifios.AllowMoveBarOnToolbar = false;
            this.barManagerListaPropriedadesMundifios.AllowQuickCustomization = false;
            this.barManagerListaPropriedadesMundifios.AllowShowToolbarsPopup = false;
            this.barManagerListaPropriedadesMundifios.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTopoListaPropriedadesMundifios});
            this.barManagerListaPropriedadesMundifios.DockControls.Add(this.barDockControlTop);
            this.barManagerListaPropriedadesMundifios.DockControls.Add(this.barDockControlBottom);
            this.barManagerListaPropriedadesMundifios.DockControls.Add(this.barDockControlLeft);
            this.barManagerListaPropriedadesMundifios.DockControls.Add(this.barDockControlRight);
            this.barManagerListaPropriedadesMundifios.Form = this;
            this.barManagerListaPropriedadesMundifios.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemConfirmar,
            this.barButtonItemFechar});
            this.barManagerListaPropriedadesMundifios.MaxItemId = 2;
            // 
            // barTopoListaPropriedadesMundifios
            // 
            this.barTopoListaPropriedadesMundifios.BarName = "Tools";
            this.barTopoListaPropriedadesMundifios.DockCol = 0;
            this.barTopoListaPropriedadesMundifios.DockRow = 0;
            this.barTopoListaPropriedadesMundifios.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTopoListaPropriedadesMundifios.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemConfirmar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTopoListaPropriedadesMundifios.OptionsBar.DrawDragBorder = false;
            this.barTopoListaPropriedadesMundifios.OptionsBar.UseWholeRow = true;
            this.barTopoListaPropriedadesMundifios.Text = "Tools";
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
            this.barButtonItemFechar.Caption = "Cancelar";
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
            this.barDockControlTop.Manager = this.barManagerListaPropriedadesMundifios;
            this.barDockControlTop.Size = new System.Drawing.Size(800, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 450);
            this.barDockControlBottom.Manager = this.barManagerListaPropriedadesMundifios;
            this.barDockControlBottom.Size = new System.Drawing.Size(800, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManagerListaPropriedadesMundifios;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 419);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(800, 31);
            this.barDockControlRight.Manager = this.barManagerListaPropriedadesMundifios;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 419);
            // 
            // vmpGridControlListaPropriedadesMundifios
            // 
            this.vmpGridControlListaPropriedadesMundifios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmpGridControlListaPropriedadesMundifios.Location = new System.Drawing.Point(0, 31);
            this.vmpGridControlListaPropriedadesMundifios.MainView = this.vmpGridViewListaPropriedadesMundifios;
            this.vmpGridControlListaPropriedadesMundifios.MenuManager = this.barManagerListaPropriedadesMundifios;
            this.vmpGridControlListaPropriedadesMundifios.Name = "vmpGridControlListaPropriedadesMundifios";
            this.vmpGridControlListaPropriedadesMundifios.ShowOnlyPredefinedDetails = true;
            this.vmpGridControlListaPropriedadesMundifios.Size = new System.Drawing.Size(800, 419);
            this.vmpGridControlListaPropriedadesMundifios.TabIndex = 4;
            this.vmpGridControlListaPropriedadesMundifios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.vmpGridViewListaPropriedadesMundifios});
            // 
            // vmpGridViewListaPropriedadesMundifios
            // 
            this.vmpGridViewListaPropriedadesMundifios.CampoChave = null;
            this.vmpGridViewListaPropriedadesMundifios.CampoValor = null;
            this.vmpGridViewListaPropriedadesMundifios.ConfiguracaoForm = null;
            this.vmpGridViewListaPropriedadesMundifios.ConfiguracaoParam = null;
            this.vmpGridViewListaPropriedadesMundifios.GridControl = this.vmpGridControlListaPropriedadesMundifios;
            this.vmpGridViewListaPropriedadesMundifios.Name = "vmpGridViewListaPropriedadesMundifios";
            this.vmpGridViewListaPropriedadesMundifios.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            // 
            // dsPropriedadesMundifios
            // 
            this.dsPropriedadesMundifios.DataSetName = "DsPropriedadesMundifios";
            this.dsPropriedadesMundifios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmListaPropriedadesMundifiosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vmpGridControlListaPropriedadesMundifios);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmListaPropriedadesMundifiosView";
            this.Size = new System.Drawing.Size(800, 450);
            this.Text = "Lista Propriedades Mundifios";
            ((System.ComponentModel.ISupportInitialize)(this.barManagerListaPropriedadesMundifios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlListaPropriedadesMundifios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewListaPropriedadesMundifios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPropriedadesMundifios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerListaPropriedadesMundifios;
        private DevExpress.XtraBars.Bar barTopoListaPropriedadesMundifios;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItemConfirmar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFechar;
        private Componentes.Sdk.Controlos.VmpGrid.VmpGridControl vmpGridControlListaPropriedadesMundifios;
        private Componentes.Sdk.Controlos.VmpGrid.VmpGridView vmpGridViewListaPropriedadesMundifios;
        private DataSource.DsPropriedadesMundifios dsPropriedadesMundifios;
    }
}