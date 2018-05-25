using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace students_log
{
    public partial class department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            SqlConnection con= new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");
            con.Open();
            string sql = "insert into tbl_department(dept_name)values('"+txt_dept.Text+"')";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            txt_dept.Text = "";
            Response.Write("<script>alert('Successfully Inserted')</script>"); 
        }
    }
}