USE [API_TESTE]
GO
/****** Object:  StoredProcedure [dbo].[spsPessoa]    Script Date: 23/10/2020 14:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spsPessoa]
  @Id	INT
--with encryption
AS
BEGIN

   SELECT @Id AS Id
   INTO #TabParametros

   SELECT a.Id, a.Nome, a.Sobrenome
   FROM Pessoa a
		JOIN #TabParametros b ON a.Id = b.Id
   WHERE a.Id = b.Id

END