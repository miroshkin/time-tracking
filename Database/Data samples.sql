USE [u0713882_time_tracker]
GO
SET IDENTITY_INSERT [u0713882_admin].[Customers] ON 

INSERT [u0713882_admin].[Customers] ([CustomerId], [Name]) VALUES (1, N'Steve Jobs')
INSERT [u0713882_admin].[Customers] ([CustomerId], [Name]) VALUES (2, N'Bill Gates')
SET IDENTITY_INSERT [u0713882_admin].[Customers] OFF
SET IDENTITY_INSERT [u0713882_admin].[Projects] ON 

INSERT [u0713882_admin].[Projects] ([ProjectId], [CustomerId], [Name]) VALUES (1, 1, N'Apple iOS 19')
INSERT [u0713882_admin].[Projects] ([ProjectId], [CustomerId], [Name]) VALUES (2, 2, N'Microsoft Windows 19')
SET IDENTITY_INSERT [u0713882_admin].[Projects] OFF
SET IDENTITY_INSERT [u0713882_admin].[WorkTypes] ON 

INSERT [u0713882_admin].[WorkTypes] ([WorkTypeId], [Name], [Price]) VALUES (1, N'Database design', 40)
INSERT [u0713882_admin].[WorkTypes] ([WorkTypeId], [Name], [Price]) VALUES (2, N'Business logic', 30)
INSERT [u0713882_admin].[WorkTypes] ([WorkTypeId], [Name], [Price]) VALUES (3, N'Website design', 50)
INSERT [u0713882_admin].[WorkTypes] ([WorkTypeId], [Name], [Price]) VALUES (4, N'Programming', 10)
INSERT [u0713882_admin].[WorkTypes] ([WorkTypeId], [Name], [Price]) VALUES (5, N'UX design', 20)
SET IDENTITY_INSERT [u0713882_admin].[WorkTypes] OFF
SET IDENTITY_INSERT [u0713882_admin].[TimeRegistrations] ON 

INSERT [u0713882_admin].[TimeRegistrations] ([TimeRegistrationId], [Date], [Duration], [ProjectId], [WorkTypeId]) VALUES (2, CAST(N'2019-05-06T00:00:00.0000000' AS DateTime2), 2, 1, 1)
INSERT [u0713882_admin].[TimeRegistrations] ([TimeRegistrationId], [Date], [Duration], [ProjectId], [WorkTypeId]) VALUES (3, CAST(N'2019-05-06T00:00:00.0000000' AS DateTime2), 3, 1, 2)
INSERT [u0713882_admin].[TimeRegistrations] ([TimeRegistrationId], [Date], [Duration], [ProjectId], [WorkTypeId]) VALUES (4, CAST(N'2019-05-06T00:00:00.0000000' AS DateTime2), 4, 2, 3)
INSERT [u0713882_admin].[TimeRegistrations] ([TimeRegistrationId], [Date], [Duration], [ProjectId], [WorkTypeId]) VALUES (5, CAST(N'2019-05-06T00:00:00.0000000' AS DateTime2), 5, 2, 4)
SET IDENTITY_INSERT [u0713882_admin].[TimeRegistrations] OFF
