namespace MyXUnit;

public class ForTesting
{
    public int GetSum(int a, int b)
    {
        return a + b;
    }

    public DateTime ConvertFromString(string dt)
    {
        return DateTime.Parse(dt);
    }    
    
    public List<string> GetList()
    {
        var list = new List<string>();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        return list;
    }
}