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

CREATE TABLE CalendarEvents
(
  Id				INTEGER PRIMARY KEY AUTOINCREMENT	NOT NULL,
  TimeBinary		INTEGER								NOT NULL, 
  HasTime			INTEGER								NOT NULL,
  Description		TEXT								NOT NULL,
  Place				TEXT								NULL
);

CREATE TABLE Facts
(
  Id				INTEGER PRIMARY KEY AUTOINCREMENT	NOT NULL,
  FactText			TEXT								NOT NULL
);

CREATE TABLE Holidays
(
  Id				INTEGER PRIMARY KEY AUTOINCREMENT	NOT NULL,
  [Day]				INTEGER								NOT NULL,
  [Month]			INTEGER								NOT NULL,
  Name				TEXT								NOT NULL,
  Description		TEXT								NOT NULL,
  Picture			TEXT								NOT NULL
);

CREATE TABLE FamousBirthdays
(
  Id				INTEGER PRIMARY KEY AUTOINCREMENT	NOT NULL,
  [Day]				INTEGER								NOT NULL,
  [Month]			INTEGER								NOT NULL,
  Name				TEXT								NOT NULL,
  Description		TEXT								NOT NULL,
  Photo				TEXT								NOT NULL
);

CREATE TABLE  Notices
(
  Id				INTEGER PRIMARY KEY AUTOINCREMENT	NOT NULL,
  Title				TEXT								NOT NULL,
  DateBinary		INTEGER								NOT NULL,
  [Text]			TEXT								NOT NULL,
  Duration			INTEGER								NOT NULL,
  IsImportant		INTEGER								NOT NULL
);

CREATE TABLE Students
(
  Id				INTEGER PRIMARY KEY AUTOINCREMENT	NOT NULL,
  Name				TEXT								NOT NULL,
  BirthdayDay		INTEGER								NOT NULL,
  BirthdayMounth	INTEGER								NOT NULL,
  Class				TEXT								NOT NULL,
  IsMale			INTEGER								NOT NULL
);

CREATE TABLE Teachers
(
  Id				INTEGER PRIMARY KEY AUTOINCREMENT	NOT NULL,
  Name				TEXT								NOT NULL,
  BirthdayDay		INTEGER								NOT NULL,
  BirthdayMounth	INTEGER								NOT NULL,
  Position			TEXT								NOT NULL,
  IsMale			INTEGER								NOT NULL
);