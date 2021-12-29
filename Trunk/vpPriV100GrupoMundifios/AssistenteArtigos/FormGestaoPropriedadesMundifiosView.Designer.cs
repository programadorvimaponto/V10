
namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    partial class FormGestaoPropriedadesMundifiosView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGestaoPropriedadesMundifiosView));
            this.barManagerGestaoPropriedadeMundifios = new DevExpress.XtraBars.BarManager(this.components);
            this.barestaoPropriedadeMundifios = new DevExpress.XtraBars.Bar();
            this.barButtonItemGravar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItemPropriedades = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.vmpGridControlGestaoPropriedadesMundifios = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridControl();
            this.vmpGridView1 = new Vimaponto.Componentes.Sdk.Controlos.VmpGrid.VmpGridView();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerGestaoPropriedadeMundifios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlGestaoPropriedadesMundifios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagerGestaoPropriedadeMundifios
            // 
            this.barManagerGestaoPropriedadeMundifios.AllowMoveBarOnToolbar = false;
            this.barManagerGestaoPropriedadeMundifios.AllowQuickCustomization = false;
            this.barManagerGestaoPropriedadeMundifios.AllowShowToolbarsPopup = false;
            this.barManagerGestaoPropriedadeMundifios.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barestaoPropriedadeMundifios,
            this.bar3});
            this.barManagerGestaoPropriedadeMundifios.DockControls.Add(this.barDockControlTop);
            this.barManagerGestaoPropriedadeMundifios.DockControls.Add(this.barDockControlBottom);
            this.barManagerGestaoPropriedadeMundifios.DockControls.Add(this.barDockControlLeft);
            this.barManagerGestaoPropriedadeMundifios.DockControls.Add(this.barDockControlRight);
            this.barManagerGestaoPropriedadeMundifios.Form = this;
            this.barManagerGestaoPropriedadeMundifios.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemGravar,
            this.barButtonItemFechar,
            this.barEditItemPropriedades,
            this.barStaticItem1});
            this.barManagerGestaoPropriedadeMundifios.MaxItemId = 4;
            this.barManagerGestaoPropriedadeMundifios.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
            this.barManagerGestaoPropriedadeMundifios.StatusBar = this.bar3;
            // 
            // barestaoPropriedadeMundifios
            // 
            this.barestaoPropriedadeMundifios.BarName = "Tools";
            this.barestaoPropriedadeMundifios.DockCol = 0;
            this.barestaoPropriedadeMundifios.DockRow = 0;
            this.barestaoPropriedadeMundifios.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barestaoPropriedadeMundifios.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemGravar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemFechar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItemPropriedades)});
            this.barestaoPropriedadeMundifios.OptionsBar.DrawDragBorder = false;
            this.barestaoPropriedadeMundifios.OptionsBar.UseWholeRow = true;
            this.barestaoPropriedadeMundifios.Text = "Tools";
            // 
            // barButtonItemGravar
            // 
            this.barButtonItemGravar.Caption = "Gravar";
            this.barButtonItemGravar.Id = 0;
            this.barButtonItemGravar.Name = "barButtonItemGravar";
            // 
            // barButtonItemFechar
            // 
            this.barButtonItemFechar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemFechar.Caption = "Fechar";
            this.barButtonItemFechar.Id = 1;
            this.barButtonItemFechar.Name = "barButtonItemFechar";
            // 
            // barEditItemPropriedades
            // 
            this.barEditItemPropriedades.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barEditItemPropriedades.Caption = "Propriedades";
            this.barEditItemPropriedades.Edit = this.repositoryItemComboBox1;
            this.barEditItemPropriedades.Id = 2;
            this.barEditItemPropriedades.Name = "barEditItemPropriedades";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Utilizadador";
            this.barStaticItem1.Id = 3;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManagerGestaoPropriedadeMundifios;
            this.barDockControlTop.Size = new System.Drawing.Size(800, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 425);
            this.barDockControlBottom.Manager = this.barManagerGestaoPropriedadeMundifios;
            this.barDockControlBottom.Size = new System.Drawing.Size(800, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Manager = this.barManagerGestaoPropriedadeMundifios;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 396);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(800, 29);
            this.barDockControlRight.Manager = this.barManagerGestaoPropriedadeMundifios;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 396);
            // 
            // vmpGridControlGestaoPropriedadesMundifios
            // 
            this.vmpGridControlGestaoPropriedadesMundifios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmpGridControlGestaoPropriedadesMundifios.Location = new System.Drawing.Point(0, 29);
            this.vmpGridControlGestaoPropriedadesMundifios.MainView = this.vmpGridView1;
            this.vmpGridControlGestaoPropriedadesMundifios.MenuManager = this.barManagerGestaoPropriedadeMundifios;
            this.vmpGridControlGestaoPropriedadesMundifios.Name = "vmpGridControlGestaoPropriedadesMundifios";
            this.vmpGridControlGestaoPropriedadesMundifios.ShowOnlyPredefinedDetails = true;
            this.vmpGridControlGestaoPropriedadesMundifios.Size = new System.Drawing.Size(800, 396);
            this.vmpGridControlGestaoPropriedadesMundifios.TabIndex = 4;
            this.vmpGridControlGestaoPropriedadesMundifios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.vmpGridView1});
            // 
            // vmpGridView1
            // 
            this.vmpGridView1.CampoChave = null;
            this.vmpGridView1.CampoValor = null;
            this.vmpGridView1.ColunasAcumuladas = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("vmpGridView1.ColunasAcumuladas")));
            this.vmpGridView1.ColunasInteracao = ((System.Collections.Generic.List<string>)(resources.GetObject("vmpGridView1.ColunasInteracao")));
            this.vmpGridView1.ColunasNaoPersonalizaveis = ((System.Collections.Generic.List<string>)(resources.GetObject("vmpGridView1.ColunasNaoPersonalizaveis")));
            this.vmpGridView1.ConfiguracaoForm = null;
            this.vmpGridView1.ConfiguracaoParam = null;
            this.vmpGridView1.GridControl = this.vmpGridControlGestaoPropriedadesMundifios;
            this.vmpGridView1.Name = "vmpGridView1";
            this.vmpGridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            // 
            // FormGestaoPropriedadesMundifiosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vmpGridControlGestaoPropriedadesMundifios);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FormGestaoPropriedadesMundifiosView";
            this.Size = new System.Drawing.Size(800, 450);
            this.Text = "FormGestaoPropriedadesMundifiosView";
            ((System.ComponentModel.ISupportInitialize)(this.barManagerGestaoPropriedadeMundifios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridControlGestaoPropriedadesMundifios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmpGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerGestaoPropriedadeMundifios;
        private DevExpress.XtraBars.Bar barestaoPropriedadeMundifios;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGravar;
        private Componentes.Sdk.Controlos.VmpGrid.VmpGridControl vmpGridControlGestaoPropriedadesMundifios;
        private Componentes.Sdk.Controlos.VmpGrid.VmpGridView vmpGridView1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFechar;
        private DevExpress.XtraBars.BarEditItem barEditItemPropriedades;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
    }
}