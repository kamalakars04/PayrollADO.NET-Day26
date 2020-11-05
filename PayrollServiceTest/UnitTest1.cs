// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fileName.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Your name"/>
// --------------------------------------------------------------------------------------------------------------------
namespace PayrollServiceTest
{
    using NUnit.Framework;
    using EmployeePayroll;

    public class Tests
    {
        PayrollServiceRepo payrollService;
        [SetUp]
        public void Setup()
        {
            payrollService = new PayrollServiceRepo();
        }

        /// <summary>
        /// TC 1 Checks the connection.
        /// </summary>
        [Test]
        public void CheckConnection()
        {
            bool actual = payrollService.CheckConnection();
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// TC 2 UC 8 Gets the payroll details.
        /// </summary>
        [Test]
        public void GetPayrollDetails()
        {
            PayrollDetails actual = payrollService.GetPayrollDetails(1);
            Assert.AreEqual(50000, actual.BasePay);
        }
    }
}