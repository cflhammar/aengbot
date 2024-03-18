
CREATE TABLE Courses
(
    Id   varchar(50) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE Subscriptions
(
    Id int NOT NULL IDENTITY PRIMARY KEY,
    CourseId varchar(50) NOT NULL,
    Date varchar(10) NOT NULL,
    FromTime varchar(5) NOT NULL,
    ToTime varchar(5) NOT NULL,
    NumberPlayers int NOT NULL,
    Email varchar(255) NOT NULL
);


