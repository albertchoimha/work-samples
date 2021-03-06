USE [C61]
GO
/****** Object:  StoredProcedure [dbo].[ProfileData_GetById]    Script Date: 12/12/2018 6:50:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ProfileData_GetById]
    @Id INT
AS
/*

Declare @_id INT = 331
EXEC profiledata_getbyid @_id

*/
BEGIN
    SELECT
        u.Id as UserId, u.Email, u.UserName,
        p.FirstName, p.MiddleInitial, p.LastName, p.Gender,
        pf.Bio, pf.Title,
        fs.BasePath, fs.SystemFileName,
		tp.id as PhoneId,tp.PhoneNumber,tp.Extension,tp.phoneType,
		pt.DisplayName,
		fs.Id as ImageId

    FROM
        Users u left JOIN Person p on u.Id = p.UserId
        left JOIN Profile pf on u.Id = pf.UserId
        left JOIN ProfileImage pi on u.Id = pi.UserId
        left JOIN FileStorage fs on pi.FileStorageId = fs.Id
		left JOIN PersonPhone pp on u.Id = pp.UserId
		left JOIN Telephone tp on pp.PhoneId = tp.Id
		left Join PhoneType pt on pt.id = tp.PhoneType

    WHERE
        u.Id = @Id
END 

