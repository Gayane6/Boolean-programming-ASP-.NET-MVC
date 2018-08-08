using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
namespace Course_Work.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View(); 
        }
        string[] Xalg2;
        string[] Yalg2;
        string[] Xalg1;
        string[] Yalg1;
        string[] Xalg3;
        string[] Yalg3;
        int length1;
        int length2;
        int length3;
        void readFile1()
        {
            string path2 = @"e:/GAGraphic.txt";
            FileStream file2 = new FileStream(path2, FileMode.Open);
            StreamReader sw2 = new StreamReader(file2);
            string text2 = sw2.ReadToEnd();
            List<string> Xalg02 = new List<string>();
            List<string> Yalg02 = new List<string>();
            string[] array2 = text2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            length2 = array2.Length;
            for (int i = 0; i < length2; i++)
            {
                if (i % 2 == 0 || i == 0)
                {
                    Xalg02.Add(array2[i]);
                }
                else
                {
                    Yalg02.Add(array2[i]);
                }
            }
            Xalg2 = Xalg02.ToArray();
            Yalg2 = Yalg02.ToArray();
            sw2.Close();
        }
        void readFile()
        {
            string path1 = @"e:/Graphic.txt";
            FileStream file1 = new FileStream(path1, FileMode.Open);
            StreamReader sw1 = new StreamReader(file1);
            string text1 = sw1.ReadToEnd();
            List<string> Xalg01 = new List<string>();
            List<string> Yalg01 = new List<string>();
            string[] array1 = text1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            length1 = array1.Length;
            for (int i = 0; i < length1; i++)
            {
               if (i % 2 == 0 || i == 0)
                {
                    Xalg01.Add(array1[i]);
                }
                else
                {
                    Yalg01.Add(array1[i]);
                }
            }
            Xalg1 = Xalg01.ToArray();
            Yalg1 = Yalg01.ToArray();
            sw1.Close();
        }

        void readFile2()
        {
            string path1 = @"e:/GAGraphicResult.txt";
            FileStream file1 = new FileStream(path1, FileMode.Open);
            StreamReader sw1 = new StreamReader(file1);
            string text1 = sw1.ReadToEnd();
            List<string> Xalg03 = new List<string>();
            List<string> Yalg03 = new List<string>();
            string[] array1 = text1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            length3 = array1.Length;
            for (int i = 0; i < length3; i++)
            {
                if (i % 2 == 0 || i == 0)
                {
                    Xalg03.Add(array1[i]);
                }
                else
                {
                    Yalg03.Add(array1[i]);
                }
            }
            Xalg3 = Xalg03.ToArray();
            Yalg3 = Yalg03.ToArray();
            sw1.Close();

        }
        public ActionResult ReportCompare()
        {
            readFile();
            readFile1();
            Chart chart1 = new Chart(width: 1200,
                    height: 600,
                    theme: ChartTheme.Green)
           .AddLegend("", "")
           .AddTitle("Графік залежності часу від кількості змінних")
           .AddSeries(
           name: "Адитивний", chartType: "Line", axisLabel: "Time",
               xValue: Xalg1,
               yValues: Yalg1)
                .AddSeries(
           name: "Генетичний", chartType: "Line",
               xValue: Xalg2,
               yValues: Yalg2)
           .Write();
            return View();
        }
        public ActionResult ReportResult()
        {
            readFile2();
            Chart chart1 = new Chart(width: 1200,
                    height: 600,
                    theme: ChartTheme.Green)
           .AddLegend("", "")
           .AddTitle("Графік залежності проценту успішного рішення від розмірності задачі")
                .AddSeries(
           name: "Генетичний", chartType: "Line",
               xValue: Xalg3,
               yValues: Yalg3)

           .Write();
            return View();
        }
    }
}