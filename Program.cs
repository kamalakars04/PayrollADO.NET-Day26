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
            // UC 1 check connection
            PayrollServiceRepo payrollService = new PayrollServiceRepo();
            Console.WriteLine(payrollService.CheckConnection());

            // UC2 get all payroll details
            payrollService.GetPayrollDetails(1);

            // UC 3 update salary of employee
            payrollService.UpdateSalaryOfEmployee(1, 7, 50000, 1000);

            // UC 5 Retrieve with start date
            payrollService.RetrieveWithStartDate(new DateTime(2014, 01, 01), new DateTime(2019, 01, 01));

            // UC 6 find min max sum avg of salary
            payrollService.PerformSumAvgMinMax();

            // UC 7 Add employee with ER
            // Create employee class
            EmployeeDetails employee = EmployeeDetails.GetInstance();
            employee.empName = "Ravan";
            employee.gender = 'M';
            employee.phoneNumber = "9874569874";
            employee.startDate = new DateTime(2019, 01, 05);
            employee.payrollId = 2;

            // Create address instance
            EmpAddress address = EmpAddress.GetInstance();
            address.street = "gandhi nagar";
            address.city = "pune";
            address.state = "Maharastra";
            payrollService.AddEmployee(employee, address);

            // UC 12 Delete emp
            payrollService.DeleteEmployee(6);
            payrollService.GetPayrollDetails();
        }
    }
}
