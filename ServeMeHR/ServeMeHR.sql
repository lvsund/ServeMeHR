USE [master]
GO
/****** Object:  Database [ServeMeHR]    Script Date: 8/27/2016 1:56:23 PM ******/
CREATE DATABASE [ServeMeHR]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ServeMeHR', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.VS2\MSSQL\DATA\ServeMeHR.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ServeMeHR_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.VS2\MSSQL\DATA\ServeMeHR_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ServeMeHR] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ServeMeHR].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ServeMeHR] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ServeMeHR] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ServeMeHR] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ServeMeHR] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ServeMeHR] SET ARITHABORT OFF 
GO
ALTER DATABASE [ServeMeHR] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ServeMeHR] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ServeMeHR] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ServeMeHR] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ServeMeHR] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ServeMeHR] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ServeMeHR] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ServeMeHR] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ServeMeHR] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ServeMeHR] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ServeMeHR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ServeMeHR] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ServeMeHR] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ServeMeHR] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ServeMeHR] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ServeMeHR] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ServeMeHR] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ServeMeHR] SET RECOVERY FULL 
GO
ALTER DATABASE [ServeMeHR] SET  MULTI_USER 
GO
ALTER DATABASE [ServeMeHR] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ServeMeHR] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ServeMeHR] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ServeMeHR] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ServeMeHR] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ServeMeHR', N'ON'
GO
ALTER DATABASE [ServeMeHR] SET QUERY_STORE = OFF
GO
USE [ServeMeHR]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ServeMeHR]
GO
/****** Object:  Table [dbo].[ADInformations]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADInformations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[sAMAccountNAme] [nvarchar](255) NOT NULL,
	[displayName] [nvarchar](255) NULL,
	[mail] [nvarchar](255) NULL,
	[title] [nvarchar](255) NULL,
	[telephoneNumber] [nvarchar](255) NULL,
	[givenName] [nvarchar](255) NOT NULL,
	[sn] [nvarchar](255) NOT NULL,
	[company] [nvarchar](255) NULL,
	[wwWHomePage] [nvarchar](255) NULL,
	[mobile] [nvarchar](255) NULL,
	[cn] [nvarchar](255) NULL,
	[APPUSERNAME] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_ADInformations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApplicConfs]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicConfs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileSystemUpload] [bit] NOT NULL,
	[ADActive] [bit] NOT NULL,
	[EmailConfirmation] [bit] NOT NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[AppAdmin] [nvarchar](255) NULL,
	[BackAdmin] [nvarchar](255) NULL,
	[LDAPConn] [nvarchar](255) NULL,
	[LDAPPath] [nvarchar](255) NULL,
	[ManageHREmail] [nvarchar](255) NULL,
	[ManageHREmailPass] [nvarchar](255) NULL,
	[SMTPHost] [nvarchar](255) NULL,
	[SMTPPort] [int] NULL,
	[EnableSSL] [bit] NULL,
 CONSTRAINT [PK_ApplicConfs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](255) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Location] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[MsgID] [nvarchar](255) NULL,
	[MsgSequence] [int] NULL,
	[SenderEmail] [nvarchar](255) NOT NULL,
	[RecipientEmail] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileDetails]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](255) NULL,
	[Extension] [nvarchar](10) NULL,
	[ServiceRequestID] [int] NULL,
 CONSTRAINT [PK_FileDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IndividualAssignmentHistories]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IndividualAssignmentHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssignedTo] [int] NOT NULL,
	[AssignedBy] [nvarchar](255) NOT NULL,
	[DateAssigned] [datetime] NOT NULL,
	[ServiceRequest] [int] NULL,
 CONSTRAINT [PK_IndividualAssignmentHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Members]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberUserid] [nvarchar](255) NOT NULL,
	[MemberFirstName] [nvarchar](255) NOT NULL,
	[MemberLastName] [nvarchar](255) NOT NULL,
	[MemberFullName] [nvarchar](255) NOT NULL,
	[MemberEmail] [nvarchar](255) NOT NULL,
	[MemberPhone] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Priorities]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priorities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PriorityDescription] [nvarchar](255) NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Team] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Priorities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestTypes]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestTypeDescription] [nvarchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[Active] [bit] NULL,
	[Team] [int] NOT NULL,
 CONSTRAINT [PK_dbo.RequestTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestTypeSteps]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestTypeSteps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StepDescription] [nvarchar](255) NOT NULL,
	[StepNumber] [int] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[Active] [bit] NULL,
	[RequestType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.RequestTypeSteps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceRequestNotes]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceRequestNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoteDescription] [nvarchar](255) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[WrittenBy] [nvarchar](255) NULL,
	[ServiceRequest] [int] NULL,
 CONSTRAINT [PK_ServiceRequestNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceRequests]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestHeading] [nvarchar](255) NOT NULL,
	[RequestDescription] [nvarchar](max) NOT NULL,
	[RequestorID] [nvarchar](255) NOT NULL,
	[RequestorFirstName] [nvarchar](255) NULL,
	[RequestorLastName] [nvarchar](255) NULL,
	[RequestorPhone] [nvarchar](255) NULL,
	[RequestorEmail] [nvarchar](255) NOT NULL,
	[DateTimeSubmitted] [datetime] NULL,
	[DateTimeStarted] [datetime] NULL,
	[DateTimeCompleted] [datetime] NULL,
	[Priority] [int] NULL,
	[RequestType] [int] NULL,
	[RequestTypeStep] [int] NULL,
	[Member] [int] NULL,
	[Status] [int] NOT NULL,
	[Team] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.ServiceRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusSets]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusSets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusDescription] [nvarchar](255) NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[StatusType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.StatusSets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusTypes]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusTypeDescription] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.StatusTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StepHistories]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StepHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[RequestTypeStep] [int] NOT NULL,
	[ServiceRequest] [int] NULL,
 CONSTRAINT [PK_StepHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TeamAssignmentHistories]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamAssignmentHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssignedBy] [nvarchar](255) NOT NULL,
	[DateAssigned] [datetime] NOT NULL,
	[ServiceRequest] [int] NOT NULL,
	[Team] [int] NOT NULL,
 CONSTRAINT [PK_TeamAssignmentHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TeamMembers]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Member] [int] NOT NULL,
	[Team] [int] NOT NULL,
 CONSTRAINT [PK_TeamMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Teams]    Script Date: 8/27/2016 1:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeamDescription] [nvarchar](255) NOT NULL,
	[TeamEmailAddress] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ApplicConfs] ON 

INSERT [dbo].[ApplicConfs] ([Id], [FileSystemUpload], [ADActive], [EmailConfirmation], [ModifiedBy], [Modified], [AppAdmin], [BackAdmin], [LDAPConn], [LDAPPath], [ManageHREmail], [ManageHREmailPass], [SMTPHost], [SMTPPort], [EnableSSL]) VALUES (1, 0, 0, 0, NULL, NULL, N'Domain\Name',N'Domain\Name', N'Server.Domain.local',N'LDAP://Server.Domain.local', N'AppEmailAddress', N'AppEmailPassword', N'smtp.gmail.com', 587, 1)
SET IDENTITY_INSERT [dbo].[ApplicConfs] OFF
SET IDENTITY_INSERT [dbo].[Members] ON 

INSERT [dbo].[Members] ([Id], [MemberUserid], [MemberFirstName], [MemberLastName], [MemberFullName], [MemberEmail], [MemberPhone]) VALUES (1, N'UnAssigned', N'UnAssigned', N'UnAssigned', N'UnAssigned', N'Unassigned@Unassigned.com', N'000 000 0000')
SET IDENTITY_INSERT [dbo].[Members] OFF
SET IDENTITY_INSERT [dbo].[Priorities] ON 

INSERT [dbo].[Priorities] ([Id], [PriorityDescription], [LastUpdated], [Active], [Team]) VALUES (1, N'--UnAssigned--', CAST(N'2016-08-27T13:48:49.467' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Priorities] OFF
SET IDENTITY_INSERT [dbo].[RequestTypes] ON 

INSERT [dbo].[RequestTypes] ([Id], [RequestTypeDescription], [LastUpdated], [Active], [Team]) VALUES (1, N'--UnAssigned--', CAST(N'2016-08-27T13:48:49.467' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[RequestTypes] OFF
SET IDENTITY_INSERT [dbo].[RequestTypeSteps] ON 

INSERT [dbo].[RequestTypeSteps] ([Id], [StepDescription], [StepNumber], [LastUpdated], [Active], [RequestType]) VALUES (1, N'--Start--', 1, CAST(N'2016-08-27T13:48:49.477' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[RequestTypeSteps] OFF
SET IDENTITY_INSERT [dbo].[StatusSets] ON 

INSERT [dbo].[StatusSets] ([Id], [StatusDescription], [LastUpdated], [Active], [StatusType]) VALUES (1, N'Not Started', CAST(N'2016-08-27T13:48:49.440' AS DateTime), 1, 1)
INSERT [dbo].[StatusSets] ([Id], [StatusDescription], [LastUpdated], [Active], [StatusType]) VALUES (2, N'In Progress', CAST(N'2016-08-27T13:48:49.443' AS DateTime), 1, 1)
INSERT [dbo].[StatusSets] ([Id], [StatusDescription], [LastUpdated], [Active], [StatusType]) VALUES (3, N'Completed', CAST(N'2016-08-27T13:48:49.443' AS DateTime), 1, 2)
SET IDENTITY_INSERT [dbo].[StatusSets] OFF
SET IDENTITY_INSERT [dbo].[StatusTypes] ON 

INSERT [dbo].[StatusTypes] ([Id], [StatusTypeDescription]) VALUES (1, N'Open')
INSERT [dbo].[StatusTypes] ([Id], [StatusTypeDescription]) VALUES (2, N'Closed')
SET IDENTITY_INSERT [dbo].[StatusTypes] OFF
SET IDENTITY_INSERT [dbo].[TeamMembers] ON 

INSERT [dbo].[TeamMembers] ([Id], [Member], [Team]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[TeamMembers] OFF
SET IDENTITY_INSERT [dbo].[Teams] ON 

INSERT [dbo].[Teams] ([Id], [TeamDescription], [TeamEmailAddress]) VALUES (1, N'--UnAssigned--', N'unassigned@unassigned.com')
SET IDENTITY_INSERT [dbo].[Teams] OFF
ALTER TABLE [dbo].[FileDetails]  WITH CHECK ADD  CONSTRAINT [FK_FileDetail_ServiceRequests] FOREIGN KEY([ServiceRequestID])
REFERENCES [dbo].[ServiceRequests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileDetails] CHECK CONSTRAINT [FK_FileDetail_ServiceRequests]
GO
ALTER TABLE [dbo].[IndividualAssignmentHistories]  WITH CHECK ADD  CONSTRAINT [FK_dbo_IndividualAssignmentHistories_dbo_ServiceRequests_ServiceRequest] FOREIGN KEY([ServiceRequest])
REFERENCES [dbo].[ServiceRequests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IndividualAssignmentHistories] CHECK CONSTRAINT [FK_dbo_IndividualAssignmentHistories_dbo_ServiceRequests_ServiceRequest]
GO
ALTER TABLE [dbo].[IndividualAssignmentHistories]  WITH CHECK ADD  CONSTRAINT [FK_IndividualAssignmentHistories_Members] FOREIGN KEY([AssignedTo])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[IndividualAssignmentHistories] CHECK CONSTRAINT [FK_IndividualAssignmentHistories_Members]
GO
ALTER TABLE [dbo].[Priorities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Priorities_dbo.Teams_Team] FOREIGN KEY([Team])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[Priorities] CHECK CONSTRAINT [FK_dbo.Priorities_dbo.Teams_Team]
GO
ALTER TABLE [dbo].[Priorities]  WITH CHECK ADD  CONSTRAINT [FK_dbo_Priorities_dbo_Teams_Team] FOREIGN KEY([Team])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[Priorities] CHECK CONSTRAINT [FK_dbo_Priorities_dbo_Teams_Team]
GO
ALTER TABLE [dbo].[RequestTypes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RequestTypes_dbo.Teams_Team] FOREIGN KEY([Team])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[RequestTypes] CHECK CONSTRAINT [FK_dbo.RequestTypes_dbo.Teams_Team]
GO
ALTER TABLE [dbo].[RequestTypes]  WITH CHECK ADD  CONSTRAINT [FK_dbo_RequestTypes_dbo_Teams_Team] FOREIGN KEY([Team])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[RequestTypes] CHECK CONSTRAINT [FK_dbo_RequestTypes_dbo_Teams_Team]
GO
ALTER TABLE [dbo].[RequestTypeSteps]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RequestTypeSteps_dbo.RequestTypes_RequestType] FOREIGN KEY([RequestType])
REFERENCES [dbo].[RequestTypes] ([Id])
GO
ALTER TABLE [dbo].[RequestTypeSteps] CHECK CONSTRAINT [FK_dbo.RequestTypeSteps_dbo.RequestTypes_RequestType]
GO
ALTER TABLE [dbo].[RequestTypeSteps]  WITH CHECK ADD  CONSTRAINT [FK_dbo_RequestTypeSteps_dbo_RequestTypes_RequestType] FOREIGN KEY([RequestType])
REFERENCES [dbo].[RequestTypes] ([Id])
GO
ALTER TABLE [dbo].[RequestTypeSteps] CHECK CONSTRAINT [FK_dbo_RequestTypeSteps_dbo_RequestTypes_RequestType]
GO
ALTER TABLE [dbo].[ServiceRequestNotes]  WITH CHECK ADD  CONSTRAINT [FK_ServiceRequestNotes_ServiceRequests] FOREIGN KEY([ServiceRequest])
REFERENCES [dbo].[ServiceRequests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServiceRequestNotes] CHECK CONSTRAINT [FK_ServiceRequestNotes_ServiceRequests]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequests_dbo.Members_Member] FOREIGN KEY([Member])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo.ServiceRequests_dbo.Members_Member]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequests_dbo.Priorities_Priority] FOREIGN KEY([Priority])
REFERENCES [dbo].[Priorities] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo.ServiceRequests_dbo.Priorities_Priority]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequests_dbo.RequestTypes_RequestType] FOREIGN KEY([RequestType])
REFERENCES [dbo].[RequestTypes] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo.ServiceRequests_dbo.RequestTypes_RequestType]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequests_dbo.RequestTypeSteps_RequestTypeStep] FOREIGN KEY([RequestTypeStep])
REFERENCES [dbo].[RequestTypeSteps] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo.ServiceRequests_dbo.RequestTypeSteps_RequestTypeStep]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequests_dbo.StatusSets_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[StatusSets] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo.ServiceRequests_dbo.StatusSets_Status]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequests_dbo.Teams_Team] FOREIGN KEY([Team])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo.ServiceRequests_dbo.Teams_Team]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo_ServiceRequests_dbo_Members_Member] FOREIGN KEY([Member])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo_ServiceRequests_dbo_Members_Member]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo_ServiceRequests_dbo_Priorities_Priority] FOREIGN KEY([Priority])
REFERENCES [dbo].[Priorities] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo_ServiceRequests_dbo_Priorities_Priority]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo_ServiceRequests_dbo_RequestTypes_RequestType] FOREIGN KEY([RequestType])
REFERENCES [dbo].[RequestTypes] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo_ServiceRequests_dbo_RequestTypes_RequestType]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo_ServiceRequests_dbo_RequestTypeSteps_RequestTypeStep] FOREIGN KEY([RequestTypeStep])
REFERENCES [dbo].[RequestTypeSteps] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo_ServiceRequests_dbo_RequestTypeSteps_RequestTypeStep]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo_ServiceRequests_dbo_StatusSets_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[StatusSets] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo_ServiceRequests_dbo_StatusSets_Status]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo_ServiceRequests_dbo_Teams_Team] FOREIGN KEY([Team])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_dbo_ServiceRequests_dbo_Teams_Team]
GO
ALTER TABLE [dbo].[StatusSets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StatusSets_dbo.StatusTypes_StatusType] FOREIGN KEY([StatusType])
REFERENCES [dbo].[StatusTypes] ([Id])
GO
ALTER TABLE [dbo].[StatusSets] CHECK CONSTRAINT [FK_dbo.StatusSets_dbo.StatusTypes_StatusType]
GO
ALTER TABLE [dbo].[StatusSets]  WITH CHECK ADD  CONSTRAINT [FK_dbo_StatusSets_dbo_StatusTypes_StatusType] FOREIGN KEY([StatusType])
REFERENCES [dbo].[StatusTypes] ([Id])
GO
ALTER TABLE [dbo].[StatusSets] CHECK CONSTRAINT [FK_dbo_StatusSets_dbo_StatusTypes_StatusType]
GO
ALTER TABLE [dbo].[StepHistories]  WITH CHECK ADD  CONSTRAINT [FK_dbo_StepHistories_dbo_RequestTypeSteps_RequestTypeStep] FOREIGN KEY([RequestTypeStep])
REFERENCES [dbo].[RequestTypeSteps] ([Id])
GO
ALTER TABLE [dbo].[StepHistories] CHECK CONSTRAINT [FK_dbo_StepHistories_dbo_RequestTypeSteps_RequestTypeStep]
GO
ALTER TABLE [dbo].[StepHistories]  WITH CHECK ADD  CONSTRAINT [FK_dbo_StepHistories_dbo_ServiceRequests_ServiceRequest] FOREIGN KEY([ServiceRequest])
REFERENCES [dbo].[ServiceRequests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StepHistories] CHECK CONSTRAINT [FK_dbo_StepHistories_dbo_ServiceRequests_ServiceRequest]
GO
ALTER TABLE [dbo].[TeamAssignmentHistories]  WITH CHECK ADD  CONSTRAINT [FK_dbo_TeamAssignmentHistories_dbo_ServiceRequests_ServiceRequest] FOREIGN KEY([ServiceRequest])
REFERENCES [dbo].[ServiceRequests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TeamAssignmentHistories] CHECK CONSTRAINT [FK_dbo_TeamAssignmentHistories_dbo_ServiceRequests_ServiceRequest]
GO
ALTER TABLE [dbo].[TeamAssignmentHistories]  WITH CHECK ADD  CONSTRAINT [FK_dbo_TeamAssignmentHistories_dbo_Teams_Team] FOREIGN KEY([Team])
REFERENCES [dbo].[Teams] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TeamAssignmentHistories] CHECK CONSTRAINT [FK_dbo_TeamAssignmentHistories_dbo_Teams_Team]
GO
ALTER TABLE [dbo].[TeamMembers]  WITH CHECK ADD  CONSTRAINT [FK_dbo_TeamMembers_dbo_Members_Member] FOREIGN KEY([Member])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[TeamMembers] CHECK CONSTRAINT [FK_dbo_TeamMembers_dbo_Members_Member]
GO
ALTER TABLE [dbo].[TeamMembers]  WITH CHECK ADD  CONSTRAINT [FK_dbo_TeamMembers_dbo_Teams_Team] FOREIGN KEY([Team])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[TeamMembers] CHECK CONSTRAINT [FK_dbo_TeamMembers_dbo_Teams_Team]
GO
USE [master]
GO
ALTER DATABASE [ServeMeHR] SET  READ_WRITE 
GO
