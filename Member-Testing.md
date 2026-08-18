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

---------------------------------

## Test Case #3

### Endpoint
POST /api/members

### Scenario
Create Member with an invalid Egyptian phone number.

### Request
```json
{
  "fullName": "Omar Hassan",
  "phone": "12345",
  "email": "omar.hassan@gmail.com",
  "gender": 1,
  "dateOfBirth": "1998-03-15",
  "emergencyContact": "01234567890",
  "membershipStartDate": "2026-08-12",
  "membershipEndDate": "2026-09-12",
  "membershipPlanId": 2,
  "packageId": null
}
```

### Expected Result
- Status Code: **400 Bad Request**
- Validation error for **Phone**
- No record should be inserted into the database.

### Actual Result
- Status Code: **400 Bad Request**
- Validation Message:
  - `Phone number must be a valid Egyptian phone number.`
- No record was inserted into the `Members` table.

### Validation Response
```json
{
  "errors": {
    "Phone": [
      "Phone number must be a valid Egyptian phone number."
    ]
  }
}
```

### Database Verification
- ✅ No new record was created.
- ✅ Database remained unchanged.

### Notes
- The request was rejected by **FluentValidation** before reaching the Service layer.
- The API correctly prevented invalid phone numbers from being processed.

### Result
✅ **Passed**


---------------------------------

# Test Case #4 - Create Member With Duplicate Email

## Objective
Verify that the API prevents creating two members with the same email.

---

## Endpoint
POST /api/Members

---

## Request Body
```json
{
  "fullName": "Ahmed Mohamed",
  "phone": "01011112222",
  "email": "ahmed@gmail.com",
  "gender": "Male",
  "dateOfBirth": "2000-05-10",
  "emergencyContact": "01099999999",
  "membershipStartDate": "2026-08-12",
  "membershipEndDate": "2026-09-12",
  "membershipPlanId": 1
}
```

---

## Expected Result
* Status Code: **400 Bad Request**
* BusinessException should be thrown.
* No new member should be inserted into the database.

---

## Actual Result
Status Code:
400 Bad Request
Response:

```json
{
  "success": false,
  "message": "A member with this email already exists.",
  "data": null,
  "errors": null,
  "statusCode": 400
}
```
Database:
* No new record inserted.

---

## Result
✅ Passed

---------------------------------


# Test Case #5 - Get Existing Member By Id

## Objective
Verify that the API returns the member details successfully when a valid member ID is provided.

---

## Endpoint
GET /api/Members/1

---

## Expected Result
* Status Code: **200 OK**
* Returns the member information.
* No changes should occur in the database.
* No exception should be thrown.

---

## Actual Result
Status Code:
200 OK
Response:
```json
{
  "id": 1,
  "fullName": "Ahmed Mohamed",
  "phone": "01012345678",
  "email": "ahmed@gmail.com",
  "gender": "Male",
  "dateOfBirth": "2000-05-10",
  "emergencyContact": "01099999999",
  "status": "Active",
  "membershipStartDate": "2026-08-12",
  "membershipEndDate": "2026-09-12",
  "membershipPlanId": 1,
  "membershipPlanName": "Basic",
  "packageId": null,
  "packageName": null
}
```

---

## Database Verification
* UpdatedAt: **NULL**
* PackageId: **NULL**
* Status: **Active (1)**

No data was modified.

---

## Exception
No exception was thrown.

---

## Result
✅ Passed


---------------------------------


# Test Case #6 - Get Member By Invalid Id

## Objective
Verify that the API returns **404 Not Found** when requesting a member that does not exist.

---

## Endpoint
GET /api/Members/123456

---

## Expected Result
* Status Code: **404 Not Found**
* Throws `NotFoundException`.
* Returns the standard API error response.
* No changes should occur in the database.

---

## Actual Result
Status Code:
404 Not Found
Response:
```json
{
  "success": false,
  "message": "Member was not found.",
  "data": null,
  "errors": null,
  "statusCode": 404
}
```

---

## Database Verification
No changes were made to the database.

---

## Exception
`NotFoundException` was thrown and handled successfully by `GlobalExceptionMiddleware`.

---

## Result
✅ Passed


---------------------------------

# Test Case #7 - Route Constraint Validation (Negative Member Id)

## Objective
Verify that the API rejects invalid route values (negative member IDs) before reaching the Controller by using ASP.NET Core Route Constraints.

---

## Endpoint
GET /api/Members/-1

---

## Route Constraint
```csharp id="r9k2d1"
[HttpGet("{memberId:int:min(1)}")]
```

---

## Expected Result
* The request should be rejected because the route parameter must be greater than or equal to **1**.
* The request must **not** reach the Controller.
* The Service layer must **not** execute.
* The Database must **not** be queried.

---

## Actual Result
* The request was rejected successfully.
* The Controller was never executed.
* No database query was performed.
* The API did not attempt to search for a member with an invalid ID.

---

## Refactoring Applied
Before:
```csharp id="qv2r4e"
[HttpGet("{memberId:int}")]
```

After:
```csharp id="n8m6sj"
[HttpGet("{memberId:int:min(1)}")]
```

The same improvement was applied to all endpoints that receive `memberId`.

---

## Result
✅ Passed


---------------------------------

## Test Case #8 - Get All Members Without Filters

### Objective
Verify that the API successfully retrieves all members when no search or filter parameters are provided.

### Endpoint
`GET /api/members`

### Request
No query parameters were provided.

### Expected Result
* Status Code: `200 OK`
* Return a paginated result.
* `PageNumber` should be `1`.
* `PageSize` should be `10`.
* `TotalCount` should reflect the total number of members.
* `Items` should contain the available members.
* No database modification should occur.

### Actual Result
**Status Code:** `200 OK`
**Response:**
```json
{
  "items": [
    {
      "id": 1,
      "fullName": "Ahmed Mohamed",
      "phone": "01012345678",
      "status": "Active",
      "membershipPlanType": "Basic",
      "packageName": null,
      "membershipEndDate": "2026-09-12"
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 1
}
```

### Database Verification
* No new member was inserted.
* No existing member was updated.
* No member was deleted.
* Database state remained unchanged.

### Exceptions
No exception occurred.

### Result
✅ **PASSED**

--------------------------------



