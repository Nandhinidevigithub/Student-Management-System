using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//calling Referencess
using StudentException;
using StudentEntity;
using StudentDAL;

namespace Student_BL
{
    public class StudentBL
    {
        StudentDAO sDao = null;
        public StudentBL()
        {
            sDao = new StudentDAO();
        }
        public bool ValidateStudent(Student stud)
        {
            return true;
        }
        public bool AddStudent(Student sObj)
        {
            bool flag = false;
            try
            {
              flag=  sDao.AddStudent(sObj);
            }
            catch (SMSException se)
            {

                throw se;
            }
            return flag;
        }

        public bool UpdateStudent(int id, Student sObj)
        {
            return sDao.UpdateStudent(id, sObj);
        }

        public bool DropStudent(int id)
        {
            return sDao.DropStudent(id);
        }

        public Student ShowStudentDetails(int id)
        {
            return sDao.ShowStudentDetails(id);
        }

        public List<Student> ShowAllStudents()
        {
            return sDao.ShowAllStudents();
        }
    }
}
