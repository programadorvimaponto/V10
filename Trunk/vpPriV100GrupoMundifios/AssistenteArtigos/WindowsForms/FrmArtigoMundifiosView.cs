using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Properties;
using static Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class.Enums;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    public partial class frmArtigoMundifiosView : CustomForm
    {

        #region Variaveis
        public frmGestaoPropriedadesMundifiosView frmArtigo { get; set; } = new frmGestaoPropriedadesMundifiosView();

        private int NrDigitosCodigoArtigo { get; set; } = 9;

        private bool isNew = true;
        // Private isF4 As Boolean = False

        private bool LoadCompleto; // Serve porque o inicialize estava a invocar a função  "Rb_CheckedChanged" que por sua vez chama a "FormatarGrelhaNE(RbNe.Checked)" e dava erro porque o form ainda não está inicializado

        private Color CorSelecao = Color.Green;
        private Color CorCaixasTexto = Color.Red;

        private Grupo _CurrentGrupo = Grupo.Filamentos;

        public frmArtigoMundifiosView()
        {
            InitializeComponent();
        }
        public enum Grupo
        {
            Nenhum,
            Fios,
            Filamentos,
            Mechas
        }
        // Const Nm As Double = 0.59

        // Public Const EmpresaExclusica As String = ""

        public Grupo CurrentGrupo
        {
            get
            {
                return _CurrentGrupo;
            }
            set
            {
                if (value != CurrentGrupo)
                {

                    // LimparTudo()

                    switch (value)
                    {
                        case Grupo.Fios:
                            {
                                this.xtraTabPageCaracteristicas.Text = "Fios";
                                this.groupControlNE.Visible = true;
                                LimparNE(true);
                                this.groupControlTipos.Visible = true;
                                LimparTipo(true);
                                this.groupControlTorcao2.Visible = true;
                                LimparTorcao2(true);
                                this.groupControlCategoria.Visible = false;
                                LimparCategoria(true);
                                this.groupControlDimensao.Visible = false;
                                LimparDimensao(true);
                                this.groupControlTexturizacao.Visible = false;
                                LimparTexturizacao(true);
                                this.groupControlCone.Visible = true;
                                LimparCone(true);
                                this.groupControlPrograma.Visible = true;
                                LimparPrograma(true);
                                break;
                            }

                        case Grupo.Filamentos:
                            {
                                this.xtraTabPageCaracteristicas.Text = "Filamentos";
                                this.groupControlNE.Visible = false;
                                LimparNE(true);
                                this.groupControlTipos.Visible = false;
                                LimparTipo(true);
                                this.groupControlTorcao2.Visible = false;
                                LimparTorcao2(true);
                                this.groupControlCategoria.Visible = true;
                                LimparCategoria(true);
                                this.groupControlDimensao.Visible = true;
                                LimparDimensao(true);
                                this.groupControlTexturizacao.Visible = true;
                                LimparTexturizacao(true);
                                this.groupControlCone.Visible = false;
                                LimparCone(true);
                                break;
                            }
                    }
                    CalcularTotais();
                }
                _CurrentGrupo = value;
            }
        }

        #endregion

        #region Load
        private void FrmArtigoMundifiosView_Load(object sender, EventArgs e)
        {

            try

            {
                //this.Icon = My.Resources.logo_plan_icon;
                //Settings.Default.ConnectionStringPrimavera = gConnection;
                //Settings.Default.PRIEMPREConnectionString = gConnectionEMPRE;
                //Settings.Default.PRIMUNDIFIOSConnectionString = gConnectionMUNDIFIOS;
                //Settings.Default.Save();

                if (!ValidarPermissoes())
                    return;

                CurrentGrupo = Grupo.Fios;

                dsEmpresasGrupo.GetEmpresasPermitidas();

                CarregarDropDownTipoComponente();

                InicializarForulario();


                this.labelEmpresaGrupo.Text = string.Empty;
                // Carregar as empresas de grupo para a combo box!
                foreach (EmpresaGrupo item in VariaveisGlobais.gLstEmpresasGrupo)
                    this.labelEmpresaGrupo.Text = this.labelEmpresaGrupo.Text + item.Empresa.ToString() + " / ";

                this.labelEmpresaGrupo.Text = Strings.Left(this.labelEmpresaGrupo.Text, Strings.Len(this.labelEmpresaGrupo.Text) - 2);

                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                this.barStaticItemVersao.Caption = "v."+version;
                // ano mes dia horaminuto sequencial, para se saber quantas se geraram ao todo

                // Versão  1: "v.17.0119.1210.001" -> versão inicial
                // Versão  2: "v.17.0131.1400.002" -> adicionado formulário de gestão de propriedades
                // Versão  3: "v.17.0206.0006.003" -> adicionado novo campo cdu_codigoAntigoMundifios
                // Versão  4: "v.17.0207.1646.004" -> alteração no fill da tabela de artigos. na yarntrade nao tinha os mesmos campos
                // Versão  5: "v.17.0209.1206.005" -> quando verificava se existia outro artigo com as mesmas propriedaeds nao estava a ter em conta a referencia.
                // -> estava a gerar mal a descrição extra porque o campo ordenação não estava preenchdio
                // -> retirar espaço no inicio e fim da descrição e descrição extra
                // Versão  6: "v.17.0216.1441.006" -> Quando verifica se ja existe algum artigo com as propriedades iguais, passar a verificar apenas para artigos não anulados.
                // Versão  7: "v.17.0313.1432.007  -> Gestão de utilizadores
                // Versão  8: "v.17.0315.1836.008  -> Acrescentadas validações à anulação do artigo
                // Versão  9: "v.17.0421.1433.009  -> Compilada nova versão porque cliente estava com erro ao abrir formulário.
                // Versão 10: "v.17.0512.1830.010  -> Proposta P.170424C (Dados Auxiliares)
                // Versão 11: "v.17.0605.1223.011  -> Adicionado Utilizador ADMIN para conseguir criar artigos nas 4 empresas do Grupo. O outro manteve-se por causa do registo das propriedades e gestão de bloqueios.
                // Versão 12: "v.17.0606.1248.012  -> Gestão de propriedades/atributos: Não permitir alterar o código da referencia gerada incremental
                // Versão 12: "v.17.0707.0020.013  -> Gestão de propriedades/atributos: Não estava a assumir a connection da mundifios, quando a empresa era a INOVAFIL
                // Versão 13: "v.17.0920.1800.014  -> Atualização da Data de Criação. Criação do campo CDU_DescricaoExtraExterna. Alteração da forma como a descrição Extra era construída.
                // Versão 14: "v.17.1121.1340.015  -> A descrição Extra Interna não estava a respeitar a ordenação dos Tipos e características
                // Versão 15: "v.17.1122.1146.016  -> Sempre que se altera o pisco da descrição extra, a descrição extra interna não é alterada.
                // Versão 16: "v.18.0625.1615.017  -> Permitir que a Filopa faça parte do leque de empresas a criar artigos pelo gerados de Artigos.
                // Versão 17: "v.18.0124.1615.018  -> Adicionado o campo CDU_DescricaoInterna ao FrmArtigoMundifios.

                LoadCompleto = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
            }
        }
        #endregion

        #region Formatações
        private void AtivarDesaticarCheckBoxDescExtra(bool Estado)
        {
            checkEditDescricaoExtraTipos.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraTorcao1.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraTorcao2.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditReferenciasLivres.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCaracteristicas.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCone.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraPrograma.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCategoria.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraDimensao.CheckedChanged -= cbDescExtra_CheckedChanged;

            this.checkEditDescricaoExtraTipos.Checked = Estado;
            this.checkEditDescricaoExtraTorcao1.Checked = Estado;
            this.checkEditDescricaoExtraTorcao2.Checked = Estado;
            this.checkEditDescricaoExtraReferencias.Checked = Estado;
            this.checkEditDescricaoExtraCaracteristicas.Checked = Estado;
            this.checkEditDescricaoExtraCone.Checked = Estado;
            this.checkEditDescricaoExtraPrograma.Checked = Estado;
            this.checkEditDescricaoExtraCategoria.Checked = Estado;
            this.checkEditDescricaoExtraDimensao.Checked = Estado;

            checkEditDescricaoExtraTipos.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraTorcao1.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraTorcao2.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditReferenciasLivres.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCaracteristicas.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCone.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraPrograma.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCategoria.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraDimensao.CheckedChanged += cbDescExtra_CheckedChanged;
        }

        private void AtivarCheckBoxAtualizaDescExtra()
        {
            dsArtigosMundifios.VMP_ART_Tipo.AtivarCheckBoxAtualizaDescExtra();
            dsArtigosMundifios.VMP_ART_Caracteristica.AtivarCheckBoxAtualizaDescExtra();
        }

        private void FormatarGrelhas()
        {
            FormatarGrelhaFamilias();
            FormatarGrelhaSubFamilias();
            FormatarGrelhaComponentes();
            FormatarGrelhaNE(true);
            FormatarGrelhaTipos();
            FormatarGrelhaTorcao1();
            FormatarGrelhaTorcao2();
            FormatarGrelhaReferencias();
            FormatarGrelhaCone();
            FormatarGrelhaCaracteristica();
            FormatarGrelhaPrograma();
            FormatarGrelhaDimensao();
            FormatarGrelhaTexturizacao();
            FormatarGrelhaCategoria();
            FormatarGrelhaDadosAuxiliares();
        }

        private void FormatarGrelhaFamilias()
        {
            try
            {


                vmpGridViewFamilias.IniciarFormatacao(true);

                vmpGridViewFamilias.FormatarColuna("CDU_DescricaoAbrev", false, Editavel: true);

                vmpGridViewFamilias.FormatarColuna("Sel", false, Editavel: true, LarguraFixa: 30);

                vmpGridViewFamilias.FormatarColuna("Familia", Visivel: true, Caption: "Código", LarguraFixa: 70);

                vmpGridViewFamilias.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                vmpGridViewFamilias.FormatarColuna("CDU_Grupo", Visivel: false);

                vmpGridViewFamilias.AdicionarRegraFormato("SEL", vmpGridViewFamilias.Columns["Sel"], CorSelecao, "Sel = True", true);

                vmpGridViewFamilias.AutoFillColumn = vmpGridViewFamilias.Columns["Descricao"];

                vmpGridViewFamilias.FinalizarFormatacao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaSubFamilias()
        {
            try
            {

                vmpGridViewSubFamilias.IniciarFormatacao(true);

                vmpGridViewSubFamilias.FormatarColuna("Familia", false, Caption: "Familia");

                vmpGridViewSubFamilias.FormatarColuna("Sel", false, Editavel: true);

                vmpGridViewSubFamilias.FormatarColuna("SubFamilia", Visivel: true, Caption: "Código", LarguraFixa: 70);

                vmpGridViewSubFamilias.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                vmpGridViewSubFamilias.AutoFillColumn = vmpGridViewFamilias.Columns["Descricao"];

                vmpGridViewSubFamilias.FinalizarFormatacao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaComponentes()
        {
            try
            {
                vmpGridViewComponentes.IniciarFormatacao(true);

                vmpGridViewComponentes.FormatarColuna("Componente", Visivel: true, LarguraFixa: 70);

                vmpGridViewComponentes.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                vmpGridViewComponentes.FormatarColuna("Percentagem", Visivel: true, Editavel: true, Caption: "%", LarguraFixa: 60);

                vmpGridViewComponentes.OptionsView.ShowFooter = true;

                vmpGridViewComponentes.FormatarColuna("Id", Visivel: false);

                vmpGridViewComponentes.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewComponentes.FormatarColuna("Ordem", Visivel: false);

                vmpGridViewComponentes.FormatarColuna("Codigo", Visivel: false, Caption: "Código", LarguraFixa: 40);

                vmpGridViewComponentes.AutoFillColumn = vmpGridViewFamilias.Columns["Descricao"];

                vmpGridViewComponentes.FinalizarFormatacao();

                dsArtigosMundifios.VMP_ART_Componente[0].Sel = true; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaNE(bool NE)
        {
            try
            {

                vmpGridViewNE.IniciarFormatacao(true);

                vmpGridViewNE.FormatarColuna("Id", Visivel: false);

                vmpGridViewNE.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewNE.FormatarColuna("DataIntroducao", Visivel: false);

                vmpGridViewNE.FormatarColuna("DataAtualizacao", Visivel: false);

                vmpGridViewNE.FormatarColuna("Posto", Visivel: false);

                vmpGridViewNE.FormatarColuna("Utilizador", Visivel: false);

                vmpGridViewNE.FormatarColuna("PostoAtualizacao", Visivel: false);

                vmpGridViewNE.FormatarColuna("UtilizadorAtualizacao", Visivel: false);

                vmpGridViewNE.FormatarColuna("Codigo", Visivel: false, Caption: "Código", LarguraFixa: 35);

                vmpGridViewNE.FormatarColuna("Descricao", Caption: "Descrição Ne", LarguraFixa: 30);

                vmpGridViewNE.FormatarColuna("DescricaoNm", Caption: "Descrição Nm", LarguraFixa: 30);

                vmpGridViewNE.FormatarColuna("NE", Visivel: true, LarguraFixa: 30);

                vmpGridViewNE.FormatarColuna("Cabos", Visivel: true, LarguraFixa: 40);

                if (NE)
                {
                    vmpGridViewNE.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição Ne", LarguraFixa: 0);
                    vmpGridViewNE.FormatarColuna("DescricaoNm", Visivel: false, Caption: "Descrição Nm");
                    vmpGridViewNE.AutoFillColumn = vmpGridViewNE.Columns["Descricao"];
                }
                else
                {
                    vmpGridViewNE.FormatarColuna("Descricao", Visivel: false, Caption: "Descrição Ne", LarguraFixa: 0);
                    vmpGridViewNE.FormatarColuna("DescricaoNm", Visivel: true, Caption: "Descrição Nm");
                    vmpGridViewNE.AutoFillColumn = vmpGridViewNE.Columns["DescricaoNm"];
                }

                vmpGridViewNE.FinalizarFormatacao();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaTipos()
        {
            try
            {

                vmpGridViewTipos.IniciarFormatacao(true);

                vmpGridViewTipos.FormatarColuna("Tipo", Visivel: true, LarguraFixa: 100);

                vmpGridViewTipos.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                vmpGridViewTipos.FormatarColuna("DescricaoExtra", Visivel: true, Editavel: true, Caption: " ", LarguraFixa: 25);

                vmpGridViewTipos.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewTipos.FormatarColuna("Codigo", Caption: "Código");

                dsArtigosMundifios.VMP_ART_Tipo.Columns["DescricaoExtra"].ReadOnly = false;

                vmpGridViewTipos.AutoFillColumn = vmpGridViewTipos.Columns["Descricao"];

                vmpGridViewTipos.FinalizarFormatacao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaTorcao1()
        {
            try
            {

                vmpGridViewTorcao1.IniciarFormatacao(true);

                vmpGridViewTorcao1.FormatarColuna("Id", Visivel: false);

                vmpGridViewTorcao1.FormatarColuna("Codigo", Visivel: false, Caption: "Código");

                vmpGridViewTorcao1.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewTorcao1.FormatarColuna("Torcao", Visivel: true, LarguraFixa: 70, Caption: "Torção");

                vmpGridViewTorcao1.FormatarColuna("Descricao", Visivel: true, LarguraFixa: 0, Caption: "Descrição");

                vmpGridViewTorcao1.AutoFillColumn = vmpGridViewTorcao1.Columns["Descricao"];

                vmpGridViewTorcao1.FinalizarFormatacao();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }


        private void FormatarGrelhaTorcao2()
        {
            try
            {

                vmpGridViewTorcao2.IniciarFormatacao(true);

                vmpGridViewTorcao2.FormatarColuna("Id", Visivel: false);

                vmpGridViewTorcao2.FormatarColuna("Codigo", Visivel: false, Caption: "Código");

                vmpGridViewTorcao2.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewTorcao2.FormatarColuna("Torcao", Visivel: true, LarguraFixa: 70, Caption: "Torção");

                vmpGridViewTorcao2.FormatarColuna("Descricao", Visivel: true, LarguraFixa: 0, Caption: "Descrição");

                vmpGridViewTorcao2.AutoFillColumn = vmpGridViewTorcao2.Columns["Descricao"];

                vmpGridViewTorcao2.FinalizarFormatacao();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaReferencias()
        {
            try
            {
                vmpGridViewReferencias.IniciarFormatacao(true);

                vmpGridViewReferencias.FormatarColuna("Id", LarguraFixa: 0);

                vmpGridViewReferencias.FormatarColuna("Codigo", Visivel: false, Caption: "Código", LarguraFixa: 0);

                vmpGridViewReferencias.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewReferencias.FormatarColuna("Ref", Visivel: true, LarguraFixa: 50, Caption: "Ref.");

                vmpGridViewReferencias.FormatarColuna("Descricao", Visivel: true, LarguraFixa: 0, Caption: "Descrição");

                vmpGridViewReferencias.FormatarColuna("CodigoCor", Visivel: true, LarguraFixa: 50, Caption: "Cód.Cor");

                vmpGridViewReferencias.FormatarColuna("DescricaoCor", Visivel: true, LarguraFixa: 180, Caption: "Descrição");

                vmpGridViewReferencias.FormatarColuna("IdCor", LarguraFixa: 0);

                vmpGridViewReferencias.AutoFillColumn = vmpGridViewReferencias.Columns["Descricao"];

                vmpGridViewReferencias.FinalizarFormatacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaCone()
        {
            try
            {

                vmpGridViewCone.IniciarFormatacao(true);

                vmpGridViewCone.FormatarColuna("Id", LarguraFixa: 0);

                vmpGridViewCone.FormatarColuna("Codigo", Caption: "Código", Visivel: false);

                vmpGridViewCone.FormatarColuna("Cone", Visivel: true, LarguraFixa: 100);

                vmpGridViewCone.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewCone.FormatarColuna("Descricao", Visivel: true, LarguraFixa: 0, Caption: "Descrição");


                vmpGridViewCone.AutoFillColumn = vmpGridViewCone.Columns["Descricao"];

                vmpGridViewCone.FinalizarFormatacao();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaCaracteristica()
        {
            try
            {

                vmpGridViewCaracteristicas.IniciarFormatacao(true);

                vmpGridViewCaracteristicas.FormatarColuna("Caracteristica", Visivel: true, LarguraFixa: 100);

                vmpGridViewCaracteristicas.FormatarColuna("Codigo", Caption: "Código");

                vmpGridViewCaracteristicas.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewCaracteristicas.FormatarColuna("DescricaoExtra", Visivel: true, Editavel: true, Caption: " ", LarguraFixa: 25);

                vmpGridViewCaracteristicas.FormatarColuna("Descricao", Visivel: true, LarguraFixa: 0, Caption: "Descrição");

                vmpGridViewCaracteristicas.AutoFillColumn = vmpGridViewCaracteristicas.Columns["Descricao"];

                dsArtigosMundifios.VMP_ART_Caracteristica.Columns["DescricaoExtra"].ReadOnly = false;

                vmpGridViewCaracteristicas.FinalizarFormatacao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaPrograma()
        {
            try
            {

                vmpGridViewPrograma.IniciarFormatacao(true);

                vmpGridViewPrograma.FormatarColuna("Codigo", Caption: "Código");

                vmpGridViewPrograma.FormatarColuna("Programa", Visivel: true, LarguraFixa: 100);

                vmpGridViewPrograma.FormatarColuna("Sel", Editavel: true);

                vmpGridViewPrograma.FormatarColuna("Descricao", LarguraFixa: 0, Caption: "Descrição", Visivel: true);

                dsArtigosMundifios.VMP_ART_Programa.Columns["Sel"].ReadOnly = false;

                vmpGridViewPrograma.AutoFillColumn = vmpGridViewPrograma.Columns["Descricao"];

                vmpGridViewPrograma.FinalizarFormatacao();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaDimensao()
        {
            try
            {

                vmpGridViewDimensao.IniciarFormatacao(true);

                vmpGridViewDimensao.FormatarColuna("Id", LarguraFixa: 0);

                vmpGridViewDimensao.FormatarColuna("Codigo", Caption: "Cód.", Visivel: false, LarguraFixa: 30);

                vmpGridViewDimensao.FormatarColuna("Filamentos", Visivel: true, LarguraFixa: 35, Caption: "Filam.");

                vmpGridViewDimensao.FormatarColuna("Cabos", Visivel: true, LarguraFixa: 35);

                vmpGridViewDimensao.FormatarColuna("Dimensao", Visivel: true, LarguraFixa: 30, Caption: "Dim.");

                vmpGridViewDimensao.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewDimensao.FormatarColuna("Descricao", Visivel: true, LarguraFixa: 0, Caption: "Descrição");

                vmpGridViewDimensao.AutoFillColumn = vmpGridViewDimensao.Columns["Descricao"];

                vmpGridViewDimensao.FinalizarFormatacao();




                panelControlCaracteristicasBaixo.Controls.Add(groupControlDimensao);
                groupControlDimensao.Dock = DockStyle.Left;
                groupControlDimensao.Size = groupControlCone.Size;
                groupControlDimensao.Location = groupControlCone.Location;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaTexturizacao()
        {
            try
            {

                vmpGridViewTexturizacao.IniciarFormatacao(true);

                vmpGridViewTexturizacao.FormatarColuna("Id", LarguraFixa: 0);

                vmpGridViewTexturizacao.FormatarColuna("Codigo", Caption: "Código", Visivel: false, LarguraFixa: 40);

                vmpGridViewTexturizacao.FormatarColuna("Sel", Editavel: true, Visivel: false);

                vmpGridViewTexturizacao.FormatarColuna("Descricao", Visivel: true, LarguraFixa: 0, Caption: "Descrição");

                vmpGridViewTexturizacao.AutoFillColumn = vmpGridViewTexturizacao.Columns["Descricao"];

                vmpGridViewTexturizacao.FinalizarFormatacao();

                panelControlCarateristicasCima.Controls.Add(groupControlTexturizacao);
                groupControlTexturizacao.Size = groupControlNE.Size;
                groupControlTexturizacao.Location = groupControlNE.Location;
                groupControlTexturizacao.Dock = DockStyle.Left;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaCategoria()
        {
            try
            {

                vmpGridViewCategoria.IniciarFormatacao(true);

                vmpGridViewCategoria.FormatarColuna("Id", LarguraFixa: 0);

                vmpGridViewCategoria.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewCategoria.FormatarColuna("Categoria", Visivel: true, LarguraFixa: 100);

                vmpGridViewCategoria.FormatarColuna("Descricao", LarguraFixa: 0, Caption: "Descrição", Visivel: true);

                vmpGridViewCategoria.AutoFillColumn = vmpGridViewCategoria.Columns["Descricao"];

                vmpGridViewCategoria.FinalizarFormatacao();

                panelControlCarateristicasCima.Controls.Add(groupControlCategoria);
                groupControlCategoria.Location = groupControlTipos.Location;
                groupControlCategoria.Size = groupControlTipos.Size;
                groupControlCategoria.Dock = DockStyle.Left;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaDadosAuxiliares()
        {
            try
            {

                vmpGridViewDadosAuxiliares.IniciarFormatacao(true);

                vmpGridViewDadosAuxiliares.FormatarColuna("Empresa", Visivel: true, Editavel: false, LarguraFixa: 80);

                vmpGridViewDadosAuxiliares.FormatarColuna("ArmazemSugestao", Visivel: true, Caption: "Armazém", LarguraFixa: 50);

                vmpGridViewDadosAuxiliares.FormatarColuna("ArmazemSugestaoDescricao", Visivel: true, Editavel: false, Caption: "Descrição", LarguraFixa: 50);

                vmpGridViewDadosAuxiliares.FormatarColuna("TipoArtigo", Visivel: true, Caption: "Tipo Artigo", LarguraFixa: 60);

                vmpGridViewDadosAuxiliares.FormatarColuna("TipoArtigoDescricao", true, Editavel: false, Caption: "Descrição", LarguraFixa: 115);

                vmpGridViewDadosAuxiliares.FormatarColuna("TipoComponente", Visivel: true, Caption: "Componente", LarguraFixa: 80);

                vmpGridViewDadosAuxiliares.FormatarColuna("GrupoTaxaDesperdicio", Visivel: true, Caption: "Grupo", LarguraFixa: 45);

                vmpGridViewDadosAuxiliares.FormatarColuna("GrupoTaxaDesperdiciodescricao", true, Editavel: false, Caption: "Descrição", LarguraFixa: 180);

                vmpGridViewDadosAuxiliares.FormatarColuna("GrupoTaxaDesperdicioTaxa", true, Editavel: false, Caption: "Taxa", LarguraFixa: 35);

                vmpGridViewDadosAuxiliares.FormatarColuna("IntrastatPautal", Visivel: true, Caption: "Intrastat", LarguraFixa: 80);

                vmpGridViewDadosAuxiliares.FormatarColuna("IntrastatPautalDescricao", true, Editavel: false, Caption: "Descrição", LarguraFixa: 150);

                vmpGridViewDadosAuxiliares.FormatarColuna("CodigoAntigo", Visivel: true, Caption: "Código Antigo", LarguraFixa: 110);

                vmpGridViewDadosAuxiliares.FormatarColuna("CodigoAntigoDescricao", Visivel: true, Caption: "Descrição", LarguraFixa: 150);

                vmpGridViewDadosAuxiliares.FormatarColuna("TipoComponenteDescricao", Caption: "Componente", LarguraFixa: 105);

                vmpGridViewCategoria.FinalizarFormatacao();

                //this.DdTipoComponente.DisplayMember = "TipoComponente";
                //this.DdTipoComponente.ValueMember = "TipoComponenteDescricao";

                //withBlock.Columns("TipoComponenteDescricao").DropDown = this.DdTipoComponente;

                //withBlock.Splits(0).DisplayColumns("ArmazemSugestao").HeadingStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0066CC");
                //withBlock.Splits(0).DisplayColumns("TipoArtigo").HeadingStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0066CC");
                //withBlock.Splits(0).DisplayColumns("TipoComponente").HeadingStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0066CC");
                //withBlock.Splits(0).DisplayColumns("TipoComponenteDescricao").HeadingStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0066CC");
                //withBlock.Splits(0).DisplayColumns("GrupoTaxaDesperdicio").HeadingStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0066CC");
                //withBlock.Splits(0).DisplayColumns("IntrastatPautal").HeadingStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0066CC");
                //withBlock.Splits(0).DisplayColumns("CodigoAntigo").HeadingStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0066CC");

                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["ArmazemSugestao"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["ArmazemSugestaoDescricao"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["TipoArtigo"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["TipoArtigoDescricao"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["TipoComponente"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["TipoComponenteDescricao"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["GrupoTaxaDesperdicio"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["GrupoTaxaDesperdiciodescricao"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["GrupoTaxaDesperdicioTaxa"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["IntrastatPautal"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["IntrastatPautalDescricao"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["CodigoAntigo"].ReadOnly = false;
                dsArtigosMundifios.DadosAuxiliaresECRA.Columns["CodigoAntigoDescricao"].ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }


        #endregion

        #region Funções

        private void RemoverBloqueiosUtilizadorPosto()
        {
            frmArtigo.dsPropriedadesMundifios.RemoverBloqueiosUtilizadorPosto();
        }

        private void AdicionarLabel()
        {
            Label lbl = new Label();
            lbl.Size = new System.Drawing.Size(500, 200);
            lbl.Location = new System.Drawing.Point((int)((this.Width / (double)2) - (lbl.Size.Width / (double)2)), (int)((this.Height / (double)2) - (lbl.Size.Height / (double)2)));
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("Microsoft Sans Serif", 25, FontStyle.Bold | FontStyle.Regular);
            lbl.Text = "Utilizador sem permissões.";
            this.Controls.Add(lbl);
        }

        private bool ValidarPermissoes()
        {
            if (!(dsEmpresasGrupo.GetTrueIfAcessoCriacaoArtigo()))
            {
                foreach (Control Controlo in this.Controls)
                    Controlo.Visible = false;
                AdicionarLabel();
                return false;
            }
            else
            {
                GerirPropriedades();
                return true;
            }
        }

        private void GerirPropriedades()
        {
            bool Estado = dsEmpresasGrupo.GetTrueIfAcessoGestaoPropriedades();

            buttonGerirTipos.Visible = Estado;
            buttonGerirNE.Visible = Estado;
            buttonGerirComponentes.Visible = Estado;
            buttonGerirCaracteristicas.Visible = Estado;
            buttonGerirTorcao1.Visible = Estado;
            buttonGerirTorcao2.Visible = Estado;
            buttonGerirReferencias.Visible = Estado;
            buttonGerirCone.Visible = Estado;
            buttonGerirPrograma.Visible = Estado;
            buttonGerirCategoria.Visible = Estado;
            buttonGerirDimensao.Visible = Estado;
            buttonGerirTextualizacao.Visible = Estado;
        }

        private void CreatForm()
        {
            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmGestaoPropriedadesMundifiosView));
            frmArtigo = result.Result;
        }

        private void GerirPropriedades_Click(object sender, EventArgs e)
        {
            frmArtigo.ShowDialog();
        }

        private void CarregarDropDownTipoComponente()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TipoComponente", typeof(string));
            dt.Columns.Add("TipoComponenteDescricao", typeof(string));
            // com uma descrição geral a indicar o tipo de lista 
            DataRow dr;

            dr = dt.NewRow();
            dr["TipoComponente"] = "2";
            dr["TipoComponenteDescricao"] = "Artigo Composto";
            dt.Rows.InsertAt(dr, 0);

            dr = dt.NewRow();
            dr["TipoComponente"] = "1";
            dr["TipoComponenteDescricao"] = "Conjunto de Artigos";
            dt.Rows.InsertAt(dr, 0);


            dr = dt.NewRow();
            dr["TipoComponente"] = "0";
            dr["TipoComponenteDescricao"] = "Artigo Simples";
            dt.Rows.InsertAt(dr, 0);

            //Resolver
            //    this.DdTipoComponente.DataSource = dt;

            //    this.DdTipoComponente.Columns("TipoComponente").Caption = "";
            //    this.DdTipoComponente.Columns("TipoComponenteDescricao").Caption = "";
            //    this.DdTipoComponente.DisplayColumns(0).Width = 0;
            //    this.DdTipoComponente.DisplayColumns(1).Width = 120;
            //    this.DdTipoComponente.Width = 120;
        }

        private void InicializarForulario()
        {
            bindingSourceDadosAuxiliares.DataSource = dsArtigosMundifios.DadosAuxiliaresECRA;


            checkEditReferenciasLivres.CheckedChanged -= checkEditReferenciasLivres_CheckedChanged;
            checkEditReferenciasLivres.Checked = false;
            checkEditReferenciasLivres.CheckedChanged += checkEditReferenciasLivres_CheckedChanged;

            // # Carregar as Grelhas
            CarregarGrelhas(checkEditReferenciasLivres.Checked);

            // # Formatacoes
            FormatarGrelhas();

            AtribuirCodigo();

            AtivarDesaticarCheckBoxDescExtra(true);

            AtribuirDescricao();
            AtribuirDescricaoExtra(true, true);

            vmpGridControlSubFamilias_MouseUp(null, null);

            RemoverBloqueiosUtilizadorPosto();

            PosicionarDadosAuxiliares(false);
        }

        private void LimparFiltros()
        {
            bindingSourceNE.Filter = null;
            vmpGridViewNE.ClearColumnsFilter();

            bindingSourceComponentes.Filter = null;
            vmpGridViewComponentes.ClearColumnsFilter();

            bindingSourceTipos.Filter = null;
            vmpGridViewTipos.ClearColumnsFilter();

            bindingSourceTorcao1.Filter = null;
            vmpGridViewTorcao2.ClearColumnsFilter();

            bindingSourceTorcao2.Filter = null;
            vmpGridViewTorcao2.ClearColumnsFilter();

            bindingSourceReferencias.Filter = null;
            vmpGridViewReferencias.ClearColumnsFilter();

            bindingSourceCaracteristicas.Filter = null;
            vmpGridViewCaracteristicas.ClearColumnsFilter();

            bindingSourceCone.Filter = null;
            vmpGridViewCone.ClearColumnsFilter();

            bindingSourcePrograma.Filter = null;
            vmpGridViewPrograma.ClearColumnsFilter();

            bindingSourceTextualizacao.Filter = null;
            vmpGridViewTexturizacao.ClearColumnsFilter();

            bindingSourceCategoria.Filter = null;
            vmpGridViewCategoria.ClearColumnsFilter();

            bindingSourceDimensao.Filter = null;
            vmpGridViewDimensao.ClearColumnsFilter();
        }

        private void checkEditReferenciasLivres_CheckedChanged(object sender, EventArgs e)
        {
            dsArtigosMundifios.CarregarReferencias(checkEditReferenciasLivres.Checked);
            AtualizarTotaisReferencia();
        }

        private void AtualizarTotaisReferencia()
        {
            labelRefTotalRegistos.Text = string.Format("{0}", dsArtigosMundifios.VMP_ART_Referencia.Count);
        }

        private void DefineGrupo(string Codigo)
        {
            switch (Codigo)
            {
                case "01":
                    {
                        CurrentGrupo = Grupo.Fios;
                        break;
                    }

                case "02":
                    {
                        CurrentGrupo = Grupo.Filamentos;
                        break;
                    }
            }
        }

        private bool AtualizarArtigoERP(string Codigo, string Empresa, string Connection, DateTime DataAtual)
        {
            try
            {

                BasBE100.BasBEArtigo ArtigoERP = new BasBE100.BasBEArtigo();

                PriV100Api.AbreEmpresa(PriV100Api.BSO.Contexto.TipoPlataforma, Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);

                if (!isNew)
                {
                    // # atualizar artigo
                    ArtigoERP = PriV100Api.BSO.Base.Artigos.Edita(Codigo);
                }

                ArtigoERP.Artigo = Codigo;
                ArtigoERP.Descricao = textEditDescricaoArtigo.Text;
                ArtigoERP.CamposUtil["CDU_DescricaoInterna"].Valor = textEditDescricaoArtigoAlterado.Text;


                ArtigoERP.Familia = dsArtigosMundifios._FamiliasSel.Familia;
                ArtigoERP.SubFamilia = dsArtigosMundifios._SubFamiliasSel.SubFamilia;
                ArtigoERP.Observacoes = memoEditObservacoes.Text;

                // AdptC.Connection.ConnectionString = Connection

                dsArtigosMundifios.PreencherComposUtilizadorPorEmpresa(ArtigoERP, Empresa);

                ArtigoERP.TrataLotes = true;
                ArtigoERP.CamposUtil["CDU_NE"].Valor = dsArtigosMundifios._NESel.Codigo;
                // ArtigoERP.CampoUtilizador("CDU_Tipo").Valor = Me.DsArtigosMundifios._TipoSel.Codigo
                ArtigoERP.CamposUtil["CDU_Torcao1"].Valor = dsArtigosMundifios._Torcao1Sel.Codigo;
                ArtigoERP.CamposUtil["CDU_Torcao2"].Valor = dsArtigosMundifios._Torcao2Sel.Codigo;
                ArtigoERP.CamposUtil["CDU_referencia"].Valor = dsArtigosMundifios._ReferenciaSel.Codigo;
                // ArtigoERP.CamposUtil["CDU_Caracteristica", Me.DsArtigosMundifios._CaracteristicaSel.Codigo
                ArtigoERP.CamposUtil["CDU_Cone"].Valor = dsArtigosMundifios._ConeSel.Codigo;
                ArtigoERP.CamposUtil["CDU_Programa"].Valor = dsArtigosMundifios._ProgramaSel.Codigo;
                ArtigoERP.CamposUtil["CDU_Dimensao"].Valor = dsArtigosMundifios._DimensaoSel.Codigo;
                ArtigoERP.CamposUtil["CDU_Texturizacao"].Valor = dsArtigosMundifios._TexturizacaoSel.Codigo;
                ArtigoERP.CamposUtil["CDU_Categoria"].Valor = dsArtigosMundifios._CategoriaSel.Codigo;

                ArtigoERP.CamposUtil["CDU_DescricaoExtra"].Valor = textEditDescricaoExtraArtigo.Text;
                ArtigoERP.CamposUtil["CDU_DescricaoExtraExterna"].Valor = this.textEditDescricaoExtraExternaArtigo.Text;

                ArtigoERP.Anulado = checkEditArtigoAnulado.Checked;
                // Me.DsArtigosMundifios.GerarDescricaoPercentagemComponentes()
                // ArtigoERP.CamposUtil["CDU_Componentes", Me.DsArtigosMundifios._DescricaoComponente
                // ArtigoERP.CamposUtil["CDU_ComponentesPerc", Me.DsArtigosMundifios._PercentagensComponente

                // Campos que permitem definir se a propriedade faz parte da descrição extra ou nao
                ArtigoERP.CamposUtil["CDU_DescExtraTipo"].Valor = checkEditDescricaoExtraTipos.Checked;
                ArtigoERP.CamposUtil["CDU_DescExtraTorcao1"].Valor = checkEditDescricaoExtraTorcao1.Checked;
                ArtigoERP.CamposUtil["CDU_DescExtraTorcao2"].Valor = checkEditDescricaoExtraTorcao2.Checked;
                ArtigoERP.CamposUtil["CDU_DescExtraReferencia"].Valor = checkEditReferenciasLivres.Checked;
                ArtigoERP.CamposUtil["CDU_DescExtraCaracteristica"].Valor = checkEditDescricaoExtraCaracteristicas.Checked;
                ArtigoERP.CamposUtil["CDU_DescExtraCone"].Valor = checkEditDescricaoExtraCone.Checked;
                ArtigoERP.CamposUtil["CDU_DescExtraPrograma"].Valor = checkEditDescricaoExtraPrograma.Checked;
                ArtigoERP.CamposUtil["CDU_DescExtraCategoria"].Valor = checkEditDescricaoExtraCategoria.Checked;
                ArtigoERP.CamposUtil["CDU_DescExtraDimensao"].Valor = checkEditDescricaoExtraDimensao.Checked;
                ArtigoERP.CamposUtil["CDU_DescricaoNm"].Valor = radioButtonNm.Checked;

                // Descrição dos campos selecionados
                ArtigoERP.CamposUtil["CDU_DescricaoNE"].Valor = dsArtigosMundifios._NESel.Descricao;

                dsArtigosMundifios.GerarDescricaoPercentagemComponentes();
                ArtigoERP.CamposUtil["CDU_DescricaoComponentes"].Valor = dsArtigosMundifios.DescricaoComponentesSel;
                ArtigoERP.CamposUtil["CDU_DescricaoComponentesPerc"].Valor = dsArtigosMundifios.DescricaoComponentesPercentagemSel;
                ArtigoERP.CamposUtil["CDU_DescricaoTipo"].Valor = dsArtigosMundifios.DescricaoTiposSel;
                ArtigoERP.CamposUtil["CDU_DescricaoTorcao1"].Valor = dsArtigosMundifios._Torcao1Sel.Descricao;
                ArtigoERP.CamposUtil["CDU_DescricaoTorcao2"].Valor = dsArtigosMundifios._Torcao2Sel.Descricao;
                ArtigoERP.CamposUtil["CDU_DescricaoReferencia"].Valor = dsArtigosMundifios._ReferenciaSel.Descricao;
                ArtigoERP.CamposUtil["CDU_DescricaoCaracteristica"].Valor = dsArtigosMundifios.DescricaoCaracteristicaSel;
                ArtigoERP.CamposUtil["CDU_DescricaoCone"].Valor = dsArtigosMundifios._ConeSel.Descricao;
                ArtigoERP.CamposUtil["CDU_DescricaoPrograma"].Valor = dsArtigosMundifios._ProgramaSel.Descricao;
                ArtigoERP.CamposUtil["CDU_DescricaoDimensao"].Valor = dsArtigosMundifios._DimensaoSel.Descricao;
                ArtigoERP.CamposUtil["CDU_DescricaoTexturizacao"].Valor = dsArtigosMundifios._TexturizacaoSel.Texturizacao;
                ArtigoERP.CamposUtil["CDU_DescricaoCategoria"].Valor = dsArtigosMundifios._CategoriaSel.Descricao;
                ArtigoERP.CamposUtil["CDU_OrdenacaoDescricaoExtra"].Valor = textEditOrdenacaoDescricaoExtra.Text;
                ArtigoERP.IVA = "23";
                ArtigoERP.UnidadeBase = "UN";
                if (isNew)
                    ArtigoERP.CamposUtil["CDU_DataCriacao"].Valor = DataAtual;

                PriV100Api.BSO.Base.Artigos.Actualiza(ArtigoERP);

                PriV100Api.BSO.FechaEmpresaTrabalho();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        private bool GravarArtigo()
        {
            string EmpresaErro = string.Empty;

            try
            {

                // Garantir que a data de criação é igual em todas as empresas para o mesmo artigo!
                DateTime DataAtual = DateTime.Now;

                foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
                {

                    // If item.Empresa <> EmpresaExclusica Then Continue For

                    EmpresaErro = item.Empresa;

                    // " Atualizar Artigo no ERP
                    if (AtualizarArtigoERP(textEditCodigoArtigo.Text, item.Empresa, item.ConnectionString, DataAtual))
                    {
                        dsArtigosMundifios.GravarComponentes(textEditCodigoArtigo.Text, item.ConnectionString);
                        dsArtigosMundifios.GravarCaracteristicas(textEditCodigoArtigo.Text, item.ConnectionString);
                        dsArtigosMundifios.GravarTipos(textEditCodigoArtigo.Text, item.ConnectionString);
                    }
                    else
                        return false;
                }

                barButtonItemNovo_ItemClick(null, null);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problema detetado na empresa: " + EmpresaErro, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            finally
            {
            }
        }

        public void GerarDescricaoExtrasArtigo(frmArtigoMundifiosView.Grupo Grupo, ref string DescricaoExtraInternaArtigo, ref string DescricaoExtraExternaArtigo)
        {
            if (Strings.Len(textEditOrdenacaoDescricaoExtra.Text) > 0)
            {
                string[] Ordenacao = Strings.Split(textEditOrdenacaoDescricaoExtra.Text, ";");

                foreach (string propriedade in Ordenacao)
                {

                    switch (Strings.Left(propriedade, 1))
                    {
                        case "T":
                            {
                                DescricaoExtraExternaArtigo = DescricaoExtraExternaArtigo + dsArtigosMundifios.VMP_ART_Tipo.Select("Codigo = '" + Strings.Right(propriedade, Strings.Len(propriedade) - 1) + "'")[0]["Descricao"] + " ";
                                DescricaoExtraInternaArtigo = DescricaoExtraInternaArtigo + dsArtigosMundifios.VMP_ART_Tipo.Select("Codigo = '" + Strings.Right(propriedade, Strings.Len(propriedade) - 1) + "'")[0]["Descricao"] + " ";
                                break;
                            }

                        case "C":
                            {
                                DescricaoExtraExternaArtigo = DescricaoExtraExternaArtigo + dsArtigosMundifios.VMP_ART_Caracteristica.Select("Codigo = '" + Strings.Right(propriedade, Strings.Len(propriedade) - 1) + "'")[0]["Descricao"] + " ";
                                DescricaoExtraInternaArtigo = DescricaoExtraInternaArtigo + dsArtigosMundifios.VMP_ART_Caracteristica.Select("Codigo = '" + Strings.Right(propriedade, Strings.Len(propriedade) - 1) + "'")[0]["Descricao"] + " ";
                                break;
                            }
                    }
                }
            }
            else
            {

                // caso tenha sido um artigo importado, não tem a ordenação preenchida...

                // * Tipos
                if (!(dsArtigosMundifios._TipoSel == null))
                {
                    if (checkEditDescricaoExtraTipos.Checked == true)
                    {
                        foreach (DsArtigosMundifios.VMP_ART_TipoRow item in dsArtigosMundifios.VMP_ART_Tipo)
                        {
                            if (item.Sel == true & Convert.ToDouble(item.Codigo) != 0 & item.DescricaoExtra == true)
                            {
                                if (!(item.IsDescricaoNull()))
                                    // If item.DescricaoExtra Then
                                    DescricaoExtraExternaArtigo = DescricaoExtraExternaArtigo + " " + item.Descricao;
                            }
                        }
                    }
                }

                // * caracteristicas

                if (!(dsArtigosMundifios._CaracteristicaSel == null))
                {
                    if (checkEditDescricaoExtraCaracteristicas.Checked == true)
                    {
                        foreach (DsArtigosMundifios.VMP_ART_CaracteristicaRow item in dsArtigosMundifios.VMP_ART_Caracteristica)
                        {
                            if (item.Codigo != "")
                            {
                                double numero = double.Parse(item.Codigo);
                                if (item.Sel == true & numero != 0 & item.DescricaoExtra == true)
                                {
                                    if (!(item.IsDescricaoNull()))
                                        DescricaoExtraExternaArtigo = DescricaoExtraExternaArtigo + " " + item.Descricao;
                                }
                            }

                        }
                    }
                }
            }


            // ***********************************************************************************************
            // interna -> independentemente dos piscos da coluna "descrição extra" preenche sempre a descrição!

            // * Tipos
            if (!(dsArtigosMundifios._TipoSel == null))
            {
                if (checkEditDescricaoExtraTipos.Checked == true)
                {
                    foreach (DsArtigosMundifios.VMP_ART_TipoRow item in dsArtigosMundifios.VMP_ART_Tipo)
                    {
                        if (item.Sel == true & Convert.ToDouble(item.Codigo) != 0)
                        {
                            if (!(item.IsDescricaoNull()))
                            {
                                // If item.DescricaoExtra Then
                                if (Strings.InStr(textEditOrdenacaoDescricaoExtra.Text, "T" + item.Codigo) == 0)
                                    DescricaoExtraInternaArtigo = DescricaoExtraInternaArtigo + " " + item.Descricao;
                            }
                        }
                    }
                }
            }

            // * caracteristicas
            if (!(dsArtigosMundifios._CaracteristicaSel == null))
            {
                if (checkEditDescricaoExtraCaracteristicas.Checked == true)
                {
                    foreach (DsArtigosMundifios.VMP_ART_CaracteristicaRow item in dsArtigosMundifios.VMP_ART_Caracteristica)
                    {
                        if (item.Codigo != "" && item.Sel == true & Convert.ToDouble(item.Codigo) != 0)
                        {
                            if (!(item.IsDescricaoNull()))
                            {
                                if (Strings.InStr(textEditOrdenacaoDescricaoExtra.Text, "C" + item.Codigo) == 0)
                                    DescricaoExtraInternaArtigo = DescricaoExtraInternaArtigo + " " + item.Descricao;
                            }
                        }
                    }
                }
            }
            // ***********************************************************************************************


            string DescricaoExtraFixa = string.Empty;

            if (!(dsArtigosMundifios._CategoriaSel == null))
            {
                if (Convert.ToDouble(dsArtigosMundifios._CategoriaSel.Codigo) != 0 & checkEditDescricaoExtraCategoria.Checked == true)
                    DescricaoExtraFixa = DescricaoExtraFixa + " " + dsArtigosMundifios._CategoriaSel.Descricao;
            }

            if (!(dsArtigosMundifios._Torcao1Sel == null))
            {
                if (Convert.ToDouble(dsArtigosMundifios._Torcao1Sel.Codigo) != 0 & checkEditDescricaoExtraTorcao1.Checked == true)
                    DescricaoExtraFixa = DescricaoExtraFixa + " " + dsArtigosMundifios._Torcao1Sel.Descricao;
            }

            if (!(dsArtigosMundifios._Torcao2Sel == null))
            {
                if (Convert.ToDouble(dsArtigosMundifios._Torcao2Sel.Codigo) != 0 & checkEditDescricaoExtraTorcao2.Checked == true)
                    DescricaoExtraFixa = DescricaoExtraFixa + " " + dsArtigosMundifios._Torcao2Sel.Descricao;
            }

            if (!(dsArtigosMundifios._ReferenciaSel == null))
            {
                if (Convert.ToDouble(dsArtigosMundifios._ReferenciaSel.Codigo) != 0 & checkEditDescricaoExtraReferencias.Checked == true)
                    DescricaoExtraFixa = DescricaoExtraFixa + " " + dsArtigosMundifios._ReferenciaSel.Descricao;
            }

            if (!(dsArtigosMundifios._ConeSel == null))
            {
                if (Convert.ToDouble(dsArtigosMundifios._ConeSel.Codigo) != 0 & checkEditDescricaoExtraCone.Checked == true)
                    DescricaoExtraFixa = DescricaoExtraFixa + " " + dsArtigosMundifios._ConeSel.Descricao;
            }

            if (!(dsArtigosMundifios._DimensaoSel == null))
            {
                if (Convert.ToDouble(dsArtigosMundifios._DimensaoSel.Codigo) != 0 & checkEditDescricaoExtraDimensao.Checked == true)
                    DescricaoExtraFixa = DescricaoExtraFixa + " " + dsArtigosMundifios._DimensaoSel.Descricao;
            }

            if (!(dsArtigosMundifios._ProgramaSel == null))
            {
                if (Convert.ToDouble(dsArtigosMundifios._ProgramaSel.Codigo) != 0 & checkEditDescricaoExtraPrograma.Checked == true)
                    DescricaoExtraFixa = DescricaoExtraFixa + " " + dsArtigosMundifios._ProgramaSel.Descricao;
            }


            RetirarPrimeiro_E_UltimoEspaco(ref DescricaoExtraFixa);
            RetirarPrimeiro_E_UltimoEspaco(ref DescricaoExtraInternaArtigo);
            RetirarPrimeiro_E_UltimoEspaco(ref DescricaoExtraExternaArtigo);

            DescricaoExtraInternaArtigo = DescricaoExtraInternaArtigo + " " + DescricaoExtraFixa;
            DescricaoExtraExternaArtigo = DescricaoExtraExternaArtigo + " " + DescricaoExtraFixa;

            RetirarPrimeiro_E_UltimoEspaco(ref DescricaoExtraInternaArtigo);
            RetirarPrimeiro_E_UltimoEspaco(ref DescricaoExtraExternaArtigo);

            DescricaoExtraInternaArtigo = Strings.Replace(DescricaoExtraInternaArtigo, "  ", " ");
            DescricaoExtraExternaArtigo = Strings.Replace(DescricaoExtraExternaArtigo, "  ", " ");
        }

        private void RetirarPrimeiro_E_UltimoEspaco(ref string str)
        {
            // * retirar espaço no inicio
            if (str.StartsWith(" "))
                str = Strings.Right(str, Strings.Len(str) - 1);
            // * retirar espaço no fim
            if (str.EndsWith(" "))
                str = Strings.Left(str, Strings.Len(str) - 1);
        }

        private void GravarDados()
        {
            try
            {
                if (!ValidacoesCodigo())
                    return;

                if (!ValidacoesDescricao())
                    return;

                if (!ValidacoesPropriedades())
                    return;

                if (!ValidacoesTodasEmpresas())
                    return;

                if (!ValidacoesDadosAuxiliares())
                    return;

                if (!Confirmacao(true))
                    return;

                if (GravarArtigo())
                    checkEditReferenciasLivres_CheckedChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void CarregarGrelhas(bool ApenasReferenciasLivres)
        {
            dsArtigosMundifios.CarregarFamilias();
            dsArtigosMundifios.CarregarSubFamilias();
            if (dsArtigosMundifios.Familias.Rows.Count > 0)
                bindingSourceSubFamilias.Filter = "Familia = '" + dsArtigosMundifios.Familias[0].Familia.ToString() + "'";
            dsArtigosMundifios.CarregarComponentes();
            CalcularTotais();
            dsArtigosMundifios.CarregarNE();
            dsArtigosMundifios.CarregarTipos();
            dsArtigosMundifios.CarregarTorcao1();
            dsArtigosMundifios.CarregarTorcao2();
            dsArtigosMundifios.CarregarReferencias(ApenasReferenciasLivres);
            labelRefTotalRegistos.Text = string.Format("{0}", dsArtigosMundifios.VMP_ART_Referencia.Count);
            dsArtigosMundifios.CarregarCor();
            dsArtigosMundifios.CarregarCone();
            dsArtigosMundifios.CarregarCaracteristica();
            dsArtigosMundifios.CarregarPrograma();

            dsArtigosMundifios.CarregarDimensao();
            dsArtigosMundifios.CarregarTexturizacao();
            dsArtigosMundifios.CarregarCategoria();
        }

        private void CalcularTotais()
        {
            bindingSourceComponentes.EndEdit();
        }

        private void ColorirTextBoxes(bool Colorir)
        {
            if (Colorir)
            {
                textEditCodigoArtigo.ForeColor = CorCaixasTexto;
                textEditDescricaoArtigo.ForeColor = CorCaixasTexto;
                // Me.txtDescArtigoAlt.ForeColor = CorCaixasTexto
                textEditDescricaoExtraArtigo.ForeColor = CorCaixasTexto;
                textEditDescricaoExtraExternaArtigo.ForeColor = CorCaixasTexto;
            }
            else
            {
                textEditCodigoArtigo.ForeColor = Color.Black;
                textEditDescricaoArtigo.ForeColor = Color.Black;
                // Me.txtDescArtigoAlt.ForeColor = Color.Black
                textEditDescricaoExtraArtigo.ForeColor = Color.Black;
                textEditDescricaoExtraExternaArtigo.ForeColor = Color.Black;
            }
        }

        private void SelecionarPrimeiroRegistoSubFamilias()
        {
            LimparSubFamilia(false);

            DsArtigosMundifios.SubFamiliasRow[] SubFamilias = (DsArtigosMundifios.SubFamiliasRow[])dsArtigosMundifios.SubFamilias.Select("Familia = '" + dsArtigosMundifios._FamiliasSel.Familia.ToString() + "'", "SubFamilia");
            if (SubFamilias.Length > 0)
                SubFamilias[0].Sel = true;
        }

        public void AtivarBloquearGrelhas(bool Ativar)
        {
            Color Cor;
            if (Ativar)
                Cor = Color.Black;
            else
                Cor = Color.Red;

            // Me.gBoxFamilias.Enabled = Ativar
            // Me.gBoxSubFamilias.Enabled = Ativar
            // Me.gBoxNE.Enabled = Ativar
            // Me.gBoxTexturizacao.Enabled = Ativar
            // Me.gBoxComponentes.Enabled = Ativar
            radioButtonNe.Enabled = Ativar;
            radioButtonNm.Enabled = Ativar;

            groupControlFamilias.ForeColor = Cor;
            groupControlSubFamilias.ForeColor = Cor;
            groupControlNE.ForeColor = Cor;
            groupControlTexturizacao.ForeColor = Cor;
            groupControlComponentes.ForeColor = Cor;

            vmpGridControlFamilias.Enabled = Ativar;
            vmpGridControlSubFamilias.Enabled = Ativar;
            vmpGridControlNE.Enabled = Ativar;
            vmpGridControlTexturizacao.Enabled = Ativar;
            vmpGridControlComponentes.Enabled = Ativar;


            //labelSubCodigo.Text = Convert.ToString(Ativar);
        }

        private void AlterouPropriedades(bool Familia = false, bool F4 = false)
        {
            if (Familia)
            {
                AtribuirCodigo();

                LimparTudo(false);

                if (dsArtigosMundifios.GetTrueIfArtigoExisteByCodigo(textEditCodigoArtigo.Text))
                {
                    ColorirTextBoxes(true);
                    isNew = false;
                    PosicionarTudo();
                    ColorirTextBoxes(true);
                }
                else
                {
                    ColorirTextBoxes(false);
                    isNew = true;
                    ColorirTextBoxes(false);
                }

                // If artigoMovimento = False then'
                AtribuirDescricao();
                AtribuirDescricaoExtra(true, true);
            }
            else if (F4)
            {
                if (dsArtigosMundifios.GetTrueIfArtigoExisteByCodigo(textEditCodigoArtigo.Text))
                {
                    // Como o artigo existe, carrego todas as referencias
                    checkEditReferenciasLivres.Checked = false;
                    ColorirTextBoxes(true);
                    isNew = false;
                    LimparTudo(true);
                    PosicionarTudo();
                    GetDescricoesBD();
                    GetNeNm();
                    GetCamposDescricaoExtra();
                    AtivarBloquearGrelhas(false);
                }
                else
                {
                    // Como o artigo NÃO existe, carrego apenas as referencias livres
                    // CbReferenciasLivres.Checked = False
                    ColorirTextBoxes(false);
                    isNew = true;
                    LimparTudo(true);
                    AtivarBloquearGrelhas(true);
                }
            }
            else
            {
                // If artigoMovimento = False then'
                AtribuirDescricao();
                AtribuirDescricaoExtra(true, true);
            }
        }

        private void ApagarDescricoes()
        {
            textEditDescricaoArtigo.Text = "";
            textEditDescricaoArtigoAlterado.Text = "";
            textEditDescricaoExtraArtigo.Text = "";
            textEditDescricaoExtraExternaArtigo.Text = "";
            textEditOrdenacaoDescricaoExtra.Text = "";
        }

        private void GetCamposDescricaoExtra()
        {
            checkEditDescricaoExtraTipos.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraTorcao1.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraTorcao2.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraReferencias.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCaracteristicas.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCone.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraPrograma.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCategoria.CheckedChanged -= cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraDimensao.CheckedChanged -= cbDescExtra_CheckedChanged;


            checkEditDescricaoExtraTipos.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraTipo;
            checkEditDescricaoExtraTorcao1.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraTorcao1;
            checkEditDescricaoExtraTorcao2.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraTorcao2;
            checkEditDescricaoExtraReferencias.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraReferencia;
            checkEditDescricaoExtraCaracteristicas.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraCaracteristica;
            checkEditDescricaoExtraCone.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraCone;
            checkEditDescricaoExtraPrograma.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraPrograma;
            checkEditDescricaoExtraCategoria.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraCategoria;
            checkEditDescricaoExtraDimensao.Checked = dsArtigosMundifios._Artigo.CDU_DescExtraDimensao;


            checkEditDescricaoExtraTipos.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraTorcao1.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraTorcao2.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraReferencias.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCaracteristicas.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCone.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraPrograma.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraCategoria.CheckedChanged += cbDescExtra_CheckedChanged;
            checkEditDescricaoExtraDimensao.CheckedChanged += cbDescExtra_CheckedChanged;
        }

        private void cbDescExtra_CheckedChanged(object sender, EventArgs e)
        {
            AtribuirDescricaoExtra(true, true);
        }



        private void GetNeNm()
        {
            radioButtonNe.CheckedChanged -= Rb_CheckedChanged;
            radioButtonNm.CheckedChanged -= Rb_CheckedChanged;

            radioButtonNm.Checked = dsArtigosMundifios._Artigo.CDU_DescricaoNm;

            radioButtonNe.CheckedChanged += Rb_CheckedChanged;
            radioButtonNm.CheckedChanged += Rb_CheckedChanged;
        }

        private void PosicionarDadosAuxiliares(bool ArtigoExiste)
        {
            dsArtigosMundifios.PreencherDadosAuxiliares(ArtigoExiste, textEditCodigoArtigo.Text);
        }

        private void GetDescricoesBD()
        {
            if (dsArtigosMundifios._Artigo == null)
            {
                textEditDescricaoArtigo.Text = dsArtigosMundifios._Artigo.Descricao;
            }

            if (!(dsArtigosMundifios._Artigo.IsCDU_DescricaoInternaNull()))
                textEditDescricaoArtigoAlterado.Text = dsArtigosMundifios._Artigo.CDU_DescricaoInterna;
            else
                textEditDescricaoArtigoAlterado.Text = "";

            if (!(dsArtigosMundifios._Artigo.IsCDU_DescricaoExtraNull()))
                textEditDescricaoExtraArtigo.Text = dsArtigosMundifios._Artigo.CDU_DescricaoExtra;
            else
                textEditDescricaoExtraArtigo.Text = "";

            if (!(dsArtigosMundifios._Artigo.IsCDU_DescricaoExtraExternaNull()))
                textEditDescricaoExtraExternaArtigo.Text = dsArtigosMundifios._Artigo.CDU_DescricaoExtraExterna;
            else
                textEditDescricaoExtraExternaArtigo.Text = "";

            if (!(dsArtigosMundifios._Artigo.IsCDU_OrdenacaoDescricaoExtraNull()))
                textEditOrdenacaoDescricaoExtra.Text = dsArtigosMundifios._Artigo.CDU_OrdenacaoDescricaoExtra;
            else
                textEditOrdenacaoDescricaoExtra.Text = "";
        }

        private void AtribuirCodigo()
        {
            textEditCodigoArtigo.TextChanged -= textEditCodigoArtigo_TextChanged;

            if (dsArtigosMundifios._FamiliasSel == null)
                // If Me.DsArtigosMundifios.Familias.GetSelecao.Length = 0 Then
                textEditCodigoArtigo.Text = "";
            else
                textEditCodigoArtigo.Text = dsArtigosMundifios._FamiliasSel.Familia + dsArtigosMundifios.GetProximoCodigoLivre().ToString().PadLeft(NrDigitosCodigoArtigo, '0');

            textEditCodigoArtigo.TextChanged += textEditCodigoArtigo_TextChanged;
            AtivarBloquearGrelhas(true);
        }

        private void AtribuirDescricao()
        {
            textEditDescricaoArtigo.Text = GerarDescricaoArtigo(CurrentGrupo);
            if (!(dsArtigosMundifios.GetTrueIfArtigoExisteByCodigo(textEditCodigoArtigo.Text)))
                textEditDescricaoArtigoAlterado.Text = GerarDescricaoArtigo(CurrentGrupo);
        }

        public string GerarDescricaoArtigo(frmArtigoMundifiosView.Grupo Grupo)
        {
            string DescricaoArtigo = string.Empty;

            string DescricaoComponente = string.Empty;

            string DescricaoPercentagemComponente = string.Empty;

            if (!(dsArtigosMundifios._FamiliasSel == null))
            {
                if (!(dsArtigosMundifios._FamiliasSel.IsCDU_DescricaoAbrevNull()))
                    DescricaoArtigo = DescricaoArtigo + " " + dsArtigosMundifios._FamiliasSel.CDU_DescricaoAbrev;
            }

            switch (Grupo)
            {
                case Grupo.Fios:
                    {
                        if (!(dsArtigosMundifios._NESel == null))
                        {
                            if (Convert.ToDouble(dsArtigosMundifios._NESel.Codigo) != 0)

                                // If RbNe.Checked Then
                                DescricaoArtigo = DescricaoArtigo + " " + dsArtigosMundifios._NESel.Descricao.ToString();
                        }

                        break;
                    }

                case Grupo.Filamentos:
                    {
                        if (!(dsArtigosMundifios._TexturizacaoSel == null))
                        {
                            if (Convert.ToDouble(dsArtigosMundifios._TexturizacaoSel.Codigo) != 0)
                                DescricaoArtigo = DescricaoArtigo + " " + dsArtigosMundifios._TexturizacaoSel.Descricao;
                        }

                        break;
                    }
            }

            DsArtigosMundifios.VMP_ART_ComponenteRow[] RowComponente = (DsArtigosMundifios.VMP_ART_ComponenteRow[])dsArtigosMundifios.VMP_ART_Componente.Select("Sel = true", "Percentagem desc,Ordem");
            foreach (var item in RowComponente)
            {
                if (Conversion.Val(item.Codigo) == 0)
                    continue;
                DescricaoComponente = DescricaoComponente + "/" + item.Descricao;
                DescricaoPercentagemComponente = DescricaoPercentagemComponente + "/" + item.Percentagem;
            }

            // Retirar a primeira barra
            if (DescricaoComponente.Length > 1)
                DescricaoComponente = Strings.Right(DescricaoComponente, Strings.Len(DescricaoComponente) - 1);
            if (DescricaoPercentagemComponente.Length > 1)
                DescricaoPercentagemComponente = Strings.Right(DescricaoPercentagemComponente, Strings.Len(DescricaoPercentagemComponente) - 1) + "%";


            if (DescricaoPercentagemComponente.Length > 1)
                DescricaoArtigo = DescricaoArtigo + " " + DescricaoPercentagemComponente;
            if (DescricaoComponente.Length > 1)
                DescricaoArtigo = DescricaoArtigo + " " + DescricaoComponente;

            if (radioButtonNm.Checked)
                DescricaoArtigo = DescricaoArtigo + " (" + dsArtigosMundifios._NESel.DescricaoNm + ")";

            // * retirar espaço no inicio
            if (DescricaoArtigo.StartsWith(" "))
                DescricaoArtigo = Strings.Right(DescricaoArtigo, Strings.Len(DescricaoArtigo) - 1);
            // * retirar espaço no fim
            if (DescricaoArtigo.EndsWith(" "))
                DescricaoArtigo = Strings.Left(DescricaoArtigo, Strings.Len(DescricaoArtigo) - 1);

            return DescricaoArtigo;
        }

        private void AtribuirDescricaoExtra(bool bInterna, bool bExterna)
        {
            // Me.txtDescExtraArtigo.Text = Me.DsArtigosMundifios.GerarDescricaoExtraArtigo(CurrentGrupo)

            string DescricaoExtraInterna = string.Empty;
            string DescricaoExtraExterna = string.Empty;

            GerarDescricaoExtrasArtigo(CurrentGrupo, ref DescricaoExtraInterna, ref DescricaoExtraExterna);


            if (bInterna)
                textEditDescricaoExtraArtigo.Text = DescricaoExtraInterna;
            if (bExterna)
                textEditDescricaoExtraExternaArtigo.Text = DescricaoExtraExterna;
        }

        private void AnularArtigo()
        {
            // # Abrir empresa
            // Dim sqltranspri As New Motor.PrimaveraERP.Transacao(gPlataformaERP, item.Empresa, gUtilizadorPRI, gPasswordPRI)

            Dictionary<string, string> EmpresasErro = new Dictionary<string, string>();
            string EmpresaErroAtual = "";
            string Aviso = "";


            try
            {

                // Verificar se é possível anular o artigo em todas as empresas
                foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
                {
                    if (!(dsArtigosMundifios.ValidaRemocaoVMP(textEditCodigoArtigo.Text, item.ConnectionString)))
                        return;

                    //PriV100Api.AbreEmpresa(PriV100Api.BSO.Contexto.TipoPlataforma, item.Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);
                    PriV100Api.BSO.AbreEmpresaTrabalho(PriV100Api.BSO.Contexto.TipoPlataforma, item.Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);

                    // Verificar se é possível eliminar o artigo nesta empresa. Caso contrário, adiciono ao Dicionario a empresa e o motivo pelo qual não é possível remover o artigo
                    if (!PriV100Api.BSO.Base.Artigos.ValidaRemocao(textEditCodigoArtigo.Text, Aviso))
                        EmpresasErro.Add(item.Empresa, Aviso);

                    PriV100Api.BSO.FechaEmpresaTrabalho();
                }

                // Verifico se existiu algum problema em alguma empresa..
                if (EmpresasErro.Count > 0)
                {
                    // Se sim, listo-os
                    string TextoErro = "";
                    foreach (var empresa in EmpresasErro)
                        TextoErro = Constants.vbNewLine + TextoErro + "-" + empresa.Key.ToString();
                    MessageBox.Show("O Artigo não pode ser anulado uma vez que já foi movimentado na(s) seguinte(s) empresa(s) " + Constants.vbNewLine + TextoErro, "Operação não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }


                // Novamente para todas as empresas do Grupo...
                foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
                {
                    EmpresaErroAtual = item.Empresa;
                    // Abrir empresa de Trabalho para a Empresa atual
                    PriV100Api.AbreEmpresa(PriV100Api.BSO.Contexto.TipoPlataforma, item.Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);

                    // Eliminar os registos das tabelas auxiliares
                    if (dsArtigosMundifios.EliminaRegistoTabelasComSelecaoMultipla(textEditCodigoArtigo.Text, item.ConnectionString))
                    {

                        // Eliminar o Artigo
                        PriV100Api.BSO.Base.Artigos.Remove(textEditCodigoArtigo.Text);
                        //throw new Exception("Empresa: " + EmpresaErroAtual + " - " + Aviso);
                    }

                    PriV100Api.BSO.FechaEmpresaTrabalho();
                }

                barButtonItemNovo_ItemClick(null, null);
            }
            catch (Exception ex)
            {
                PriV100Api.BSO.FechaEmpresaTrabalho();
                MessageBox.Show(ex.Message, "Problemas na anulaão do artigo na empresa " + EmpresaErroAtual, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                dsArtigosMundifios.AdptTiposArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
                dsArtigosMundifios.AdptCaracteristicasArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
                dsArtigosMundifios.AdptComponentesArtigo.changeConnection(PriV100Api.VSO.Contexto.ConnectionStringInstancia);
            }

        }
        #endregion

        #region Eventos Grelha

        private void vmpGridViewDadosAuxiliares_ColumnChanged(object sender, EventArgs e)
        {

            try
            {

                DsArtigosMundifios.DadosAuxiliaresECRARow activeRow;
                activeRow = (DsArtigosMundifios.DadosAuxiliaresECRARow)vmpGridViewDadosAuxiliares.GetDataRow(vmpGridViewDadosAuxiliares.FocusedRowHandle);
                if (activeRow != null)
                {
                    switch (vmpGridViewDadosAuxiliares.FocusedColumn.FieldName)
                    {
                        case "ArmazemSugestao":
                            {
                                activeRow.ArmazemSugestaoDescricao = dsArtigosMundifios.GetDescricaoArmazem(dsArtigosMundifios.GetConnectionString(activeRow.Empresa), activeRow.ArmazemSugestao);
                                break;
                            }

                        case "TipoArtigo":
                            {
                                activeRow.TipoArtigoDescricao = dsArtigosMundifios.GetDescricaoTipoArtigo(dsArtigosMundifios.GetConnectionString(activeRow.Empresa), activeRow.TipoArtigo);
                                break;
                            }

                        case "GrupoTaxaDesperdicio":
                            {
                                DsArtigosMundifios.TDU_GrupoTaxaDesperdicioRow GrupoTaxa = dsArtigosMundifios.GetGrupoTaxaDesperdicio(dsArtigosMundifios.GetConnectionString(activeRow.Empresa), activeRow.GrupoTaxaDesperdicio);

                                if (GrupoTaxa != null)
                                {
                                    activeRow.GrupoTaxaDesperdiciodescricao = GrupoTaxa.CDU_Descricao;
                                    activeRow.GrupoTaxaDesperdicioTaxa = GrupoTaxa.CDU_Taxa;
                                }
                                else
                                {
                                    activeRow.GrupoTaxaDesperdiciodescricao = string.Empty;
                                    activeRow.GrupoTaxaDesperdicioTaxa = 0;
                                }

                                break;
                            }

                        case "IntrastatPautal":
                            {
                                activeRow.IntrastatPautalDescricao = dsArtigosMundifios.GetDescricaoIntrastatByIntrastat(dsArtigosMundifios.GetConnectionString(activeRow.Empresa), activeRow.IntrastatPautal);
                                break;
                            }

                        case "CodigoAntigo":
                            {
                                activeRow.CodigoAntigoDescricao = dsArtigosMundifios.GetDescricaoArtigoByArtigo(dsArtigosMundifios.GetConnectionString(activeRow.Empresa), activeRow.CodigoAntigo);
                                break;
                            }

                        case "TipoComponenteDescricao":
                            {
                                switch (activeRow.TipoComponenteDescricao)
                                {
                                    case "Artigo Simples":
                                        {
                                            activeRow.TipoComponente = 0;
                                            break;
                                        }

                                    case "Conjunto de Artigos":
                                        {
                                            activeRow.TipoComponente = 1;
                                            break;
                                        }

                                    case "Artigo Composto":
                                        {
                                            activeRow.TipoComponente = 2;
                                            break;
                                        }

                                    default:
                                        {
                                            activeRow.TipoComponente = 9; // Nas validações testa se for > 2 dá erro
                                            break;
                                        }
                                }

                                break;
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void vmpGridViewDadosAuxiliares_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {


        }

        private void vmpGridViewDadosAuxiliares_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                string CampoGrelha = vmpGridViewDadosAuxiliares.FocusedColumn.FieldName;
                DsArtigosMundifios.DadosAuxiliaresECRARow activeRow = (DsArtigosMundifios.DadosAuxiliaresECRARow)vmpGridViewDadosAuxiliares.GetDataRow(vmpGridViewDadosAuxiliares.FocusedRowHandle);
                frmListaMundifiosView frm;

                switch (CampoGrelha)
                {
                    case "ArmazemSugestao":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.Armazens, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("ArmazemSugestao"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("ArmazemSugestaoDescricao")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }
                    case "ArmazemSugestaoDescricao":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.Armazens, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("ArmazemSugestao"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("ArmazemSugestaoDescricao")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }

                    case "GrupoTaxaDesperdicioTaxa":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.GrupoTaxaDesperdicio, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicio"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicioDescricao"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicioTaxa")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }

                    case "GrupoTaxaDesperdicioDescricao":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.GrupoTaxaDesperdicio, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicio"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicioDescricao"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicioTaxa")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }

                    case "GrupoTaxaDesperdicio":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.GrupoTaxaDesperdicio, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicio"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicioDescricao"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("GrupoTaxaDesperdicioTaxa")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }
                    case "IntrastatPautalDescricao":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.IntrastatPautal, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("IntrastatPautal"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("IntrastatPautalDescricao")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }
                    case "IntrastatPautal":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.IntrastatPautal, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("IntrastatPautal"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("IntrastatPautalDescricao")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }

                    case "CodigoAntigoDescricao":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.CodigoAntigo, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("CodigoAntigo"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("CodigoAntigoDescricao")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }
                    case "CodigoAntigo":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.CodigoAntigo, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("CodigoAntigo"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("CodigoAntigoDescricao")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }

                    case "TipoArtigoDescricao":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.TipoArtigo, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("TipoArtigo"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("TipoArtigoDescricao")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }
                    case "TipoArtigo":
                        {
                            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaMundifiosView));
                            frm = result.Result;
                            frm.ShowDialog(Geral.ListagemMundifios.TipoArtigo, new ListParameterDataRow(activeRow, dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("TipoArtigo"), dsArtigosMundifios.DadosAuxiliaresECRA.Columns.IndexOf("TipoArtigoDescricao")), dsArtigosMundifios.GetConnectionString(activeRow.Empresa));
                            break;
                        }
                }
            }
        }
        #region Familias
        private void vmpGridViewFamilias_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DsArtigosMundifios.FamiliasRow activeRowFamilias;

                activeRowFamilias = (DsArtigosMundifios.FamiliasRow)vmpGridViewFamilias.GetDataRow(vmpGridViewFamilias.FocusedRowHandle);

                bindingSourceSubFamilias.Filter = "Familia = '" + activeRowFamilias.Familia.ToString() + "'";

                PosicionarSubFamilia();
            }

            // 02/02/2017 A pedido da Dra mafalda (telefone) lista sempre as mesmas referencias, independentemente das familias.
            // AtualizarReferencias(activeRowFamilias.Familia.ToString, e.LastRow, gridFamilias.Row)

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bindingSourceSubFamilias.Filter = "Familia = '" + "" + "'";
            }
        }

        private void vmpGridViewFamilias_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (vmpGridViewFamilias.RowCount < 0)
                    return;

                if (!vmpGridViewFamilias.ActiveFilterEnabled)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.FamiliasRow activeRowFamilias;
                activeRowFamilias = (DsArtigosMundifios.FamiliasRow)vmpGridViewFamilias.GetDataRow(vmpGridViewFamilias.FocusedRowHandle);

                var Estado = activeRowFamilias.Sel;


                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.Familias, false);

                // DefineGrupo(activeRowFamilias.CDU_Grupo)

                activeRowFamilias.Sel = !Estado;

                // Garantir que existe sempre uma família selecionada!
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.Familias) == 0)
                    activeRowFamilias.Sel = true;

                // #
                bindingSourceSubFamilias.Filter = "Familia = '" + activeRowFamilias.Familia.ToString() + "'";

                SelecionarPrimeiroRegistoSubFamilias();

                AlterouPropriedades(true);

                DefineGrupo(activeRowFamilias.CDU_Grupo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bindingSourceSubFamilias.Filter = "Familia = '" + "" + "'";
            }

            //gridFamilias.Refresh();
        }

        private void vmpGridViewFamilias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewFamilias_MouseUp(null, null);
        }

        private void vmpGridViewFamilias_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.FamiliasRow activeRow;
                activeRow = (DsArtigosMundifios.FamiliasRow)vmpGridViewFamilias.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region subfamilias
        private void vmpGridControlSubFamilias_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (vmpGridViewSubFamilias.FocusedRowHandle < 0)
                    return;

                if (!vmpGridViewSubFamilias.ActiveFilterEnabled)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.SubFamiliasRow activeRow;
                activeRow = (DsArtigosMundifios.SubFamiliasRow)vmpGridViewSubFamilias.GetDataRow(vmpGridViewSubFamilias.FocusedRowHandle);

                var Estado = activeRow.Sel;

                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.SubFamilias, false);
                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.SubFamilias) == 0)
                    activeRow.Sel = true;

                switch (activeRow.SubFamilia)
                {
                    case "02":
                        {
                            LimparTipo(false);

                            int y = 0;
                            foreach (DsArtigosMundifios.VMP_ART_TipoRow tipo in dsArtigosMundifios.VMP_ART_Tipo.Select("Codigo = '002'"))
                            {
                                tipo.Sel = true;
                                vmpGridViewPrograma.FocusedRowHandle = y;
                            }

                            vmpGridViewPrograma.FocusedRowHandle = dsArtigosMundifios.GetIndexRow(dsArtigosMundifios.VMP_ART_Tipo, "Codigo = 002");
                            break;
                        }

                    case "03":
                        {
                            LimparTipo(false);

                            foreach (DsArtigosMundifios.VMP_ART_TipoRow tipo in dsArtigosMundifios.VMP_ART_Tipo.Select("Codigo = '001'"))
                                tipo.Sel = true;

                            vmpGridViewTipos.FocusedRowHandle = dsArtigosMundifios.GetIndexRow(dsArtigosMundifios.VMP_ART_Tipo, "Codigo = 001");
                            break;
                        }

                    default:
                        {
                            LimparTipo(false);

                            foreach (DsArtigosMundifios.VMP_ART_TipoRow tipo in dsArtigosMundifios.VMP_ART_Tipo.Select("Codigo = '000'"))
                                tipo.Sel = true;

                            vmpGridViewTipos.FocusedRowHandle = dsArtigosMundifios.GetIndexRow(dsArtigosMundifios.VMP_ART_Tipo, "Codigo = 000");
                            break;
                        }
                }


                AlterouPropriedades(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridSubFamilias.Refresh();
        }

        private void vmpGridViewSubFamilias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridControlSubFamilias_MouseUp(null, null);
        }
        private void vmpGridViewSubFamilias_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.SubFamiliasRow activeRow;
                activeRow = (DsArtigosMundifios.SubFamiliasRow)vmpGridViewSubFamilias.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region componentes


        private void vmpGridViewComponentes_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (dsArtigosMundifios.VMP_ART_Componente.ObtemTotalPercentagem() != 100)
                e.Appearance.BackColor = Color.Red;
            else
                e.DefaultDraw();
        }

        private void vmpGridViewComponentes_ColumnChanged(object sender, EventArgs e)
        {
            // Garantir que o valor colocado é positivo e sem decimais
            DsArtigosMundifios.VMP_ART_ComponenteRow activeRow;
            activeRow = (DsArtigosMundifios.VMP_ART_ComponenteRow)vmpGridViewComponentes.GetDataRow(vmpGridViewComponentes.FocusedRowHandle);

            if (activeRow != null)
            {

                try
                {
                    if (activeRow != null && Convert.ToInt32(activeRow.Percentagem) < 0)
                        activeRow.Percentagem = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    activeRow.Percentagem = 0;
                }



                activeRow.Percentagem = (int)Math.Round(Convert.ToDecimal(activeRow.Percentagem), 0);
                if (Convert.ToInt32(activeRow.Percentagem) > 0)
                    activeRow.Sel = true;
                else
                    activeRow.Sel = false;
                //this.gridComponentes.UpdateData();
                bindingSourceComponentes.EndEdit();

                CalcularTotais();
                AlterouPropriedades();

            }
        }
        private void RetirarSelecaoDosRegistosSemPercentagem(Guid id = default(Guid))
        {
            foreach (DsArtigosMundifios.VMP_ART_ComponenteRow item in dsArtigosMundifios.VMP_ART_Componente)
            {
                // Coloquei o filtro pelo Id pq depois do click, o sel = true mas a % = 0 e devo manter a seleção
                if (Convert.ToInt32(item.Percentagem) == 0 & item.Sel == true & item.Id != id)
                    item.Sel = false;
            }
        }


        private void vmpGridViewComponentes_MouseUp(object sender, MouseEventArgs e)
        {

            try
            {

                bindingSourceComponentes.EndEdit();

                if (vmpGridViewComponentes.FocusedRowHandle < 0)
                    return;


                if (!vmpGridViewComponentes.ActiveFilterEnabled)
                    return;

                // If e.LastRow = gridComponentes.Row Then Exit Sub

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_ComponenteRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ComponenteRow)vmpGridViewComponentes.GetDataRow(vmpGridViewComponentes.FocusedRowHandle);

                var Estado = activeRow.Sel;
                // Me.DsArtigosMundifios.RetirarSelecao(DsArtigosMundifios.VMP_ART_Componente)

                activeRow.Sel = !Estado;
                if (activeRow.Sel == false)
                    activeRow.Percentagem = 0;

                if (vmpGridViewComponentes.FocusedRowHandle > 0)
                    vmpGridViewComponentes.FocusedColumn = vmpGridViewComponentes.Columns["Percentagem"];

                RetirarSelecaoDosRegistosSemPercentagem(activeRow.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CalcularTotais();
            AlterouPropriedades();
            // this.gridComponentes.resh();
        }
        private void vmpGridViewComponentes_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DsArtigosMundifios.VMP_ART_ComponenteRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ComponenteRow)vmpGridViewComponentes.GetDataRow(vmpGridViewComponentes.FocusedRowHandle);
                RetirarSelecaoDosRegistosSemPercentagem(activeRow.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void vmpGridViewComponentes_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                e.TotalValue = dsArtigosMundifios.VMP_ART_Componente.ObtemTotalPercentagem();
                //vmpGridViewComponentes.Columns["Percentagem"].FooterText = dsArtigosMundifios.VMP_ART_Componente.ObtemTotalPercentagem;
            }
        }
        private void vmpGridViewComponentes_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    bindingSourceComponentes.EndEdit();
                    // AddHandler gridComponentes.AfterColUpdate, AddressOf gridComponentes_AfterColUpdate


                    DsArtigosMundifios.VMP_ART_ComponenteRow activeRow;
                    activeRow = (DsArtigosMundifios.VMP_ART_ComponenteRow)vmpGridViewComponentes.GetDataRow(vmpGridViewComponentes.FocusedRowHandle);
                    if (Convert.ToInt32(activeRow.Percentagem) == 0)
                        activeRow.Sel = !activeRow.Sel;
                    else
                        activeRow.Sel = true;

                    RetirarSelecaoDosRegistosSemPercentagem(activeRow.Id);

                    CalcularTotais();
                    AlterouPropriedades();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void vmpGridViewComponentes_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_ComponenteRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ComponenteRow)vmpGridViewComponentes.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region NE 

        private void vmpGridViewNE_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_NERow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_NERow)vmpGridViewNE.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridControlNE_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (vmpGridViewNE.FocusedRowHandle < 0)
                    return;

                if (!vmpGridViewNE.ActiveFilterEnabled)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_NERow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_NERow)vmpGridViewNE.GetDataRow(vmpGridViewNE.FocusedRowHandle);

                var Estado = activeRow.Sel;

                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_NE, false);
                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_NE) == 0)
                    activeRow.Sel = true;

                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridNE.resh();
        }

        private void vmpGridControlNE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridControlNE_MouseUp(null, null);
        }

        #endregion

        #region Tipos

        private void vmpGridViewTipos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_TipoRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_TipoRow)vmpGridViewTipos.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridViewTipos_ColumnChanged(object sender, EventArgs e)
        {
            try
            {
                //bindingSourceTipos.EndEdit();

                // Quando Carrego no bit da descrição extra, não posso alterar a descrição interna! Porque depois vai colocar sempre no final da descrição o atributo que retirei.
                // A pedido do Eng.º Joaquim Costa dia 2017-11-21
                if (vmpGridViewTipos.FocusedColumn != null && vmpGridViewTipos.FocusedColumn.FieldName == "DescricaoExtra")
                {
                    // # apanhar linha do dataset
                    DsArtigosMundifios.VMP_ART_TipoRow activeRow;
                    activeRow = (DsArtigosMundifios.VMP_ART_TipoRow)vmpGridViewTipos.GetDataRow(vmpGridViewTipos.FocusedRowHandle);
                    DefinirOrdenacao("T" + activeRow.Codigo, activeRow.DescricaoExtra & activeRow.Sel);
                    AtribuirDescricaoExtra(false, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridViewTipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewTipos_MouseUp(null, null);
        }

        private void vmpGridViewTipos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!vmpGridViewTipos.ActiveFilterEnabled)
                    //return;

                    // this.gridTipos.UpdateData();
                    bindingSourceTipos.EndEdit();

                if (vmpGridViewTipos.FocusedRowHandle < 0)
                    return;

                string CampoGrelha = vmpGridViewTipos.FocusedColumn.FieldName;
                if (CampoGrelha == "DescricaoExtra")
                    return;
                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_TipoRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_TipoRow)vmpGridViewTipos.GetDataRow(vmpGridViewTipos.FocusedRowHandle);

                // Dim CampoGrelha As String = gridTipos.Splits(0).DisplayColumns(gridTipos.Col).DataColumn.DataField
                // If CampoGrelha = "DescricaoExtra" Then
                // DefinirOrdenacao("T" & activeRow.Codigo, activeRow.DescricaoExtra And activeRow.Sel)
                // AlterouPropriedades()
                // Exit Sub
                // End If

                var Estado = activeRow.Sel;


                if (dsArtigosMundifios.VMP_ART_Tipo.Select("Sel = 'True'").Length == 1)
                    dsArtigosMundifios.VMP_ART_Tipo[0].Sel = true;

                // Caso selecione um Tipo, retiro a seleção do N/A
                if (dsArtigosMundifios.VMP_ART_Tipo.Select("Sel = 'True'").Length > 0)
                    dsArtigosMundifios.VMP_ART_Tipo[0].Sel = false;

                activeRow.Sel = !Estado;

                DefinirOrdenacao("T" + activeRow.Codigo, activeRow.DescricaoExtra & activeRow.Sel);
                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridTipos.resh();
        }


        #endregion

        #region Limpar

        private void LimparTudo(bool Familia = true)
        {
            if (Familia)
            {
                LimparFamilia(true);
                LimparSubFamilia(true);
            }

            LimparComponente(true);
            LimparNE(true);
            LimparTipo(true);
            LimparReferencia(true);
            LimparTorcao1(true);
            LimparTorcao2(true);
            LimparCone(true);
            LimparCaracteristica(true);
            LimparPrograma(true);
            LimparDimensao(true);
            LimparTexturizacao(true);
            LimparCategoria(true);

            CalcularTotais();

            ApagarDescricoes();

            ApagarOutrosCampos();

            AtivarDesaticarCheckBoxDescExtra(true);

            ApagarRbNeNm();

            PosicionarDadosAuxiliares(false);
        }

        private void ApagarOutrosCampos()
        {
            checkEditArtigoAnulado.Checked = false;
            memoEditObservacoes.Text = "";
        }

        private void LimparFamilia(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.Familias, SelPrimeiro);
            //gridFamilias.Refresh();
        }

        private void LimparSubFamilia(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.SubFamilias, SelPrimeiro);
            if (vmpGridViewSubFamilias.FocusedRowHandle > 0)
            {
                DsArtigosMundifios.FamiliasRow FamiliaAtiva;
                FamiliaAtiva = (DsArtigosMundifios.FamiliasRow)vmpGridViewFamilias.GetDataRow(vmpGridViewFamilias.FocusedRowHandle);
                vmpGridViewSubFamilias.FocusedRowHandle = 0;
            }
            if (SelPrimeiro)
                vmpGridControlSubFamilias_MouseUp(null, null);
        }

        private void LimparComponente(bool SelPrimeiro)
        {
            dsArtigosMundifios.RemoverPercentagem(dsArtigosMundifios.VMP_ART_Componente);
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Componente, SelPrimeiro);

            if (vmpGridViewComponentes.RowCount > 0)
                vmpGridViewComponentes.FocusedRowHandle = 0;
        }

        private void LimparNE(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_NE, SelPrimeiro);
            if (vmpGridViewNE.RowCount > 0)
                vmpGridViewNE.FocusedRowHandle = 0;
        }

        private void LimparTipo(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Tipo, SelPrimeiro);
            if (vmpGridViewTipos.RowCount > 0)
                vmpGridViewTipos.FocusedRowHandle = 0;
        }

        private void LimparReferencia(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Referencia, SelPrimeiro);
            if (vmpGridViewReferencias.RowCount > 0)
                vmpGridViewReferencias.FocusedRowHandle = 0;
        }

        private void LimparTorcao1(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Torcao1, SelPrimeiro);
            if (vmpGridViewTorcao1.RowCount > 0)
                vmpGridViewTorcao1.FocusedRowHandle = 0;
        }

        private void LimparTorcao2(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Torcao2, SelPrimeiro);
            if (vmpGridViewTorcao2.RowCount > 0)
                vmpGridViewTorcao2.FocusedRowHandle = 0;
        }

        private void LimparCone(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Cone, SelPrimeiro);
            if (vmpGridViewCone.RowCount > 0)
                vmpGridViewCone.FocusedRowHandle = 0;
        }

        private void LimparCaracteristica(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Caracteristica, SelPrimeiro);
            if (vmpGridViewCaracteristicas.RowCount > 0)
                vmpGridViewCaracteristicas.FocusedRowHandle = 0;
        }

        private void LimparPrograma(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Programa, SelPrimeiro);
            if (vmpGridViewPrograma.RowCount > 0)
                vmpGridViewPrograma.FocusedRowHandle = 0;
        }

        private void LimparDimensao(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Dimensao, SelPrimeiro);
            if (vmpGridViewDimensao.RowCount > 0)
                vmpGridViewDimensao.FocusedRowHandle = 0;
        }

        private void LimparTexturizacao(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Texturizacao, SelPrimeiro);
            if (vmpGridViewTexturizacao.RowCount > 0)
                vmpGridViewTexturizacao.FocusedRowHandle = 0;
        }

        private void LimparCategoria(bool SelPrimeiro)
        {
            dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Categoria, SelPrimeiro);
            if (vmpGridViewCategoria.RowCount > 0)
                vmpGridViewCategoria.FocusedRowHandle = 0;
        }
        #endregion

        #region Referencias
        private void vmpGridViewReferencias_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_ReferenciaRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ReferenciaRow)vmpGridViewReferencias.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridViewReferencias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewReferencias_MouseUp(sender, null);
        }

        private void vmpGridViewReferencias_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (vmpGridViewReferencias.FocusedRowHandle < 0)
                    return;

                if (!vmpGridViewReferencias.ActiveFilterEnabled)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_ReferenciaRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ReferenciaRow)vmpGridViewReferencias.GetDataRow(vmpGridViewReferencias.FocusedRowHandle);

                var Estado = activeRow.Sel;
                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Referencia, false);

                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_Referencia) == 0)
                    activeRow.Sel = true;

                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridReferencias.Refresh();
        }
        #endregion

        #region torcao 1

        private void vmpGridViewTorcao1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_Torcao1Row activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_Torcao1Row)vmpGridViewTorcao1.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void vmpGridViewTorcao1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewTorcao1_MouseUp(null, null);
        }

        private void vmpGridViewTorcao1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (vmpGridViewTorcao1.FocusedRowHandle < 0)
                    return;

                if (!vmpGridViewTorcao1.ActiveFilterEnabled)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_Torcao1Row activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_Torcao1Row)vmpGridViewTorcao1.GetDataRow(vmpGridViewTorcao1.FocusedRowHandle);

                var Estado = activeRow.Sel;
                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Torcao1, false);

                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_Torcao1) == 0)
                    activeRow.Sel = true;

                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridTorcao1.Refresh();
        }
        #endregion

        #region Torcao 2

        private void vmpGridViewTorcao2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_Torcao2Row activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_Torcao2Row)vmpGridViewTorcao2.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridViewTorcao2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewTorcao2_MouseUp(null, null);
        }

        private void vmpGridViewTorcao2_MouseUp(object sender, MouseEventArgs e)
        {

            try
            {
                if (vmpGridViewTorcao2.FocusedRowHandle < 0)
                    return;

                if (!vmpGridViewTorcao2.ActiveFilterEnabled)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_Torcao2Row activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_Torcao2Row)vmpGridViewTorcao2.GetDataRow(vmpGridViewTorcao2.FocusedRowHandle);

                var Estado = activeRow.Sel;
                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Torcao2, false);

                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_Torcao2) == 0)
                    activeRow.Sel = true;
                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridTorcao2.Refresh();
        }


        #endregion

        #region Cone
        private void vmpGridViewCone_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_ConeRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ConeRow)vmpGridViewCone.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void vmpGridViewCone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewCone_MouseUp(null, null);
        }

        private void vmpGridViewCone_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!vmpGridViewCone.ActiveFilterEnabled)
                    return;

                if (vmpGridViewCone.FocusedRowHandle < 0)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_ConeRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ConeRow)vmpGridViewCone.GetDataRow(vmpGridViewCone.FocusedRowHandle);

                var Estado = activeRow.Sel;
                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Cone, false);

                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_Cone) == 0)
                    activeRow.Sel = true;

                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridCone.Refresh();
        }



        #endregion

        #region Caracteristica
        private void vmpGridViewCaracteristicas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_CaracteristicaRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_CaracteristicaRow)vmpGridViewCaracteristicas.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void vmpGridViewCaracteristicas_ColumnChanged(object sender, EventArgs e)
        {
            try
            {
                //this.gridCaracteristica.UpdateData();
                bindingSourceCaracteristicas.EndEdit();


                // Quando Carrego no bit da descrição extra, não posso alterar a descrição interna! Porque depois vai colocar sempre no final da descrição o atributo que retirei.
                // A pedido do Eng.º Joaquim Costa dia 2017-11-21
                if (vmpGridViewCaracteristicas.FocusedColumn != null && vmpGridViewCaracteristicas.FocusedColumn.FieldName == "DescricaoExtra")
                {

                    // # apanhar linha do dataset
                    DsArtigosMundifios.VMP_ART_CaracteristicaRow activeRow;
                    activeRow = (DsArtigosMundifios.VMP_ART_CaracteristicaRow)vmpGridViewCaracteristicas.GetDataRow(vmpGridViewCaracteristicas.FocusedRowHandle);
                    DefinirOrdenacao("C" + activeRow.Codigo, activeRow.DescricaoExtra & activeRow.Sel);
                    AtribuirDescricaoExtra(false, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void vmpGridViewCaracteristicas_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void vmpGridViewCaracteristicas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewCaracteristicas_MouseUp(null, null);

        }

        private void vmpGridViewCaracteristicas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!vmpGridViewCaracteristicas.ActiveFilterEnabled)
                    return;

                //this.gridCaracteristica.UpdateData();
                bindingSourceCaracteristicas.EndEdit();

                if (vmpGridViewCaracteristicas.FocusedRowHandle < 0)
                    return;

                string CampoGrelha = vmpGridViewCaracteristicas.FocusedColumn.FieldName;
                if (CampoGrelha == "DescricaoExtra")
                    return;


                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_CaracteristicaRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_CaracteristicaRow)vmpGridViewCaracteristicas.GetDataRow(vmpGridViewCaracteristicas.FocusedRowHandle);

                var Estado = activeRow.Sel;

                if (dsArtigosMundifios.VMP_ART_Caracteristica.Select("Sel = 'True'").Length == 1)
                    dsArtigosMundifios.VMP_ART_Caracteristica[0].Sel = true;

                // Caso selecione uma caracteristica, retiro a seleção do N/A
                if (dsArtigosMundifios.VMP_ART_Caracteristica.Select("Sel = 'True'").Length > 0)
                    dsArtigosMundifios.VMP_ART_Caracteristica[0].Sel = false;


                activeRow.Sel = !Estado;

                DefinirOrdenacao("C" + activeRow.Codigo, activeRow.DescricaoExtra & activeRow.Sel);

                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridCaracteristica.Refresh();
        }


        #endregion

        #region Programa
        private void vmpGridViewPrograma_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_ProgramaRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ProgramaRow)vmpGridViewPrograma.GetDataRow(e.RowHandle);

                if (activeRow != null && activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridViewPrograma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewPrograma_MouseUp(null, null);
        }

        private void vmpGridViewPrograma_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!vmpGridViewPrograma.ActiveFilterEnabled)
                    return;

                if (vmpGridViewPrograma.FocusedRowHandle < 0)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_ProgramaRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_ProgramaRow)vmpGridViewPrograma.GetDataRow(vmpGridViewPrograma.FocusedRowHandle);

                var Estado = activeRow.Sel;
                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Programa, false);

                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_Programa) == 0)
                    activeRow.Sel = true;

                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridPrograma.Refresh();
        }

        #endregion

        #region Dimensao

        private void vmpGridViewDimensao_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_DimensaoRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_DimensaoRow)vmpGridViewDimensao.GetDataRow(e.RowHandle);

                if (activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridViewDimensao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewDimensao_MouseUp(null, null);
        }

        private void vmpGridViewDimensao_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!vmpGridViewDimensao.ActiveFilterEnabled)
                    return;

                if (vmpGridViewDimensao.FocusedRowHandle < 0)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_DimensaoRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_DimensaoRow)vmpGridViewDimensao.GetDataRow(vmpGridViewDimensao.FocusedRowHandle);

                var Estado = activeRow.Sel;
                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Dimensao, false);

                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_Dimensao) == 0)
                    activeRow.Sel = true;

                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridDimensao.Refresh();

        }

        #endregion

        #region Categoria
        private void vmpGridViewCategoria_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_CategoriaRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_CategoriaRow)vmpGridViewCategoria.GetDataRow(e.RowHandle);

                if (activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridViewCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewCategoria_MouseUp(null, null);
        }

        private void vmpGridViewCategoria_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!vmpGridViewCategoria.ActiveFilterEnabled)
                    return;

                if (vmpGridViewCategoria.FocusedRowHandle < 0)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_CategoriaRow activeRowCategoria;
                activeRowCategoria = (DsArtigosMundifios.VMP_ART_CategoriaRow)vmpGridViewCategoria.GetDataRow(vmpGridViewCategoria.FocusedRowHandle);
                var Estado = activeRowCategoria.Sel;


                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Categoria, false);
                activeRowCategoria.Sel = !Estado;

                // Garantir que existe sempre uma família selecionada!
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_Categoria) == 0)
                    activeRowCategoria.Sel = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //gridCategoria.Refresh();

            AlterouPropriedades();
        }
        #endregion

        #region Texturizacao
        private void vmpGridViewTexturizacao_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_TexturizacaoRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_TexturizacaoRow)vmpGridViewTexturizacao.GetDataRow(e.RowHandle);

                if (activeRow.Sel)
                {
                    e.Appearance.BackColor = CorSelecao;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vmpGridViewTexturizacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                vmpGridViewTexturizacao_MouseUp(null, null);
        }

        private void vmpGridViewTexturizacao_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (vmpGridViewTexturizacao.FocusedRowHandle < 0)
                    return;

                if (!vmpGridViewTexturizacao.ActiveFilterEnabled)
                    return;

                // # apanhar linha do dataset
                DsArtigosMundifios.VMP_ART_TexturizacaoRow activeRow;
                activeRow = (DsArtigosMundifios.VMP_ART_TexturizacaoRow)vmpGridViewTexturizacao.GetDataRow(vmpGridViewTexturizacao.FocusedRowHandle);

                var Estado = activeRow.Sel;
                dsArtigosMundifios.RetirarSelecao(dsArtigosMundifios.VMP_ART_Texturizacao, false);

                activeRow.Sel = !Estado;

                // Garantir que existe sempre um registo selecionado
                if (dsArtigosMundifios.GetNumeroRegistosSelecionados(dsArtigosMundifios.VMP_ART_Texturizacao) == 0)
                    activeRow.Sel = true;

                AlterouPropriedades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // gridTexturizacao.Refresh();
        }
        #endregion



        #endregion

        #region Descricao Extra
        private void DefinirOrdenacao(string Codigo, bool Adicionar)
        {
            if (Conversion.Val(Strings.Right(Codigo, Strings.Len(Codigo) - 1)) == 0)
                return;

            if (Adicionar)
            {
                // se for para adicionar...

                // 1º verifica mesmo se nao tem lá nada.. se não tiver é que adiciona de facto!
                if (Strings.InStr(textEditOrdenacaoDescricaoExtra.Text, Codigo) == 0)
                    textEditOrdenacaoDescricaoExtra.Text = textEditOrdenacaoDescricaoExtra.Text + Codigo + ";";
            }
            else
                // Retiro o código 
                textEditOrdenacaoDescricaoExtra.Text = Strings.Replace(textEditOrdenacaoDescricaoExtra.Text, Codigo, "");

            // Validações
            if (textEditOrdenacaoDescricaoExtra.Text.Length > 0)
            {
                if (Strings.Left(textEditOrdenacaoDescricaoExtra.Text, 1) == ";")
                    textEditOrdenacaoDescricaoExtra.Text = Strings.Right(textEditOrdenacaoDescricaoExtra.Text, Strings.Len(textEditOrdenacaoDescricaoExtra.Text) - 1);
            }

            textEditOrdenacaoDescricaoExtra.Text = Strings.Replace(textEditOrdenacaoDescricaoExtra.Text, ";;", ";");
        }

        #endregion

        #region Eventos
        private void textEditCodigoArtigo_TextChanged(object sender, EventArgs e)
        {
            AlterouPropriedades(false, true);
        }

        private void textEditCodigoArtigo_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.F4)
                    simpleButtonF4_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        #endregion

        #region Posicionar


        private void PosicionarTudo()
        {

            // LimparTudo()

            // Caso o artigo exista, selecionar todas as grelhas correspondentes!
            PosicionarFamilia();
            PosicionarSubFamilia();
            PosicionarComponentes();
            PosicionarNE();
            // PosicionarTipo()
            PosicionarReferencias();
            PosicionarTorcao1();
            PosicionarTorcao2();
            PosicionarCone();
            PosicionarCaracteristicas();
            Posicionartipos();
            PosicionarPrograma();
            PosicionarDimensao();
            PosicionarTexturizacao();
            PosicionarCategoria();
            PosicionarOutrosDados();
            PosicionarDadosAuxiliares(true);
        }

        private void PosicionarOutrosDados()
        {

            // If Me.DsArtigosMundifios._Artigo.IsCDU_ParafinadoNull Then
            // Me.cbParafinado.Checked = False
            // Else
            checkEditArtigoAnulado.Checked = dsArtigosMundifios._Artigo.ArtigoAnulado;
            // End If

            if (dsArtigosMundifios._Artigo.IsObservacoesNull())
                memoEditObservacoes.Text = "";
            else
                memoEditObservacoes.Text = dsArtigosMundifios._Artigo.Observacoes;
        }

        private void PosicionarPrograma()
        {
            LimparPrograma(false);
            if (dsArtigosMundifios._Artigo.IsCDU_ProgramaNull())
                return;

            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_ProgramaRow r in dsArtigosMundifios.VMP_ART_Programa)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_Programa)
                {
                    vmpGridViewPrograma.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarCone()
        {
            LimparCone(false);
            if (dsArtigosMundifios._Artigo.IsCDU_ConeNull())
                return;

            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_ConeRow r in dsArtigosMundifios.VMP_ART_Cone)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_Cone)
                {
                    vmpGridViewCone.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarTorcao2()
        {
            LimparTorcao2(false);
            if (dsArtigosMundifios._Artigo == null)
                return;

            if (dsArtigosMundifios._Artigo.IsCDU_Torcao2Null())
                return;

            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_Torcao2Row r in dsArtigosMundifios.VMP_ART_Torcao2)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_Torcao2)
                {
                    vmpGridViewTorcao2.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarTorcao1()
        {
            LimparTorcao1(false);
            if (dsArtigosMundifios._Artigo.IsCDU_Torcao1Null())
                return;

            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_Torcao1Row r in dsArtigosMundifios.VMP_ART_Torcao1)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_Torcao1)
                {
                    vmpGridViewTorcao1.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarReferencias()
        {
            LimparReferencia(false);
            if (dsArtigosMundifios._Artigo.IsCDU_ReferenciaNull())
                return;

            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_ReferenciaRow r in dsArtigosMundifios.VMP_ART_Referencia)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_Referencia)
                {
                    vmpGridViewReferencias.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarNE()
        {
            LimparNE(false);
            if (dsArtigosMundifios._Artigo.IsCDU_NENull())
                return;

            // # fazer focus na row do respectivo grupo e familia do artigo
            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_NERow r in dsArtigosMundifios.VMP_ART_NE)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_NE)
                {
                    vmpGridViewNE.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarComponentes()
        {
            LimparComponente(false);
            // Retirar a selação anterior
            // RetirarSelecao(DsArtigosMundifios.VMP_ART_Componente)

            // Garante que a 1ª linha fica selecionada, quer haja registo para selecionar ou não
            if (vmpGridViewComponentes.RowCount > 0)
                vmpGridViewComponentes.FocusedRowHandle = 0;

            // # fazer focus na row do respectivo grupo e familia do artigo
            int y = 0;
            double TotalQtd = 0;

            // 'Para cada registos da tabela de Componentes Artigo carregada ( para cada componentes do artigo)
            // For Each rCA As ArtigosMundifios.VMP_ART_ComponenteArtigoRow In Me.DsArtigosMundifios.VMP_ART_ComponenteArtigo


            // Para cada componente em todos os componentes
            foreach (DsArtigosMundifios.VMP_ART_ComponenteRow r in dsArtigosMundifios.VMP_ART_Componente)
            {
                if (dsArtigosMundifios.VMP_ART_ComponenteArtigo.Select("Idcomponente = '" + r.Id.ToString() + "'", "").Length > 0)
                {
                    foreach (DsArtigosMundifios.VMP_ART_ComponenteArtigoRow Registo in dsArtigosMundifios.VMP_ART_ComponenteArtigo.Select("Idcomponente = '" + r.Id.ToString() + "' AND CodigoArtigo = '" + dsArtigosMundifios._Artigo.Artigo.ToString() + "' ", ""))
                    {
                        vmpGridViewComponentes.FocusedRowHandle = y;
                        r.Sel = true;
                        r.Percentagem = Registo.Percentagem;
                        TotalQtd = TotalQtd + Registo.Percentagem;
                        break;
                    }
                }

                y += 1;
            }

            //vmpGridViewComponentes.Columns["Percentagem"].FooterText = TotalQtd;

            CalcularTotais();
        }

        private void PosicionarCaracteristicas()
        {
            LimparCaracteristica(false);

            // Garante que a 1ª linha fica selecionada, quer haja registo para selecionar ou não
            if (vmpGridViewCaracteristicas.RowCount > 0)
                vmpGridViewCaracteristicas.FocusedRowHandle = 0;

            int y = 0;

            // Para cada Caracteristica em todos as caracteristicas
            foreach (DsArtigosMundifios.VMP_ART_CaracteristicaRow r in dsArtigosMundifios.VMP_ART_Caracteristica)
            {
                if (dsArtigosMundifios.VMP_ART_CaracteristicaArtigo.Select("IdCaracteristica = '" + r.Id.ToString() + "'", "").Length > 0)
                {
                    foreach (DsArtigosMundifios.VMP_ART_CaracteristicaArtigoRow Registo in dsArtigosMundifios.VMP_ART_CaracteristicaArtigo.Select("IdCaracteristica = '" + r.Id.ToString() + "' AND CodigoArtigo = '" + dsArtigosMundifios._Artigo.Artigo.ToString() + "' ", ""))
                    {
                        vmpGridViewCaracteristicas.FocusedRowHandle = y;
                        r.Sel = true;
                        break;
                    }
                }
                y += 1;
            }
        }

        private void Posicionartipos()
        {
            LimparTipo(false);

            // Garante que a 1ª linha fica selecionada, quer haja registo para selecionar ou não
            if (vmpGridViewTipos.RowCount > 0)
                vmpGridViewTipos.FocusedRowHandle = 0;

            int y = 0;

            // Para cada Tipo em todos as Tipos
            foreach (DsArtigosMundifios.VMP_ART_TipoRow r in dsArtigosMundifios.VMP_ART_Tipo)
            {
                if (dsArtigosMundifios.VMP_ART_TipoArtigo.Select("IdTipo = '" + r.Id.ToString() + "'", "").Length > 0)
                {
                    foreach (DsArtigosMundifios.VMP_ART_TipoArtigoRow Registo in dsArtigosMundifios.VMP_ART_TipoArtigo.Select("IdTipo = '" + r.Id.ToString() + "' AND CodigoArtigo = '" + dsArtigosMundifios._Artigo.Artigo.ToString() + "' ", ""))
                    {
                        vmpGridViewTipos.FocusedRowHandle = y;
                        r.Sel = true;
                        break;
                    }
                }
                y += 1;
            }
        }

        private void PosicionarFamilia()
        {

            // RetirarSelecao(DsArtigosMundifios.Familias)

            // If DsArtigosMundifios._Artigo.IsFamiliaNull Then
            // If Me.gridFamilias.RowCount > 0 Then Me.gridFamilias.Row = 0
            // Me.gridFamilias.Refresh()
            // Exit Sub
            // End If

            LimparFamilia(true);

            if (dsArtigosMundifios._Artigo.IsFamiliaNull())
                return;

            // # fazer focus na row do respectivo grupo e familia do artigo
            int y = 0;
            foreach (DsArtigosMundifios.FamiliasRow r in dsArtigosMundifios.Familias)
            {
                if (r.Familia == dsArtigosMundifios._Artigo.Familia)
                {
                    vmpGridViewFamilias.FocusedRowHandle = y;
                    r.Sel = true;
                    bindingSourceSubFamilias.Filter = "Familia = '" + r.Familia.ToString() + "'";
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarSubFamilia()
        {
            LimparSubFamilia(false);

            // Se o artigo não for válido, saio da função
            if (!(dsArtigosMundifios.Artigo.ArtigoValido()))
                return;

            // Se o artigo não tiver subfamília, saio da função
            if (dsArtigosMundifios._Artigo.IsSubFamiliaNull())
                return;

            if (dsArtigosMundifios._FamiliasSel == null)
                return;
            // # fazer focus na row da Subfamilia (isto se o Artigo existir e tiver subfamília)
            int y = 0;
            foreach (DsArtigosMundifios.SubFamiliasRow r in dsArtigosMundifios.SubFamilias.Select("Familia = '" + dsArtigosMundifios._FamiliasSel.Familia + "'", ""))
            {
                if (r.Familia == dsArtigosMundifios._Artigo.Familia & r.SubFamilia == dsArtigosMundifios._Artigo.SubFamilia)
                {
                    vmpGridViewSubFamilias.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarDimensao()
        {
            LimparDimensao(false);
            if (dsArtigosMundifios._Artigo.IsCDU_DimensaoNull())
                return;

            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_DimensaoRow r in dsArtigosMundifios.VMP_ART_Dimensao)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_Dimensao)
                {
                    vmpGridViewDimensao.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarTexturizacao()
        {
            LimparTexturizacao(false);
            if (dsArtigosMundifios._Artigo.IsCDU_TexturizacaoNull())
                return;

            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_TexturizacaoRow r in dsArtigosMundifios.VMP_ART_Texturizacao)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_Texturizacao)
                {
                    vmpGridViewTexturizacao.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }
        }

        private void PosicionarCategoria()
        {
            LimparCategoria(false);

            if (dsArtigosMundifios._Artigo.IsCDU_CategoriaNull())
                return;

            int y = 0;
            foreach (DsArtigosMundifios.VMP_ART_CategoriaRow r in dsArtigosMundifios.VMP_ART_Categoria)
            {
                if (r.Codigo == dsArtigosMundifios._Artigo.CDU_Categoria)
                {
                    vmpGridViewCategoria.FocusedRowHandle = y;
                    r.Sel = true;
                    break;
                }
                y += 1;
            }

        }

        #endregion

        #region Menu
        private void barButtonItemAtualizaAtributos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InicializarForulario();
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GravarDados();
        }

        private void barButtonItemNovo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            // Garantir que a cor das textbox representa um artigo que não existe!
            ColorirTextBoxes(false);

            // Limpar todas as Tabelas
            LimparTudo(false);

            // Apagar o campo Base e parafinado
            ApagarOutrosCampos();

            // Colocar a false todas as cb da desc extra
            AtivarDesaticarCheckBoxDescExtra(true);

            // RadioBottom Nr Nm
            ApagarRbNeNm();

            // Checkbox por linha de Tipos e Caracteristicas
            AtivarCheckBoxAtualizaDescExtra();

            // Colocar por defeito o grupo Fios
            // CurrentGrupo = Grupo.Fios

            // Gerar um novo código
            AtribuirCodigo();

            // Gerar a descrição
            AtribuirDescricao();
            AtribuirDescricaoExtra(true, true);

            AlterouPropriedades();

            // Retirei do sitio #@1@# porque ao carregar nas subfamilias limpava os dados auxiliares. Como este botão passava pelo mesmo sitio que  #@1@#  comentei e passei a função para aqui 
            PosicionarDadosAuxiliares(false);

            isNew = true;

            // Apagar a tabela dos componentes
            dsArtigosMundifios.VMP_ART_ComponenteArtigo.Clear();

            checkEditReferenciasLivres.Checked = false;

            LimparFiltros();

            // Posicionar no tab 1
            TabControl.SelectedTabPage = xtraTabPageCaracteristicas;

            RemoverBloqueiosUtilizadorPosto();


        }

        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AlterouPropriedades(false, true);
        }

        private void barButtonItemRemover_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!Confirmacao(false))
                return;

            AnularArtigo();
        }

        private void barButtonItemCopiar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            AtribuirCodigo();
            isNew = true;
            dsArtigosMundifios.AtualizarArtigoNosComponentes(textEditCodigoArtigo.Text);
            dsArtigosMundifios.AtualizarArtigoNasCaracteristicas(textEditCodigoArtigo.Text);
            dsArtigosMundifios.AtualizarArtigoNosTipos(textEditCodigoArtigo.Text);
            ColorirTextBoxes(false);


        }
        #endregion

        #region Ne Nm

        private void Rb_CheckedChanged(object sender, EventArgs e)
        {

            // Serve porque o inicialize estava a invocar esta função e ao invocar abaixo a função "FormatarGrelhaNE(RbNe.Checked)" e dava erro porque o form ainda não está inicializado
            if (!LoadCompleto)
                return;

            // Formatar a grelha consoante a seleção desejada
            FormatarGrelhaNE(radioButtonNe.Checked);

            AtribuirDescricao();
            AtribuirDescricaoExtra(true, true);
        }

        private void ApagarRbNeNm()
        {
            radioButtonNe.Checked = true;
            radioButtonNm.Checked = false;
        }



        #endregion

        #region  Validacoes

        private bool Confirmacao(bool GravarAtualizar)
        {
            MsgBoxResult Resposta = Constants.vbYes;

            if (GravarAtualizar)
            {
                if (isNew)
                    Resposta = (MsgBoxResult)MessageBox.Show(string.Format("Pretende gravar o artigo {0}?", textEditCodigoArtigo.Text), "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    Resposta = (MsgBoxResult)MessageBox.Show(string.Format("Pretende gravar o artigo {0}?", textEditCodigoArtigo.Text), "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
                Resposta = (MsgBoxResult)MessageBox.Show(string.Format("Pretende gravar o artigo {0}?", textEditCodigoArtigo.Text), "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Resposta == Constants.vbYes)
                return true;
            else
                return false;
        }
        private bool ValidacoesDescricao()
        {
            return dsArtigosMundifios.ValidacoesDescricao();
        }

        private bool ValidacoesPropriedades()
        {

            // Garantir que a grelha não está em modo editável ainda
            bindingSourceComponentes.EndEdit();
            //this.gridComponentes.UpdateData();
            textEditCodigoArtigo.Select();

            return dsArtigosMundifios.ValidacoesPropriedades(textEditCodigoArtigo.Text);
        }

        private bool ValidacoesDadosAuxiliares()
        {
            return dsArtigosMundifios.ValidacoesDadosAuxiliares();
        }


        private bool ValidacoesTodasEmpresas()
        {
            return dsArtigosMundifios.ValidacoesTodasEmpresas(textEditCodigoArtigo.Text, isNew);
        }

        private bool ValidacoesCodigo()
        {
            if (textEditCodigoArtigo.Text.Length == 0)
            {
                MessageBox.Show("Artigo inválido.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }

            if (this.textEditCodigoArtigo.Text.Length != 12)
            {
                MessageBox.Show("A codificação do artigo deve respeitar a seguinte regra:" + Constants.vbNewLine + Constants.vbNewLine + "3 Digitos para o Grupo" + Constants.vbNewLine + "9 Digitos para o código sequêncial.", "Artigo inválido.", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }

            if (dsArtigosMundifios.Devolve1SeFamiliaDefinida() == false)
            {
                MessageBox.Show("Família inválida.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }

            if (dsArtigosMundifios.GetTrueIfArtigoExisteByCodigo(this.textEditCodigoArtigo.Text))
            {
                if (isNew)
                {
                    // Garantir que de facto o artigo não existe. Por ser multiUtilizador, se já exisitir tenho de identificar o próximo livre.
                    // Se for no modo "Novo" gero um novo codigo, porque algum utilizador pode ter criado um artigo entretanto.
                    textEditCodigoArtigo.TextChanged -= textEditCodigoArtigo_TextChanged;
                    this.textEditCodigoArtigo.Text = dsArtigosMundifios._FamiliasSel.Familia + dsArtigosMundifios.GetProximoCodigoLivre().ToString().PadLeft(NrDigitosCodigoArtigo, '0');
                    textEditCodigoArtigo.TextChanged += textEditCodigoArtigo_TextChanged;
                }
                else
                {
                }
            }

            return true;

        }

        #endregion

        private void barButtonItemLimpaDadosAux_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PosicionarDadosAuxiliares(false);
        }

        private void simpleButtonF4_Click(object sender, EventArgs e)
        {
            try
            {
                frmListaGeral frm;
                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaGeral));
                frm = result.Result;
                frm.ShowDialog(Listagem.ArtigoMundifios, new ListParamTextBox(textEditCodigoArtigo, textEditDescricaoArtigo, null));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void radioButtonNe_CheckedChanged(object sender, EventArgs e)
        {
            Rb_CheckedChanged(sender, e);
        }

        private void radioButtonNm_CheckedChanged(object sender, EventArgs e)
        {
            Rb_CheckedChanged(sender, e);
        }

        private void buttonGerirNE_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.NE;
            GerirPropriedades_Click(sender, e);
        }

        private void buttonGerirComponentes_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.Componente;
            GerirPropriedades_Click(sender, e);
        }

        private void buttonGerirTipos_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.Tipo;
            GerirPropriedades_Click(sender, e);
        }

        private void buttonGerirCaracteristicas_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.Caracteristica;
            GerirPropriedades_Click(sender, e);
        }

        private void buttonGerirTorcao1_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.Torcao1;
            GerirPropriedades_Click(sender, e);
        }

        private void buttonGerirTorcao2_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.Torcao2;
            GerirPropriedades_Click(sender, e);
        }

        private void buttonGerirReferencias_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.Referencia;
            GerirPropriedades_Click(sender, e);
        }

        private void buttonGerirCone_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.Cone;
            GerirPropriedades_Click(sender, e);
        }

        private void buttonGerirPrograma_Click(object sender, EventArgs e)
        {
            CreatForm();
            frmArtigo.TabelaAtual = (frmGestaoPropriedadesMundifiosView.Tabela)frmUcGestaoPropriedadeView.Tabela.Programa;
            GerirPropriedades_Click(sender, e);
        }
    }
}
