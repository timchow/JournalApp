create PROCEDURE [dbo].[JournalDeleteForUser]

@userId AS INT,
@journalId AS INT,
@permanentDelete AS BIT = 0,
@modifiedBy AS VARCHAR(50) = NULL,
@modifiedDate AS DATETIME = NULL

AS
BEGIN

IF (@permanentDelete = 1)
DELETE FROM Journal WHERE UserId = @userId AND JournalId = @journalId;
ELSE
UPDATE Journal SET IsActive = 0, ModifiedBy = COALESCE(ModifiedBy,@modifiedBy), ModifiedDate = COALESCE(ModifiedDate,@modifiedDate) 
WHERE UserId = @userId AND JournalId = @journalId AND IsActive = 1;

END