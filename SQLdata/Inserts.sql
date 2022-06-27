INSERT INTO dbo.Nodelist (entityId, name, hierarchyDefinitionId, hierarchyId, parentEntityId, path)
VALUES
('PlantA', 'Plant A', '0f8ed032-e998-70b9-7b90-39f86639ea75', 'LDS', null, '/Plant A'),
('PlantB', 'Plant B', '0f8ed032-e998-70b9-7b90-39f86639ea75', 'LDS', null, '/Plant B'),
('AreaB', 'Area B', '0f8ed032-e998-70b9-7b90-39f86639ea75', 'LDS', 'PlantA', '/Plant A/Area B'),
('AreaA', 'Area A', '0f8ed032-e998-70b9-7b90-39f86639ea75', 'LDS', 'PlantA', '/Plant A/Area A'),
('Line1', 'Line 1', '0f8ed032-e998-70b9-7b90-39f86639ea75', 'LDS', 'AreaA', '/Plant A/Area A/Line 1'),
('Line2', 'Line 2', '0f8ed032-e998-70b9-7b90-39f86639ea75', 'LDS', 'AreaA', '/Plant A/Area A/Line 2'),
('Line3', 'Line 3', '0f8ed032-e998-70b9-7b90-39f86639ea75', 'LDS', 'AreaB', '/Plant A/Area B/Line 3');

INSERT INTO dbo.Andon (type, warnCount, alarmCount, entityId)
VALUES
('Operation', 0, 0, 'Line1'),
('Operation', 0, 0, 'Line2'),
('Operation', 0, 0, 'Line3'),
('Safety', 0, 0, 'Line1'),
('Safety', 0, 0, 'Line2'),
('Safety', 0, 0, 'Line3'),
('Stock', 0, 0, 'Line1'),
('Stock', 0, 0, 'Line2'),
('Stock', 0, 0, 'Line3'),
('FQC', 0, 0, 'Line1'),
('FQC', 0, 0, 'Line2'),
('FQC', 0, 0, 'Line3'),
('Delivery', 0, 0, 'Line1'),
('Delivery', 0, 0, 'Line2'),
('Delivery', 0, 0, 'Line3'),
('Service', 0, 0, 'Line1'),
('Service', 0, 0, 'Line2'),
('Service', 0, 0, 'Line3'),
('Setup', 0, 0, 'Line1'),
('Setup', 0, 0, 'Line2'),
('Setup', 0, 0, 'Line3'),
('IQC', 0, 0, 'Line1'),
('IQC', 0, 0, 'Line2'),
('IQC', 0, 0, 'Line3');


