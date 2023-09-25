using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // Task Parallel Library => TPL

namespace Learning_csharp {
    public class ThreadingTask {

        public void Run() {
            var source = new CancellationTokenSource();
            List<Task> tasks = new List<Task>();
            try {
                // 使用 async和await 创建的Task任务
                tasks.Add(MakeTea());

                // 使用Task.Factory创建的Task任务
                tasks.Add(Task.Factory.StartNew(() => Console.WriteLine("Doing some other work 1....")));
                tasks.Add(Task.Factory.StartNew(() => Console.WriteLine("Doing some other work 2....")));
                tasks.Add(Task.Factory.StartNew(() => Console.WriteLine("Doing some other work 3....")));

                // 主动cancel
                source.Cancel();

                Task.WaitAll(tasks.ToArray());

                // 使用Parallel.foreach
                List<int> intList = new List<int>() {1, 23, 234, 5, 31, 34, 5, 12, 3, 345, 123, 123, 25, 34, 234};
                Parallel.ForEach(intList, num => {
                    Thread.Sleep(200);
                    Console.WriteLine(num);
                });

                // 使用Parallel.for
                Parallel.For(0, 20, num => Console.WriteLine(num));

                // Parallel是本身是阻塞的
                Console.WriteLine("after Parallel");
            } catch (Exception ex) {
                Console.WriteLine(ex.GetType());
            }
             
       }

        public async Task<string> MakeTea() {
            var boilingWater = BoilWater();
            Console.WriteLine("take the cups out");
            Console.WriteLine("put tea in cups");

            var water = await boilingWater;

            var tea = $"pour {water} in cups";
            Console.WriteLine(tea);
            return tea;
        }

        public async Task<string> BoilWater() {
            Console.WriteLine("start the kettle");
            Console.WriteLine("waiting for the kettle");

            await Task.Delay(2000);

            Console.WriteLine("kettle finished boiling");
            return "water";
        }

        public void DoSomething(CancellationToken token) {
            if (token.IsCancellationRequested) {
                Console.WriteLine("Cancellation requested.");
                token.ThrowIfCancellationRequested();
            }
            for (int i = 0; i < 10; i++) {
                Console.WriteLine("i = {0}", i);
                Thread.Sleep(200);
            }
        }
    }

}
