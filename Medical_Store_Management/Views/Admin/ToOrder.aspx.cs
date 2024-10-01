using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Medical_Store_Management.Views.Admin
{
	public partial class ToOrder : System.Web.UI.Page
	{
		string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadMedicinesToOrder();
			}
		}

		private void LoadMedicinesToOrder()
		{
			// SQL query to get medicines where quantity is less than 10
			string query = "SELECT i.item AS MedicineName, c.Name AS CompanyName, i.quantity AS Quantity, i.amount AS Amount FROM Items i JOIN Company c ON i.Company = c.Id WHERE i.quantity < 10";

			using (SqlConnection con = new SqlConnection(conStr))
			{
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					try
					{
						con.Open();
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						DataTable dt = new DataTable();
						da.Fill(dt);

						// Bind the result to the GridView
						gvMedicinesToOrder.DataSource = dt;
						gvMedicinesToOrder.DataBind();
					}
					catch (Exception ex)
					{
						// Handle the exception
						Console.WriteLine(ex.Message);
					}
				}
			}
		}
	}
}
