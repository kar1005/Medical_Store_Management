using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace Medical_Store_Management.Views.Admin
{
	public partial class BillHistory : System.Web.UI.Page
	{
		string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadBillHistory();
			}
		}

		private void LoadBillHistory()
		{
			string query = "SELECT CONVERT(DATE, o.Date) AS BillDate, SUM(o.TotalBill) AS TotalEarnings FROM Orders o GROUP BY CONVERT(DATE, o.Date) ORDER BY BillDate DESC;";

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

						gvBillHistory.DataSource = dt;
						gvBillHistory.DataBind();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
			}
		}

		protected void btnViewDetails_Click(object sender, EventArgs e)
		{
			string selectedDate = (sender as Button).CommandArgument;
			Response.Redirect("ViewBills.aspx?date=" + selectedDate);
		}
	}
}
