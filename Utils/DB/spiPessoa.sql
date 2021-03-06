USE [API_TESTE]
GO
/****** Object:  StoredProcedure [dbo].[spiPessoa]    Script Date: 27/10/2020 11:14:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spiPessoa]
  @TabParametrosJSON   VARCHAR(MAX),  
  @Id INT OUTPUT
--with encryption                   
AS 
BEGIN
  --DROP TABLE #TabParametros
  SELECT 
		(SELECT ISNULL(MAX(a.Id),0) + 1 FROM Pessoa a) AS Id,
		 JsonData.Nome								   AS Nome,
         JsonData.Sobrenome							   AS Sobrenome,
         JsonData.Email  							   AS Email,
         JsonData.Telefone							   AS Telefone,
         JsonData.DataNascimento					   AS DataNascimento,
         JsonData.Cpf   							   AS Cpf,
         JsonData.Rg    							   AS Rg,
         JsonData.HabCarro							   AS HabCarro,
         JsonData.HabMoto							   AS HabMoto,
         JsonData.Ativo							       AS Ativo	
    INTO #TabParametros
    FROM OPENJSON (@TabParametrosJSON,   N'$')
    WITH (Nome			  VARCHAR(255)   N'$.Nome',
	      Sobrenome		  VARCHAR(255)   N'$.Sobrenome',
		  Email			  VARCHAR(200)   N'$.Email',
		  Telefone		  VARCHAR(255)   N'$.Telefone',
		  DataNascimento  DATETIME       N'$.DataNascimento',
		  Cpf			  VARCHAR(14)	 N'$.Cpf',
		  Rg			  VARCHAR(12)	 N'$.Rg',
		  HabCarro		  BIT			 N'$.HabCarro',
		  HabMoto		  BIT			 N'$.HabMoto',
		  Ativo			  BIT			 N'$.Ativo') AS JsonData

  INSERT INTO Pessoa(Id, Nome, Sobrenome, Email, Telefone, DataNascimento, Cpf, Rg, HabCarro, HabMoto, Ativo)
  SELECT a.Id, 
         a.Nome, 
		 a.Sobrenome, 
		 a.Email, 
		 a.Telefone, 
		 a.DataNascimento, 
		 a.Cpf, 
		 a.Rg, 
		 a.HabCarro, 
		 a.HabMoto, 
		 a.Ativo
    FROM #TabParametros a

  SELECT @Id = a.Id
	 FROM #TabParametros a
END