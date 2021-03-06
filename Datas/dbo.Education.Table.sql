
CREATE TABLE [dbo].[Education](
	[Id] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (1, 0, N'研究生教育')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (2, 0, N'本专科教育')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (3, 0, N'中等职业学校')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (4, 0, N'普通高中')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (5, 0, N'初中')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (6, 0, N'小学')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (7, 0, N'其他')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (8, 1, N'博士研究生')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (9, 1, N'硕士研究生')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (10, 1, N'硕士生班')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (11, 1, N'中央党校研究生')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (12, 1, N'省（区、市）委党校研究生')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (13, 2, N'大学')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (14, 2, N'大专')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (15, 2, N'大学普通班')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (16, 2, N'第二学士学位班')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (17, 2, N'中央党校大学')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (18, 2, N'省（区、市）委党校大学')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (19, 2, N'中央党校大专')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (20, 2, N'省（区、市）委党校大专')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (21, 3, N'中等专科')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (22, 3, N'职业高中')
INSERT [dbo].[Education] ([Id], [ParentId], [Name]) VALUES (23, 3, N'技工学校')
