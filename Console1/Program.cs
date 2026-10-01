using System.Data;
using Console1.Service;
using Console1.Model;

namespace Console1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CityService cityservice = new CityService();

            var item2 = cityservice.CityIns(new City { name = "Shymkent", population = 1500000, year = 1900});
            Console.WriteLine(item2);
            
            var data = cityservice.CityGetAll();
            foreach (City item in data)
            {
                Console.WriteLine($"{item.id} - {item.name} - {item.population}");
            }
            
            // var item = cityservice.GetById(2);
            // if (item != null)
            //     Console.WriteLine($"{item.id}  - {item.name} - {item.population}");
            // else 
            //     Console.WriteLine($"Not found");

            // var dt = MyTable.GetDataTable();
            // foreach (var item in dt.Rows)
            // {
            //     Console.WriteLine($"{item}");
            // } 

            // var list = MyTable.GetList();
            // foreach (var i in list.Where(z=>z.id > 1))
            // {
            //     Console.WriteLine($"{i.id} - {i.name}");
            // }
        }
    }
}