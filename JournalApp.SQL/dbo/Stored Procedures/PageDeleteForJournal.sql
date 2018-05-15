CREATE PROCEDURE [dbo].[PageDeleteForJournal]

@journalId AS INT,
@pageId AS INT,
@permanentDelete AS BIT = 0,
@modifiedBy AS VARCHAR(50) = NULL,
@modifiedDate AS DATETIME = NULL

AS
BEGIN

IF (@permanentDelete = 1)
DELETE FROM Page WHERE JournalId = @journalId AND PageId = @pageId;
ELSE
UPDATE Page SET IsActive = 0, ModifiedBy = COALESCE(ModifiedBy,@modifiedBy), ModifiedDate = COALESCE(ModifiedDate,@modifiedDate) 
WHERE JournalId = @journalId AND PageId = @pageId AND IsActive = 1;

END