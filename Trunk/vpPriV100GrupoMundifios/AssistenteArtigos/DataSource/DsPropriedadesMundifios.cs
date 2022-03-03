using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource
{


    partial class DsPropriedadesMundifios
    {

        #region Criação propriedades

        // # Criação de NE
        public VMP_ART_NERow _Ne
        {
            get
            {
                return VMP_ART_NE[0];
            }
        }

        // # Criação de NE
        public VMP_ART_ComponenteRow _Componente
        {
            get
            {
                return VMP_ART_Componente[0];
            }
        }
        #endregion
        partial class VMP_ART_BloqueiosDataTable
        {


            public bool _TabelaBloqueada;
            public bool TabelaBloqueada
            {
                get
                {
                    return _TabelaBloqueada;
                }
                set
                {
                    _TabelaBloqueada = value;
                }
            }

            public object VMP_ART_NE { get; private set; }

            public void teste(ref object Linha)
            {
                Interaction.MsgBox("");
            }
        }

        partial class VMP_ART_CorDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }
            private void VMP_ART_CorDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_CorRow cor = (VMP_ART_CorRow)e.Row;
                cor.Id = Guid.NewGuid();
                cor.Codigo = Strings.Format(GetNextCode() + 1, "00");
                cor.Cor = "";
                cor.Descricao = "";
                cor.DataIntroducao = DateTime.Now;
                cor.DataAtualizacao = DateTime.Now;
                cor.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                cor.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                cor.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                cor.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                cor.Ordenacao = 999;
            }
        }

        partial class VMP_ART_DimensaoDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_DimensaoDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_DimensaoRow dimensao = (VMP_ART_DimensaoRow)e.Row;
                dimensao.Id = Guid.NewGuid();
                dimensao.Codigo = Strings.Format(GetNextCode() + 1, "000");
                dimensao.Dimensao = 1;
                dimensao.Cabos = 1;
                dimensao.Filamentos = 1;
                dimensao.Descricao = "";
                dimensao.DataIntroducao = DateTime.Now;
                dimensao.DataAtualizacao = DateTime.Now;
                dimensao.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                dimensao.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                dimensao.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                dimensao.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                dimensao.Ordenacao = 999;
            }
        }

        partial class VMP_ART_CategoriaDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }


            private void VMP_ART_CategoriaDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_CategoriaRow categoria = (VMP_ART_CategoriaRow)e.Row;
                categoria.Id = Guid.NewGuid();
                categoria.Codigo = Strings.Format(GetNextCode() + 1, "000");
                categoria.Categoria = "";
                categoria.Descricao = "";
                categoria.DataIntroducao = DateTime.Now;
                categoria.DataAtualizacao = DateTime.Now;
                categoria.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                categoria.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                categoria.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                categoria.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                categoria.Ordenacao = 999;
            }
        }

        partial class VMP_ART_TexturizacaoDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_TexturizacaoDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_TexturizacaoRow texturizacao = (VMP_ART_TexturizacaoRow)e.Row;
                texturizacao.Id = Guid.NewGuid();
                texturizacao.Codigo = Strings.Format(GetNextCode() + 1, "000");
                texturizacao.Texturizacao = "";
                texturizacao.Descricao = "";
                texturizacao.DataIntroducao = DateTime.Now;
                texturizacao.DataAtualizacao = DateTime.Now;
                texturizacao.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                texturizacao.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                texturizacao.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                texturizacao.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                texturizacao.Ordenacao = 999;
            }
        }

        partial class VMP_ART_ProgramaDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_ProgramaDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_ProgramaRow programa = (VMP_ART_ProgramaRow)e.Row;
                programa.Id = Guid.NewGuid();
                programa.Codigo = Strings.Format(GetNextCode() + 1, "0000");
                programa.Programa = "";
                programa.Descricao = "";
                programa.DataIntroducao = DateTime.Now;
                programa.DataAtualizacao = DateTime.Now;
                programa.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                programa.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                programa.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                programa.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                programa.Ordenacao = 999;
            }

        }

        partial class VMP_ART_ConeDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_ConeDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_ConeRow cone = (VMP_ART_ConeRow)e.Row;
                cone.Id = Guid.NewGuid();
                cone.Codigo = Strings.Format(GetNextCode() + 1, "00");
                cone.Cone = "";
                cone.Descricao = "";
                cone.DataIntroducao = DateTime.Now;
                cone.DataAtualizacao = DateTime.Now;
                cone.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                cone.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                cone.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                cone.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                cone.Ordenacao = 999;
            }
        }

        partial class VMP_ART_ReferenciaDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_ReferenciaDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                DsPropriedadesMundifiosTableAdapters.VMP_ART_CorTableAdapter AdptCor = new DsPropriedadesMundifiosTableAdapters.VMP_ART_CorTableAdapter();
                VMP_ART_ReferenciaRow Referencia = (VMP_ART_ReferenciaRow)e.Row;
                Referencia.Id = Guid.NewGuid();
                Referencia.Codigo = Strings.Format(GetNextCode() + 1, "00000");
                Referencia.Ref = "";
                Referencia.Descricao = "";
                Referencia.IdCor = (Guid)AdptCor.GetIdCodigoZero();
                Referencia.DataIntroducao = DateTime.Now;
                Referencia.DataAtualizacao = DateTime.Now;
                Referencia.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                Referencia.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                Referencia.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                Referencia.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                Referencia.Ordenacao = 99999;
            }
        }

        partial class VMP_ART_Torcao2DataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_Torcao2DataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_Torcao2Row torcao2 = (VMP_ART_Torcao2Row)e.Row;
                torcao2.Id = Guid.NewGuid();
                torcao2.Codigo = Strings.Format(GetNextCode() + 1, "00");
                torcao2.Torcao = "";
                torcao2.Descricao = "";
                torcao2.DataIntroducao = DateTime.Now;
                torcao2.DataAtualizacao = DateTime.Now;
                torcao2.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                torcao2.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                torcao2.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                torcao2.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                torcao2.Ordenacao = 999;
            }
        }

        partial class VMP_ART_Torcao1DataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_Torcao1DataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_Torcao1Row torcao1 = (VMP_ART_Torcao1Row)e.Row;
                torcao1.Id = Guid.NewGuid();
                torcao1.Codigo = Strings.Format(GetNextCode() + 1, "00");
                torcao1.Torcao = "";
                torcao1.Descricao = "";
                torcao1.DataIntroducao = DateTime.Now;
                torcao1.DataAtualizacao = DateTime.Now;
                torcao1.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                torcao1.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                torcao1.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                torcao1.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                torcao1.Ordenacao = 999;
            }
        }

        partial class VMP_ART_CaracteristicaDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_CaracteristicaDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_CaracteristicaRow caracteristicas = (VMP_ART_CaracteristicaRow)e.Row;
                caracteristicas.Id = Guid.NewGuid();
                caracteristicas.Codigo = Strings.Format(GetNextCode() + 1, "000");
                caracteristicas.Caracteristica = "";
                caracteristicas.Descricao = "";
                caracteristicas.DataIntroducao = DateTime.Now;
                caracteristicas.DataAtualizacao = DateTime.Now;
                caracteristicas.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                caracteristicas.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                caracteristicas.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                caracteristicas.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                caracteristicas.Ordenacao = 999;
            }
        }

        partial class VMP_ART_TipoDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_TipoDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_TipoRow tipo = (VMP_ART_TipoRow)e.Row;
                tipo.Id = Guid.NewGuid();
                tipo.Codigo = Strings.Format(GetNextCode() + 1, "000");
                tipo.Tipo = "";
                tipo.Descricao = "";
                tipo.DataIntroducao = DateTime.Now;
                tipo.DataAtualizacao = DateTime.Now;
                tipo.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                tipo.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                tipo.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                tipo.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                tipo.Ordenacao = 999;
            }
        }

        partial class VMP_ART_ComponenteDataTable
        {
            public int GetMaxOrdem()
            {
                object Ordem = Compute("MAX(Ordem)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }


            private void VMP_ART_ComponenteDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_ComponenteRow componente = (VMP_ART_ComponenteRow)e.Row;
                componente.Id = Guid.NewGuid();
                componente.Codigo = Strings.Format(GetNextCode() + 1, "000");
                componente.Ordem = GetMaxOrdem() + 1;
                componente.Componente = "";
                componente.Descricao = "";
                componente.DataIntroducao = DateTime.Now;
                componente.DataAtualizacao = DateTime.Now;
                componente.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                componente.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                componente.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                componente.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                componente.Ordenacao = 999;
            }
        }

        partial class VMP_ART_NEDataTable
        {
            public int GetNextCode()
            {
                object Ordem = Compute("MAX(Codigo)", "");
                return Convert.IsDBNull(Ordem) ? 0 : Convert.ToInt32(Ordem);
            }

            private void VMP_ART_NEDataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                VMP_ART_NERow Ne = (VMP_ART_NERow)e.Row;
                Ne.Id = Guid.NewGuid();
                Ne.Codigo = Strings.Format(GetNextCode() + 1, "000");
                Ne.NE = 1;
                Ne.Cabos = 1;
                Ne.Descricao = "";
                Ne.DescricaoNm = "";
                Ne.DataIntroducao = DateTime.Now;
                Ne.DataAtualizacao = DateTime.Now;
                Ne.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                Ne.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                Ne.PostoAtualizacao = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
                Ne.UtilizadorAtualizacao = PriV100Api.BSO.Contexto.UtilizadorActual;
                Ne.Ordenacao = 999;
            }
        }

        public DsPropriedadesMundifiosTableAdapters.VMP_ART_NETableAdapter AdptNE { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_NETableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_ComponenteTableAdapter AdptComponente { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_ComponenteTableAdapter();

        public DsPropriedadesMundifiosTableAdapters.VMP_ART_TipoTableAdapter AdptTipo { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_TipoTableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_Torcao1TableAdapter AdptTorcao1 { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_Torcao1TableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_Torcao2TableAdapter AdptTorcao2 { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_Torcao2TableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_ReferenciaTableAdapter AdptReferencia { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_ReferenciaTableAdapter();

        public DsPropriedadesMundifiosTableAdapters.VMP_ART_CorTableAdapter AdptCor { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_CorTableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_ConeTableAdapter AdptCone { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_ConeTableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_CaracteristicaTableAdapter AdptCaracteristica { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_CaracteristicaTableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_ProgramaTableAdapter AdptPrograma { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_ProgramaTableAdapter();
        // 
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_DimensaoTableAdapter AdptDimensao { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_DimensaoTableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_TexturizacaoTableAdapter AdptTexturizacao { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_TexturizacaoTableAdapter();
        public DsPropriedadesMundifiosTableAdapters.VMP_ART_CategoriaTableAdapter AdptCategoria { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_CategoriaTableAdapter();

        public DsPropriedadesMundifiosTableAdapters.VMP_ART_BloqueiosTableAdapter AdptBloqueios { get; set; } = new DsPropriedadesMundifiosTableAdapters.VMP_ART_BloqueiosTableAdapter();



        public void GravarNEBD()
        {
            VMP_ART_NEDataTable Tabela_Ecra = (VMP_ART_NEDataTable)VMP_ART_NE.GetChanges();
            VMP_ART_NEDataTable Tabela_Ecra2 = (VMP_ART_NEDataTable)VMP_ART_NE.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptNE.Update(VMP_ART_NE);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptNE.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_NE.Clear();

                Tabela_Ecra2 = (VMP_ART_NEDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_NERow row in Tabela_Ecra2)
                    VMP_ART_NE.ImportRow(row);

                AdptNE.Update(VMP_ART_NE);
            }


            AdptNE.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
        }

        public void ReporDadosNe()
        {
            AdptNE.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptNE.Fill(VMP_ART_NE);
        }

        public void GravarComponente()
        {
            VMP_ART_ComponenteDataTable Tabela_Ecra = (VMP_ART_ComponenteDataTable)VMP_ART_Componente.GetChanges();
            VMP_ART_ComponenteDataTable Tabela_Ecra2 = (VMP_ART_ComponenteDataTable)VMP_ART_Componente.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptComponente.Update(VMP_ART_Componente);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptComponente.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Componente.Clear();

                Tabela_Ecra2 = (VMP_ART_ComponenteDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_ComponenteRow row in Tabela_Ecra2)
                    VMP_ART_Componente.ImportRow(row);
                AdptComponente.Update(VMP_ART_Componente);
            }

            AdptComponente.changeConnection(VariaveisGlobais.gLstEmpresasGrupo[0].ConnectionString);
        }

        public void ReporDadosComponente()
        {
            AdptComponente.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptComponente.Fill(VMP_ART_Componente);
        }




        public void GravarTipo()
        {
            VMP_ART_TipoDataTable Tabela_Ecra = (VMP_ART_TipoDataTable)VMP_ART_Tipo.GetChanges();
            VMP_ART_TipoDataTable Tabela_Ecra2 = (VMP_ART_TipoDataTable)VMP_ART_Tipo.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptTipo.Update(VMP_ART_Tipo);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptTipo.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Tipo.Clear();

                Tabela_Ecra2 = (VMP_ART_TipoDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_TipoRow row in Tabela_Ecra2)
                    VMP_ART_Tipo.ImportRow(row);
                AdptTipo.Update(VMP_ART_Tipo);
            }

            AdptTipo.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
        }

        public void ReporDadosTipo()
        {
            AdptTipo.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptTipo.Fill(VMP_ART_Tipo);
        }



        public void GravarCaracteristica()
        {
            VMP_ART_CaracteristicaDataTable Tabela_Ecra = (VMP_ART_CaracteristicaDataTable)VMP_ART_Caracteristica.GetChanges();
            VMP_ART_CaracteristicaDataTable Tabela_Ecra2 = (VMP_ART_CaracteristicaDataTable)VMP_ART_Caracteristica.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptCaracteristica.Update(VMP_ART_Caracteristica);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptCaracteristica.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Caracteristica.Clear();

                Tabela_Ecra2 = (VMP_ART_CaracteristicaDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_CaracteristicaRow row in Tabela_Ecra2)
                    VMP_ART_Caracteristica.ImportRow(row);
                AdptCaracteristica.Update(VMP_ART_Caracteristica);
            }

            AdptCaracteristica.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
        }

        public void ReporDadosCaracteristica()
        {
            AdptCaracteristica.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptCaracteristica.Fill(VMP_ART_Caracteristica);
        }



        public void GravarTorcao1()
        {
            VMP_ART_Torcao1DataTable Tabela_Ecra = (VMP_ART_Torcao1DataTable)VMP_ART_Torcao1.GetChanges();
            VMP_ART_Torcao1DataTable Tabela_Ecra2 = (VMP_ART_Torcao1DataTable)VMP_ART_Torcao1.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptTorcao1.Update(VMP_ART_Torcao1);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptTorcao1.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Torcao1.Clear();

                Tabela_Ecra2 = (VMP_ART_Torcao1DataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_Torcao1Row row in Tabela_Ecra2)
                    VMP_ART_Torcao1.ImportRow(row);
                AdptTorcao1.Update(VMP_ART_Torcao1);
            }
        }

        public void ReporDadosTorcao1()
        {
            AdptTorcao1.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptTorcao1.Fill(VMP_ART_Torcao1);
        }



        public void GravarTorcao2()
        {
            VMP_ART_Torcao2DataTable Tabela_Ecra = (VMP_ART_Torcao2DataTable)VMP_ART_Torcao2.GetChanges();
            VMP_ART_Torcao2DataTable Tabela_Ecra2 = (VMP_ART_Torcao2DataTable)VMP_ART_Torcao2.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptTorcao2.Update(VMP_ART_Torcao2);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptTorcao2.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Torcao2.Clear();

                Tabela_Ecra2 = (VMP_ART_Torcao2DataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_Torcao2Row row in Tabela_Ecra2)
                    VMP_ART_Torcao2.ImportRow(row);
                AdptTorcao2.Update(VMP_ART_Torcao2);
            }
        }

        public void ReporDadosTorcao2()
        {
            AdptTorcao2.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptTorcao2.Fill(VMP_ART_Torcao2);
        }



        public void GravarReferencia()
        {
            VMP_ART_ReferenciaDataTable Tabela_Ecra = (VMP_ART_ReferenciaDataTable)VMP_ART_Referencia.GetChanges();
            VMP_ART_ReferenciaDataTable Tabela_Ecra2 = (VMP_ART_ReferenciaDataTable)VMP_ART_Referencia.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptReferencia.Update(VMP_ART_Referencia);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptReferencia.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Referencia.Clear();

                Tabela_Ecra2 = (VMP_ART_ReferenciaDataTable)Tabela_Ecra.GetChanges();
                foreach (DsPropriedadesMundifios.VMP_ART_ReferenciaRow row in Tabela_Ecra2)
                    VMP_ART_Referencia.ImportRow(row);

                AdptReferencia.Update(VMP_ART_Referencia);
            }
        }

        public void ReporDadosReferencia()
        {
            AdptReferencia.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptReferencia.FillJoinCores(VMP_ART_Referencia);
        }



        public void GravarCone()
        {
            VMP_ART_ConeDataTable Tabela_Ecra = (VMP_ART_ConeDataTable)VMP_ART_Cone.GetChanges();
            VMP_ART_ConeDataTable Tabela_Ecra2 = (VMP_ART_ConeDataTable)VMP_ART_Cone.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptCone.Update(VMP_ART_Cone);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptCone.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Cone.Clear();

                Tabela_Ecra2 = (VMP_ART_ConeDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_ConeRow row in Tabela_Ecra2)
                    VMP_ART_Cone.ImportRow(row);
                AdptCone.Update(VMP_ART_Cone);
            }
            AdptCone.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
        }

        public void ReporDadosCone()
        {
            AdptCone.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptCone.Fill(VMP_ART_Cone);
        }



        public void GravarPrograma()
        {
            VMP_ART_ProgramaDataTable Tabela_Ecra = (VMP_ART_ProgramaDataTable)VMP_ART_Programa.GetChanges();
            VMP_ART_ProgramaDataTable Tabela_Ecra2 = (VMP_ART_ProgramaDataTable)VMP_ART_Programa.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptPrograma.Update(VMP_ART_Programa);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptPrograma.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Programa.Clear();

                Tabela_Ecra2 = (VMP_ART_ProgramaDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_ProgramaRow row in Tabela_Ecra2)
                    VMP_ART_Programa.ImportRow(row);
                AdptPrograma.Update(VMP_ART_Programa);
            }
            AdptPrograma.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
        }

        public void ReporDadosPrograma()
        {
            AdptPrograma.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptPrograma.Fill(VMP_ART_Programa);
        }



        public void GravarTexturizacao()
        {
            VMP_ART_TexturizacaoDataTable Tabela_Ecra = (VMP_ART_TexturizacaoDataTable)VMP_ART_Texturizacao.GetChanges();
            VMP_ART_TexturizacaoDataTable Tabela_Ecra2 = (VMP_ART_TexturizacaoDataTable)VMP_ART_Texturizacao.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptTexturizacao.Update(VMP_ART_Texturizacao);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptTexturizacao.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Texturizacao.Clear();

                Tabela_Ecra2 = (VMP_ART_TexturizacaoDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_TexturizacaoRow row in Tabela_Ecra2)
                    VMP_ART_Texturizacao.ImportRow(row);
                AdptTexturizacao.Update(VMP_ART_Texturizacao);
            }
            AdptTexturizacao.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
        }

        public void ReporDadosTexturizacao()
        {
            AdptTexturizacao.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptTexturizacao.Fill(VMP_ART_Texturizacao);
        }



        public void GravarCategoria()
        {
            VMP_ART_CategoriaDataTable Tabela_Ecra = (VMP_ART_CategoriaDataTable)VMP_ART_Categoria.GetChanges();
            VMP_ART_CategoriaDataTable Tabela_Ecra2 = (VMP_ART_CategoriaDataTable)VMP_ART_Categoria.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptCategoria.Update(VMP_ART_Categoria);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptCategoria.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Categoria.Clear();

                Tabela_Ecra2 = (VMP_ART_CategoriaDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_CategoriaRow row in Tabela_Ecra2)
                    VMP_ART_Categoria.ImportRow(row);
                AdptCategoria.Update(VMP_ART_Categoria);
            }

            AdptCategoria.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
        }

        public void ReporDadosCategoria()
        {
            AdptCategoria.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptCategoria.Fill(VMP_ART_Categoria);
        }



        public void GravarDimensao()
        {
            VMP_ART_DimensaoDataTable Tabela_Ecra = (VMP_ART_DimensaoDataTable)VMP_ART_Dimensao.GetChanges();
            VMP_ART_DimensaoDataTable Tabela_Ecra2 = (VMP_ART_DimensaoDataTable)VMP_ART_Dimensao.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptDimensao.Update(VMP_ART_Dimensao);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptDimensao.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Dimensao.Clear();

                Tabela_Ecra2 = (VMP_ART_DimensaoDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_DimensaoRow row in Tabela_Ecra2)
                    VMP_ART_Dimensao.ImportRow(row);
                AdptDimensao.Update(VMP_ART_Dimensao);
            }
            AdptDimensao.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
        }

        public void ReporDadosDimensao()
        {
            AdptDimensao.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptDimensao.Fill(VMP_ART_Dimensao);
        }



        public void GravarCor()
        {
            VMP_ART_CorDataTable Tabela_Ecra = (VMP_ART_CorDataTable)VMP_ART_Cor.GetChanges();
            VMP_ART_CorDataTable Tabela_Ecra2 = (VMP_ART_CorDataTable)VMP_ART_Cor.GetChanges();
            if (Tabela_Ecra == null)
                return;
            AdptCor.Update(VMP_ART_Cor);

            foreach (var item in VariaveisGlobais.gLstEmpresasGrupo)
            {
                if (item.Empresa == PriV100Api.BSO.Contexto.CodEmp)
                    continue;

                AdptCor.changeConnection(item.ConnectionString);

                // Limpara tabela senao ao adicionar dava erro!
                VMP_ART_Cor.Clear();

                Tabela_Ecra2 = (VMP_ART_CorDataTable)Tabela_Ecra.GetChanges();

                foreach (DsPropriedadesMundifios.VMP_ART_CorRow row in Tabela_Ecra2)
                    VMP_ART_Cor.ImportRow(row);
                AdptCor.Update(VMP_ART_Cor);
            }
            AdptCor.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
        }

        public void ReporDadosCor()
        {
            AdptCor.changeConnection(Properties.Settings.Default.PRIMUNDIFIOSConnectionString);
            AdptCor.Fill(VMP_ART_Cor);
        }

        public VMP_ART_BloqueiosRow _Bloqueio
        {
            get
            {
                return VMP_ART_Bloqueios[0];
            }
        }

        public void IdentificarBloqueio(string Tabela)
        {
            RemoverBloqueiosUtilizadorPosto();

            AdptBloqueios.FillByTabela(VMP_ART_Bloqueios, Tabela);

            VMP_ART_Bloqueios.TabelaBloqueada = false;

            if (VMP_ART_Bloqueios.Count == 0)
                RegistarBloqueio(Tabela);
            else if (VMP_ART_Bloqueios[0].Utilizador != PriV100Api.BSO.Contexto.UtilizadorActual)
                VMP_ART_Bloqueios.TabelaBloqueada = true;
        }

        public void RegistarBloqueio(string Tabela)
        {
            VMP_ART_BloqueiosRow NovoBloqueio = VMP_ART_Bloqueios.NewVMP_ART_BloqueiosRow();
            NovoBloqueio.Id = Guid.NewGuid();
            NovoBloqueio.Tabela = Tabela;
            NovoBloqueio.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
            NovoBloqueio.Posto = PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador();
            NovoBloqueio.Data = DateTime.Now;
            VMP_ART_Bloqueios.AddVMP_ART_BloqueiosRow(NovoBloqueio);
            AdptBloqueios.Update(VMP_ART_Bloqueios);
        }

        public void RemoverBloqueiosUtilizadorPosto()
        {
            AdptBloqueios.Fill(VMP_ART_Bloqueios);

            foreach (VMP_ART_BloqueiosRow row in VMP_ART_Bloqueios.Select("Utilizador = '" + PriV100Api.BSO.Contexto.UtilizadorActual + "' AND Posto = '" + PriV100Api.BSO.DSO.Plat.UtilAPIs.DaNomeComputador() + "'"))
                row.Delete();

            AdptBloqueios.Update(VMP_ART_Bloqueios);
        }


    }
}


namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource.DsPropriedadesMundifiosTableAdapters {
    
    
    public partial class VMP_ART_DimensaoTableAdapter {
    }

    partial class VMP_ART_NETableAdapter
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

    partial class VMP_ART_BloqueiosTableAdapter
    {
        public void changeConnection(string _Connection)
        {
            foreach (System.Data.IDbCommand it in CommandCollection)
                it.Connection.ConnectionString = _Connection;
        }
    }
}
