using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learning_csharp {
    public class Serialization {
        public async void Run() {
            Person person = new Person();
            person.Name = "Trueman";
            person.Age = 30;
            person.SayHello();

            // 数组
            List<Person> persons = new List<Person>() {
                new Person() {
                    Name = "Trueman",
                    Age = 30
                },
                new Person() {
                    Name = "Badman",
                    Age = 45
                }
            };

            string jsonPath1 = Path.Combine(Environment.CurrentDirectory, "person.json");
            string jsonPath2 = Path.Combine(Environment.CurrentDirectory, "persons.json");
            Console.WriteLine("jsonPath1: {0}", jsonPath1);
            Console.WriteLine("jsonPath2: {0}", jsonPath2);
            // JSON序列化
            //using (StreamWriter jsonStream = File.CreateText(jsonPath1)) {
            //    Newtonsoft.Json.JsonSerializer jjs = new Newtonsoft.Json.JsonSerializer();
            //    jjs.Serialize(jsonStream, person);
            //}
            //using (StreamWriter jsonStream  = File.CreateText(jsonPath2)) {
            //    Newtonsoft.Json.JsonSerializer jjs = new Newtonsoft.Json.JsonSerializer();
            //    jjs.Serialize(jsonStream, persons);
            //}

            // JSON反序列化
            using (JsonReader jsonReader = new JsonTextReader(new StreamReader(jsonPath1))) {
                Person? person1 = new Newtonsoft.Json.JsonSerializer().Deserialize<Person>(jsonReader);
                person1?.SayHello();
            }
            using (JsonReader jsonReader = new JsonTextReader(new StreamReader(jsonPath2))) {
                List<Person>? persons1 = new Newtonsoft.Json.JsonSerializer().Deserialize<List<Person>>(jsonReader);
                if (persons1 is not null && persons1.Count > 0) {
                    foreach (Person p in persons1) {
                        p.SayHello();
                    }
                }
            }

            Console.ReadKey();
        }
    }

    public class Person {
        public string Name { get; set; }
        public int Age { get; set; }

        public void SayHello() {
            Console.WriteLine("{0}-year-old {1} is saying hello", Age, Name);
        }
    }
}
