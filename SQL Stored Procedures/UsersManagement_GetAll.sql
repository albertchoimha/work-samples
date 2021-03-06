USE [C61]
GO
/****** Object:  StoredProcedure [dbo].[UsersManagement_GetAll]    Script Date: 12/12/2018 6:51:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[UsersManagement_GetAll]
AS

BEGIN
	SELECT
		u.Id AS UserId, 
		p.FirstName, p.MiddleInitial, p.LastName,
		u.Email

	FROM Users u
		left join UserRoles ur ON u.Id = ur.UserId
		left join Roles r ON ur.RoleId = r.RoleId
		left join Person p ON p.UserId = u.Id

	GROUP BY u.Id, p.FirstName, p.MiddleInitial, p.LastName, u.Email 
END

