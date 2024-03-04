namespace Database
{

  class FileManager
  {
    // CreateFile if doesn't exist
    public static void CreateFile(string path)
    {
      if (!File.Exists(path))
        File.CreateText(path);
    }

    // Create Directory if doesn't exist
    public static void CreateDirectory(string path)
    {
      Directory.CreateDirectory(path);
    }

    public static bool Exists(string path) { return File.Exists(path); }

    public static void WriteLine(string path, string line)
    {
      File.WriteAllText(path, line + "\n");
    }

    public static void WriteLine(string path, string[] line)
    {
      File.WriteAllText(path, String.Join(",", line) + "\n");
    }

    public static void AppendLines(string path, List<string[]> lines)
    {
      var format_lines = lines.Select(line => String.Join(",", line));
      File.AppendAllLines(path, format_lines);
    }

    public static string Read(string path) { return File.ReadAllText(path); }

    public static List<string[]> ReadLines(string path, int skip = 1,
                                           int num = -1)
    {
      List<string[]> lines = new List<string[]>();
      using (StreamReader sr = new StreamReader(path))
      {
        while (skip-- > 0)
          sr.ReadLine();
        string line;
        while ((num == -1 || num-- > 0) && (line = sr.ReadLine()) != null)
          lines.Add(line.Split(","));
      }
      return lines;
    }
  }

} // namesapce Database
