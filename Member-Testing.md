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

## Test Data Preparation — Members

Before starting the pagination and filtering test scenarios, additional member records were created to provide a realistic dataset for testing.

### Purpose

The purpose of this step is to prepare enough and sufficiently varied member data to test:

* Pagination
* Search
* Membership status filtering
* Membership plan filtering
* Combination filters
* Empty-result scenarios

### Existing Data

Member ID `1` already existed in the database:

* **Full Name:** Ahmed Mohamed
* **Phone:** 01012345678
* **Email:** [ahmed@gmail.com](mailto:ahmed@gmail.com)
* **Gender:** Male
* **Membership Plan:** Basic
* **Status:** Active

### Additional Members Created

The following 11 members were added successfully through the `POST /api/members` endpoint using Swagger:

| ID | Full Name       | Gender | Membership Plan | Status  |
| -: | --------------- | ------ | --------------- | ------- |
|  2 | Ali Hassan      | Male   | Basic           | Active  |
|  3 | Omar Khaled     | Male   | Premium         | Active  |
|  4 | Mohamed Adel    | Male   | Basic           | Frozen  |
|  5 | Youssef Ibrahim | Male   | Premium         | Active  |
|  6 | Karim Ahmed     | Male   | Basic           | Expired |
|  7 | Sara Mohamed    | Female | Premium         | Active  |
|  8 | Menna Ali       | Female | Basic           | Frozen  |
|  9 | Nour Khaled     | Female | Premium         | Active  |
| 10 | Mahmoud Samir   | Male   | Basic           | Active  |
| 11 | Hossam Tarek    | Male   | Premium         | Expired |
| 12 | Mariam Adel     | Female | Basic           | Active  |

### Data Distribution

After adding the records, the test dataset contains **12 members** in total.

#### Membership Status Distribution

| Status    |  Count |
| --------- | -----: |
| Active    |      8 |
| Frozen    |      2 |
| Expired   |      2 |
| **Total** | **12** |

#### Membership Plan Distribution

| Membership Plan |  Count |
| --------------- | -----: |
| Basic           |      6 |
| Premium         |      6 |
| **Total**       | **12** |

### Membership Dates

The newly created members use the following membership period:

* **Membership Start Date:** `2026-09-20`
* **Membership End Date:** `2027-09-20`

This dataset provides sufficient variation for the upcoming pagination and filtering scenarios.

### Verification

The members were created successfully through the API, and the resulting records were available in the database.

Expected total member count:

```sql
SELECT COUNT(*) AS TotalMembers
FROM Members;
```

Expected result:

```text
TotalMembers = 12
```

### Result

**Status: PASS**

The database now contains the required test dataset for the upcoming member search and pagination tests.



--------------------------------



## Test Case #9 — Pagination

### Test Case Information

| Field         | Details                                                                                        |
| ------------- | ---------------------------------------------------------------------------------------------- |
| Test Case ID  | TC-09                                                                                          |
| Feature       | Member Pagination                                                                              |
| Endpoint      | `GET /api/members`                                                                             |
| Method        | `GET`                                                                                          |
| Objective     | Verify that members are correctly divided into pages according to `pageNumber` and `pageSize`. |
| Preconditions | Database is available, migrations are applied, API is running, and 12 members exist.           |

---

### 1. Test Scenario

Retrieve the first page of members with a page size of 5.

### Request

```http
GET /api/members?pageNumber=1&pageSize=5
```

---

### 2. Expected Result

The API should:

* Return `200 OK`.
* Return exactly 5 members in the `items` collection.
* Return `pageNumber = 1`.
* Return `pageSize = 5`.
* Return `totalCount = 12`.
* Order members by `FullName`.
* Use `Id` as a secondary ordering criterion to provide deterministic ordering when multiple members have the same name.
* Not modify any database records.

Expected pagination distribution:

```text
Total Members = 12
Page Size     = 5

Page 1 → 5 Members
Page 2 → 5 Members
Page 3 → 2 Members
```

---

### 3. Prediction Before Execution

Before executing the request, the expected response was predicted as follows:

```text
Status Code  → 200 OK
Items Count  → 5
Page Number  → 1
Page Size    → 5
Total Count  → 12
```

It was also predicted that the members would be ordered alphabetically by `FullName`.

---

### 4. Actual Result

The API returned:

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
    },
    {
      "id": 2,
      "fullName": "Ali Hassan",
      "phone": "01012345679",
      "status": "Active",
      "membershipPlanType": "Basic",
      "packageName": null,
      "membershipEndDate": "2027-09-20"
    },
    {
      "id": 11,
      "fullName": "Hossam Tarek",
      "phone": "01012345688",
      "status": "Active",
      "membershipPlanType": "Premium",
      "packageName": null,
      "membershipEndDate": "2027-09-20"
    },
    {
      "id": 6,
      "fullName": "Karim Ahmed",
      "phone": "01012345683",
      "status": "Active",
      "membershipPlanType": "Basic",
      "packageName": null,
      "membershipEndDate": "2027-09-20"
    },
    {
      "id": 10,
      "fullName": "Mahmoud Samir",
      "phone": "01012345687",
      "status": "Active",
      "membershipPlanType": "Basic",
      "packageName": null,
      "membershipEndDate": "2027-09-20"
    }
  ],
  "pageNumber": 1,
  "pageSize": 5,
  "totalCount": 12
}
```

### Result Verification

| Verification          | Expected |   Actual | Result |
| --------------------- | -------: | -------: | ------ |
| HTTP Status           |      200 |      200 | PASS   |
| Items Count           |        5 |        5 | PASS   |
| Page Number           |        1 |        1 | PASS   |
| Page Size             |        5 |        5 | PASS   |
| Total Count           |       12 |       12 | PASS   |
| Ordering              | FullName | FullName | PASS   |
| Database Modification |     None |     None | PASS   |

---

### 5. Why `totalCount = 12`

`totalCount` represents the total number of records matching the applied filters before pagination is applied.

The repository calculates it before `Skip()` and `Take()`:

```csharp
var totalCount = await query.CountAsync(ct);
```

Pagination is then applied:

```csharp
.Skip((request.PageNumber - 1) * request.PageSize)
.Take(request.PageSize)
```

Therefore:

```text
Total Members = 12
Page Size     = 5
```

results in:

```text
Page 1 → 5 items
Page 2 → 5 items
Page 3 → 2 items
```

while `totalCount` remains `12` for all three pages.

---

### 6. Implementation Verification

The search query follows this processing order:

```text
WHERE
  ↓
COUNT
  ↓
ORDER
  ↓
PAGINATION
  ↓
SELECT
```

The default ordering is:

```csharp
.OrderBy(m => m.FullName)
.ThenBy(m => m.Id)
```

`FullName` is the primary ordering criterion, while `Id` acts as a deterministic tie-breaker when multiple members have the same name.

Pagination is applied after ordering:

```csharp
.Skip((request.PageNumber - 1) * request.PageSize)
.Take(request.PageSize)
```

This ensures that pagination is applied to a consistently ordered dataset.

---

### 7. Refactoring During Testing

During the test review, the existing ordering:

```csharp
.OrderBy(m => m.FullName)
```

was reviewed.

The ordering was functionally correct, but a secondary ordering criterion was added to make the result deterministic when multiple members have the same `FullName`.

Refactored implementation:

```csharp
.OrderBy(m => m.FullName)
.ThenBy(m => m.Id)
```

The test was executed again after the refactoring.

### Retest Result

The response remained correct and matched the expected pagination behavior.

**Retest: PASS**

---

### 8. Database Verification

The database was checked after executing the request:

```sql
SELECT
    Id,
    FullName,
    Status,
    MembershipPlanId
FROM Members
ORDER BY Id;
```

The database contained 12 members and no records were modified by the `GET` request.

### Database Result

**No database modification detected.**

---

### 9. Exceptions

No exception occurred during execution.

---

### 10. Final Result

**Status: PASS**

The pagination functionality works correctly.

The test successfully verified:

* Correct HTTP status code.
* Correct page size.
* Correct page number.
* Correct total record count.
* Correct alphabetical ordering.
* Deterministic secondary ordering using member ID.
* Correct pagination behavior.
* No database modification.
* Successful retest after refactoring.


--------------------------------