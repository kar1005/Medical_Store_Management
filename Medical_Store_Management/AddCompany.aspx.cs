using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Medical_Store_Management
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

        protected void btnAddCompany_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Company";
            adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            builder = new SqlCommandBuilder(adapter);
            try
            {
                con.Open();
                adapter.Fill(ds, "Company");
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            DataTable dt = ds.Tables["Company"];
            DataRow dr = dt.NewRow();
            dr["Name"] = tbCompanyName.Text;
            dt.Rows.Add(dr);
            adapter.Update(ds, "Company");
        }
    }
}