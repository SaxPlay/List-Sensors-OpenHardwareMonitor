using System;
using OpenHardwareMonitor.Hardware;

namespace ListHardwareSensors
{
    class Program
    {
        public class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer MyComputer)
            {
                MyComputer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
            public void VisitParent(IHardware parent) { }
        }
        static void GetSystemInfo()
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer MyComputer = new Computer();

            MyComputer.Open();
            MyComputer.CPUEnabled = true;
            MyComputer.GPUEnabled = true;
            MyComputer.RAMEnabled = true;
            MyComputer.MainboardEnabled = true;
            MyComputer.FanControllerEnabled = true;
            MyComputer.HDDEnabled = true;
            MyComputer.Accept(updateVisitor);
            for (int i = 0; i < MyComputer.Hardware.Length; i++)
            {
                for (int j = 0; j < MyComputer.Hardware[i].Sensors.Length; j++)
                {
                    Console.WriteLine("Hardware[" + i.ToString().PadRight(3) + "] sensor [" + j.ToString().PadRight(3) + "]-> " + MyComputer.Hardware[i].Sensors[j].Name.PadLeft(20) + MyComputer.Hardware[i].Sensors[j].SensorType.ToString().PadLeft(30) + "    "+ MyComputer.Hardware[i].Sensors[j].Identifier.ToString().PadLeft(30) + "   ->   "+ MyComputer.Hardware[i].Sensors[j].Value.ToString().PadLeft(10) + "\r");
             
                }
            }

            MyComputer.Close();
        }
        static void Main(string[] args)
        {

            GetSystemInfo();
            Console.WriteLine("\n\n HIT anykey to exit");
            Console.ReadKey();

        }
    }
    
}