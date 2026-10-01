using System;
using System.Collections.Generic; 
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console1
{
    public class MyTable
    {
        public static DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("dt", typeof(DateTime));

            for (int i = 0; i < 10; i++)
            {
                dt.Rows.Add(new object[] { i, "string - " + i.ToString(), DateTime.Now });
            }

            return dt;
        }

        public static List<TestModel> GetList()
        {
            List<TestModel> list = new List<TestModel>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new TestModel
                {
                    id = i,
                    name = "string - " + i.ToString(),
                    dt = DateTime.Now
                });
            }

            return list;
        }
    }

    public class TestModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime dt { get; set; }
    }
}