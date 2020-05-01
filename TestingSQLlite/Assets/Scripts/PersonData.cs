using SQLite4Unity3d;
public class PersonData
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public string url { get; set; }
    public string photo { get; set; }
    public string name { get; set; }
}
