# Welcome to the LawSchool API

This is a simple api built with C# language and .NET framework that. Please feel free to clone or fork the repository for your own learning purposes. This is **FOR LEARNING ONLY**, it is not fit for production grade software. I welcome criticisms and suggestions to improve my code.

## Note

- The API program can run in a docker container and it is the recommended usage.
- Make sure you have docker (docker desktop for windows) installed on your local machine.

## Usage

Clone the repository

A `docker-compose.yml` file is available at the root of the repository

Run

```bash
docker compose up --build
```

## API Endpoints

### POST `/api/v1/students`

- Request body

```json
{
    "firstName": "student first name",
    "lastName": "student last name",
    "email": "student's email",
    "department": "student's department (preferably a law department)",
    "GPA": 4.00 // A floating point number in two decimal places ,
}
```

- Response body

```json
{
    "id": 1,
    "fullName": "first name + last name",
    "email": "student's email",
    "department": "student's department (preferably a law department)",
    "GPA": 4.00 // A floating point number in two decimal places ,
}
```

### GET `/api/v1/student/{id}`

- Response body

```json
{
    "id": 1,
    "fullName": "first name + last name",
    "email": "student's email",
    "department": "student's department (preferably a law department)",
    "GPA": 4.00 // A floating point number in two decimal places ,
}
```

### GET `/api/v1/students`

- Request query parameters
The request query parameters are available to filter results
  - pageNumber: The page you want to navigate to
  - pageSize: The number of results you want on each page
  - department: The department from which you want the results
  - minGpa: The lowest gpa score for results
  - maxGpa: The highest gpa score for results
  
For example, you want to have 10 students per page and you want to go to page 1, and you want the results of your request to contain only students from civil law that have gpa scores between 4.00 and 4.45 i.e ```4.00 <= gpa >= 4.45```, then your request url will look like this:

`
localhost:5125/api/v1/students?pageNumber=1&pageSize=10&department=civil%20law&minGpa=4&maxGpa=4.45
`

Note that `%20` means space character.

- Response body

```json
[
    {
        "id": 1,
        "fullName": "first name + ' ' + last name",
        "email": "student's email",
        "department": "Civil Law (preferably a law department)",
        "GPA": 4.00 // A floating point number in two decimal places ,
    },
    {
        "id": 1,
        "fullName": "first name + ' ' + last name",
        "email": "student's email",
        "department": "Civil Law (preferably a law department)",
        "GPA": 4.00 // A floating point number in two decimal places ,
    }
    ...
]

```

### GET `/api/v1/students/{id}`

- Response body

```json
{
    "id": 1,
    "fullName": "first name + ' ' + last name",
    "email": "student's email",
    "department": "Civil Law (preferably a law department)",
    "GPA": 4.00 // A floating point number in two decimal places ,
}
```

### DELETE `/api/v1/students/{id}`

- Response body: An empty body is returned with a 204 status code
