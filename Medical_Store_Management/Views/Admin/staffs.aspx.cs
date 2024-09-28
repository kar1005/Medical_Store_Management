using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medical_Store_Management.Views.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand("SELECT * FROM Staff", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                panelEdit.Visible = true;  // Ensure this line is reached
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string id = e.CommandArgument.ToString();
                LoadItemData(id);
            }
        }
        private void LoadItemData(string id)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand("SELECT * FROM Staff WHERE ID = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                itemId.Value = id;
                tbName.Text = rdr["Name"].ToString();
                tbEmail.Text = rdr["Email"].ToString();
                tbPassword.Text = rdr["Password"].ToString();
            }
            rdr.Close();
            con.Close();
        }
        protected void OnStaffUpdated(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Staff SET Name=@Name,Email= @Email,Password=@Password WHERE ID = @Id";
            using (con)
            {
                con.Open();
                using (cmd)
                {
                    int id = Int32.Parse(itemId.Value);
                    string name = tbName.Text.ToString();
                    string email = tbEmail.Text;
                    string password = tbPassword.Text.ToString();
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
            Page_Load(sender, e);
        }
        protected void OnStaffDeleted(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM Staff WHERE ID = @Id";
            using (con)
            {
                con.Open();
                using (cmd)
                {
                    int id = Int32.Parse(itemId.Value);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                panelEdit.Visible = false;
                BindGrid();
                Page_Load(sender, e);
            }
        }
    }
}