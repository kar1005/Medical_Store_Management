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
    public partial class AddItem : System.Web.UI.Page
    {
        string conStr = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlCommandBuilder builder;
        DataSet ds;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                con = new SqlConnection(conStr);
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select Id,Name from Company";
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                builder = new SqlCommandBuilder(adapter);
                dt = new DataTable();
                try
                {
                    con.Open();
                    adapter.Fill(dt);
                    ddlCompany.Items.Clear();
                    ddlCompany.Items.Add(new ListItem("--Select--", "0"));
                    foreach (DataRow dr in dt.Rows)
                    {
                        string value = dr["Id"].ToString();
                        string text = dr["Name"].ToString();

                        ddlCompany.Items.Add(new ListItem(text, value));
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Items";
            adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            builder = new SqlCommandBuilder(adapter);
            try
            {
                con.Open();
                adapter.Fill(ds, "Items");
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            DataTable dt = ds.Tables["Items"];
            DataRow dr = dt.NewRow();
            dr["Item"] = tbItem.Text;
            dr["Company"] = ddlCompany.SelectedItem.Value;
            dr["Amount"] = tbAmount.Text;
            dr["Quantity"] = tbQuantity.Text;
            dr["Description"] = tbDescription.Text;
            dt.Rows.Add(dr);
            adapter.Update(ds, "Items");
        }
    }
}