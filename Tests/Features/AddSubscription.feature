Feature: AddSubscription

    Scenario: User subscribes to a course and time
        Given courses exist
          | Id | Name          |
          | 1  | Golf Course 1 |
          | 2  | Golf Course 2 |
        Given users subscribed to
          | Email               | CourseId | FromTime         | ToTime           | NumberOfPlayers |
          | user@email.com      | 1        | 2025-01-01T09:00 | 2025-01-01T12:00 | 2               |
          | user@email.com      | 2        | 2025-02-01T09:00 | 2025-02-01T12:00 | 3               |
          | otherUser@email.com | 2        | 2025-02-01T09:00 | 2025-02-01T12:00 | 3               |
        And a user with email user@email.com wants to get subscriptions
        When the request is made
        Then the subscription are returned
          | Email          | CourseId | FromTime         | ToTime           | NumberOfPlayers |
          | user@email.com | 1        | 2025-01-01T09:00 | 2025-01-01T12:00 | 2               |
          | user@email.com | 2        | 2025-02-01T09:00 | 2025-02-01T12:00 | 3               |

    Scenario: User subscribes to an invalid course
        Given a user wants to subscribe to
          | Email          | CourseId | FromTime         | ToTime           | NumberOfPlayers |
          | user@email.com | 3        | 2025-01-01T09:00 | 2025-01-01T12:00 | 2               |
        When the request is made
        Then the request is not successful with
          | status | message               |
          | 400    | Course does not exist |