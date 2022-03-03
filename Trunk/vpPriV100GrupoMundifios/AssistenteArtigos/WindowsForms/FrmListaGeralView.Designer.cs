
namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    partial class frmListaGeral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaGeral));
            this.barManagerListaGeral = new DevExpress.XtraBars.BarManager();
            this.barTopoListaGeral = new DevExpress.XtraBars.Bar();
            this.barButtonItemConfirmar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItemCount = new DevExpress.XtraBars.BarStaticItem();
            this.vmpGridControlListaGeral = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridControl();
            this.vmpGridViewListaGeral = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridView();
            this.panelBaixoListaGeral = new System.Windows.Forms.Panel();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelClienteBloqueado = new System.Windows.Forms.Label();
            this.labelLegClienteBloqueado = new System.Windows.Forms.Label();
            this.labelArtigoDesabilitado = new System.Windows.Forms.Label();
            this.labelLegArtigoDescontinuado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerListaGeral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlListaGeral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewListaGeral)).BeginInit();
            this.panelBaixoListaGeral.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManagerListaGeral
            // 
            this.barManagerListaGeral.AllowMoveBarOnToolbar = false;
            this.barManagerListaGeral.AllowQuickCustomization = false;
            this.barManagerListaGeral.AllowShowToolbarsPopup = false;
            this.barManagerListaGeral.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTopoListaGeral});
            this.barManagerListaGeral.DockControls.Add(this.barDockControlTop);
            this.barManagerListaGeral.DockControls.Add(this.barDockControlBottom);
            this.barManagerListaGeral.DockControls.Add(this.barDockControlLeft);
            this.barManagerListaGeral.DockControls.Add(this.barDockControlRight);
            this.barManagerListaGeral.Form = this;
            this.barManagerListaGeral.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemConfirmar,
            this.barButtonItemFechar,
            this.barStaticItemCount});
            this.barManagerListaGeral.MaxItemId = 3;
            // 
            // barTopoListaGeral
            // 
            this.barTopoListaGeral.BarName = "Tools";
            this.barTopoListaGeral.DockCol = 0;
            this.barTopoListaGeral.DockRow = 0;
            this.barTopoListaGeral.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTopoListaGeral.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemConfirmar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTopoListaGeral.OptionsBar.DrawDragBorder = false;
            this.barTopoListaGeral.OptionsBar.UseWholeRow = true;
            this.barTopoListaGeral.Text = "Tools";
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
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManagerListaGeral;
            this.barDockControlTop.Size = new System.Drawing.Size(1282, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 531);
            this.barDockControlBottom.Manager = this.barManagerListaGeral;
            this.barDockControlBottom.Size = new System.Drawing.Size(1282, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManagerListaGeral;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 500);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1282, 31);
            this.barDockControlRight.Manager = this.barManagerListaGeral;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 500);
            // 
            // barStaticItemCount
            // 
            this.barStaticItemCount.Caption = "Count";
            this.barStaticItemCount.Id = 2;
            this.barStaticItemCount.Name = "barStaticItemCount";
            // 
            // vmpGridControlListaGeral
            // 
            this.vmpGridControlListaGeral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmpGridControlListaGeral.Location = new System.Drawing.Point(0, 31);
            this.vmpGridControlListaGeral.MainView = this.vmpGridViewListaGeral;
            this.vmpGridControlListaGeral.MenuManager = this.barManagerListaGeral;
            this.vmpGridControlListaGeral.Name = "vmpGridControlListaGeral";
            this.vmpGridControlListaGeral.ShowOnlyPredefinedDetails = true;
            this.vmpGridControlListaGeral.Size = new System.Drawing.Size(1282, 500);
            this.vmpGridControlListaGeral.TabIndex = 4;
            this.vmpGridControlListaGeral.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.vmpGridViewListaGeral});
            // 
            // vmpGridViewListaGeral
            // 
            this.vmpGridViewListaGeral.CampoChave = null;
            this.vmpGridViewListaGeral.CampoValor = null;
            this.vmpGridViewListaGeral.ColunasAcumuladas = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("vmpGridViewListaGeral.ColunasAcumuladas")));
            this.vmpGridViewListaGeral.ColunasInteracao = ((System.Collections.Generic.List<string>)(resources.GetObject("vmpGridViewListaGeral.ColunasInteracao")));
            this.vmpGridViewListaGeral.ColunasNaoPersonalizaveis = ((System.Collections.Generic.List<string>)(resources.GetObject("vmpGridViewListaGeral.ColunasNaoPersonalizaveis")));
            this.vmpGridViewListaGeral.ConfiguracaoForm = null;
            this.vmpGridViewListaGeral.ConfiguracaoParam = null;
            this.vmpGridViewListaGeral.GridControl = this.vmpGridControlListaGeral;
            this.vmpGridViewListaGeral.Name = "vmpGridViewListaGeral";
            this.vmpGridViewListaGeral.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            // 
            // panelBaixoListaGeral
            // 
            this.panelBaixoListaGeral.Controls.Add(this.labelCount);
            this.panelBaixoListaGeral.Controls.Add(this.labelClienteBloqueado);
            this.panelBaixoListaGeral.Controls.Add(this.labelLegClienteBloqueado);
            this.panelBaixoListaGeral.Controls.Add(this.labelArtigoDesabilitado);
            this.panelBaixoListaGeral.Controls.Add(this.labelLegArtigoDescontinuado);
            this.panelBaixoListaGeral.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBaixoListaGeral.Location = new System.Drawing.Point(0, 502);
            this.panelBaixoListaGeral.Name = "panelBaixoListaGeral";
            this.panelBaixoListaGeral.Size = new System.Drawing.Size(1282, 29);
            this.panelBaixoListaGeral.TabIndex = 9;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(3, 8);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(39, 13);
            this.labelCount.TabIndex = 36;
            this.labelCount.Text = "Label1";
            // 
            // labelClienteBloqueado
            // 
            this.labelClienteBloqueado.AutoSize = true;
            this.labelClienteBloqueado.Location = new System.Drawing.Point(817, 8);
            this.labelClienteBloqueado.Name = "labelClienteBloqueado";
            this.labelClienteBloqueado.Size = new System.Drawing.Size(94, 13);
            this.labelClienteBloqueado.TabIndex = 35;
            this.labelClienteBloqueado.Text = "Estado Bloqueado";
            this.labelClienteBloqueado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelClienteBloqueado.Visible = false;
            // 
            // labelLegClienteBloqueado
            // 
            this.labelLegClienteBloqueado.BackColor = System.Drawing.Color.SandyBrown;
            this.labelLegClienteBloqueado.Location = new System.Drawing.Point(796, 7);
            this.labelLegClienteBloqueado.Name = "labelLegClienteBloqueado";
            this.labelLegClienteBloqueado.Size = new System.Drawing.Size(15, 15);
            this.labelLegClienteBloqueado.TabIndex = 34;
            this.labelLegClienteBloqueado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelLegClienteBloqueado.Visible = false;
            // 
            // labelArtigoDesabilitado
            // 
            this.labelArtigoDesabilitado.AutoSize = true;
            this.labelArtigoDesabilitado.Location = new System.Drawing.Point(953, 7);
            this.labelArtigoDesabilitado.Name = "labelArtigoDesabilitado";
            this.labelArtigoDesabilitado.Size = new System.Drawing.Size(109, 13);
            this.labelArtigoDesabilitado.TabIndex = 33;
            this.labelArtigoDesabilitado.Text = "Artigo Descontinuado";
            this.labelArtigoDesabilitado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelArtigoDesabilitado.Visible = false;
            // 
            // labelLegArtigoDescontinuado
            // 
            this.labelLegArtigoDescontinuado.BackColor = System.Drawing.Color.OrangeRed;
            this.labelLegArtigoDescontinuado.Location = new System.Drawing.Point(932, 6);
            this.labelLegArtigoDescontinuado.Name = "labelLegArtigoDescontinuado";
            this.labelLegArtigoDescontinuado.Size = new System.Drawing.Size(15, 15);
            this.labelLegArtigoDescontinuado.TabIndex = 32;
            this.labelLegArtigoDescontinuado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelLegArtigoDescontinuado.Visible = false;
            // 
            // frmListaGeral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBaixoListaGeral);
            this.Controls.Add(this.vmpGridControlListaGeral);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmListaGeral";
            this.Size = new System.Drawing.Size(1282, 531);
            this.Text = "Lista Geral";
            this.Load += new System.EventHandler(this.FrmListaGeral_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerListaGeral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlListaGeral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridViewListaGeral)).EndInit();
            this.panelBaixoListaGeral.ResumeLayout(false);
            this.panelBaixoListaGeral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerListaGeral;
        private DevExpress.XtraBars.Bar barTopoListaGeral;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItemConfirmar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFechar;
        private Componentes.Sdk.Controlos.VmpGrid.VmpGridControl vmpGridControlListaGeral;
        private Componentes.Sdk.Controlos.VmpGrid.VmpGridView vmpGridViewListaGeral;
        private DevExpress.XtraBars.BarStaticItem barStaticItemCount;
        private System.Windows.Forms.Panel panelBaixoListaGeral;
        internal System.Windows.Forms.Label labelCount;
        internal System.Windows.Forms.Label labelClienteBloqueado;
        internal System.Windows.Forms.Label labelLegClienteBloqueado;
        internal System.Windows.Forms.Label labelArtigoDesabilitado;
        internal System.Windows.Forms.Label labelLegArtigoDescontinuado;
    }
}