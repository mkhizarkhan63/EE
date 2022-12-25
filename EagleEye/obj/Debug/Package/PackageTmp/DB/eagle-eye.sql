USE [master]
GO
/****** Object:  Database [EagleEye]    Script Date: 10/5/2022 11:26:10 AM ******/
CREATE DATABASE [EagleEye]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EagleEye', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\EagleEye.mdf' , SIZE = 922624KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EagleEye_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\EagleEye_log.ldf' , SIZE = 1964480KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EagleEye].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EagleEye] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EagleEye] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EagleEye] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EagleEye] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EagleEye] SET ARITHABORT OFF 
GO
ALTER DATABASE [EagleEye] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EagleEye] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EagleEye] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EagleEye] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EagleEye] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EagleEye] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EagleEye] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EagleEye] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EagleEye] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EagleEye] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EagleEye] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EagleEye] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EagleEye] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EagleEye] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EagleEye] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EagleEye] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EagleEye] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EagleEye] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EagleEye] SET  MULTI_USER 
GO
ALTER DATABASE [EagleEye] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EagleEye] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EagleEye] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EagleEye] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [EagleEye] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EagleEye] SET QUERY_STORE = OFF
GO
USE [EagleEye]
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
USE [EagleEye]
GO
/****** Object:  Table [dbo].[tbl_device]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_device](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Device_ID] [varchar](50) NULL,
	[Device_Name] [varchar](50) NULL,
	[Device_Type] [varchar](50) NULL,
	[Device_Info] [varchar](max) NULL,
	[Face_Data_Ver] [varchar](10) NULL,
	[Firmware] [varchar](250) NULL,
	[Firmware_Filename] [varchar](max) NULL,
	[Fk_Bin_Data_Lib] [varchar](25) NULL,
	[Fp_Data_Ver] [varchar](10) NULL,
	[Supported_Enroll_Data] [varchar](50) NULL,
	[Device_Status_Info] [varchar](max) NULL,
	[Max_Record] [varchar](10) NULL,
	[Real_FaceReg] [varchar](50) NULL,
	[Max_FaceReg] [varchar](50) NULL,
	[Real_FPReg] [varchar](50) NULL,
	[Max_FPReg] [varchar](50) NULL,
	[Real_IDCardReg] [varchar](50) NULL,
	[Max_IDCardReg] [varchar](50) NULL,
	[Real_Manager] [varchar](50) NULL,
	[Max_Manager] [varchar](50) NULL,
	[Real_PasswordReg] [varchar](50) NULL,
	[Max_PasswordReg] [varchar](50) NULL,
	[Real_PvReg] [varchar](10) NULL,
	[Max_PvReg] [varchar](10) NULL,
	[Total_log_Count] [varchar](10) NULL,
	[Total_log_Max] [varchar](10) NULL,
	[Real_Employee] [varchar](10) NULL,
	[Max_Employee] [varchar](10) NULL,
	[Device_LastStatusTime] [datetime] NULL,
	[Alarm_Delay] [varchar](10) NULL,
	[Allow_EarlyTime] [varchar](10) NULL,
	[Allow_LateTime] [varchar](10) NULL,
	[Anti-back] [varchar](10) NULL,
	[DoorMagnetic_Delay] [varchar](10) NULL,
	[DoorMagnetic_Type] [varchar](10) NULL,
	[Glog_Warning] [varchar](10) NULL,
	[OpenDoor_Delay] [varchar](10) NULL,
	[Receive_Interval] [varchar](10) NULL,
	[Reverify_Time] [varchar](10) NULL,
	[Show_ResultTime] [varchar](10) NULL,
	[Screensavers_Time] [varchar](10) NULL,
	[Sleep_Time] [varchar](10) NULL,
	[Use_Alarm] [varchar](10) NULL,
	[Volume] [varchar](10) NULL,
	[Wiegand_Input] [varchar](10) NULL,
	[Wiegand_Output] [varchar](10) NULL,
	[Wiegand_Type] [varchar](10) NULL,
	[Multi_Users] [varchar](10) NULL,
	[Device_Group] [int] NULL,
	[Server_Address] [varchar](20) NULL,
	[Server_Port] [varchar](20) NULL,
	[Sys_Time] [datetime] NULL,
	[Device_Status] [int] NULL,
	[Reader_ID] [int] NULL,
	[Active] [bit] NOT NULL,
	[Last_Polled_Record] [datetime] NULL,
	[Device_Location] [varchar](50) NULL,
	[OpenDoor] [varchar](10) NULL,
	[IsSlave] [bit] NULL,
 CONSTRAINT [PK_tbl_device] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_employee]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_employee](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [varchar](50) NULL,
	[Employee_Name] [varchar](150) NULL,
	[Employee_Photo] [varchar](max) NULL,
	[Card_No] [varchar](50) NULL,
	[User_Privilege] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[FingerPrint] [bit] NULL,
	[Face] [bit] NULL,
	[Palm] [bit] NULL,
	[Password] [varchar](50) NULL,
	[Device_Id] [varchar](50) NULL,
	[fkDepartment_Code] [int] NULL,
	[fkLocation_Code] [int] NULL,
	[fkDesignation_Code] [int] NULL,
	[fkEmployeeType_Code] [int] NULL,
	[Email] [varchar](50) NULL,
	[Address] [varchar](max) NULL,
	[Active] [int] NULL,
	[Telephone] [varchar](50) NULL,
	[Trans_Id] [varchar](50) NULL,
	[Update_Date] [datetime] NULL,
	[Cmd_Param] [varbinary](max) NULL,
	[finger_0] [varbinary](max) NULL,
	[finger_1] [varbinary](max) NULL,
	[finger_2] [varbinary](max) NULL,
	[finger_3] [varbinary](max) NULL,
	[finger_4] [varbinary](max) NULL,
	[finger_5] [varbinary](max) NULL,
	[finger_6] [varbinary](max) NULL,
	[finger_7] [varbinary](max) NULL,
	[finger_8] [varbinary](max) NULL,
	[finger_9] [varbinary](max) NULL,
	[face_data] [varbinary](max) NULL,
	[palm_0] [varbinary](max) NULL,
	[palm_1] [varbinary](max) NULL,
	[photo_data] [varbinary](max) NULL,
	[Valid_DateStart] [varchar](50) NULL,
	[Valid_DateEnd] [varchar](50) NULL,
	[Sunday] [varchar](25) NULL,
	[Monday] [varchar](25) NULL,
	[Tuesday] [varchar](25) NULL,
	[Wednesday] [varchar](25) NULL,
	[Thursday] [varchar](25) NULL,
	[Friday] [varchar](25) NULL,
	[Saturday] [varchar](25) NULL,
	[IsDelete] [bit] NULL,
	[WorkHourPolicyCode] [int] NULL,
 CONSTRAINT [PK_tbl_employee] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[employee_loaction]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[employee_loaction]as
select tbl_employee.Employee_ID,tbl_employee.Employee_Name,tbl_employee.Device_Id,tbl_device.Device_Name
from tbl_employee
inner join tbl_device
ON tbl_employee.Device_Id=tbl_device.Device_ID
GO
/****** Object:  Table [dbo].[tbl_attendence]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_attendence](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Attendance_DateTime] [datetime] NULL,
	[Attendance_Photo] [varchar](max) NULL,
	[Polling_DateTime] [datetime] NULL,
	[Device_ID] [varchar](250) NULL,
	[Employee_ID] [varchar](50) NULL,
	[Employee_Name] [varchar](150) NULL,
	[Status] [varchar](250) NULL,
	[Status_Description] [varchar](250) NULL,
	[WorkCode] [varchar](25) NULL,
	[WorkCode_Description] [varchar](250) NULL,
	[DoorStatus] [varchar](100) NULL,
	[Verify_Mode] [varchar](250) NULL,
	[Status_TIS] [bit] NULL,
	[Status_Oracle] [bit] NULL,
	[Status_SQL] [bit] NULL,
	[Ext_Status] [varchar](50) NULL,
	[Status_MySQL] [bit] NULL,
 CONSTRAINT [PK_tbl_attendence] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[emp_attendance_location]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view  [dbo].[emp_attendance_location] AS
select Attendance_DateTime,Employee_ID,Employee_Name,device_name from tbl_attendence

inner join tbl_device
on 
tbl_attendence.Device_ID=tbl_device.Device_ID

GO
/****** Object:  Table [dbo].[tbl_account]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_account](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Hash] [varchar](max) NULL,
	[Salt] [varbinary](max) NULL,
	[LastLogin] [datetime] NULL,
	[Location] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_account] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_att_status]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_att_status](
	[Code] [int] NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_att_status] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_awaitingdevice]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_awaitingdevice](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Device_ID] [varchar](50) NULL,
	[Device_Name] [varchar](50) NULL,
	[Device_Info] [varchar](max) NULL,
	[Device_Status_Info] [varchar](max) NULL,
	[Device_Type] [varchar](25) NULL,
	[IsConnected] [bit] NULL,
 CONSTRAINT [PK_tbl_awaitingdevice] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_communication]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_communication](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Server_IP] [varchar](50) NULL,
	[SignalR_Port] [varchar](50) NULL,
	[Server_Port] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_communication] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_department]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_department](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](250) NULL,
 CONSTRAINT [PK_tbl_department] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_designation]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_designation](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](250) NULL,
 CONSTRAINT [PK_tbl_designation] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_employeetype]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_employeetype](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](250) NULL,
 CONSTRAINT [PK_tbl_employeetype] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_exceptionlog]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_exceptionlog](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Exception_Layer] [varchar](250) NULL,
	[Method] [varchar](max) NULL,
	[Stacktrace] [varchar](max) NULL,
	[Error_Message] [varchar](max) NULL,
	[Form] [varchar](250) NULL,
	[Exception_DateTime] [datetime] NULL,
 CONSTRAINT [PK_tbl_exceptionlog] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_expiredusers]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_expiredusers](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [varchar](50) NULL,
	[Employee_Name] [varchar](150) NULL,
	[Employee_Photo] [varchar](max) NULL,
	[Card_No] [varchar](50) NULL,
	[User_Privilege] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[FingerPrint] [bit] NULL,
	[Face] [bit] NULL,
	[Palm] [bit] NULL,
	[Password] [varchar](50) NULL,
	[Device_Id] [varchar](50) NULL,
	[fkDepartment_Code] [int] NULL,
	[fkLocation_Code] [int] NULL,
	[fkDesignation_Code] [int] NULL,
	[fkEmployeeType_Code] [int] NULL,
	[Email] [varchar](50) NULL,
	[Address] [varchar](max) NULL,
	[Active] [int] NULL,
	[Telephone] [varchar](50) NULL,
	[Trans_Id] [varchar](50) NULL,
	[Update_Date] [datetime] NULL,
	[Cmd_Param] [varbinary](max) NULL,
	[finger_0] [varbinary](max) NULL,
	[finger_1] [varbinary](max) NULL,
	[finger_2] [varbinary](max) NULL,
	[finger_3] [varbinary](max) NULL,
	[finger_4] [varbinary](max) NULL,
	[finger_5] [varbinary](max) NULL,
	[finger_6] [varbinary](max) NULL,
	[finger_7] [varbinary](max) NULL,
	[finger_8] [varbinary](max) NULL,
	[finger_9] [varbinary](max) NULL,
	[face_data] [varbinary](max) NULL,
	[palm_0] [varbinary](max) NULL,
	[palm_1] [varbinary](max) NULL,
	[photo_data] [varbinary](max) NULL,
	[Valid_DateStart] [varchar](50) NULL,
	[Valid_DateEnd] [varchar](50) NULL,
	[Sunday] [varchar](25) NULL,
	[Monday] [varchar](25) NULL,
	[Tuesday] [varchar](25) NULL,
	[Wednesday] [varchar](25) NULL,
	[Thursday] [varchar](25) NULL,
	[Friday] [varchar](25) NULL,
	[Saturday] [varchar](25) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_tbl_expiredusers] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkcmd_trans]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkcmd_trans](
	[trans_id] [varchar](16) NOT NULL,
	[device_id] [varchar](24) NOT NULL,
	[user_id] [varchar](50) NULL,
	[timezone_no] [varchar](50) NULL,
	[cmd_code] [varchar](32) NOT NULL,
	[return_code] [varchar](64) NULL,
	[status] [varchar](16) NOT NULL,
	[update_time] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkcmd_trans_cmd_param]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkcmd_trans_cmd_param](
	[trans_id] [varchar](16) NOT NULL,
	[device_id] [varchar](24) NOT NULL,
	[cmd_param] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkcmd_trans_cmd_param_offline]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkcmd_trans_cmd_param_offline](
	[trans_id] [varchar](16) NOT NULL,
	[device_id] [varchar](24) NOT NULL,
	[cmd_param] [varbinary](max) NULL,
 CONSTRAINT [PK_tbl_fkcmd_trans_cmd_param_offline] PRIMARY KEY CLUSTERED 
(
	[trans_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkcmd_trans_cmd_result]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkcmd_trans_cmd_result](
	[trans_id] [varchar](16) NOT NULL,
	[device_id] [varchar](24) NOT NULL,
	[cmd_result] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkcmd_trans_cmd_result_log_data]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkcmd_trans_cmd_result_log_data](
	[trans_id] [nvarchar](16) NOT NULL,
	[device_id] [nvarchar](24) NOT NULL,
	[user_id] [nvarchar](64) NULL,
	[verify_mode] [nvarchar](64) NULL,
	[io_mode] [nvarchar](32) NULL,
	[io_time] [datetime] NULL,
	[io_workcode] [nchar](16) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkcmd_trans_cmd_result_slog_data]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkcmd_trans_cmd_result_slog_data](
	[trans_id] [nvarchar](16) NOT NULL,
	[device_id] [nvarchar](24) NOT NULL,
	[kind] [nvarchar](64) NULL,
	[tobackup_number] [nvarchar](64) NULL,
	[user_id] [nvarchar](64) NULL,
	[touser_id] [nvarchar](64) NULL,
	[operation_time] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkcmd_trans_cmd_result_user_id_list]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkcmd_trans_cmd_result_user_id_list](
	[code] [int] IDENTITY(1,1) NOT NULL,
	[trans_id] [nvarchar](16) NOT NULL,
	[device_id] [nvarchar](24) NOT NULL,
	[user_id] [nvarchar](64) NULL,
	[backup_number] [int] NULL,
 CONSTRAINT [PK_tbl_fkcmd_trans_cmd_result_user_id_list] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkcmd_trans_offline]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkcmd_trans_offline](
	[trans_id] [varchar](16) NOT NULL,
	[device_id] [varchar](24) NULL,
	[user_id] [varchar](50) NULL,
	[timezone_no] [varchar](50) NULL,
	[cmd_code] [varchar](32) NULL,
	[return_code] [varchar](64) NULL,
	[status] [varchar](16) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_tbl_fkcmd_trans_offline] PRIMARY KEY CLUSTERED 
(
	[trans_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_fkdevice_status]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_fkdevice_status](
	[device_id] [varchar](24) NOT NULL,
	[device_name] [varchar](24) NOT NULL,
	[connected] [int] NOT NULL,
	[last_update_time] [datetime] NOT NULL,
	[last_update_fk_time] [datetime] NULL,
	[device_info] [nvarchar](2048) NULL,
	[dev_model] [varchar](24) NULL,
	[dev_status_info] [nvarchar](1024) NULL,
	[con_status] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_irregularemployee]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_irregularemployee](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [varchar](50) NULL,
	[Employee_Name] [varchar](150) NULL,
	[Employee_Photo] [varchar](max) NULL,
	[Card_No] [varchar](50) NULL,
	[User_Privilege] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[FingerPrint] [bit] NULL,
	[Face] [bit] NULL,
	[Palm] [bit] NULL,
	[Password] [varchar](50) NULL,
	[Device_Id] [varchar](50) NULL,
	[fkDepartment_Code] [int] NULL,
	[fkLocation_Code] [int] NULL,
	[fkDesignation_Code] [int] NULL,
	[fkEmployeeType_Code] [int] NULL,
	[Email] [varchar](50) NULL,
	[Address] [varchar](max) NULL,
	[Active] [int] NULL,
	[Telephone] [varchar](50) NULL,
	[Trans_Id] [varchar](50) NULL,
	[Update_Date] [datetime] NULL,
	[Cmd_Param] [varbinary](max) NULL,
	[finger_0] [varbinary](max) NULL,
	[finger_1] [varbinary](max) NULL,
	[finger_2] [varbinary](max) NULL,
	[finger_3] [varbinary](max) NULL,
	[finger_4] [varbinary](max) NULL,
	[finger_5] [varbinary](max) NULL,
	[finger_6] [varbinary](max) NULL,
	[finger_7] [varbinary](max) NULL,
	[finger_8] [varbinary](max) NULL,
	[finger_9] [varbinary](max) NULL,
	[face_data] [varbinary](max) NULL,
	[palm_0] [varbinary](max) NULL,
	[palm_1] [varbinary](max) NULL,
	[photo_data] [varbinary](max) NULL,
	[IsDelete] [bit] NULL,
	[Msg] [varchar](250) NULL,
 CONSTRAINT [PK_tbl_irregularemployee] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_location]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_location](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](250) NOT NULL,
 CONSTRAINT [PK_tbl_location] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_menu]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_menu](
	[Menu_Id] [int] IDENTITY(1,1) NOT NULL,
	[Menu_Name] [varchar](100) NULL,
	[Menu_Controller] [varchar](100) NULL,
	[Menu_Action] [varchar](100) NULL,
	[Parent] [varchar](25) NULL,
	[Icon] [varchar](25) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tbl_meun] PRIMARY KEY CLUSTERED 
(
	[Menu_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_menurights]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_menurights](
	[Menu_Rights_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [int] NULL,
	[Menu_Id] [int] NULL,
	[Insert] [bit] NULL,
	[Update] [bit] NULL,
	[Delete] [bit] NULL,
	[View] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tbl_menurights] PRIMARY KEY CLUSTERED 
(
	[Menu_Rights_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_operationlog]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_operationlog](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Trans_ID] [varchar](25) NULL,
	[Device_ID] [varchar](25) NULL,
	[Device_Name] [varchar](50) NULL,
	[Action] [varchar](250) NULL,
	[Status] [varchar](10) NULL,
	[Message] [varchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[UserName] [varchar](50) NULL,
	[Device_Status] [int] NULL,
 CONSTRAINT [PK_tbl_operationlog] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_policyDetail]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_policyDetail](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[DayCheck] [bit] NULL,
	[Day] [varchar](10) NULL,
	[Workhour] [varchar](20) NULL,
	[Overtime] [varchar](20) NULL,
	[Breakhour] [varchar](20) NULL,
	[isOvertimeActive] [bit] NULL,
	[PolicyCode] [int] NULL,
 CONSTRAINT [PK_tbl_policyDetail] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_policyWithEmployee]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_policyWithEmployee](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Emp_id] [varchar](20) NULL,
	[Dt] [datetime] NULL,
	[Workhour] [varchar](20) NULL,
	[Overtime] [varchar](20) NULL,
	[Breakhour] [varchar](20) NULL,
	[Extrahour] [varchar](20) NULL,
	[Policycode] [varchar](20) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_tbl_policyWithEmployee] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_realtime_doorstatus]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_realtime_doorstatus](
	[update_time] [datetime] NOT NULL,
	[device_id] [varchar](24) NULL,
	[door_status] [varchar](16) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_realtime_enroll_data]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_realtime_enroll_data](
	[code] [int] IDENTITY(1,1) NOT NULL,
	[update_time] [datetime] NOT NULL,
	[device_id] [varchar](24) NOT NULL,
	[user_id] [varchar](64) NOT NULL,
	[user_data] [varbinary](max) NULL,
 CONSTRAINT [PK_tbl_realtime_enroll_data] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_realtime_glog]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_realtime_glog](
	[update_time] [datetime] NOT NULL,
	[device_id] [varchar](24) NULL,
	[user_id] [varchar](64) NULL,
	[verify_mode] [varchar](64) NULL,
	[io_mode] [varchar](32) NULL,
	[io_time] [datetime] NULL,
	[io_workcode] [nchar](16) NULL,
	[log_image] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_realtime_userinfo]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_realtime_userinfo](
	[user_id] [varchar](60) NOT NULL,
	[user_name] [varchar](50) NULL,
	[privilige] [varchar](50) NULL,
	[card] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Face] [varbinary](max) NULL,
	[photo] [varbinary](max) NULL,
	[Fp_0] [varbinary](max) NULL,
	[Fp_1] [varbinary](max) NULL,
	[Fp_2] [varbinary](max) NULL,
	[Fp_3] [varbinary](max) NULL,
	[Fp_4] [varbinary](max) NULL,
	[Fp_5] [varbinary](max) NULL,
	[Fp_6] [varbinary](max) NULL,
	[Fp_7] [varbinary](max) NULL,
	[Fp_8] [varbinary](max) NULL,
	[Fp_9] [varbinary](max) NULL,
 CONSTRAINT [PK_tbl_realtime_userinfo] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_setting]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_setting](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[AppSetting_Path] [varchar](max) NULL,
	[ServiceName] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_setting] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_temptable]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_temptable](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Device_ID] [varchar](50) NULL,
	[Employee_ID] [varchar](50) NULL,
	[Cmd] [varchar](200) NULL,
	[DateTime] [datetime] NULL,
 CONSTRAINT [PK_tbl_TempTable] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_timezone]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_timezone](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Timezone_No] [varchar](20) NULL,
	[Timezone_Name] [varchar](50) NULL,
	[Period_1_Start] [varchar](50) NULL,
	[Period_1_End] [varchar](50) NULL,
	[Period_2_Start] [varchar](50) NULL,
	[Period_2_End] [varchar](50) NULL,
	[Period_3_Start] [varchar](50) NULL,
	[Period_3_End] [varchar](50) NULL,
	[Period_4_Start] [varchar](50) NULL,
	[Period_4_End] [varchar](50) NULL,
	[Period_5_Start] [varchar](50) NULL,
	[Period_5_End] [varchar](50) NULL,
	[Period_6_Start] [varchar](50) NULL,
	[Period_6_End] [varchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_tbl_timezone] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_workcode]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_workcode](
	[Code] [int] NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_workcode] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_workHourPolicy]    Script Date: 10/5/2022 11:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_workHourPolicy](
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[PolicyName] [varchar](50) NOT NULL,
	[isActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_workHourPolicy] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[tbl_account] ON 

INSERT [dbo].[tbl_account] ([Code], [UserName], [Hash], [Salt], [LastLogin], [Location]) VALUES (1, N'admin', N'HrQbbegzOY7PwzqAxeGXz98TuE7RW8+PPRG4zLyUqxfiJ7KvEDI1ALJh3gilZueuxRa3t1mf26kVdeaPeB4dJwAAAAAAAAAAAAA=', 0x30, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_account] OFF
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (0, N'NO-STATUS')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (1, N'IN')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (2, N'OUT')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (3, N'LUNCH ')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (4, N'LUNCH OUT')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (5, N'OT IN')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (6, N'OT OUT')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (7, N'TEA IN')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (8, N'TEA OUT')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (9, N'PRAY IN')
INSERT [dbo].[tbl_att_status] ([Code], [Name]) VALUES (10, N'PRAY OUT')
SET IDENTITY_INSERT [dbo].[tbl_communication] ON 

INSERT [dbo].[tbl_communication] ([Code], [Server_IP], [SignalR_Port], [Server_Port]) VALUES (1, N'192.168.005.009', N'8878', N'6088')
SET IDENTITY_INSERT [dbo].[tbl_communication] OFF
SET IDENTITY_INSERT [dbo].[tbl_employeetype] ON 

INSERT [dbo].[tbl_employeetype] ([Code], [Description]) VALUES (1, N'Permanant')
INSERT [dbo].[tbl_employeetype] ([Code], [Description]) VALUES (2, N'Temporary')
SET IDENTITY_INSERT [dbo].[tbl_employeetype] OFF
SET IDENTITY_INSERT [dbo].[tbl_location] ON 

INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (1, N'Global')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (3, N'LEW3')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (4, N'KEW4')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (5, N'LEW5')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (6, N'KHQ')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (7, N'LEW2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (8, N'LHQ2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (9, N'LEW4')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (10, N'KEW8')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (11, N'KEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (12, N'KEW3')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (13, N'IMSW')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (14, N'LEW7')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (15, N'KEW2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (16, N'LMSW')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (17, N'KMSW')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (18, N'LEW6')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (19, N'KEW6')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (20, N'KEW5')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (21, N'IEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (22, N'IEW3')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (23, N'GEW2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (24, N'IEW2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (25, N'FEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (26, N'LEW8')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (27, N'IEW4')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (28, N'GEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (29, N'SEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (30, N'FEW2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (31, N'KEW7')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (32, N'IHQ')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (33, N'HEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (34, N'IEW6-M')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (35, N'PEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (36, N'IEW6')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (37, N'IEW5')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (38, N'LEW11')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (39, N'KEW12')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (40, N'KEW13')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (41, N'LPHMC')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (42, N'LEW7-M')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (43, N'LEW9')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (44, N'KEW9')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (45, N'PEW2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (46, N'KEW10')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (47, N'KEW12-M')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (48, N'IMEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (49, N'IEW2-M')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (50, N'LEW8-M')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (51, N'LEW10')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (52, N'LMEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (53, N'LMEW2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (54, N'LMEW3')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (55, N'LMEW4')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (56, N'KPHMC')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (57, N'KEW11')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (58, N'KEW14')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (59, N'LEW12')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (60, N'LPHMC-ACS')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (61, N'LEW13')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (62, N'FEW3')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (63, N'IEW10')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (64, N'KEW15')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (65, N'LMSW-Sec Room IN')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (66, N'LMSW-Sec Room OUT')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (67, N'LEW1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (68, N'IEW9')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (69, N'LEW16')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (70, N'LEW17')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (71, N'LEW23')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (72, N'SEW2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (73, N'GEW3')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (74, N'FEW4')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (75, N'IEW11')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (76, N'KEW16')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (77, N'KEW18')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (78, N'IEW16')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (79, N'IEW17')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (80, N'IEW13')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (81, N'IEW14')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (82, N'IEW15')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (83, N'IEW8')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (84, N'IEW12')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (85, N'IEW7')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (86, N'LRTC-1')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (87, N'LRTC-2')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (88, N'LEW15')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (89, N'LEW18')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (90, N'FEW5')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (91, N'KHQ-DayCare')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (92, N'LHQ2 Back Door')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (93, N'LHQ1 Main Entrance')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (94, N'LHQ1 Back Door')
INSERT [dbo].[tbl_location] ([Code], [Description]) VALUES (95, N'LHQ1 Finance Office')
SET IDENTITY_INSERT [dbo].[tbl_location] OFF
SET IDENTITY_INSERT [dbo].[tbl_menu] ON 

INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (1, N'MultiView', N'Home', N'MultiView', NULL, N'icon-grid', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (2, N'Device Management', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (3, N'Device', N'Device', N'Index', N'2', N'icon-layers', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (4, N'Monitoring', N'Monitoring', N'Index', N'2', N'icon-clipboard', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (5, N'Polling Data', N'Polling', N'Index', N'2', N'icon-chevrons-down', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (6, N'User Management', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (7, N'Users', N'User', N'Index', N'6', N'icon-users', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (8, N'Irregular Users', N'IrregularUser', N'Index', N'6', N'icon-alert-triangle', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (9, N'Administrators', NULL, N'Index', N'6', N'icon-shield', 0)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (10, N'Software Settings', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (11, N'Accounts', N'Accounts', N'Index', N'10', N'icon-user-check', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (12, N'Settings', N'Settings', N'Index', N'10', N'icon-settings', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (13, N'EagleEye Service', N'WinService', N'Index', N'10', N'icon-command', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (14, N'Port', N'Port', N'Index', N'10', N'icon-share-2', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (15, N'Daily Attendence Report', N'Report', N'Index', N'2', N'icon-list', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (16, N'Download List', N'List', N'Index', N'10', N'icon-download', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (17, N'Individual Report', N'Report', N'Single', N'2', N'icon-file', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (18, N'Time Zone', N'TimeZone', N'Index', N'10', N'icon-clock', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (19, N'TimeZones Broadcast Status', N'TimeZonesBroadcast', N'Index', N'10', N'icon-cloud-off', 0)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (20, N'Operation Log', N'OperationLog', N'Index', N'10', N'icon-disc', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (21, N'Work Hour Policy', N'WorkHourPolicy', N'Index', N'2', N'icon-watch', 1)
INSERT [dbo].[tbl_menu] ([Menu_Id], [Menu_Name], [Menu_Controller], [Menu_Action], [Parent], [Icon], [IsActive]) VALUES (22, N'Work Hour Report', N'PolicyReport', N'Index', N'2', N'feather icon-file-text', 1)
SET IDENTITY_INSERT [dbo].[tbl_menu] OFF
SET IDENTITY_INSERT [dbo].[tbl_menurights] ON 

INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1115, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1116, 1, 3, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1117, 1, 4, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1118, 1, 5, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1119, 1, 7, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1120, 1, 8, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1121, 1, 11, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1122, 1, 12, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1123, 1, 13, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1124, 1, 14, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1125, 1, 15, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1126, 1, 16, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (1127, 1, 17, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (2180, 1, 18, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (2181, 1, 19, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (2182, 1, 20, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (2690, 1, 21, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_menurights] ([Menu_Rights_Id], [User_Id], [Menu_Id], [Insert], [Update], [Delete], [View], [IsActive]) VALUES (2691, 1, 22, 1, 1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[tbl_menurights] OFF
SET IDENTITY_INSERT [dbo].[tbl_setting] ON 

INSERT [dbo].[tbl_setting] ([Code], [AppSetting_Path], [ServiceName]) VALUES (1, N'H:\KHIZAR - Development\EagleEye Standard_workhour\EagleEye\AppSettings.xml', N'EagleEye Service')
SET IDENTITY_INSERT [dbo].[tbl_setting] OFF
SET IDENTITY_INSERT [dbo].[tbl_timezone] ON 

INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (1, N'1', N'TZ1', N'00:00', N'23:59', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 1)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (2, N'2', N'TZ2', N'16:00', N'16:30', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 1)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (3, N'3', N'TZ3', N'14:00', N'16:00', N'16:01', N'18:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 1)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (4, N'4', N'TZ4', N'05:05', N'17:05', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 1)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (5, N'5', N'TZ5', N'02:02', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 1)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (6, N'6', N'TZ6', N'01:00', N'02:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 1)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (7, N'7', N'TZ7', N'14:00', N'01:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 1)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (8, N'8', N'TZ8', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (9, N'9', N'TZ9', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (10, N'10', N'TZ10', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (11, N'11', N'TZ11', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (12, N'12', N'TZ12', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (13, N'13', N'TZ13', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (14, N'14', N'TZ14', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (15, N'15', N'TZ15', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (16, N'16', N'TZ16', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (17, N'17', N'TZ17', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (18, N'18', N'TZ18', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (19, N'19', N'TZ19', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (20, N'20', N'TZ20', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (21, N'21', N'TZ21', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (22, N'22', N'TZ22', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (23, N'23', N'TZ23', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (24, N'24', N'TZ24', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (25, N'25', N'TZ25', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (26, N'26', N'TZ26', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (27, N'27', N'TZ27', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (28, N'28', N'TZ28', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (29, N'29', N'TZ29', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (30, N'30', N'TZ30', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (31, N'31', N'TZ31', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (32, N'32', N'TZ32', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (33, N'33', N'TZ33', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (34, N'34', N'TZ34', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (35, N'35', N'TZ35', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (36, N'36', N'TZ36', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (37, N'37', N'TZ37', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (38, N'38', N'TZ38', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (39, N'39', N'TZ39', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (40, N'40', N'TZ40', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (41, N'41', N'TZ41', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (42, N'42', N'TZ42', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (43, N'43', N'TZ43', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (44, N'44', N'TZ44', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (45, N'45', N'TZ45', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (46, N'46', N'TZ46', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (47, N'47', N'TZ47', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (48, N'48', N'TZ48', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (49, N'49', N'TZ49', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (50, N'50', N'TZ50', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (51, N'51', N'TZ51', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (52, N'52', N'TZ52', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (53, N'53', N'TZ53', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (54, N'54', N'TZ54', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (55, N'55', N'TZ55', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (56, N'56', N'TZ56', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (57, N'57', N'TZ57', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (58, N'58', N'TZ58', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (59, N'59', N'TZ59', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (60, N'60', N'TZ60', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (61, N'61', N'TZ61', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (62, N'62', N'TZ62', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (63, N'63', N'TZ63', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (64, N'64', N'TZ64', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (65, N'65', N'TZ65', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (66, N'66', N'TZ66', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (67, N'67', N'TZ67', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (68, N'68', N'TZ68', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (69, N'69', N'TZ69', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (70, N'70', N'TZ70', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (71, N'71', N'TZ71', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (72, N'72', N'TZ72', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (73, N'73', N'TZ73', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (74, N'74', N'TZ74', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (75, N'75', N'TZ75', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (76, N'76', N'TZ76', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (77, N'77', N'TZ77', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (78, N'78', N'TZ78', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (79, N'79', N'TZ79', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (80, N'80', N'TZ80', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (81, N'81', N'TZ81', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (82, N'82', N'TZ82', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (83, N'83', N'TZ83', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (84, N'84', N'TZ84', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (85, N'85', N'TZ85', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (86, N'86', N'TZ86', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (87, N'87', N'TZ87', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (88, N'88', N'TZ88', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (89, N'89', N'TZ89', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (90, N'90', N'TZ90', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (91, N'91', N'TZ91', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (92, N'92', N'TZ92', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (93, N'93', N'TZ93', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (94, N'94', N'TZ94', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (95, N'95', N'TZ95', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (96, N'96', N'TZ96', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (97, N'97', N'TZ97', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (98, N'98', N'TZ98', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (99, N'99', N'TZ99', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
GO
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (100, N'100', N'TZ100', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (101, N'101', N'TZ101', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (102, N'102', N'TZ102', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (103, N'103', N'TZ103', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (104, N'104', N'TZ104', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (105, N'105', N'TZ105', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (106, N'106', N'TZ106', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (107, N'107', N'TZ107', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (108, N'108', N'TZ108', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (109, N'109', N'TZ109', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (110, N'110', N'TZ110', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (111, N'111', N'TZ111', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (112, N'112', N'TZ112', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (113, N'113', N'TZ113', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (114, N'114', N'TZ114', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (115, N'115', N'TZ115', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (116, N'116', N'TZ116', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (117, N'117', N'TZ117', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (118, N'118', N'TZ118', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (119, N'119', N'TZ119', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (120, N'120', N'TZ120', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (121, N'121', N'TZ121', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (122, N'122', N'TZ122', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (123, N'123', N'TZ123', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (124, N'124', N'TZ124', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (125, N'125', N'TZ125', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (126, N'126', N'TZ126', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (127, N'127', N'TZ127', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (128, N'128', N'TZ128', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (129, N'129', N'TZ129', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (130, N'130', N'TZ130', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (131, N'131', N'TZ131', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (132, N'132', N'TZ132', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (133, N'133', N'TZ133', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (134, N'134', N'TZ134', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (135, N'135', N'TZ135', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (136, N'136', N'TZ136', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (137, N'137', N'TZ137', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (138, N'138', N'TZ138', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (139, N'139', N'TZ139', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (140, N'140', N'TZ140', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (141, N'141', N'TZ141', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (142, N'142', N'TZ142', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (143, N'143', N'TZ143', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (144, N'144', N'TZ144', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (145, N'145', N'TZ145', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (146, N'146', N'TZ146', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (147, N'147', N'TZ147', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (148, N'148', N'TZ148', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (149, N'149', N'TZ149', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (150, N'150', N'TZ150', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (151, N'151', N'TZ151', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (152, N'152', N'TZ152', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (153, N'153', N'TZ153', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (154, N'154', N'TZ154', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (155, N'155', N'TZ155', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (156, N'156', N'TZ156', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (157, N'157', N'TZ157', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (158, N'158', N'TZ158', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (159, N'159', N'TZ159', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (160, N'160', N'TZ160', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (161, N'161', N'TZ161', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (162, N'162', N'TZ162', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (163, N'163', N'TZ163', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (164, N'164', N'TZ164', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (165, N'165', N'TZ165', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (166, N'166', N'TZ166', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (167, N'167', N'TZ167', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (168, N'168', N'TZ168', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (169, N'169', N'TZ169', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (170, N'170', N'TZ170', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (171, N'171', N'TZ171', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (172, N'172', N'TZ172', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (173, N'173', N'TZ173', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (174, N'174', N'TZ174', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (175, N'175', N'TZ175', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (176, N'176', N'TZ176', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (177, N'177', N'TZ177', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (178, N'178', N'TZ178', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (179, N'179', N'TZ179', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (180, N'180', N'TZ180', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (181, N'181', N'TZ181', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (182, N'182', N'TZ182', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (183, N'183', N'TZ183', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (184, N'184', N'TZ184', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (185, N'185', N'TZ185', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (186, N'186', N'TZ186', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (187, N'187', N'TZ187', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (188, N'188', N'TZ188', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (189, N'189', N'TZ189', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (190, N'190', N'TZ190', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (191, N'191', N'TZ191', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (192, N'192', N'TZ192', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (193, N'193', N'TZ193', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (194, N'194', N'TZ194', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (195, N'195', N'TZ195', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (196, N'196', N'TZ196', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (197, N'197', N'TZ197', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (198, N'198', N'TZ198', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (199, N'199', N'TZ199', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
GO
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (200, N'200', N'TZ200', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (201, N'201', N'TZ201', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (202, N'202', N'TZ202', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (203, N'203', N'TZ203', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (204, N'204', N'TZ204', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (205, N'205', N'TZ205', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (206, N'206', N'TZ206', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (207, N'207', N'TZ207', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (208, N'208', N'TZ208', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (209, N'209', N'TZ209', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (210, N'210', N'TZ210', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (211, N'211', N'TZ211', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (212, N'212', N'TZ212', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (213, N'213', N'TZ213', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (214, N'214', N'TZ214', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (215, N'215', N'TZ215', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (216, N'216', N'TZ216', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (217, N'217', N'TZ217', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (218, N'218', N'TZ218', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (219, N'219', N'TZ219', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (220, N'220', N'TZ220', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (221, N'221', N'TZ221', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (222, N'222', N'TZ222', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (223, N'223', N'TZ223', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (224, N'224', N'TZ224', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (225, N'225', N'TZ225', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (226, N'226', N'TZ226', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (227, N'227', N'TZ227', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (228, N'228', N'TZ228', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (229, N'229', N'TZ229', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (230, N'230', N'TZ230', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (231, N'231', N'TZ231', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (232, N'232', N'TZ232', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (233, N'233', N'TZ233', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (234, N'234', N'TZ234', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (235, N'235', N'TZ235', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (236, N'236', N'TZ236', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (237, N'237', N'TZ237', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (238, N'238', N'TZ238', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (239, N'239', N'TZ239', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (240, N'240', N'TZ240', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (241, N'241', N'TZ241', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (242, N'242', N'TZ242', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (243, N'243', N'TZ243', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (244, N'244', N'TZ244', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (245, N'245', N'TZ245', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (246, N'246', N'TZ246', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (247, N'247', N'TZ247', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (248, N'248', N'TZ248', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (249, N'249', N'TZ249', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (250, N'250', N'TZ250', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (251, N'251', N'TZ251', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (252, N'252', N'TZ252', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (253, N'253', N'TZ253', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (254, N'254', N'TZ254', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
INSERT [dbo].[tbl_timezone] ([Code], [Timezone_No], [Timezone_Name], [Period_1_Start], [Period_1_End], [Period_2_Start], [Period_2_End], [Period_3_Start], [Period_3_End], [Period_4_Start], [Period_4_End], [Period_5_Start], [Period_5_End], [Period_6_Start], [Period_6_End], [Status]) VALUES (255, N'255', N'TZ255', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', N'00:00', 0)
SET IDENTITY_INSERT [dbo].[tbl_timezone] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_tbl_attendence]    Script Date: 10/5/2022 11:26:12 AM ******/
ALTER TABLE [dbo].[tbl_attendence] ADD  CONSTRAINT [IX_tbl_attendence] UNIQUE NONCLUSTERED 
(
	[Code] ASC,
	[Attendance_DateTime] ASC,
	[Employee_ID] ASC,
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [idx_employee_id]    Script Date: 10/5/2022 11:26:12 AM ******/
CREATE NONCLUSTERED INDEX [idx_employee_id] ON [dbo].[tbl_employee]
(
	[Employee_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [PK_tbl_fkcmd_trans]    Script Date: 10/5/2022 11:26:12 AM ******/
ALTER TABLE [dbo].[tbl_fkcmd_trans] ADD  CONSTRAINT [PK_tbl_fkcmd_trans] PRIMARY KEY NONCLUSTERED 
(
	[trans_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [PK_tbl_fkcmd_trans_cmd_param]    Script Date: 10/5/2022 11:26:12 AM ******/
ALTER TABLE [dbo].[tbl_fkcmd_trans_cmd_param] ADD  CONSTRAINT [PK_tbl_fkcmd_trans_cmd_param] PRIMARY KEY NONCLUSTERED 
(
	[trans_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [PK_tbl_fkcmd_trans_cmd_result]    Script Date: 10/5/2022 11:26:12 AM ******/
ALTER TABLE [dbo].[tbl_fkcmd_trans_cmd_result] ADD  CONSTRAINT [PK_tbl_fkcmd_trans_cmd_result] PRIMARY KEY NONCLUSTERED 
(
	[trans_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [PK_tbl_fkdevice_status]    Script Date: 10/5/2022 11:26:12 AM ******/
ALTER TABLE [dbo].[tbl_fkdevice_status] ADD  CONSTRAINT [PK_tbl_fkdevice_status] PRIMARY KEY NONCLUSTERED 
(
	[device_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_check_reset_fk_cmd]    Script Date: 10/5/2022 11:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	PEFIS, 리일현
-- Create date: 2013-12-21
-- Description:	'기대재기동'지령이 발행된것이 있으면 그것을 기대로 내려보낸다.
--  출퇴근기를 재기동하면 현재 수행중의 모든 지령들이 없어지므로 자료기지의 표에서도 이러한 내용이 반영되여야 한다.
--  해당 출퇴근기에 대하여 지령수행상태가 'RUN'인 지령들은 모두 상태를 'CANCELLED'로 바꾼다.
--  지령수행상태가 'RUN'인 지령들은 big_field테이블에
--   지령수행과정에 주고 받은 자료의 잔해가 남아있을수 있다. 따라서 이러한 것들도 함께 지워버린다.
-- =============================================
CREATE PROCEDURE [dbo].[usp_check_reset_fk_cmd]
	-- Add the parameters for the stored procedure here
	@dev_id varchar(24),
	@trans_id varchar(16) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- trans_id를 무효한 값으로 설정한다.
    select @trans_id=''
    if @dev_id is null or len(@dev_id) = 0
		return -1
    
    -- Insert statements for procedure here
	-- 해당 기대에 대하여 '기대재기동'지령이 발행된것이 있는가 조사한다.
	SELECT @trans_id=trans_id FROM tbl_fkcmd_trans where device_id=@dev_id AND cmd_code='RESET_FK' AND status='WAIT'
	if @@ROWCOUNT = 0
		return -2 -- 없다면 복귀한다.
	
	begin transaction
	BEGIN TRY
		declare @trans_id_tmp as varchar(16)
		declare @csrTransId as cursor
		set @csrTransId = Cursor For
			 select trans_id
			 from tbl_fkcmd_trans
			 where device_id=@dev_id AND status='RUN'

		-- 기대의 지령수행상태가 'RUN'인것들을 조사하여 그에 해당한 레코드들을
		--  tbl_fkcmd_trans_cmd_param 표와 tbl_fkcmd_trans_cmd_result 표에서 지운다.
		Open @csrTransId
		Fetch Next From @csrTransId	Into @trans_id_tmp
		While(@@FETCH_STATUS = 0)
		begin
			DELETE FROM tbl_fkcmd_trans_cmd_param WHERE trans_id=@trans_id_tmp
			DELETE FROM tbl_fkcmd_trans_cmd_result WHERE trans_id=@trans_id_tmp
			Fetch Next From @csrTransId	Into @trans_id_tmp
		end
		close @csrTransId
		
		-- 기대의 지령수행상태가 'RUN'인것들을 'CANCELLED'로 바꾼다.
		UPDATE tbl_fkcmd_trans SET status='CANCELLED', update_time = GETDATE() WHERE device_id=@dev_id AND status='RUN'
		-- 기대에 대해 '재기동'지령이 발행된것들이 또 있으면 그것들의 상태를 'RESULT'로 바꾼다.
		UPDATE tbl_fkcmd_trans SET status='RESULT', update_time = GETDATE() WHERE device_id=@dev_id AND cmd_code='RESET_FK'
	END TRY
    BEGIN CATCH
		rollback transaction
		select @trans_id=''
		return -2
    END CATCH

	commit transaction
	return 0
END -- proc: usp_check_reset_fk_cmd






GO
/****** Object:  StoredProcedure [dbo].[usp_receive_cmd]    Script Date: 10/5/2022 11:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	PEFIS, 리일현
-- Create date: 2013-12-24
-- Modified date: 2014-12-4
-- Description:	기대가 자기에게로 발행한 지령을 얻어낼때 호출된다.
--  tbl_fkcmd_trans표에서 지령수행상태가 'WAIT'로 되여 있는것들 가운데서 가장 시간이 오래된것을 얻어낸 다음 상태를 'RUN'으로 바꾼다.
--  일부 지령들에 대해서(SET_ENROLL_DATA, SET_USER_INO)의 파라메터들은
--   tbl_fkcmd_trans_cmd_param 표에 존재하게 된다.
--
--  만일 어떤 기대에 대해서 발행된 새 지령을 얻는 시점에서
--   tbl_fkcmd_trans표에 이 기대로 발행된 지령들중 상태가 'RUN'인 지령들이 존재하면 그 지령들의 상태를 'CANCELLED'로 바꾼다.
--  기대가 지령을 받아 처리하고 결과를 올려보내던 중 어떤 문제로 하여 결과를 올려보내지 못하여 이러한 기록들이 생길수 있다.
--  이러한 지령들은 상태가 'RUN'에서 더 바뀔 가능성이 없으므로 상태를 'CANCELLED'로 바꾼다.
-- 또한 상태를 'RUN'으로부터 'CANCELLED'로 바꿀때 tbl_fkcmd_trans_cmd_param, tbl_fkcmd_trans_cmd_result 표들에 남아있는 잔해들을 지운다.
-- =============================================
CREATE PROCEDURE [dbo].[usp_receive_cmd]
	-- Add the parameters for the stored procedure here
	@dev_id varchar(24),
	@trans_id varchar(16) output,
	@cmd_code varchar(32) output,
	@cmd_param_bin varbinary(max) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select @trans_id = ''
	-- 파라메터들을 검사한다.
	if @dev_id is null or len(@dev_id) = 0
		return -1
	
	begin transaction
	BEGIN TRY
		declare @trans_id_tmp as varchar(16)
		declare @csrTransId as cursor
		
		-- 먼저 tbl_fkcmd_trans 표에서 실행상태가 'RUN'인것들의 trans_id를 얻어낸다.
		set @csrTransId = Cursor For
			 select trans_id
			 from tbl_fkcmd_trans
			 where device_id=@dev_id AND status='RUN'
		
		-- tbl_fkcmd_trans_cmd_param, tbl_fkcmd_trans_cmd_result 표들에서 
		--  해당 trans_id에 해당한 레코드들을 삭제한다.
		Open @csrTransId
		Fetch Next From @csrTransId	Into @trans_id_tmp
		While(@@FETCH_STATUS = 0)
		begin
			DELETE FROM tbl_fkcmd_trans_cmd_param WHERE trans_id=@trans_id_tmp
			DELETE FROM tbl_fkcmd_trans_cmd_result WHERE trans_id=@trans_id_tmp
			Fetch Next From @csrTransId	Into @trans_id_tmp
		end
		close @csrTransId
	END TRY
    BEGIN CATCH
		rollback transaction
		select @trans_id=''
		return -2
    END CATCH
	
	-- tbl_fkcmd_trans 표에서 실행상태가 'RUN' 이던 트랜잭션들의 상태를 'CANCELLED'로 바꾼다.
	UPDATE tbl_fkcmd_trans SET status='CANCELLED', update_time = GETDATE() WHERE device_id=@dev_id AND status='RUN'
	if @@error <> 0
	begin
		rollback transaction
		return -2
	end
	commit transaction
	
		BEGIN TRY
		SELECT @trans_id=trans_id, @cmd_code=cmd_code FROM tbl_fkcmd_trans
		WHERE device_id=@dev_id AND status='WAIT' ORDER BY update_time DESC
		
		if @@ROWCOUNT = 0
		begin
			select @trans_id=''
			return -3
		end
		
		--  tbl_fkcmd_trans_cmd_param 표의 cmd_param 필드의 값을 출구파라메터 @cmd_param_bin에 설정한다.
		select @cmd_param_bin=cmd_param from tbl_fkcmd_trans_cmd_param
		where trans_id=@trans_id
		
		--  tbl_fkcmd_trans 표의 status 필드의 값을 'WAIT'로 바꾼다.
		UPDATE tbl_fkcmd_trans SET status='RUN', update_time = GETDATE() WHERE trans_id=@trans_id
	END TRY
    BEGIN CATCH
    	select @trans_id=''
		return -2
	END CATCH

	return 0
END -- proc: usp_receive_cmd






GO
/****** Object:  StoredProcedure [dbo].[usp_set_cmd_result]    Script Date: 10/5/2022 11:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	PEFIS, 리일현
-- Create date: 2014-12-5
-- Description:	기대가 지령수행결과를 올려보낼때 호출된다.
--  지령의 수행결과 얻어진 자료를 tbl_fkcmd_trans_cmd_result 표에 보관한다.
--  tbl_fkcmd_trans 표에서 trans_id 에 해당한 지령의 수행상태가 'RUN'로 되여 있는 경우
--   지령의 결과코드를 보관하고 그 지령의 상태를 'RESULT'로 바꾼다.
-- =============================================
CREATE PROCEDURE [dbo].[usp_set_cmd_result]
	-- Add the parameters for the stored procedure here
	@dev_id varchar(24),
	@trans_id varchar(16),
	@return_code varchar(128),
	@cmd_result_bin varbinary(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- 파라메터들을 검사한다.
	if @dev_id is null or len(@dev_id) = 0
		return -1
	if @trans_id is null or len(@trans_id) = 0
		return -1
	
	begin transaction
	BEGIN TRY
		select trans_id from tbl_fkcmd_trans where trans_id = @trans_id and status='RUN'
		if @@ROWCOUNT != 1
		begin
			return -2
		end
		
		-- 먼저 tbl_fkcmd_trans_cmd_result 표에서 @trans_id에 해당한 레코드를 지우고 결과자료를 삽입한다.
		-- 만일 바이너리결과자료의 길이가 0이면 레코드를 삽입하지 않는다.
		delete from tbl_fkcmd_trans_cmd_result where trans_id=@trans_id
		if len(@cmd_result_bin) > 0
		begin
			insert into tbl_fkcmd_trans_cmd_result (trans_id, device_id, cmd_result) values(@trans_id, @dev_id, @cmd_result_bin)
		end
		
		-- tbl_fkcmd_trans 표에서 실행상태가 'RUN' 이던 트랜잭션들의 상태를 'RESULT'로 바꾼다.
		update tbl_fkcmd_trans set status='RESULT', return_code=@return_code, update_time = GETDATE() where trans_id=@trans_id and device_id=@dev_id and status='RUN'
	END TRY
    BEGIN CATCH
		rollback transaction
		return -3
    END CATCH
	
	if @@error <> 0
	begin
		rollback transaction
		return -3
	end
	commit transaction
	
	return 0
END -- proc: usp_set_cmd_result







GO
/****** Object:  StoredProcedure [dbo].[usp_update_device_conn_status]    Script Date: 10/5/2022 11:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	PEFIS, 리일현
-- Create date: 2013-12-21
-- Description:	기대의 접속상태표를 갱신한다.
--   기대는 일정한 시간간격으로 자기에게 발해된 지령이 있는가를 문의한다. 이때 기대의 접속상태표를 갱신한다.
--   이때 기대는 기대시간과 기대의 펌웨어는 무엇인가 등과 같은 정보를 함께 올려 보낸다.
-- =============================================
CREATE PROCEDURE [dbo].[usp_update_device_conn_status]
	-- Add the parameters for the stored procedure here
	@dev_id varchar(24),
	@dev_name varchar(24),
	@tm_last_update datetime,
	@fktm_last_update datetime,
	@dev_info varchar(2048),
	@dev_model varchar(24)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @dev_registered int
	if len(@dev_id) < 1 
		return -1
	if len(@dev_name) < 1 
		return -1
	
	begin transaction
	
	SELECT @dev_registered = COUNT(device_id) from tbl_fkdevice_status WHERE device_id=@dev_id
	if  @dev_registered = 0
	begin
		INSERT INTO tbl_fkdevice_status( 
				device_id, 
				device_name, 
				connected, 
				last_update_time, 
				last_update_fk_time, 
				device_info,
				dev_model)
			VALUES(
				@dev_id,
				@dev_name, 
				1,
				@tm_last_update,
				@fktm_last_update,
				@dev_info,
				@dev_model)
	end	
	else -- if @@ROWCOUNT = 0
	begin
		UPDATE tbl_fkdevice_status SET 
				device_id=@dev_id, 
				device_name=@dev_name, 
				connected=1,
				last_update_time=@tm_last_update,
				last_update_fk_time=@fktm_last_update,
				device_info=@dev_info,
				dev_model=@dev_model
			WHERE 
				device_id=@dev_id
	end
	
	if @@error <> 0
	begin
		rollback transaction
		return -2
	end
	
	commit transaction
	return 0
END -- proc: usp_update_device_conn_status







GO
USE [master]
GO
ALTER DATABASE [EagleEye] SET  READ_WRITE 
GO
