# User Data Fetcher - Practical Test

This is a .NET 8.0 console application that fetches user data from multiple public APIs and saves the combined result into a file in either JSON or CSV format.

## Features

- Fetches users from the following APIs:
  1. https://randomuser.me/api/?results=500
  2. https://jsonplaceholder.typicode.com/users
  3. https://dummyjson.com/users
  4. https://reqres.in/api/users

- Extracts the following user details:
  - First Name
  - Last Name
  - Email
  - Source ID (unique ID from the data source)

- Prompts the user to input:
  - The folder path where the output file will be saved
  - The desired file format (`json` or `csv`)

- Saves all users to a single file, overwriting if the file already exists

- Designed for quick execution with asynchronous API calls running in parallel

- Easily extendable: Adding a new API source requires minimal code changes thanks to the `IUserSource` interface

## How to run

1. Clone the repository
2. Build the project using .NET 8.0 SDK
3. Run the console app
4. Follow the prompts to enter the folder path and choose the file format

## Notes

- If you encounter any issues or have questions, feel free to contact me via email.

---

Good luck and thank you for the opportunity!

