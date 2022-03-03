/*
-------------------------------------------------------------------------------------------
Data:	    10-04-2019
Autor:	    Andre Sousa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Alteração para adicionar novos campos na tabela LinhasDoc
Coment.:    
-------------------------------------------------------------------------------------------
*/


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Situacao'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Situacao NVARCHAR(3) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Situacao'

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
									  'CDU_Situacao', 
									  'Situação', 
									  'Situação',
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT * FROM [TDU_SituacoesLote] WITH (NOLOCK) ORDER BY [CDU_Situacao]')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Situacao'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Situacao NVARCHAR(3) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Situacao'

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
									  'CDU_Situacao', 
									  'Situação', 
									  'Situação',
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT * FROM [TDU_SituacoesLote] WITH (NOLOCK) ORDER BY [CDU_Situacao]')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Parafinado'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Parafinado BIT NOT NULL DEFAULT 0

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Parafinado'

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
									  'CDU_Parafinado', 
									  'Parafinado', 
									  'Parafinado',
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
				AND COLUMN_NAME = 'CDU_Parafinado'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Parafinado BIT

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Parafinado'

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
									  'CDU_Parafinado', 
									  'Parafinado', 
									  'Parafinado',
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
				AND COLUMN_NAME = 'CDU_TipoQualidade'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_TipoQualidade NVARCHAR(3) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_TipoQualidade'

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
									  'CDU_TipoQualidade', 
									  'TipoQualidade', 
									  'TipoQualidade',
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT * FROM [TDU_TipoQualidade] WITH (NOLOCK) ORDER BY [CDU_TipoQualidade]')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_TipoQualidade'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_TipoQualidade NVARCHAR(3) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_TipoQualidade'

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
									  'CDU_TipoQualidade', 
									  'TipoQualidade', 
									  'TipoQualidade',
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT * FROM [TDU_TipoQualidade] WITH (NOLOCK) ORDER BY [CDU_TipoQualidade]')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_PaisOrigem'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_PaisOrigem NVARCHAR(2) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_PaisOrigem'

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
									  'CDU_PaisOrigem', 
									  'PaisOrigem', 
									  'PaisOrigem',
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT [Pais], [Descricao] FROM [Paises] WITH (NOLOCK) ORDER BY [Pais]')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_PaisOrigem'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_PaisOrigem NVARCHAR(2) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_PaisOrigem'

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
									  'CDU_PaisOrigem', 
									  'PaisOrigem', 
									  'PaisOrigem',
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT [Pais], [Descricao] FROM [Paises] WITH (NOLOCK) ORDER BY [Pais]')
		END
END
GO

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_Seguradora'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Seguradora NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Seguradora'

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
									  'CDU_Seguradora', 
									  'Seguradora', 
									  'Seguradora',
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
				AND COLUMN_NAME = 'CDU_Seguradora'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Seguradora NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Seguradora'

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
									  'CDU_Seguradora', 
									  'Seguradora', 
									  'Seguradora',
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
				AND COLUMN_NAME = 'CDU_NumCertificado'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_NumCertificado NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NumCertificado'

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
									  'CDU_NumCertificado', 
									  'NumCertificado', 
									  'NumCertificado',
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
				AND COLUMN_NAME = 'CDU_NumCertificado'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_NumCertificado NVARCHAR(50) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NumCertificado'

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
									  'CDU_NumCertificado', 
									  'NumCertificado', 
									  'NumCertificado',
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
				AND COLUMN_NAME = 'CDU_Parque'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_Parque NVARCHAR(5) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Parque'

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
									  'CDU_Parque', 
									  'Parque', 
									  'Parque',
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT * FROM [TDU_Parques] WITH (NOLOCK) ORDER BY [CDU_Codigo]')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDocRascunhos'
				AND COLUMN_NAME = 'CDU_Parque'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_Parque NVARCHAR(5) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Parque'

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
									  'CDU_Parque', 
									  'Parque', 
									  'Parque',
									  0, 
									  @Var + 1, 
									  '', 
									  'SELECT * FROM [TDU_Parques] WITH (NOLOCK) ORDER BY [CDU_Codigo]')
		END
END
GO


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'LinhasDoc'
				AND COLUMN_NAME = 'CDU_ObsMdf'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDoc ADD CDU_ObsMdf NVARCHAR(200) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ObsMdf'

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
									  'CDU_ObsMdf', 
									  'ObsMdf', 
									  'ObsMdf',
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
				AND COLUMN_NAME = 'CDU_ObsMdf'
			)
		BEGIN
			DECLARE @Var INT
			ALTER TABLE LinhasDocRascunhos ADD CDU_ObsMdf NVARCHAR(200) NULL

			DELETE StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ObsMdf'

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
									  'CDU_ObsMdf', 
									  'ObsMdf', 
									  'ObsMdf',
									  0, 
									  @Var + 1, 
									  '', 
									  NULL)
		END
END










