USE [master]
GO
/****** Object:  Database [UdemyETicaretDB]    Script Date: 19.08.2017 12:34:31 ******/
CREATE DATABASE [UdemyETicaretDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UdemyETicaretDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\UdemyETicaretDB.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UdemyETicaretDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\UdemyETicaretDB_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UdemyETicaretDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UdemyETicaretDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UdemyETicaretDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UdemyETicaretDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UdemyETicaretDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UdemyETicaretDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UdemyETicaretDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET RECOVERY FULL 
GO
ALTER DATABASE [UdemyETicaretDB] SET  MULTI_USER 
GO
ALTER DATABASE [UdemyETicaretDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UdemyETicaretDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UdemyETicaretDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UdemyETicaretDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UdemyETicaretDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'UdemyETicaretDB', N'ON'
GO
USE [UdemyETicaretDB]
GO
/****** Object:  User [fgurdal]    Script Date: 19.08.2017 12:34:31 ******/
CREATE USER [fgurdal] FOR LOGIN [fgurdal] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [uniqueidentifier] NOT NULL,
	[AdresDescription] [nvarchar](300) NULL,
	[Member_Id] [int] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Parent_Id] [int] NULL,
	[Description] [nvarchar](250) NULL,
	[AddedDate] [datetime] NULL,
	[ModifedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](250) NOT NULL,
	[Member_Id] [int] NOT NULL,
	[Product_Id] [int] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Members]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[Surname] [nvarchar](25) NULL,
	[Email] [nvarchar](60) NOT NULL,
	[Password] [nvarchar](60) NOT NULL,
	[Bio] [nvarchar](300) NULL,
	[ProfileImageName] [nvarchar](50) NULL,
	[AddedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[MemberType] [int] NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MessageReplies]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageReplies](
	[Id] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](300) NOT NULL,
	[MessageId] [uniqueidentifier] NOT NULL,
	[Member_Id] [int] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MessageReplies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messages]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](60) NOT NULL,
	[IsRead] [bit] NOT NULL,
	[AddedDate] [nchar](10) NOT NULL,
	[ModifiedDate] [nchar](10) NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [tinyint] NOT NULL,
	[Order_Id] [uniqueidentifier] NOT NULL,
	[Product_Id] [int] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[Member_Id] [int] NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[Status] [nchar](10) NULL,
	[Description] [nvarchar](250) NULL,
	[AddedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 19.08.2017 12:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](25) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[IsContinued] [bit] NOT NULL,
	[StarPoint] [int] NOT NULL,
	[StarGivenMemberCount] [int] NOT NULL,
	[ProductImageName] [nvarchar](50) NOT NULL,
	[UnitsInStock] [int] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[Category_Id] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Members] FOREIGN KEY([Member_Id])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Members]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Categories] FOREIGN KEY([Parent_Id])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Categories]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Members] FOREIGN KEY([Member_Id])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Members]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Products] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Products]
GO
ALTER TABLE [dbo].[MessageReplies]  WITH CHECK ADD  CONSTRAINT [FK_MessageReplies_Members] FOREIGN KEY([Member_Id])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[MessageReplies] CHECK CONSTRAINT [FK_MessageReplies_Members]
GO
ALTER TABLE [dbo].[MessageReplies]  WITH CHECK ADD  CONSTRAINT [FK_MessageReplies_Messages] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([Id])
GO
ALTER TABLE [dbo].[MessageReplies] CHECK CONSTRAINT [FK_MessageReplies_Messages]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([Order_Id])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Members] FOREIGN KEY([Member_Id])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Members]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
USE [master]
GO
ALTER DATABASE [UdemyETicaretDB] SET  READ_WRITE 
GO
