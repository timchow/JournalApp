CREATE PROCEDURE [dbo].[PageUpsertForJournal]

@journalId AS INT,
@pageId AS INT,
@newTitle AS VARCHAR(MAX) = NULL,
@newJournalId AS INT = NULL

AS
BEGIN

IF (@pageId = 0)
INSERT INTO Page (Title, JournalId, IsActive) VALUES (@newTitle,@journalId,1);

ELSE

UPDATE Page SET Title = COALESCE(@newTitle,Title),JournalId = COALESCE(@newJournalId,JournalId)
WHERE PageId = @pageId AND JournalId = @journalId;

END