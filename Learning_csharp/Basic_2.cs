// 命名空间
using System.Timers;
using MyLib.MyNamespace;

namespace Learning_csharp {
    internal class Basic_2 {
        public void run() {

            //// 测试扩展方法
            //int x_demo = 0;
            //x_demo.JustAPrint();

            //// 测试委托
            //TestDelegate td = new TestDelegate(test3);
            //Console.WriteLine(td(x_demo));

            //// 测试多播委托
            //Student student1 = new Student() {Id = 1, PenColor = ConsoleColor.Yellow};
            //Student student2 = new Student() {Id = 2, PenColor = ConsoleColor.Red};
            //Student student3 = new Student() {Id = 3, PenColor = ConsoleColor.Blue};
            //Action action1 = new Action(student1.DoHomework);
            //Action action2 = new Action(student2.DoHomework);
            //Action action3 = new Action(student3.DoHomework);
            //// 多播委托
            ////action1 += action2;
            ////action1 += action3;
            ////action1();
            //// 隐式异步调用（这个方法在这个平台上貌似不能用）
            ////action1.BeginInvoke(null, null);
            ////action2.BeginInvoke(null, null);
            ////action3.BeginInvoke(null, null);
            //// 显示异步调用 - Thread
            //Thread t1 = new Thread(new ThreadStart(student1.DoHomework));
            //Thread t2 = new Thread(new ThreadStart(student2.DoHomework));
            //Thread t3 = new Thread(new ThreadStart(student3.DoHomework));
            ////t1.Start();
            ////t2.Start();
            ////t3.Start();
            //// 显示异步调用 - Task（这个好像没生效，不知道为啥）
            //Task tk1 = new Task(new Action(student1.DoHomework));
            //Task tk2 = new Task(new Action(student2.DoHomework));
            //Task tk3 = new Task(new Action(student3.DoHomework));
            //tk1.Start();
            //tk2.Start();
            //tk3.Start();

            //// 1. 事件拥有者（event source）
            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Interval = 1000;
            //// 3. 事件响应者（订阅者） boy（event subscriber）
            //Boy boy = new Boy();
            //Girl girl = new Girl();
            //// 5. 事件订阅：使用 “+=” 进行订阅
            //timer.Elapsed += boy.Action; // 2. 事件成员（event）：Elapsed
            //timer.Elapsed += girl.Action;
            //timer.Start();
            //Console.ReadLine();

            //// 事件处理器在事件拥有者内部的范例
            //Customer customer = new Customer();
            //Waiter waiter = new Waiter();
            //customer.Order += waiter.Action;
            //customer.Action();
            //customer.PayTheBill();

            // 泛型委托、类型推断等
            //Action action1 = new Action(this.action1);
            //action1();
            //Action<string> action2 = new Action<string>(this.action2);
            //action2("aaaaaaaa");
            //Func<int, double, int> func1 = new Func<int, double, int>(this.func1);
            //int res = func1(100, 30.0);
            //Console.WriteLine("res: {0}", res);
            //int res2 = typeInfer((t, w) => {
            //    Console.WriteLine("t: {0}, w: {1}", t, w);
            //    return t;
            //}, 100, 20.0);
            //Console.WriteLine("typeInfer: res2 = {0}", res2);

            // LINQ: .NET Language Integrated Query

            //// 类的继承
            //Car car = new Car();
            //car.PrintOwner();
            //car.Hi();

        }

        // 测试委托的方法
        public int test3(int x) {
           return x;
        }

        public void action1() {
            Console.WriteLine("action1");
        }
        public void action2(string msg) {
            Console.WriteLine("action2: {0}", msg);
        }
        public T func1<T, W>(T t, W w) {
            Console.WriteLine("fun1: w = {0}", w);
            return t;
        }
        public T typeInfer<T, W>(Func<T, W, T> func2, T t, W w) {
            return func2(t, w);
        }

    }

    // 事件响应者
    class Boy {
        // 4. 事件处理器（event handler）
        internal void Action(object? sender, ElapsedEventArgs e) {
            Console.WriteLine("Jump!");
        }
    }
    class Girl {
        internal void Action(object? sender, ElapsedEventArgs e) {
            Console.WriteLine("Sing!");
        }
    }

    // 创建了一个委托类
    public delegate int TestDelegate(int x);

    class Demo1 {
        public int x = 1;
    }

    public static class SomeTypeExtension {
        public static void JustAPrint(this int x) {
            Console.WriteLine("this is a test extension function");
        }
    }

    // 测试多播委托
    class Student {
        public int Id { get; set; }
        public ConsoleColor PenColor { get; set; }

        public void DoHomework() {
            for (int i = 0; i < 5; i++) {
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine("Student {0} is doing his/her homework, he/she's been doing for {1} hour(s).", Id, i);
                Thread.Sleep(1000);
            }
        }
    }


    public class Customer {
        //private OrderEventHandler orderEventHandler;
        //public event OrderEventHandler Order {
        //    add {
        //        this.orderEventHandler += value;
        //    }
        //    remove {
        //        this.orderEventHandler -= value;
        //    }
        //}

        public event OrderEventHandler Order;

        public double Bill {
            get; set;
        }

        public void PayTheBill() {
            Console.WriteLine("I will pay ${0}.", this.Bill);
        }

        public void WalkIn() {
            Console.WriteLine("Walk into the restaurant.");
        }

        public void SitDown() {
            Console.WriteLine("Sit down.");
        }

        public void Think() {
            for (int i = 0; i < 5; i++) {
                Console.WriteLine("Let me think...");
                Thread.Sleep(1000);
            }

            //if (this.orderEventHandler != null) {
            //    OrderEventArgs e = new OrderEventArgs();
            //    e.DishName = "Kongpao Chicken";
            //    e.Size = "large";
            //    this.orderEventHandler.Invoke(this, e);
            //}
            if (this.Order != null) {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Kongpao Chicken";
                e.Size = "large";
                this.Order.Invoke(this, e);
            }
        }

        public void Action() {
            Console.ReadLine();
            this.WalkIn();
            this.SitDown();
            this.Think();
        }
    }

    public class Waiter {
        public void Action(Customer customer, OrderEventArgs e) {
            Console.WriteLine("I will serve you the dish - {0}.", e.DishName);

            double price = 10;
            switch (e.Size) {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }
            customer.Bill += price;
        }
    }

    public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);

    public class OrderEventArgs: EventArgs {
        public string DishName {
            get; set;
        }
        public string Size {
            get; set;
        }
    }


}