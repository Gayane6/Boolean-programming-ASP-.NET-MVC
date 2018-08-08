using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Course_Work.Models;
using System.IO;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.Mvc;
using System.Web.Helpers;
namespace Course_Work.Models
{
    public class Generation
    {
        public Generator InputData { get; set; }
        public List <Result> Result { get; set; }

        public Generation(Generator generator)
        {
            InputData = generator;
            Result = new List<Result>();
        }       
        public Generation()
        {
            // TODO: Complete member initialization
        }
        int FromVariables;
        int ToVariables;
        int Step;
        int Number;
        int FromC;
        int ToC;
        string path1 = @"d:/Result.txt";
        string path2 = @"d:/GAGraphicResult.txt";
        string pathg1 = @"d:/Graphic.txt";
        string pathg2 = @"d:/GAGraphic.txt";
        public void createFile1(string text)
        {
            if (File.Exists(path1)==false)
            {
                File.WriteAllText(path1, text, Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(path1, text, Encoding.UTF8);
            }
        }
        public void createFileForGraghic1(string text)
        {
             

            if (File.Exists(pathg1) == false)
            {
                File.WriteAllText(pathg1, text, Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(pathg1, text, Encoding.UTF8);
            }
        }
        public void createFileForGraghic3(string text)
        {


            if (File.Exists(path2) == false)
            {
                File.WriteAllText(path2, text, Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(path2, text, Encoding.UTF8);
            }
        }
        public void createFileForGraghic2(string text)
        {
             
            if (File.Exists(pathg2) == false)
            {
                File.WriteAllText(pathg2, text, Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(pathg2, text, Encoding.UTF8);
            }
        }
        public int[,] Table(int[,] a, int[] b, int[] c)
        {
            int[,] table = new int[a.GetLength(0) + 1, a.GetLength(1) + 1];
            for (int i = 0; i < a.GetLength(0) + 1; i++)
                for (int j = 0; j < a.GetLength(1) + 1; j++)
                {
                    if (j == 0 && i != b.Length) table[i, j] = 1;
                    if (i == b.Length && j == 0) table[i, j] = 0;
                }
            for (int k = 1; k < a.GetLength(0) + 1; k++)
                for (int l = 1; l < a.GetLength(1) + 1; l++)
                {
                    table[k - 1, l] = a[k - 1, l - 1];
                    if (k == b.Length) table[k, l] = c[l - 1] * (-1);
                }
            return table;
        }
        public List<List<Result>> General()
        {
            FromVariables = InputData.FromVariables;
            ToVariables = InputData.ToVariables;
            Step = InputData.Step;
            Number = InputData.Number;
            FromC=InputData.FromC;
            ToC=InputData.ToC;
            string method = "";
            method = InputData.SelectedMethod.ToString();   
            List<List<Result>> res = new List<List<Result>>();
            int resultga;
            for (int i = FromVariables; i <= ToVariables; i += Step)
            {
                string Name = " ";
                string Name2 = " ";
                string Time = " ";
                string str = " ";
                string str1 = " ";
                string str2 = " ";
                string graphic1 = " ";
                string graphicb = " ";
                string ZZ = " ";
                int ZZint = 0;
                string XXFINAL = " ";
                resultga = 0;
                string garesult = " ";
                Random rand = new Random();
                string AA = " ";
                string BB = " ";
                string CC = " ";
                string XX = " ";
                int[] arrayc = new int[i];
                int[,] arraya = new int[Number, i];
                int[] arrayb = new int[Number];
                int[] arrayx = new int[i];
                int[] c = new int[i];
                int[,] a = new int[Number, i];
                int[] x = new int[i];
                int[] b = new int[Number];
                for (int k = 0; k < i; k++)
                {
                    c[k] = rand.Next(FromC, ToC);
                    arrayc[k] = c[k];
                    CC += c[k].ToString() + " ";
                }
                for (int k = 0; k < Number; k++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        a[k, j] = rand.Next(0, 2);
                        arraya[k, j] = a[k, j];
                        AA += a[k, j].ToString() + " ";
                    }
                    Console.WriteLine("/n");
                }
                for (int k = 0; k < i; k++)
                {
                    x[k] = 1;
                    arrayx[k] = x[k];
                    XX += x[k].ToString() + " ";
                }

                for (int k = 0; k < Number; k++)
                {
                    b[k] = 1;
                    arrayb[k] = b[k];
                    BB += b[k].ToString() + " ";

                }
               if (method == "Адитивний")
                { 
                    DateTime startTime = DateTime.Now;
                    int[] cc = new int[c.Length];
                    for (int k = 0; k < cc.Length; k++)
                    {
                        cc[k] = c[k];
                    }
                    Additive adit = new Additive();
                    int[] bt = adit.transformationB(a, b, c);
                    int[] ct = adit.transformationC(a, b, c);
                    int[,] at = adit.tramsformationA(a, b, c);
                    int[] xnew = adit.xfinal(at, bt, ct, x);
                    int[] xfin = adit.final(xnew, cc);
                    for (int k = 0; k < xfin.Length; k++)
                    {
                        XXFINAL += xfin[k].ToString();
                        ZZint += xfin[k] * ct[k];
                    }
                    ZZ = ZZint.ToString();
                    Name = "Метод неявного перебору по векторній решітці";
                    DateTime endTime = DateTime.Now;
                    Time = endTime.Subtract(startTime).Milliseconds.ToString();
                    Result.Add(new Models.Result
                    {
                        AA = AA,
                        BB = BB,
                        CC = CC,
                        XX = XX,
                        XFinal = XXFINAL,
                        ZFinal = ZZ,
                        Name1 = Name,
                        Time = Time
                    });
                    str += Name + "\r\n" + CC + "\r\n" + AA + "\r\n" + BB + "\r\n" + XXFINAL + "\r\n" + ZZ + "\r\n" + Time + "\r\n";
                    graphic1 += i.ToString() + " " + Time + " ";

                }
                if (method == "Генетичний")
                {
                     
                    int[,] aa = new int[b.Length, c.Length];
                    for (int k = 0; k < a.GetLength(0); k++)
                        for (int j = 0; j < a.GetLength(1); j++)
                            aa[k, j] = a[k, j];
                    int[] bb;
                    int[] cc;
                    Array.Copy(b, bb = new int[b.Length], b.Length);
                    Array.Copy(c, cc = new int[c.Length], c.Length);
                    DateTime startTime = DateTime.Now;
                    int[,] table = Table(aa, bb, cc);
                    int[] result = new int[table.GetLength(1)];
                    int[,] table_result;
                    string XXFINAL1 = "";
                    int Z = 0;
                    string ZZ1 = "";
                    GA S = new GA(table);
                    table_result = S.Calculate(result);
                    for (int k = 0; k < table.GetLength(1) - 1; k++)
                    {
                        Z += result[k] * cc[k];
                        XXFINAL1 += result[k].ToString() + " ";
                    }
                    int record = Z;
                    int[] result1 = S.main_function(cc, bb, aa, i, record);
                    ZZ1 = Z.ToString();
                    DateTime endTime = DateTime.Now;
                    Name2 = "Генетичний алгоритм";
                    Time = endTime.Subtract(startTime).Milliseconds.ToString();
                    Result.Add(new Models.Result
                    {
                        
                        AA = AA,
                        BB = BB,
                        CC = CC,
                        XX = XX,
                        XFinal = XXFINAL1,
                        ZFinal = ZZ1,
                        Name1 = Name2,
                        Time = Time
                    });
                    str += Name2 + "\r\n" + CC + "\r\n" + AA + "\r\n" + BB + "\r\n" + XXFINAL1 + "\r\n" + ZZ1 + "\r\n" + Time + "\r\n";
                    graphicb += i.ToString() + " " + Time + " ";
                }
                if (method == "Обидва")
                {

                    DateTime startTime = DateTime.Now;

                    int[] cc = new int[c.Length];
                    for (int k = 0; k < cc.Length; k++)
                    {
                        cc[k] = c[k];
                    }
                    Additive adit = new Additive();
                    int[] bt = adit.transformationB(a, b, c);
                    int[] ct = adit.transformationC(a, b, c);
                    int[,] at = adit.tramsformationA(a, b, c);
                    int[] xnew = adit.xfinal(at, bt, ct, x);
                    int[] xfin = adit.final(xnew, cc);
                    for (int k = 0; k < xfin.Length; k++)
                    {
                        XXFINAL += xfin[k].ToString();
                        ZZint += xfin[k] * cc[k];
                    }
                    ZZ = ZZint.ToString();
                    Name = "Метод неявного перебору по векторній решітці";
                    DateTime endTime = DateTime.Now;
                    Time = endTime.Subtract(startTime).Milliseconds.ToString();
                    Result.Add(new Models.Result
                    {
                        AA = AA,
                        BB = BB,
                        CC = CC,
                        XX = XX,
                        XFinal = XXFINAL,
                        ZFinal = ZZ,
                        Name1 = Name,
                        Time = Time
                    });
                    str += Name + "\r\n" + CC + "\r\n" + AA + "\r\n" + BB + "\r\n" + XXFINAL + "\r\n" + ZZ + "\r\n" + Time + "\r\n";
                    graphic1 += i.ToString() + " " + Time + " ";
                    int[,] aa = new int[b.Length, c.Length];
                    for (int k = 0; k < a.GetLength(0); k++)
                        for (int j = 0; j < a.GetLength(1); j++)
                            aa[k, j] = a[k, j];
                    int[] bb;
                    int[] cc1;
                    Array.Copy(b, bb = new int[b.Length], b.Length);
                    Array.Copy(c, cc1 = new int[c.Length], c.Length);
                    DateTime startTime1 = DateTime.Now;
                    int[,] table = Table(aa, bb, cc1);
                    int[] result = new int[table.GetLength(1)];
                    int[,] table_result;
                    string XXFINAL1 = "";
                    int Z = 0;
                    int Z1 = 0;
                    string ZZ1 = "";
                    GA S = new GA(table);
                    table_result = S.Calculate(result);
                    for (int k = 0; k < table.GetLength(1) - 1; k++)
                    {
                        Z += result[k] * cc[k];
                        XXFINAL1 += result[k].ToString() + " ";
                    }
                    int record = Z;
                    int[] result1 = S.main_function(cc, bb, aa, i, record);
                    ZZ1 = Z.ToString();
                    DateTime endTime1 = DateTime.Now;
                    Name2 = "Генетичний алгоритм";
                    Time = endTime.Subtract(startTime).Milliseconds.ToString();
                    Result.Add(new Models.Result
                    {

                        AA = AA,
                        BB = BB,
                        CC = CC,
                        XX = XX,
                        XFinal = XXFINAL1,
                        ZFinal = ZZ1,
                        Name1 = Name2,
                        Time = Time
                    });
                     
                   
                    
                        str += Name2 + "\r\n" + CC + "\r\n" + AA + "\r\n" + BB + "\r\n" + XXFINAL1 + "\r\n" + ZZ1 + "\r\n" + Time + "\r\n";
                        graphicb += i.ToString() + " " + Time + " ";
                        garesult += i.ToString() + " " + resultga.ToString() + " ";
                    
                }
                    createFile1(str);
                    createFileForGraghic3(garesult);
                    createFileForGraghic1(graphic1);
                    createFileForGraghic2(graphicb);

                    res.Add(Result);
                }
            
            return res;
             
        }
    }
}
























//table_result = S1.Calculate(result);
//for (int i = 0; i < table.GetLength(1) - 1; i++)
//{

//    Z1 += result[i] * cc1[i];
//    XXFINAL1 += result[i].ToString() + " ";
//}
//int[,] table_result;
//string XXFINAL1 = "";