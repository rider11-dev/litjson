using LitJson;
using System.Text;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Test();
            Console.WriteLine("done!");
            Console.ReadKey();
        }

        internal static void Test()
        {
            var target = "out.orders.json";
            if (File.Exists(target))
            {
                File.Delete(target);
            }
            var json = File.ReadAllText("data.json", Encoding.UTF8);
            JsonData jsonData = JsonMapper.ToObject(json);
            var orders = jsonData["orders"];
            json = orders.ToJson();
            File.WriteAllText(target, json);
        }
    }
}