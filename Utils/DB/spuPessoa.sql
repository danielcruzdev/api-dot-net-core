USE [API_TESTE]
GO
/****** Object:  StoredProcedure [dbo].[spuPessoa]    Script Date: 27/10/2020 11:21:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spuPessoa]
  @TabParametrosJSON   VARCHAR(MAX),
  @Id				   INT OUTPUT
--with encryption                   
AS 
BEGIN
  --DROP TABLE #TabParametros
  SELECT JsonData.Id			   AS Id,
		 JsonData.Nome			   AS Nome,
         JsonData.Sobrenome		   AS Sobrenome,
         JsonData.Email  		   AS Email,
         JsonData.Telefone		   AS Telefone,
         JsonData.DataNascimento   AS DataNascimento,
         JsonData.Cpf   		   AS Cpf,
         JsonData.Rg    		   AS Rg,
         JsonData.HabCarro		   AS HabCarro,
         JsonData.HabMoto		   AS HabMoto,
         JsonData.Ativo			   AS Ativo	
    INTO #TabParametros
    FROM OPENJSON (@TabParametrosJSON,   N'$')
    WITH (Id			  INT			 N'$.Id',
		  Nome			  VARCHAR(255)   N'$.Nome',
	      Sobrenome		  VARCHAR(255)   N'$.Sobrenome',
		  Email			  VARCHAR(200)   N'$.Email',
		  Telefone		  VARCHAR(255)   N'$.Telefone',
		  DataNascimento  DATETIME       N'$.DataNascimento',
		  Cpf			  VARCHAR(14)	 N'$.Cpf',
		  Rg			  VARCHAR(12)	 N'$.Rg',
		  HabCarro		  BIT			 N'$.HabCarro',
		  HabMoto		  BIT			 N'$.HabMoto',
		  Ativo			  BIT			 N'$.Ativo') AS JsonData

  UPDATE a
     SET Nome			= b.Nome,
		 Sobrenome		= b.Sobrenome,
		 Email			= b.Email,
		 Telefone		= b.Telefone,
		 DataNascimento = b.DataNascimento,
		 Cpf			= b.Cpf,
		 Rg				= b.Rg,
		 HabCarro		= b.HabCarro,
		 HabMoto		= b.HabMoto,
		 Ativo			= b.Ativo
    FROM Pessoa  a
	     JOIN #TabParametros b ON a.Id = b.Id

  SELECT @Id = a.Id
	 FROM #TabParametros a
END