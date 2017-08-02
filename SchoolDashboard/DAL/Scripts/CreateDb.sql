CREATE TABLE SchoolLevels
(
  Id			INT	PRIMARY KEY	NOT NULL,
  [Description]	TEXT			NOT NULL
)

CREATE TABLE Lessons
(
  Id				INT PRIMARY KEY	NOT NULL,
  SchoolLevel		INT				NOT NULL,
  StartTimeString	TEXT			NOT NULL,
  EndTimeString		TEXT			NOT NULL,
  [Description]		TEXT			NOT NULL,
  FOREIGN KEY (SchoolLevel) REFERENCES SchoolLevels(Id)
)