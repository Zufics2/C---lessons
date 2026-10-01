using Console1.Abstract;
using Console1.Model;
using Console1.MyDbContext;

namespace Console1.Service;

public class CityService : ICity
{
    public IEnumerable<City> CityGetAll()
    {
        using var db = new MyContext();
        return db.city.ToList();
    }

    public City GetById(int id)
    {
        using var db = new MyContext();
        return db.city.Find(id);
    }

    public string CityIns(City city)
    {
        using var db = new MyContext();
        db.city.Add(city);
        db.SaveChanges();
        return "Added";
    }
}