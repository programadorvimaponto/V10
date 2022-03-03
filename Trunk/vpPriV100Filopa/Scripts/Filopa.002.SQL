/*
-------------------------------------------------------------------------------------------
Data:	    22-05-2018
Autor:	    Andre Sousa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Correções ao script inicial.
Coment.:    
-------------------------------------------------------------------------------------------
*/


--Adicionar novos CDU no cabeçalho - CDU_Porto CDU_Destino CDU_Localidade

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Porto'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Porto  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Porto'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'CabecDoc'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('CabecDoc', 
									  'CDU_Porto', 
									  'Porto', 
									  'Porto', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Porto'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Porto  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Porto'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'CabecDocRascunhos'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('CabecDocRascunhos', 
									  'CDU_Porto', 
									  'Porto', 
									  'Porto', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Destino'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Destino  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Destino'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'CabecDoc'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('CabecDoc', 
									  'CDU_Destino', 
									  'Destino', 
									  'Destino', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Destino'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Destino  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Destino'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'CabecDocRascunhos'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('CabecDocRascunhos', 
									  'CDU_Destino', 
									  'Destino', 
									  'Destino', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Localidade'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Localidade  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Localidade'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'CabecDoc'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('CabecDoc', 
									  'CDU_Localidade', 
									  'Localidade', 
									  'Localidade', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Localidade'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Localidade  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Localidade'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'CabecDocRascunhos'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('CabecDocRascunhos', 
									  'CDU_Localidade', 
									  'Localidade', 
									  'Localidade', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]')
		END
END
GO

--Adicionar novo CDU nas linhas - CDU_LoteFornecedor

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_LoteFornecedor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_LoteFornecedor NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_LoteFornecedor'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'LinhasDoc'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('LinhasDoc', 
									  'CDU_LoteFornecedor', 
									  'LoteFornecedor', 
									  'LoteFornecedor', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_LoteFornecedor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_LoteFornecedor NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_LoteFornecedor'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'LinhasDocRascunhos'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('LinhasDocRascunhos', 
									  'CDU_LoteFornecedor', 
									  'LoteFornecedor', 
									  'LoteFornecedor', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  NULL)
		END
END
GO

--Remover os campos Vendedor - Comissao Vendedor

BEGIN
	IF EXISTS (
			SELECT 1
			FROM sysobjects
			WHERE NAME = 'LinhasDoc_CDU_Vendedor_DF'
			)
	BEGIN
		ALTER TABLE LinhasDoc DROP CONSTRAINT LinhasDoc_CDU_Vendedor_DF
	END
END

BEGIN
	IF EXISTS (
			SELECT 1
			FROM sysobjects
			WHERE NAME = 'LinhasDocRascunhos_CDU_Vendedor_DF'
			)
	BEGIN
		ALTER TABLE LinhasDocRascunhos DROP CONSTRAINT LinhasDocRascunhos_CDU_Vendedor_DF
	END
END

BEGIN
	IF EXISTS (
			SELECT 1
			FROM sysobjects
			WHERE NAME = 'LinhasDoc_CDU_ComissaoVendedor_DF'
			)
	BEGIN
		ALTER TABLE LinhasDoc DROP CONSTRAINT LinhasDoc_CDU_ComissaoVendedor_DF
	END
END

BEGIN
	IF EXISTS (
			SELECT 1
			FROM sysobjects
			WHERE NAME = 'LinhasDocRascunhos_CDU_ComissaoVendedor_DF'
			)
	BEGIN
		ALTER TABLE LinhasDocRascunhos DROP CONSTRAINT LinhasDocRascunhos_CDU_ComissaoVendedor_DF
	END
END


BEGIN
	IF EXISTS (
			SELECT *
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Vendedor'				
			)
	BEGIN
		ALTER TABLE LinhasDoc DROP COLUMN CDU_Vendedor
		delete from StdCamposVar where campo = 'CDU_Vendedor' and Tabela='LinhasDoc'
		
	END
END
GO

BEGIN
	IF EXISTS (
			SELECT *
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Vendedor'				
			)
	BEGIN
		ALTER TABLE LinhasDocRascunhos DROP COLUMN CDU_Vendedor
		delete from StdCamposVar where campo = 'CDU_Vendedor' and Tabela='LinhasDocRascunhos'

	END
END
GO


BEGIN
	IF EXISTS (
			SELECT *
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_ComissaoVendedor'				
			)
	BEGIN
		ALTER TABLE LinhasDoc DROP COLUMN CDU_ComissaoVendedor
		delete from StdCamposVar where campo = 'CDU_ComissaoVendedor' and Tabela='LinhasDoc'
		
	END
END
GO

BEGIN
	IF EXISTS (
			SELECT *
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_ComissaoVendedor'				
			)
	BEGIN
		ALTER TABLE LinhasDocRascunhos DROP COLUMN CDU_ComissaoVendedor
		delete from StdCamposVar where campo = 'CDU_ComissaoVendedor' and Tabela='LinhasDocRascunhos'
	END
END
GO

BEGIN
	IF NOT EXISTS (Select 1 from StdCamposVar where Tabela='Fornecedores' and Campo = 'CDU_Vendedor')

		BEGIN
			DECLARE @Var INT
			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			INSERT INTO StdCamposVar (Tabela, 
												  Campo, 
												  Descricao, 
												  Texto, 
												  Visivel, 
												  Ordem, 
												  ValorDefeito, 
												  Query) 
										  VALUES ('Fornecedores', 
												  'CDU_Vendedor', 
												  'Vendedor', 
												  'Vendedor', 
												  1, 
												  @Var + 1, 
												  NULL, 
												  'SELECT [Vendedor],[Nome],[Comissao] FROM [Vendedores] WITH (NOLOCK)')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'Clientes'
				AND COLUMN_NAME = 'CDU_Comissao'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE Clientes ADD CDU_Comissao  REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'Clientes' AND Campo = 'CDU_Comissao'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'Clientes'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('Clientes', 
									  'CDU_Comissao', 
									  'Comissão', 
									  'Comissão', 
									  1, 
									  @Var + 1, 
									  0, 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'Fornecedores'
				AND COLUMN_NAME = 'CDU_UsaComissaoVendedor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE Fornecedores ADD CDU_UsaComissaoVendedor  BIT NULL

			DELETE StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_UsaComissaoVendedor'

			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'Fornecedores' 
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('Fornecedores', 
									  'CDU_UsaComissaoVendedor', 
									  'UsaComissãoVendedor', 
									  'UsaComissãoVendedor', 
									  1, 
									  @Var + 1, 
									  'TRUE', 
									  NULL)
		END
END
GO




--Tornar os campos de utilizador invisiveis (stdcamposvar)


BEGIN 

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Fornecedor'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Fornecedor'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Agente'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Agente'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Comissao'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Comissao'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Incoterms'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Incoterms'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_CustoFrete'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_CustoFrete'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_NCartaCredito'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_NCartaCredito'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DataLimiteEmissaoCC'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DataLimiteEmissaoCC'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DataPrevistaChegada'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DataPrevistaChegada'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_CompanhiaMaritima'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_CompanhiaMaritima'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Navio'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Navio'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_NBL'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_NBL'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DataComunicacaoCliente'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DataComunicacaoCliente'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Banco'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Banco'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Agente'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Agente'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Comissao'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Comissao'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Observacoes'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Observacoes'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_DataPrevistaEmbarque'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_DataPrevistaEmbarque'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Incoterms'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Incoterms'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_DPEAlteradaMotivo'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_DPEAlteradaMotivo'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ETRCliente'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ETRCliente'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ETRFornecedor'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ETRFornecedor'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Certificado1'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Certificado1'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_EstadoPagamento'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_EstadoPagamento'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_CertificadoTratado1'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_CertificadoTratado1'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Certificado2'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Certificado2'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_CertificadoTratado2'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_CertificadoTratado2'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_PesoLiquido'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_PesoLiquido'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_PesoBruto'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_PesoBruto'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NVolumes'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NVolumes'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NFatura'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NFatura'

	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NContentor'
	UPDATE StdCamposVar SET  Visivel = 0 WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NContentor'

			
END