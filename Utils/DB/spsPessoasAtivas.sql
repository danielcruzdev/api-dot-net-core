USE [API_TESTE]
GO
/****** Object:  StoredProcedure [dbo].[spsPessoas]    Script Date: 27/10/2020 11:34:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spsPessoasAtivas]
--with encryption
AS
BEGIN
   SELECT * FROM Pessoa a
	WHERE a.Ativo = 1
END