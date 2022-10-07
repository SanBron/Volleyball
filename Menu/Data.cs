using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardMenuDemo
{
    public class Settings
    {
        enum Restrictions {LL=20,LW=6,LS=40,HL=100,HW=30,HS=2000}
        public void SetDefault()
        {
            int Length = 0;
            int Width = 0;
            int Speed = 0;
            String line;
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\sanmo\\source\\repos\\Volleyball\\Menu\\defaults.txt");
                line = sr.ReadLine();
                Length = Convert.ToInt32(line);
                line = sr.ReadLine();
                Width = Convert.ToInt32(line);
                line = sr.ReadLine();
                Speed = Convert.ToInt32(line);
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Game.AreSettingsEmpty = false;
                Console.WriteLine("Настройки скопированны в буфер");
            }
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\sanmo\\source\\repos\\Volleyball\\Menu\\options.txt");
                sw.WriteLine(Length);
                sw.WriteLine(Width);
                sw.WriteLine(Speed);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Настройки перенесены в файл");
            }
            Thread.Sleep(2000);
        }
        public void SetNew()
        {
            Console.WriteLine("Запишите высоту поля, ширину поля и скорость игры построчно");
            int Length;
            int Width;
            int Speed;
            Length = Convert.ToInt32(Console.ReadLine());
            Width = Convert.ToInt32(Console.ReadLine());
            Speed = Convert.ToInt32(Console.ReadLine());
            if (Length > Convert.ToInt32(Restrictions.LL) && Width > Convert.ToInt32(Restrictions.LW) && Speed > Convert.ToInt32(Restrictions.LS) && Length <= Convert.ToInt32(Restrictions.HL) && Width <= Convert.ToInt32(Restrictions.HW) && Speed <= Convert.ToInt32(Restrictions.HS))
            {
                try
                {
                    StreamWriter sw = new StreamWriter("C:\\Users\\sanmo\\source\\repos\\Volleyball\\Menu\\options.txt");
                    sw.WriteLine(Length);
                    sw.WriteLine(Width);
                    sw.WriteLine(Speed);
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Game.AreSettingsEmpty = false;
                    Console.WriteLine("Настройки перенесены в файл");
                }
                Thread.Sleep(2000);

            }
            else
            {
                Console.WriteLine("Неправильные настройки, будут установлены");
                Console.WriteLine("настройки по умолчанию");
                Thread.Sleep(2000);
                SetDefault();
            }
        }
        public int[] SetOptions()
        {
            int[] SData = new int[3];
            String line;
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\sanmo\\source\\repos\\Volleyball\\Menu\\options.txt");
                line = sr.ReadLine();
                SData[0] = Convert.ToInt32(line);
                line = sr.ReadLine();
                SData[1] = Convert.ToInt32(line);
                line = sr.ReadLine();
                SData[2] = Convert.ToInt32(line);
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return SData;
        }
    }

}
