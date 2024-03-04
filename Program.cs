namespace Database;

class Program
{
  public static void Main(string[] args)
  {
    Database animal =
        new Database("animal", new string[] { "ID", "Name", "Age" });
  }
}
