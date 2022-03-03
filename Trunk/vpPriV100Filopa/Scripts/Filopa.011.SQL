/*
-------------------------------------------------------------------------------------------
Data:	    19-06-2019
Autor:	    Andre Sousa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Criação de um script para copiar os novos campos durante a transformação
Coment.:    
-------------------------------------------------------------------------------------------
*/

	IF NOT EXISTS (
			SELECT 1
			FROM LigacaoCamposUtil
			WHERE TabelaOrigem = 'LinhasDoc'
				AND CampoOrigem = 'CDU_Situacao'
				AND TabelaDestino = 'LinhasDoc'
				AND CampoDestino = 'CDU_Situacao'
				AND Operacao = 4
				AND DocumentoOrigem = ''
			)
		BEGIN		
			INSERT INTO LigacaoCamposUtil
						(TabelaOrigem, CampoOrigem, TabelaDestino, CampoDestino, Operacao, ModuloOrigem, ModuloDestino, DocumentoOrigem) 
						VALUES
						('LinhasDoc', 'CDU_Situacao','LinhasDoc', 'CDU_Situacao', '4', 'V', 'V', '')
		END

	IF NOT EXISTS (
			SELECT 1
			FROM LigacaoCamposUtil
			WHERE TabelaOrigem = 'LinhasDoc'
				AND CampoOrigem = 'CDU_Parafinado'
				AND TabelaDestino = 'LinhasDoc'
				AND CampoDestino = 'CDU_Parafinado'
				AND Operacao = 4
				AND DocumentoOrigem = ''
			)
		BEGIN		
			INSERT INTO LigacaoCamposUtil
						(TabelaOrigem, CampoOrigem, TabelaDestino, CampoDestino, Operacao, ModuloOrigem, ModuloDestino, DocumentoOrigem) 
						VALUES
						('LinhasDoc', 'CDU_Parafinado','LinhasDoc', 'CDU_Parafinado', '4', 'V', 'V', '')
		END		

	IF NOT EXISTS (
			SELECT 1
			FROM LigacaoCamposUtil
			WHERE TabelaOrigem = 'LinhasDoc'
				AND CampoOrigem = 'CDU_PaisOrigem'
				AND TabelaDestino = 'LinhasDoc'
				AND CampoDestino = 'CDU_PaisOrigem'
				AND Operacao = 4
				AND DocumentoOrigem = ''
			)
		BEGIN		
			INSERT INTO LigacaoCamposUtil
						(TabelaOrigem, CampoOrigem, TabelaDestino, CampoDestino, Operacao, ModuloOrigem, ModuloDestino, DocumentoOrigem) 
						VALUES
						('LinhasDoc', 'CDU_PaisOrigem','LinhasDoc', 'CDU_PaisOrigem', '4', 'V', 'V', '')
		END

	IF NOT EXISTS (
			SELECT 1
			FROM LigacaoCamposUtil
			WHERE TabelaOrigem = 'LinhasDoc'
				AND CampoOrigem = 'CDU_Seguradora'
				AND TabelaDestino = 'LinhasDoc'
				AND CampoDestino = 'CDU_Seguradora'
				AND Operacao = 4
				AND DocumentoOrigem = ''
			)
		BEGIN		
			INSERT INTO LigacaoCamposUtil
						(TabelaOrigem, CampoOrigem, TabelaDestino, CampoDestino, Operacao, ModuloOrigem, ModuloDestino, DocumentoOrigem) 
						VALUES
						('LinhasDoc', 'CDU_Seguradora','LinhasDoc', 'CDU_Seguradora', '4', 'V', 'V', '')
		END		

	IF NOT EXISTS (
			SELECT 1
			FROM LigacaoCamposUtil
			WHERE TabelaOrigem = 'LinhasDoc'
				AND CampoOrigem = 'CDU_Parque'
				AND TabelaDestino = 'LinhasDoc'
				AND CampoDestino = 'CDU_Parque'
				AND Operacao = 4
				AND DocumentoOrigem = ''
			)
		BEGIN		
			INSERT INTO LigacaoCamposUtil
						(TabelaOrigem, CampoOrigem, TabelaDestino, CampoDestino, Operacao, ModuloOrigem, ModuloDestino, DocumentoOrigem) 
						VALUES
						('LinhasDoc', 'CDU_Parque','LinhasDoc', 'CDU_Parque', '4', 'V', 'V', '')
		END

	IF NOT EXISTS (
			SELECT 1
			FROM LigacaoCamposUtil
			WHERE TabelaOrigem = 'LinhasDoc'
				AND CampoOrigem = 'CDU_ObsMdf'
				AND TabelaDestino = 'LinhasDoc'
				AND CampoDestino = 'CDU_ObsMdf'
				AND Operacao = 4
				AND DocumentoOrigem = ''
			)
		BEGIN	
			INSERT INTO LigacaoCamposUtil
						(TabelaOrigem, CampoOrigem, TabelaDestino, CampoDestino, Operacao, ModuloOrigem, ModuloDestino, DocumentoOrigem) 
						VALUES
						('LinhasDoc', 'CDU_ObsMdf','LinhasDoc', 'CDU_ObsMdf', '4', 'V', 'V', '')
		END

	IF NOT EXISTS (
			SELECT 1
			FROM LigacaoCamposUtil
			WHERE TabelaOrigem = 'LinhasDoc'
				AND CampoOrigem = 'CDU_NumCertificado'
				AND TabelaDestino = 'LinhasDoc'
				AND CampoDestino = 'CDU_NumCertificado'
				AND Operacao = 4
				AND DocumentoOrigem = ''
			)
		BEGIN			
			INSERT INTO LigacaoCamposUtil
						(TabelaOrigem, CampoOrigem, TabelaDestino, CampoDestino, Operacao, ModuloOrigem, ModuloDestino, DocumentoOrigem) 
						VALUES
						('LinhasDoc', 'CDU_NumCertificado','LinhasDoc', 'CDU_NumCertificado', '4', 'V', 'V', '')
		END

	IF NOT EXISTS (
			SELECT 1
			FROM LigacaoCamposUtil
			WHERE TabelaOrigem = 'LinhasDoc'
				AND CampoOrigem = 'CDU_TipoQualidade'
				AND TabelaDestino = 'LinhasDoc'
				AND CampoDestino = 'CDU_TipoQualidade'
				AND Operacao = 4
				AND DocumentoOrigem = ''
			)
		BEGIN		
			INSERT INTO LigacaoCamposUtil
						(TabelaOrigem, CampoOrigem, TabelaDestino, CampoDestino, Operacao, ModuloOrigem, ModuloDestino, DocumentoOrigem) 
						VALUES
						('LinhasDoc', 'CDU_TipoQualidade','LinhasDoc', 'CDU_TipoQualidade', '4', 'V', 'V', '')
		END
