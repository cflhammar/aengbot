CREATE TABLE Notified
(
    Id int NOT NULL IDENTITY PRIMARY KEY,
    CourseId varchar(50) NOT NULL ,
    TeeTime datetime NOT NULL,
    Email VARCHAR(255) NOT NULL
);