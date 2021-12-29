/*
-------------------------------------------------------------------------------------------
Data:	  02-09-2021
Autor:	  Rafael Oliveira
Solução:    GrupoMundifios
Objetivos:  Script para inserir tokens de todas as funcionalidades
Coment.: 
-------------------------------------------------------------------------------------------
*/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'CompraFio'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'CompraFio'
		,'Compra de Fios'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'CondPagRQ'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'CondPagRQ'
		,'Condicao Pagamento RQ'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'SerieC'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'SerieC'
		,'Garantir que o Documento foi transformado da serieC'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'ArmazemEntreposto'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'ArmazemEntreposto'
		,'Armazem Entreposto'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'ArtigosNovos'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'ArtigosNovos'
		,'ArtigosNovos'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'AvisoCompraTaras'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'AvisoCompraTaras'
		,'Insere linhas alertar que a Mundifios compra Taras'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'AlertaRupturaStkMin'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'AlertaRupturaStkMin'
		,'Executa a spAlertaArm11e17 para aviso de ruptura de stocks minimos nesses armazéns.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'AlertaObsCliente'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'AlertaObsCliente'
		,'Nas ECL/GCs alerta o comercial para especificações do cliente. dbo.Cliente.CDU_ObsEncomenda'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'AlertaCriarFornecedor'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'AlertaCriarFornecedor'
		,'Sempre que uma ficha de fornecedor é gravada, envia email com lista de Fornecedores que existem na Munditalia e que não existem na Mundifios. Necessário devido à integração da contabilidade.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'AlertaCriarCliente'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'AlertaCriarCliente'
		,'Sempre que uma ficha de cliente é gravada, envia email com lista de Clientes que existem na Munditalia e que não existem na Mundifios. Necessário devido à integração da contabilidade.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'CEC'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'CEC'
		,'Cria Confirmação de Encomenda'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'CertificadosFaturaPDF'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'CertificadosFaturaPDF'
		,'CertificadosFaturaPDF'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'CertificadosOrg'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'CertificadosOrg'
		,'CertificadosOrg'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'ComparaLinhasComissoes'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'ComparaLinhasComissoes'
		,'Valida comissoes diferentes nas Linhas da ECL'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'CustoTransportes'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'CustoTransportes'
		,'Caso seja documento de transporte, abre formulário FrmCustoTransportes.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'EmDisputa'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'EmDisputa'
		,'EmDisputa'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'FixCambioECL'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'FixCambioECL'
		,'FixCambioECL'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'GuiaCargaEstado'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'GuiaCargaEstado'
		,'Colocar as Guias de Carga deste artigo lote com o estado Expedido.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'IdiomaArtigo'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'IdiomaArtigo'
		,'Coloca a descrição do artigo no idioma do cliente, se for PT coloca a descrição + descriçãoExtra'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'InfoFioValidaLinha'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'InfoFioValidaLinha'
		,'Validacao da informação do fio'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'InstrucaoAcabamento'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'InstrucaoAcabamento'
		,'Acrescenta uma linha com instruções de acabamento no caso de artigos especiais.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'IntegracaoCambio'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'IntegracaoCambio'
		,'Atualiza o Cambio nas empresas do grupo'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'IntegracaoClientes'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'IntegracaoClientes'
		,'Verifica e atualiza informaçoes das empresas do grupo'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'LoteEncomendaIgualGuia'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'LoteEncomendaIgualGuia'
		,'Colocar o Lote da Encomendas igual ao da Guia'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'ValidacoesLLT'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'ValidacoesLLT'
		,'Nas LLT e LLR só permite gravar caso o Cliente seja nacional.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'ValidaPrBase'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'ValidaPrBase'
		,'Verificação de PrBase > PrecUnit, se sim não Grava. So para mercado Externo'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'ValidaStockGC'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'ValidaStockGC'
		,'Nas GCs verifica se existe stock no armazem indicado, sugere armazem com stock. Se sugestão for aceite altera na linha da GC e na ECL que lhe deu origem.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'VerificaCliente4FLevouArtLote'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'VerificaCliente4FLevouArtLote'
		,'Se GR e cliente 4F chama VerificaCliente4FLevouArtLote'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'VerificaTesteQuimico'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'VerificaTesteQuimico'
		,'Verifica se o teste quimico foi realizado.  Envia email para o laboratorio alertar para saida de ArtigoLote sem teste quimico.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'SugereLotes'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'SugereLotes'
		,'Sugere o próximo lote a ser utilizado.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'TarasDevolver'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'TarasDevolver'
		,'TarasDevolver'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'TestaPlafond'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'TestaPlafond'
		,'Tratar Plafond Cliente'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'FAC'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'FAC'
		,'Verifica se é tipo de documento FAC e executa algumas funcoes'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'Facol'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'Facol'
		,'Facol'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'PreencherPercentagemOrg'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'PreencherPercentagemOrg'
		,'definir as percentagens dos artigos certificados.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'PCustoPrTab'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'PCustoPrTab'
		,'Verificações Preços'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'IntegracaoCondPag'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'IntegracaoCondPag'
		,'IntegracaoCondPag'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'Encargos'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'Encargos'
		,'Encargos'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'CopiarLotes'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'CopiarLotes'
		,'Copia lotes de forma a ter uma centralização de ArtigoLotes criados.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'DyStar'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'DyStar'
		,'Adicionar linha especial nas compras ao fornecedor DyStar'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'MyTools'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'MyTools'
		,'Nos Embarques colocar o campo CDU_DataUltimaAtualizacao com a data de hoje. Este campo é utilizado no MyTools para as cores do semaforo. '
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'IntegracaoIntrastat'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'IntegracaoIntrastat'
		,'Tratar Intrastat '
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'Intrastat'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'Intrastat'
		,'Colocar valores no intrastat e colocar nas linhas'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'PrintPackingList'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'PrintPackingList'
		,'Nas GCs Imprime PackingList'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'PrecoBase0'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'PrecoBase0'
		,'Nao deixa gravar nenhuma documento caso o Preço Base seja 0'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'IntegracaoFilopaDestino'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'IntegracaoFilopaDestino'
		,'IntegracaoFilopaDestino'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'Default'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'Default'
		,'Default'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'CopiaEntreEmpresas'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'CopiaEntreEmpresas'
		,'CopiaEntreEmpresas'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'Inditex'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'Inditex'
		,'Ctrl + O - Abre o formulário da Inditex FrmInditex. '
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'FornecedoresCertificados'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'FornecedoresCertificados'
		,'Ctrl + Q - Formulário Certificados FrmFornecedoresCerts.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'HistoricoPlafond'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'HistoricoPlafond'
		,'Guarda o Histórico Plafond do Cliente'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'ValidaGrupoFG'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'ValidaGrupoFG'
		,'Garantir que faturas entre empresas o documento utilizado é FG/FGS ou NCG.'
		,'0'
		);
END
IF NOT EXISTS (
		SELECT 1
		FROM TDU_FuncionalidadesExt
		WHERE CDU_TokenFuncionalidade = 'QtdMinCliente'
		)
BEGIN
	INSERT INTO TDU_FuncionalidadesExt (
		CDU_TokenFuncionalidade
		,CDU_DescricaoFuncionalidade
		,CDU_AplicaFuncionalidade
		)
	VALUES (
		'QtdMinCliente'
		,'Alerta do comercial para quantidades minimas por Cliente. Valida as quantidade pela tabela TDU_QntMinimas.'
		,'0'
		);
END
GO
