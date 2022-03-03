using DevExpress.UnitConversion;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource
{
    partial class DsArtigosMundifios
    {
        #region Classes
        partial class VMP_ART_ComponenteDataTable
        {
            public decimal ObtemTotalPercentagem()
            {
                object Soma = this.Compute(String.Format("SUM(Percentagem)"), "Sel = True");
                return Convert.ToDecimal(Convert.IsDBNull(Soma) ? 0 : Soma);
            }
            public bool Devolve1SeComponenteSemPercentagem()
            {
                int RegistosComPercentagemZero = this.Select("Sel = True and Percentagem = 0").Length;
                return Convert.ToBoolean(RegistosComPercentagemZero);
            }
            public int GetMaxOrdem()
            {
                object Ordem = this.Compute("MAX(Ordem)", "");
                return Convert.ToInt32(Convert.IsDBNull(Ordem) ? 0 : Ordem);
            }
            private void VMP_ART_ComponenteDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_ComponenteRow Componente = (VMP_ART_ComponenteRow)e.Row;
                Componente.Id = Guid.NewGuid();
                Componente.Ordem = GetMaxOrdem() + 1;
                Componente.DataIntroducao = DateTime.Now;
                Componente.DataAtualizacao = DateTime.Now;
                Componente.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                Componente.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                Componente.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                Componente.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                Componente.Ordenacao = 999;
            }


        }

        partial class VMP_ART_CaracteristicaDataTable
        {
            public void AtivarCheckBoxAtualizaDescExtra()
            {
                foreach (VMP_ART_CaracteristicaRow Item in this.Select("DescricaoExtra = 'False'"))
                    Item.DescricaoExtra = true;
            }

        }

        partial class VMP_ART_TipoDataTable
        {
            public void AtivarCheckBoxAtualizaDescExtra()
            {
                foreach (VMP_ART_TipoRow Item in this.Select("DescricaoExtra = 'False'"))
                    Item.DescricaoExtra = true;
            }

            private void VMP_ART_TipoDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_TipoRow TipoArt = (VMP_ART_TipoRow)e.Row;
                TipoArt.Id = Guid.NewGuid();
                TipoArt.DataIntroducao = DateTime.Now;
                TipoArt.DataAtualizacao = DateTime.Now;
                TipoArt.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                TipoArt.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                TipoArt.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                TipoArt.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
            }

        }
        //falta 
        partial class ArtigoDataTable
        {
            public bool ArtigoValido()
            {
                return Convert.ToBoolean(Count);
            }

        }

        partial class VMP_ART_TipoArtigoDataTable
        {
            private void VMP_ART_TipoArtigoDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_TipoArtigoRow TipoArt = (VMP_ART_TipoArtigoRow)e.Row;
                TipoArt.Id = Guid.NewGuid();
                TipoArt.DataIntroducao = DateTime.Now;
                TipoArt.DataAtualizacao = DateTime.Now;
                TipoArt.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                TipoArt.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                TipoArt.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                TipoArt.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
            }

        }

        partial class VMP_ART_CaracteristicaArtigoDataTable
        {
            private void VMP_ART_CaracteristicaArtigoDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_CaracteristicaArtigoRow CaraceteristicaArt = (VMP_ART_CaracteristicaArtigoRow)e.Row;
                CaraceteristicaArt.Id = Guid.NewGuid();
                CaraceteristicaArt.DataIntroducao = DateTime.Now;
                CaraceteristicaArt.DataAtualizacao = DateTime.Now;
                CaraceteristicaArt.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                CaraceteristicaArt.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                CaraceteristicaArt.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                CaraceteristicaArt.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
            }

        }

        partial class VMP_ART_ComponenteArtigoDataTable
        {
            private void VMP_ART_ComponenteArtigoDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_ComponenteArtigoRow ComponenteArtigo = (VMP_ART_ComponenteArtigoRow)e.Row;
                ComponenteArtigo.Id = Guid.NewGuid();
                ComponenteArtigo.DataIntroducao = DateTime.Now;
                ComponenteArtigo.DataAtualizacao = DateTime.Now;
                ComponenteArtigo.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                ComponenteArtigo.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                ComponenteArtigo.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                ComponenteArtigo.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
            }
        }
        #endregion

        #region

        public DsArtigosMundifiosTableAdapters.ArtigoTableAdapter AdptArtigo { get; set; } = new DsArtigosMundifiosTableAdapters.ArtigoTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_ComponenteArtigoTableAdapter AdptComponentesArtigo { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_ComponenteArtigoTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_CaracteristicaArtigoTableAdapter AdptCaracteristicasArtigo { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_CaracteristicaArtigoTableAdapter();

        public DsArtigosMundifiosTableAdapters.VMP_ART_TipoTableAdapter AdptTipos { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_TipoTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_TipoArtigoTableAdapter AdptTiposArtigo { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_TipoArtigoTableAdapter();

        public DsArtigosMundifiosTableAdapters.FamiliasTableAdapter AdptFamilias { get; set; } = new DsArtigosMundifiosTableAdapters.FamiliasTableAdapter();
        private DsArtigosMundifiosTableAdapters.SubFamiliasTableAdapter AdptSubFamilias { get; set; } = new DsArtigosMundifiosTableAdapters.SubFamiliasTableAdapter();

        public DsArtigosMundifiosTableAdapters.VMP_ART_ComponenteTableAdapter AdptComponentes { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_ComponenteTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_NETableAdapter AdptNE { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_NETableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_TipoTableAdapter AdptTipo { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_TipoTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_Torcao1TableAdapter AdptTorcao1 { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_Torcao1TableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_Torcao2TableAdapter AdptTorcao2 { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_Torcao2TableAdapter();

        public DsArtigosMundifiosTableAdapters.VMP_ART_ReferenciaTableAdapter AdptReferencias { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_ReferenciaTableAdapter();

        public DsArtigosMundifiosTableAdapters.VMP_ART_CorTableAdapter AdptCor { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_CorTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_ConeTableAdapter AdptCone { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_ConeTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_CaracteristicaTableAdapter AdptCaracteristica { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_CaracteristicaTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_ProgramaTableAdapter AdptPrograma { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_ProgramaTableAdapter();

        public DsArtigosMundifiosTableAdapters.VMP_ART_DimensaoTableAdapter AdptDimensao { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_DimensaoTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_TexturizacaoTableAdapter AdptTexturizacao { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_TexturizacaoTableAdapter();
        public DsArtigosMundifiosTableAdapters.VMP_ART_CategoriaTableAdapter AdptCategoria { get; set; } = new DsArtigosMundifiosTableAdapters.VMP_ART_CategoriaTableAdapter();

        public DsArtigosMundifiosTableAdapters.QueriesTableAdapter AdptQueries { get; set; } = new DsArtigosMundifiosTableAdapters.QueriesTableAdapter();

        public string _DescricaoComponente = "";
        public string _PercentagensComponente = "";

        public DsArtigosMundifiosTableAdapters.IntrastatMercadoriaTableAdapter AdptIntrastat { get; set; } = new DsArtigosMundifiosTableAdapters.IntrastatMercadoriaTableAdapter();

        public DsArtigosMundifiosTableAdapters.DadosAuxiliaresECRATableAdapter AdptDadosAuxiliaresECRA { get; set; } = new DsArtigosMundifiosTableAdapters.DadosAuxiliaresECRATableAdapter();
        public DsArtigosMundifiosTableAdapters.TDU_GrupoTaxaDesperdicioTableAdapter AdptGrupoTaxaDesperdicio { get; set; } = new DsArtigosMundifiosTableAdapters.TDU_GrupoTaxaDesperdicioTableAdapter();

        #endregion
        #region Propriedades
        // # Classe Intrastat
        public IntrastatMercadoriaRow _IntrastatMercadoria
        {
            get
            {
                return IntrastatMercadoria[0];
            }
        }

        // # Classe artigo
        public ArtigoRow _Artigo
        {
            get
            {
                return Artigo[0];
            }
        }

        public FamiliasRow _FamiliasSel
        {
            get
            {
                FamiliasRow[] FamiliasSel = (FamiliasRow[])Familias.Select("Sel = True", "");
                if (FamiliasSel.Length > 0)
                    return FamiliasSel[0];
                else
                    return null;
            }
        }
        public SubFamiliasRow _SubFamiliasSel
        {
            get
            {
                SubFamiliasRow[] SubFamiliasSel = (SubFamiliasRow[])SubFamilias.Select("Sel = True", "");
                if (SubFamiliasSel.Length > 0)
                    return SubFamiliasSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_NERow _NESel
        {
            get
            {
                VMP_ART_NERow[] NESel = (VMP_ART_NERow[])VMP_ART_NE.Select("Sel = True", "");
                if (NESel.Length > 0)
                    return NESel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_TipoRow _TipoSel
        {
            get
            {
                VMP_ART_TipoRow[] TipoSel = (VMP_ART_TipoRow[])VMP_ART_Tipo.Select("Sel = True", "");
                if (TipoSel.Length > 0)
                    return TipoSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_Torcao1Row _Torcao1Sel
        {
            get
            {
                VMP_ART_Torcao1Row[] Torcao1Sel = (VMP_ART_Torcao1Row[])VMP_ART_Torcao1.Select("Sel = True", "");
                if (Torcao1Sel.Length > 0)
                    return Torcao1Sel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_Torcao2Row _Torcao2Sel
        {
            get
            {
                VMP_ART_Torcao2Row[] Torcao2Sel = (VMP_ART_Torcao2Row[])VMP_ART_Torcao2.Select("Sel = True", "");
                if (Torcao2Sel.Length > 0)
                    return Torcao2Sel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_ReferenciaRow _ReferenciaSel
        {
            get
            {
                VMP_ART_ReferenciaRow[] ReferenciaSel = (VMP_ART_ReferenciaRow[])VMP_ART_Referencia.Select("Sel = True", "");
                if (ReferenciaSel.Length > 0)
                    return ReferenciaSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_CaracteristicaRow _CaracteristicaSel
        {
            get
            {
                VMP_ART_CaracteristicaRow[] CaracteristicaSel = (VMP_ART_CaracteristicaRow[])VMP_ART_Caracteristica.Select("Sel = True", "");
                if (CaracteristicaSel.Length > 0)
                    return CaracteristicaSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_ConeRow _ConeSel
        {
            get
            {
                VMP_ART_ConeRow[] ConeSel = (VMP_ART_ConeRow[])VMP_ART_Cone.Select("Sel = True", "");
                if (ConeSel.Length > 0)
                    return ConeSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_ProgramaRow _ProgramaSel
        {
            get
            {
                VMP_ART_ProgramaRow[] ProgramaSel = (VMP_ART_ProgramaRow[])VMP_ART_Programa.Select("Sel = True", "");
                if (ProgramaSel.Length > 0)
                    return ProgramaSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_DimensaoRow _DimensaoSel
        {
            get
            {
                VMP_ART_DimensaoRow[] DimensaoSel = (VMP_ART_DimensaoRow[])VMP_ART_Dimensao.Select("Sel = True", "");
                if (DimensaoSel.Length > 0)
                    return DimensaoSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_CategoriaRow _CategoriaSel
        {
            get
            {
                VMP_ART_CategoriaRow[] CategoriaSel = (VMP_ART_CategoriaRow[])VMP_ART_Categoria.Select("Sel = True", "");
                if (CategoriaSel.Length > 0)
                    return CategoriaSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        public VMP_ART_TexturizacaoRow _TexturizacaoSel
        {
            get
            {
                VMP_ART_TexturizacaoRow[] TexturizacaoSel = (VMP_ART_TexturizacaoRow[])VMP_ART_Texturizacao.Select("Sel = True", "");
                if (TexturizacaoSel.Length > 0)
                    return TexturizacaoSel[0];
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }


        public string DescricaoComponentesSel
        {
            get
            {
                var DescricaoComponente = string.Empty;

                VMP_ART_ComponenteRow[] RowComponente = (VMP_ART_ComponenteRow[])VMP_ART_Componente.Select("Sel = true", "Percentagem desc,Ordem");
                foreach (var item in RowComponente)
                {
                    if (Conversion.Val(item.Codigo) == 0)
                        continue;
                    DescricaoComponente = DescricaoComponente + "/" + item.Descricao;
                }

                if (DescricaoComponente.Length > 0)
                    DescricaoComponente = Strings.Right(DescricaoComponente, DescricaoComponente.Length - 1);

                return DescricaoComponente;
            }
        }

        public string DescricaoComponentesPercentagemSel
        {
            get
            {
                var PercentagensComponente = string.Empty;

                VMP_ART_ComponenteRow[] RowComponente = (VMP_ART_ComponenteRow[])VMP_ART_Componente.Select("Sel = true", "Percentagem desc,Ordem");
                foreach (var item in RowComponente)
                {
                    if (Conversion.Val(item.Codigo) == 0)
                        continue;
                    PercentagensComponente = PercentagensComponente + "/" + item.Percentagem;
                }

                if (PercentagensComponente.Length > 0)
                    PercentagensComponente = Strings.Right(PercentagensComponente, PercentagensComponente.Length - 1);

                return PercentagensComponente;
            }
        }

        public string DescricaoTiposSel
        {
            get
            {
                var Descricao = string.Empty;

                VMP_ART_TipoRow[] Row = (VMP_ART_TipoRow[])VMP_ART_Tipo.Select("Sel = true", "Codigo");
                foreach (var item in Row)
                {
                    if (Conversion.Val(item.Codigo) == 0)
                        continue;
                    Descricao = Descricao + "/" + item.Descricao;
                }

                if (Descricao.Length > 0)
                    Descricao = Strings.Right(Descricao, Descricao.Length - 1);

                return Descricao;
            }
        }

        public string DescricaoCaracteristicaSel
        {
            get
            {
                var Descricao = string.Empty;

                VMP_ART_CaracteristicaRow[] Row = (VMP_ART_CaracteristicaRow[])VMP_ART_Caracteristica.Select("Sel = true", "Codigo");
                foreach (var item in Row)
                {
                    if (Conversion.Val(item.Codigo) == 0)
                        continue;
                    Descricao = Descricao + "/" + item.Descricao;
                }

                if (Descricao.Length > 0)
                    Descricao = Strings.Right(Descricao, Descricao.Length - 1);

                return Descricao;
            }
        }

        #endregion

        #region CRUD
        private void DeleteRowsComponentes(ref VMP_ART_ComponenteArtigoDataTable DeleteComponentes)
        {
            if (DeleteComponentes != null)
                AdptComponentesArtigo.Update(DeleteComponentes);
        }
        public bool EliminaRegistoTabelasComSelecaoMultipla(string CodigoArtigo, string Connection)
        {
            try
            {
                // Características
                // Tipos
                // Componentes

                using (TransactionScope AdaptersTransaction = new TransactionScope())
                {
                    AdptTiposArtigo.changeConnection(Connection);
                    AdptCaracteristicasArtigo.changeConnection(Connection);
                    AdptComponentesArtigo.changeConnection(Connection);

                    AdptTiposArtigo.FillByCodigoArtigo(this.VMP_ART_TipoArtigo, CodigoArtigo);
                    foreach (VMP_ART_TipoArtigoRow Row in VMP_ART_TipoArtigo)
                        Row.Delete();

                    AdptCaracteristicasArtigo.FillByCodigoArtigo(this.VMP_ART_CaracteristicaArtigo, CodigoArtigo);
                    foreach (VMP_ART_CaracteristicaArtigoRow Row in VMP_ART_CaracteristicaArtigo)
                        Row.Delete();

                    AdptComponentesArtigo.FillByCodigoArtigo(this.VMP_ART_ComponenteArtigo, CodigoArtigo);
                    foreach (VMP_ART_ComponenteArtigoRow Row in VMP_ART_ComponenteArtigo)
                        Row.Delete();
                    AdptTiposArtigo.Update(VMP_ART_TipoArtigo);
                    AdptCaracteristicasArtigo.Update(VMP_ART_CaracteristicaArtigo);
                    AdptComponentesArtigo.Update(VMP_ART_ComponenteArtigo);

                    AdaptersTransaction.Complete();

                    AdptTiposArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
                    AdptCaracteristicasArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
                    AdptComponentesArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool GravarComponentes(string CodigoArtigo, string Connection)
        {
            try
            {
                TransactionScope TransactionAdapter = new TransactionScope();

                using (TransactionAdapter)
                {
                    AdptComponentesArtigo.changeConnection(Connection);

                    AdptComponentesArtigo.FillByCodigoArtigo(VMP_ART_ComponenteArtigo, CodigoArtigo);

                    // Remover os registos que estão na tabela componenteArtigo e que não estão selecionados na tabela de Componentes
                    foreach (VMP_ART_ComponenteArtigoRow ComponenteArtigo in VMP_ART_ComponenteArtigo)
                    {
                        if (VMP_ART_Componente.Select("ID = '" + ComponenteArtigo.Id.ToString() + "' AND Sel = True").Length == 0)
                            ComponenteArtigo.Delete();
                    }

                    foreach (VMP_ART_ComponenteRow Componente in VMP_ART_Componente.Select("sel = True AND Percentagem > 0", ""))
                    {
                        VMP_ART_ComponenteArtigoRow[] ComponenteArtigo = (VMP_ART_ComponenteArtigoRow[])VMP_ART_ComponenteArtigo.Select("ID = '" + Componente.Id.ToString() + "'");
                        if (ComponenteArtigo.Length > 0)
                            ComponenteArtigo[0].Percentagem = Convert.ToInt32(Componente.Percentagem);
                        else
                        {
                            VMP_ART_ComponenteArtigoRow NovoComponenteArtigo = VMP_ART_ComponenteArtigo.NewVMP_ART_ComponenteArtigoRow();
                            NovoComponenteArtigo.CodigoArtigo = CodigoArtigo;
                            NovoComponenteArtigo.IdComponente = Componente.Id;
                            NovoComponenteArtigo.Percentagem = Componente.Percentagem;
                            NovoComponenteArtigo.Id = Guid.NewGuid();

                            VMP_ART_ComponenteArtigo.AddVMP_ART_ComponenteArtigoRow(NovoComponenteArtigo);
                        }
                    }
                    AdptComponentesArtigo.Update(VMP_ART_ComponenteArtigo);

                    TransactionAdapter.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                AdptComponentesArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
            }
        }

        public bool GravarCaracteristicas(string CodigoArtigo, string Connection)
        {
            try
            {
                TransactionScope TransactionAdapter = new TransactionScope();

                using (TransactionAdapter)
                {
                    AdptCaracteristicasArtigo.changeConnection(Connection);

                    AdptCaracteristicasArtigo.FillByCodigoArtigo(VMP_ART_CaracteristicaArtigo, CodigoArtigo);

                    // Remover os registos que estão na tabela CaracteristicaArtigo e que não estão selecionados na tabela de Caracteristicas
                    foreach (VMP_ART_CaracteristicaArtigoRow CaracteristicaArtigo in VMP_ART_CaracteristicaArtigo)
                    {
                        if (VMP_ART_Caracteristica.Select("ID = '" + CaracteristicaArtigo.Id.ToString() + "' AND Sel = True").Length == 0)
                            CaracteristicaArtigo.Delete();
                    }

                    foreach (VMP_ART_CaracteristicaRow Caracteristica in VMP_ART_Caracteristica.Select("sel = True", ""))
                    {
                        VMP_ART_CaracteristicaArtigoRow[] CaracteristicaArtigo = (VMP_ART_CaracteristicaArtigoRow[])VMP_ART_CaracteristicaArtigo.Select("ID = '" + Caracteristica.Id.ToString() + "'");

                        if (CaracteristicaArtigo.Length > 0)
                        {
                        }
                        else
                        {
                            VMP_ART_CaracteristicaArtigoRow NovoCaracteristicaArtigo = VMP_ART_CaracteristicaArtigo.NewVMP_ART_CaracteristicaArtigoRow();
                            NovoCaracteristicaArtigo.CodigoArtigo = CodigoArtigo;
                            NovoCaracteristicaArtigo.IdCaracteristica = Caracteristica.Id;
                            NovoCaracteristicaArtigo.DescExtra = Caracteristica.DescricaoExtra;
                            NovoCaracteristicaArtigo.Id = Guid.NewGuid();
                            VMP_ART_CaracteristicaArtigo.AddVMP_ART_CaracteristicaArtigoRow(NovoCaracteristicaArtigo);
                        }
                    }
                    AdptCaracteristicasArtigo.Update(VMP_ART_CaracteristicaArtigo);

                    TransactionAdapter.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                AdptCaracteristicasArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
            }
        }

        public bool GravarTipos(string CodigoArtigo, string Connection)
        {
            try
            {
                TransactionScope TransactionAdapter = new TransactionScope();

                using (TransactionAdapter)
                {
                    AdptTiposArtigo.changeConnection(Connection);

                    AdptTiposArtigo.FillByCodigoArtigo(VMP_ART_TipoArtigo, CodigoArtigo);

                    // Remover os registos que estão na tabela TipoArtigo e que não estão selecionados na tabela de Tipos
                    foreach (VMP_ART_TipoArtigoRow TipoArtigo in VMP_ART_TipoArtigo)
                    {
                        if (VMP_ART_Tipo.Select("ID = '" + TipoArtigo.Id.ToString() + "' AND Sel = True").Length == 0)
                            TipoArtigo.Delete();
                    }

                    foreach (VMP_ART_TipoRow Tipo in VMP_ART_Tipo.Select("sel = True", ""))
                    {
                        VMP_ART_TipoArtigoRow[] TipoArtigo = (VMP_ART_TipoArtigoRow[])VMP_ART_TipoArtigo.Select("ID = '" + Tipo.Id.ToString() + "'");

                        if (TipoArtigo.Length > 0)
                        {
                        }
                        else
                        {
                            VMP_ART_TipoArtigoRow NovoTipoArtigo = VMP_ART_TipoArtigo.NewVMP_ART_TipoArtigoRow();
                            NovoTipoArtigo.CodigoArtigo = CodigoArtigo;
                            NovoTipoArtigo.IdTipo = Tipo.Id;
                            NovoTipoArtigo.DescExtra = Tipo.DescricaoExtra;
                            NovoTipoArtigo.Id = Guid.NewGuid();
                            VMP_ART_TipoArtigo.AddVMP_ART_TipoArtigoRow(NovoTipoArtigo);
                        }
                    }
                    AdptTiposArtigo.Update(VMP_ART_TipoArtigo);

                    TransactionAdapter.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                AdptTiposArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
            }
        }
        public bool GetTrueIfArtigoExisteByCodigo(string Codigo)
        {
            AdptArtigo.FillByCodigo(Artigo, Codigo);

            // Se o cartigo existir..
            if (Artigo.Rows.Count > 0)
            {
                // Carrego os componentes
                AdptComponentesArtigo.FillByCodigoArtigo(this.VMP_ART_ComponenteArtigo, Codigo);
                AdptCaracteristicasArtigo.FillByCodigoArtigo(this.VMP_ART_CaracteristicaArtigo, Codigo);
                AdptTiposArtigo.FillByCodigoArtigo(this.VMP_ART_TipoArtigo, Codigo);
            }
            else
            {
                this.VMP_ART_ComponenteArtigo.Clear();
                this.VMP_ART_CaracteristicaArtigo.Clear();
                this.VMP_ART_TipoArtigo.Clear();
            }

            return Convert.ToBoolean(Artigo.Rows.Count);
        }

        public bool GetTrueIfTodasPropriedadesEscolhias()
        {
            if (_FamiliasSel == null)
                return false;
            if (_SubFamiliasSel == null)
                return false;
            if (_TexturizacaoSel == null)
                return false;
            if (_NESel == null)
                return false;
            if (_CategoriaSel == null)
                return false;
            if (_TipoSel == null)
                return false;
            if (_Torcao1Sel == null)
                return false;
            if (_Torcao2Sel == null)
                return false;
            if (_ReferenciaSel == null)
                return false;
            if (_CaracteristicaSel == null)
                return false;
            if (_ConeSel == null)
                return false;
            if (_ProgramaSel == null)
                return false;
            if (_DimensaoSel == null)
                return false;
            return true;
        }

        public bool GetTrueIfArtigoExisteByPropriedades(string Codigo)
        {

            // Verificar se existe algum artigo com as mesmas propriedades
            AdptArtigo.FillByPropriedades(Artigo, Codigo, _FamiliasSel.Familia, _SubFamiliasSel.SubFamilia, _NESel.Codigo, _Torcao1Sel.Codigo, _Torcao2Sel.Codigo, _ConeSel.Codigo, _ProgramaSel.Codigo, _DimensaoSel.Codigo, _TexturizacaoSel.Codigo, _CategoriaSel.Codigo, _ReferenciaSel.Codigo);
            // AdptArtigo.FillByPropriedadesPrincipais(Artigo, Codigo, _FamiliasSel.Familia, _SubFamiliasSel.SubFamilia, _NESel.Codigo, _TexturizacaoSel.Codigo)


            // Se existirem registos
            if (Artigo.Rows.Count > 0)
            {

                // Carrego os componentes da BD e comparo com os escolhidos no ecrã
                var ArtigoRepetido = GetArtigoComMesmasPropriedadesMultiplas();

                if (Strings.Len(ArtigoRepetido) > 0)
                {
                    MessageBox.Show(string.Format("Já existe o artigo {0} cujos propriedades são iguais ao que pretende criar", ArtigoRepetido), "Artigo Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                this.VMP_ART_ComponenteArtigo.Clear();
                this.VMP_ART_CaracteristicaArtigo.Clear();
                this.VMP_ART_TipoArtigo.Clear();
                return false;
            }
        }

        public bool GetTrueIfArtigoExisteByPropriedades(bool ConsiderarCodigo, string Codigo = "")
        {
            if (ConsiderarCodigo)
            {
                // Se passar código, significa que vou verificar se o artigo existe através do código

                AdptArtigo.FillByCodigo(Artigo, Codigo);

                // Se o cartigo existir..
                if (Artigo.Rows.Count > 0)
                {
                    AdptComponentesArtigo.FillByCodigoArtigo(this.VMP_ART_ComponenteArtigo, Codigo);
                    AdptCaracteristicasArtigo.FillByCodigoArtigo(this.VMP_ART_CaracteristicaArtigo, Codigo);
                    AdptTiposArtigo.FillByCodigoArtigo(this.VMP_ART_TipoArtigo, Codigo);
                }
                else
                {
                    // AdicionarArtigo(Codigo)
                    this.VMP_ART_ComponenteArtigo.Clear();
                    this.VMP_ART_CaracteristicaArtigo.Clear();
                    this.VMP_ART_TipoArtigo.Clear();
                }

                return Convert.ToBoolean(Artigo.Rows.Count);
            }
            else
            {
                // Se não passar código, significa que vou verificar se o artigo existe através das propriedades

                string Familia = string.Empty;
                string SubFamilia = string.Empty;
                string NE = string.Empty;

                // Familia = Familias.GetSelecao
                // SubFamilia = SubFamilias.GetSelecao
                // NE = VMP_ART_NE.GetSelecao
                if (_FamiliasSel != null)
                    Familia = _FamiliasSel.Familia;
                if (_SubFamiliasSel != null)
                    SubFamilia = _SubFamiliasSel.SubFamilia;
                if (_NESel != null)
                    NE = _NESel.Codigo;

                // AdptArtigo.FillByPropriedadesPrincipais(Artigo, "", _FamiliasSel.Familia, _SubFamiliasSel.SubFamilia, _NESel.Codigo, _TexturizacaoSel.Codigo)
                AdptArtigo.FillByPropriedades(Artigo, "", Familia, _SubFamiliasSel.SubFamilia, _NESel.Codigo, _Torcao1Sel.Codigo, _Torcao2Sel.Codigo, _ConeSel.Codigo, _ProgramaSel.Codigo, _DimensaoSel.Codigo, _TexturizacaoSel.Codigo, _CategoriaSel.Codigo, _ReferenciaSel.Codigo);

                if (Artigo.Rows.Count > 0)
                {
                    var ArtigoRepetido = GetArtigoComMesmasPropriedadesMultiplas();

                    if (Strings.Len(ArtigoRepetido) > 0)
                    {
                        MessageBox.Show(string.Format("Já existe o artigo {0} cujos propriedades são iguais ao que pretende criar", ArtigoRepetido), "Artigo Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    // AdicionarArtigo(Codigo)
                    this.VMP_ART_ComponenteArtigo.Clear();
                    this.VMP_ART_CaracteristicaArtigo.Clear();
                    this.VMP_ART_TipoArtigo.Clear();
                    return false;
                }
            }
        }

        private string GetArtigoComOsMesmosTipos(string _Artigo)
        {

            // Apesar de estar num ciclo, só faz isto uma vez porque recebo dos componentes o codigo do artigo

            // Podem haver vários artigos com as mesmas características.. Então, para cada um deles...
            foreach (ArtigoRow ArtigoRow in Artigo.Select("Artigo = '" + _Artigo + "'"))
            {

                // Carregar a tabela de componentesArtigo com o código do artigo que existe
                AdptTiposArtigo.FillByCodigoArtigo(this.VMP_ART_TipoArtigo, ArtigoRow.Artigo);


                // Verificar se o número de componentes é o mesmo que os da grelha!
                if (VMP_ART_TipoArtigo.Rows.Count != VMP_ART_Tipo.Select("sel = True ").Length)
                {
                    VMP_ART_TipoArtigo.Clear();
                    _Artigo = string.Empty;
                    goto Continua;
                }


                // Para cada registo da grelha/Tabela que se encontra no ecrã
                foreach (VMP_ART_TipoRow Registo in VMP_ART_Tipo.Select("Sel = True", ""))
                {
                    if (this.VMP_ART_TipoArtigo.Select("idTipo = '" + Registo.Id.ToString() + "'", "").Length == 0)
                    {
                        VMP_ART_TipoArtigo.Clear();
                        _Artigo = string.Empty;
                        goto Continua;
                    }
                }
                return _Artigo;
            Continua:
                ;
            }
            return _Artigo;
        }

        private string GetArtigoComAsMesmasCaracteristicas(string _Artigo)
        {

            // Apesar de estar num ciclo, só faz isto uma vez porque recebo dos componentes o codigo do artigo

            // Podem haver vários artigos com as mesmas características.. Então, para cada um deles...
            foreach (ArtigoRow ArtigoRow in Artigo.Select("Artigo = '" + _Artigo + "'"))
            {

                // Carregar a tabela de componentesArtigo com o código do artigo que existe
                AdptCaracteristicasArtigo.FillByCodigoArtigo(this.VMP_ART_CaracteristicaArtigo, ArtigoRow.Artigo);


                // Verificar se o número de componentes é o mesmo que os da grelha!
                if (VMP_ART_CaracteristicaArtigo.Rows.Count != VMP_ART_Caracteristica.Select("sel = True ").Length)
                {
                    VMP_ART_CaracteristicaArtigo.Clear();
                    _Artigo = string.Empty;
                    goto Continua;
                }


                // Para cada registo da grelha/Tabela que se encontra no ecrã
                foreach (VMP_ART_CaracteristicaRow Registo in VMP_ART_Caracteristica.Select("Sel = True", ""))
                {
                    if (this.VMP_ART_CaracteristicaArtigo.Select("idCaracteristica = '" + Registo.Id.ToString() + "'", "").Length == 0)
                    {
                        VMP_ART_CaracteristicaArtigo.Clear();
                        _Artigo = string.Empty;
                        goto Continua;
                    }
                }


                return _Artigo;

            Continua:
                ;
            }

            return _Artigo;
        }

        private string GetArtigoComMesmasPropriedadesMultiplas()
        {
            string ArtigoIgual = string.Empty;

            // Podem haver vários artigos com as mesmas características.. Então, para cada um deles...
            foreach (ArtigoRow ArtigoRow in Artigo)
            {

                // PArto do principio que os componentes são diferentes!
                ArtigoIgual = ArtigoRow.Artigo;

                // Carregar a tabela de componentesArtigo com o código do artigo que existe
                AdptComponentesArtigo.FillByCodigoArtigo(this.VMP_ART_ComponenteArtigo, ArtigoRow.Artigo);

                // Verificar se o número de componentes é o mesmo que os da grelha!
                if (VMP_ART_ComponenteArtigo.Rows.Count != VMP_ART_Componente.Select("sel = True And Percentagem > 0").Length)
                {

                    // Se for diferente sei já que os componentes são diferentes, logo continui o ciclo
                    VMP_ART_ComponenteArtigo.Clear();
                    // Limpo a variável
                    ArtigoIgual = string.Empty;
                    continue;
                }


                // Para cada registo da grelha/Tabela que se encontra no ecrã selecionado e com % > 0
                foreach (VMP_ART_ComponenteRow Registo in VMP_ART_Componente.Select("Sel = True", "Percentagem"))
                {

                    // Vejo se tambem existe DIFERENÇAS na tabela de componente artigo, na base de dados, para o artigo diferente do atual, cujas características são todas iguais
                    if (this.VMP_ART_ComponenteArtigo.Select("idComponente = '" + Registo.Id.ToString() + "' AND Percentagem = " + Registo.Percentagem + "", "").Length == 0)
                    {
                        // Ok, significa que há diferenças! Logo o artigo não é igual. Limpo a variável
                        VMP_ART_ComponenteArtigo.Clear();
                        // Limpo a variável
                        ArtigoIgual = string.Empty;
                        goto Continua; // Continuo o ciclo para o seguinte
                    }
                }

                // Se um artigo tiver os mesmos componentes, vou verificar as caracteristicas! Se não tiver os mesmos componentes, não preciso comparar as carcteristicas
                if (Strings.Len(ArtigoIgual) > 0)
                {

                    // Retorna o mesmo artigo caso as caracteristicas sejam as mesmas!
                    ArtigoIgual = GetArtigoComAsMesmasCaracteristicas(ArtigoIgual);

                    if (Strings.Len(ArtigoIgual) > 0)
                    {
                        ArtigoIgual = GetArtigoComOsMesmosTipos(ArtigoIgual);

                        if (Strings.Len(ArtigoIgual) > 0)
                            return ArtigoIgual;
                    }
                }

            Continua:
                ;
            }

            return ArtigoIgual;
        }

        public void CarregarFamilias()
        {
            AdptFamilias.Fill(this.Familias);
            if (this.Familias.Rows.Count > 0)
                this.Familias[0].Sel = true;
        }

        public void CarregarSubFamilias()
        {
            AdptSubFamilias.Fill(this.SubFamilias);
            if (this.Familias.Rows.Count > 0)
            {
                if (this.SubFamilias.Rows.Count > 0)
                {
                    SubFamiliasRow[] x = (SubFamiliasRow[])this.SubFamilias.Select("Familia = '" + Familias[0].Familia + "'");
                    if (x.Length > 0)
                        x[0].Sel = true;
                }
            }
        }

        public void CarregarComponentes()
        {

            AdptComponentes.Fill(VMP_ART_Componente);
            if (VMP_ART_Componente.Rows.Count > 0)
                VMP_ART_Componente[0].Sel = true;
        }


        public void CarregarNE()
        {
            AdptNE.Fill(this.VMP_ART_NE);
            if (this.VMP_ART_NE.Rows.Count > 0)
                this.VMP_ART_NE[0].Sel = true;
        }

        public void CarregarTipos()
        {
            AdptTipo.Fill(this.VMP_ART_Tipo);
            if (this.VMP_ART_Tipo.Rows.Count > 0)
                this.VMP_ART_Tipo[0].Sel = true;
        }

        public void CarregarTorcao1()
        {
            AdptTorcao1.Fill(this.VMP_ART_Torcao1);
            if (this.VMP_ART_Torcao1.Rows.Count > 0)
                this.VMP_ART_Torcao1[0].Sel = true;
        }

        public void CarregarTorcao2()
        {
            AdptTorcao2.Fill(this.VMP_ART_Torcao2);
            if (this.VMP_ART_Torcao2.Rows.Count > 0)
                this.VMP_ART_Torcao2[0].Sel = true;
        }

        public void CarregarReferencias(bool ApenasLivres, string Familia = "")
        {
            AdptReferencias.FillByEstado(this.VMP_ART_Referencia, ApenasLivres.ToString());
            // If Len(Familia) = 0 Then
            // AdptReferencias.FillByFamiliaEstado(Me.VMP_ART_Referencia, _FamiliasSel.Familia.ToString, ApenasLivres)
            // Else
            // AdptReferencias.FillByFamiliaEstado(Me.VMP_ART_Referencia, Familia, ApenasLivres)
            // End If

            if (this.VMP_ART_Referencia.Rows.Count > 0)
                this.VMP_ART_Referencia[0].Sel = true;
        }

        public void CarregarCor()
        {
            AdptCor.Fill(this.VMP_ART_Cor);
            if (this.VMP_ART_Cor.Rows.Count > 0)
                this.VMP_ART_Cor[0].Sel = true;
        }

        public void CarregarCone()
        {
            AdptCone.Fill(this.VMP_ART_Cone);
            if (this.VMP_ART_Cone.Rows.Count > 0)
                this.VMP_ART_Cone[0].Sel = true;
        }

        public void CarregarCaracteristica()
        {
            AdptCaracteristica.Fill(this.VMP_ART_Caracteristica);
            if (this.VMP_ART_Caracteristica.Rows.Count > 0)
                this.VMP_ART_Caracteristica[0].Sel = true;
        }

        public void CarregarPrograma()
        {
            AdptPrograma.Fill(this.VMP_ART_Programa);
            if (this.VMP_ART_Programa.Rows.Count > 0)
                this.VMP_ART_Programa[0].Sel = true;
        }

        public void CarregarDimensao()
        {
            AdptDimensao.Fill(this.VMP_ART_Dimensao);
            if (this.VMP_ART_Dimensao.Rows.Count > 0)
                this.VMP_ART_Dimensao[0].Sel = true;
        }

        public void CarregarTexturizacao()
        {
            AdptTexturizacao.Fill(this.VMP_ART_Texturizacao);
            if (this.VMP_ART_Texturizacao.Rows.Count > 0)
                this.VMP_ART_Texturizacao[0].Sel = true;
        }

        public void CarregarCategoria()
        {
            AdptCategoria.Fill(this.VMP_ART_Categoria);
            if (this.VMP_ART_Categoria.Rows.Count > 0)
                this.VMP_ART_Categoria[0].Sel = true;
        }

        #endregion


        #region Funções

        public void AtualizarArtigoNosComponentes(string Artigo)
        {
            foreach (VMP_ART_ComponenteArtigoRow componentes in VMP_ART_ComponenteArtigo)
            {
                componentes.Id = Guid.NewGuid();
                componentes.CodigoArtigo = Artigo;
            }
        }


        public void AtualizarArtigoNasCaracteristicas(string Artigo)
        {
            foreach (VMP_ART_CaracteristicaArtigoRow Caracteristica in VMP_ART_CaracteristicaArtigo)
            {
                Caracteristica.Id = Guid.NewGuid();
                Caracteristica.CodigoArtigo = Artigo;
            }
        }

        public void AtualizarArtigoNosTipos(string Artigo)
        {
            foreach (VMP_ART_TipoArtigoRow Tipo in VMP_ART_TipoArtigo)
            {
                Tipo.Id = Guid.NewGuid();
                Tipo.CodigoArtigo = Artigo;
            }
        }

        public string GerarDescricaoArtigo_OLD(frmArtigoMundifiosView.Grupo Grupo)
        {
            string DescricaoArtigo = string.Empty;

            string DescricaoComponente = string.Empty;

            string DescricaoPercentagemComponente = string.Empty;

            if (!_FamiliasSel.IsCDU_DescricaoAbrevNull())
                DescricaoArtigo = DescricaoArtigo + " " + _FamiliasSel.CDU_DescricaoAbrev;

            // Dim RowSubFamilia() As SubFamiliasRow = SubFamilias.Select("Sel = true", "")
            // If RowSubFamilia.Length > 0 Then
            // DescricaoArtigo = DescricaoArtigo & " " & RowSubFamilia(0).Descricao
            // End If

            switch (Grupo)
            {
                case frmArtigoMundifiosView.Grupo.Fios:
                    {
                        if (!(_NESel == null))
                        {
                            if (Convert.ToDouble(_NESel.Codigo) != 0)
                                DescricaoArtigo = DescricaoArtigo + " " + _NESel.Descricao;
                        }

                        break;
                    }

                case frmArtigoMundifiosView.Grupo.Filamentos:
                    {
                        if (!(_TexturizacaoSel == null))
                        {
                            if (Convert.ToDouble(_TexturizacaoSel.Codigo) != 0)
                                DescricaoArtigo = DescricaoArtigo + " " + _TexturizacaoSel.Descricao;
                        }

                        break;
                    }
            }

            VMP_ART_ComponenteRow[] RowComponente = (VMP_ART_ComponenteRow[])VMP_ART_Componente.Select("Sel = true", "Percentagem desc,Ordem");
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


            DescricaoArtigo = DescricaoArtigo + " " + DescricaoPercentagemComponente;
            DescricaoArtigo = DescricaoArtigo + " " + DescricaoComponente;

            return DescricaoArtigo;

        }

        public void GerarDescricaoPercentagemComponentes()
        {
            _DescricaoComponente = string.Empty;
            _PercentagensComponente = string.Empty;

            VMP_ART_ComponenteRow[] RowComponente = (VMP_ART_ComponenteRow[])VMP_ART_Componente.Select("Sel = true", "Percentagem desc,Ordem");
            foreach (var item in RowComponente)
            {
                if (Conversion.Val(item.Codigo) == 0)
                    continue;
                _DescricaoComponente = _DescricaoComponente + "/" + item.Descricao;
                _PercentagensComponente = _PercentagensComponente + "/" + item.Percentagem;
            }

            if (_DescricaoComponente.Length > 0)
                _DescricaoComponente = Strings.Right(_DescricaoComponente, _DescricaoComponente.Length - 1);

            if (_PercentagensComponente.Length > 0)
                _PercentagensComponente = Strings.Right(_PercentagensComponente, _PercentagensComponente.Length - 1);
        }


        public long GetNumeroRegistosSelecionados(DataTable Table)
        {
            object Nr = Table.Select("Sel = true", "").Length;
            return Convert.ToInt32(Convert.IsDBNull(Nr) ? 0 : Nr);
        }

        public void RetirarSelecao(DataTable Table, bool SelPorDefeito)
        {
            foreach (DataRow Row in Table.Rows)
                Row["Sel"] = false;

            if (SelPorDefeito)
            {
                foreach (DataRow Row in Table.Rows)
                {
                    Row["Sel"] = true;
                    break;
                }
            }

            if (Table.TableName == "VMP_ART_Componente")
            {
                foreach (DataRow Row in Table.Rows)
                    Row["Percentagem"] = 0;
            }
        }


        public void RemoverPercentagem(DataTable Table)
        {
            foreach (DataRow Row in Table.Rows)
                Row["Percentagem"] = 0;
        }

        public string DescricaoAbreviadaFamilia()
        {
            FamiliasRow[] RegistosSelecionados = (FamiliasRow[])Familias.Select("Sel = True", "");
            if (!RegistosSelecionados[0].IsCDU_DescricaoAbrevNull())
                return RegistosSelecionados[0].CDU_DescricaoAbrev;
            else
                return "";
        }

        public long GetProximoCodigoLivre()
        {
            string sUltimoRegisto = AdptArtigo.GetLastNumeroByFamilia(_FamiliasSel.Familia + "%").ToString();
            // Dim sUltimoRegisto As String = AdptArtigo.GetLastNumeroByFamilia(Familias.GetSelecao & "%")
            long lUltimoRegisto = 0;

            long.TryParse(sUltimoRegisto, out lUltimoRegisto);
            return lUltimoRegisto + 1;
        }

        public int GetIndexRow(DataTable Tabela, string Filtro)
        {
            int Posicao = 0;

            foreach (DataRow r in Tabela.Select(Filtro))
            {
                r["Sel"] = true;
                Posicao = Tabela.Rows.IndexOf(r);
            }

            return Posicao;
        }

        public bool PreencherComposUtilizadorPorEmpresa(BasBE100.BasBEArtigo Artigo, string Empresa)
        {
            DadosAuxiliaresECRARow DadosARegistar;

            if (this.DadosAuxiliaresECRA.Select("Empresa = '" + Empresa + "'").Length > 0)
            {
                DadosARegistar = (DadosAuxiliaresECRARow)this.DadosAuxiliaresECRA.Select("Empresa = '" + Empresa + "'")[0];

                Artigo.ArmazemSugestao = DadosARegistar.ArmazemSugestao;
                Artigo.LocalizacaoSugestao = DadosARegistar.ArmazemSugestao;
                Artigo.TipoArtigo = DadosARegistar.TipoArtigo;
                Artigo.Classe = DadosARegistar.TipoComponente;

                if (DadosARegistar.IntrastatPautal.Length > 0)
                    Artigo.IntrastatCodigoPautal = DadosARegistar.IntrastatPautal;

                Artigo.CamposUtil["CDU_GrupoTaxaDesperdicio"].Valor = DadosARegistar.GrupoTaxaDesperdicio;
            }

            // convem garantir que todos os codigos estão vazios
            // Caso o utilizador altere um codigo antigo, mantenha o antigo caso nao limpasse
            Artigo.CamposUtil["CDU_CodigoAntigoMundifios"].Valor = string.Empty;
            Artigo.CamposUtil["CDU_CodigoAntigoAvefios"].Valor = string.Empty;
            Artigo.CamposUtil["CDU_CodigoAntigoInovafil"].Valor = string.Empty;
            Artigo.CamposUtil["CDU_CodigoAntigoYarntrade"].Valor = string.Empty;
            Artigo.CamposUtil["CDU_CodigoAntigoFilopa"].Valor = string.Empty;
            Artigo.CamposUtil["CDU_CodigoAntigo"].Valor = string.Empty;


            foreach (DadosAuxiliaresECRARow CodigoAntigo in this.DadosAuxiliaresECRA.Select("CodigoAntigo <> '' "))
            {
                switch (Strings.UCase(CodigoAntigo.Empresa))
                {
                    case "MUNDIFIOS":
                        {
                            Artigo.CamposUtil["CDU_CodigoAntigoMundifios"].Valor = CodigoAntigo.CodigoAntigo;
                            Artigo.CamposUtil["CDU_CodigoAntigo"].Valor = CodigoAntigo.CodigoAntigo;
                            break;
                        }

                    case "AVEFIOS":
                        {
                            Artigo.CamposUtil["CDU_CodigoAntigoAvefios"].Valor = CodigoAntigo.CodigoAntigo;
                            Artigo.CamposUtil["CDU_CodigoAntigo"].Valor = CodigoAntigo.CodigoAntigo;
                            break;
                        }

                    case "INOVAFIL":
                        {
                            Artigo.CamposUtil["CDU_CodigoAntigoInovafil"].Valor = CodigoAntigo.CodigoAntigo;
                            Artigo.CamposUtil["CDU_CodigoAntigo"].Valor = CodigoAntigo.CodigoAntigo;
                            break;
                        }

                    case "YARNTRADE":
                        {
                            Artigo.CamposUtil["CDU_CodigoAntigoYarntrade"].Valor = CodigoAntigo.CodigoAntigo;
                            Artigo.CamposUtil["CDU_CodigoAntigo"].Valor = CodigoAntigo.CodigoAntigo;
                            break;
                        }

                    case "FILOPA":
                        {
                            Artigo.CamposUtil["CDU_CodigoAntigoFilopa"].Valor = CodigoAntigo.CodigoAntigo;
                            Artigo.CamposUtil["CDU_CodigoAntigo"].Valor = CodigoAntigo.CodigoAntigo;
                            break;
                        }
                }
            }

            return true;
        }

        public FamiliasDataTable GetDadosPreDefinidosFamilia(string Connection)
        {
            FamiliasDataTable MyFamilias = new FamiliasDataTable();

            AdptFamilias.changeConnection(Connection);

            if (_FamiliasSel.Familia == null)
            {
                MessageBox.Show("Erro importante");
                MessageBox.Show("Erro importante");
            }
            AdptFamilias.FillByFamilia(MyFamilias, _FamiliasSel.Familia);

            AdptFamilias.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);

            return MyFamilias;
        }


        public string GetConnectionString(string NomeEmpresa)
        {
            string sConnectionString = string.Empty;

            for (int i = 0; i <= VariaveisGlobais.gLstEmpresasGrupo.Count - 1; i++)
            {
                if (VariaveisGlobais.gLstEmpresasGrupo[i].Empresa == NomeEmpresa)
                    return VariaveisGlobais.gLstEmpresasGrupo[i].ConnectionString;
            }
            return string.Empty;//ver melhor
        }

        public void ApagarDadosAuxiliares()
        {
            this.DadosAuxiliaresECRA.Clear();
        }

        public string GetDescricaoArmazem(string Connection, string Armazem)
        {
            DsArtigosMundifiosTableAdapters.QueriesTableAdapter adpt = new DsArtigosMundifiosTableAdapters.QueriesTableAdapter();
            adpt.changeConnection(Connection);

            string Descricao = adpt.GetDescricaoArmazemByArmazem(Armazem).ToString();

            if (Descricao != null)
                return Descricao;
            else
                return string.Empty;
        }

        public string GetDescricaoTipoArtigo(string Connection, string TipoArtigo)
        {
            DsArtigosMundifiosTableAdapters.QueriesTableAdapter adpt = new DsArtigosMundifiosTableAdapters.QueriesTableAdapter();
            adpt.changeConnection(Connection);

            string Descricao = adpt.GetDescricaoTipoArtigoByTipoArtigo(TipoArtigo).ToString();
            if (Descricao != null)
                return Descricao;
            else
                return string.Empty;
        }

        public string GetDescricaoIntrastatByIntrastat(string Connection, string Intrastat)
        {
            DsArtigosMundifiosTableAdapters.QueriesTableAdapter adpt = new DsArtigosMundifiosTableAdapters.QueriesTableAdapter();
            adpt.changeConnection(Connection);

            string Descricao = adpt.GetDescricaoIntrastatByIntrastat(Intrastat).ToString();
            if (Descricao != null)
                return Descricao;
            else
                return string.Empty;
        }

        public string GetDescricaoArtigoByArtigo(string Connection, string Artigo)
        {
            DsArtigosMundifiosTableAdapters.QueriesTableAdapter adpt = new DsArtigosMundifiosTableAdapters.QueriesTableAdapter();
            adpt.changeConnection(Connection);

            string Descricao = adpt.GetDescricaoArtigoByArtigo(Artigo).ToString();
            if (Descricao != null)
                return Descricao;
            else
                return string.Empty;
        }

        public TDU_GrupoTaxaDesperdicioRow GetGrupoTaxaDesperdicio(string Connection, string Codigo)
        {

            // Identificar os dados relativos à taxa porque foi alterado diretamente na grelha
            DsArtigosMundifiosTableAdapters.TDU_GrupoTaxaDesperdicioTableAdapter MyadpGrupoTaxaDesperdicio = new DsArtigosMundifiosTableAdapters.TDU_GrupoTaxaDesperdicioTableAdapter();
            MyadpGrupoTaxaDesperdicio.changeConnection(Connection);
            MyadpGrupoTaxaDesperdicio.FillByCodigo(this.TDU_GrupoTaxaDesperdicio, Codigo);
            MyadpGrupoTaxaDesperdicio.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);

            if (TDU_GrupoTaxaDesperdicio.Count > 0)
                return TDU_GrupoTaxaDesperdicio[0];
            else
                return null;

        }

        public void PreencherDadosAuxiliares(bool ArtigoExiste, string Artigo)
        {
            ApagarDadosAuxiliares();

            if (ArtigoExiste == false)
            {
                foreach (EmpresaGrupo item in VariaveisGlobais.gLstEmpresasGrupo)
                {
                    var MyFamilias = GetDadosPreDefinidosFamilia(item.ConnectionString);

                    DadosAuxiliaresECRARow NovoRegisto = DadosAuxiliaresECRA.NewDadosAuxiliaresECRARow();
                    NovoRegisto.Empresa = item.Empresa;

                    if (!(MyFamilias[0].IsNull("CDU_ArmazemSugestao")))
                    {
                        NovoRegisto.ArmazemSugestao = MyFamilias[0].CDU_ArmazemSugestao;
                        NovoRegisto.ArmazemSugestaoDescricao = GetDescricaoArmazem(item.ConnectionString, NovoRegisto.ArmazemSugestao);
                    }
                    else
                    {
                        NovoRegisto.ArmazemSugestao = "";
                        NovoRegisto.ArmazemSugestaoDescricao = "";
                    }

                    if (!(MyFamilias[0].IsNull("CDU_TipoArtigo")))
                        NovoRegisto.TipoArtigo = MyFamilias[0].CDU_TipoArtigo;
                    else
                        NovoRegisto.TipoArtigo = Convert.ToString(3);

                    NovoRegisto.TipoArtigoDescricao = GetDescricaoTipoArtigo(item.ConnectionString, MyFamilias[0].CDU_TipoArtigo);

                    NovoRegisto.TipoComponente = 0;
                    NovoRegisto.TipoComponenteDescricao = "Artigo Simples";

                    NovoRegisto.GrupoTaxaDesperdicio = "";
                    NovoRegisto.GrupoTaxaDesperdiciodescricao = "";
                    NovoRegisto.GrupoTaxaDesperdicioTaxa = 0;

                    NovoRegisto.IntrastatPautal = "";
                    NovoRegisto.IntrastatPautalDescricao = "";

                    NovoRegisto.CodigoAntigo = "";
                    NovoRegisto.CodigoAntigoDescricao = "";

                    DadosAuxiliaresECRA.AddDadosAuxiliaresECRARow(NovoRegisto);
                }
            }
            else
            {
                foreach (EmpresaGrupo item in VariaveisGlobais.gLstEmpresasGrupo)
                {

                    // Atualizar a connection

                    AdptDadosAuxiliaresECRA.changeConnection(item.ConnectionString);
                    // Identificar os dados

                    DadosAuxiliaresECRADataTable MYEcra = new DadosAuxiliaresECRADataTable();
                    DadosAuxiliaresECRADataTable MYEcra2 = new DadosAuxiliaresECRADataTable();
                    AdptDadosAuxiliaresECRA.FillByArtigo(MYEcra, Artigo);

                    MYEcra2 = MYEcra;

                    if (MYEcra.Count > 0)
                    {
                        foreach (DadosAuxiliaresECRARow Linha in MYEcra)
                            DadosAuxiliaresECRA.ImportRow(Linha);
                    }
                }

                AdptDadosAuxiliaresECRA.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
            }
        }


        #endregion

        #region


        public bool Devolve1SeFamiliaDefinida()
        {
            object FamiliaDefinida = this.Familias.Select("Sel = True").Length;
            return Convert.ToBoolean(Convert.IsDBNull(FamiliaDefinida) ? 0 : FamiliaDefinida);
        }

        public bool Devolve1SeComponentesValidos()
        {
            var Soma = VMP_ART_Componente.ObtemTotalPercentagem();
            if (Soma != 100)
                return false;
            else
                return true;
        }

        public bool Devolve1SeComponenteSemPercentagem()
        {
            return VMP_ART_Componente.Devolve1SeComponenteSemPercentagem();
        }

        public bool Devolve1SeNEDefinida()
        {
            object NEDefinida = this.VMP_ART_NE.Select("Sel = True").Length;
            return Convert.ToBoolean(Convert.IsDBNull(NEDefinida) ? 0 : NEDefinida);
        }

        /// <summary>
        ///     ''' Função que valida se o artigo pode ser removido ou não
        ///     ''' </summary>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool ValidaRemocaoVMP(string CodigoArtigo, string ConnectionString)
        {
            // true pode ser removido
            // Falso não pode ser removido

            bool resultado = false;

            try
            {

                // Alterar a coneção para a connection atual
                AdptQueries.changeConnection(ConnectionString);

                // Validação
                if (!ValidaRemocaoAlternativas(CodigoArtigo))
                    goto FIM;

                // Validação
                // If Not ValidaRemocaoAlternativas2(CodigoArtigo) Then GoTo FIM

                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;
                MessageBox.Show(ex.Message, "Problemas na validação da remoção das tabelas do VMP-PLAN ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
            }

        FIM:
            ;

            // Repor connection inicial
            AdptQueries.changeConnection(Properties.Settings.Default.PRIEMPREConnectionString);

            return resultado;
        }

        private bool ValidaRemocaoAlternativas(string CodigoArtigo)
        {
            // true pode ser removido
            // Falso não pode ser removido
            bool resultado = false;

            try
            {
                string Artigo = string.Empty;
                if (AdptQueries.ValidaRemocaoVIM_AlternativasByArtigo(CodigoArtigo) == null)
                    Artigo = null;
                else
                    Artigo = AdptQueries.ValidaRemocaoVIM_AlternativasByArtigo(CodigoArtigo).ToString();

                if (Artigo == null)
                    resultado = true;
                else
                {
                    MessageBox.Show(string.Format("O artigo {0} está referenciado na tabela VIM_Alternativas ", CodigoArtigo), "Operação inválida", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    resultado = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resultado = false;
            }

            return resultado;
        }

        private bool ValidaRemocaoAlternativas2(string CodigoArtigo)
        {
            // true pode ser removido
            // Falso não pode ser removido
            bool resultado = false;

            try
            {
                string Artigo = string.Empty;

                Artigo = AdptQueries.ValidaRemocaoVIM_AlternativasByArtigo(CodigoArtigo).ToString();
                if (Artigo == null)
                    resultado = true;
                else
                {
                    MessageBox.Show(string.Format("O artigo {0} está referenciado na tabela VIM_Alternativas ", CodigoArtigo), "Operação inválida", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    resultado = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resultado = false;
            }

            return resultado;
        }

        public bool ValidacoesDescricao()
        {
            if (Convert.ToInt32(Devolve1SeNEDefinida()) == 0)
            {
                MessageBox.Show("NE inválido.", "Validações", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }

            return true;
        }

        public bool ValidacoesPropriedades(string Artigo)
        {
            if (Convert.ToInt32(Devolve1SeComponentesValidos()) == 0)
            {
                MessageBox.Show("O somatório das percentagens dos componentes deverá ser de 100 %", "Componentes inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }

            if (Devolve1SeComponenteSemPercentagem())
            {
                MessageBox.Show("Existe um componente sem percentagem atribuída.", "Percentagem inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }

            if (Convert.ToDouble(_NESel.Codigo) == 0)
            {
                MessageBox.Show("A propriedade NE não é válida", "Propriedade inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }

            if (Convert.ToDouble(_TipoSel.Codigo) == 0)
            {
                MessageBox.Show("A propriedade Tipo não é válida", "Propriedade inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }
            // Garantir que todas as propriedades estão escolhidas!

            if (!GetTrueIfTodasPropriedadesEscolhias())
            {
                MessageBox.Show("Deverá escolher sempre uma propriedade em cada grelha.", "Propriedades em falta", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); return false;
            }

            // Garantir que não existe nenhum outro código com as mesmas características!!
            if (GetTrueIfArtigoExisteByPropriedades(Artigo))
                return false;

            return true;
        }

        public bool ValidacoesDadosAuxiliares()
        {
            foreach (DadosAuxiliaresECRARow Registo in DadosAuxiliaresECRA)
            {
                if (Registo.ArmazemSugestaoDescricao.Length == 0)
                {
                    MessageBox.Show(string.Format("Armazém inválido na empresa {0} ", Registo.Empresa), "Validações Armazém", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return false;
                }
                if (Registo.TipoArtigoDescricao.Length == 0)
                {
                    MessageBox.Show(string.Format("Tipo de Artigo inválido na empresa {0} ", Registo.Empresa), "Validações Tipo de Artigo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return false;
                }
                if (Registo.TipoComponente > 2)
                {
                    MessageBox.Show(string.Format("Tipo Componente inválido na empresa {0} ", Registo.Empresa), "Validações Tipo Componente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return false;
                }
                if (Registo.GrupoTaxaDesperdicio.Length > 0 & Registo.GrupoTaxaDesperdiciodescricao.Length == 0)
                {
                    MessageBox.Show(string.Format("Grupo inválido na empresa {0} ", Registo.Empresa), "Validações Grupo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return false;
                }
                if (Registo.IntrastatPautal.Length > 0 & Registo.IntrastatPautalDescricao.Length == 0)
                {
                    MessageBox.Show(string.Format("Intrastat inválido na empresa {0} ", Registo.Empresa), "Validações Intrastat", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return false;
                }
                if (Registo.CodigoAntigo.Length > 0 & Registo.CodigoAntigoDescricao.Length == 0)
                {
                    MessageBox.Show(string.Format("Código Antigo inválido na empresa {0} ", Registo.Empresa), "Validações Código Antigo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return false;
                }
            }
            return true;
        }

        public bool ValidacoesTodasEmpresas(string Artigo, bool isNew)
        {
            try
            {
                bool ArtigoExiste;

                foreach (EmpresaGrupo item in VariaveisGlobais.gLstEmpresasGrupo)
                {

                    // Limitar a uma empresa
                    // If item.Empresa <> EmpresaExclusica Then Continue For

                    if (item.Empresa != VariaveisGlobais.gLstEmpresasGrupo[0].Empresa)
                    {
                        AdptArtigo.changeConnection(item.ConnectionString);


                        ArtigoExiste = GetTrueIfArtigoExisteByCodigo(Artigo);

                        // If isNew And GetTrueIfArtigoExisteByCodigo(Artigo) Then
                        if (isNew & ArtigoExiste)
                        {
                            MessageBox.Show(string.Format("O artigo {0} já existe na empresa {1}", Artigo.ToString(), item.Empresa.ToString()), "Validações", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return false;
                        }

                        if (!isNew & ArtigoExiste == false)
                        {
                            MessageBox.Show(string.Format("O artigo {0} a editar não existe na empresa {1}", Artigo.ToString(), item.Empresa.ToString()), "Validações", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return false;
                        }

                        if (GetTrueIfArtigoExisteByPropriedades(Artigo))
                        {
                            MessageBox.Show(string.Format("Já existe o artigo {0} cujas características são iguais ao que pretende criar", _Artigo.Artigo.ToString()), "Validações", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validações", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                // Repor a connectionString
                AdptArtigo.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
            }
        }

        #endregion

    }
}

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource.DsArtigosMundifiosTableAdapters
{
    partial class ArtigoTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class QueriesTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_ComponenteArtigoTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_CaracteristicaArtigoTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_TipoArtigoTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class FamiliasTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class SubFamiliasTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_ComponenteTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_TipoTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_Torcao1TableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_Torcao2TableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_ReferenciaTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_CorTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_ConeTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_CaracteristicaTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_ProgramaTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_DimensaoTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_TexturizacaoTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class VMP_ART_CategoriaTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class IntrastatMercadoriaTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class DadosAuxiliaresECRATableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

    partial class TDU_GrupoTaxaDesperdicioTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }

}
