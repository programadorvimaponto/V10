/*
-------------------------------------------------------------------------------------------
Data:	  18-08-2021
Autor:	  Rafael Oliveira
Solução:    GrupoMundifios
Objetivos:  Nova tabela para guardar funcionalidades a executar
Coment.: 
-------------------------------------------------------------------------------------------
*/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (
		SELECT 1
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = 'TDU_FuncionalidadesExt'
		)
BEGIN
	CREATE TABLE TDU_FuncionalidadesExt (
		[CDU_TokenFuncionalidade] [nvarchar](50) NOT NULL
		,[CDU_DescricaoFuncionalidade] [nvarchar](4000) NOT NULL
		,[CDU_AplicaFuncionalidade] [bit] NOT NULL
		,CONSTRAINT PK_TDU_FuncionalidadesExt PRIMARY KEY CLUSTERED (CDU_TokenFuncionalidade ASC)
		)

	EXEC ('INSERT INTO StdTabelasVar (Tabela, Apl) VALUES (''TDU_FuncionalidadesExt'',''ERP'')')

	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'TDU_FuncionalidadesExt'
				AND COLUMN_NAME = '[CDU_TokenFuncionalidade]'
			)
	BEGIN
		DECLARE @var INT

		SELECT @var = COALESCE(MAX(Ordem), 0)
		FROM STDCAMPOSVAR
		WHERE TABELA = 'TDU_FuncionalidadesExt'

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,ValorDefeito
			,Query
			)
		VALUES (
			'TDU_FuncionalidadesExt'
			,'CDU_TokenFuncionalidade'
			,'TokenFunc'
			,'TokenFunc'
			,1
			,@var + 1
			,''
			,NULL
			)
	END

	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'TDU_FuncionalidadesExt'
				AND COLUMN_NAME = '[CDU_DescricaoFuncionalidade]'
			)
	BEGIN
		DECLARE @var2 INT

		SELECT @var2 = COALESCE(MAX(Ordem), 0)
		FROM STDCAMPOSVAR
		WHERE TABELA = 'TDU_FuncionalidadesExt'

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,ValorDefeito
			,Query
			)
		VALUES (
			'TDU_FuncionalidadesExt'
			,'CDU_DescricaoFuncionalidade'
			,'DescricaoFunc'
			,'DescricaoFunc'
			,1
			,@var2 + 1
			,''
			,NULL
			)
	END

	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'TDU_FuncionalidadesExt'
				AND COLUMN_NAME = '[CDU_AplicaFuncionalidade]'
			)
	BEGIN
		DECLARE @var3 INT

		SELECT @var3 = COALESCE(MAX(Ordem), 0)
		FROM STDCAMPOSVAR
		WHERE TABELA = 'TDU_FuncionalidadesExt'

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,ValorDefeito
			,Query
			)
		VALUES (
			'TDU_FuncionalidadesExt'
			,'CDU_AplicaFuncionalidade'
			,'AplicaFunc'
			,'AplicaFunc'
			,1
			,@var3 + 1
			,''
			,NULL
			)
	END

END
GO