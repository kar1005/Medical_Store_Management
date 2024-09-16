using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medical_Store_Management.Forms
{
	public partial class index : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}


		protected void btnLogin_Click(object sender, EventArgs e)
		{
			// Replace with your user authentication logic
			if (ValidateUser(txtUsername.Text, txtPassword.Text))
			{
				// Create a session variable to indicate successful login
				Session["IsLoggedIn"] = true;

				// Redirect to the home page or dashboard
				Response.Redirect("/Forms/Admin/adminHome.aspx");
			}
			else
			{
				lblErrorMessage.Text = "Invalid username or password.";
			}
		}

		private bool ValidateUser(string username, string password)
		{
			// Simulate user validation (replace with database check)
			return username == "admin" && password == "password123";
		}

	}
}