using Console1.Model;

namespace Console1.Abstract;

public interface ICity
{
    IEnumerable<City> CityGetAll();
    City GetById(int id);
    string CityIns(City city);
}