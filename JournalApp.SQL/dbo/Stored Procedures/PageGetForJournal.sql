CREATE PROCEDURE [dbo].[PageGetForJournal]
@journalId AS INT,
@isActive AS BIT = 1

AS
BEGIN

SELECT PageId, JournalId, Title  FROM Page 
WHERE JournalId = @journalId AND IsActive = @isActive;

END