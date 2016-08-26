using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Windows.Markup;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace IGS___Employee_Management_Portal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public Double telephoneNo;
        public Double mobileNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            getCompany(sender, e);
            getTitle(sender, e);
            getGender(sender, e);
            restrictions.Visible = false;
            blankpass.Visible = false;
            conmessage.Visible = false;

            formsubmit.ServerClick += new EventHandler(addEmployee_click);
        }

        public void getCompany(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Get SQL Server address and database
                string ConnectString = "server=localhost;database=IGS;integrated security=SSPI";

                //Define a query for company table to retreive columns
                string QueryString = "select CompanyName from dbo.Companies";

                //Define sql connection method and data source
                SqlConnection myConnection = new SqlConnection(ConnectString);
                SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, myConnection);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "dbo.Companies");

                //Assign company name columns to dropdown list             
                CompList.DataSource = ds;
                CompList.DataTextField = "CompanyName";
                CompList.DataValueField = "CompanyName";
                CompList.DataBind();
            }
        }

        public void getTitle(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Get SQL Server address and database
                string ConnectString = "server=localhost;database=IGS;integrated security=SSPI";

                //Define a query for title table to retreive columns
                string QueryString = "select Title from dbo.Title";

                //Define sql connection method and data source
                SqlConnection myConnection = new SqlConnection(ConnectString);
                SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, myConnection);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "dbo.Title");

                //Assign number of titles to a drop down list             
                TitleList.DataSource = ds;
                TitleList.DataTextField = "Title";
                TitleList.DataValueField = "Title";
                TitleList.DataBind();
            }
        }

        public void getGender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Get SQL Server address and database
                string ConnectString = "server=localhost;database=IGS;integrated security=SSPI";

                //Define a query for gender table to retreive columns
                string QueryString = "select Gender from dbo.Gender";

                //Define sql connection method and data source
                SqlConnection myConnection = new SqlConnection(ConnectString);
                SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, myConnection);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "dbo.Gender");

                //Assign number of genders to dropdown list             
                GenderList.DataSource = ds;
                GenderList.DataTextField = "Gender";
                GenderList.DataValueField = "Gender";
                GenderList.DataBind();
            }
        }

        //protected void formSubmit(object sender, EventArgs e)
        //{
        //    conmessage.Visible = true;
        //}

        public void addEmployee_click(object sender, EventArgs e)
        {
            
            //if (passwordConfirmation(password.ToString(), passcon.ToString()))
            //{
            //    Response.Write("Password does not match!");
            //}

            string ConnectString = "server=localhost;database=IGS;integrated security=SSPI";
            string QueryString = "select * from dbo.Employees";

            SqlConnection myConnection = new SqlConnection(ConnectString);
            SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, myConnection);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "dbo.Employees");
            SqlCommandBuilder cb = new SqlCommandBuilder(myCommand);
            DataRow drow = ds.Tables["dbo.Employees"].NewRow();
            drow["CompanyName"] = CompList;
            drow["Department"] = dept;
            drow["Title"] = TitleList;
            drow["Forename"] = fname;
            drow["Surname"] = sname;
            drow["Gender"] = GenderList;
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

            //formSubmit(sender, e);
        }

        //public bool passwordConfirmation(string pass1, string pass2)
        //{
        //    pass1 = password.ToString();
        //    pass2 = passcon.ToString();
        //    if (pass1.Equals(pass2))
        //    {
        //        return true;
        //    }
        //    else if(pass2 == null)
        //    {
        //       return blankpass.Visible = true;
        //    }
        //    else
        //    {
        //        return restrictions.Visible = true;
        //    }
            
        //}
    }
}