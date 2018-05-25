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
    public partial class batch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");
                con.Open();
                string com = "Select dept_id,dept_name from tbl_department";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddl_dept.DataSource = dt;
                ddl_dept.DataTextField = "dept_name";
                ddl_dept.DataValueField = "dept_id";
                ddl_dept.DataBind();
                ddl_dept.Items.Insert(0, new ListItem("select", "0"));
                con.Close();
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");
            con.Open();
            string sql = "insert into tbl_batch(batch_name,cors_id,dept_id)values('" + txt_batch.Text + "','"+ddl_cors.SelectedItem.Value+"','" + ddl_dept.SelectedItem.Value + "')";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            ddl_cors.ClearSelection();
            txt_batch.Text = "";
            ddl_dept.ClearSelection();
            Response.Write("<script>alert('Successfully Inserted')</script>"); 
        }

        protected void ddl_cors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");
            con.Open();
            string com = "Select cors_id,cors_name from tbl_course where dept_id='" + ddl_dept.SelectedItem.Value + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_cors.DataSource = dt;
            ddl_cors.DataTextField = "cors_name";
            ddl_cors.DataValueField = "cors_id";
            ddl_cors.DataBind();
            ddl_cors.Items.Insert(0, new ListItem("select", "0"));
            con.Close();
        }
    }
}