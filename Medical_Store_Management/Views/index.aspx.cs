﻿using LinqToDB.Data;
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
            cmd.CommandText = "SELECT Password FROM Staff WHERE Email = @Email";
            cmd.Parameters.AddWithValue("@Email", email);
            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    Response.Write("<script>alert('No Staff found os this email id');</script>");
                }
                else
                {

                    var passDB = result.ToString();
                    if (password != passDB)
                    {
                        Response.Write("<script>alert('Passwords Doot Match');</script>");
                    }
                    else
                    {
                        Session["Email"] = email;
                        Response.Redirect("AddOrder.aspx");
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