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
            /*
                * 使用litjson的说明：
                * 1、目标是解析第一层json结构，按key排序（ascii码），然后拼接字符串用来生成签名字符串
                * 2、应该使用请求内容的原始数据处理，否则签名会不一致
                * 3、严格来说应该逐个字符读取，但为了提取首层json结构，难度较大，所以还是使用json解析sdk
                * 4、前提是保证反序列化->再序列化后和请求原始内容一致
                * 4、newtonsoft解析json时，可能会将数值的小数位数改变，导致和请求原始内容不同，故不用它
                * 5、litjson比较轻，代码量少，改造比较容易
                * 6、通过改造litjson源码，将所有jtoken（数值、布尔）的value全部按字符串处理，保证序列化->反序列化后结果和原始请求内容一致
                */
            JsonData jsonData = JsonMapper.ToObject(json);
            var orders = jsonData["orders"];
            json = orders.ToJson();
            File.WriteAllText(target, json);
        }
    }
}