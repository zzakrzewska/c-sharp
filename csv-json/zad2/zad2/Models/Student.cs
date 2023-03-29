namespace zad2.Models;

public class Student
{
    public string IndexNumber { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public DateTime Birthdate { get; set; }
    public string Email { get; set; }
    public string MothersName { get; set; }
    public string FathersName { get; set; }
    public Studies Studies { get; set; }
}