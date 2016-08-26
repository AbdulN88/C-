using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

namespace IGS___Employee_Management_Portal
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        public Double telephoneNo;
        public Double mobileNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            string EmpID = Request.QueryString["EmpID"];
            string CompName = Request.QueryString["CompName"];
            string Dept = Request.QueryString["Dept"];
            string title = Request.QueryString["title"];
            string FName = Request.QueryString["FName"];
            string SName = Request.QueryString["SName"];
            string Gender = Request.QueryString["Gender"];
            string EmailAdd = Request.QueryString["EmailAdd"];
            string Password = Request.QueryString["Password"];
            string TelNo = Request.QueryString["TelNo"];
            string Mobile = Request.QueryString["Mobile"];

            EmpID += empID;
            CompName += compname;
            Dept += dept;
            title += Title;
            FName += fname;
            SName += sname;
            Gender += gender;
            EmailAdd += emailadd;
            Password += password;
            TelNo += telno;
            Mobile += mobno;

            empID.Disabled = true;

        }

        protected void formSubmit(object sender, EventArgs e)
        {
            conmessage.Visible = true;
        }

        public void addEmployee_click(object sender, EventArgs e)
        {

            if (passwordConfirmation(password.ToString(), passcon.ToString()))
            {
                Response.Write("Password does not match!");
            }

            string ConnectString = "server=localhost;database=IGS;integrated security=SSPI";
            string QueryString = "select * from dbo.Employees";

            SqlConnection myConnection = new SqlConnection(ConnectString);
            SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, myConnection);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "dbo.Employees");
            SqlCommandBuilder cb = new SqlCommandBuilder(myCommand);
            DataRow drow = ds.Tables["dbo.Employees"].NewRow();
            drow["CompanyName"] = compname;
            drow["Department"] = dept;
            drow["Title"] = Title;
            drow["Forename"] = fname;
            drow["Surname"] = sname;
            drow["Gender"] = gender;
            drow["EmailAddress"] = emailadd;
            drow["Password"] = password;

            if (!string.IsNullOrEmpty(telno.ToString()))
            {
                Double.TryParse(telno.ToString(), out telephoneNo);
                if (telephoneNo > 0)
                {
                    drow["Contact"] = telno;
                }
            }

            if (!string.IsNullOrEmpty(mobno.ToString()))
            {
                Double.TryParse(mobno.ToString(), out mobileNo);
                if (mobileNo > 0)
                {
                    drow["Mobile"] = mobno;
                }
            }

            ds.Tables["dbo.Employees"].Rows.Add();
            myCommand.Update(ds, "dbo.Employees");

            formSubmit(sender, e);
        }

        public bool passwordConfirmation(string pass1, string pass2)
        {
            pass1 = password.ToString();
            pass2 = passcon.ToString();
            if (pass1.Equals(pass2))
            {
                return true;
            }
            else if (pass2 == null)
            {
                return blankpass.Visible = true;
            }
            else
            {
                return restrictions.Visible = true;
            }

        }
    }
}