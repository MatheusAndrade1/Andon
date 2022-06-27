CREATE PROCEDURE GetParent @entityId nvarchar(255)
AS
WITH cte AS (
  SELECT *, 1 AS level FROM NodeList WHERE entityId=@entityId
  UNION ALL
  SELECT NodeList.*, cte.level + 1 AS level FROM NodeList
  JOIN cte ON NodeList.entityId = cte.parentEntityId
)
SELECT cte.id, cte.entityId FROM cte
ORDER BY level DESC