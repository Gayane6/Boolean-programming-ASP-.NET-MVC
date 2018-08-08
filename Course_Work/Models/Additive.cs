using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace Course_Work.Models
{
    public class Additive
    {
        int[,] a;
        int[] b;
        int[] c;
        int[] x;
        int[] cc;
        public int[] transformationB(int[,] at, int[] bt, int[] ct)
        {
            ArrayList position = new ArrayList();
            int[] count = new int[bt.Length];
            //ЦФ
            for (int i = 0; i < ct.Length; i++)
            {
                if (ct[i] < 0)
                    ct[i] = ct[i] * (-1);
                else if (ct[i] >= 0) position.Add(i);
            }
            //Ограничения
            for (int i = 0; i < bt.Length; i++)
            {
                for (int j = 0; j < ct.Length; j++)
                {
                    for (int k = 0; k < position.Count; k++)
                    {
                        if (j == (int)position[k])
                        {
                            at[i, j] = at[i, j] * (-1);
                            if (at[i, j] < 0) count[i]++;
                        }

                    }
                }
                bt[i] = bt[i] - count[i];

            }
            return bt;
        }
        public int[,] tramsformationA(int[,] at, int[] bt, int[] ct)
        {
            ArrayList position = new ArrayList();
            int[] count = new int[ct.Length];
            for (int i = 0; i < bt.Length; i++)
            {
                for (int j = 0; j < ct.Length; j++)
                {
                    for (int k = 0; k < position.Count; k++)
                    {
                        if (j == (int)position[k])
                        {
                            at[i, j] = at[i, j] * (-1);
                            if (at[i, j] < 0) count[i]++;
                        }
                    }
                }
            }
            return at;
        }
        public int[] transformationC(int[,] at, int[] bt, int[] ct)
        {
            ArrayList position = new ArrayList();
            //ЦФ
            for (int i = 0; i < ct.Length; i++)
            {
                if (ct[i] < 0)
                    ct[i] = ct[i] * (-1);
                else if (ct[i] > 0) position.Add(i);

            }
            return ct;
        }
        public int[] test1(int[,] a, int[] b, int[] c, int[] x)
        {
            ArrayList position = new ArrayList();
            int countb = 0;
            int count = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] < 0)
                {
                    for (int j = 0; j < c.Length; j++)
                    {
                        if (a[i, j] >= 0)
                            position.Add(j);
                    }
                    countb++;
                }
            }
            Console.WriteLine(countb);
            for (int k = 0; k < position.Count; k++)
            {
                for (int j = 0; j < position.Count; j++)
                {
                    if (Convert.ToInt32(position[k]) == Convert.ToInt32(position[j]))
                    {
                        count++;
                    }
                }
                if (count == countb) x[(int)(position[k])] = 0;
                count = 0;
            }
            return x;
        }
        public int[] test3(int[,] a, int[] b, int[] c, int[] x)
        {
            int[] sum = new int[b.Length];
            bool[] boolean = new bool[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                boolean[i] = true;
            }
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < c.Length; j++)
                {
                    if (x[j] == 1 && b[i] < 0)
                    {
                        sum[i] += a[i, j] * x[j];
                    }
                }
                if (b[i] < 0 && sum[i] <= b[i]) boolean[i] = true;
            }
            for (int i = 0; i < b.Length; i++)
            {
                if (boolean[i] == false) x[i] = 0;
            }
            return x;
        }
        public int[] sumsum(int[,] a, int[] b, int[] c, int[] x)
        {
            int[] sum = new int[x.Length];
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    if (x[j] == 1)
                    {
                        sum[j] += Math.Min(0, b[i] - a[i, j] * x[j]);
                    }
                    else sum[j] = int.MinValue;
                }
            }
            return sum;
        }
        public int findmax(int[] sum, int[] c, int[] x)
        {
            int newindex = 0;
            int max = int.MinValue;
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    if (x[i] == 1 && sum[i] != int.MinValue)
                    {
                        if (sum[i] > max)
                        {
                            if (sum[i] == sum[j] && c[i] > c[j])
                            {
                                max = sum[i];
                                newindex = i;
                            }
                            else
                            {
                                max = sum[i];
                                newindex = i;
                            }
                        }
                    }
                }
            }
            return newindex;
        }

        public int[] zfunc(int[,] a, int[] b, int[] c, int[] x)
        {
            int[] z = new int[c.Length];
            int[] xnew = new int[x.Length];
            int zmax = 0;
            int count1 = 0;
            int indexz = 0;
            int[] x1 = test1(a, b, c, x);
            int[] xx = test3(a, b, c, x1);
            int[] sum = sumsum(a, b, c, xx);
            int index = findmax(sum, c, xx);//получение первого индекса
            while (count1 < x.Length + 1)
            {
                for (int i = 0; i < c.Length; i++)
                {
                    if (i == index) z[index] = c[index] * xx[index];
                    z[i] = c[i] * xx[i];
                    Console.WriteLine("z new new" + i + " = " + z[i]);
                }
                for (int i = 0; i < x.Length; i++)  //обнуляем недопустимые точки
                {
                    if (z[index] < z[i] && xx[i] != 0)
                        xx[i] = 0;
                }
                for (int i = 0; i < c.Length; i++)  //создаем массив копию х
                {
                    xnew[i] = xx[i];
                    Console.WriteLine("xxxxx = " + i + " = " + xnew[i]);  
                }  

                //присвоение пройденной сумме значения
                sum[index] = int.MinValue;
                //подсчет минимальных значений
                for (int i = 0; i < x.Length; i++)
                {
                    if (sum[i] == int.MinValue)
                        count1++;
                }
                index = findmax(sum, c, xnew);//находим новый индекс
            }
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] > zmax)
                    zmax = c[i];
                if (c[i] == zmax) indexz = i;
            }
            for (int i = 0; i < c.Length; i++)
            {
                Console.WriteLine("xnew " + i + " = " + xnew[i]);             
            }
            return x;
        }

        public int[] final(int[] x, int[] cc)
        {
            int[] xx = new int[x.Length];
            int z = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (cc[i] > 0) xx[i] = 1 - x[i];               
            }

            for (int i = 0; i < x.Length; i++)
            {
                z += xx[i] * cc[i];
            }             
            return xx;
        }
        public int[] xfinal(int[,] a, int[] b, int[] c, int[] x)
        {
            int[] x3 = zfunc(a, b, c, x);
            return x3;
        }
    }
}