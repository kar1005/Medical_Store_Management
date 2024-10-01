using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace Medical_Store_Management.Views.Admin
{
	public partial class ViewBills : System.Web.UI.Page
	{
		string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string selectedDate = Request.QueryString["date"];
				if (!string.IsNullOrEmpty(selectedDate))
				{
					LoadBills(selectedDate);
				}
			}
		}

		private void LoadBills(string selectedDate)
		{
			string query = @"
                SELECT 
                    o.Id AS OrderId, 
                    c.Name AS CustomerName, 
                    o.TotalBill,
                    o.PaymentMode,
                    oi.Item, 
                    oi.Quantity, 
                    i.Amount AS UnitPrice, 
                    (oi.Quantity * i.Amount) AS TotalPrice 
                FROM [Orders] o
                JOIN Customer c ON o.Customer = c.Id
                JOIN [Order_Items] oi ON o.Id = oi.[Order]
                JOIN Items i ON oi.Item = i.Id
                WHERE CONVERT(DATE, o.Date) = @SelectedDate
                ORDER BY o.Id, oi.Id";

			using (SqlConnection con = new SqlConnection(conStr))
			{
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);
					try
					{
						con.Open();
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						DataTable dt = new DataTable();
						da.Fill(dt);

						rptBills.DataSource = dt;
						rptBills.DataBind();
					}
					catch (Exception ex)
					{
						// Handle the exception (e.g., display an error message)
						lblError.Text = "An error occurred while loading the bills: " + ex.Message;
						lblError.Visible = true;
					}
				}
			}
		}

		protected void rptBills_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Repeater rptMedicines = (Repeater)e.Item.FindControl("rptMedicines");
				DataRowView drv = (DataRowView)e.Item.DataItem;
				string orderId = drv["OrderId"].ToString();

				DataView dv = new DataView((DataTable)rptBills.DataSource);
				dv.RowFilter = "OrderId = " + orderId;

				rptMedicines.DataSource = dv;
				rptMedicines.DataBind();
			}
		}
	}
}