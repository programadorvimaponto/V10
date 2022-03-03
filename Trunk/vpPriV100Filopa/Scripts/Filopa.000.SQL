/*
-------------------------------------------------------------------------------------------
Data:	    05-04-2018
Autor:	    Andre Sousa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Criação de tabelas específicas para o cliente Filopa.
Coment.:    
-------------------------------------------------------------------------------------------
*/


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_NAME = 'TDU_Agentes'
			)
	BEGIN
		CREATE TABLE TDU_Agentes (
			CDU_Codigo NVARCHAR(25) NOT NULL
			,CDU_Descricao NVARCHAR(100) NULL			
			,CONSTRAINT PK_TDU_Agentes PRIMARY KEY NONCLUSTERED (CDU_Codigo ASC)
			)

		DELETE
		FROM StdCamposVar
		WHERE Tabela = 'TDU_Agentes'

		DELETE
		FROM StdTabelasVar
		WHERE Tabela = 'TDU_Agentes'

		INSERT INTO StdTabelasVar (
			Tabela
			,Apl
			)
		VALUES (
			'TDU_Agentes'
			,'ERP'
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_Agentes'
			,'CDU_Codigo'
			,'Código'
			,'Código'
			,1
			,1
			,NULL
			,NULL
			,NULL
			,0
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_Agentes'
			,'CDU_Descricao'
			,'Descrição'
			,'Descrição'
			,1
			,2
			,NULL
			,NULL
			,NULL
			,0
			)
		
	END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_NAME = 'TDU_CompanhiasMaritimas'
			)
	BEGIN
		CREATE TABLE TDU_CompanhiasMaritimas (
			CDU_Codigo NVARCHAR(25)NOT NULL
			,CDU_Descricao NVARCHAR(100) NULL			
			,CONSTRAINT PK_TDU_CompanhiasMaritimas PRIMARY KEY NONCLUSTERED (CDU_Codigo ASC)
			)

		DELETE
		FROM StdCamposVar
		WHERE Tabela = 'TDU_CompanhiasMaritimas'

		DELETE
		FROM StdTabelasVar
		WHERE Tabela = 'TDU_CompanhiasMaritimas'

		INSERT INTO StdTabelasVar (
			Tabela
			,Apl
			)
		VALUES (
			'TDU_CompanhiasMaritimas'
			,'ERP'
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_CompanhiasMaritimas'
			,'CDU_Codigo'
			,'Código'
			,'Código'
			,1
			,1
			,NULL
			,NULL
			,NULL
			,0
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_CompanhiasMaritimas'
			,'CDU_Descricao'
			,'Descrição'
			,'Descrição'
			,1
			,2
			,NULL
			,NULL
			,NULL
			,0
			)
		
	END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_NAME = 'TDU_EstadoPagamento'
			)
	BEGIN
		CREATE TABLE TDU_EstadoPagamento (
			CDU_Codigo NVARCHAR(25) NOT NULL
			,CDU_Descricao NVARCHAR(100) NULL			
			,CONSTRAINT PK_TDU_EstadoPagamento PRIMARY KEY NONCLUSTERED (CDU_Codigo ASC)
			)

		DELETE
		FROM StdCamposVar
		WHERE Tabela = 'TDU_EstadoPagamento'

		DELETE
		FROM StdTabelasVar
		WHERE Tabela = 'TDU_EstadoPagamento'

		INSERT INTO StdTabelasVar (
			Tabela
			,Apl
			)
		VALUES (
			'TDU_EstadoPagamento'
			,'ERP'
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_EstadoPagamento'
			,'CDU_Codigo'
			,'Código'
			,'Código'
			,1
			,1
			,NULL
			,NULL
			,NULL
			,0
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_EstadoPagamento'
			,'CDU_Descricao'
			,'Descrição'
			,'Descrição'
			,1
			,2
			,NULL
			,NULL
			,NULL
			,0
			)
		
	END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_NAME = 'TDU_Certificados'
			)
	BEGIN
		CREATE TABLE TDU_Certificados (
			CDU_Codigo NVARCHAR(25) NOT NULL
			,CDU_Descricao NVARCHAR(100) NULL			
			,CONSTRAINT PK_TDU_Certificados PRIMARY KEY NONCLUSTERED (CDU_Codigo ASC)
			)

		DELETE
		FROM StdCamposVar
		WHERE Tabela = 'TDU_Certificados'

		DELETE
		FROM StdTabelasVar
		WHERE Tabela = 'TDU_Certificados'

		INSERT INTO StdTabelasVar (
			Tabela
			,Apl
			)
		VALUES (
			'TDU_Certificados'
			,'ERP'
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_Certificados'
			,'CDU_Codigo'
			,'Código'
			,'Código'
			,1
			,1
			,NULL
			,NULL
			,NULL
			,0
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_Certificados'
			,'CDU_Descricao'
			,'Descrição'
			,'Descrição'
			,1
			,2
			,NULL
			,NULL
			,NULL
			,0
			)
		
	END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_NAME = 'TDU_Locais'
			)
	BEGIN
		CREATE TABLE TDU_Locais (
			CDU_Local NVARCHAR(25) NOT NULL
			,CDU_Descricao NVARCHAR(200) NULL			
			,CONSTRAINT PK_TDU_Locais PRIMARY KEY NONCLUSTERED (CDU_Local ASC)
			)

		DELETE
		FROM StdCamposVar
		WHERE Tabela = 'TDU_Locais'

		DELETE
		FROM StdTabelasVar
		WHERE Tabela = 'TDU_Locais'

		INSERT INTO StdTabelasVar (
			Tabela
			,Apl
			)
		VALUES (
			'TDU_Locais'
			,'ERP'
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_Locais'
			,'CDU_Local'
			,'Local'
			,'Local'
			,1
			,1
			,NULL
			,NULL
			,NULL
			,0
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_Locais'
			,'CDU_Descricao'
			,'Descrição'
			,'Descrição'
			,1
			,2
			,NULL
			,NULL
			,NULL
			,0
			)
		
	END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'Fornecedores'
				AND COLUMN_NAME = 'CDU_Vendedor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE Fornecedores ADD CDU_Vendedor  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_Vendedor'

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

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'Fornecedores'
				AND COLUMN_NAME = 'CDU_Comissao'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE Fornecedores ADD CDU_Comissao  REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_Comissao'

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
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Fornecedor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Fornecedor  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Fornecedor'

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
									  'CDU_Fornecedor', 
									  'Fornecedor', 
									  'Fornecedor', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT [Fornecedor],[Nome],[CDU_Vendedor] FROM [Fornecedores] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Agente'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Agente  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Agente'

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
									  'CDU_Agente', 
									  'Agente', 
									  'Agente', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Agentes] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Comissao'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Comissao  REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Comissao'

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
									  'CDU_Comissao', 
									  'Comissão', 
									  'Comissão', 
									  0, 
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
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Incoterms'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Incoterms  NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Incoterms'

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
									  'CDU_Incoterms', 
									  'Incoterms', 
									  'Incoterms', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [IntrastatCondEntrega] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_CustoFrete'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_CustoFrete  REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_CustoFrete'

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
									  'CDU_CustoFrete', 
									  'Custo Frete', 
									  'Custo Frete', 
									  0, 
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
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_NCartaCredito'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_NCartaCredito NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_NCartaCredito'

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
									  'CDU_NCartaCredito', 
									  'Nº Carta Crédito', 
									  'Nº Carta Crédito', 
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
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_DataLimiteEmissaoCC'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_DataLimiteEmissaoCC  DATETIME NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DataLimiteEmissaoCC'

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
									  'CDU_DataLimiteEmissaoCC', 
									  'Data Limite Emissão CC', 
									  'DataLimiteEmissãoCC', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_DataPrevistaChegada'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_DataPrevistaChegada  DATETIME NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DataPrevistaChegada'

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
									  'CDU_DataPrevistaChegada', 
									  'Data Chegada Prev.', 
									  'Data Chegada Prev.', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_CompanhiaMaritima'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_CompanhiaMaritima  NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_CompanhiaMaritima'

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
									  'CDU_CompanhiaMaritima', 
									  'Companhia Marítima', 
									  'Companhia Marítima', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_CompanhiasMaritimas] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Navio'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Navio  NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Navio'

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
									  'CDU_Navio', 
									  'Navio', 
									  'Navio', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_NBL'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_NBL  NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_NBL'

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
									  'CDU_NBL', 
									  'NBL', 
									  'NBL', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_DataComunicacaoCliente'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_DataComunicacaoCliente  DATETIME NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DataComunicacaoCliente'

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
									  'CDU_DataComunicacaoCliente', 
									  'Data Comunição Cliente', 
									  'DataCom.Cliente', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Banco'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDoc ADD CDU_Banco NVARCHAR(3) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Banco'

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
									  'CDU_Banco', 
									  'Banco', 
									  'Banco', 
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT * FROM [TDU_Bancos] WITH (NOLOCK) ORDER BY [CDU_Banco]')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Agente'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Agente NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Agente'

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
									  'CDU_Agente', 
									  'Agente', 
									  'Agente', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT [CDU_Agente] FROM [CabecDoc] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Comissao'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Comissao REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Comissao'

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
									  'CDU_Comissao', 
									  'Comissão', 
									  'Comissão', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Observacoes'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Observacoes NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Observacoes'

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
									  'CDU_Observacoes', 
									  'Observacões', 
									  'Observacões', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_DataPrevistaEmbarque'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_DataPrevistaEmbarque DATETIME NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_DataPrevistaEmbarque'

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
									  'CDU_DataPrevistaEmbarque', 
									  'DataPrev.Embarque', 
									  'DataPrev.Embarque', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Incoterms'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Incoterms NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Incoterms'

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
									  'CDU_Incoterms', 
									  'Incoterms', 
									  'Incoterms', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT [CDU_Incoterms] FROM [CabecDoc] WITH (NOLOCK)')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_DPEAlteradaMotivo'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_DPEAlteradaMotivo NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_DPEAlteradaMotivo'

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
									  'CDU_DPEAlteradaMotivo', 
									  'DPE Alterada Motivo', 
									  'DPEAlteradaMotivo', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_ETRCliente'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_ETRCliente SMALLINT NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ETRCliente'

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
									  'CDU_ETRCliente', 
									  'ETR Cliente', 
									  'ETR Cliente', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_ETRFornecedor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_ETRFornecedor SMALLINT NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ETRFornecedor'

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
									  'CDU_ETRFornecedor', 
									  'ETR Fornecedor', 
									  'ETR Fornecedor', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Certificado1'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Certificado1 SMALLINT NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Certificado1'

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
									  'CDU_Certificado1', 
									  'Certificado 1', 
									  'Certificado 1', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_EstadoPagamento'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_EstadoPagamento NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_EstadoPagamento'

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
									  'CDU_EstadoPagamento', 
									  'Estado Pagamento', 
									  'Estado Pagamento', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_CertificadoTratado1'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_CertificadoTratado1 NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_CertificadoTratado1'

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
									  'CDU_CertificadoTratado1', 
									  'Certificado Tratado 1', 
									  'CertificadoTratado1', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Certificados] WITH (NOLOCK)')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Certificado2'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Certificado2 SMALLINT NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Certificado2'

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
									  'CDU_Certificado2', 
									  'Certificado 2', 
									  'Certificado 2', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_CertificadoTratado2'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_CertificadoTratado2 NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_CertificadoTratado2'

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
									  'CDU_CertificadoTratado2', 
									  'Certificado Tratado 2', 
									  'CertificadoTratado2', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Certificados] WITH (NOLOCK)')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_PesoLiquido'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_PesoLiquido Float NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_PesoLiquido'

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
									  'CDU_PesoLiquido', 
									  'Peso Líquido', 
									  'Peso Líquido', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_PesoBruto'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_PesoBruto Float NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_PesoBruto'

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
									  'CDU_PesoBruto', 
									  'Peso Bruto', 
									  'Peso Bruto', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_NVolumes'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_NVolumes NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NVolumes'

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
									  'CDU_NVolumes', 
									  'Nº Volumes', 
									  'Nº Volumes', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_NFatura'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_NFatura NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NFatura'

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
									  'CDU_NFatura', 
									  'Nº Fatura', 
									  'Nº Fatura', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_NContentor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_NContentor NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NContentor'

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
									  'CDU_NContentor', 
									  'Nº Contentor', 
									  'Nº Contentor', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO



--BEGIN
--	IF NOT EXISTS (
--			SELECT 1
--			FROM INFORMATION_SCHEMA.COLUMNS
--			WHERE TABLE_NAME = 'LinhasDoc'
--				AND COLUMN_NAME = 'CDU_Vendedor'
--			)
--		BEGIN
--			DECLARE @Var INT
--			ALTER TABLE LinhasDoc ADD CDU_Vendedor NVARCHAR(100) NULL

--			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Vendedor'

--			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
--			WHERE TABELA = 'LinhasDoc'
--			INSERT INTO StdCamposVar (Tabela, 
--									  Campo, 
--									  Descricao, 
--									  Texto, 
--									  Visivel, 
--									  Ordem, 
--									  ValorDefeito, 
--									  Query) 
--							  VALUES ('LinhasDoc', 
--									  'CDU_Vendedor', 
--									  'Vendedor', 
--									  'Vendedor', 
--									  0, 
--									  @Var + 1, 
--									  'SELECT [CDU_Vendedor] FROM [Fornecedores] WITH (NOLOCK)', 
--									  NULL)
--		END
--END
--GO


--BEGIN
--	IF NOT EXISTS (
--			SELECT 1
--			FROM INFORMATION_SCHEMA.COLUMNS
--			WHERE TABLE_NAME = 'LinhasDoc'
--				AND COLUMN_NAME = 'CDU_ComissaoVendedor'
--			)
--		BEGIN
--			DECLARE @Var INT
--			ALTER TABLE LinhasDoc ADD CDU_ComissaoVendedor REAL NULL

--			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ComissaoVendedor'

--			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
--			WHERE TABELA = 'LinhasDoc'
--			INSERT INTO StdCamposVar (Tabela, 
--									  Campo, 
--									  Descricao, 
--									  Texto, 
--									  Visivel, 
--									  Ordem, 
--									  ValorDefeito, 
--									  Query) 
--							  VALUES ('LinhasDoc', 
--									  'CDU_ComissaoVendedor', 
--									  'Comissão Vendedor', 
--									  'Comissão Vendedor', 
--									  0, 
--									  @Var + 1, 
--									  0, 
--									  NULL)
--		END
--END
--GO



------------------------
--Campos Para Rascunhos
------------------------

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Fornecedor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Fornecedor  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Fornecedor'

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
									  'CDU_Fornecedor', 
									  'Fornecedor', 
									  'Fornecedor', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT [Fornecedor],[Nome],[CDU_Vendedor] FROM [Fornecedores] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Agente'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Agente  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Agente'

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
									  'CDU_Agente', 
									  'Agente', 
									  'Agente', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Agentes] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Comissao'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Comissao  REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Comissao'

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
									  'CDU_Comissao', 
									  'Comissão', 
									  'Comissão', 
									  0, 
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
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Incoterms'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Incoterms  NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Incoterms'

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
									  'CDU_Incoterms', 
									  'Incoterms', 
									  'Incoterms', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [IntrastatCondEntrega] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_CustoFrete'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_CustoFrete  REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_CustoFrete'

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
									  'CDU_CustoFrete', 
									  'Custo Frete', 
									  'Custo Frete', 
									  0, 
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
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_NCartaCredito'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_NCartaCredito  INT NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_NCartaCredito'

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
									  'CDU_NCartaCredito', 
									  'Nº Carta Crédito', 
									  'Nº Carta Crédito', 
									  0, 
									  @Var + 1, 
									  1, 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_DataLimiteEmissaoCC'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_DataLimiteEmissaoCC  DATETIME NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DataLimiteEmissaoCC'

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
									  'CDU_DataLimiteEmissaoCC', 
									  'Data Limite Emissão CC', 
									  'DataLimiteEmissãoCC', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_DataPrevistaChegada'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_DataPrevistaChegada  DATETIME NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DataPrevistaChegada'

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
									  'CDU_DataPrevistaChegada', 
									  'Data Chegada Prev.', 
									  'Data Chegada Prev.', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_CompanhiaMaritima'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_CompanhiaMaritima  NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_CompanhiaMaritima'

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
									  'CDU_CompanhiaMaritima', 
									  'Companhia Marítima', 
									  'Companhia Marítima', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_CompanhiasMaritimas] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Navio'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Navio  NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Navio'

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
									  'CDU_Navio', 
									  'Navio', 
									  'Navio', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_NBL'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_NBL  NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_NBL'

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
									  'CDU_NBL', 
									  'NBL', 
									  'NBL', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_DataComunicacaoCliente'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_DataComunicacaoCliente  DATETIME NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DataComunicacaoCliente'

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
									  'CDU_DataComunicacaoCliente', 
									  'Data Comunição Cliente', 
									  'DataCom.Cliente', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_Banco'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE CabecDocRascunhos ADD CDU_Banco NVARCHAR(3) NULL

			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Banco'

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
									  'CDU_Banco', 
									  'Banco', 
									  'Banco', 
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT * FROM [TDU_Bancos] WITH (NOLOCK) ORDER BY [CDU_Banco]')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Agente'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Agente NVARCHAR(25) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Agente'

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
									  'CDU_Agente', 
									  'Agente', 
									  'Agente', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT [CDU_Agente] FROM [CabecDocRascunhos] WITH (NOLOCK)')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Comissao'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Comissao REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Comissao'

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
									  'CDU_Comissao', 
									  'Comissão', 
									  'Comissão', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Observacoes'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Observacoes NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Observacoes'

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
									  'CDU_Observacoes', 
									  'Observacões', 
									  'Observacões', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_DataPrevistaEmbarque'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_DataPrevistaEmbarque DATETIME NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_DataPrevistaEmbarque'

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
									  'CDU_DataPrevistaEmbarque', 
									  'DataPrev.Embarque', 
									  'DataPrev.Embarque', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Incoterms'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Incoterms NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Incoterms'

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
									  'CDU_Incoterms', 
									  'Incoterms', 
									  'Incoterms', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT [CDU_Incoterms] FROM [CabecDocRascunhos] WITH (NOLOCK)')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_DPEAlteradaMotivo'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_DPEAlteradaMotivo NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_DPEAlteradaMotivo'

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
									  'CDU_DPEAlteradaMotivo', 
									  'DPE Alterada Motivo', 
									  'DPEAlteradaMotivo', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_ETRCliente'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_ETRCliente SMALLINT NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ETRCliente'

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
									  'CDU_ETRCliente', 
									  'ETR Cliente', 
									  'ETR Cliente', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_ETRFornecedor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_ETRFornecedor SMALLINT NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ETRFornecedor'

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
									  'CDU_ETRFornecedor', 
									  'ETR Fornecedor', 
									  'ETR Fornecedor', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Certificado1'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Certificado1 SMALLINT NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Certificado1'

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
									  'CDU_Certificado1', 
									  'Certificado 1', 
									  'Certificado 1', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_EstadoPagamento'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_EstadoPagamento NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_EstadoPagamento'

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
									  'CDU_EstadoPagamento', 
									  'Estado Pagamento', 
									  'Estado Pagamento', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_CertificadoTratado1'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_CertificadoTratado1 NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_CertificadoTratado1'

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
									  'CDU_CertificadoTratado1', 
									  'Certificado Tratado 1', 
									  'CertificadoTratado1', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Certificados] WITH (NOLOCK)')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Certificado2'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Certificado2 SMALLINT NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Certificado2'

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
									  'CDU_Certificado2', 
									  'Certificado 2', 
									  'Certificado 2', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_CertificadoTratado2'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_CertificadoTratado2 NVARCHAR(100) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_CertificadoTratado2'

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
									  'CDU_CertificadoTratado2', 
									  'Certificado Tratado 2', 
									  'CertificadoTratado2', 
									  0, 
									  @Var + 1, 
									  NULL, 
									  'SELECT * FROM [TDU_Certificados] WITH (NOLOCK)')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_PesoLiquido'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_PesoLiquido Float NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_PesoLiquido'

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
									  'CDU_PesoLiquido', 
									  'Peso Líquido', 
									  'Peso Líquido', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_PesoBruto'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_PesoBruto Float NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_PesoBruto'

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
									  'CDU_PesoBruto', 
									  'Peso Bruto', 
									  'Peso Bruto', 
									  0, 
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
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_NVolumes'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_NVolumes NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NVolumes'

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
									  'CDU_NVolumes', 
									  'Nº Volumes', 
									  'Nº Volumes', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_NFatura'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_NFatura NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NFatura'

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
									  'CDU_NFatura', 
									  'Nº Fatura', 
									  'Nº Fatura', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_NContentor'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_NContentor NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NContentor'

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
									  'CDU_NContentor', 
									  'Nº Contentor', 
									  'Nº Contentor', 
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END
GO


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

--BEGIN
--	IF NOT EXISTS (
--			SELECT 1
--			FROM INFORMATION_SCHEMA.COLUMNS
--			WHERE TABLE_NAME = 'LinhasDocRascunhos'
--				AND COLUMN_NAME = 'CDU_Vendedor'
--			)
--		BEGIN
--			DECLARE @Var INT
--			ALTER TABLE LinhasDocRascunhos ADD CDU_Vendedor NVARCHAR(100) NULL

--			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Vendedor'

--			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
--			WHERE TABELA = 'LinhasDocRascunhos'
--			INSERT INTO StdCamposVar (Tabela, 
--									  Campo, 
--									  Descricao, 
--									  Texto, 
--									  Visivel, 
--									  Ordem, 
--									  ValorDefeito, 
--									  Query) 
--							  VALUES ('LinhasDocRascunhos', 
--									  'CDU_Vendedor', 
--									  'Vendedor', 
--									  'Vendedor', 
--									  0, 
--									  @Var + 1, 
--									  NULL, 
--									  'SELECT [CDU_Vendedor] FROM [Fornecedores] WITH (NOLOCK)')
--		END
--END
--GO


--BEGIN
--	IF NOT EXISTS (
--			SELECT 1
--			FROM INFORMATION_SCHEMA.COLUMNS
--			WHERE TABLE_NAME = 'LinhasDocRascunhos'
--				AND COLUMN_NAME = 'CDU_ComissaoVendedor'
--			)
--		BEGIN
--			DECLARE @Var INT
--			ALTER TABLE LinhasDocRascunhos ADD CDU_ComissaoVendedor REAL NULL

--			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ComissaoVendedor'

--			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
--			WHERE TABELA = 'LinhasDocRascunhos'
--			INSERT INTO StdCamposVar (Tabela, 
--									  Campo, 
--									  Descricao, 
--									  Texto, 
--									  Visivel, 
--									  Ordem, 
--									  ValorDefeito, 
--									  Query) 
--							  VALUES ('LinhasDocRascunhos', 
--									  'CDU_ComissaoVendedor', 
--									  'Comissão Vendedor', 
--									  'Comissão Vendedor', 
--									  0, 
--									  @Var + 1, 
--									  0, 
--									  NULL)
--		END
--END
--GO

