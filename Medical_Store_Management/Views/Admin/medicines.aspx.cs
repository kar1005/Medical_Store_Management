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
	public partial class medicines : System.Web.UI.Page
	{
        string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;// + ";Encrypt=False;";
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
            cmd = new SqlCommand("SELECT * FROM Items", con);
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
            cmd = new SqlCommand("SELECT * FROM Items WHERE ID = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                itemId.Value = id;
                tbItem.Text = rdr["Item"].ToString();
                tbAmount.Text = rdr["Amount"].ToString();
                tbQuantity.Text = rdr["Quantity"].ToString();
                tbDescription.Text = rdr["Description"].ToString();
            }
            rdr.Close();
            con.Close();
        }
        protected void OnItemUpdated(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Items SET Item=@Item,Amount=@Amount,Quantity=@Quantity,Description=@Description WHERE ID = @Id";
            using (con)
            {
                con.Open();
                using (cmd)
                {
                    int id = Int32.Parse(itemId.Value);
                    string item = tbItem.Text.ToString();
                    int amount = Int32.Parse(tbAmount.Text);
                    int quantity = Int32.Parse(tbQuantity.Text);
                    string description = tbDescription.Text.ToString();
                    cmd.Parameters.AddWithValue("@Item", item);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
            Page_Load(sender, e);
        }
        protected void OnItemDeleted(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM Items WHERE ID = @Id";
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


 
 
 