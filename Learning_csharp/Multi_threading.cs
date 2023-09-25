﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_csharp {
    public class Multi_threading {

        public void Run() {
            //// 获取当前线程（这个方法运行在主线程，因此获取的就是主线程）
            //Thread thread = Thread.CurrentThread;
            //thread.Name = "mian thread";
            //Console.WriteLine("Name of mian thread: {0}", thread.Name);

            //// 通过方法（委托）创建线程
            //Thread t1 = new Thread(new ThreadStart(CountDown)); // 委托完整版
            //Thread t2 = new Thread(CountUp); // 委托简单版（直接把方法丢进去）
            //t1.Start();
            //t2.Start();

            //// 通过lambda表达式创建线程
            //Thread t3 = new Thread(() => {
            //    for (int i = 0; i < 3; i++) {
            //        Console.WriteLine("Thread #3 is created by lambda expression. Countting {0}", i);
            //        Thread.Sleep(1000);
            //    }
            //    Console.WriteLine("Thread #3 is finished.");
            //});
            //t3.Start()

            // 线程池
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolTest));
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolTest2));
            Console.ReadLine();

        }

        public void CountDown() {
            for (int i = 1; i <= 10; i++) {
                Console.WriteLine("Thread #1 is countting down: {0}", i);
                Thread.Sleep(400);
            }
            Console.WriteLine("Thread #1 finished counting.");
        }

        public void CountUp() {
            for (int i = 10; i >= 1; i--) {
                Console.WriteLine("Thread #2 is countting up: {0}", i);
                Thread.Sleep(400);
            }
            Console.WriteLine("Thread #2 finished counting.");
        }

        public void ThreadPoolTest(Object obj) {
            for (int i = 10; i >= 1; i--) {
                Console.WriteLine("ThreadPoolTest is running: counting {0}", i);
                Thread.Sleep(400);
            }
        }

        public void ThreadPoolTest2(Object obj) {
            for (int i = 10; i >= 1; i--) {
                Console.WriteLine("ThreadPoolTest2 is running: counting {0}", i);
                Thread.Sleep(400);
            }
        }

    }


}
