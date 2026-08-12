USE GymManagmentDB;
GO

INSERT INTO MembershipPlans
(
    Type,
    Price,
    Description,
    DurationInDays,
    MaxSessionsPerMonth,
    IncludesPersonalTrainer,
    IsActive,
    CreatedAt,
    UpdatedAt
)
VALUES
(
    1,
    300,
    'Basic membership plan',
    30,
    12,
    0,
    1,
    GETUTCDATE(),
    NULL
),
(
    2,
    500,
    'Standard membership plan',
    30,
    20,
    0,
    1,
    GETUTCDATE(),
    NULL
),
(
    3,
    800,
    'Premium membership plan',
    30,
    999,
    1,
    1,
    GETUTCDATE(),
    NULL
);