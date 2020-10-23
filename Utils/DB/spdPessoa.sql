USE [API_TESTE]
GO
/****** Object:  StoredProcedure [dbo].[spiPessoa]    Script Date: 23/10/2020 14:23:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spdPessoa]
  @Id INT
--with encryption                   
AS 
BEGIN
  --DROP TABLE #TabParametros
  SELECT @Id AS Id
   INTO #TabParametros

   DELETE FROM a
   FROM Pessoa a
		JOIN #TabParametros b ON a.Id = b.Id
END