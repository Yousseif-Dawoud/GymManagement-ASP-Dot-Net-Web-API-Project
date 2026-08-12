# Member API Testing

---------------------------------

## Test Case 1

### Endpoint
POST /api/members

### Scenario
Create a member with valid data.

### Expected Result
- 201 Created
- Member saved successfully
- Status = Active
- Package = null

### Actual Result
Passed ✅

### Database Verification
- CreatedAt ✔
- UpdatedAt ✔
- Email normalized ✔
- MembershipPlan linked ✔
- Package null ✔

### Notes
No issues found.

---------------------------------

## Test Case #2

### Endpoint
POST /api/members

### Scenario
Create member with empty FullName.

### Expected
400 Bad Request

### Actual
400 Bad Request

### Validation Message
'Full Name' must not be empty.

### Database Verification
No record inserted.

### Result
✅ Passed
