using lesson180926.Model;

namespace lesson180926.Abstract
{
    public interface ICity
    {
        IEnumerable<CityDTO> CityGetAll();
        CityDTO CityGetById(int id);
        string CityAdd(CityDTO city);
        string CityEdit(CityDTO city, int id);
        string CityDelete(int id);
    }
}
