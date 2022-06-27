CREATE FUNCTION GetChildFunc (
    @entityId NVARCHAR(255)
)
RETURNS TABLE
AS
RETURN
WITH cte AS (
  SELECT *, 1 AS level FROM NodeList WHERE entityId=@entityId
  UNION ALL
  SELECT NodeList.*, cte.level + 1 AS level FROM NodeList
  JOIN cte ON NodeList.parentEntityId = cte.entityId
)
SELECT type, SUM(warnCount) AS warnCount,SUM(alarmCount) AS alarmCount FROM ANDON WHERE entityId IN
(SELECT cte.entityId FROM cte)
GROUP BY type;