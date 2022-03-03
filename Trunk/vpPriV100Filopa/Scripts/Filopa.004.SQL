/*
-------------------------------------------------------------------------------------------
Data:	    14-06-2018
Autor:	    Andre Sousa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Novos campos CDU_ValorFOB , CDU_ConsideraValorFOB
Coment.:    
-------------------------------------------------------------------------------------------
*/

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_ValorFOB'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_ValorFOB REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ValorFOB'

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
									  'CDU_ValorFOB', 
									  'ValorFOB', 
									  'ValorFOB',
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
				AND COLUMN_NAME = 'CDU_ValorFOB'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_ValorFOB REAL NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ValorFOB'

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
									  'CDU_ValorFOB', 
									  'ValorFOB', 
									  'ValorFOB',
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
			WHERE TABLE_NAME = 'Clientes'
				AND COLUMN_NAME = 'CDU_ConsideraValorFOB'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE Clientes ADD CDU_ConsideraValorFOB BIT

			DELETE StdCamposVar WHERE Tabela = 'Clientes' AND Campo = 'CDU_ConsideraValorFOB'

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
									  'CDU_ConsideraValorFOB', 
									  'ConsideraValorFOB', 
									  'ConsideraValorFOB',
									  1, 
									  @Var + 1, 
									  0, 
									  NULL)
		END
END
GO


UPDATE StdCamposVar SET  ValorDefeito = '' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Banco'
UPDATE StdCamposVar SET  ValorDefeito = '' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Banco'


BEGIN 
	IF NOT EXISTS (
		   SELECT 1
		   FROM INFORMATION_SCHEMA.COLUMNS
		   WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_ComissaoConsiderada'
		   )
		BEGIN
			DECLARE @Var INT

			ALTER TABLE LinhasDoc ADD CDU_ComissaoConsiderada BIT NOT NULL DEFAULT 0;
			
			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ComissaoConsiderada'

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
									  'CDU_ComissaoConsiderada', 
									  'ComissãoConsiderada', 
									  'ComissãoConsiderada',
									  0,  
									  @Var + 1, 
									  0,  
									  Null) 
		END
END

GO

BEGIN 
	IF NOT EXISTS (
		   SELECT 1
		   FROM INFORMATION_SCHEMA.COLUMNS
		   WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_ComissaoConsiderada'
		   )
		BEGIN
			DECLARE @Var INT

			ALTER TABLE LinhasDocRascunhos ADD CDU_ComissaoConsiderada BIT NOT NULL DEFAULT 0;
			
			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ComissaoConsiderada'

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
							  VALUES ('LinhasDocRascunhos', 
									  'CDU_ComissaoConsiderada', 
									  'ComissãoConsiderada', 
									  'ComissãoConsiderada',
									  0,  
									  @Var + 1, 
									  0,  
									  Null) 
		END
END

GO

BEGIN 
	IF NOT EXISTS (
		   SELECT 1
		   FROM INFORMATION_SCHEMA.COLUMNS
		   WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_ValorLiquidoOrig'
		   )
		BEGIN
			DECLARE @Var INT

			ALTER TABLE LinhasDoc ADD CDU_ValorLiquidoOrig REAL NOT NULL DEFAULT 0;
			
			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ValorLiquidoOrig'

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
									  'CDU_ValorLiquidoOrig', 
									  'ValorLiquidoOrig', 
									  'ValorLiquidoOrig',
									  0,  
									  @Var + 1, 
									  0,  
									  Null) 
		END
END

GO

BEGIN 
	IF NOT EXISTS (
		   SELECT 1
		   FROM INFORMATION_SCHEMA.COLUMNS
		   WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_ValorLiquidoOrig'
		   )
		BEGIN
			DECLARE @Var INT

			ALTER TABLE LinhasDocRascunhos ADD CDU_ValorLiquidoOrig REAL NOT NULL DEFAULT 0;
			
			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ValorLiquidoOrig'

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
									  'CDU_ValorLiquidoOrig', 
									  'ValorLiquidoOrig', 
									  'ValorLiquidoOrig',
									  0,  
									  @Var + 1, 
									  0,  
									  Null) 
		END
END

GO

BEGIN 
IF NOT EXISTS (
		   SELECT 1
		   FROM INFORMATION_SCHEMA.COLUMNS
		   WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_DataEmissaoCC'
		   )
		BEGIN
			DECLARE @Var INT

			ALTER TABLE CabecDoc ADD CDU_DataEmissaoCC DATETIME NULL;
			
			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DataEmissaoCC'

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
									  'CDU_DataEmissaoCC', 
									  'DataEmissãoCC', 
									  'DataEmissãoCC',
									  0,  
									  @Var + 1, 
									  '',  
									  Null) 
		END
END

GO

BEGIN 
IF NOT EXISTS (
		   SELECT 1
		   FROM INFORMATION_SCHEMA.COLUMNS
		   WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_DataEmissaoCC'
		   )
		BEGIN
			DECLARE @Var INT

			ALTER TABLE CabecDocRascunhos ADD CDU_DataEmissaoCC DATETIME DEFAULT NULL;
			
			DELETE StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DataEmissaoCC'

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
									  'CDU_DataEmissaoCC', 
									  'DataEmissãoCC', 
									  'DataEmissãoCC',
									  0,  
									  @Var + 1, 
									  '',  
									  Null) 
		END
END

GO

BEGIN 
IF NOT EXISTS (
		   SELECT 1
		   FROM INFORMATION_SCHEMA.COLUMNS
		   WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_Vendedor'
		   )
		BEGIN
			DECLARE @Var INT

			ALTER TABLE CabecDoc ADD CDU_Vendedor NVARCHAR(3) NULL;
			
			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Vendedor'

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
									  'CDU_Vendedor', 
									  'Vendedor', 
									  'Vendedor',
									  0,  
									  @Var + 1, 
									  '',  
									  Null) 
		END
END

GO

BEGIN 
IF NOT EXISTS (
		   SELECT 1
		   FROM INFORMATION_SCHEMA.COLUMNS
		   WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_ComissaoVendedor'
		   )
		BEGIN
			DECLARE @Var INT

			ALTER TABLE CabecDoc ADD CDU_ComissaoVendedor Real NULL;
			
			DELETE StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_ComissaoVendedor'

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
									  'CDU_ComissaoVendedor', 
									  'ComissãoVendedor', 
									  'ComissãoVendedor',
									  0,  
									  @Var + 1, 
									  0,  
									  Null) 
		END
END