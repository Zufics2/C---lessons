using lesson180926.Abstract;

namespace lesson180926.Service
{
    public class StudentService : IStudent
    {
        public int GetSum(int a, int b) => a + b;
        public string GetConcat(string a, string b) => $"{a}{b}";
    }
}

