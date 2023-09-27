// 命名空间
namespace Learning_csharp {

    class MainClass {
        static void Main(string[] args) {

            //// 类型、变量
            //int var_int = 0;
            //short var_short = 0;
            //long var_long = 0L;
            //float var_float = 0F;
            //double var_double = 0D;

            //uint var_uint = 0;
            //ushort var_ushort = 0;
            //ulong var_ulong = 0L;

            //// 操作符（运算符）
            //var_int = 1 + 1;
            //var_short = 1 - 1;
            //var_long = 1 * 2;
            //var_float = 1 / 2;
            //var_double += 1;
            //var_uint -= 1;
            //var_ushort *= 1;
            //var_ulong /= 1;

            //// 一元运算符
            //int x1 = +1;
            //x1 = -1;
            //x1++;
            //x1--;
            //++x1;
            //--x1;
            //long x2 = x1; // 隐式类型转换：低精度类型转为高精度类型
            //int x3 = (int) x2; // 强制类型转换操作符：高精度类型转为低精度类型
            //bool bool1 = true;
            //bool1 = !bool1;

            //// 关系检测运算符
            //if (x1 == 0) {
            //}
            //if (x1 >= 0) {
            //}
            //if (x1 <= 0) {
            //}
            //if (x1 != 0) {
            //}
            //Test t = new Test();
            //t.Id = 1; // Id在这里是属性，不是字段
            //TestSon ts = new TestSon();
            //bool exam = t is Test;
            //// 类型检测运算符（is）
            //Console.WriteLine("exam: " + exam); // True
            //bool exam2 = ts is Test;
            //Console.WriteLine("exam2: " + exam2); // True
            //// 类型检测运算符（as）
            //Object o = new Test();
            //Test t2 = o as Test;
            //Console.WriteLine("type of t2: " + t2.GetType().Name);

            //// 位运算
            //int x4 = 15;
            //int x5 = x4 >> 2; // 在不溢出的情况下，往右移几位就是除以几次2
            //int x6 = x4 << 2; // 同上条件，往左几位就是乘以几次2

            //// 逻辑与、逻辑或、逻辑异或
            //int x9 = 7;
            //int y9 = 28;
            //int z1 = x9 | y9;
            //int z2 = x9 & y9;
            //int z3 = x9 ^ y9;

            //// 条件与、条件或
            //if (z1 > 0 || z2 <= 0) {
            //}
            //if (z2 < 0 && z3 >= 0) {
            //}

            //// 可空类型
            //Nullable<int> x10 = null;
            //int? x11 = null;
            //// null 合并符号（??）
            //x11 = x11 ?? 10; // 如果x11不为null则x12 = x11，否则x12 = 10
            //x11 ??= 10;

            //// 条件操作符（其实就是大众熟知的三目运算符 ? : ）
            //int x13 = x11 == null ? 10 : x11.Value;

            //// 表达式：以上每一行都是一个或多个表达式组成的“一行”语句

            //// switch 表达式
            //switch (x11) {
            //    case 10:
            //        break;
            //    case 11:
            //        break;
            //    case 12:
            //    case 13:
            //        break;
            //    default:
            //        break;
            //}

            //// while 循环语句
            //int loop_x = 0;
            //while (x1++ < 100) {
            //}

            //// for 循环语句
            //for (int i = 0; i < 100; i++) {
            //}

            //// foreach 循环语句
            //foreach (int num in new List<int>()) {
            //}

            //// try-catch 语句
            //try {
            //} catch (Exception e) { }


            //// Basic_2.class
            //Basic_2 basic_2 = new Basic_2();
            //basic_2.run();

            //// Basic_2.class
            //Basic_3 basic_3 = new Basic_3();
            //basic_3.run();

            //// 多线程、线程池
            //Multi_threading multi_threading = new Multi_threading();
            //multi_threading.Run();

            //// 任务机制
            //ThreadingTask threadingTask = new ThreadingTask();
            //threadingTask.Run();

            //// socket编程（客户端）
            //Socket_learning socket_Learning = new Socket_learning();
            //socket_Learning.Run();

            // 序列化、反序列化
            Serialization serialization = new Serialization();
            serialization.Run();

        }

        // 值类型参数、引用类型参数
        public void test1(
            int x, // 值类型参数
            Demo1 demo1 // 引用类型参数
            ) {
            x = 10; // 值参数重新赋值不会影响传进来的原始变量
            demo1.x = 2; // 引用参数中的字段、属性、方法（即所有成员）被改变时，原始变量中对应的成员也会被改变
            demo1 = new Demo1(); // 引用参数重新赋值也不会影响传进来的原始变量
            demo1.x = 3; // 如果重新赋值后再改变成员，原始变量就不会变
        }

        // 引用参数
        public void test2(
            ref int x, // 引用值参数
            ref Demo1 demo1 // 引用引用类型参数
            ) {
            x = 10; // 引用参数就是重新赋值后，原变量也会改变，
            demo1 = new Demo1(); // 同理
        }

    }

    // 类
    class Test {
        // 类的成员变量（字段）
        private int id;
        private string name;

        // 属性（其实就是JAVA里的Getter和Setter的另一种形式）
        public int Id {
            set {
                id = value;
            }
            get {
                return id;
            }
        }
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
        public int Age { get; set; }

        // 构造函数
        public Test() {
            id = 1;
            name = "test";
        }

        // 方法（成员函数）
        public void PrintSomething(string something) {
            Console.WriteLine("something: " + something);
        }
    }

    // 类的继承
    class TestSon: Test {
        private int id;
        private string name;
        static int kk; // 静态成员变量（静态字段）

        public int Id {
            get => id;
            set => id = value;
        }
        public string nickname { get; set; } // 简略声明属性

        public TestSon() {
            Id = 11;
            name = "test_son";
            kk = 1;
        }

        public void test() { // 普通方法可以操作静态字段，但静态方法不可以操作普通字段
            kk += 1;
        }

        // 先声明一个字典，用于索引器的测试
        Dictionary<string, int> dict = new Dictionary<string, int>();
        // 索引器通常使用在集合类型中，用在一般类里很少见
        public int? this[string subject] {
            get {
                if (dict.ContainsKey(subject)) {
                    return dict[subject];
                }
                return null;
            }
            set {
                if (!value.HasValue) {
                    throw new InvalidOperationException("value cannot be null");   
                }
                if (dict.ContainsKey(subject)) {
                    dict[subject] = value.Value;
                } else {
                    dict.Add(subject, value.Value);
                }
            }
        }
    }
}