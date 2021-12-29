/*
-------------------------------------------------------------------------------------------
Data:	  02-09-2021
Autor:	  Rafael Oliveira
Solução:    GrupoMundifios
Objetivos:  Script para criar CDU's necessários relativos ás funcionalidades
Coment.: 
-------------------------------------------------------------------------------------------
*/
    
BEGIN
	IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Condpag' AND COLUMN_NAME = 'CDU_RQ')
	BEGIN
		ALTER TABLE Condpag ADD CDU_RQ BIT NULL 

		DELETE FROM StdCamposVar WHERE Tabela = 'Condpag' AND Campo = 'CDU_RQ'
		
		DECLARE @OrdemMax INT
		SELECT @OrdemMax = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Condpag'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Condpag', 'CDU_RQ', 'Requisicao Quinzenal', 'Requisicao Quinzenal', 1, 
				@OrdemMax + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Condpag ADD CONSTRAINT Condpag_CDU_RQ_DF DEFAULT (0) FOR CDU_RQ;


		--CDUs são sempre sugeridos como NOT NULL.
		--Mas para se criar um DEFAULT, o campo é criado como NULL, é criado o DEFAULT, atualizado e colocado a NOT NULL
		EXEC('UPDATE Condpag SET CDU_RQ = 0')
		ALTER TABLE Condpag ALTER COLUMN CDU_RQ BIT NOT NULL
		

	END


	IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Condpag' AND COLUMN_NAME = 'CDU_RM')
	BEGIN
		ALTER TABLE Condpag ADD CDU_RM BIT NULL 

		DELETE FROM StdCamposVar WHERE Tabela = 'Condpag' AND Campo = 'CDU_RM'
		
		DECLARE @OrdemMax1 INT
		SELECT @OrdemMax1 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Condpag'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Condpag', 'CDU_RM', 'Requisicao Mensal', 'Requisicao Mensal', 1, 
				@OrdemMax1 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Condpag ADD CONSTRAINT Condpag_CDU_RM_DF DEFAULT (0) FOR CDU_RM;


		--CDUs são sempre sugeridos como NOT NULL.
		--Mas para se criar um DEFAULT, o campo é criado como NULL, é criado o DEFAULT, atualizado e colocado a NOT NULL
		EXEC('UPDATE Condpag SET CDU_RM = 0')
		ALTER TABLE Condpag ALTER COLUMN CDU_RM BIT NOT NULL
		

	END




			IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_AlteradaDataVenc')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_AlteradaDataVenc bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_AlteradaDataVenc'
		
		DECLARE @OrdemMax2 INT
		SELECT @OrdemMax2 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_AlteradaDataVenc', 'AlteradaDataVenc', 'AlteradaDataVenc', 1, 
				@OrdemMax2 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDoc ADD CONSTRAINT CabecDoc_CDU_AlteradaDataVenc_DF DEFAULT (0) FOR CDU_AlteradaDataVenc;		

		
		--CDUs são sempre sugeridos como NOT NULL.
		--Mas para se criar um DEFAULT, o campo é criado como NULL, é criado o DEFAULT, atualizado e colocado a NOT NULL
		EXEC('UPDATE CabecDoc SET CDU_AlteradaDataVenc = 0')
		ALTER TABLE CabecDoc ALTER COLUMN CDU_AlteradaDataVenc BIT NOT NULL

	END


				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Clientes' AND COLUMN_NAME = 'CDU_ObsEncomenda')
	BEGIN
		ALTER TABLE Clientes ADD CDU_ObsEncomenda nvarchar(4000) null

		DELETE FROM StdCamposVar WHERE Tabela = 'Clientes' AND Campo = 'CDU_ObsEncomenda'
		
		DECLARE @OrdemMax3 INT
		SELECT @OrdemMax3 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Clientes'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Clientes', 'CDU_ObsEncomenda', 'Obs Encomenda', 'Obs Encomenda', 1, 
				@OrdemMax3 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Clientes ADD CONSTRAINT Clientes_CDU_ObsEncomenda_DF DEFAULT (0) FOR CDU_ObsEncomenda;		

		

	END


					IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_DespDAU')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_DespDAU varchar(20) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_DespDAU'
		
		DECLARE @OrdemMax4 INT
		SELECT @OrdemMax4 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_DespDAU', 'A - Nº DAU', 'A - Nº DAU', 1, 
				@OrdemMax4 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_DespDAU_DF DEFAULT (0) FOR CDU_DespDAU;		



	END



						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_Regime')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_Regime varchar(10) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_Regime'
		
		DECLARE @OrdemMax5 INT
		SELECT @OrdemMax5 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_Regime', '37 - Regime', '37 - Regime', 1, 
				@OrdemMax5 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_Regime_DF DEFAULT (0) FOR CDU_Regime;		



	END

	
						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_CodMerc')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_CodMerc nvarchar(10) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_CodMerc'
		
		DECLARE @OrdemMax6 INT
		SELECT @OrdemMax6 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_CodMerc', '33 - Código Mercadorias', '33 - Código Mercadorias', 1, 
				@OrdemMax6 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_CodMerc_DF DEFAULT (0) FOR CDU_CodMerc;		



	END


							IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_Contramarca')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_Contramarca varchar(30) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_Contramarca'
		
		DECLARE @OrdemMax7 INT
		SELECT @OrdemMax7 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_Contramarca', '40 - Contramarca', '40 - Contramarca', 1, 
				@OrdemMax7 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_Contramarca_DF DEFAULT (0) FOR CDU_Contramarca;		



	END


								IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_ContramarcaData')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_ContramarcaData datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_ContramarcaData'
		
		DECLARE @OrdemMax8 INT
		SELECT @OrdemMax8 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_ContramarcaData', '40 - Contramarca Data', '40 - Contramarca Data', 1, 
				@OrdemMax8 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_ContramarcaData_DF DEFAULT (0) FOR CDU_ContramarcaData;		



	END


	--								IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasSTK' AND COLUMN_NAME = 'CDU_DespTipoImportacao')
	--BEGIN
	--	ALTER TABLE LinhasSTK ADD CDU_DespTipoImportacao varchar(10) null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasSTK' AND Campo = 'CDU_DespTipoImportacao'
		
	--	DECLARE @OrdemMax9 INT
	--	SELECT @OrdemMax9 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasSTK'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasSTK', 'CDU_DespTipoImportacao', 'TipoImportacao', 'TipoImportacao', 1, 
	--			@OrdemMax9 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasSTK ADD CONSTRAINT LinhasSTK_CDU_DespTipoImportacao_DF DEFAULT (0) FOR CDU_DespTipoImportacao;		



	--END


	--									IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasSTK' AND COLUMN_NAME = 'CDU_Volumes')
	--BEGIN
	--	ALTER TABLE LinhasSTK ADD CDU_Volumes nvarchar(100) null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasSTK' AND Campo = 'CDU_Volumes'
		
	--	DECLARE @OrdemMax10 INT
	--	SELECT @OrdemMax10 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasSTK'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasSTK', 'CDU_Volumes', '31 - Volumes', '31 - Volumes', 1, 
	--			@OrdemMax10 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasSTK ADD CONSTRAINT LinhasSTK_CDU_Volumes_DF DEFAULT (0) FOR CDU_Volumes;		



	--END


	
	--									IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasSTK' AND COLUMN_NAME = 'CDU_MassaBruta')
	--BEGIN
	--	ALTER TABLE LinhasSTK ADD CDU_MassaBruta float null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasSTK' AND Campo = 'CDU_MassaBruta'
		
	--	DECLARE @OrdemMax11 INT
	--	SELECT @OrdemMax11 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasSTK'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasSTK', 'CDU_MassaBruta', 'Massa Bruta', 'Massa Bruta', 1, 
	--			@OrdemMax11 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasSTK ADD CONSTRAINT LinhasSTK_CDU_MassaBruta_DF DEFAULT (0) FOR CDU_MassaBruta;		



	--END


	--									IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasSTK' AND COLUMN_NAME = 'CDU_MassaLiq')
	--BEGIN
	--	ALTER TABLE LinhasSTK ADD CDU_MassaLiq float null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasSTK' AND Campo = 'CDU_MassaLiq'
		
	--	DECLARE @OrdemMax12 INT
	--	SELECT @OrdemMax12 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasSTK'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasSTK', 'CDU_MassaLiq', 'Massa Liquida', 'Massa Liquida', 1, 
	--			@OrdemMax12 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasSTK ADD CONSTRAINT LinhasSTK_CDU_MassaLiq_DF DEFAULT (0) FOR CDU_MassaLiq;		



	--END


	
	--									IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasSTK' AND COLUMN_NAME = 'CDU_ValorAduaneiro')
	--BEGIN
	--	ALTER TABLE LinhasSTK ADD CDU_ValorAduaneiro float null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasSTK' AND Campo = 'CDU_ValorAduaneiro'
		
	--	DECLARE @OrdemMax13 INT
	--	SELECT @OrdemMax13 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasSTK'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasSTK', 'CDU_ValorAduaneiro', 'Valor Aduaneiro', 'Valor Aduaneiro', 1, 
	--			@OrdemMax13 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasSTK ADD CONSTRAINT LinhasSTK_CDU_ValorAduaneiro_DF DEFAULT (0) FOR CDU_ValorAduaneiro;		



	--END


	--										IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasSTK' AND COLUMN_NAME = 'CDU_IvaDAU')
	--BEGIN
	--	ALTER TABLE LinhasSTK ADD CDU_IvaDAU float null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasSTK' AND Campo = 'CDU_IvaDAU'
		
	--	DECLARE @OrdemMax14 INT
	--	SELECT @OrdemMax14 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasSTK'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasSTK', 'CDU_IvaDAU', 'IVA DAU', 'IVA DAU', 1, 
	--			@OrdemMax14 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasSTK ADD CONSTRAINT LinhasSTK_CDU_IvaDAU_DF DEFAULT (0) FOR CDU_IvaDAU;		



	--END


	--											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasSTK' AND COLUMN_NAME = 'CDU_DireitosDAU')
	--BEGIN
	--	ALTER TABLE LinhasSTK ADD CDU_DireitosDAU float null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasSTK' AND Campo = 'CDU_DireitosDAU'
		
	--	DECLARE @OrdemMax15 INT
	--	SELECT @OrdemMax15 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasSTK'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasSTK', 'CDU_DireitosDAU', 'Direitos DAU', 'Direitos DAU', 1, 
	--			@OrdemMax15 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasSTK ADD CONSTRAINT LinhasSTK_CDU_DireitosDAU_DF DEFAULT (0) FOR CDU_DireitosDAU;		



	--END




													IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Clientes' AND COLUMN_NAME = 'CDU_EntidadeInterna')
	BEGIN
		ALTER TABLE Clientes ADD CDU_EntidadeInterna nvarchar(12) null

		DELETE FROM StdCamposVar WHERE Tabela = 'Clientes' AND Campo = 'CDU_EntidadeInterna'
		
		DECLARE @OrdemMax16 INT
		SELECT @OrdemMax16 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Clientes'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Clientes', 'CDU_EntidadeInterna', 'Entidade Interna', 'Entidade Interna', 1, 
				@OrdemMax16 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Clientes ADD CONSTRAINT Clientes_CDU_EntidadeInterna_DF DEFAULT (0) FOR CDU_EntidadeInterna;		



	END


														IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_EntidadeInterna')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_EntidadeInterna nvarchar(12) null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_EntidadeInterna'
		
		DECLARE @OrdemMax17 INT
		SELECT @OrdemMax17 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_EntidadeInterna', 'Entidade Interna', 'Entidade Interna', 1, 
				@OrdemMax17 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_EntidadeInterna_DF DEFAULT (0) FOR CDU_EntidadeInterna;		



	END


	--						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasStk' AND COLUMN_NAME = 'CDU_DespDAU')
	--BEGIN
	--	ALTER TABLE LinhasStk ADD CDU_DespDAU varchar(20) null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasStk' AND Campo = 'CDU_DespDAU'
		
	--	DECLARE @OrdemMax18 INT
	--	SELECT @OrdemMax18 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasStk'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasStk', 'CDU_DespDAU', 'A - Nº DAU', 'A - Nº DAU', 1, 
	--			@OrdemMax18 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasStk ADD CONSTRAINT LinhasStk_CDU_DespDAU_DF DEFAULT (0) FOR CDU_DespDAU;		



	--END



	--						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasStk' AND COLUMN_NAME = 'CDU_CodMerc')
	--BEGIN
	--	ALTER TABLE LinhasStk ADD CDU_CodMerc nvarchar(10) null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasStk' AND Campo = 'CDU_CodMerc'
		
	--	DECLARE @OrdemMax19 INT
	--	SELECT @OrdemMax19 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasStk'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasStk', 'CDU_CodMerc', '33 - Código Mercadorias', '33 - Código Mercadorias', 1, 
	--			@OrdemMax19 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasStk ADD CONSTRAINT LinhasStk_CDU_CodMerc_DF DEFAULT (0) FOR CDU_CodMerc;		



	--END


	
	--					IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasStk' AND COLUMN_NAME = 'CDU_Regime')
	--BEGIN
	--	ALTER TABLE LinhasStk ADD CDU_Regime varchar(10) null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasStk' AND Campo = 'CDU_Regime'
		
	--	DECLARE @OrdemMax20 INT
	--	SELECT @OrdemMax20 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasStk'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasStk', 'CDU_Regime', '37 - Regime', '37 - Regime', 1, 
	--			@OrdemMax20 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasStk ADD CONSTRAINT LinhasStk_CDU_Regime_DF DEFAULT (0) FOR CDU_Regime;		



	--END



	--							IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasStk' AND COLUMN_NAME = 'CDU_Contramarca')
	--BEGIN
	--	ALTER TABLE LinhasStk ADD CDU_Contramarca varchar(30) null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasStk' AND Campo = 'CDU_Contramarca'
		
	--	DECLARE @OrdemMax21 INT
	--	SELECT @OrdemMax21 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasStk'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasStk', 'CDU_Contramarca', '40 - Contramarca', '40 - Contramarca', 1, 
	--			@OrdemMax21 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasStk ADD CONSTRAINT LinhasStk_CDU_Contramarca_DF DEFAULT (0) FOR CDU_Contramarca;		



	--END


	--							IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasStk' AND COLUMN_NAME = 'CDU_ContramarcaData')
	--BEGIN
	--	ALTER TABLE LinhasStk ADD CDU_ContramarcaData datetime null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'LinhasStk' AND Campo = 'CDU_ContramarcaData'
		
	--	DECLARE @OrdemMax22 INT
	--	SELECT @OrdemMax22 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'LinhasStk'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('LinhasStk', 'CDU_ContramarcaData', '40 - Contramarca Data', '40 - Contramarca Data', 1, 
	--			@OrdemMax22 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE LinhasStk ADD CONSTRAINT LinhasStk_CDU_ContramarcaData_DF DEFAULT (0) FOR CDU_ContramarcaData;		



	--END


						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_DespDAU')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_DespDAU varchar(20) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_DespDAU'
		
		DECLARE @OrdemMax23 INT
		SELECT @OrdemMax23 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_DespDAU', 'A - Nº DAU', 'A - Nº DAU', 1, 
				@OrdemMax23 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_DespDAU_DF DEFAULT (0) FOR CDU_DespDAU;		



	END


	
						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_Regime')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_Regime varchar(10) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Regime'
		
		DECLARE @OrdemMax24 INT
		SELECT @OrdemMax24 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_Regime', '37 - Regime', '37 - Regime', 1, 
				@OrdemMax24 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_Regime_DF DEFAULT (0) FOR CDU_Regime;		



	END


							IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Artigo' AND COLUMN_NAME = 'CDU_DescricaoExtra')
	BEGIN
		ALTER TABLE Artigo ADD CDU_DescricaoExtra nvarchar(200) null

		DELETE FROM StdCamposVar WHERE Tabela = 'Artigo' AND Campo = 'CDU_DescricaoExtra'
		
		DECLARE @OrdemMax25 INT
		SELECT @OrdemMax25 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Artigo'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Artigo', 'CDU_DescricaoExtra', 'DescricaoExtra', 'DescricaoExtra', 1, 
				@OrdemMax25 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Artigo ADD CONSTRAINT Artigo_CDU_DescricaoExtra_DF DEFAULT (0) FOR CDU_DescricaoExtra;		



	END


								IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Artigo' AND COLUMN_NAME = 'CDU_DataCriacao')
	BEGIN
		ALTER TABLE Artigo ADD CDU_DataCriacao datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'Artigo' AND Campo = 'CDU_DataCriacao'
		
		DECLARE @OrdemMax26 INT
		SELECT @OrdemMax26 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Artigo'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Artigo', 'CDU_DataCriacao', 'DataCriacao', 'DataCriacao', 1, 
				@OrdemMax26 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Artigo ADD CONSTRAINT Artigo_CDU_DataCriacao_DF DEFAULT (0) FOR CDU_DataCriacao;		



	END


									IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Artigo' AND COLUMN_NAME = 'CDU_DescricaoInterna')
	BEGIN
		ALTER TABLE Artigo ADD CDU_DescricaoInterna datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'Artigo' AND Campo = 'CDU_DescricaoInterna'
		
		DECLARE @OrdemMax27 INT
		SELECT @OrdemMax27 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Artigo'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Artigo', 'CDU_DescricaoInterna', 'DescricaoInterna', 'DescricaoInterna', 1, 
				@OrdemMax27 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Artigo ADD CONSTRAINT Artigo_CDU_DescricaoInterna_DF DEFAULT (0) FOR CDU_DescricaoInterna;		



	END


										IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_TipoDocRastreabilidade')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_TipoDocRastreabilidade nvarchar(5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_TipoDocRastreabilidade'
		
		DECLARE @OrdemMax28 INT
		SELECT @OrdemMax28 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_TipoDocRastreabilidade', 'TipoDoc.Rastreab', 'TipoDoc.Rastreab', 1, 
				@OrdemMax28 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDoc ADD CONSTRAINT CabecDoc_CDU_TipoDocRastreabilidade_DF DEFAULT (0) FOR CDU_TipoDocRastreabilidade;		



	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_TipoDocRastreabilidade')
	BEGIN
		ALTER TABLE CabecDocRascunhos ADD CDU_TipoDocRastreabilidade nvarchar(5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_TipoDocRastreabilidade'
		
		DECLARE @OrdemMax29 INT
		SELECT @OrdemMax29 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDocRascunhos', 'CDU_TipoDocRastreabilidade', 'TipoDoc.Rastreab', 'TipoDoc.Rastreab', 1, 
				@OrdemMax29 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDocRascunhos ADD CONSTRAINT CabecDocRascunhos_CDU_TipoDocRastreabilidade_DF DEFAULT (0) FOR CDU_TipoDocRastreabilidade;		



	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_NumDocRastreabilidade')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_NumDocRastreabilidade int null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_NumDocRastreabilidade'
		
		DECLARE @OrdemMax30 INT
		SELECT @OrdemMax30 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_NumDocRastreabilidade', 'NumDoc.Rastreab', 'NumDoc.Rastreab', 1, 
				@OrdemMax30 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDoc ADD CONSTRAINT CabecDoc_CDU_NumDocRastreabilidade_DF DEFAULT (0) FOR CDU_NumDocRastreabilidade;		

			--CDUs são sempre sugeridos como NOT NULL.
		--Mas para se criar um DEFAULT, o campo é criado como NULL, é criado o DEFAULT, atualizado e colocado a NOT NULL
		EXEC('UPDATE CabecDoc SET CDU_NumDocRastreabilidade = 0')
		ALTER TABLE CabecDoc ALTER COLUMN CDU_NumDocRastreabilidade int NOT NULL

	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_NumDocRastreabilidade')
	BEGIN
		ALTER TABLE CabecDocRascunhos ADD CDU_NumDocRastreabilidade int null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_NumDocRastreabilidade'
		
		DECLARE @OrdemMax31 INT
		SELECT @OrdemMax31 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDocRascunhos', 'CDU_NumDocRastreabilidade', 'NumDoc.Rastreab', 'NumDoc.Rastreab', 1, 
				@OrdemMax31 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDocRascunhos ADD CONSTRAINT CabecDocRascunhos_CDU_NumDocRastreabilidade_DF DEFAULT (0) FOR CDU_NumDocRastreabilidade;		

					--CDUs são sempre sugeridos como NOT NULL.
		--Mas para se criar um DEFAULT, o campo é criado como NULL, é criado o DEFAULT, atualizado e colocado a NOT NULL
		EXEC('UPDATE CabecDocRascunhos SET CDU_NumDocRastreabilidade = 0')
		ALTER TABLE CabecDocRascunhos ALTER COLUMN CDU_NumDocRastreabilidade int NOT NULL

	END



											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_SerieDocRastreabilidade')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_SerieDocRastreabilidade nvarchar(5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_SerieDocRastreabilidade'
		
		DECLARE @OrdemMax32 INT
		SELECT @OrdemMax32 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_SerieDocRastreabilidade', 'SerieDocRastreab', 'SerieDocRastreab', 1, 
				@OrdemMax32 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDoc ADD CONSTRAINT CabecDoc_CDU_SerieDocRastreabilidade_DF DEFAULT (0) FOR CDU_SerieDocRastreabilidade;		



	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_SerieDocRastreabilidade')
	BEGIN
		ALTER TABLE CabecDocRascunhos ADD CDU_SerieDocRastreabilidade nvarchar(5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_SerieDocRastreabilidade'
		
		DECLARE @OrdemMax33 INT
		SELECT @OrdemMax33 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDocRascunhos', 'CDU_SerieDocRastreabilidade', 'SerieDocRastreab', 'SerieDocRastreab', 1, 
				@OrdemMax33 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDocRascunhos ADD CONSTRAINT CabecDocRascunhos_CDU_SerieDocRastreabilidade_DF DEFAULT (0) FOR CDU_SerieDocRastreabilidade;		



	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_DocumentoCompraDestino')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_DocumentoCompraDestino nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DocumentoCompraDestino'
		
		DECLARE @OrdemMax34 INT
		SELECT @OrdemMax34 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_DocumentoCompraDestino', 'DocumentoCompraDestino', 'DocumentoCompraDestino', 1, 
				@OrdemMax34 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDoc ADD CONSTRAINT CabecDoc_CDU_DocumentoCompraDestino_DF DEFAULT (0) FOR CDU_DocumentoCompraDestino;		



	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_DocumentoCompraDestino')
	BEGIN
		ALTER TABLE CabecDocRascunhos ADD CDU_DocumentoCompraDestino nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DocumentoCompraDestino'
		
		DECLARE @OrdemMax35 INT
		SELECT @OrdemMax35 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDocRascunhos', 'CDU_DocumentoCompraDestino', 'DocumentoCompraDestino', 'DocumentoCompraDestino', 1, 
				@OrdemMax35 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDocRascunhos ADD CONSTRAINT CabecDocRascunhos_CDU_DocumentoCompraDestino_DF DEFAULT (0) FOR CDU_DocumentoCompraDestino;		



	END



												IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_DocumentoVendaDestino')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_DocumentoVendaDestino nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DocumentoVendaDestino'
		
		DECLARE @OrdemMax36 INT
		SELECT @OrdemMax36 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_DocumentoVendaDestino', 'DocumentoVendaDestino', 'DocumentoVendaDestino', 1, 
				@OrdemMax36 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDoc ADD CONSTRAINT CabecDoc_CDU_DocumentoVendaDestino_DF DEFAULT (0) FOR CDU_DocumentoVendaDestino;		



	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_DocumentoVendaDestino')
	BEGIN
		ALTER TABLE CabecDocRascunhos ADD CDU_DocumentoVendaDestino nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DocumentoVendaDestino'
		
		DECLARE @OrdemMax37 INT
		SELECT @OrdemMax37 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDocRascunhos', 'CDU_DocumentoVendaDestino', 'DocumentoVendaDestino', 'DocumentoVendaDestino', 1, 
				@OrdemMax37 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDocRascunhos ADD CONSTRAINT CabecDocRascunhos_CDU_DocumentoVendaDestino_DF DEFAULT (0) FOR CDU_DocumentoVendaDestino;		



	END


													IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_DocumentoOrigem')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_DocumentoOrigem nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_DocumentoOrigem'
		
		DECLARE @OrdemMax38 INT
		SELECT @OrdemMax38 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_DocumentoOrigem', 'DocumentoOrigem', 'DocumentoOrigem', 1, 
				@OrdemMax38 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDoc ADD CONSTRAINT CabecDoc_CDU_DocumentoOrigem_DF DEFAULT (0) FOR CDU_DocumentoOrigem;		



	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_DocumentoOrigem')
	BEGIN
		ALTER TABLE CabecDocRascunhos ADD CDU_DocumentoOrigem nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_DocumentoOrigem'
		
		DECLARE @OrdemMax39 INT
		SELECT @OrdemMax39 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDocRascunhos', 'CDU_DocumentoOrigem', 'DocumentoOrigem', 'DocumentoOrigem', 1, 
				@OrdemMax39 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDocRascunhos ADD CONSTRAINT CabecDocRascunhos_CDU_DocumentoOrigem_DF DEFAULT (0) FOR CDU_DocumentoOrigem;		



	END


														IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_IDLinhaOriginalGrupo')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_IDLinhaOriginalGrupo nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_IDLinhaOriginalGrupo'
		
		DECLARE @OrdemMax40 INT
		SELECT @OrdemMax40 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_IDLinhaOriginalGrupo', 'IdLinhaOrig.Grupo', 'IdLinhaOrig.Grupo', 1, 
				@OrdemMax40 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_IDLinhaOriginalGrupo_DF DEFAULT (0) FOR CDU_IDLinhaOriginalGrupo;		



	END


															IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_IDLinhaOriginalGrupo')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_IDLinhaOriginalGrupo nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_IDLinhaOriginalGrupo'
		
		DECLARE @OrdemMax41 INT
		SELECT @OrdemMax41 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_IDLinhaOriginalGrupo', 'IdLinhaOrig.Grupo', 'IdLinhaOrig.Grupo', 1, 
				@OrdemMax41 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_IDLinhaOriginalGrupo_DF DEFAULT (0) FOR CDU_IDLinhaOriginalGrupo;		



	END


																IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_Gots')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_Gots bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_Gots'
		
		DECLARE @OrdemMax42 INT
		SELECT @OrdemMax42 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_Gots', 'Gots', 'Gots', 1, 
				@OrdemMax42 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_Gots_DF DEFAULT (0) FOR CDU_Gots;		



	END


																IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_GotsData')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_GotsData datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_GotsData'
		
		DECLARE @OrdemMax43 INT
		SELECT @OrdemMax43 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_GotsData', 'GotsData', 'GotsData', 1, 
				@OrdemMax43 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_GotsData_DF DEFAULT (0) FOR CDU_GotsData;		



	END


																	IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_Grs')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_Grs bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_Grs'
		
		DECLARE @OrdemMax44 INT
		SELECT @OrdemMax44 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_Grs', 'Grs', 'Grs', 1, 
				@OrdemMax44 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_Grs_DF DEFAULT (0) FOR CDU_Grs;		



	END


																		IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_GrsData')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_GrsData datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_GrsData'
		
		DECLARE @OrdemMax45 INT
		SELECT @OrdemMax45 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_GrsData', 'GrsData', 'GrsData', 1, 
				@OrdemMax45 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_GrsData_DF DEFAULT (0) FOR CDU_GrsData;		



	END

																			IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_Ocs')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_Ocs bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_Ocs'
		
		DECLARE @OrdemMax46 INT
		SELECT @OrdemMax46 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_Ocs', 'Ocs', 'Ocs', 1, 
				@OrdemMax46 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_Ocs_DF DEFAULT (0) FOR CDU_Ocs;		



	END



																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_OcsData')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_OcsData datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_OcsData'
		
		DECLARE @OrdemMax47 INT
		SELECT @OrdemMax47 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_OcsData', 'OcsData', 'OcsData', 1, 
				@OrdemMax47 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_OcsData_DF DEFAULT (0) FOR CDU_OcsData;		



	END


																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_Bci')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_Bci bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_Bci'
		
		DECLARE @OrdemMax48 INT
		SELECT @OrdemMax48 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_Bci', 'Bci', 'Bci', 1, 
				@OrdemMax48 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_Bci_DF DEFAULT (0) FOR CDU_Bci;		



	END


																					IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_BciData')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_BciData datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_BciData'
		
		DECLARE @OrdemMax49 INT
		SELECT @OrdemMax49 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_BciData', 'BciData', 'BciData', 1, 
				@OrdemMax49 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_BciData_DF DEFAULT (0) FOR CDU_BciData;		



	END


	
																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_Fsc')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_Fsc bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_Fsc'
		
		DECLARE @OrdemMax50 INT
		SELECT @OrdemMax50 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_Fsc', 'Fsc', 'Fsc', 1, 
				@OrdemMax50 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_Fsc_DF DEFAULT (0) FOR CDU_Fsc;		



	END


	
																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Fornecedores' AND COLUMN_NAME = 'CDU_FscData')
	BEGIN
		ALTER TABLE Fornecedores ADD CDU_FscData datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_FscData'
		
		DECLARE @OrdemMax51 INT
		SELECT @OrdemMax51 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Fornecedores'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Fornecedores', 'CDU_FscData', 'FscData', 'FscData', 1, 
				@OrdemMax51 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Fornecedores ADD CONSTRAINT Fornecedores_CDU_FscData_DF DEFAULT (0) FOR CDU_FscData;		



	END


		
																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_NumCertificadoTrans')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_NumCertificadoTrans nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_NumCertificadoTrans'
		
		DECLARE @OrdemMax52 INT
		SELECT @OrdemMax52 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_NumCertificadoTrans', 'Num.Cert.Transacao', 'Num.Cert.Transacao', 1, 
				@OrdemMax52 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_NumCertificadoTrans_DF DEFAULT (0) FOR CDU_NumCertificadoTrans;		



	END

	
		
																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_DataCertificadoTrans')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_DataCertificadoTrans datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_DataCertificadoTrans'
		
		DECLARE @OrdemMax53 INT
		SELECT @OrdemMax53 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_DataCertificadoTrans', 'Dt.Cert.Transacao', 'Dt.Cert.Transacao', 1, 
				@OrdemMax53 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_DataCertificadoTrans_DF DEFAULT (0) FOR CDU_DataCertificadoTrans;		



	END


														IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_ProgramLabels')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_ProgramLabels int null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_ProgramLabels'
		
		DECLARE @OrdemMax54 INT
		SELECT @OrdemMax54 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_ProgramLabels', 'Cert.Prog.Label', 'Cert.Prog.Label', 1, 
				@OrdemMax54 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_ProgramLabels_DF DEFAULT (0) FOR CDU_ProgramLabels;		



	END


																					IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_Bci')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_Bci bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_Bci'
		
		DECLARE @OrdemMax55 INT
		SELECT @OrdemMax55 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_Bci', 'Bci', 'Bci', 1, 
				@OrdemMax55 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_Bci_DF DEFAULT (0) FOR CDU_Bci;		



	END


	--													IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'TDU_CertificadosLabels' AND COLUMN_NAME = 'CDU_Id')
	--BEGIN
	--	ALTER TABLE TDU_CertificadosLabels ADD CDU_Id int null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'TDU_CertificadosLabels' AND Campo = 'CDU_Id'
		
	--	DECLARE @OrdemMax56 INT
	--	SELECT @OrdemMax56 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'TDU_CertificadosLabels'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('TDU_CertificadosLabels', 'CDU_Id', 'Id', 'Id', 1, 
	--			@OrdemMax56 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE TDU_CertificadosLabels ADD CONSTRAINT TDU_CertificadosLabels_CDU_Id_DF DEFAULT (0) FOR CDU_Id;		

	--			--CDUs são sempre sugeridos como NOT NULL.
	--	--Mas para se criar um DEFAULT, o campo é criado como NULL, é criado o DEFAULT, atualizado e colocado a NOT NULL
	--	EXEC('UPDATE TDU_CertificadosLabels SET CDU_Id = 0')
	--	ALTER TABLE TDU_CertificadosLabels ALTER COLUMN CDU_Id int NOT NULL

	--END


	--														IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'TDU_CertificadosLabels' AND COLUMN_NAME = 'CDU_Program')
	--BEGIN
	--	ALTER TABLE TDU_CertificadosLabels ADD CDU_Program nvarchar(50) null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'TDU_CertificadosLabels' AND Campo = 'CDU_Program'
		
	--	DECLARE @OrdemMax57 INT
	--	SELECT @OrdemMax57 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'TDU_CertificadosLabels'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('TDU_CertificadosLabels', 'CDU_Program', 'Program', 'Program', 1, 
	--			@OrdemMax57 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE TDU_CertificadosLabels ADD CONSTRAINT TDU_CertificadosLabels_CDU_Program_DF DEFAULT (0) FOR CDU_Program;		

	--END


	--															IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'TDU_CertificadosLabels' AND COLUMN_NAME = 'CDU_Label')
	--BEGIN
	--	ALTER TABLE TDU_CertificadosLabels ADD CDU_Label nvarchar(50) null

	--	DELETE FROM StdCamposVar WHERE Tabela = 'TDU_CertificadosLabels' AND Campo = 'CDU_Label'
		
	--	DECLARE @OrdemMax58 INT
	--	SELECT @OrdemMax58 = COALESCE(MAX(Ordem), 0)
	--	FROM StdCamposVar WITH(NOLOCK)
	--	WHERE TABELA = 'TDU_CertificadosLabels'

	--	INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
	--	VALUES ('TDU_CertificadosLabels', 'CDU_Label', 'Label', 'Label', 1, 
	--			@OrdemMax58 + 1,'0','')
				
	--	--Criar DEFAULT (se aplicável...) 
 --       ALTER TABLE TDU_CertificadosLabels ADD CONSTRAINT TDU_CertificadosLabels_CDU_Label_DF DEFAULT (0) FOR CDU_Label;		

	--END



																	IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_EmitirCertificado')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_EmitirCertificado bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_EmitirCertificado'
		
		DECLARE @OrdemMax59 INT
		SELECT @OrdemMax59 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_EmitirCertificado', 'EmitirCertificado', 'EmitirCertificado', 1, 
				@OrdemMax59 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_EmitirCertificado_DF DEFAULT (0) FOR CDU_EmitirCertificado;		

	END


	
																	IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_EmitirCertificado')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_EmitirCertificado bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_EmitirCertificado'
		
		DECLARE @OrdemMax60 INT
		SELECT @OrdemMax60 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_EmitirCertificado', 'EmitirCertificado', 'EmitirCertificado', 1, 
				@OrdemMax60 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_EmitirCertificado_DF DEFAULT (0) FOR CDU_EmitirCertificado;		

	END


																		IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Artigo' AND COLUMN_NAME = 'CDU_DescricaoExtraExterna')
	BEGIN
		ALTER TABLE Artigo ADD CDU_DescricaoExtraExterna nvarchar(200) null

		DELETE FROM StdCamposVar WHERE Tabela = 'Artigo' AND Campo = 'CDU_DescricaoExtraExterna'
		
		DECLARE @OrdemMax61 INT
		SELECT @OrdemMax61 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Artigo'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Artigo', 'CDU_DescricaoExtraExterna', 'DescricaoExtraExterna', 'DescricaoExtraExterna', 1, 
				@OrdemMax61 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Artigo ADD CONSTRAINT Artigo_CDU_DescricaoExtraExterna_DF DEFAULT (0) FOR CDU_DescricaoExtraExterna;		

	END



									IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_NumCertificadoTrans')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_NumCertificadoTrans nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NumCertificadoTrans'
		
		DECLARE @OrdemMax62 INT
		SELECT @OrdemMax62 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_NumCertificadoTrans', 'Num.Cert.Transacao', 'Num.Cert.Transacao', 1, 
				@OrdemMax62 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_NumCertificadoTrans_DF DEFAULT (0) FOR CDU_NumCertificadoTrans;		



	END

										IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_NumCertificadoTrans')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_NumCertificadoTrans nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NumCertificadoTrans'
		
		DECLARE @OrdemMax63 INT
		SELECT @OrdemMax63 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_NumCertificadoTrans', 'Num.Cert.Transacao', 'Num.Cert.Transacao', 1, 
				@OrdemMax63 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_NumCertificadoTrans_DF DEFAULT (0) FOR CDU_NumCertificadoTrans;		



	END



										IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_NumCertificadoTrans2')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_NumCertificadoTrans2 nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NumCertificadoTrans2'
		
		DECLARE @OrdemMax64 INT
		SELECT @OrdemMax64 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_NumCertificadoTrans2', 'Num.Cert.Transacao2', 'Num.Cert.Transacao2', 1, 
				@OrdemMax64 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_NumCertificadoTrans2_DF DEFAULT (0) FOR CDU_NumCertificadoTrans2;		



	END


											IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_NumCertificadoTrans2')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_NumCertificadoTrans2 nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NumCertificadoTrans2'
		
		DECLARE @OrdemMax65 INT
		SELECT @OrdemMax65 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_NumCertificadoTrans2', 'Num.Cert.Transacao2', 'Num.Cert.Transacao2', 1, 
				@OrdemMax65 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_NumCertificadoTrans2_DF DEFAULT (0) FOR CDU_NumCertificadoTrans2;		



	END



												IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_NumCertificadoTrans3')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_NumCertificadoTrans3 nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_NumCertificadoTrans3'
		
		DECLARE @OrdemMax66 INT
		SELECT @OrdemMax66 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_NumCertificadoTrans3', 'Num.Cert.Transacao2', 'Num.Cert.Transacao2', 1, 
				@OrdemMax66 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_NumCertificadoTrans3_DF DEFAULT (0) FOR CDU_NumCertificadoTrans3;		



	END



	
												IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_NumCertificadoTrans3')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_NumCertificadoTrans3 nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_NumCertificadoTrans3'
		
		DECLARE @OrdemMax67 INT
		SELECT @OrdemMax67 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_NumCertificadoTrans3', 'Num.Cert.Transacao3', 'Num.Cert.Transacao3', 1, 
				@OrdemMax67 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_NumCertificadoTrans3_DF DEFAULT (0) FOR CDU_NumCertificadoTrans3;		



	END


													IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_QtdCertificadoTrans')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_QtdCertificadoTrans decimal(18,5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_QtdCertificadoTrans'
		
		DECLARE @OrdemMax68 INT
		SELECT @OrdemMax68 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_QtdCertificadoTrans', 'QtdCertTransacao', 'QtdCertTransacao', 1, 
				@OrdemMax68 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_QtdCertificadoTrans_DF DEFAULT (0) FOR CDU_QtdCertificadoTrans;		



	END


													IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_QtdCertificadoTrans')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_QtdCertificadoTrans decimal(18,5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_QtdCertificadoTrans'
		
		DECLARE @OrdemMax69 INT
		SELECT @OrdemMax69 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_QtdCertificadoTrans', 'QtdCertTransacao', 'QtdCertTransacao', 1, 
				@OrdemMax69 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_QtdCertificadoTrans_DF DEFAULT (0) FOR CDU_QtdCertificadoTrans;		



	END


														IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_QtdCertificadoTrans2')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_QtdCertificadoTrans2 decimal(18,5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_QtdCertificadoTrans2'
		
		DECLARE @OrdemMax70 INT
		SELECT @OrdemMax70 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_QtdCertificadoTrans2', 'QtdCertTransacao2', 'QtdCertTransacao2', 1, 
				@OrdemMax70 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_QtdCertificadoTrans2_DF DEFAULT (0) FOR CDU_QtdCertificadoTrans2;		



	END



															IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_QtdCertificadoTrans2')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_QtdCertificadoTrans2 decimal(18,5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_QtdCertificadoTrans2'
		
		DECLARE @OrdemMax71 INT
		SELECT @OrdemMax71 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_QtdCertificadoTrans2', 'QtdCertTransacao2', 'QtdCertTransacao2', 1, 
				@OrdemMax71 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_QtdCertificadoTrans2_DF DEFAULT (0) FOR CDU_QtdCertificadoTrans2;		



	END



															IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_QtdCertificadoTrans3')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_QtdCertificadoTrans3 decimal(18,5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_QtdCertificadoTrans3'
		
		DECLARE @OrdemMax72 INT
		SELECT @OrdemMax72 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_QtdCertificadoTrans3', 'QtdCertTransacao3', 'QtdCertTransacao3', 1, 
				@OrdemMax72 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_QtdCertificadoTrans3_DF DEFAULT (0) FOR CDU_QtdCertificadoTrans3;		



	END


	
															IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_QtdCertificadoTrans3')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_QtdCertificadoTrans3 decimal(18,5) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_QtdCertificadoTrans3'
		
		DECLARE @OrdemMax73 INT
		SELECT @OrdemMax73 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_QtdCertificadoTrans3', 'QtdCertTransacao3', 'QtdCertTransacao3', 1, 
				@OrdemMax73 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_QtdCertificadoTrans3_DF DEFAULT (0) FOR CDU_QtdCertificadoTrans3;		



	END


																IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_CertificadoEmitido')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_CertificadoEmitido bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_CertificadoEmitido'
		
		DECLARE @OrdemMax74 INT
		SELECT @OrdemMax74 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_CertificadoEmitido', 'CertEmitido', 'CertEmitido', 1, 
				@OrdemMax74 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_CertificadoEmitido_DF DEFAULT (0) FOR CDU_CertificadoEmitido;		



	END


																	IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_CertificadoEmitido')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_CertificadoEmitido bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_CertificadoEmitido'
		
		DECLARE @OrdemMax75 INT
		SELECT @OrdemMax75 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_CertificadoEmitido', 'CertEmitido', 'CertEmitido', 1, 
				@OrdemMax75 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_CertificadoEmitido_DF DEFAULT (0) FOR CDU_CertificadoEmitido;		



	END



	

																		IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_BCIEmitido')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_BCIEmitido bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_BCIEmitido'
		
		DECLARE @OrdemMax76 INT
		SELECT @OrdemMax76 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_BCIEmitido', 'BCIEmitido', 'BCIEmitido', 1, 
				@OrdemMax76 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_BCIEmitido_DF DEFAULT (0) FOR CDU_BCIEmitido;		



	END



																		IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_BCIEmitido')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_BCIEmitido bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_BCIEmitido'
		
		DECLARE @OrdemMax77 INT
		SELECT @OrdemMax77 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_BCIEmitido', 'BCIEmitido', 'BCIEmitido', 1, 
				@OrdemMax77 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_BCIEmitido_DF DEFAULT (0) FOR CDU_BCIEmitido;		



	END


																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_ObsCertificadoTrans')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_ObsCertificadoTrans bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ObsCertificadoTrans'
		
		DECLARE @OrdemMax78 INT
		SELECT @OrdemMax78 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_ObsCertificadoTrans', 'ObsCertificadoTrans', 'ObsCertificadoTrans', 1, 
				@OrdemMax78 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_ObsCertificadoTrans_DF DEFAULT (0) FOR CDU_ObsCertificadoTrans;		



	END


																			IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_ObsCertificadoTrans')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_ObsCertificadoTrans bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ObsCertificadoTrans'
		
		DECLARE @OrdemMax79 INT
		SELECT @OrdemMax79 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_ObsCertificadoTrans', 'ObsCertificadoTrans', 'ObsCertificadoTrans', 1, 
				@OrdemMax79+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_ObsCertificadoTrans_DF DEFAULT (0) FOR CDU_ObsCertificadoTrans;		



	END



																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_Cambio')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_Cambio float null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Cambio'
		
		DECLARE @OrdemMax80 INT
		SELECT @OrdemMax80 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_Cambio', 'Cambio', 'Cambio', 1, 
				@OrdemMax80+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_Cambio_DF DEFAULT (0) FOR CDU_Cambio;		



	END


																					IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_Cambio')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_Cambio float null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Cambio'
		
		DECLARE @OrdemMax81 INT
		SELECT @OrdemMax81 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_Cambio', 'Cambio', 'Cambio', 1, 
				@OrdemMax81+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_Cambio_DF DEFAULT (0) FOR CDU_Cambio;		



	END
	


																					IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_EstadoGC')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_EstadoGC nvarchar(10) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_EstadoGC'
		
		DECLARE @OrdemMax82 INT
		SELECT @OrdemMax82 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_EstadoGC', 'EstadoGC', 'EstadoGC', 1, 
				@OrdemMax82+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_EstadoGC_DF DEFAULT (0) FOR CDU_EstadoGC;		



	END


																						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_EstadoGC')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_EstadoGC nvarchar(10) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_EstadoGC'
		
		DECLARE @OrdemMax83 INT
		SELECT @OrdemMax83 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_EstadoGC', 'EstadoGC', 'EstadoGC', 1, 
				@OrdemMax83+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_EstadoGC_DF DEFAULT (0) FOR CDU_EstadoGC;		



	END



																						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_EntidadeFinalGrupo')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_EntidadeFinalGrupo nvarchar(12) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_EntidadeFinalGrupo'
		
		DECLARE @OrdemMax84 INT
		SELECT @OrdemMax84 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_EntidadeFinalGrupo', 'EntidadeFinalGrupo', 'EntidadeFinalGrupo', 1, 
				@OrdemMax84+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT CabecDoc_CDU_EntidadeFinalGrupo_DF DEFAULT (0) FOR CDU_EntidadeFinalGrupo;		



	END


																							IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_EntidadeFinalGrupo')
	BEGIN
		ALTER TABLE CabecDocRascunhos ADD CDU_EntidadeFinalGrupo nvarchar(12) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_EntidadeFinalGrupo'
		
		DECLARE @OrdemMax85 INT
		SELECT @OrdemMax85 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDocRascunhos', 'CDU_EntidadeFinalGrupo', 'EntidadeFinalGrupo', 'EntidadeFinalGrupo', 1, 
				@OrdemMax85+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT CabecDocRascunhos_CDU_EntidadeFinalGrupo_DF DEFAULT (0) FOR CDU_EntidadeFinalGrupo;		



	END



																							IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_IdiomaEntidadeFinalGrupo')
	BEGIN
		ALTER TABLE CabecDoc ADD CDU_IdiomaEntidadeFinalGrupo nvarchar(12) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_IdiomaEntidadeFinalGrupo'
		
		DECLARE @OrdemMax86 INT
		SELECT @OrdemMax86 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDoc', 'CDU_IdiomaEntidadeFinalGrupo', 'IdiomaEntFinalGrupo', 'IdiomaEntFinalGrupo', 1, 
				@OrdemMax86+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDoc ADD CONSTRAINT CabecDoc_CDU_IdiomaEntidadeFinalGrupo_DF DEFAULT (0) FOR CDU_IdiomaEntidadeFinalGrupo;		



	END


																								IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_IdiomaEntidadeFinalGrupo')
	BEGIN
		ALTER TABLE CabecDocRascunhos ADD CDU_IdiomaEntidadeFinalGrupo nvarchar(12) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_IdiomaEntidadeFinalGrupo'
		
		DECLARE @OrdemMax87 INT
		SELECT @OrdemMax87 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecDocRascunhos', 'CDU_IdiomaEntidadeFinalGrupo', 'IdiomaEntFinalGrupo', 'IdiomaEntFinalGrupo', 1, 
				@OrdemMax87+ 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecDocRascunhos ADD CONSTRAINT CabecDocRascunhos_CDU_IdiomaEntidadeFinalGrupo_DF DEFAULT (0) FOR CDU_IdiomaEntidadeFinalGrupo;		



	END



																			IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_DescricaoExtraExterna')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_DescricaoExtraExterna nvarchar(200) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_DescricaoExtraExterna'
		
		DECLARE @OrdemMax88 INT
		SELECT @OrdemMax88 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_DescricaoExtraExterna', 'DescExtraExterna', 'DescExtraExterna', 1, 
				@OrdemMax88 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_DescricaoExtraExterna_DF DEFAULT (0) FOR CDU_DescricaoExtraExterna;		

	END


																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_DescricaoExtraExterna')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_DescricaoExtraExterna nvarchar(200) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_DescricaoExtraExterna'
		
		DECLARE @OrdemMax89 INT
		SELECT @OrdemMax89 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_DescricaoExtraExterna', 'DescExtraExterna', 'DescExtraExterna', 1, 
				@OrdemMax89 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_DescricaoExtraExterna_DF DEFAULT (0) FOR CDU_DescricaoExtraExterna;		

	END


	
																			IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_ReferenciaCliente')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_ReferenciaCliente nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_ReferenciaCliente'
		
		DECLARE @OrdemMax90 INT
		SELECT @OrdemMax90 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_ReferenciaCliente', 'ReferenciaCliente', 'ReferenciaCliente', 1, 
				@OrdemMax90 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_ReferenciaCliente_DF DEFAULT (0) FOR CDU_ReferenciaCliente;		

	END


																				IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_ReferenciaCliente')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_ReferenciaCliente nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_ReferenciaCliente'
		
		DECLARE @OrdemMax91 INT
		SELECT @OrdemMax91 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_ReferenciaCliente', 'ReferenciaCliente', 'ReferenciaCliente', 1, 
				@OrdemMax91 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_ReferenciaCliente_DF DEFAULT (0) FOR CDU_ReferenciaCliente;		

	END



																					IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'Clientes' AND COLUMN_NAME = 'CDU_PrintLab')
	BEGIN
		ALTER TABLE Clientes ADD CDU_PrintLab bit null

		DELETE FROM StdCamposVar WHERE Tabela = 'Clientes' AND Campo = 'CDU_PrintLab'
		
		DECLARE @OrdemMax92 INT
		SELECT @OrdemMax92 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'Clientes'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('Clientes', 'CDU_PrintLab', 'PrintLab', 'PrintLab', 1, 
				@OrdemMax92 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE Clientes ADD CONSTRAINT Clientes_CDU_PrintLab_DF DEFAULT (0) FOR CDU_PrintLab;		

	END


	
																					IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDoc' AND COLUMN_NAME = 'CDU_PrecoBase')
	BEGIN
		ALTER TABLE LinhasDoc ADD CDU_PrecoBase float null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_PrecoBase'
		
		DECLARE @OrdemMax93 INT
		SELECT @OrdemMax93 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDoc'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDoc', 'CDU_PrecoBase', 'PrecoBase', 'PrecoBase', 1, 
				@OrdemMax93 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDoc ADD CONSTRAINT LinhasDoc_CDU_PrecoBase_DF DEFAULT (0) FOR CDU_PrecoBase;		

	END


																						IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasDocRascunhos' AND COLUMN_NAME = 'CDU_PrecoBase')
	BEGIN
		ALTER TABLE LinhasDocRascunhos ADD CDU_PrecoBase float null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_PrecoBase'
		
		DECLARE @OrdemMax94 INT
		SELECT @OrdemMax94 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasDocRascunhos'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasDocRascunhos', 'CDU_PrecoBase', 'PrecoBase', 'PrecoBase', 1, 
				@OrdemMax94 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasDocRascunhos ADD CONSTRAINT LinhasDocRascunhos_CDU_PrecoBase_DF DEFAULT (0) FOR CDU_PrecoBase;		
		
	END


																							IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'LinhasCompras' AND COLUMN_NAME = 'CDU_DataUltimaAtualizacao')
	BEGIN
		ALTER TABLE LinhasCompras ADD CDU_DataUltimaAtualizacao datetime null

		DELETE FROM StdCamposVar WHERE Tabela = 'LinhasCompras' AND Campo = 'CDU_DataUltimaAtualizacao'
		
		DECLARE @OrdemMax95 INT
		SELECT @OrdemMax95 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'LinhasCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('LinhasCompras', 'CDU_DataUltimaAtualizacao', 'DataUltimaAtual', 'DataUltimaAtual', 1, 
				@OrdemMax95 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE LinhasCompras ADD CONSTRAINT LinhasCompras_CDU_DataUltimaAtualizacao_DF DEFAULT (0) FOR CDU_DataUltimaAtualizacao;		
		
	END



																								IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecCompras' AND COLUMN_NAME = 'CDU_DocumentoOrigem')
	BEGIN
		ALTER TABLE CabecCompras ADD CDU_DocumentoOrigem nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecCompras' AND Campo = 'CDU_DocumentoOrigem'
		
		DECLARE @OrdemMax96 INT
		SELECT @OrdemMax96 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecCompras', 'CDU_DocumentoOrigem', 'DocumentoOrigem', 'DocumentoOrigem', 1, 
				@OrdemMax96 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecCompras ADD CONSTRAINT CabecCompras_CDU_DocumentoOrigem_DF DEFAULT (0) FOR CDU_DocumentoOrigem;		
		
	END


																									IF NOT EXISTS (SELECT 1	FROM INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = 'CabecCompras' AND COLUMN_NAME = 'CDU_BaseDadosOrigem')
	BEGIN
		ALTER TABLE CabecCompras ADD CDU_BaseDadosOrigem nvarchar(50) null

		DELETE FROM StdCamposVar WHERE Tabela = 'CabecCompras' AND Campo = 'CDU_BaseDadosOrigem'
		
		DECLARE @OrdemMax97 INT
		SELECT @OrdemMax97 = COALESCE(MAX(Ordem), 0)
		FROM StdCamposVar WITH(NOLOCK)
		WHERE TABELA = 'CabecCompras'

		INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query)
		VALUES ('CabecCompras', 'CDU_BaseDadosOrigem', 'BaseDadosOrigem', 'BaseDadosOrigem', 1, 
				@OrdemMax97 + 1,'0','')
				
		--Criar DEFAULT (se aplicável...) 
        ALTER TABLE CabecCompras ADD CONSTRAINT CabecCompras_CDU_BaseDadosOrigem_DF DEFAULT (0) FOR CDU_BaseDadosOrigem;		
		
	END

END 
GO




