using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Work.Models
{
    public class GA
    {
        int[,] table; 
        int m, n;
        List<int> basis; 
        public GA(int[,] source)
        {
            m = source.GetLength(0);
            n = source.GetLength(1);
            table = new int[m, n + m - 1];
            basis = new List<int>();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (j < n)
                        table[i, j] = source[i, j];
                    else
                        table[i, j] = 0;
                }
                 
                if ((n + i) < table.GetLength(1))
                {
                    table[i, n + i] = 1;
                    basis.Add(n + i);
                }
            }
            n = table.GetLength(1);
        }       
        public int[,] Calculate(int[] result)
        {
            int mainCol, mainRow; 

            while (!IsItEnd())
            {
                mainCol = findMainCol();
                mainRow = findMainRow(mainCol);
                basis[mainRow] = mainCol;

                int[,] new_table = new int[m, n];

                for (int j = 0; j < n; j++)
                {
                    try
                    {
                        new_table[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];
                    }
                    catch (System.DivideByZeroException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        break;
                    }
                }
                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;

                    for (int j = 0; j < n; j++)
                        new_table[i, j] = table[i, j] - table[i, mainCol] * new_table[mainRow, j];
                }
                table = new_table;
            }
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = table[k, 0];
                else
                    result[i] = 0;
            }

            return table;
        }
        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < n; j++)
            {
                if (table[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }
        private int findMainCol()
        {
            int mainCol = 1;

            for (int j = 2; j < n; j++)
                if (table[m - 1, j] < table[m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }
        private int findMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i = 0; i < m - 1; i++)
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }

            for (int i = mainRow + 1; i < m - 1; i++)
                if ((table[i, mainCol] > 0) && ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                    mainRow = i;

            return mainRow;
        }
        static Random random = new Random();
        bool check = false;
        public int[] main_function(int[] c, int[] b, int[,] a, int startx, int record)
        {
            int genom = 1000;
            List<int[]> list = Genotype(startx, genom);
            int index = 0;
            int iteration = 0;
            int ClosestLeft = 0;
            int ClosestRight = 0;
            int[] sum = new int[b.Length];
            {
                List<int[]> list1 = fitness(list, c, record, genom);
                List<int> list2 = parents(list1, c, record);
                int[] array1 = Crossover(list1, list2, c);
                int[] mutated = Mutate(array1);
                List<int[]> list3 = Optimize(list1, mutated, c);

                int[] current = new int[list3.Count];

                for (int j = 0; j < list3.Count; j++)
                {
                    for (int i = 0; i < c.Length; i++)
                    {
                        current[j] += list3[j][i] * c[i];
                    }
                    if (current[j] == record)
                    {
                        index = j;
                        break;    
                    }
                    }
                    list = list3;

                    int[] current1 = new int[list.Count];
                    int Z = 0;

                    for (int j = 0; j < list.Count; j++)
                    {
                        for (int i = 0; i < c.Length; i++)
                        {
                            current1[j] += list[j][i] * c[i];
                        }
                        Z += current1[j];
                    }
                    iteration++;
                    if (iteration == 99 && check == false)
                    {
                        Array.Sort(current);
                        for (int i = 0; i < current.Length; i++)
                        {
                            int data = current[i];
                            if (data < record && ClosestLeft < data)
                                ClosestLeft = data;
                            else if (data > record && ClosestRight > data)
                                ClosestRight = data;
                        }
                        if (Math.Abs(ClosestLeft - record) < Math.Abs(ClosestRight - record))
                        {
                            for (int i = 0; i < current.Length; i++)
                            {
                                if (current[i] == ClosestLeft) index = i;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < current.Length; i++)
                            {
                                if (current[i] == ClosestRight) index = i;
                            }

                        }
                    }   
                   return list3[index];
                } while (iteration < 200) ;     
            Console.ReadKey();
        }
        /// Метод для формирования начальной популяции
        public static List<int[]> Genotype(int startx, int genom)
        {
            List<int[]> genotype = new List<int[]>();
            int[] chromosome = new int[startx];
            for (int j = 0; j < genom; j++)
            {

                for (int i = 0; i < startx; i++)
                {
                    chromosome[i] = random.Next(0, 2);
                }
                genotype.Add(chromosome);
                chromosome = new int[startx];
            }
            for (int j = 0; j < genotype.Count; j++)
            {
                for (int i = 0; i < startx; i++)
                {
                    Console.Write(genotype[j][i]);
                }
                Console.WriteLine("\n");
            }
            return genotype;
        }
        /// Метод для проверки недопустимых решений 

        public static List<int[]> fitness(List<int[]> list, int[] c, int record, int genom)
        {
            int[] current = new int[list.Count];

            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < c.Length; i++)
                {
                    current[j] += list[j][i] * c[i];
                }
            }
            for (int j = 0; j < list.Count; j++)
            {
                if (current[j] > record)
                {
                    Console.Write(current[j] + "\n");
                    list.RemoveAt(j);
                }
            }
            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < c.Length; i++)
                {
                    Console.Write(list[j][i]);
                }
                Console.WriteLine("\n");
            }
            return list;
        }
        public static List<int> parents(List<int[]> list, int[] c, int record)
        {
            int parent1 = 0;
            List<int> parents = new List<int>();
            int[] current = new int[list.Count];
            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < c.Length; i++)
                {
                    current[j] += list[j][i] * c[i];
                }
                if (current[j] == record)
                    parent1 = j;
            }
            int parent2 = random.Next(0, list.Count + 1);
            parents.Add(parent1);
            parents.Add(parent2);
            Console.Write("parent1 = " + parent1);
            Console.Write("parent2 = " + parent2);
            return parents;
        }
        public static int[] Crossover(List<int[]> list, List<int> parent, int[] c)
        {
            int[] array = new int[c.Length];
            int n = random.Next(0, c.Length);
            Console.Write("n = " + n + "\n");
            for (int i = 0; i < c.Length; i++)
            {
                array[i] = list[parent[0]][i];
                if (i > n)
                {
                    array[i] = list[parent[1]][i];
                }
            }
             
            return array;
        }
        public static int[] Mutate(int[] array)
        {
            int n = random.Next(0, array.Length);
            List<int> indexs = new List<int>();
            for (int i = 0; i < n; i++)
            {
                indexs.Add(random.Next(0, array.Length));
            }
            for (int i = 0; i < indexs.Count; i++)
            {

                array[indexs[i]] = array[indexs[i]] % 2;
            }
            return array;
        }
        public static List<int[]> Optimize(List<int[]> list, int[] array, int[] c)
        {
            int[] current = new int[list.Count];
            int min = int.MaxValue;
            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < c.Length; i++)
                {
                    current[j] += list[j][i] * c[i];
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (current[i] < min)
                {
                    min = i;
                }

            }
            for (int i = 0; i < array.Length; i++)
            {
                list[min][i] = array[i];
            }
            return list;
        }


    }
}