USE [Biblioteka]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 04.10.2024 13:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 04.10.2024 13:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id_Book] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[GenreID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[AvailableCopies] [int] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id_Book] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 04.10.2024 13:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Id_Genre] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[Id_Genre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reader]    Script Date: 04.10.2024 13:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reader](
	[Id_Reader] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[ContactDetails] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Reader] PRIMARY KEY CLUSTERED 
(
	[Id_Reader] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rental]    Script Date: 04.10.2024 13:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rental](
	[Id_Rental] [int] IDENTITY(1,1) NOT NULL,
	[ReaderId] [int] NULL,
	[BookId] [int] NULL,
	[RentalDate] [date] NOT NULL,
	[ReturnDate] [date] NOT NULL,
	[Returned] [bit] NOT NULL,
 CONSTRAINT [PK_Rental] PRIMARY KEY CLUSTERED 
(
	[Id_Rental] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241001051434_first', N'8.0.8')
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([Id_Book], [Title], [Author], [GenreID], [Year], [Description], [AvailableCopies]) VALUES (2, N'Война и мир', N'Валера', 1, 5, N'Рим', 2)
SET IDENTITY_INSERT [dbo].[Book] OFF
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (1, N'Эпопея')
SET IDENTITY_INSERT [dbo].[Genre] OFF
SET IDENTITY_INSERT [dbo].[Reader] ON 

INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (2, N'Саня', N'Саня', CAST(N'2006-09-19' AS Date), N'262377823')
INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (3, N'Гизатулин', N'Расим', CAST(N'2009-09-19' AS Date), N'65328943864')
INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (4, N'Гизатулин', N'Расим', CAST(N'2009-09-19' AS Date), N'65328943864')
SET IDENTITY_INSERT [dbo].[Reader] OFF
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Genre_GenreID] FOREIGN KEY([GenreID])
REFERENCES [dbo].[Genre] ([Id_Genre])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Genre_GenreID]
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Rental_Book_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([Id_Book])
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Rental_Book_BookId]
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Rental_Reader_ReaderId] FOREIGN KEY([ReaderId])
REFERENCES [dbo].[Reader] ([Id_Reader])
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Rental_Reader_ReaderId]
GO
