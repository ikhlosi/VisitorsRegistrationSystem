USE [VisitorsRegistrationSystem]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 27/10/2022 10:47:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[street] [varchar](250) NOT NULL,
	[houseNr] [varchar](250) NOT NULL,
	[bus] [varchar](250) NULL,
	[postalCode] [varchar](250) NOT NULL,
	[city] [varchar](250) NOT NULL,
	[country] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [VisitorsRegistrationSystem]
GO

/****** Object:  Table [dbo].[Company]    Script Date: 27/10/2022 10:52:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Company](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](250) NOT NULL,
	[VAT] [varchar](250) NOT NULL,
	[email] [varchar](250) NOT NULL,
	[telNr] [varchar](250) NULL,
	[addressId] [int] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [VisitorsRegistrationSystem]
GO

/****** Object:  Table [dbo].[Employee]    Script Date: 27/10/2022 10:52:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](250) NOT NULL,
	[lastName] [varchar](250) NOT NULL,
	[email] [varchar](250) NULL,
	[occupation] [varchar](250) NOT NULL,
	[companyId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [VisitorsRegistrationSystem]
GO

/****** Object:  Table [dbo].[Visit]    Script Date: 27/10/2022 10:52:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Visit](
	[visitorId] [int] NOT NULL,
	[startTime] [datetime2](7) NOT NULL,
	[endTime] [datetime2](7) NOT NULL,
	[companyId] [int] NOT NULL,
	[employeeId] [int] NOT NULL,
 CONSTRAINT [PK_Visit] PRIMARY KEY CLUSTERED 
(
	[visitorId] ASC,
	[startTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




USE [VisitorsRegistrationSystem]
GO

/****** Object:  Table [dbo].[Visitor]    Script Date: 27/10/2022 10:53:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Visitor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](250) NOT NULL,
	[email] [varchar](250) NOT NULL,
	[visitorCompany] [varchar](250) NULL,
 CONSTRAINT [PK_Visitor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Address] FOREIGN KEY([addressId])
REFERENCES [dbo].[Address] ([id])
GO

ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Address]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([id])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Company]
GO


ALTER TABLE [dbo].[Visit]  WITH CHECK ADD  CONSTRAINT [FK_Visit_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([id])
GO

ALTER TABLE [dbo].[Visit] CHECK CONSTRAINT [FK_Visit_Company]
GO

ALTER TABLE [dbo].[Visit]  WITH CHECK ADD  CONSTRAINT [FK_Visit_Employee] FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([id])
GO

ALTER TABLE [dbo].[Visit] CHECK CONSTRAINT [FK_Visit_Employee]
GO

ALTER TABLE [dbo].[Visit]  WITH CHECK ADD  CONSTRAINT [FK_Visit_Visitor] FOREIGN KEY([visitorId])
REFERENCES [dbo].[Visitor] ([id])
GO

ALTER TABLE [dbo].[Visit] CHECK CONSTRAINT [FK_Visit_Visitor]
GO