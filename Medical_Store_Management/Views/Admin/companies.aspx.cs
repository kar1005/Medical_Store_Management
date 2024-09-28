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
    public partial class companies : System.Web.UI.Page
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
            cmd = new SqlCommand("SELECT * FROM Company", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridViewCompany.DataSource = dt;
            GridViewCompany.DataBind();
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
            cmd = new SqlCommand("SELECT * FROM Company WHERE ID = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                itemId.Value = id;
                tbName.Text = rdr["Name"].ToString();
            }
            rdr.Close();
            con.Close();
        }
        protected void OnStaffUpdated(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Company SET Name=@Name WHERE ID = @Id";
            using (con)
            {
                con.Open();
                using (cmd)
                {
                    int id = Int32.Parse(itemId.Value);
                    string name = tbName.Text.ToString();
                    cmd.Parameters.AddWithValue("@Name", name);
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
            cmd.CommandText = "DELETE FROM Company WHERE ID = @Id";
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