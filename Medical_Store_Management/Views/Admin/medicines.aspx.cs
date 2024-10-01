using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace Medical_Store_Management.Views.Admin
{
	public partial class medicines : System.Web.UI.Page
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
			using (con = new SqlConnection(conStr))
			{
				using (cmd = new SqlCommand("SELECT * FROM Items", con))
				{
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					GridView1.DataSource = dt;
					GridView1.DataBind();
				}
			}
		}

		protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "EditRow")
			{
				panelEdit.Visible = true;
				string id = e.CommandArgument.ToString();
				LoadItemData(id);
			}
		}

		private void LoadItemData(string id)
		{
			using (con = new SqlConnection(conStr))
			{
				using (cmd = new SqlCommand("SELECT * FROM Items WHERE ID = @Id", con))
				{
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
				}
			}
		}

		protected void OnItemUpdated(object sender, EventArgs e)
		{
			using (con = new SqlConnection(conStr))
			{
				using (cmd = new SqlCommand("UPDATE Items SET Item=@Item, Amount=@Amount, Quantity=@Quantity, Description=@Description WHERE ID = @Id", con))
				{
					con.Open();
					int id = Int32.Parse(itemId.Value);
					cmd.Parameters.AddWithValue("@Item", tbItem.Text);
					cmd.Parameters.AddWithValue("@Amount", Int32.Parse(tbAmount.Text));
					cmd.Parameters.AddWithValue("@Quantity", Int32.Parse(tbQuantity.Text));
					cmd.Parameters.AddWithValue("@Description", tbDescription.Text);
					cmd.Parameters.AddWithValue("@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
			BindGrid();
			panelEdit.Visible = false;
		}

		protected void OnItemDeleted(object sender, EventArgs e)
		{
			using (con = new SqlConnection(conStr))
			{
				using (cmd = new SqlCommand("DELETE FROM Items WHERE ID = @Id", con))
				{
					con.Open();
					int id = Int32.Parse(itemId.Value);
					cmd.Parameters.AddWithValue("@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
			panelEdit.Visible = false;
			BindGrid();
		}

		protected void OnSearch(object sender, EventArgs e)
		{
			string searchTerm = tbSearch.Text.Trim();
			using (con = new SqlConnection(conStr))
			{
				using (cmd = new SqlCommand("SELECT * FROM Items WHERE Item LIKE @SearchTerm OR Description LIKE @SearchTerm", con))
				{
					cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					GridView1.DataSource = dt;
					GridView1.DataBind();
				}
			}
		}

		protected void OnCancel(object sender, EventArgs e)
		{
			panelEdit.Visible = false;
			ClearForm();
		}

		private void ClearForm()
		{
			itemId.Value = string.Empty;
			tbItem.Text = string.Empty;
			tbAmount.Text = string.Empty;
			tbQuantity.Text = string.Empty;
			tbDescription.Text = string.Empty;
		}
	}
}