USE [ShopDB]
GO
/****** Object:  Table [dbo].[Categores]    Script Date: 30/09/2017 4:11:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Alias] [varchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Categores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Categores] ON 

INSERT [dbo].[Categores] ([Id], [Name], [Alias], [Content], [ParentId]) VALUES (1, N'Smart phone', N'smart-phone', N'Smart phone', NULL)
INSERT [dbo].[Categores] ([Id], [Name], [Alias], [Content], [ParentId]) VALUES (2, N'Battery', N'battery', N'battery', NULL)
INSERT [dbo].[Categores] ([Id], [Name], [Alias], [Content], [ParentId]) VALUES (3, N'Iphone', N'iphone', N'I-phone', 1)
INSERT [dbo].[Categores] ([Id], [Name], [Alias], [Content], [ParentId]) VALUES (4, N'Android phone', N'android-phone', N'Android OS', 1)
INSERT [dbo].[Categores] ([Id], [Name], [Alias], [Content], [ParentId]) VALUES (5, N'Smart phone battery', N'smart-phone-battery', N'Battery', 2)
INSERT [dbo].[Categores] ([Id], [Name], [Alias], [Content], [ParentId]) VALUES (6, N'Laptop battery', N'laptop-baterry', N'Battery for laptop', 2)
INSERT [dbo].[Categores] ([Id], [Name], [Alias], [Content], [ParentId]) VALUES (7, N'Baterry Iphone', N'battery-iphone', N'Battery for iphone', 5)
SET IDENTITY_INSERT [dbo].[Categores] OFF
/****** Object:  StoredProcedure [dbo].[SP_GetAllCategory]    Script Date: 30/09/2017 4:11:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GetAllCategory]
AS
BEGIN
	SELECT [Id]
      ,[Name]
      ,[Alias]
      ,[Content]
      ,[ParentId]
  FROM [dbo].[Categores]
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllCategoryParentV2]    Script Date: 30/09/2017 4:11:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		lpnam
-- Create date: 19/08/2017
-- Description:	SP_GetAllCategoryParentV2 'Get info user login'
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllCategoryParentV2]
AS
BEGIN
	SELECT [Id]
      ,[Name]
      ,(SELECT C2.[Id],C2.[Name],C2.[Alias],C2.[Content],C2.[ParentId] 
		FROM [dbo].[Categores] C2 
		WHERE C2.[ParentId] = C1.Id
		FOR XML PATH ('Category'), root ('ArrayOfCategory')) AS ListCategory
  FROM [dbo].[Categores] C1

END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetCategoryById]    Script Date: 30/09/2017 4:11:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		lpnam
-- Create date: 19/08/2017
-- Description:	SP_GetCategoryById 'Get info user login'
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCategoryById]
	@Id int = null
AS
BEGIN
	SET NOCOUNT ON;
	Set @Id = ISNull(@Id,0)
	
	SELECT [Id]
      ,[Name]
      ,[Alias]
      ,[Content]
      ,[ParentId]
  FROM [dbo].[Categores]
	WHERE [Id] = @Id

END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetCategoryParentById]    Script Date: 30/09/2017 4:11:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetCategoryParentById]
	@Id int = null
AS
BEGIN
	SET NOCOUNT ON;
	Set @Id = ISNull(@Id,0)

	SELECT (SELECT [Id],[Name],[Alias],[Content],[ParentId] FROM Categores WHERE [Id] = @Id FOR XML PATH ('Category')) AS Category
      ,(SELECT [Id],[Name],[Alias],[Content],[ParentId] FROM [dbo].[Categores] WHERE [ParentId] = @Id
		FOR XML PATH ('Category'), root ('ArrayOfCategory')) AS ListCategory
	FROM [dbo].[Categores]
	WHERE [Id] = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetCategoryParentByIdV2]    Script Date: 30/09/2017 4:11:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		lpnam
-- Create date: 19/08/2017
-- Description:	SP_GetCategoryParentByIdV2 'Get info user login'
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCategoryParentByIdV2]
	@Id int = null
AS
BEGIN
	SET NOCOUNT ON;
	Set @Id = ISNull(@Id,0)
	
	SELECT [Id]
      ,[Name]
      ,(SELECT [Id],[Name],[Alias],[Content],[ParentId] FROM [dbo].[Categores] WHERE [ParentId] = @Id
		FOR XML PATH ('Category'), root ('ArrayOfCategory')) AS ListCategory
  FROM [dbo].[Categores]
	WHERE [Id] = @Id

END

GO
