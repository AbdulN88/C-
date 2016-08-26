using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Text;
using System.Xml;
using HtmlAgilityPack;
namespace IGS___Employee_Management_Portal
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public string EmpID;
        public string CompName;
        public string Dept;
        public string title;
        public string FName;
        public string SName;
        public string Gender;
        public string EmailAdd;
        public string Password;
        public string TelNo;
        public string Mobile;
        public string TskNm;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!this.IsPostBack)
            {
                //Populating a DataTable from database.
                DataTable dt = this.GetData();

                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                html.Append("<table border = '1' id = 'table'>");

                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                    
                }
                    html.Append("<th>");
                    html.Append("Amend Details");
                    html.Append("</th>");
                    html.Append("</tr>");

                DataSet ds = new DataSet();
                //Building the Data rows.
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }                    
                    
                    //Append hotspot navigations to table
                        html.Append("<td>");
                        html.Append(empSel);
                        html.Append("</td>");
                }
                
                //Table end.
                html.Append("</table>");
                
                //Append the HTML string to Placeholder.
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });

                amendEmp.ServerClick += new EventHandler(amendEmp_Click);
                
            }
        }
        

        private DataTable GetData()
        {
            string ConnectString = "server=localhost;database=IGS;integrated security=SSPI";
            string QueryString = "select * from dbo.Employees";
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                using (SqlCommand cmd = new SqlCommand(QueryString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                            
                        }
                    }
                }
            }
        }

        public void amendEmp_Click(object sender, EventArgs e)
        {
            
            if (empSel.Checked)
            {
                HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                HtmlNode table = html.DocumentNode.SelectSingleNode("//table[@border='1']");
                DataTable edt = new DataTable();
                var rows = table.SelectNodes("tr");
                for (int i = 0; i < rows.Count; ++i)
                {
                    //if row = then these are headers
                    if (i == 0)
                    {
                        var cols = rows[i].SelectNodes("th");
                        edt.Columns.Add(new DataColumn(cols[0].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[1].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[2].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[3].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[4].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[5].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[6].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[7].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[8].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[9].InnerText.ToString()));
                        edt.Columns.Add(new DataColumn(cols[10].InnerText.ToString()));
                    }
                    //row>0 then data
                    else
                    {
                        var cols = rows[i].SelectNodes("td");

                        DataRow dr = edt.NewRow();
                        dr[0] = cols[0].InnerText.ToString();
                        dr[1] = cols[1].InnerText.ToString();
                        dr[2] = cols[2].InnerText.ToString();
                        dr[3] = cols[3].InnerText.ToString();
                        dr[4] = cols[4].InnerText.ToString();
                        dr[5] = cols[5].InnerText.ToString();
                        dr[6] = cols[6].InnerText.ToString();
                        dr[7] = cols[7].InnerText.ToString();
                        dr[8] = cols[8].InnerText.ToString();
                        dr[9] = cols[9].InnerText.ToString();
                        dr[10] = cols[10].InnerText.ToString();
                        edt.Rows.Add(dr);

                        EmpID += dr[0];
                        CompName += dr[1];
                        Dept += dr[2];
                        title += dr[3];
                        FName += dr[4];
                        SName += dr[5];
                        Gender += dr[6];
                        EmailAdd += dr[7];
                        Password += dr[8];
                        TelNo += dr[9];
                        Mobile += dr[10];
                    }

                }
                Server.Transfer("~/AmendEmployeeDetails.aspx?EmpID=EmpID&CompName=CompName&Dept=Dept&title=title&FName=FName&SName=SName&Gender=Gender&EmailAdd=EmailAdd&Password=Password&TelNo=TelNo&Mobile=Mobile");
            }
        }
        
    }
}