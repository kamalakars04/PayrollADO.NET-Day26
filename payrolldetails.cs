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

    public class PayrollDetails
    {
        public int EmpId { get; set; }
        public int Payrollid { get; set; }
        public  int BasePay { get; set; }
        public  int Deductions { get; set; }
        public  int TaxablePay { get; set; }
        public  int IncomeTax { get; set; }
        public  int NetPay { get; set; }

        public static PayrollDetails instance = null;
        public static PayrollDetails GetInstance()
        {
            if (instance == null)
                instance = new PayrollDetails();
            return instance;
        }
    }
}
