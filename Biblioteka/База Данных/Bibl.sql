USE [Biblioteka]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11.10.2024 21:17:46 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 11.10.2024 21:17:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id_Book] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[GenreID] [int] NULL,
	[Year] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[AvailableCopies] [int] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id_Book] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 11.10.2024 21:17:47 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reader]    Script Date: 11.10.2024 21:17:47 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rental]    Script Date: 11.10.2024 21:17:47 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240928080434_first', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([Id_Book], [Title], [Author], [GenreID], [Year], [Description], [AvailableCopies]) VALUES (2, N'Гари Поттер', N'Гарик', 1, 20, N'Мальчик без родителей', 5)
INSERT [dbo].[Book] ([Id_Book], [Title], [Author], [GenreID], [Year], [Description], [AvailableCopies]) VALUES (8, N'Ded', N'Maksim', 2, 7, N'VOt i Pomer', 3)
INSERT [dbo].[Book] ([Id_Book], [Title], [Author], [GenreID], [Year], [Description], [AvailableCopies]) VALUES (11, N'efsdfh', N'fjhsdfhdjk', 1, 3, N'sdbjhf', 3)
INSERT [dbo].[Book] ([Id_Book], [Title], [Author], [GenreID], [Year], [Description], [AvailableCopies]) VALUES (12, N'Властелин колец', N'Дойл', 2, 4, N'Хоббиты бьют орков', 10)
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (1, N'Приключеня')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (2, N'Ужасы')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (3, N'string')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (4, N'Боевик')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (5, N'Ужасы')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (6, N'Роман-эпопея')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (7, N'Мистика')
SET IDENTITY_INSERT [dbo].[Genre] OFF
GO
SET IDENTITY_INSERT [dbo].[Reader] ON 

INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (1, N'Саша', N'Севцов', CAST(N'2005-08-30' AS Date), N'76832478676')
INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (2, N'Рома', N'Степанов', CAST(N'2006-09-15' AS Date), N'87473843654')
INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (5, N'Максим', N'Смирнов', CAST(N'2005-08-30' AS Date), N'89445634272')
INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (6, N'Саша', N'Пловцов', CAST(N'2005-08-30' AS Date), N'78946712724')
INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (7, N'Максим', N'Горький', CAST(N'2006-09-15' AS Date), N'86983432161')
INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (8, N'Рома', N'Салтыков', CAST(N'2006-09-15' AS Date), N'86452362354')
INSERT [dbo].[Reader] ([Id_Reader], [FirstName], [LastName], [DateOfBirth], [ContactDetails]) VALUES (9, N'Саша', N'Фролов', CAST(N'2005-08-30' AS Date), N'35665612368')
SET IDENTITY_INSERT [dbo].[Reader] OFF
GO
SET IDENTITY_INSERT [dbo].[Rental] ON 

INSERT [dbo].[Rental] ([Id_Rental], [ReaderId], [BookId], [RentalDate], [ReturnDate], [Returned]) VALUES (1, 1, 8, CAST(N'2024-09-24' AS Date), CAST(N'2024-09-26' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Rental] OFF
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Genre_GenreID] FOREIGN KEY([GenreID])
REFERENCES [dbo].[Genre] ([Id_Genre])
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
