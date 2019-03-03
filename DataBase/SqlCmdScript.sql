USE [master];
GO

CREATE DATABASE [PimsCommand]; 
GO
USE [PimsCommand];
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductData](
	[ZamroID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[MinOrderQuantity] [int] NOT NULL,
	[UnitOfMeasure] [nvarchar](50) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[PurchasePrice] [float] NOT NULL,
	[Available] [tinyint] NOT NULL,
 CONSTRAINT [PK_ProductData] PRIMARY KEY CLUSTERED 
(
	[ZamroID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE DATABASE [PimsQuery]; 
GO
USE [PimsQuery];
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductData](
	[ZamroID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[MinOrderQuantity] [int] NOT NULL,
	[UnitOfMeasure] [nvarchar](50) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[PurchasePrice] [float] NOT NULL,
	[Available] [tinyint] NOT NULL,
 CONSTRAINT [PK_ProductData] PRIMARY KEY CLUSTERED 
(
	[ZamroID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



