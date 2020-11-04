using System;

namespace EmployeePayroll
{
    class Program
    {
        static void Main(string[] args)
        {
            //UC 1 check connection
            PayrollServiceRepo payrollService = new PayrollServiceRepo();
            Console.WriteLine(payrollService.CheckConnection());

            //UC2 get all payroll details
            payrollService.GetPayrollDetails(1);
        }
    }
}
