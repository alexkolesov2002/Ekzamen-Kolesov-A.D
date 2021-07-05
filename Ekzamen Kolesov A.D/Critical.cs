using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekzamen_Kolesov_A.D
{/// <summary>
/// Класс решения математической модели
/// </summary>
    public class Critical
    {
        public int max, maxind;
        string Str = "";
        public List<List<Put>> Rezult = new List<List<Put>>();
        public List<Put> ListPutey; 
        public List<Put> Read = ReadFile();
        public void StroitPyt()
        {
                ListPutey = Read.FindAll(x => x.Tochka1 == Read[Minimal(Read)].Tochka1);//запись точки начала в лист путей
                foreach (Put x in ListPutey) //построение путей из начальных возможных перемещений
                {
                    MM(Read, x);
                    Rezult.Add(Vetki(Read, Str));
                    Str = "";
                }
        }
        /// <summary>
        /// Главный метод решения
        /// </summary>
        public void Reshenie()
        {
            StroitPyt();
            maxind = 0;
            max = Rezult[0][0].Dlina;
            for (int i = 0; i < ListPutey.Count; i++) // подсчет стоимости путей
            {
                if (RaschetDlini(Rezult[i]) >= max) // выбор самого большого
                {
                    max = RaschetDlini(Rezult[i]);
                    maxind = i;
                }
            }
            OutPut();
        }
        /// <summary>
        /// Запись решения в файл
        /// </summary>
        public void OutPut()
        {
            foreach (Put rb in Rezult[maxind])
            {
                string s = (rb.Tochka1 + " - " + rb.Tochka2);
                Debug.WriteLine(s);
            }
            Debug.WriteLine(max);
            using (StreamWriter Writer = new StreamWriter(@"Output.csv", false, Encoding.Default, 10))
            {
                foreach (Put rb in Rezult[maxind])
                {
                    string s = (rb.Tochka1 + " - " + rb.Tochka2);
                    Writer.WriteLine(s);
                }
                Writer.WriteLine(max);
            }
        }
        /// <summary>
        /// Структура для хранения промежуточных итогов
        /// </summary>        
        public struct Put
        {//Точки, длина
            public int Tochka1;
            public int Tochka2;
            public int Dlina;

            /// <summary>
            /// Переопределение метода ToString
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Tochka1.ToString() + " - " + Tochka2.ToString() + " " + Dlina.ToString();
            }
        }
        /// <summary>
        /// Нахождление начальной точки
        /// </summary>
        /// <param name="t1"></param>
        /// <returns></returns>
        public int Minimal(List<Put> t1)
        {
            int min = t1[0].Tochka1, minind = 0;
            foreach (Put rb in t1)
            {
                if (rb.Tochka1 <= min)
                {
                    min = rb.Tochka1;
                    minind = t1.IndexOf(rb);
                }
            }
            return minind;
        }
        /// <summary>
        /// Нахождение конечной точки
        /// </summary>
        /// <param name="t2"></param>
        /// <returns></returns>
        int Maximum(List<Put> t2)
        {
            int min = t2[0].Tochka2, maxind = 0;
            foreach (Put rb in t2)
            {
                if (rb.Tochka2 >= min)
                {
                    min = rb.Tochka1;
                    maxind = t2.IndexOf(rb);
                }
            }
            return maxind;
        }
        /// <summary>
        /// Посмтроение пути
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="minel"></param>
        /// <returns></returns>
        int MM(List<Put> ls, Put minel)
        {
            int ret = 0;
            Put Poisk = ls.Find(x => x.Tochka1 == minel.Tochka1 && x.Tochka2 == minel.Tochka2);//Поиск возможных вариантов передвижения
            Str += Poisk.Tochka1.ToString() + "-" + Poisk.Tochka2.ToString();//Пишем передвижение
            if (Poisk.Tochka2 == ls[Maximum(ls)].Tochka2)//Смотрим не в конце ли мы
            {
                Str += ";";
                return Poisk.Dlina;
            }
            else
            {
                for (int i = 0; i < ls.Count; i++)//Ищем стоимость перемещения в ту точку в которую мы пришли
                {
                    if (ls[i].Tochka1 == Poisk.Tochka2)
                    {
                        Str += ",";
                        ret = MM(ls, ls[i]) + Poisk.Dlina;
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// Чтение данные их файла
        /// </summary>
        /// <returns></returns>
        public static List<Put> ReadFile()
        {
            List<Put> RezultRead = new List<Put>();
            try
            {

                using (StreamReader Reader = new StreamReader("Input.csv"))
                {
                    while (Reader.EndOfStream != true)
                    {
                        string[] str1 = Reader.ReadLine().Split(';');
                        string[] str2 = str1[0].Split('-');
                        RezultRead.Add(new Put { Tochka1 = Convert.ToInt32(str2[0]), Tochka2 = Convert.ToInt32(str2[1]), Dlina = Convert.ToInt32(str1[1]) });
                    }
                }
                return RezultRead;
            }
            catch
            {
                Debug.WriteLine("Измените данные в файле");
                System.Environment.Exit(1);
                return RezultRead;
            }
        }
        /// <summary>
        /// Проверка на наличие ветвлений
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Put> Vetki(List<Put> ls, string s)
        {
            List<List<Put>> RezultPromez = new List<List<Put>>();
            string[] str1 = s.Split(';');
            foreach (string st1 in str1)
            {
                if (st1 != "")
                {
                    RezultPromez.Add(new List<Put>());
                    string[] str2 = st1.Split(',');
                    foreach (string st2 in str2)
                    {
                        if (st2 != "")
                        {
                            string[] str3 = st2.Split('-');
                            RezultPromez[RezultPromez.Count - 1].Add(ls.Find(x => x.Tochka1 == Convert.ToInt32(str3[0]) && x.Tochka2 == Convert.ToInt32(str3[1])));
                        }
                    }
                }
            }
            for (int i = 0; i < RezultPromez.Count; i++)
            {
                if (i > 0)
                {
                    if (RezultPromez[i][0].Tochka1 != RezultPromez[i][RezultPromez[i].Count - 1].Tochka2)
                    {
                        RezultPromez[i].InsertRange(0, RezultPromez[i - 1].FindAll(x => RezultPromez[i - 1].IndexOf(x) <= RezultPromez[i - 1].FindIndex(y => y.Tochka2 == RezultPromez[i][0].Tochka1)));
                    }
                }
            }
            int max = RezultPromez[0][0].Dlina, maxind = 0;
            for (int i = 0; i < RezultPromez.Count; i++)
            {
                if (RaschetDlini(RezultPromez[i]) >= max)
                {
                    max = RaschetDlini(RezultPromez[i]);
                    maxind = i;
                }
            }
            return RezultPromez[maxind];
        }
        /// <summary>
        /// Расчет длины пути
        /// </summary>
        /// <param name="Dlin"></param>
        /// <returns></returns>
        public int RaschetDlini(List<Put> Dlin)
        {
            int RezultProm = 0;
            foreach (Put rb in Dlin)
            {
                RezultProm += rb.Dlina;
            }
            return RezultProm;
        }
    }
  }


