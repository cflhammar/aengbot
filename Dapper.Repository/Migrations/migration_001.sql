CREATE SCHEMA IF NOT EXISTS aengbot;

CREATE TABLE IF NOT EXISTS aengbot.courses
(
    id   varchar(50) PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS aengbot.subscriptions
(
    id       SERIAL PRIMARY KEY,
    courseId varchar(50) NOT NULL,
    date varchar(10) NOT NULL,
    from_time varchar(5) NOT NULL,
    to_time varchar(5) NOT NULL,
    number_players int NOT NULL,
    email varchar(255) NOT NULL
);


