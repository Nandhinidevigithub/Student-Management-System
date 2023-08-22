using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//calling References
using StudentException;
using StudentEntity;

using System.IO;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudentDAL
{
    public class StudentDAO
    {
        // static List<Student> myStudList = new List<Student>();

        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader sdr = null;

        SqlParameter[] param = null;

        
        public StudentDAO()
        {
            con = new SqlConnection();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            // con.ConnectionString = "Server=.;Integrated Security=SSPI;Database=Cgapril20";
            param = new SqlParameter[5];

        }

        public bool AddStudent(Student sObj)
        {
            bool flag = true;
            int result = 0;
            try
            {
                if (sObj != null)
                {
                    // myStudList.Add(sObj);
                    con.Open();

                    //Init Parameters

                    param[0] = new SqlParameter("@Rno", sObj.RollNo);
                    param[1] = new SqlParameter("@Name", sObj.Name);
                    param[2] = new SqlParameter("@Phy", sObj.Phy);
                    param[3] = new SqlParameter("@Chem", sObj.Chem);
                    param[4] = new SqlParameter("@Math", sObj.Maths);

                    cmd = new SqlCommand();
                    cmd.CommandText = "Insert into StudentScores values(@Rno,@Name,@Phy,@Chem,@Math)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.Parameters.AddRange(param);

                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                    throw new SMSException("Student Object is not Initialized...");
                }
            }
            catch (SMSException se)
            {

                throw se;
            }
            return flag;
        }

        public bool UpdateStudent(int id, Student sObj)
        {
            bool flag = true;
            int result = 0;
            //Student tempStud = myStudList.FirstOrDefault(s => s.RollNo == id);
            try
            {
                if (sObj != null)
                {

                    con.Open();

                    //Init Parameters

                    param[0] = new SqlParameter("@Rno", sObj.RollNo);
                    param[1] = new SqlParameter("@Name", sObj.Name);
                    param[2] = new SqlParameter("@Phy", sObj.Phy);
                    param[3] = new SqlParameter("@Chem", sObj.Chem);
                    param[4] = new SqlParameter("@Math", sObj.Maths);

                    cmd = new SqlCommand();
                    cmd.CommandText = "Update StudentScores set  Name=@Name,Phy=@Phy,Chem=@Chem,Maths=@Math where RollNo=@Rno";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.Parameters.AddRange(param);

                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                    throw new SMSException("Student Record is not Updated...");
                }
            }
            catch (SMSException se)
            {
                throw se;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return flag;

        }

        public bool DropStudent(int id)
        {
            bool flag = true;
            int result = 0;

            try
            {
                if (id > 0)
                {
                    // myStudList.Remove(tempStud);
                    con.Open();

                    //Init Parameters
                    SqlParameter p1 = new SqlParameter("@Rno", id);

                    cmd = new SqlCommand();
                    cmd.CommandText = "Delete from StudentScores where RollNo=@Rno";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.Parameters.Add(p1);

                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        flag = true;
                    }

                }
                else
                {
                    flag = false;
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }


            return flag;
        }

        public Student ShowStudentDetails(int id)
        {
            // Student tempStud = myStudList.FirstOrDefault(s => s.RollNo == id);
            Student tempStud = null;
            try
            {
                if (id > 0)
                {
                    con.Open();

                    //Init Parameters
                    SqlParameter p1 = new SqlParameter("@Rno", id);

                    cmd = new SqlCommand();
                    cmd.CommandText = "Select * from  StudentScores where RollNo=@Rno";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.Parameters.Add(p1);

                    sdr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    if (sdr.HasRows)
                    {
                        dt.Load(sdr);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        DataRow drow = dt.Rows[0];//Fetch Row from Data Table
                        //Assign Row Data to Student Object
                        tempStud = new Student();
                        tempStud.RollNo = Int32.Parse(drow[0].ToString());
                        tempStud.Name = drow[1].ToString();
                        tempStud.Phy = Int32.Parse(drow[2].ToString());
                        tempStud.Chem = Int32.Parse(drow[3].ToString());
                        tempStud.Maths = Int32.Parse(drow[4].ToString());
                        tempStud.Total = Int32.Parse(drow[5].ToString());
                        tempStud.Percentage = Int32.Parse(drow[6].ToString());
                    }


                    return tempStud;
                }
                else
                {
                    throw new SMSException($"No Student Found with Rollno : {id}");
                }
            }
            catch (SqlException e)
            {
                throw new SMSException(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public List<Student> ShowAllStudents()
        {
            List<Student> myStudList = null;
            Student tempStud = null;
            try
            {

                con.Open();

                //Init Parameters


                cmd = new SqlCommand();
                cmd.CommandText = "Select * from  StudentScores";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;


                sdr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                }

                if (dt.Rows.Count > 0)
                {
                    myStudList = new List<Student>();
                    foreach (DataRow drow in dt.Rows)
                    {

                        //Assign Row Data to Student Object
                        tempStud = new Student();
                        tempStud.RollNo = Int32.Parse(drow[0].ToString());
                        tempStud.Name = drow[1].ToString();
                        tempStud.Phy = Int32.Parse(drow[2].ToString());
                        tempStud.Chem = Int32.Parse(drow[3].ToString());
                        tempStud.Maths = Int32.Parse(drow[4].ToString());
                        tempStud.Total = Int32.Parse(drow[5].ToString());
                        tempStud.Percentage = Int32.Parse(drow[6].ToString());

                        //Add TempStudent in to List
                        myStudList.Add(tempStud);
                    }


                   // return myStudList;
                }
                else
                {
                    throw new SMSException($"No Student data Found  ");
                }
            }
            catch (SqlException e)
            {
                throw new SMSException(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return myStudList;
        }
    }
}
