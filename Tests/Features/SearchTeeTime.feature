Feature: Search tee times logic tests

    Scenario: Given a tee time was found
        Given current date is 2025-01-01
        Given courses exist
          | Id | Name        |
          | 1  | GolfCourse1 |
        And users subscribed to
          | Email          | CourseId | FromTime         | ToTime           | NumberOfPlayers |
          | user@email.com | 1        | 2025-02-01T09:00 | 2025-02-01T12:00 | 2               |
        And sweetspot has bookings
          | CourseId | FromTime         | ToTime           | AvailableSlots | Status | Event   |
          | 1        | 2025-02-01T10:00 | 2025-02-01T10:10 | 2              |        |         |
          | 1        | 2025-02-01T10:10 | 2025-02-01T10:20 | 0              |        |         |
          | 1        | 2025-02-01T10:20 | 2025-02-01T10:30 | 4              | Stängd |         |
          | 1        | 2025-02-01T10:30 | 2025-02-01T10:40 | 4              |        | Tävling |
          | 1        | 2025-02-01T10:40 | 2025-02-01T10:50 | 4              |        |         |
        When the service is triggered to search
        Then notifications are sent to
          | Email          | CourseName  | TeeTime          | AvailableSlots |
          | user@email.com | GolfCourse1 | 2025-02-01T10:00 | 2              |
          | user@email.com | GolfCourse1 | 2025-02-01T10:40 | 4              |