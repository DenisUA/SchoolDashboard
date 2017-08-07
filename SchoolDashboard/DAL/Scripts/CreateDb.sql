CREATE TABLE SchoolLevels
(
  Id			INTEGER	PRIMARY KEY AUTOINCREMENT	NOT NULL,
  Description	TEXT								NOT NULL
);

CREATE TABLE Lessons
(
  Id				INTEGER	PRIMARY KEY AUTOINCREMENT	NOT NULL,
  SchoolLevel		INTEGER								NOT NULL,
  StartTimeSeconds	REAL								NOT NULL,
  EndTimeSeconds	REAL								NOT NULL,
  Description		TEXT								NOT NULL,
  FOREIGN KEY (SchoolLevel) REFERENCES SchoolLevels(Id)
);

CREATE TABLE Awards
(
  Id				INTEGER PRIMARY KEY AUTOINCREMENT	NOT NULL,
  [Type]			INTEGER								NOT NULL,
  Owner				TEXT								NOT NULL,
  Description		TEXT								NOT NULL
);