using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;


namespace Medical_Store_Management.Views.Admin
{
	public partial class GenerateBill : System.Web.UI.Page
	{
		string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
		SqlConnection con;
		SqlCommand cmd;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				con = new SqlConnection(conStr);
				cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandText = "Select Id,Item,Amount from Items";
				try
				{
					con.Open();
					ddlMedcineName.Items.Clear();
					ddlMedcineName.Items.Add(new ListItem("--Select--", "0"));
					SqlDataReader rdr  = cmd.ExecuteReader();
					while (rdr.Read())
					{
						string value = rdr["Id"].ToString();
						string text = rdr["Item"].ToString();

						ddlMedcineName.Items.Add(new ListItem(text, value));
					}
					con.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
			string medicineJson = hfMedicineList.Value;
			List<string> medicines = JsonConvert.DeserializeObject<List<string>>(medicineJson);

			string contactNumber = tbcontactNumber.Text;	

            //checking if the customer exists
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
			cmd.CommandText = "SELECT COUNT(*) FROM Customer WHERE Phone_no = @ContactNumber";
			cmd.Connection = con;
			string customerName = null;
			if (panelName.Visible)
			{
				customerName = tbCustomerName.Text;
				InsertCustomer(contactNumber, customerName);
			}
            try
            {
				using (con)
				{
					con.Open();
					using (cmd)
					{
						cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
						int count = (int)cmd.ExecuteScalar();
						if (count == 0)
						{
							panelName.Visible = true;
						}
						else
						{

						}
					}
				}
			}catch(Exception ex)
            {
				Response.Write(ex.Message);
            }
		}
		protected void ddlMedicne_IndexChanged(object sender, EventArgs e)
        {
			int id = Int32.Parse(ddlMedcineName.SelectedItem.Value);
			con = new SqlConnection(conStr);
			cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = "SELECT Amount FROM Items WHERE Id=@Id";
            using (con)
            {
				con.Open();
                using (cmd)
                {
					cmd.Parameters.AddWithValue("@Id", id);
					var amount = cmd.ExecuteScalar();
					medicinePrice.Value = amount.ToString();

				}
            }
		}
		private void InsertCustomer(string contactNumber, string customerName)
		{
			con = new SqlConnection(conStr);
			cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = "INSERT INTO Customer(Name,Phone_no) VALUES(@Name,@Contact)";
			using (con)
			{
				con.Open();
				using (cmd)
				{
					cmd.Parameters.AddWithValue("@Name", customerName);
					cmd.Parameters.AddWithValue("@Contact", contactNumber);
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}