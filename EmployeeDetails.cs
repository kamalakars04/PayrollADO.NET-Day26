// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fileName.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Your name"/>
// --------------------------------------------------------------------------------------------------------------------
namespace EmployeePayroll
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EmployeeDetails
    {
        public int empId { get; set; }
        public string empName { get; set; }
        public char gender { get; set; }
        public string phoneNumber { get; set; }
        public int payrollId { get; set; }
        public DateTime startDate { get; set; }
        public List<int> deptid { get; set; }

        private static EmployeeDetails instance = null;
        private EmployeeDetails()
        {

        }
        public static EmployeeDetails GetInstance()
        {
            if (instance == null)
                instance = new EmployeeDetails();
            return instance;
        }
    }
}
