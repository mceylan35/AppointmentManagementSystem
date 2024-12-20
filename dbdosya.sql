USE [AppointmentManagementDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17.11.2024 17:50:20 ******/
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
/****** Object:  Table [dbo].[Appointments]    Script Date: 17.11.2024 17:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[Id] [uniqueidentifier] NOT NULL,
	[AppointmentDate] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModifiedAt] [datetime2](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 17.11.2024 17:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModifiedAt] [datetime2](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 17.11.2024 17:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModifiedAt] [datetime2](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 17.11.2024 17:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17.11.2024 17:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModifiedAt] [datetime2](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115121030_initialDB', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115123334_updatecreatedBy', N'9.0.0')
GO
INSERT [dbo].[Appointments] ([Id], [AppointmentDate], [Status], [Notes], [UserId], [ServiceId], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'b2b76dd2-528f-4ab2-4788-08dd06a03fc1', CAST(N'2025-02-01T15:15:00.0000000' AS DateTime2), 1, N'sadasd', N'3f57290d-808b-4b59-d0c9-08dd069f8c2a', N'602d6534-c55b-44cd-86eb-1a335f77f3bb', CAST(N'2024-11-17T00:39:09.1049497' AS DateTime2), N'yeniadmin', CAST(N'2024-11-17T14:11:52.1370685' AS DateTime2), N'admin', 0)
INSERT [dbo].[Appointments] ([Id], [AppointmentDate], [Status], [Notes], [UserId], [ServiceId], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'17c2847f-d6df-4d8d-4e91-08dd0702d7ba', CAST(N'2025-12-15T04:00:00.0000000' AS DateTime2), 1, N'teste', N'23332b48-aba9-4342-8aa2-08dd0702b8fe', N'602d6534-c55b-44cd-86eb-1a335f77f3bb', CAST(N'2024-11-17T12:24:54.7199971' AS DateTime2), N'testuser', CAST(N'2024-11-17T14:28:16.4349138' AS DateTime2), N'testuser', 0)
INSERT [dbo].[Appointments] ([Id], [AppointmentDate], [Status], [Notes], [UserId], [ServiceId], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'b717d2d3-aa30-4eb3-a0e6-08dd070be7a0', CAST(N'2026-12-11T10:10:00.0000000' AS DateTime2), 2, N'tyyyy', N'23332b48-aba9-4342-8aa2-08dd0702b8fe', N'602d6534-c55b-44cd-86eb-1a335f77f3bb', CAST(N'2024-11-17T13:29:46.9056012' AS DateTime2), N'testuser', CAST(N'2024-11-17T14:46:32.8230794' AS DateTime2), N'admin', 0)
INSERT [dbo].[Appointments] ([Id], [AppointmentDate], [Status], [Notes], [UserId], [ServiceId], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'f8cd5a48-0545-4e77-4620-08dd071430a0', CAST(N'2025-11-12T17:28:00.0000000' AS DateTime2), 0, N'teste', N'23332b48-aba9-4342-8aa2-08dd0702b8fe', N'602d6534-c55b-44cd-86eb-1a335f77f3bb', CAST(N'2024-11-17T14:29:44.2463663' AS DateTime2), N'testuser', CAST(N'2024-11-17T14:39:06.9919239' AS DateTime2), N'testuser', 1)
INSERT [dbo].[Appointments] ([Id], [AppointmentDate], [Status], [Notes], [UserId], [ServiceId], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'455234a8-2ba2-4be1-4621-08dd071430a0', CAST(N'2024-11-17T17:32:00.0000000' AS DateTime2), 0, N'hghjgj', N'23332b48-aba9-4342-8aa2-08dd0702b8fe', N'602d6534-c55b-44cd-86eb-1a335f77f3bb', CAST(N'2024-11-17T14:31:01.8850686' AS DateTime2), N'testuser', CAST(N'2024-11-17T14:35:00.3882667' AS DateTime2), N'testuser', 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'52e00028-2ec9-4484-b32d-301448d1a512', N'User', N'Normal kullanıcı', CAST(N'2024-11-15T12:34:26.4814738' AS DateTime2), NULL, NULL, NULL, 0)
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'db487d0f-577e-48ed-a11b-52955ba6b1df', N'Admin', N'Sistem yöneticisi', CAST(N'2024-11-15T12:34:26.4813588' AS DateTime2), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Services] ([Id], [Name], [Description], [IsActive], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'602d6534-c55b-44cd-86eb-1a335f77f3bb', N'Far Ayarı', N'Araç far ayar kontrolü', 1, CAST(N'2024-11-15T12:34:32.6715427' AS DateTime2), NULL, NULL, NULL, 0)
INSERT [dbo].[Services] ([Id], [Name], [Description], [IsActive], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'caab27a3-0188-4048-bbdd-2958e7b49a9f', N'Fren Testi', N'Araç fren sistemi kontrolü', 1, CAST(N'2024-11-15T12:34:32.6715274' AS DateTime2), NULL, NULL, NULL, 0)
INSERT [dbo].[Services] ([Id], [Name], [Description], [IsActive], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'46a0fe92-912c-4ae0-8bce-903bf8033c8b', N'Egzoz Gazı Ölçümü', N'Araç egzoz emisyon ölçümü', 1, CAST(N'2024-11-15T12:34:32.6715370' AS DateTime2), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'48c7b384-512d-4b47-8946-08dd0571db41', N'd8f68652-a53d-450f-be46-83e56b416f72', N'52e00028-2ec9-4484-b32d-301448d1a512')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'9426d48e-0b3d-40c3-db36-08dd069f8fd0', N'db969042-0f6d-4052-b461-f03630f21f9b', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'b3fbe8f4-db71-4115-db37-08dd069f8fd0', N'3f57290d-808b-4b59-d0c9-08dd069f8c2a', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'ae4ba44a-7900-49db-4918-08dd06f70ca5', N'1ddd32af-3f08-4b4c-4e3d-08dd06f5af88', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'aa3ba039-3655-4f5f-7375-08dd06f86a29', N'90a9e3aa-6aff-4993-e788-08dd06f869fc', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'87a8f948-4901-4372-8580-08dd06faef81', N'3b9c94ff-f8f8-4441-fed9-08dd06f718c2', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'e2daadcd-7eb8-49d7-8581-08dd06faef81', N'61ccbf3a-2cba-48e1-fe5e-08dd06a1036a', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'910526f6-1b5e-4857-8582-08dd06faef81', N'8e4915d6-7c5f-485e-8592-08dd06957b1c', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'2d05ed1f-260e-4ec3-8583-08dd06faef81', N'8f7b6a13-8cdf-4ac7-4e3c-08dd06f5af88', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'f408385b-5030-4095-58ec-08dd06fb4e5a', N'e4d89dba-4ea9-4707-d0ca-08dd069f8c2a', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'fb71bf0d-c450-431e-b86f-08dd0702b91b', N'23332b48-aba9-4342-8aa2-08dd0702b8fe', N'52e00028-2ec9-4484-b32d-301448d1a512')
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (N'5c089158-bc1f-4ead-ad60-08dd0711d4d6', N'd5d0d484-6831-48e0-af36-08dd06f93d2e', N'db487d0f-577e-48ed-a11b-52955ba6b1df')
GO
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'bc967261-5e2c-45ac-dc4b-08dd0693ea3e', N'asdasd', N'asdasd@sadsad.com', N'$2a$11$zo55apsvh6px6ozFnzVp4u/4l5UZ7/iMjEKlY./yeNpE.Lh021uSq', 1, NULL, CAST(N'2024-11-16T23:10:51.7221062' AS DateTime2), N'admin', CAST(N'2024-11-16T23:17:16.7875129' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'8e4915d6-7c5f-485e-8592-08dd06957b1c', N'user1', N'asaadasd@sadsad.com', N'$2a$11$VBPinEtzL/1MeRJ9yReMXuEa.NUxLuWMx/M33UiS/hn/rujcwCtue', 1, NULL, CAST(N'2024-11-16T23:22:04.2727448' AS DateTime2), N'admin', NULL, NULL, 0)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'f3ccd9e3-288c-47ac-5694-08dd069c84c7', N'asdasd121', N'asdaasdsadsd@sadsad.com', N'$2a$11$7ZqyjguLFS2sy.nOTPwgGO0MWwcICWtUCjib7h3acZEfd/tBd3XVC', 0, NULL, CAST(N'2024-11-17T00:12:26.8815453' AS DateTime2), N'admin', CAST(N'2024-11-17T00:12:36.3215496' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'3f57290d-808b-4b59-d0c9-08dd069f8c2a', N'yeniadmin', N'yeni@gmail.com', N'$2a$11$wihT4FP80E9.msxsvy/Idu3PBfhmsIq46a49VS0cwFRikX9ymDkt6', 1, NULL, CAST(N'2024-11-17T00:34:07.8516023' AS DateTime2), N'admin', NULL, NULL, 0)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'e4d89dba-4ea9-4707-d0ca-08dd069f8c2a', N'Osman', N'sinav@gmail.com', N'$2a$11$yZyf696r62MkqAWEugOFievL0kShgfSDUIHayw7Wd6xujDaiBVNwO', 1, NULL, CAST(N'2024-11-17T00:38:28.1218537' AS DateTime2), N'yeniadmin', NULL, NULL, 0)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'61ccbf3a-2cba-48e1-fe5e-08dd06a1036a', N'test1', N'a123asdsdasd@sadsad.com', N'$2a$11$e11HHuejkGP/7dNrXnSl9uTgLPUhZq5yYSSYnHyYo1vZA9taX7xAe', 0, NULL, CAST(N'2024-11-17T00:44:38.9183684' AS DateTime2), N'admin', CAST(N'2024-11-17T11:31:05.8696662' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'8f7b6a13-8cdf-4ac7-4e3c-08dd06f5af88', N'usertest1', N'usertest1@mail.com', N'$2a$11$1R4zV2.AyMy/hdMgkg1cT.T70UZUPzPWP8B3Gjvioo8Lr2LNpVCue', 1, NULL, CAST(N'2024-11-17T10:50:45.6147650' AS DateTime2), N'admin', NULL, NULL, 0)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'1ddd32af-3f08-4b4c-4e3d-08dd06f5af88', N'usertest145', N'user15test1@mail.com', N'$2a$11$DaqYloNbDhsUSBDU5P7/IuQjfPW8DyVDEK.dxOQ8ZqmfXVjROB6ge', 0, NULL, CAST(N'2024-11-17T10:51:20.8574238' AS DateTime2), N'admin', CAST(N'2024-11-17T11:00:35.1192076' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'6d8ac38a-918e-4b6a-a3a9-08dd06f6235f', N'asdasd23432asdsad', N'asdfsdfqsadasd@asd.com', N'$2a$11$xG2ADh3fhEmB7U/R4rHu..L4FLmCBS17e8Cn805D2cClLSsO5E3mu', 0, NULL, CAST(N'2024-11-17T10:53:58.2631376' AS DateTime2), N'admin', CAST(N'2024-11-17T11:10:23.8501844' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'3b9c94ff-f8f8-4441-fed9-08dd06f718c2', N're1', N'asdaasdassd@sadssadsadad.com', N'$2a$11$wKWhStlCmdm4L78tf6l8W.Urc6bZMXG1trZ.q63Fa2AcLpV9qo2Ym', 0, NULL, CAST(N'2024-11-17T11:00:49.8637797' AS DateTime2), N'admin', CAST(N'2024-11-17T11:31:11.6182804' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'121b79e1-8013-4a95-feda-08dd06f718c2', N're1sdfdsf', N'asdaasddsfdsfassd@sadssadsadad.com', N'$2a$11$JnlG1wHcnhm7bklahsEUfOWu94XxwtGMO/2l46zGGUqm8XDSAi74i', 0, NULL, CAST(N'2024-11-17T11:01:12.6660496' AS DateTime2), N'admin', CAST(N'2024-11-17T11:10:26.3177443' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'90a9e3aa-6aff-4993-e788-08dd06f869fc', N'test32asdsad', N'asdasdfsdfqsadasd@assadsadd.com', N'$2a$11$b9t1q0ZTowPZ5YAn53S4aO.RCDDPb0aUQ3c6fyKP7.p6WMxEsxOje', 0, NULL, CAST(N'2024-11-17T11:10:15.7620931' AS DateTime2), N'admin', CAST(N'2024-11-17T11:10:28.4164886' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'd5d0d484-6831-48e0-af36-08dd06f93d2e', N'asdasd234asdsad32asdsad', N'usasddasd@sadsad.com', N'$2a$11$0jgYKT5/lmfwRJfZ/f8wfe8t3/b082NNnTQKSoMkuHZC/P.EA6KsO', 0, NULL, CAST(N'2024-11-17T11:16:09.9590353' AS DateTime2), N'admin', CAST(N'2024-11-17T14:12:32.7237150' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'23332b48-aba9-4342-8aa2-08dd0702b8fe', N'testuser', N'testuser@gmail.com', N'$2a$11$Za./HW58NdRFpdWIyv0f9eVNT4bTPqHpmnAb.vE9Pw9/jA0qgfZkq', 1, NULL, CAST(N'2024-11-17T12:24:03.1849290' AS DateTime2), N'admin', NULL, NULL, 0)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'd8f68652-a53d-450f-be46-83e56b416f72', N'user', N'user@system.com', N'$2a$11$i3ApNkCUWJFGN5DZDrV.Fe0wmm2o1Gur48xpRUYyc/2UzYwAGXZYS', 1, NULL, CAST(N'2024-11-15T12:34:32.2961412' AS DateTime2), NULL, CAST(N'2024-11-16T23:11:17.0858904' AS DateTime2), N'admin', 1)
INSERT [dbo].[Users] ([Id], [Username], [Email], [PasswordHash], [IsActive], [PhoneNumber], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [IsDeleted]) VALUES (N'db969042-0f6d-4052-b461-f03630f21f9b', N'admin', N'admin@system.com', N'$2a$11$ukJ43kAWiI7DCPMeEH4gzOFjLTgGtGeKkgYet.14HtIzLKRwix.xu', 1, NULL, CAST(N'2024-11-15T12:34:32.2961434' AS DateTime2), NULL, NULL, NULL, 0)
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Services_ServiceId]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
