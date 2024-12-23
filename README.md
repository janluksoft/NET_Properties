# Properties in C# - advanced control of 'init', 'get' accessors

## Description

This .NET 9 C# program: demonstrates the use of properties and advanced control of {init, get} accessors in classes and records with C# 11 syntax.
The program uses properties, data validation, records, and new features introduced in C# versions after 6. It reads a list of people with attributes such as name, postal code, age, and date of birth. The program validates the data and reports errors if invalid input is encountered.

## Features
- **Records (`record`)**: Uses immutable data structures (`RecPerson`) with `init` properties.
- **Data Validation**: Checks for invalid names, postal codes, ages, and dates of birth.
- **Interface with Default Methods (`IPrintPerson`)**: Implements an interface with method definitions.
- **Index from End (`^` Operator)**: Uses `List[^1]` to access the last element in a list.
- **String Manipulation**: Trims and formats names while preserving spaces in records.
- **Regex Validation**: Ensures postal codes and names follow specific formats.
- **Exception Handling**: Catches invalid input and prevents program crashes.

## Program Breakdown

### 1. **Records for Immutable Data**
```csharp
public record RecPerson(string FirstName, string SurName, string PostCode, int Age, string DateBr)
```
- Uses `record` to enforce immutability.
- `init;` properties allow setting values only at initialization.
- Name validation is included in the constructor.

### 2. **Person Class with Validations**
```csharp
public class Person
```
- Uses `string?` for nullable properties.
- Implements validation methods:
  - `ValidateName()` prevents numbers in names.
  - `ValidateDate()` ensures correct date formatting.
  - `PostCode` validates postal code format.

### 3. **Exception Handling and Input Processing**
- Errors in records trigger an exception, stopping further processing.
- Errors in `Person` objects are handled individually, allowing the program to continue.

### 4. **Interface for Printing (`IPrintPerson`)**
- Uses default method implementations (`C# 8`).
- Provides formatted output for `RecPerson` and `Person` objects.

## Summary of C# > 6 Features Used

| Feature | C# Version | Usage |
|---------|------------|------------------------------------------------|
| **Records (`record`)** | C# 9 | `RecPerson` for immutable objects |
| **`init;` Properties** | C# 9 | Restricts property modification after initialization |
| **Index from End (`^` Operator)** | C# 8 | Accesses the last element in a list |
| **Interface Method Implementation** | C# 8 | Default method bodies in `IPrintPerson` |
| **Regex Validation** | - | Ensures name, postal code, and date formats |
| **`field` keyword** | C# 11 | Used for auto-implemented properties |

## Example Console Output

```
Proposal:  Person: Irena  Szewinska  Code: 00-432, Age: 54, BirthDate: 1965-12-11.
   Entry:  Person: Irena Szewinska   Code: 00-432, Age: 54, BirthDate: 1965-12-11.
Proposal:  Person:    Iga   Swiatek  Code: 00-432, Age: 31, BirthDate: 1994-48-01.
           ERROR:    Invalid date format [1994-48-01]. Use YYYY-MM-DD.
Proposal:  Person:   Mark     Twain  Code: 00-432, Age: 120, BirthDate: 1905-03-24.
           ERROR:    The Age [120] is too big.
Proposal:  Person:    Tom    Pid7ck  Code: 22-435, Age: 27, BirthDate: 2000-12-01.
           ERROR:    The name [   Pid7ck] contains numbers!
Proposal:  Person: Zhang     Lin     Code: 32-471, Age: 33, BirthDate: 1992-11-15.
   Entry:  Person: Zhang Lin         Code: 32-471, Age: 33, BirthDate: 1992-11-15.
```

## Conclusion
This program demonstrates modern C# features and best practices for working with properties, validation, and data integrity using records and interfaces.

