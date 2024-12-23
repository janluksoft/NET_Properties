using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace pN9_Prop2; // ✅ C# 10: File-scoped namespace

internal class Program : IPrintPerson
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        IPrintPerson printer = new Program(); // Use interface reference


        try
        {   // ✅ C# 9: Records with init-only properties
            var ListRecord = new List<RecPerson>
            {
                new("Irena ", "Szewinska", "00-432", 54, "1965-12-11"),
                new("   Ewa", "  Swoboda", "00-432", 25, "2000-07-08"),
                new("   Iga", "  Swiatek", "00-432", 31, "1994-48-01"),
                new("Serena", "  Wiliams", "00-432", 44, "1981-01-14"),
                new("  Mark", "    Twain", "00-432",120, "1905-03-24"),
                new("   Tom", "   Pid7ck", "22-432y",27, "2000-12-01"),
                new("Marita", "     Koch", "32-471", 45, "20FR-07-09"),
                new("Thomas", "   Ceccon", "WN-432", 29, "1996-05-05"),
                new("Javier", "Sotomayor", "74-832", 43, "1983-07-04"),
                new("Zhang ", "   Lin   ", "32-471", 33, "1992-11-15")
            };

            var ListPerson = new List<Person>();

            foreach (var item in ListRecord)
            {
                ListPerson.Add(new Person());
                Person cPerson = ListPerson[^1]; // ✅ C# 8: Index from end

                try
                {
                    printer.PrintPersonObj("Proposal: ", item); //Print data in from Records. Works!

                    cPerson.SetFields(item.FirstName, item.SurName, item.PostCode, item.Age, item.DateBr);
                    printer.PrintPersonObj("   Entry: ", cPerson); //Print data out from class. Works!
                }
                catch (Exception e)
                {
                    Console.WriteLine("           ERROR:    " + e.Message);
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error in the Record: " + e.Message);
        }
    }
}

// ✅ C# 9: Records with `init;` properties
public record RecPerson(string FirstName, string SurName, string PostCode, int Age, string DateBr)
{
    public string FirstName { get; init; } = ValidateName(FirstName);
    public string SurName { get; init; } = SurName;
    public string PostCode { get; init; } = PostCode;
    public int Age { get; init; } = Age;
    public string DateBr { get; init; } = DateBr;

    private static string ValidateName(string name)
    {
        if (Regex.IsMatch(name, @"\d")) throw new Exception($"The name [{name}] contains numbers!");
        return name;
    }
}

// ✅ Updated Person class
public class Person
{
    // ✅ C# 11: word 'field'
    public string? FirstName
    { get => field; set => field = ValidateName(value); }

    public string? SurName
    { get => field; set => field = ValidateName(value); }

    public string? PostCode
    {
        get => field;
        set
        {
            if (field == value) return;
            if (!Regex.IsMatch(value, @"^\d{2}-\d{3}$"))
                throw new Exception($"Invalid PostCode format [{value}]. Expected: '00-000'.");
            field = value;
        }
    }

    public int Age
    {
        get => field;
        set
        {
            if (value > 110) throw new Exception($"The Age [{value}] is too big.");
            field = value;
        }
    }

    public string? DateBr
    {
        get => field;
        set => field = ValidateDate(value);
    }

    public void SetFields(string? firstName, string? surName, string? postCode, int age, string dateBr)
    {
        FirstName = firstName;
        SurName = surName;
        PostCode = postCode;
        Age = age;
        DateBr = dateBr;
    }

    private static string ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new Exception("Name cannot be empty.");
        if (Regex.IsMatch(name, @"\d")) throw new Exception($"The name [{name}] contains numbers!");
        return name.Trim();
    }

    private static string ValidateDate(string? date)
    {
        if (!DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
            throw new Exception($"Invalid date format [{date}]. Use YYYY-MM-DD.");
        return date;
    }
}

// ✅ IPrintPerson interface
public interface IPrintPerson
{
    // ✅ C# 8: Body of method in interface
    public void PrintPersonObj(string xInfo, Person xR) => 
        PrintPerson(xInfo, xR.FirstName, xR.SurName, xR.PostCode, xR.Age, xR.DateBr);
    void PrintPersonObj(string xInfo, RecPerson xR) => 
        PrintPerson(xInfo, xR.FirstName, xR.SurName, xR.PostCode, xR.Age, xR.DateBr);

    void PrintPerson(string xInfo, string xFname, string xSname, string xCode, int xAge, string xDate)
    {
        string mess = $"Person: {FixedString(xFname + " " + xSname, 16)}" +
                      $"  Code: {xCode}, Age: {xAge}, BirthDate: {xDate}.";
        Console.WriteLine(xInfo + " " + mess);
    }

    string FixedString(string xmessage, int xLen)
    {
        return (xmessage.PadRight(xLen).Substring(0, xLen));
    }
}
