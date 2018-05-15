CREATE PROCEDURE [dbo].[BulletPointGetForPage]

@pageId AS INT,
@isActive AS BIT = 1

AS
BEGIN

SELECT PageId, BulletPointId, Content FROM BulletPoint 
WHERE PageId = @pageId AND IsActive = @isActive;

END