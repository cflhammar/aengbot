Feature: AddSubscription

    Scenario: User subscribes to a course and time
        Given courses exist
          | Id | Name        |
          | 1        | Golf Course |
        Given a user subscribed to
          | Email          | CourseId | FromTime         | ToTime           | NumberOfPlayers |
          | user@email.com | 1        | 2025-01-01T09:00 | 2025-01-01T12:00 | 2               |
#        And a user with email user@email.com wants to get subscriptions
#        Then the subscription are returned
#          | Email          | CourseId | FromTime         | ToTime           | NumberOfPlayers |
#          | user@email.com | 1        | 2025-01-01T09:00 | 2025-01-01T12:00 | 2               |