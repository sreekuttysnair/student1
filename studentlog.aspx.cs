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
    public partial class studentlog : System.Web.UI.Page
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

        protected void ddl_batch_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");
            con.Open();
            string sql = "insert into tbl_student(stud_name,admissn_no,batch_id,batch_name,cors_id,cors_name,dept_id,dept_name)values('" + txt_stud.Text + "','"+txt_adm.Text+"','"+ddl_batch.SelectedItem.Value+"','"+ddl_batch.SelectedItem.Text+"','" + ddl_cors.SelectedItem.Value + "','"+ddl_cors.SelectedItem.Text+"','" + ddl_dept.SelectedItem.Value + "','"+ddl_dept.SelectedItem.Text+"')";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            ddl_cors.ClearSelection();
            ddl_batch.ClearSelection();
            ddl_dept.ClearSelection();
            txt_adm.Text = "";
            txt_stud.Text = "";
            ShowData();
            Response.Write("<script>alert('Successfully Inserted')</script>"); 
        }
        protected void ShowData()
        {

            SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");
            //string a = "select tbl_department.dept_name,tbl_department.dept_id,tbl_course.course_name from tbl_department inner join tbl_course on tbl_department.dept_id=tbl_course.dept_id";
            string a = "select admissn_no,stud_name,cors_name,batch_name from tbl_student where dept_id='" + ddl_dept.SelectedValue + "' and cors_id='"+ddl_cors.SelectedValue+"' and batch_id='"+ddl_batch.SelectedValue+"'   ";
            //and active=1";

            /*  <asp:TemplateField HeaderText="deptName">  
                          <ItemTemplate>  
                              <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("dept_name") %>'></asp:Label>  
                          </ItemTemplate>  
                              </asp:TemplateField>      con.Open();
            
                  string a = "   select tbl_department.dept_name,tbl_course.dept_id,tbl_course.course_name from tbl_department inner join  tbl_course on  tbl_department.dept_id=tbl_course.dept_id  ";
                 */
            SqlDataAdapter adptss = new SqlDataAdapter(a, con);
            DataTable dtss = new DataTable();
            adptss.Fill(dtss);
            GridView1.DataSource = dtss;
            GridView1.DataBind();
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    ShowData();

        //}

        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            GridView1.EditIndex = e.NewEditIndex;
            ShowData();
        }
        protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");
            con.Open();
            //Finding the controls from Gridview for the row which is going to update  
            //********** Label dept_name = GridView1.Rows[e.RowIndex].FindControl("lbl_Name") as Label;
            //   Label dept_name = GridView1.Rows[e.RowIndex].FindControl("lbl_Names") as Label;
            Label admissn_no = GridView1.Rows[e.RowIndex].FindControl("lbl_admissn_no") as Label;

            TextBox stud_name = GridView1.Rows[e.RowIndex].FindControl("txt_studName") as TextBox;
            TextBox cors_name = GridView1.Rows[e.RowIndex].FindControl("txt_cors") as TextBox;
           // TextBox batch_name = GridView1.Rows[e.RowIndex].FindControl("txt_batch") as TextBox;
            // TextBox city = GridView1.Rows[e.RowIndex].FindControl("txt_City") tas TextBox;
            string ds = Convert.ToString(stud_name.Text);
            //updating the record  
            SqlCommand cmd = new SqlCommand("Update tbl_student set stud_name = '" + stud_name.Text + "',cors_name='" + cors_name.Text + "' where admissn_no =" + Convert.ToInt32(admissn_no.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }
        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            ShowData();
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");

            con.Open();
            //Finding the controls from Gridview for the row which is going to update  
            //********** Label dept_name = GridView1.Rows[e.RowIndex].FindControl("lbl_Name") as Label;
            //   Label dept_name = GridView1.Rows[e.RowIndex].FindControl("lbl_Names") as Label;
            Label admissn_no = GridView1.Rows[e.RowIndex].FindControl("lbl_admissn_no") as Label;
            TextBox stud_name = GridView1.Rows[e.RowIndex].FindControl("txt_studName") as TextBox;
            TextBox cors_name = GridView1.Rows[e.RowIndex].FindControl("txt_cors") as TextBox;
            TextBox batch_name = GridView1.Rows[e.RowIndex].FindControl("txt_batch") as TextBox;
            // TextBox city = GridView1.Rows[e.RowIndex].FindControl("txt_City") tas TextBox;
            string ds = Convert.ToString(admissn_no.Text);
            //updating the record  
            SqlCommand cmd = new SqlCommand("delete from tbl_student where admissn_no =" + Convert.ToInt32(admissn_no.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            // GridView1.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }
        protected void btn_view_Click(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddl_cors_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress1;Initial Catalog=studentslog;Integrated Security=True");
            con.Open();
            string com = "Select batch_id,batch_name from tbl_batch where dept_id='" + ddl_dept.SelectedItem.Value + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_batch.DataSource = dt;
            ddl_batch.DataTextField = "batch_name";
            ddl_batch.DataValueField = "batch_id";
            ddl_batch.DataBind();
            ddl_batch.Items.Insert(0, new ListItem("select", "0"));
            con.Close();
        }
    }
}