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


