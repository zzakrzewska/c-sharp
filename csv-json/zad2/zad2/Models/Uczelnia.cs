namespace zad2.Models;

public class Uczelnia
{
    public DateTime CreatedAt { get; set; }
    public string Author { get; set; }
    public IEnumerable<Student> Students { get; set; }
    public IEnumerable<ActiveStudies> ActiveStudies { get; set; }
}