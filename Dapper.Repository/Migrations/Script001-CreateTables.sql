
CREATE TABLE Courses
(
    Id   varchar(50) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE Subscriptions
(
    Id int NOT NULL IDENTITY PRIMARY KEY,
    CourseId varchar(50) NOT NULL,
    FromTime datetime NOT NULL,
    ToTime datetime NOT NULL,
    NumberPlayers int NOT NULL,
    Email varchar(255) NOT NULL
);


