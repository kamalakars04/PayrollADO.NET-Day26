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

        /// <summary>
        /// TC 11 Adds the employee.
        /// </summary>
        [Test]
        public void AddEmployee()
        {
            // Arrange
            EmployeeDetails employeeDetails = EmployeeDetails.GetInstance();
            employeeDetails.gender = 'M';
            employeeDetails.empName = "Rama";
            employeeDetails.payrollId = 1;
            employeeDetails.phoneNumber = "4578547896";
            employeeDetails.startDate = new System.DateTime(2019, 08, 24);
            EmpAddress empAddress = new EmpAddress { city = "trivandrum", state = "Kerala", street = "nagar" };
            int deptid = 1;

            // Act
            PayrollServiceRepo payrollService = new PayrollServiceRepo();
            bool actual = payrollService.AddEmployee(employeeDetails, empAddress, deptid);
            bool expected = false;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}