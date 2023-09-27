using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Learning_csharp {
    internal class Socket_learning {

        public void Run() {
            // 测试socket和速动连接、控制、读取
            TestSocketWithSudong();
        }

        public void TestSocketWithSudong() {
            // 程序号对应CRC16位校验码
            Dictionary<string, string> psetNo = new Dictionary<string, string> {
                { "01", "63B6" }, // 01号程序
                { "02", "23b7" }, // 02号程序
                { "03", "e277" }, // 03号程序
                { "04", "a3b5" }, // 04号程序
                { "05", "6275" }, // 05号程序
                { "06", "2274" }, // 06号程序
                { "07", "e3b4" }, // 07号程序
                { "08", "a3b0" }, // 08号程序
                { "09", "6270" }, // 09号程序
                { "10", "a3ba" }, // 10号程序
                { "11", "627a" }, // 11号程序
            };
            // 要切换的程序号
            string pset = "03";
            // 切换程序命令
            string commandPset = "55AA07020500" + pset + psetNo[pset] + "0D0A";
            // 禁用工具命令
            string commandLock = "55AA070100020000000D0A";
            // 使能工具命令
            string commandUnlock = "55AA070100000000000D0A";

            // 客户端连接服务器
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try {
                // 测试连接速动工具（连接服务器）
                socketClient.Connect(IPAddress.Parse("192.168.1.16"), int.Parse("1200"));

                // 切换程序号
                socketClient.Send(Convert.FromHexString(commandPset));
                Thread.Sleep(100);
                byte[] msgBytes = new byte[1024 * 1024];
                int len = socketClient.Receive(msgBytes, 0, msgBytes.Length, SocketFlags.None);
                string msg = ParseBytesToString(msgBytes, len);
                Console.WriteLine("【切换程序号】指令下发反馈：{0}", msg);

                // 使能工具
                socketClient.Send(Convert.FromHexString(commandUnlock));

                // 禁用工具
                //socketClient.Send(Convert.FromHexString(commandLock));

                // 实时读取数据
                while (true) {
                    try {
                        msgBytes = new byte[1024 * 1024];
                        int lenInLoop = socketClient.Receive(msgBytes, 0, msgBytes.Length, SocketFlags.None);
                        string msgInLoop = ParseBytesToString(msgBytes, lenInLoop);
                        string msgHead = msgInLoop.Substring(0, 8);

                        Console.WriteLine("=============================================================================");
                        switch (msgHead) {
                            case "55AA2781":
                                // 拧紧扭力
                                double torque = GetDataByStr(msgInLoop.Substring(14, 4));
                                double torqueMax = GetDataByStr(msgInLoop.Substring(48, 4)); // 最大值
                                double torqueMin = GetDataByStr(msgInLoop.Substring(52, 4)); // 最小值

                                // 锁附（旋入）角度
                                int enterAngle = GetDataByStr2(msgInLoop.Substring(22, 4));
                                int enterAngleMax = GetDataByStr2(msgInLoop.Substring(64, 4)); // 最大值
                                int enterAngleMin = GetDataByStr2(msgInLoop.Substring(68, 4)); // 最小值
                                
                                // 拧紧角度
                                int angle = GetDataByStr2(msgInLoop.Substring(26, 4));
                                int angleMax = GetDataByStr2(msgInLoop.Substring(56, 4)); // 最大值
                                int angleMin = GetDataByStr2(msgInLoop.Substring(60, 4)); // 最小值

                                // 拧紧状态
                                string status = msgInLoop.Substring(42, 2);
                                if (status.Equals("01")) {
                                    status = "OK";
                                } else if (status.Equals("04")) {
                                    status = "CCW";
                                } else {
                                    status = "NG";
                                }

                                // 拧紧结果
                                Console.WriteLine("以下为拧紧结果：");
                                Console.WriteLine("扭力值：{0}", torque);
                                Console.WriteLine("扭力最大值：{0}", torqueMax);
                                Console.WriteLine("扭力最小值：{0}", torqueMin);
                                Console.WriteLine();
                                Console.WriteLine("锁附（旋入）角度：{0}", enterAngle);
                                Console.WriteLine("锁附（旋入）角度最大值：{0}", enterAngleMax);
                                Console.WriteLine("锁附（旋入）角度最小值：{0}", enterAngleMin);
                                Console.WriteLine();
                                Console.WriteLine("拧紧角度：{0}", angle);
                                Console.WriteLine("拧紧角度最大值：{0}", angleMax);
                                Console.WriteLine("拧紧角度最小值：{0}", angleMin);
                                Console.WriteLine();
                                Console.WriteLine("拧紧状态：{0}", status);
                                break;
                            case "55AA0585":
                                Console.WriteLine("工具运行中");
                                break;
                            default:
                                Console.WriteLine("未知返回码：{0}", msgInLoop);
                                break;
                        }

                    } catch (Exception ex) {
                        Console.WriteLine(ex.StackTrace);
                        Thread.Sleep(100);
                    }
                    Console.WriteLine("=============================================================================");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
            Console.ReadLine();
        }

        public string ParseBytesToString(byte[] bytes, int len) {
            string resultStr = "";
            for (int i = 0; i < len; i++) {
                string msgTemp = Convert.ToString(bytes[i], 16);
                if (msgTemp.Length == 1) {
                    msgTemp = "0" + msgTemp;
                }
                resultStr += msgTemp;
            }
            return resultStr.ToUpper();
        }

        public double GetDataByStr(string dataStr) {
            string dataStrTemp = dataStr.Substring(2, 2) + dataStr.Substring(0, 2);
            return Convert.ToInt32(dataStrTemp, 16) / 1000.0;
        }

        public int GetDataByStr2(string dataStr) {
            string dataStrTemp = dataStr.Substring(2, 2) + dataStr.Substring(0, 2);
            return Convert.ToInt32(dataStrTemp, 16);
        }
    }
}
