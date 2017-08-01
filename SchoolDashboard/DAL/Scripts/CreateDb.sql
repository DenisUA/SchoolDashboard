CREATE TABLE SchoolLevels
(
  Id			INT	PRIMARY KEY	NOT NULL,
  Description	TEXT			NOT NULL
)

CREATE TABLE Lessons
(
  Id			INT PRIMARY KEY	NOT NULL,
  SchoolLevel	INT				NOT NULL,
  StartTime		TEXT			NOT NULL,
  EndTime		TEXT			NOT NULL,
  FOREIGN KEY (SchoolLevel) REFERENCES SchoolLevels(Id)
)