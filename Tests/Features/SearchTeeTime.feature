Feature: Search tee times logic tests

    Scenario: Given a tee time was found
        Given current date is 2025-01-01
        Given courses exist
          | Id | Name          |
          | 1  | Golf Course 1 |
        And users subscribed to
          | Email          | CourseId | FromTime         | ToTime           | NumberOfPlayers |
          | user@email.com | 1        | 2025-02-01T09:00 | 2025-02-01T12:00 | 2               |
        And sweetspot has bookings
          | CourseId | FromTime         | ToTime           | AvailableSlots | Status | Event |
          | 1        | 2025-02-01T09:00 | 2025-02-01T09:10 | 2              |        |       |
        When the service is triggered to search