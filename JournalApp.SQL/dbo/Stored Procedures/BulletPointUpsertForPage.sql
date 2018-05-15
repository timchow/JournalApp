CREATE PROCEDURE [dbo].[BulletPointUpsertForPage]

@pageId AS INT,
@bulletPointId AS INT,
@newContent AS VARCHAR(MAX) = NULL,
@newPageId AS INT = NULL

AS
BEGIN

IF (@bulletPointId = 0)
INSERT INTO BulletPoint (Content, PageId, IsActive) VALUES (@newContent,@pageId,1);

ELSE

UPDATE BulletPoint SET Content = COALESCE(@newContent,Content),PageId = COALESCE(@newPageId,PageId)
WHERE PageId = @pageId AND BulletPointId = @bulletPointId;

END