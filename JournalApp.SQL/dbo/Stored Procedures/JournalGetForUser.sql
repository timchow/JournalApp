CREATE PROCEDURE [dbo].[JournalGetForUser]
@userId AS INT,
@isActive AS BIT = 1

AS
BEGIN

SELECT JournalId, UserId, Title, Description, ImagePath  FROM Journal 
WHERE UserId = @userId AND IsActive = @isActive;

END