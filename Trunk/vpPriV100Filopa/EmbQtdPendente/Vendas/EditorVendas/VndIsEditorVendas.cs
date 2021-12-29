using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Vimaponto.PrimaveraV100;
using Generico;
using Microsoft.VisualBasic;

namespace EmbQtdPendente
{
    public class VndIsEditorVendas : EditorVendas
    {

        private StdBELista listQtdPendente;
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (this.DocumentoVenda.Tipodoc == "EMB" & this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor + "" != "" & BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "" != "")
                listQtdPendente = BSO.Consulta("select cd.CDU_NBL, ln.* from LinhasDoc ln inner join CabecDoc cd on cd.Id=ln.IdCabecDoc where ln.IdCabecDoc='" + this.DocumentoVenda.ID + "'");

        }


        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);



            // ################################################################################################################################################################
            // ############# JFC - 28/10/2019 - Preenchimento do campo CDU_QtdPendenteEmb. Tem como objetivo filtrar Embarques efetivos de Embarques previstos           ######
            // ################################################################################################################################################################

            if (this.DocumentoVenda.Tipodoc == "EMB" & this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor + "" != "" & BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "" != "")
            {

                // Primeira vez que se grava o EMB e o BL não se encontra preenchido.
                if (listQtdPendente.Vazia() & Strings.Trim(this.DocumentoVenda.CamposUtil["CDU_NBL"].Valor.ToString()) + "" == "0")
                {
                    for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                            PriV100Api.BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_QtdPendenteEmb=isnull(ln2.CDU_QtdPendenteEmb,0)+replace('" + this.DocumentoVenda.Linhas.GetEdita(i).Quantidade + "',',','.') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinha + "'");
                    }
                }

                // O EMB já foi gravado pelo menos uma vez,e continua com o BL por preencher. Necessário conferir quantidades.
                if (!listQtdPendente.Vazia() & Strings.Trim(this.DocumentoVenda.CamposUtil["CDU_NBL"].Valor.ToString()) + "" == "0")
                {
                    for (var j = 1; j <= listQtdPendente.NumLinhas(); j++)
                    {
                        listQtdPendente.Inicio();

                        for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                        {
                            if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" &  Guid.TryParse(this.DocumentoVenda.Linhas.GetEdita(i).IdLinha, out Guid idLinha) && listQtdPendente.Valor("Id") == idLinha)
                            {
                                if (this.DocumentoVenda.Linhas.GetEdita(i).Quantidade != listQtdPendente.Valor("Quantidade"))
                                    BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_QtdPendenteEmb= ln2.CDU_QtdPendenteEmb - replace('" + listQtdPendente.Valor("Quantidade") + "',',','.') + replace('" + this.DocumentoVenda.Linhas.GetEdita(i).Quantidade + "',',','.') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinha + "'");
                                break;
                            }
                        }
                    }
                }

                listQtdPendente.Inicio();
                // Primeira vez que se insere o BL num EMB já lançado. Necessário conferir quantidades.
                if (!listQtdPendente.Vazia() & Strings.Trim(this.DocumentoVenda.CamposUtil["CDU_NBL"].Valor.ToString()) != "0")
                {
                    // MsgBox listQtdPendente("CDU_NBL")
                    if (listQtdPendente.Valor("CDU_NBL") + "" == "0")
                    {
                        listQtdPendente.Inicio();

                        for (var j = 1; j <= listQtdPendente.NumLinhas(); j++)
                        {
                            for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                            {
                                if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & Guid.TryParse(this.DocumentoVenda.Linhas.GetEdita(i).IdLinha, out Guid idLinha) && listQtdPendente.Valor("Id") == idLinha)
                                {
                                    // MsgBox listQtdPendente("Quantidade")
                                    if (this.DocumentoVenda.Linhas.GetEdita(i).Quantidade != listQtdPendente.Valor("Quantidade"))
                                        PriV100Api.BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_QtdPendenteEmb= ln2.CDU_QtdPendenteEmb - replace('" + this.DocumentoVenda.Linhas.GetEdita(i).Quantidade + "',',','.') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinha + "'");
                                    else
                                        PriV100Api.BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_QtdPendenteEmb= ln2.CDU_QtdPendenteEmb - replace('" + this.DocumentoVenda.Linhas.GetEdita(i).Quantidade + "',',','.') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinha + "'");

                                    break;
                                }
                            }
                            listQtdPendente.Seguinte();
                        }
                    }
                }
            }

        }


    }
}
