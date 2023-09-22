using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_csharp {
    internal class Basic_3 {
        public void run() {

            // 接口、抽象类的概念和JAVA的几乎如出一辙，除了写法稍有点不一样
            //IVehicle car = new Car();
            //car.Run();
            //IVehicle truck = new Truck();
            //truck.Run();
            //car.Stop();
            //truck.Stop();

            // 抽象类、接口的松耦合示例
            //PersonWhoHasAVehicle ordinaryDriver = new PersonWhoHasAVehicle(new Car());
            //PersonWhoHasAVehicle truckDriver = new PersonWhoHasAVehicle(new Truck());
            //ordinaryDriver.Drive();
            //truckDriver.Drive();
            //ordinaryDriver.Park();
            //truckDriver.Park();

            //// 接口隔离：tank接口作为一个vehicle接口的实现，它还具有weapon的性质，但是隔离开了，它可以单独作为vehicle使用
            //PersonWhoHasAVehicle person1 = new PersonWhoHasAVehicle(new HeavyTank());
            //person1.Drive();
            //person1.Park();

            // 显式实现接口方法
            WarmKiller warmKiller = new WarmKiller();
            warmKiller.Love();
            // 必须是IKiller的实例才可以调用显示实现的接口
            IKiller killer = warmKiller as IKiller;
            killer.Kill();

        }
    }

    // 一个拥有交通工具的人的类
    class PersonWhoHasAVehicle {
        private IVehicle _vehicle;

        public PersonWhoHasAVehicle(IVehicle vehicle) => _vehicle = vehicle;

        public void Drive() {
            _vehicle.Run();
        }

        public void Park() {
            _vehicle.Stop();
        }
    }

    // 接口
    interface IVehicle {
        void Stop();
        void Run();
    }

    // 抽象类
    abstract class Vehicle: IVehicle {
        public void Stop() {
            Console.WriteLine("Vehicle is stopped.");
        }

        public abstract void Run();
    }

    class Car: Vehicle {
        public override void Run() {
            Console.WriteLine("Car is running.");
        }
    }

    class Truck: Vehicle {
        public override void Run() {
            Console.WriteLine("Truck is running.");
        }
    }

    interface Weapon {
        void Fire();
    }

    interface ITank: IVehicle, Weapon {
        
    }

    class LightTank: ITank {
        public void Fire() {
            Console.WriteLine("LightTank is firing.");
        }
        public void Run() {
            Console.WriteLine("LightTank is running.");
        }
        public void Stop() {
            Console.WriteLine("LightTank is stopped.");
        }
    }

    class HeavyTank: ITank {
        public void Fire() {
            Console.WriteLine("HeavyTank is firing.");
        }
        public void Run() {
            Console.WriteLine("HeavyTank is running.");
        }
        public void Stop() {
            Console.WriteLine("HeavyTank is stopped.");
        }
    }


    // 接口的显式实现
    interface IGentlemen {
        void Love();
    }

    interface IKiller {
        void Kill();
    }

    public class WarmKiller: IGentlemen, IKiller {
        // 显式实现时，不能添加任何访问修饰符
        void IKiller.Kill() {
            Console.WriteLine("Kill!!!");
        }
        public void Love() {
            Console.WriteLine("Love!");
        }
    }

}
