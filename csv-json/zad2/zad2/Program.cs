using System.Runtime.InteropServices.JavaScript;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using zad2.Models;

//@"/Users/zosia/uczelnia/2 rok/2 semestr/apbd/cwiczenia2_ko-zzakrzewska/dane.csv"

string csvPath;
string outputPath;
string logsPath;
string outputFormat;

if (args.Length == 4)
{
    csvPath = args[0];
    if (!File.Exists(csvPath))
    {
        throw new FileNotFoundException();
    }
    
    outputPath = args[1];
    if (!Directory.Exists(outputPath))
    {
        throw new DirectoryNotFoundException();
    }
    
    logsPath = args[2];
    if (!File.Exists(logsPath))
    {
        throw new FileNotFoundException();
    }
    
    outputFormat = args[3];
    if (!outputFormat.Equals("json"))
    {
        throw new InvalidOperationException();
    }
}
else
{
    throw new ArgumentOutOfRangeException();
}

var csvContent = File.ReadLines(csvPath);
var students = new List<Student>();
var logs = File.CreateText(logsPath);
logs.AutoFlush = true;

var dict = new Dictionary<string, int>();

csvContent.ToList().ForEach(row =>
{
    var splitted = row.Split(",");
    
    if (splitted.Length != 9)
    {
        logs.WriteLine($"Wiersz nie posiada odpowiedniej ilości kolumn: {row}");
        return;
    }

    if (splitted.Any(e => e.Trim() == ""))
    {
        logs.WriteLine($"Wiersz nie może posiadać pustych kolumn: {row}");
        return;
    }

    var studies = new Studies
    {
        Name = splitted[2],
        Mode = splitted[3],
    };
    
    var student = new Student
    {
        IndexNumber = splitted[4],
        Fname = splitted[0],
        Lname = splitted[1],
        Birthdate = DateTime.Parse(splitted[5]),
        Email = splitted[6],
        MothersName = splitted[7],
        FathersName = splitted[8],
        Studies = studies,
    };

    if (students.Any(e =>
            e.Fname == student.Fname && e.Lname == student.Lname && e.IndexNumber == student.IndexNumber))
    {
        logs.WriteLine($"Duplikat: {row}");
    }

    dict[studies.Name] = dict.ContainsKey(studies.Name) 
        ? ++dict[studies.Name] 
        : 1;

    students.Add(student);
});

var json = JsonSerializer.Serialize(new UczelniaWrapper {
        uczelnia = new Uczelnia {
            CreatedAt = DateTime.Now,
            Author = "ko",
            Students = students,
            ActiveStudies = dict.Select(e =>
                new ActiveStudies {
                    Name = e.Key,
                    NumberOfStudents = e.Value
                })
        }
    }, 
    new JsonSerializerOptions {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    });

File.WriteAllText((outputPath+"studenci.json"), json);
Console.WriteLine(json);
