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
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    public partial class frmUcGestaoPropriedadeView : CustomForm
    {
        public frmUcGestaoPropriedadeView()
        {
            InitializeComponent();
        }


        public event ZEventoTesteEventHandler ZEventoTeste;

        public delegate void ZEventoTesteEventHandler();

        private bool LoadCompleto;

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

        private void FrmUcGestaoPropriedadeView_Load(object sender, EventArgs e)
        {
            // If TabelaAtual  IsDBNull() Then TabelaAtual = Tabela.Caracteristica
            dsEmpresasGrupo.GetEmpresasPermitidas();

            CarregarComboBox();

            CarregarDados();

            LoadCompleto = true;
        }

        private void CarregarDados()
        {
            CarregarGrelha();
            FormatarGrelhas();
        }


        private void CarregarGrelha()
        {
            switch (TabelaAtual)
            {
                case Tabela.NE:
                    {
                        labelTabela.Text = "NE";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceNE;
                        dsPropriedadesMundifios.AdptNE.Fill(dsPropriedadesMundifios.VMP_ART_NE);
                        break;
                    }

                case Tabela.Componente:
                    {
                        labelTabela.Text = "Componente";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceComponente;
                        dsPropriedadesMundifios.AdptComponente.Fill(dsPropriedadesMundifios.VMP_ART_Componente);
                        break;
                    }

                case Tabela.Tipo:
                    {
                        labelTabela.Text = "Tipo";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceTipo;
                        dsPropriedadesMundifios.AdptTipo.Fill(dsPropriedadesMundifios.VMP_ART_Tipo);
                        break;
                    }

                case Tabela.Caracteristica:
                    {
                        labelTabela.Text = "Caracteristica";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceCaracteristica;
                        dsPropriedadesMundifios.AdptCaracteristica.Fill(dsPropriedadesMundifios.VMP_ART_Caracteristica);
                        break;
                    }

                case Tabela.Torcao1:
                    {
                        labelTabela.Text = "Torcao1";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceTorcao1;
                        dsPropriedadesMundifios.AdptTorcao1.Fill(dsPropriedadesMundifios.VMP_ART_Torcao1);
                        break;
                    }

                case Tabela.Torcao2:
                    {
                        labelTabela.Text = "Torcao2";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceTorcao2;
                        dsPropriedadesMundifios.AdptTorcao2.Fill(dsPropriedadesMundifios.VMP_ART_Torcao2);
                        break;
                    }

                case Tabela.Referencia:
                    {
                        labelTabela.Text = "Referência";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceReferencia;
                        dsPropriedadesMundifios.AdptReferencia.FillJoinCores(dsPropriedadesMundifios.VMP_ART_Referencia);

                        dsPropriedadesMundifios.AdptCor.Fill(dsPropriedadesMundifios.VMP_ART_Cor);
                        break;
                    }

                case Tabela.Cone:
                    {
                        labelTabela.Text = "Cone";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceCone;
                        dsPropriedadesMundifios.AdptCone.Fill(dsPropriedadesMundifios.VMP_ART_Cone);
                        break;
                    }

                case Tabela.Programa:
                    {
                        labelTabela.Text = "Programa";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourcePrograma;
                        dsPropriedadesMundifios.AdptPrograma.Fill(dsPropriedadesMundifios.VMP_ART_Programa);
                        break;
                    }

                case Tabela.Texturizacao:
                    {
                        labelTabela.Text = "Texturização";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceTexturizacao;
                        dsPropriedadesMundifios.AdptTexturizacao.Fill(dsPropriedadesMundifios.VMP_ART_Texturizacao);
                        break;
                    }

                case Tabela.Dimensao:
                    {
                        labelTabela.Text = "Dimensão";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceDimensao;
                        dsPropriedadesMundifios.AdptDimensao.Fill(dsPropriedadesMundifios.VMP_ART_Dimensao);
                        break;
                    }

                case Tabela.Categoria:
                    {
                        labelTabela.Text = "Categoria";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceCategoria;
                        dsPropriedadesMundifios.AdptCategoria.Fill(dsPropriedadesMundifios.VMP_ART_Categoria);
                        break;
                    }

                case Tabela.Cor:
                    {
                        labelTabela.Text = "Cor";
                        vmpGridControlGestaoPropriedades.DataSource = bindingSourceCores;
                        dsPropriedadesMundifios.AdptCor.Fill(dsPropriedadesMundifios.VMP_ART_Cor);
                        break;
                    }
            }
        }

        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (Gravar())
                MessageBox.Show("Sucesso", "Gestão de " + TabelaAtual.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void CarregarComboBox()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Table");
            DataRow _ravi = dt.NewRow();
            _ravi["Table"] = Tabela.NE.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Componente.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Tipo.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Caracteristica.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Torcao1.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Torcao2.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Referencia.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Cone.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Texturizacao.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Dimensao.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Programa.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Categoria.ToString();
            dt.Rows.Add(_ravi);
            _ravi["Table"] = Tabela.Cor.ToString();
            dt.Rows.Add(_ravi);
            barEditItemGestaoPropriedades.DataBindings.Add("Selecao", dt, "Enum", true, DataSourceUpdateMode.OnPropertyChanged);


            //barEditItemGestaoPropriedades.SelectedIndex = barEditItemGestaoPropriedades.FindString(TabelaAtual.ToString);
        }

        private bool Gravar()
        {
            try
            {
                bool Resultado = true;



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

                vmpGridViewGestaoPropriedades.FocusedRowHandle = vmpGridViewGestaoPropriedades.RowCount;

                return Resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gestão de " + TabelaAtual.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordem", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Componente", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FormatarGrelhaNE()
        {
            try

            {
                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ne", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Cabos", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição Ne");

                vmpGridViewGestaoPropriedades.FormatarColuna("DescricaoNm", Visivel: true, Caption: "Descrição Nm");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();


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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Tipo", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();


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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Torcao", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Torcao", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Cor", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Ref", Visivel: true, Caption: "Referência");

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ref", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Cone", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Caracteristica", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Programa", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Dimensao", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Cabos", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Filamentos", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Texturizacao", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Categoria", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

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

                vmpGridViewGestaoPropriedades.IniciarFormatacao(true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Codigo", Visivel: true, Caption: "Código");

                vmpGridViewGestaoPropriedades.FormatarColuna("Cor", Visivel: true);

                vmpGridViewGestaoPropriedades.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição");

                vmpGridViewGestaoPropriedades.FormatarColuna("Ordenacao", Visivel: true, Caption: "Ordenação");

                vmpGridViewGestaoPropriedades.AutoFillColumn = vmpGridViewGestaoPropriedades.Columns["Descricao"];

                vmpGridViewGestaoPropriedades.FinalizarFormatacao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void vmpGridViewGestaoPropriedades_KeyDown(object sender, KeyEventArgs e)
        {

            switch (TabelaAtual)
            {
                case Tabela.Referencia:
                    {
                        if (e.KeyCode == Keys.F4)
                        {
                            string CampoGrelha = vmpGridViewGestaoPropriedades.FocusedColumn.FieldName;
                            DsPropriedadesMundifios.VMP_ART_ReferenciaRow activeRowPasso = (DsPropriedadesMundifios.VMP_ART_ReferenciaRow)vmpGridViewGestaoPropriedades.GetDataRow(vmpGridViewGestaoPropriedades.FocusedRowHandle);

                            if (CampoGrelha == "Cor")
                            {
                                ListParameterDataRow List = new ListParameterDataRow(activeRowPasso, dsPropriedadesMundifios.VMP_ART_Referencia.Columns.IndexOf(CampoGrelha), dsPropriedadesMundifios.VMP_ART_Referencia.Columns.IndexOf(CampoGrelha), dsPropriedadesMundifios.VMP_ART_Referencia.Columns.IndexOf("IdCor"));
                                frmListaPropriedadesMundifiosView frm = new frmListaPropriedadesMundifiosView();

                                frm.ShowDialog();
                            }
                        }

                        break;
                    }
            }
        }

        private void vmpGridViewGestaoPropriedades_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (TabelaAtual)
            {
                case Tabela.Referencia:
                    {
                        string CampoGrelha = vmpGridViewGestaoPropriedades.FocusedColumn.FieldName;
                        if (CampoGrelha == "Cor")
                            e.Handled = true;
                        break;
                    }
            }
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItemGravar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Gravar())
                MessageBox.Show( "Sucesso", "Gestão de " + TabelaAtual.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
