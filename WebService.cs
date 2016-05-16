using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

using Microsoft.SharePoint.Employee;

using CCC.SP.Employee.SPClasses;

namespace CCC.SP.Employee
{
    public class TimesheetService : ITimesheetService
    {
        private IEnumerable<object> ID;



        [OperationBehavior]
        public String CreateEmployee(String EmailAdd, String Title, String FirstName, String SecondName, String AddLine1, String AddLine2, String City, String County, String Postcode)
        {
            SPEmployee Employee = new SPEmployee(EmailAdd, Title, FirstName, SecondName, AddLine1, AddLine2, City, County, Postcode);
            return Employee.ID.ToString();
        }

        [OperationBehavior]
        public String CreateTimesheet(Int32 TimesheetID, String ShiftDate, String MorningTimeIn, String MorningTimeOut, String AfternoonTimeIn, String AfternoonTimeOut, 
            String LeaveType, Int32 LeaveHours)
        {
            SPIncidents Timesheet = new SPIncidents(TimesheetID, ShiftDate, MorningTimeIn, MorningTimeOut, AfternoonTimeIn, AfternoonTimeOut, LeaveType, LeaveHours);
            return Timesheet.ID.ToString();
        }



        [OperationBehavior]
        public List<SPEmployee> GetEmployee()
        {
            return GetEmployeeList();
        }

        [OperationBehavior]
        public SPEmployee GetEmployee(Int32 EmployeeID)
        {
            return new SPEmployee(EmployeeID);
        }

        [OperationBehavior]
        public List<SPEmployee> SearchEmployee(String SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                List<SPEmployee> listEmployee = new List<SPEmployee>();

                try
                {
                    using (EmployeeContext context = SPUtil.GetSPContext(""))
                    {
                        CamlQuery query = new CamlQuery();
                        query.ViewXml =
                            @"<View>
                            <Query>
                                <Where><Or><Contains><FieldRef Name='Title' /><Value Type='Text'>" + SearchString + @"</Value></Contains><Contains><FieldRef Name='EmployeeID' /><Value Type='Text'>" + SearchString + @"</Value></Contains></Or></Where> 
                            </Query>
                        </View>";

                        ListItemCollection SPEmployeeDetails = context.Web.Lists.GetByTitle("Employees").GetItems(query);
                        context.Load(SPEmployeeDetails);
                        context.ExecuteQuery();

                        foreach (ListItem SPEmployeeItem in SPEmployeeDetails)
                        {
                            listEmployee.Add(new SPC(SPEmployeeItem.Id));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                return listEmployee;
            }

            return null;
        }


        private List<SPEmployee> GetEmployeeList()
        {
            List<SPEmployee> list = new List<SPEmployee>();

            try
            {
                using (EmployeeContext context = SPUtil.GetSPContext(""))
                {
                    CamlQuery caml = new CamlQuery();
                    ListItemCollection SPEmployees = context.Web.Lists.GetByTitle("Employees").GetItems(caml);
                    context.Load(SPEmployees);
                    context.ExecuteQuery();


                    foreach (ListItem SPEmployee in SPEmployees)
                    {
                        list.Add(new SPEmployees()
                        {
                            ID = SPEmployee.Id,
                            Title = Convert.ToString(SPEmployee["Name_x0020__x002d__x0020_Title"]),
                            Forename = Convert.ToString(SPEmployee["Name_x0020__x002d__x0020_Forenam"]),
                            Surname = Convert.ToString(SPSPEmployee["Name_x0020__x002d__x0020_Surname"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return list;
        }

    }
}
