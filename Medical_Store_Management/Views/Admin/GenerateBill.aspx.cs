using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medical_Store_Management.Views.Pharmacist;
using Newtonsoft.Json;


using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace Medical_Store_Management.Views.Admin

{
	public partial class GenerateBill : System.Web.UI.Page
	{
		private string accountSid = ConfigurationManager.AppSettings["accountSid"];
		private string authToken = ConfigurationManager.AppSettings["authToken"];
		private const string twilioWhatsAppNumber = "whatsapp:+14155238886"; // Twilio sandbox WhatsApp number
		private static string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
		private Decimal totalBill;
		//string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//LoadMedicines();
			}
		}


		[System.Web.Services.WebMethod]


		public static List<MedicineSuggestion> GetMedicineSuggestions(string prefix)
		{
			List<MedicineSuggestion> suggestions = new List<MedicineSuggestion>();
			using (SqlConnection con = new SqlConnection(conStr))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT Id, Item, Amount, Quantity FROM Items WHERE Item LIKE @Prefix + '%'", con))
				{
					cmd.Parameters.AddWithValue("@Prefix", prefix);
					con.Open();
					using (SqlDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							suggestions.Add(new MedicineSuggestion
							{
								Id = Convert.ToInt32(rdr["Id"]),
								Name = rdr["Item"].ToString(),
								Price = Convert.ToDecimal(rdr["Amount"]),
								Quantity = Convert.ToInt32(rdr["Quantity"])
							});
						}
					}
				}
			}
			return suggestions;
		}

		[WebMethod]
		public static List<CustomerSuggestion> GetCustomerSuggestions(string prefix)
		{
			List<CustomerSuggestion> suggestions = new List<CustomerSuggestion>();
			using (SqlConnection con = new SqlConnection(conStr))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, Phone_no FROM Customer WHERE Phone_no LIKE @Prefix + '%'", con))
				{
					cmd.Parameters.AddWithValue("@Prefix", prefix);
					con.Open();
					using (SqlDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							suggestions.Add(new CustomerSuggestion
							{
								Id = Convert.ToInt32(rdr["Id"]),
								Name = rdr["Name"].ToString(),
								Phone_no = rdr["Phone_no"].ToString()
							});
						}
					}
				}
			}
			return suggestions;
		}




		public static string GetMedicineDetails(string medicineId)
		{

			string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
			using (SqlConnection con = new SqlConnection(conStr))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT Amount, Quantity FROM Items WHERE Id = @Id", con))
				{
					cmd.Parameters.AddWithValue("@Id", medicineId);
					con.Open();
					using (SqlDataReader rdr = cmd.ExecuteReader())
					{
						if (rdr.Read())
						{
							return JsonConvert.SerializeObject(new
							{
								Price = rdr["Amount"].ToString(),
								Quantity = rdr["Quantity"].ToString()
							});
						}
					}
				}
			}
			return "{}";
		}

		protected void PlaceOrder_Click(object sender, EventArgs e)
		{
			string medicineJson = hfMedicineList.Value;
			List<Medicine> medicines = JsonConvert.DeserializeObject<List<Medicine>>(medicineJson);
			string contactNumber = tbContactNumber.Text.Trim();
			string customerName = tbCustomerName.Text.Trim();

			// Validate inputs before processing the order
			if (medicines == null || medicines.Count == 0)
			{
				ShowErrorMessage("No medicines selected for the order.");
				return;
			}

			if (string.IsNullOrEmpty(contactNumber))
			{
				ShowErrorMessage("Contact number is required.");
				return;
			}

			SqlConnection con = new SqlConnection(conStr);
			using (con)
			{
				try
				{
					con.Open();
					LogMessage("Database connection opened successfully.");

					using (SqlTransaction transaction = con.BeginTransaction())
					{
						try
						{
							// Check if customer exists
							int customerId = GetOrCreateCustomer(contactNumber, transaction, con);
							LogMessage($"Customer ID: {customerId}");

							// Create order
							int orderId = CreateOrder(customerId, transaction, con);
							LogMessage($"Order ID: {orderId}");

							var sendContact = $"91{contactNumber}";
							sendBill(medicines, sendContact, customerName);
							// Add order details and update stock
							foreach (var medicine in medicines)
							{
								AddOrderDetail(orderId, medicine, transaction, con);
								UpdateStock(medicine.Id, medicine.Quantity, transaction, con);
								LogMessage($"Processed medicine: {medicine.Id}, Quantity: {medicine.Quantity}");
							}
							
							transaction.Commit();
							LogMessage("Transaction committed successfully.");
							ClearForm();
							ShowSuccessMessage("Order placed successfully!");
						}
						catch (Exception ex)
						{
							transaction.Rollback();
							LogMessage($"Transaction rolled back. Error: {ex.Message}");
							throw; // Re-throw the exception to be caught by the outer catch block
						}
					}
				}
				catch (SqlException sqlEx)
				{
					LogMessage($"SQL Exception: {sqlEx.Message}\nStack Trace: {sqlEx.StackTrace}");
					ShowErrorMessage("Database error: " + sqlEx.Message);
				}
				catch (Exception ex)
				{
					LogMessage($"General Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
					ShowErrorMessage("Error placing order: " + ex.Message);
				}
				finally
				{
					if (con.State == ConnectionState.Open)
					{
						con.Close();
						LogMessage("Database connection closed.");
					}
				}
			}
		}

		private void sendBill(List<Medicine> medicines, string phoneNumber, string customerName)
		{
			if (string.IsNullOrEmpty(phoneNumber) || medicines == null || medicines.Count == 0)
			{
				LogMessage("Invalid phone number or empty medicine list.");
				return;
			}

			try
			{
				// Initialize the Twilio client
				TwilioClient.Init(accountSid, authToken);

				// Prepare the message body
				StringBuilder messageBody = new StringBuilder();
				messageBody.AppendLine($"Thank You for Shopping with us {customerName}!");
				messageBody.AppendLine();
				messageBody.AppendLine($"{"Item",-30} {"Quantity",8} {"Price",12}");
				messageBody.AppendLine(new string('-', 52));

				totalBill = 0;

				foreach (var medicine in medicines)
				{
					decimal itemTotal = medicine.Price * medicine.Quantity;
					messageBody.AppendLine($"{medicine.Name,-30} {medicine.Quantity,8} {medicine.Price,12:C}");
					totalBill += itemTotal;
				}

				messageBody.AppendLine(new string('-', 52));
				messageBody.AppendLine($"{"Total",-30} {"",-8} {totalBill,12:C}");

				// Send the WhatsApp message
				var message = MessageResource.Create(
					from: new PhoneNumber(twilioWhatsAppNumber),
					to: new PhoneNumber($"whatsapp:+{phoneNumber}"),
					body: messageBody.ToString()
				);

				LogMessage($"WhatsApp message sent successfully to {phoneNumber}");
			}
			catch (Exception ex)
			{
				LogMessage($"Error sending WhatsApp message: {ex.Message}");
			}
		}


		private void LogMessage(string message)
		{
			// Implement logging here. This could write to a file, database, or use a logging framework.
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now}: {message}");
		}

		private int GetOrCreateCustomer(string contactNumber, SqlTransaction transaction, SqlConnection con)
		{
			using (SqlCommand cmd = new SqlCommand("SELECT Id FROM Customer WHERE Phone_no = @ContactNumber", con, transaction))
			{
				cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
				object result = cmd.ExecuteScalar();
				if (result != null)
				{
					return (int)result;
				}
			}

			// Customer doesn't exist, create new customer
			string customerName = tbCustomerName.Text.Trim();
			if (string.IsNullOrEmpty(customerName))
			{
				ShowErrorMessage("Customer name is required for new customers.");
				throw new InvalidOperationException("Customer name is required for new customers.");
			}

			using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (Name, Phone_no) VALUES (@Name, @Contact); SELECT SCOPE_IDENTITY();", con, transaction))
			{
				cmd.Parameters.AddWithValue("@Name", customerName);
				cmd.Parameters.AddWithValue("@Contact", contactNumber);
				return Convert.ToInt32(cmd.ExecuteScalar());
			}
		}

		private int CreateOrder(int customerId, SqlTransaction transaction, SqlConnection con)
		{
			decimal totalAmount;
			LogMessage(hiddenTotalAmount.Value); // Now using hidden field value
			if (!decimal.TryParse(hiddenTotalAmount.Value.Replace("₹", "").Trim(), out totalAmount))
			{
				ShowErrorMessage("Invalid total amount.");
				throw new InvalidOperationException("Invalid total amount.");
			}

			using (SqlCommand cmd = new SqlCommand("INSERT INTO Orders (Customer, Date, TotalBill, PaymentMode) VALUES (@CustomerId, @OrderDate, @TotalAmount, 'Cash'); SELECT SCOPE_IDENTITY();", con, transaction))
			{
				cmd.Parameters.AddWithValue("@CustomerId", customerId);
				cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
				cmd.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(totalAmount));
				return Convert.ToInt32(cmd.ExecuteScalar());
			}
		}

		private void AddOrderDetail(int orderId, Medicine medicine, SqlTransaction transaction, SqlConnection con)
		{
			using (SqlCommand cmd = new SqlCommand("INSERT INTO Order_Items ([Order], Item, Quantity) VALUES (@OrderId, @MedicineId, @Quantity)", con, transaction))
			{
				cmd.Parameters.AddWithValue("@OrderId", orderId);
				cmd.Parameters.AddWithValue("@MedicineId", medicine.Id);
				cmd.Parameters.AddWithValue("@Quantity", medicine.Quantity);
				cmd.ExecuteNonQuery();
			}

		}

		// Method to update the stock of the ordered medicine
		private void UpdateStock(int medicineId, int quantity, SqlTransaction transaction, SqlConnection con)
		{
			using (SqlCommand cmd = new SqlCommand("UPDATE Items SET Quantity = Quantity - @Quantity WHERE Id = @MedicineId", con, transaction))
			{
				cmd.Parameters.AddWithValue("@Quantity", quantity);
				cmd.Parameters.AddWithValue("@MedicineId", medicineId);
				int rowsAffected = cmd.ExecuteNonQuery();

				if (rowsAffected == 0)
				{
					ShowErrorMessage($"No medicine found with ID {medicineId}. Stock not updated.");
					throw new InvalidOperationException($"No medicine found with ID {medicineId}. Stock not updated.");
				}
			}
		}

		private void ClearForm()
		{
			//ddlMedicineName.SelectedIndex = 0;
			tbQuantity.Text = string.Empty;
			tbContactNumber.Text = string.Empty;
			tbCustomerName.Text = string.Empty;
			lblTotalAmount.Text = "₹0"; // Ensure the currency symbol is consistent
			hfMedicineList.Value = "[]";
			ClientScript.RegisterStartupScript(this.GetType(), "ClearMedicineList", "clearMedicineList();", true);
		}

		private void ShowSuccessMessage(string message)
		{
			ClientScript.RegisterStartupScript(this.GetType(), "ShowSuccess", $"showSuccessMessage('{message}');", true);
		}

		private void ShowErrorMessage(string message)
		{
			ClientScript.RegisterStartupScript(this.GetType(), "ShowError", $"showErrorMessage('{message}');", true);
		}
	}

	public class Medicine
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}

	public class MedicineSuggestion
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}

	public class CustomerSuggestion
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Phone_no { get; set; }
	}
}
