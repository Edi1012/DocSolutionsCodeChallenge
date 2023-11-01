# DocSolutionsCodeChallenge

## Prerequisites to Run this API

Before running this API, you need to follow these steps:

### 1. Create a Database

Execute the following SQL script to create the necessary stored procedures and table:

```sql
USE [DocsSolutionsCodeChallenge]
GO

-- Stored Procedure: EmployeeLogin
-- This procedure checks if an employee with the provided user and password exists.
CREATE PROCEDURE [dbo].[EmployeeLogin]
    @inputUser NVARCHAR(255),
    @inputPassword NVARCHAR(255)
AS
BEGIN
    DECLARE @EmployeeCount INT;

    -- Check if an employee with the provided user and password exists
    SELECT @EmployeeCount = COUNT(*)
    FROM employees
    WHERE [user] = @inputUser AND [password] = @inputPassword;

    -- If an employee with the provided credentials exists, return 1; otherwise, return 0
    IF @EmployeeCount > 0
        SELECT 1 AS Result;  -- Employee exists
    ELSE
        SELECT 0 AS Result;  -- Employee does not exist
END;
GO

-- Stored Procedure: InsertEmployee
-- This procedure inserts employee data into the 'employees' table.
CREATE PROCEDURE [dbo].[InsertEmployee]
    @name NVARCHAR(255),
    @user NVARCHAR(255),
    @password NVARCHAR(255)
AS
BEGIN
    INSERT INTO employees (name, [user], [password])
    VALUES (@name, @user, @password);
END;
GO

-- Stored Procedure: ListEmployees
-- This procedure retrieves a list of employees from the 'employees' table.
CREATE PROCEDURE [dbo].[ListEmployees]
AS
BEGIN
    SELECT Id, name, [user], [password] FROM employees;
END;
GO

-- Stored Procedure: UserEmployeeExist
-- This procedure checks if an employee with the provided user exists.
CREATE PROCEDURE [dbo].[UserEmployeeExist]
    @user NVARCHAR(255)
AS
BEGIN
    DECLARE @EmployeeCount INT;

    -- Check if an employee with the provided user exists
    SELECT @EmployeeCount = COUNT(*)
    FROM employees
    WHERE [user] = @user;

    -- Return the count (1 if employee exists, 0 otherwise)
    SELECT @EmployeeCount AS Result;
END;
GO

-- Table: Employees
-- This table stores employee information.
SET ANSI_PADDING ON;
CREATE TABLE [dbo].[Employees](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [name] [varchar](100) NULL,
    [user] [varchar](100) NOT NULL,
    [password] [varchar](100) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF;
```

### 2. Insert Data

Insert the following data into the 'Employees' table:

```sql
INSERT INTO [dbo].[Employees] ([Id], [name], [user], [password])
VALUES (1, 'Isaac', 'admin', '202cb962ac59075b964b07152d234b70');
```

### 3. Modify the Connection String in `appsettings.json`

Update the connection string in the `appsettings.json` file with the appropriate values for your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=DESKTOP-I3SE1BF; TimeOut=130; Initial Catalog=DocsSolutionsCodeChallenge; Persist Security Info=True; User ID=sa;Password=123;"
  },
  // Other settings...
}
```

Now you can run the API with the database and data properly configured.
