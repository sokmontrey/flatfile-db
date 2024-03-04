namespace Database
{
  class Database
  {
    private string name = "";
    private string[] fields;

    private string base_path = "databases";
    private string db_path;
    private string counter_path;

    private int counter = 0;
    private int num_id_digits = 8;
    private int id_field = 0;
    private List<string[]> data;

    public Database(string name, string[] fields)
    {
      this.name = name;
      this.fields = fields;

      this.db_path = $"{this.base_path}/{name}.csv";
      this.counter_path = $"{this.base_path}/{name}_counter.txt";

      this.counter = 0;
      this.SetupIfNotExist();
      this.ReadCounter();
      // Allowed the Database to set itself up when provided the files
      this.ReadFields();
      this.ReadData();
    }

    /* --------------------------Create-------------------------- */

    private void WriteCounter()
    {
      FileManager.WriteLine(this.counter_path, this.counter.ToString());
    }

    private void WriteFields()
    {
      FileManager.WriteLine(this.db_path, this.fields);
    }

    private void WriteData()
    {
      this.WriteFields();
      FileManager.AppendLines(this.db_path, this.data);
    }

    public void Add(string[] line, bool gen_id = true)
    {
      if (gen_id)
      {
        string id = this.GetCurrentId();
        line[this.id_field] = id;
        this.counter++;
      }
      this.data.Add(line);
    }

    /* ---------------------------Read--------------------------- */

    public List<string[]> GetAll() { return this.data; }

    private void ReadData()
    {
      this.data = FileManager.ReadLines(this.db_path, 1);
    }

    private int ReadCounter()
    {
      string counter_str = FileManager.Read(this.counter_path);
      return this.counter = int.Parse(counter_str);
    }

    private void ReadFields()
    {
      this.fields = FileManager.ReadLines(this.db_path, 0, 1)[0];
    }

    /* --------------------------Search-------------------------- */

    public int IndexOfId(string id)
    {
      int i = 0;
      foreach (var line in this.data)
      {
        if (line[this.id_field] == id)
          return i;
        i++;
      }
      return -1;
    }

    private int IndexOfField(string field)
    {
      for (int i = 0; i < this.fields.Length; i++)
        if (this.fields[i] == field)
          return i;
      return -1;
    }

    public List<string[]> SearchBy(int field_index, string value)
    {
      return this.data.Where(line => line[field_index] == value).ToList();
    }

    /* --------------------------Update-------------------------- */

    public int UpdateById(string id, string[] new_values)
    {
      // item with id doesn't exist
      int i = this.IndexOfId(id);
      if (i == -1)
        return -1;

      this.data[i] = new_values;
      return i;
    }

    /* --------------------------Delete-------------------------- */

    public int DeleteById(string id)
    {
      int i = this.IndexOfId(id);
      if (i == -1)
        return -1;

      this.data.RemoveAt(i);
      this.counter--;
      return i;
    }

    /* --------------------------Helper-------------------------- */

    private void SetupIfNotExist()
    {
      if (FileManager.Exists(this.db_path))
        return;
      FileManager.CreateDirectory(this.base_path);
      FileManager.CreateFile(this.db_path);
      FileManager.CreateFile(this.counter_path);

      this.WriteCounter();
      this.WriteFields();
    }

    public string GetCurrentId()
    {
      return this.counter.ToString().PadLeft(this.num_id_digits, '0');
    }
  }
} // namespace Database
