using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM2
{
    class MemberDAL
    {
        static string myconnstrn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //helper funciotns
        #region addMember and addTime 
        public int calculateTimeLeft(DateTime lastUpdate, int timeLeft)
        {
            int newTimeLeft = 0;
            DateTime date1 = lastUpdate;
            newTimeLeft = (date1 - DateTime.Now).Days;

            if (newTimeLeft < 0)
            {
                newTimeLeft = 0;
            }

            return newTimeLeft;
        }
        public DataTable afterAdd()
        {
            SqlConnection conn = new SqlConnection(myconnstrn);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT  MID, Fname, Lname, TimeValid, TimeLeft from MemberInfo";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        public DataTable onLoadCalculate()
        {
            SqlConnection conn = new SqlConnection(myconnstrn);

            //Create datatable
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            int newTimeLeft = 0;

            try
            {
                string sql = "SELECT MID, TimeValid, TimeLeft from MemberInfo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = int.Parse(dr["MID"].ToString());
                    newTimeLeft = calculateTimeLeft(DateTime.Parse(dr["TimeValid"].ToString()), int.Parse(dr["TimeLeft"].ToString()));

                    string sql2 = "UPDATE MemberInfo set TimeLeft=@TimeLeft WHERE MID=@mid";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@TimeLeft", newTimeLeft);
                    cmd2.Parameters.AddWithValue("@mid", id);

                    int rows = cmd2.ExecuteNonQuery();
                }
                string sql3 = "SELECT  MID, Fname, Lname, TimeValid, TimeLeft from MemberInfo";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt2);
            }
            catch (Exception ex)
            {
                //display error message
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close db connection
                conn.Close();
            }
            return dt2;
        }
        public bool SelectAddTime(MemberBLL bll, int timeInMonths)
        {
            DateTime newDT;
            DateTime oldDT;
            int TimeLeft;
            bool isSuccess = false;
            if (bll.TimeLeft > 0)
            {
                oldDT = DateTime.Parse(bll.TimeValid.ToString());
                newDT = oldDT.AddMonths(timeInMonths);
                TimeLeft = (newDT - DateTime.Now).Days;
            }
            else
            {
                newDT = DateTime.Now.AddMonths(timeInMonths);
                TimeLeft = (newDT - DateTime.Now).Days;
            }

            SqlConnection conn = new SqlConnection(myconnstrn);

            try
            {
                string sql = "UPDATE MemberInfo Set TimeValid=@TimeValid, TimeLeft=@TimeLeft WHERE MID=@MID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@TimeValid", newDT.ToString());
                cmd.Parameters.AddWithValue("@TimeLeft", TimeLeft.ToString());
                cmd.Parameters.AddWithValue("@MID", bll.ID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;

        }
        public bool Insert(MemberBLL bll)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrn);

            try
            {
                string sql = "INSERT INTO MemberInfo(Fname,Lname,Gender,Email, Dob, mAddress, JoinDate, TimeValid, TimeLeft, Mobile)" +
                "VALUES(@fname,@lname, @gender, @email, @Dob, @mAddress, @JoinDate, @TimeValid, @TimeLeft, @Mobile)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@fname", bll.FirstName);
                cmd.Parameters.AddWithValue("@lname", bll.Lastname);
                cmd.Parameters.AddWithValue("@gender", bll.Gender);
                cmd.Parameters.AddWithValue("@email", bll.Email);
                cmd.Parameters.AddWithValue("@Dob", bll.DOB);
                cmd.Parameters.AddWithValue("@mAddress", bll.Address);
                cmd.Parameters.AddWithValue("@JoinDate", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@TimeValid", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@TimeLeft", "0");
                cmd.Parameters.AddWithValue("@Mobile", bll.Mobile);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return isSuccess;
        }

        public DataTable search(string keyword)
        {
            SqlConnection conn = new SqlConnection(myconnstrn);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT MID, Fname, Lname, TimeValid, TimeLeft from MemberInfo WHERE MID LIKE '%" + keyword + "%'" +
                "OR Fname LIKE '%" + keyword + "%' OR Lname LIKE'%" + keyword + "%'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion

        #region CheckTime
        public DataTable onLoadCalculate_CheckTime()
        {
            SqlConnection conn = new SqlConnection(myconnstrn);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            int newTimeLeft = 0;

            try
            {
                string sql = "SELECT MID, TimeValid, TimeLeft from MemberInfo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = int.Parse(dr["MID"].ToString());
                    newTimeLeft = calculateTimeLeft(DateTime.Parse(dr["TimeValid"].ToString()), int.Parse(dr["TimeLeft"].ToString()));

                    string sql2 = "UPDATE MemberInfo set TimeLeft=@TimeLeft WHERE MID=@mid";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@TimeLeft", newTimeLeft);
                    cmd2.Parameters.AddWithValue("@mid", id);

                    int rows = cmd2.ExecuteNonQuery();
                }
                string sql3 = "SELECT  MID, Fname, Lname, TimeLeft from MemberInfo";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt2);
            }
            catch (Exception ex)
            {
                //display error message
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close db connection
                conn.Close();
            }
            return dt2;
        }
        public DataTable loadData_CheckTIme()
        {
            SqlConnection conn = new SqlConnection(myconnstrn);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT MID, Fname, Lname, TimeLeft from MemberInfo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #endregion

        #region UpdateData

        public DataTable loadData_UpdateInfo()
        {

            SqlConnection conn = new SqlConnection(myconnstrn);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT MID, Fname, Lname, Gender, Email, Dob, mAddress, Mobile from MemberInfo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return dt;
        }

        public DataTable search_UpdateInfo(string keyword)
        {
            SqlConnection conn = new SqlConnection(myconnstrn);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT MID, Fname, Lname, Gender, Email, Dob, mAddress, Mobile from MemberInfo WHERE MID LIKE '%" + keyword + "%'" +
                "OR Fname LIKE '%" + keyword + "%' OR Lname LIKE'%" + keyword + "%' OR Email LIKE '%" + keyword + "%'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public bool upadte_UpdateInfo(MemberBLL bll)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrn);

            try
            {
                string sql = "UPDATE MemberInfo SET Fname=@Fname, Lname=@Lname, Gender=@Gender, Email=@Email, DOB=@DOB," +
                "mAddress=@mAddress, Mobile=@Mobile WHERE MID=@MId";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Fname", bll.FirstName);
                cmd.Parameters.AddWithValue("@Lname", bll.Lastname);
                cmd.Parameters.AddWithValue("@Gender", bll.Gender);
                cmd.Parameters.AddWithValue("@Email", bll.Email);
                cmd.Parameters.AddWithValue("@DOB", bll.DOB);
                cmd.Parameters.AddWithValue("@mAddress", bll.Address);
                cmd.Parameters.AddWithValue("@Mobile", bll.Mobile);
                cmd.Parameters.AddWithValue("@MID", bll.ID);

                conn.Open();
                //create and integer variable to hol value afte query is excecuted
                int rows = cmd.ExecuteNonQuery();

                //the value of rows will be greater than 0 if queery excecuted succesfully
                // else it will be 0
                if (rows > 0)
                {
                    //Query excecuted sufesfullly
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return isSuccess;
        }
        #endregion

        #region Delete
        public DataTable Select_Delete()
        {
            SqlConnection conn = new SqlConnection(myconnstrn);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT MID, Fname, Lname from MemberInfo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }


            return dt;
        }
        public DataTable Search_Delete(string keyword)
        {
            SqlConnection conn = new SqlConnection(myconnstrn);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT MID, Fname, Lname from MemberInfo WHERE MID LIKE '%" + keyword + "%'" +
                  "OR Fname LIKE '%" + keyword + "%' OR Lname LIKE'%" + keyword + "%'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        public bool delete(MemberBLL bll)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrn);

            try
            {
                string sql = "DELETE FROM MemberInfo WHERE MID=@MID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MID", bll.ID);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion
        #region DeleteTime
        public DataTable select_DeleteTime()
        {

            SqlConnection conn = new SqlConnection(myconnstrn);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT  MID, Fname, Lname, TimeValid, TimeLeft from MemberInfo";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;

        }

        public bool upadte_DleteTime(MemberBLL bll)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrn);

            try
            {
                string sql = "UPDATE MemberInfo SET TimeValid=@TimeValid, TimeLeft=@TimeLeft  " +
                "WHERE MID=@MId";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MID", bll.ID);
                cmd.Parameters.AddWithValue("@TimeValid", bll.TimeValid);
                cmd.Parameters.AddWithValue("@TimeLeft", bll.TimeLeft);

                conn.Open();
                //create and integer variable to hol value afte query is excecuted
                int rows = cmd.ExecuteNonQuery();

                //the value of rows will be greater than 0 if queery excecuted succesfully
                // else it will be 0
                if (rows > 0)
                {
                    //Query excecuted sufesfullly
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return isSuccess;
        }
        #endregion
    }
}
