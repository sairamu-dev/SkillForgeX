USE [master]
GO
/****** Object:  Database [DevTaskFlow]    Script Date: 26-08-2025 00:28:44 ******/
CREATE DATABASE [DevTaskFlow]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DevTaskFlow', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DevTaskFlow.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DevTaskFlow_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DevTaskFlow_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DevTaskFlow] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DevTaskFlow].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DevTaskFlow] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DevTaskFlow] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DevTaskFlow] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DevTaskFlow] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DevTaskFlow] SET ARITHABORT OFF 
GO
ALTER DATABASE [DevTaskFlow] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DevTaskFlow] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DevTaskFlow] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DevTaskFlow] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DevTaskFlow] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DevTaskFlow] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DevTaskFlow] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DevTaskFlow] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DevTaskFlow] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DevTaskFlow] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DevTaskFlow] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DevTaskFlow] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DevTaskFlow] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DevTaskFlow] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DevTaskFlow] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DevTaskFlow] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DevTaskFlow] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DevTaskFlow] SET RECOVERY FULL 
GO
ALTER DATABASE [DevTaskFlow] SET  MULTI_USER 
GO
ALTER DATABASE [DevTaskFlow] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DevTaskFlow] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DevTaskFlow] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DevTaskFlow] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DevTaskFlow] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DevTaskFlow] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DevTaskFlow', N'ON'
GO
ALTER DATABASE [DevTaskFlow] SET QUERY_STORE = ON
GO
ALTER DATABASE [DevTaskFlow] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DevTaskFlow]
GO
/****** Object:  Table [dbo].[UDT_ApiService]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_ApiService](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApiName] [varchar](25) NULL,
	[ApiKey] [varchar](50) NULL,
	[Month] [varchar](10) NULL,
	[UsageCount] [int] NULL,
	[isActive] [bit] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_ErrorLog]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_ErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Error] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_Feedback]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_Feedback](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Message] [varchar](510) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_PortalRoles]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_PortalRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Roles] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_Projects]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_Projects](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[ProjectName] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_Skills]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_Skills](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SkillSet] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_SkillsWithKeywords]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_SkillsWithKeywords](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Keyword] [varchar](510) NULL,
	[Skills] [varchar](510) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_SkillsWithKeywords_Bkp]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_SkillsWithKeywords_Bkp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Keyword] [varchar](510) NULL,
	[Skills] [varchar](510) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_Tasks]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_Tasks](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[AssignedTo] [int] NULL,
	[Priority] [varchar](20) NULL,
	[Status] [varchar](20) NULL,
	[RequiredSkills] [nvarchar](255) NULL,
	[EstimatedDays] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ProjectID] [int] NULL,
	[CompletePercentage] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDT_Users]    Script Date: 26-08-2025 00:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDT_Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[UserRole] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ConcurrentTask] [int] NULL,
	[Skills] [varchar](250) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UDT_ApiService] ON 
GO
INSERT [dbo].[UDT_ApiService] ([ID], [ApiName], [ApiKey], [Month], [UsageCount], [isActive], [ModifiedDate]) VALUES (1, N'gemini-1.5-flash', N'Place your api key here', N'Aug', 34, 1, CAST(N'2025-08-16T22:47:30.183' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[UDT_ApiService] OFF
GO
SET IDENTITY_INSERT [dbo].[UDT_ErrorLog] ON 
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (1, N'test', CAST(N'2025-08-09T02:28:19.780' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (2, N'Attempted to divide by zero. - /Manager/Home/GetDeveloperTask', CAST(N'2025-08-12T12:42:28.537' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (3, N'Attempted to divide by zero. - /Manager/Home/GetDeveloperTask', CAST(N'2025-08-12T13:14:32.020' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (4, N'Attempted to divide by zero. - /Manager/Home/GetDeveloperTask', CAST(N'2025-08-13T12:32:58.750' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (6, N'Attempted to divide by zero. - /Manager/Home/GetDeveloperTask', CAST(N'2025-08-13T12:52:11.867' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (7, N'Cannot access a closed Stream. - /Home/GetFeedbacks', CAST(N'2025-08-16T14:06:52.847' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (8, N'The model item passed into the ViewDataDictionary is of type ''DevTaskFlow.ViewModels.ApiViewModel'', but this ViewDataDictionary instance requires a model item of type ''DevTaskFlow.Areas.Admin.ViewModels.ApiViewmodel''. - /Admin/Home/Api', CAST(N'2025-08-17T21:33:08.160' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (9, N'Missing type map configuration or unsupported mapping.

Mapping types:
Api -> ApiViewmodel
DevTaskFlow.Repository_pattern.Core.Enitities.Api -> DevTaskFlow.Areas.Admin.ViewModels.ApiViewmodel - /Admin/Home/Api', CAST(N'2025-08-17T21:39:43.533' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (10, N'Could not load type ''<>f__AnonymousType8`3'' from assembly ''DevTaskFlow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null''. - /Manager', CAST(N'2025-08-18T00:27:08.467' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (11, N'Data is Null. This method or property cannot be called on Null values. - /Home/Signup', CAST(N'2025-08-18T12:53:18.877' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (5, N'Attempted to divide by zero. - /Manager/Home/GetDeveloperTask', CAST(N'2025-08-13T12:38:54.930' AS DateTime))
GO
INSERT [dbo].[UDT_ErrorLog] ([ID], [Error], [CreatedDate]) VALUES (13, N'Value cannot be null. (Parameter ''values'') - /Admin/Home/UpdateTask', CAST(N'2025-08-21T00:32:27.060' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[UDT_ErrorLog] OFF
GO
SET IDENTITY_INSERT [dbo].[UDT_Feedback] ON 
GO
INSERT [dbo].[UDT_Feedback] ([ID], [Name], [Email], [Message]) VALUES (1, N'sai', N'test@email', N'test')
GO
INSERT [dbo].[UDT_Feedback] ([ID], [Name], [Email], [Message]) VALUES (2, N'sai', N'test@email', N'test')
GO
INSERT [dbo].[UDT_Feedback] ([ID], [Name], [Email], [Message]) VALUES (3, N'sai', N'test@email', N'good job keep doing!')
GO
INSERT [dbo].[UDT_Feedback] ([ID], [Name], [Email], [Message]) VALUES (4, N'sai', N'test@email', N'good job keep doing buddy')
GO
INSERT [dbo].[UDT_Feedback] ([ID], [Name], [Email], [Message]) VALUES (5, N'sai', N'test@email', N'test1
')
GO
INSERT [dbo].[UDT_Feedback] ([ID], [Name], [Email], [Message]) VALUES (6, N'sai', N'saismart4864@gmail', N'feedback check')
GO
INSERT [dbo].[UDT_Feedback] ([ID], [Name], [Email], [Message]) VALUES (7, N'sai', N'saismart4864@gmail', N'dev- test')
GO
SET IDENTITY_INSERT [dbo].[UDT_Feedback] OFF
GO
SET IDENTITY_INSERT [dbo].[UDT_PortalRoles] ON 
GO
INSERT [dbo].[UDT_PortalRoles] ([ID], [Roles], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Admin', 1, CAST(N'2025-07-04T11:22:08.143' AS DateTime), 1, NULL, NULL)
GO
INSERT [dbo].[UDT_PortalRoles] ([ID], [Roles], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Manager', 1, CAST(N'2025-07-04T11:22:40.950' AS DateTime), 1, NULL, NULL)
GO
INSERT [dbo].[UDT_PortalRoles] ([ID], [Roles], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Developer', 1, CAST(N'2025-07-04T11:22:59.980' AS DateTime), 1, NULL, NULL)
GO
INSERT [dbo].[UDT_PortalRoles] ([ID], [Roles], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'Guest', 1, CAST(N'2025-07-04T11:23:10.433' AS DateTime), 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[UDT_PortalRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[UDT_Projects] ON 
GO
INSERT [dbo].[UDT_Projects] ([ID], [ProjectName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1000, N'TeamForge', 1, CAST(N'2025-07-04T12:14:38.487' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Projects] ([ID], [ProjectName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1001, N'NextGen ', 1, CAST(N'2025-07-04T12:15:07.970' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Projects] ([ID], [ProjectName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1002, N'WorkBridge ', 1, CAST(N'2025-07-04T12:15:58.507' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Projects] ([ID], [ProjectName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1003, N'WorkNest ', 1, CAST(N'2025-07-04T12:16:13.453' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Projects] ([ID], [ProjectName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1004, N'ProjectPulse ', 1, CAST(N'2025-07-04T12:16:22.157' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[UDT_Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[UDT_Skills] ON 
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1, N'C#', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (2, N'ASP .Net MVC', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (3, N'ASP NET.CORE', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (4, N'ASP.NET Webapi', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (5, N'ASP. Net Webforms', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (6, N'Entity Framework', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (7, N'SQL', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (8, N'CSS', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (9, N'HTML', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (10, N'JavaScript', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
INSERT [dbo].[UDT_Skills] ([ID], [SkillSet], [IsActive], [CreatedDate], [CreatedBy]) VALUES (11, N'jQuery', 1, CAST(N'2025-08-18T12:22:26.317' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[UDT_Skills] OFF
GO
SET IDENTITY_INSERT [dbo].[UDT_SkillsWithKeywords] ON 
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (7, N'page loading', N'JavaScript, jQuery, CSS', 1, CAST(N'2025-08-21T00:39:14.903' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (12, N'notifications', N'ASP .NET MVC,ASP NET.CORE,ASP.NET Webapi,C#,JavaScript,jQuery', 1, CAST(N'2025-08-22T12:56:47.490' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (15, N'pagination', N'ASP .Net MVC,ASP NET.CORE,ASP.NET Webapi,ASP. Net Webforms,C#,JavaScript', 1, CAST(N'2025-08-23T00:11:18.030' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (16, N'homepage', N'ASP .Net MVC,ASP NET.CORE,ASP.NET Webapi,ASP. Net Webforms,C#,JavaScript,HTML,CSS,JavaScript', 1, CAST(N'2025-08-23T00:11:18.037' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1, N'excel', N'SQL', 1, CAST(N'2025-08-20T12:37:17.680' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (2, N'error', N'C#, ASP .Net MVC, ASP NET.CORE, ASP.NET Webapi, ASP. Net Webforms', 1, CAST(N'2025-08-20T12:37:17.687' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (3, N'download', N'ASP .Net MVC, ASP NET.CORE, ASP.NET Webapi, ASP. Net Webforms', 1, CAST(N'2025-08-20T12:37:17.687' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (4, N'bootstrap', N'CSS, HTML', 1, CAST(N'2025-08-20T12:44:50.810' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (5, N'unit test', N'C#', 1, CAST(N'2025-08-20T12:51:49.143' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (6, N'module', N'C#', 1, CAST(N'2025-08-20T12:51:49.147' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (8, N'logger', N'C#', 1, CAST(N'2025-08-21T12:51:11.647' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (19, N'horizontal line', N'HTML,CSS', 1, CAST(N'2025-08-25T12:20:05.727' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (10, N'stored procedures', N'SQL', 1, CAST(N'2025-08-22T12:12:26.600' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (11, N'optimize', N'SQL', 1, CAST(N'2025-08-22T12:12:30.743' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (13, N'report', N'SQL', 1, CAST(N'2025-08-22T13:03:17.713' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (14, N'download feature', N'SQL,ASP.NET WebApi,C#', 1, CAST(N'2025-08-22T13:03:17.720' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (17, N'loading spinner', N'JavaScript,jQuery,CSS', 1, CAST(N'2025-08-23T00:38:30.797' AS DateTime), 1)
GO
INSERT [dbo].[UDT_SkillsWithKeywords] ([ID], [Keyword], [Skills], [IsActive], [CreatedDate], [CreatedBy]) VALUES (18, N'task creation page', N'JavaScript,jQuery,CSS,HTML,CSS,JavaScript', 1, CAST(N'2025-08-23T00:38:30.800' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[UDT_SkillsWithKeywords] OFF
GO
SET IDENTITY_INSERT [dbo].[UDT_Tasks] ON 
GO
INSERT [dbo].[UDT_Tasks] ([TaskID], [Title], [Description], [CreatedBy], [AssignedTo], [Priority], [Status], [RequiredSkills], [EstimatedDays], [CreatedDate], [EndDate], [ProjectID], [CompletePercentage]) VALUES (4, N'bug fix', N'fix page slow loading issue', 1, 3, N'Medium', N'Completed', N'CSS,JavaScript,jQuery', 4, CAST(N'2025-08-21T00:41:04.683' AS DateTime), CAST(N'2025-08-26T00:00:00.000' AS DateTime), 1000, NULL)
GO
INSERT [dbo].[UDT_Tasks] ([TaskID], [Title], [Description], [CreatedBy], [AssignedTo], [Priority], [Status], [RequiredSkills], [EstimatedDays], [CreatedDate], [EndDate], [ProjectID], [CompletePercentage]) VALUES (5, N'bug fix', N'implement sidepanel for dashboard page', 1, 3, N'Medium', N'Completed', N'HTML,CSS,JavaScript,ASP .Net MVC,ASP NET.CORE,ASP.NET Webapi,ASP. Net Webforms', 4, CAST(N'2025-08-21T13:07:55.340' AS DateTime), CAST(N'2025-08-25T00:00:00.000' AS DateTime), 1001, NULL)
GO
INSERT [dbo].[UDT_Tasks] ([TaskID], [Title], [Description], [CreatedBy], [AssignedTo], [Priority], [Status], [RequiredSkills], [EstimatedDays], [CreatedDate], [EndDate], [ProjectID], [CompletePercentage]) VALUES (6, N'optimize stored procedures', N'optimize stored procedures', 1, 5, N'Medium', N'Pending', N'SQL', 4, CAST(N'2025-08-22T12:12:46.647' AS DateTime), CAST(N'2025-08-26T00:00:00.000' AS DateTime), 1001, 17)
GO
INSERT [dbo].[UDT_Tasks] ([TaskID], [Title], [Description], [CreatedBy], [AssignedTo], [Priority], [Status], [RequiredSkills], [EstimatedDays], [CreatedDate], [EndDate], [ProjectID], [CompletePercentage]) VALUES (7, N'implement notifications page', N'implement notifications page', 1, 3, N'Medium', N'Pending', N'ASP .NET MVC,ASP NET.CORE,ASP.NET Webapi,C#,JavaScript,jQuery', 0, CAST(N'2025-08-22T12:57:08.157' AS DateTime), CAST(N'2025-08-22T00:00:00.000' AS DateTime), 1001, NULL)
GO
INSERT [dbo].[UDT_Tasks] ([TaskID], [Title], [Description], [CreatedBy], [AssignedTo], [Priority], [Status], [RequiredSkills], [EstimatedDays], [CreatedDate], [EndDate], [ProjectID], [CompletePercentage]) VALUES (8, N'bug fix', N'implement download report feature', 1, 8, N'Medium', N'Pending', N'SQL,ASP.NET WebApi,C#', 0, CAST(N'2025-08-22T13:03:17.803' AS DateTime), CAST(N'2025-08-22T00:00:00.000' AS DateTime), 1001, NULL)
GO
INSERT [dbo].[UDT_Tasks] ([TaskID], [Title], [Description], [CreatedBy], [AssignedTo], [Priority], [Status], [RequiredSkills], [EstimatedDays], [CreatedDate], [EndDate], [ProjectID], [CompletePercentage]) VALUES (9, N'implement pagination for homepage', N'implement pagination for homepage', 1, 3, N'Medium', N'Pending', N'ASP .Net MVC,ASP NET.CORE,ASP.NET Webapi,ASP. Net Webforms,C#,JavaScript,HTML,CSS', 4, CAST(N'2025-08-23T00:11:18.133' AS DateTime), CAST(N'2025-08-27T00:00:00.000' AS DateTime), 1001, NULL)
GO
INSERT [dbo].[UDT_Tasks] ([TaskID], [Title], [Description], [CreatedBy], [AssignedTo], [Priority], [Status], [RequiredSkills], [EstimatedDays], [CreatedDate], [EndDate], [ProjectID], [CompletePercentage]) VALUES (10, N'implement loading spinner for task creation page', N'implement loading spinner for task creation page', 1, 4, N'Medium', N'Pending', N'JavaScript,jQuery,CSS,HTML', 2, CAST(N'2025-08-23T00:38:30.823' AS DateTime), CAST(N'2025-08-25T00:00:00.000' AS DateTime), 1001, NULL)
GO
INSERT [dbo].[UDT_Tasks] ([TaskID], [Title], [Description], [CreatedBy], [AssignedTo], [Priority], [Status], [RequiredSkills], [EstimatedDays], [CreatedDate], [EndDate], [ProjectID], [CompletePercentage]) VALUES (11, N'UI/UX change', N'make horizontal line for homepage', 1, 7, N'Low', N'Pending', N'CSS,HTML', 0, CAST(N'2025-08-25T12:20:05.773' AS DateTime), CAST(N'2025-08-25T00:00:00.000' AS DateTime), 1000, NULL)
GO
SET IDENTITY_INSERT [dbo].[UDT_Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[UDT_Users] ON 
GO
INSERT [dbo].[UDT_Users] ([ID], [UserName], [Password], [UserRole], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [ConcurrentTask], [Skills]) VALUES (1, N'AdminUser', N'Password01', 1, 1, CAST(N'2025-08-18T12:37:08.930' AS DateTime), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[UDT_Users] ([ID], [UserName], [Password], [UserRole], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [ConcurrentTask], [Skills]) VALUES (2, N'Manager', N'Password01', 2, 1, CAST(N'2025-08-18T12:37:08.930' AS DateTime), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[UDT_Users] ([ID], [UserName], [Password], [UserRole], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [ConcurrentTask], [Skills]) VALUES (3, N'Adam', N'Password01', 3, 1, CAST(N'2025-08-18T12:37:08.930' AS DateTime), 1, CAST(N'2025-08-23T00:11:18.130' AS DateTime), 2, 2, N'C#,ASP .Net MVC,ASP NET.CORE,ASP.NET Webapi,ASP. Net Webforms,Entity Framework,SQL,CSS,HTML,JavaScript,jQuery')
GO
INSERT [dbo].[UDT_Users] ([ID], [UserName], [Password], [UserRole], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [ConcurrentTask], [Skills]) VALUES (4, N'Bob', N'Password01', 3, 1, CAST(N'2025-08-18T12:37:08.930' AS DateTime), 1, CAST(N'2025-08-23T00:38:30.823' AS DateTime), 2, 1, N'C#,CSS,HTML,JavaScript,jQuery')
GO
INSERT [dbo].[UDT_Users] ([ID], [UserName], [Password], [UserRole], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [ConcurrentTask], [Skills]) VALUES (5, N'David', N'Password01', 3, 1, CAST(N'2025-08-18T12:37:08.930' AS DateTime), 1, CAST(N'2025-08-22T12:31:31.130' AS DateTime), 1, 1, N'C#,ASP .Net MVC,ASP NET.CORE,ASP.NET Webapi,ASP. Net Webforms,Entity Framework,SQL')
GO
INSERT [dbo].[UDT_Users] ([ID], [UserName], [Password], [UserRole], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [ConcurrentTask], [Skills]) VALUES (6, N'Henry', N'Password01', 3, 1, CAST(N'2025-08-18T12:37:08.930' AS DateTime), 1, CAST(N'2025-08-25T12:51:53.340' AS DateTime), 1, 0, N'CSS,HTML,JavaScript,jQuery')
GO
INSERT [dbo].[UDT_Users] ([ID], [UserName], [Password], [UserRole], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [ConcurrentTask], [Skills]) VALUES (7, N'Paul', N'Password01', 3, 1, CAST(N'2025-08-18T12:37:08.930' AS DateTime), 1, CAST(N'2025-08-25T12:51:53.340' AS DateTime), 1, 1, N'CSS,HTML,JavaScript,jQuery')
GO
INSERT [dbo].[UDT_Users] ([ID], [UserName], [Password], [UserRole], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [ConcurrentTask], [Skills]) VALUES (8, N'Tom', N'Password01', 3, 1, CAST(N'2025-08-18T12:37:08.930' AS DateTime), 1, CAST(N'2025-08-22T13:03:17.800' AS DateTime), 2, 1, N'C#,ASP .Net MVC,ASP NET.CORE,ASP.NET Webapi,ASP. Net Webforms,Entity Framework,SQL,CSS,HTML,JavaScript,jQuery')
GO
SET IDENTITY_INSERT [dbo].[UDT_Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__UDT_Port__F92229BC7E8409F1]    Script Date: 26-08-2025 00:28:44 ******/
ALTER TABLE [dbo].[UDT_PortalRoles] ADD UNIQUE NONCLUSTERED 
(
	[Roles] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UDT_ApiService] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[UDT_Projects] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UDT_Skills] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UDT_Skills] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UDT_SkillsWithKeywords] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UDT_SkillsWithKeywords] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UDT_SkillsWithKeywords] ADD  CONSTRAINT [DF_UDT_SkillsWithKeywords_CreatedBy]  DEFAULT ((1)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[UDT_Tasks] ADD  DEFAULT ('Medium') FOR [Priority]
GO
ALTER TABLE [dbo].[UDT_Tasks] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[UDT_Tasks] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UDT_Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UDT_Tasks]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[UDT_Projects] ([ID])
GO
ALTER TABLE [dbo].[UDT_Users]  WITH CHECK ADD FOREIGN KEY([UserRole])
REFERENCES [dbo].[UDT_PortalRoles] ([ID])
GO
ALTER TABLE [dbo].[UDT_Tasks]  WITH CHECK ADD CHECK  (([Priority]='Critical' OR [Priority]='High' OR [Priority]='Medium' OR [Priority]='Low'))
GO
ALTER TABLE [dbo].[UDT_Tasks]  WITH CHECK ADD  CONSTRAINT [CK__UDT_Tasks__Status_ValidValues] CHECK  (([Status]='Completed' OR [Status]='InProgress' OR [Status]='Pending' OR [Status]='In-Queue'))
GO
ALTER TABLE [dbo].[UDT_Tasks] CHECK CONSTRAINT [CK__UDT_Tasks__Status_ValidValues]
GO
ALTER TABLE [dbo].[UDT_Users]  WITH CHECK ADD  CONSTRAINT [CHK_ConcurrentTask_Limit] CHECK  (([ConcurrentTask]>=(0) AND [ConcurrentTask]<=(5)))
GO
ALTER TABLE [dbo].[UDT_Users] CHECK CONSTRAINT [CHK_ConcurrentTask_Limit]
GO
USE [master]
GO
ALTER DATABASE [DevTaskFlow] SET  READ_WRITE 
GO
