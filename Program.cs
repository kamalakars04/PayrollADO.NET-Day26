// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fileName.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Your name"/>
// --------------------------------------------------------------------------------------------------------------------
namespace EmployeePayroll
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            //UC 1 check connection
            PayrollServiceRepo payrollService = new PayrollServiceRepo();
            Console.WriteLine(payrollService.CheckConnection());

            //UC2 get all payroll details
            payrollService.GetPayrollDetails(1);

            //UC 3 update salary of employee
            payrollService.UpdateSalaryOfEmployee(1, 7, 50000, 1000);

            // UC 5 Retrieve with start date
            payrollService.RetrieveWithStartDate(new DateTime(2014,01,01), new DateTime(2019,01,01));
        }
    }
}
