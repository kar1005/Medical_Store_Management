using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Script.Services;
using System.Web.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Medical_Store_Management.Views.Admin
{
	public partial class sendMessage : System.Web.UI.Page
	{
		// Twilio credentials (replace with your own credentials)
		private string accountSid = ConfigurationManager.AppSettings["accountSid"];
		private string authToken = ConfigurationManager.AppSettings["authToken"];
		private const string twilioWhatsAppNumber = "whatsapp:+14155238886"; // Twilio sandbox WhatsApp number
		private static string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{
			// Page load logic if needed
		}

		protected void btnSend_Click(object sender, EventArgs e)
		{
			string phoneNumber = txtPhoneNumber.Text.Trim();
			string messageBody = txtMessage.Text.Trim();

			if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(messageBody))
			{
				lblError.Text = "Phone number and message cannot be empty.";
				return;
			}

			try
			{
				// Initialize the Twilio client
				TwilioClient.Init(accountSid, authToken);
				var sendNumber = $"91{phoneNumber}";
				// Send the WhatsApp message
				var message = MessageResource.Create(
					from: new PhoneNumber(twilioWhatsAppNumber),
					to: new PhoneNumber($"whatsapp:+{sendNumber}"),
					body: messageBody
				);
				System.Diagnostics.Debug.WriteLine(messageBody);

				// Display success message
				lblResult.Text = "Message sent successfully!";
				lblError.Text = string.Empty;
			}
			catch (Exception ex)
			{
				// Display error message
				lblError.Text = $"Error: {ex.Message}";
				lblResult.Text = string.Empty;
			}
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static List<CustomerSuggestion> GetCustomerSuggestions(string prefix)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
			var suggestions = new List<CustomerSuggestion>();

			using (var conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string query = @"SELECT TOP 10 Name, Phone_no
                                 FROM Customer
                                 WHERE Name LIKE @Prefix + '%' OR Phone_no LIKE @Prefix + '%'";

				using (var cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue("@Prefix", prefix);

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							suggestions.Add(new CustomerSuggestion
							{
								Name = reader["Name"].ToString(),
								Phone_no = reader["Phone_no"].ToString()
							});
						}
					}
				}
			}

			return suggestions;
		}
	}


}