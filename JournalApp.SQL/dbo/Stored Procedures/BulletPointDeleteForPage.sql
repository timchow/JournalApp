CREATE PROCEDURE [dbo].[BulletPointDeleteForPage]

@pageId AS INT,
@bulletPointId AS INT,
@permanentDelete AS BIT = 0,
@modifiedBy AS VARCHAR(50) = NULL,
@modifiedDate AS DATETIME = NULL

AS
BEGIN

IF (@permanentDelete = 1)
DELETE FROM BulletPoint WHERE PageId = @pageId AND BulletPointId = @bulletPointId;
ELSE
UPDATE BulletPoint SET IsActive = 0, ModifiedBy = COALESCE(ModifiedBy,@modifiedBy), ModifiedDate = COALESCE(ModifiedDate,@modifiedDate) 
WHERE PageId = @pageId AND BulletPointId = @bulletPointId AND IsActive = 1;

END