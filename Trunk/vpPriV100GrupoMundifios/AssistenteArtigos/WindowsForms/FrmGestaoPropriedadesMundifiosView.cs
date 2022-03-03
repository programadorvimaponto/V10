using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    public partial class frmGestaoPropriedadesMundifiosView : CustomForm
    {
        #region Variaveis
        public frmGestaoPropriedadesMundifiosView()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }


        public event ZEventoTesteEventHandler ZEventoTeste;

        public delegate void ZEventoTesteEventHandler();

        private bool LoadCompleto;

        private Tabela _TabelaAtual;
        public Tabela TabelaAtual
        {
            get
            {
                return _TabelaAtual;
            }
            set
            {
                _TabelaAtual = value;
                CarregarDados();
            }
        }
        #endregion
        public enum Tabela
        {
            NE,
            Componente,
            Tipo,
            Caracteristica,
            Torcao1,
            Torcao2,
            Referencia,
            Cone,
            Programa,
            Texturizacao,
            Dimensao,
            Categoria,
            Cor
        }

        private void FrmGestaoPropriedadesMundifiosView_FormClosed(object sender, FormClosedEventArgs e)
        {
            dsPropriedadesMundifios.RemoverBloqueiosUtilizadorPosto();
        }

        private void FrmGestaoPropriedadesMundifiosView_Load(object sender, EventArgs e)
        {
            labelGrafico.Text = "";

            //'Me.DsEmpresasGrupo.GetEmpresasPermitidas()

            CarregarComboBox();

            CarregaDados();

            LoadCompleto = true;
        }
        public void CarregaDados()
        {
            CarregarGrelha();
            FormatarGrelhas();
        }

        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (Gravar())
                MessageBox.Show("Sucesso", "Gestão de " + TabelaAtual.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        #region Funções
        private void CarregarComboBox()
        {
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Clear();
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.NE.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Componente.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Tipo.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Caracteristica.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Torcao1.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Torcao2.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Referencia.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Cone.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Texturizacao.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Dimensao.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Programa.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Categoria.ToString());
            comboBoxEditGestaoPropriedadesMundifios.Properties.Items.Add(Tabela.Cor.ToString());
            comboBoxEditGestaoPropriedadesMundifios.SelectedIndex = (int)TabelaAtual;
        }




        private bool Gravar()
        {
            try
            {
                bool Resultado = true;

                //vmpGridViewGestaoPropriedadesMundifios.UpdateData();

                switch (TabelaAtual)
                {
                    case Tabela.NE:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarNEBD();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosNe();
                            }

                            break;
                        }

                    case Tabela.Componente:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarComponente();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosComponente();
                            }

                            break;
                        }

                    case Tabela.Tipo:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarTipo();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosTipo();
                            }

                            break;
                        }

                    case Tabela.Caracteristica:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarCaracteristica();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosCaracteristica();
                            }

                            break;
                        }

                    case Tabela.Torcao1:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarTorcao1();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosTorcao1();
                            }

                            break;
                        }

                    case Tabela.Torcao2:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarTorcao2();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosTorcao2();
                            }

                            break;
                        }

                    case Tabela.Referencia:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarReferencia();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosReferencia();
                            }

                            break;
                        }

                    case Tabela.Cone:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarCone();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosCone();
                            }

                            break;
                        }

                    case Tabela.Programa:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarPrograma();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosPrograma();
                            }

                            break;
                        }

                    case Tabela.Texturizacao:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarTexturizacao();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosTexturizacao();
                            }

                            break;
                        }

                    case Tabela.Dimensao:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarDimensao();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosDimensao();
                            }

                            break;
                        }

                    case Tabela.Categoria:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarCategoria();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosCategoria();
                            }

                            break;
                        }

                    case Tabela.Cor:
                        {
                            try
                            {
                                dsPropriedadesMundifios.GravarCor();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Resultado = false;
                            }
                            finally
                            {
                                dsPropriedadesMundifios.ReporDadosCor();
                            }

                            break;
                        }
                }

                vmpGridViewGestaoPropriedadesMundifios.FocusedRowHandle = vmpGridViewGestaoPropriedadesMundifios.RowCount;

                return Resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gestão de " + TabelaAtual.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        #endregion

        #region Formatações
        private void FormatarGrelhas()
        {


            switch (TabelaAtual)
            {
                case Tabela.NE:
                    {
                        FormatarGrelhaNE();
                        break;
                    }

                case Tabela.Componente:
                    {
                        FormatarGrelhaComponentes();
                        break;
                    }

                case Tabela.Tipo:
                    {
                        FormatarGrelhaTipos();
                        break;
                    }

                case Tabela.Caracteristica:
                    {
                        FormatarGrelhaCaracteristica();
                        break;
                    }

                case Tabela.Torcao1:
                    {
                        FormatarGrelhaTorcao1();
                        break;
                    }

                case Tabela.Torcao2:
                    {
                        FormatarGrelhaTorcao2();
                        break;
                    }

                case Tabela.Referencia:
                    {
                        FormatarGrelhaReferencias();
                        break;
                    }

                case Tabela.Cone:
                    {
                        FormatarGrelhaCone();
                        break;
                    }

                case Tabela.Programa:
                    {
                        FormatarGrelhaPrograma();
                        break;
                    }

                case Tabela.Texturizacao:
                    {
                        FormatarGrelhaTexturizacao();
                        break;
                    }

                case Tabela.Dimensao:
                    {
                        FormatarGrelhaDimensao();
                        break;
                    }

                case Tabela.Categoria:
                    {
                        FormatarGrelhaCategoria();
                        break;
                    }

                case Tabela.Cor:
                    {
                        FormatarGrelhaCor();
                        break;
                    }
            }
        }

        private void FormatarGrelhaComponentes()
        {
            try
            {
                {
                    vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordem", true, LarguraFixa: 50);

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Componente", true, LarguraFixa: 150);

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                    vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                    vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormatarGrelhaNE()
        {
            try
            {


                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("NE", true, LarguraFixa: 30);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Cabos", Visivel: true, LarguraFixa: 50);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição Ne");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("DescricaoNm", Visivel: true, Caption: "Descrição Nm");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Tipo", Visivel: true, LarguraFixa: 150);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Torcao", Visivel: true, LarguraFixa: 150);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();


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
                {
                    vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Torcao", Visivel: true, LarguraFixa: 150);

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                    vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                    vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                    vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();
                }
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

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("CodigoCor", Visivel: true, Caption: "Código cor (F4)");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ref", Visivel: true, Caption: "Referência");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();
                //withBlock.Splits(0).DisplayColumns("CodigoCor").HeadingStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0066CC");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void FormatarGrelhaCores()
        {
            try
            {

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Cor", Visivel: true, LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 250);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Cone", Visivel: true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();


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

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Caracteristica", Visivel: true, LarguraFixa: 150);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Programa", Visivel: true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();

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


                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Dimensao", Visivel: true, LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Cabos", Visivel: true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Filamentos", Visivel: true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Texturizacao", Visivel: true, LarguraFixa: 150);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();

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


                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Categoria", Visivel: true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void FormatarGrelhaCor()
        {
            try
            {

                vmpGridViewGestaoPropriedadesMundifios.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Cor", Visivel: true);

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedadesMundifios.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação", LarguraFixa: 60);

                vmpGridViewGestaoPropriedadesMundifios.AutoFillColumn = vmpGridViewGestaoPropriedadesMundifios.Columns["Descricao"];

                vmpGridViewGestaoPropriedadesMundifios.FinalizarFormatacao();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        #endregion


        private void vmpGridControlGestaoPropriedadesMundifios_KeyDown(object sender, KeyEventArgs e)
        {
            switch (TabelaAtual)
            {
                case Tabela.Referencia:
                    {
                        if (e.KeyCode == Keys.F4)
                        {
                            string CampoGrelha = vmpGridViewGestaoPropriedadesMundifios.FocusedColumn.FieldName;
                            DsPropriedadesMundifios.VMP_ART_ReferenciaRow activeRowPasso;
                            activeRowPasso = (DsPropriedadesMundifios.VMP_ART_ReferenciaRow)vmpGridViewGestaoPropriedadesMundifios.GetDataRow(vmpGridViewGestaoPropriedadesMundifios.FocusedRowHandle);

                            if (CampoGrelha == "Cor")
                            {
                                frmListaPropriedadesMundifiosView frm;
                                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(frmListaPropriedadesMundifiosView));
                                frm = result.Result;
                                frm.ShowDialog();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void vmpGridControlGestaoPropriedadesMundifios_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (TabelaAtual)
            {
                case Tabela.Referencia:
                    string CampoGrelha = vmpGridViewGestaoPropriedadesMundifios.FocusedColumn.FieldName;
                    if (CampoGrelha == "CodigoCor")
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #region Funções

        private void CarregarDados()
        {
            IdentificarBloqueio();
            CarregarGrelha();
            FormatarGrelhas();
        }

        private void IdentificarBloqueio()
        {

            // Me.DsPropriedadesMundifios.RemoverBloqueio("")

            // Verificar disponibilidade da tabela
            dsPropriedadesMundifios.IdentificarBloqueio(TabelaAtual.ToString());

            barGestaoPropriedadeMundifios.Visible = dsPropriedadesMundifios.VMP_ART_Bloqueios.TabelaBloqueada;

            if (dsPropriedadesMundifios.VMP_ART_Bloqueios.TabelaBloqueada)
                barStaticItemUtilizador.Caption = Strings.Format("Em edição pelo utilizador '{0}' no posto '{1}' desde '{2}'" + Strings.UCase(dsPropriedadesMundifios.VMP_ART_Bloqueios[0].Utilizador) + Strings.UCase(dsPropriedadesMundifios.VMP_ART_Bloqueios[0].Posto) + dsPropriedadesMundifios.VMP_ART_Bloqueios[0].Data);

            if (!dsPropriedadesMundifios.VMP_ART_Bloqueios.TabelaBloqueada)
                vmpGridControlGestaoPropriedadesMundifios.Enabled = !dsPropriedadesMundifios.VMP_ART_Bloqueios.TabelaBloqueada;

            if (!dsPropriedadesMundifios.VMP_ART_Bloqueios.TabelaBloqueada)
                barButtonItemGravar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            else
                barButtonItemGravar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void CarregarGrelha()
        {
            switch (TabelaAtual)
            {
                case Tabela.NE:
                    {
                        labelGrafico.Text = "NE";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceNE;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosNe();
                        break;
                    }

                case Tabela.Componente:
                    {
                        labelGrafico.Text = "Componente";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceComponente;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosComponente();
                        break;
                    }

                case Tabela.Tipo:
                    {
                        labelGrafico.Text = "Tipo";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceTipo;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosTipo();
                        break;
                    }

                case Tabela.Caracteristica:
                    {
                        labelGrafico.Text = "Caracteristica";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceCaracteristica;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosCaracteristica();
                        break;
                    }

                case Tabela.Torcao1:
                    {
                        labelGrafico.Text = "Torcao1";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceTorcao1;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosTorcao1();
                        break;
                    }

                case Tabela.Torcao2:
                    {
                        labelGrafico.Text = "Torcao2";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceTorcao2;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosTorcao2();
                        break;
                    }

                case Tabela.Referencia:
                    {
                        labelGrafico.Text = "Referência";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceReferencia;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosReferencia();
                        // Me.DsPropriedadesMundifios.AdptReferencia.FillJoinCores(Me.DsPropriedadesMundifios.VMP_ART_Referencia)

                        dsPropriedadesMundifios.AdptCor.Fill(dsPropriedadesMundifios.VMP_ART_Cor);
                        break;
                    }

                case Tabela.Cone:
                    {
                        labelGrafico.Text = "Cone";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceCone;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosCone();
                        break;
                    }

                case Tabela.Programa:
                    {
                        labelGrafico.Text = "Programa";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourcePrograma;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosPrograma();
                        break;
                    }

                case Tabela.Texturizacao:
                    {
                        labelGrafico.Text = "Texturização";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceTexturizacao;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosTexturizacao();
                        break;
                    }

                case Tabela.Dimensao:
                    {
                        labelGrafico.Text = "Dimensão";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceDimensao;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosDimensao();
                        break;
                    }

                case Tabela.Categoria:
                    {
                        labelGrafico.Text = "Categoria";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceCategoria;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosCategoria();
                        break;
                    }

                case Tabela.Cor:
                    {
                        labelGrafico.Text = "Cor";
                        vmpGridControlGestaoPropriedadesMundifios.DataSource = bindingSourceCores;
                        vmpGridViewGestaoPropriedadesMundifios.PopulateColumns();
                        dsPropriedadesMundifios.ReporDadosCor();
                        break;
                    }
            }
        }

        #endregion


        private void comboBoxEditGestaoPropriedadesMundifios_SelectedIndexChanged(object sender, EventArgs e)
            {
            if (LoadCompleto)
                TabelaAtual = (Tabela)comboBoxEditGestaoPropriedadesMundifios.SelectedIndex;
            vmpGridViewGestaoPropriedadesMundifios.FocusedRowHandle = vmpGridViewGestaoPropriedadesMundifios.RowCount;
        }
    }
}
