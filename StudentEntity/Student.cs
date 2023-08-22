using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEntity
{
    /// <summary>
    /// Student Entity class 
    /// </summary>
    public class Student
    {
        #region Properties
        public int RollNo { get; set; }
        public string Name { get; set; }

       // public string Gender { get; set; }

        public int Phy { get; set; }
        public int Chem { get; set; }
        public int Maths { get; set; }

        public int Total
        { 
            get;
            set;
         }
        public float Percentage 
        { 
            get; 
            set; 
        }
        #endregion

    }
}
