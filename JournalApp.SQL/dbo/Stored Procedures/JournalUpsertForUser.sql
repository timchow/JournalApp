CREATE PROCEDURE [dbo].[JournalUpsertForUser]

@userId AS INT,
@journalId AS INT,
@newTitle AS VARCHAR(MAX) = NULL,
@newDescription AS VARCHAR(MAX) = NULL,
@newImagePath AS VARCHAR(MAX) = NULL

AS
BEGIN

IF (@journalId = 0)
INSERT INTO Journal (UserId, Title, Description, ImagePath, IsActive) VALUES (@userId, @newTitle,@newDescription,@newImagePath,1);

ELSE

UPDATE Journal SET Title = COALESCE(@newTitle,Title),Description = COALESCE(@newDescription,Description), ImagePath = COALESCE(@newImagePath,ImagePath)
WHERE JournalId = @journalId AND UserId = @userId;

END