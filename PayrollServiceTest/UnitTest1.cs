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
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// TC 1 Checks the connection.
        /// </summary>
        [Test]
        public void CheckConnection()
        {
            PayrollServiceRepo payrollService = new PayrollServiceRepo();
            bool actual = payrollService.CheckConnection();
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
    }
}