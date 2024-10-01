using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medical_Store_Management.Forms
{
	public partial class index : System.Web.UI.Page
	{
		string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
		SqlConnection con;
		SqlCommand cmd;
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnSubmit_click(object sender, EventArgs e)
        {
            string email = emailTB.Text.Trim();
            string password = passwordTB.Text.Trim();
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT password FROM Staff WHERE Email = @Email";
            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
				System.Diagnostics.Debug.WriteLine($"Object Result: {result} and entered password is: {password}");

				if (result == null)
                {
                    Response.Write("<script>alert('No Staff found os this email id');</script>");
                }
                else
                {

					var passDB = result.ToString().Trim();
					if (password.Trim() == passDB)
					{
						Session["Email"] = email;
						Response.Redirect("AddOrder.aspx");
					}
					else
					{
						Response.Write("<script>alert('Passwords Do not Match');</script>");
					}
				}
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
	}
}