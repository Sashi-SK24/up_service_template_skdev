# Unified Platform Windows Service Template (Work in Progress)


## Requirements

- .NET 7.0
- Microsoft SQL Server 2022 Express

## Getting Started


### Database Migration
--- 
1. **Creating a new migration file** 
    
    **(Only applicable for new Models)*
    
    - PowerShell Command
    ```
    Add-Migration InitialCreate
    ```

    - .NET Core CLI Command
    ```
    dotnet ef migrations add InitialCreate
    ```

    ---
2. **Applying migration file / Update Database**
    
    - PowerShell Command
    ```
    Update-Database
    ```

    - .NET Core CLI Command
    ```
    dotnet ef database update
    ```

    ---
3. **Remove the last migration file**
    
    - PowerShell Command
    ```
    Remove-Migration
    ```

    - .NET Core CLI Command
    ```
    dotnet ef migrations remove
    ```

    ---
4. **Listing all migration files**
    
    - PowerShell Command
    ```
    Get-Migration
    ```

    - .NET Core CLI Command
    ```
    dotnet ef migrations list
    ```

### Development
