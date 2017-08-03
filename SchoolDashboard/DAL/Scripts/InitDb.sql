PRAGMA temp_store = 2;
CREATE TEMP TABLE _Variables(Name TEXT PRIMARY KEY, [Value] INTEGER);

INSERT INTO SchoolLevels ([Description]) VALUES ('1 клас');

INSERT INTO _Variables VALUES ('Id', (SELECT last_insert_rowid()));

INSERT INTO Lessons (SchoolLevel, StartTimeSeconds, EndTimeSeconds, [Description]) VALUES 
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 32400, 34500, 'Урок 1'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 35400, 37500, 'Урок 2'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 38700, 40800, 'Урок 3'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 42000, 44100, 'Урок 4'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 45000, 47100, 'Урок 5');

INSERT INTO SchoolLevels ([Description]) VALUES ('2-4 клас');

UPDATE _Variables SET [Value] = (SELECT last_insert_rowid()) WHERE Name = 'Id';

INSERT INTO Lessons (SchoolLevel, StartTimeSeconds, EndTimeSeconds, [Description]) VALUES 
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 32400, 34800, 'Урок 1'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 35400, 37800, 'Урок 2'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 39000, 41400, 'Урок 3'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 42600, 45000, 'Урок 4'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 45600, 48000, 'Урок 5'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 48600, 51000, 'Урок 6');

INSERT INTO SchoolLevels ([Description]) VALUES ('Старша школа');

UPDATE _Variables SET [Value] = (SELECT last_insert_rowid()) WHERE Name = 'Id';

INSERT INTO Lessons (SchoolLevel, StartTimeSeconds, EndTimeSeconds, [Description]) VALUES 
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 32400, 35100, 'Урок 1'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 35700, 38400, 'Урок 2'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 39000, 41700, 'Урок 3'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 42900, 45600, 'Урок 4'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 46800, 49500, 'Урок 5'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 50100, 52800, 'Урок 6'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 53400, 56100, 'Урок 7');

DROP TABLE _Variables;