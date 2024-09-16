using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medical_Store_Management.Forms
{
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlCommandBuilder builder;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Staff";
            adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            builder = new SqlCommandBuilder(adapter);
            try
            {
                con.Open();
                adapter.Fill(ds, "Staff");
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            DataTable dt = ds.Tables["Staff"];
            DataRow dr = dt.NewRow();
            dr["Name"] = tbName.Text;
            dr["Email"] = tbEmail.Text;
            dr["Password"] = tbPassword.Text;
            dt.Rows.Add(dr);
            adapter.Update(ds, "Staff");
        }
    }
}