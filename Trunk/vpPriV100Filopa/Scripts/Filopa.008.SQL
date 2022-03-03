
-- ---------------------------------------------
-- -- A CORRER SÓ NA BASE DE DADOS DA FILOPA ---
-- ---------------------------------------------



-- ----------------------------------------------------------------------------------------
-- NOTA MUITO IMPORTANTE :                                                               --
-- ----------------------------------------------------------------------------------------
-- DEPOIS DE CORRER MANUALMENTE ESTA SCRIPT                                              --
-- É NECESSÁRIO NO ADMINISTRADOR PRIMAVERA DE AMBAS AS BASES DE DADOS CORRER             --
-- A OPÇÃO DE RECONSTRUÇÃO DAS TABELAS DE UTILIZADOR PARA QUE OS NOVOS CAMPOS SEJAM      --
-- BEM RECONHECIDOS PELO PRIMAVERA E PELOS MOTORES DO PRIMAVERA                          --
-- ----------------------------------------------------------------------------------------       








/*
-------------------------------------------------------------------------------------------
Data:	    16-05-2019
Autor:	    Gualter Costa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Adição de Campos de Utilizador para Rastreabilidade da cópia de documentos entre bases de dados nas tabelas (CabecDoc, CabecDocRascunhos e CabecCompras)
Coment.:    
-------------------------------------------------------------------------------------------
*/






-- -----------------------
-- TABELA : CABECDOC   ------------------------------------------------------------------------------------------------------------
-- -----------------------

--        Os campos CDU_DocumentoVendaDestino, CDU_DocumentoCompraDestino, CDU_DocumentoOrigem ---> São criados para serem usados na rastreabilidade da cópia de documentos entre bases de dados



-- campo : CDU_DocumentoVendaDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_DocumentoVendaDestino')
BEGIN

       ALTER TABLE CabecDoc ADD CDU_DocumentoVendaDestino NVARCHAR(50) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecDoc'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecDoc', 'CDU_DocumentoVendaDestino', 'Doc. Venda Destino', 'Doc. Venda Destino', 1, @Var + 1, '', NULL)
END
GO



--  campo : CDU_DocumentoCompraDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_DocumentoCompraDestino')
BEGIN

       ALTER TABLE CabecDoc ADD CDU_DocumentoCompraDestino NVARCHAR(50)
 
       DECLARE @Var INT                   
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecDoc'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecDoc', 'CDU_DocumentoCompraDestino', 'Doc. Compra Destino', 'Doc. Compra Destino', 1, @Var + 1, '', NULL)
END
GO





-- campo : CDU_DocumentoOrigem

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecDoc' AND COLUMN_NAME = 'CDU_DocumentoOrigem')
BEGIN

       ALTER TABLE CabecDoc ADD CDU_DocumentoOrigem NVARCHAR(50) 
       
       DECLARE @Var INT            
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecDoc'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecDoc', 'CDU_DocumentoOrigem', 'Doc. Origem', 'Doc. Origem', 1, @Var + 1, '', NULL)
END
GO











-- --------------------------------
-- TABELA : cabecDocRascunhos   ----------------------------------------------------------------------------------------------------------------------------------------------------
-- --------------------------------


--        Os campos CDU_DocumentoVendaDestino, CDU_DocumentoCompraDestino, CDU_DocumentoOrigem ---> São criados para serem usados na rastreabilidade da cópia de documentos entre bases de dados




-- campo : CDU_DocumentoVendaDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_DocumentoVendaDestino')
BEGIN

       ALTER TABLE CabecDocRascunhos ADD CDU_DocumentoVendaDestino NVARCHAR(50) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecDocRascunhos'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecDocRascunhos', 'CDU_DocumentoVendaDestino', 'Doc. Venda Destino', 'Doc. Venda Destino', 1, @Var + 1, '', NULL)
END
GO




--  campo : CDU_DocumentoCompraDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_DocumentoCompraDestino')
BEGIN

       ALTER TABLE CabecDocRascunhos ADD CDU_DocumentoCompraDestino NVARCHAR(50)
 
       DECLARE @Var INT                   
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecDocRascunhos'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecDocRascunhos', 'CDU_DocumentoCompraDestino', 'Doc. Compra Destino', 'Doc. Compra Destino', 1, @Var + 1, '', NULL)
END
GO



-- campo : CDU_DocumentoOrigem

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecDocRascunhos' AND COLUMN_NAME = 'CDU_DocumentoOrigem')
BEGIN

       ALTER TABLE CabecDocRascunhos ADD CDU_DocumentoOrigem NVARCHAR(50) 
       
       DECLARE @Var INT            
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecDocRascunhos'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecDocRascunhos', 'CDU_DocumentoOrigem', 'Doc. Origem', 'Doc. Origem', 1, @Var + 1, '', NULL)
END
GO







-- -----------------------
-- TABELA : CabecCompras   ------------------------------------------------------------------------------------------------------------
-- -----------------------

--  NOTA :  Os campos CDU_DocumentoVendaDestino, CDU_DocumentoCompraDestino, CDU_DocumentoOrigem ---> São criados para serem usados na rastreabilidade da cópia de documentos entre bases de dados




-- campo : CDU_DocumentoVendaDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecCompras' AND COLUMN_NAME = 'CDU_DocumentoVendaDestino')
BEGIN

       ALTER TABLE CabecCompras ADD CDU_DocumentoVendaDestino NVARCHAR(50) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecCompras'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecCompras', 'CDU_DocumentoVendaDestino', 'Doc. Venda Destino', 'Doc. Venda Destino', 1, @Var + 1, '', NULL)
END
GO





--  campo : CDU_DocumentoCompraDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecCompras' AND COLUMN_NAME = 'CDU_DocumentoCompraDestino')
BEGIN

       ALTER TABLE CabecCompras ADD CDU_DocumentoCompraDestino NVARCHAR(50)
 
       DECLARE @Var INT                   
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecCompras'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecCompras', 'CDU_DocumentoCompraDestino', 'Doc. Compra Destino', 'Doc. Compra Destino', 1, @Var + 1, '', NULL)
END
GO





-- campo : CDU_DocumentoOrigem

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CabecCompras' AND COLUMN_NAME = 'CDU_DocumentoOrigem')
BEGIN

       ALTER TABLE CabecCompras ADD CDU_DocumentoOrigem NVARCHAR(50) 
       
       DECLARE @Var INT            
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'CabecCompras'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('CabecCompras', 'CDU_DocumentoOrigem', 'Doc. Origem', 'Doc. Origem', 1, @Var + 1, '', NULL)
END
GO


GO

---------------------------- NO MODULO DE COMPRAS NÃO HÁ TABELA DE RASCUNHOS ---------------------------------------


GO