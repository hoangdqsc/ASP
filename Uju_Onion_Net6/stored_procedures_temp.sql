CREATE PROCEDURE GetUserById
    @Id INT
AS
BEGIN
    SELECT * FROM Users WHERE Id = @Id
END
GO

CREATE PROCEDURE GetAllUsers
AS
BEGIN
    SELECT * FROM Users
END
GO

CREATE PROCEDURE AddUser
    @Username NVARCHAR(50),
    @Email NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @Roles NVARCHAR(50)
AS
BEGIN
    INSERT INTO Users (Username, Email, PasswordHash, Roles)
    VALUES (@Username, @Email, @PasswordHash, @Roles)
END
GO

CREATE PROCEDURE UpdateUser
    @Id INT,
    @Username NVARCHAR(50),
    @Email NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @Roles NVARCHAR(50)
AS
BEGIN
    UPDATE Users
    SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Roles = @Roles
    WHERE Id = @Id
END
GO

CREATE PROCEDURE DeleteUser
    @Id INT
AS
BEGIN
    DELETE FROM Users WHERE Id = @Id
END
GO
