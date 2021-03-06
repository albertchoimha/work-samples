USE [C61]
GO
/****** Object:  StoredProcedure [dbo].[ProfileDataPerson_Update]    Script Date: 12/12/2018 6:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ProfileDataPerson_Update]
	@UserId INT,
	@FirstName NVARCHAR(255),
	@MiddleInitial NCHAR(1),
	@LastName NVARCHAR(255),
	@Gender NVARCHAR(50),
	@ModifiedBy NVARCHAR(128)

AS 
/*
DECLARE
		@_userid int = 331,
		@_firstname nvarchar(255) = 'testing',
		@_middleinitial nchar(1) = 's',
		@_lastname nvarchar(255) = 'testing',
		@_modifiedBy nvarchar(128) = 'me'

		EXEC ProfileDataPerson_Update @_userid, @_firstname, @_middleinitial, @_lastname,@_modifiedBy
		SELECT * from person where userid = @_userid
*/
BEGIN
	DECLARE @ModifiedDate DATETIME = getutcdate()
	UPDATE Person
		SET	
		FirstName = @FirstName,
		MiddleInitial = @MiddleInitial,
		LastName = @LastName,
		Gender = @Gender,
		ModifiedBy = @ModifiedBy

		WHERE 
		UserId = @UserId
		
		IF @@ROWCOUNT=0
			INSERT INTO Person (
			UserId,
			FirstName,
			MiddleInitial,
			LastName,
			Gender,
			ModifiedBy
			) VALUES (
			@UserId,
			@FirstName,
			@MiddleInitial,
			@LastName,
			@Gender,
			@ModifiedBy)
END


