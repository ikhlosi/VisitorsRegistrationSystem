USE [master]
GO

/****** Object:  Database [VisitorsRegistrationSystem]    Script Date: 27/10/2022 10:35:22 ******/
CREATE DATABASE [VisitorsRegistrationSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VisitorsRegistrationSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\VisitorsRegistrationSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VisitorsRegistrationSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\VisitorsRegistrationSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VisitorsRegistrationSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET ARITHABORT OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET  DISABLE_BROKER 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET  MULTI_USER 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET DB_CHAINING OFF 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET QUERY_STORE = OFF
GO

ALTER DATABASE [VisitorsRegistrationSystem] SET  READ_WRITE 
GO

