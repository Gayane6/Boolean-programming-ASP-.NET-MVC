using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
 

namespace Course_Work.Models
{
    public class Result
    {      
        public int FromVariables { get; set; }       
        public int ToVariables { get; set; }      
        public int FromC { get; set; }
        public int ToC { get; set; }
        public int Number { get; set; }        
        public int Z { get; set; }
        public string MaxMin { get; set; }
        public string Time{ get; set; }
        public int[,] A { get; set; }
        public int[] B { get; set; }
        public int[] C { get; set; }
        public int[] X { get; set; }
        public string AA { get; set; }
        public string BB { get; set; }
        public string CC { get; set; }
        public string XX { get; set; }
        public string XFinal { get; set; }
        public string ZFinal { get; set; }
        public string Name1 { get; set; }
   

        
    }
}