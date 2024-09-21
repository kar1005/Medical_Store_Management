using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medical_Store_Management.Views.Admin
{
	public partial class categories : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void saveBtn_Click(object sender, EventArgs e)
		{
			String txt = F_name.Text;
			Response.Write(txt);
		}

		protected void editBtn_Click(object sender, EventArgs e)
		{

		}

		protected void deleteBtn_Click(object sender, EventArgs e)
		{

		}
	}
}