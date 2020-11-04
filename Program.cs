using System;

namespace EmployeePayroll
{
    class Program
    {
        static void Main(string[] args)
        {
            PayrollServiceRepo payrollService = new PayrollServiceRepo();
            Console.WriteLine(payrollService.CheckConnection());
        }
    }
}
