namespace Database;

class Program
{
  public static void Main(string[] args)
  {
    Database animal =
        new Database("animal", new string[] { "ID", "Name", "Age" });

    // animal.Add(new string[] {"", "Max", "2"});
    animal.DeleteById("00000001");
    animal.Save();
  }
}
