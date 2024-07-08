using FastWeather.Models.ZipCodes;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var db = new ZipCodeDB();
        db.AddData();
        db.RetrieveData();
    }
}

