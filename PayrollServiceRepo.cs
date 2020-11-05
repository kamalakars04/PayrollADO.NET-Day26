// // --------------------------------------------------------------------------------------------------------------------
// <copyright file="fileName.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Your name"/>
// --------------------------------------------------------------------------------------------------------------------
namespace EmployeePayroll
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using NLog;

    public class PayrollServiceRepo
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        // Create connection string 
        public static string connectionString = @"Server=LAPTOP-CTKSHLKD\SQLEXPRESS; Initial Catalog =payroll_service; User ID = sa; Password=kamal@99";
        SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        /// UC 1 Checks the connection.
        /// </summary>
        /// <returns>bool</returns>
        public bool CheckConnection()
        {
            logger.Debug("User tried to check connection");
            try
            {
                // If no error return true else false
                connection.Open();
                connection.Close();
                logger.Info("connection established");
                return true;
            }
            catch
            {
                logger.Info("connection not established");
                return false;
            }
        }

        /// <summary>
        /// UC 2 Gets the payroll details.
        /// UC 4 Refactor to have singleton class
        /// </summary>
        public PayrollDetails GetPayrollDetails(int empid = 0)
        {
            try
            {
                // Create a command with text type and parameters
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.GetPayRollDetails";
                if (empid != 0)
                    command.Parameters.AddWithValue("@empId", empid);
                command.Connection = connection;
                connection.Open() ;

                //Read the output of command execution
                SqlDataReader reader = command.ExecuteReader();

                // Use of singleton class
                PayrollDetails payrollDetails = PayrollDetails.GetInstance();
                Console.WriteLine("Empid    payrollid   basepay     deductions      taxable     tax     netpay");
                while (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        payrollDetails.EmpId = Convert.ToInt32(reader[0]);
                        payrollDetails.Payrollid = Convert.ToInt32(reader[1]);
                        payrollDetails.BasePay = Convert.ToInt32(reader[2]);
                        payrollDetails.Deductions = Convert.ToInt32(reader[3]);
                        payrollDetails.TaxablePay = Convert.ToInt32(reader[4]);
                        payrollDetails.IncomeTax = Convert.ToInt32(reader[5]);
                        payrollDetails.NetPay = Convert.ToInt32(reader[6]);
                        Console.WriteLine(payrollDetails.EmpId + "           " + payrollDetails.Payrollid + "       " + payrollDetails.Payrollid + "               " +
                         payrollDetails.Deductions + "         " + payrollDetails.TaxablePay + "           " + payrollDetails.IncomeTax
                         + "            " + payrollDetails.NetPay);
                    }
                    else
                        break;
                }
                logger.Info("User accesed payroll records");
                connection.Close();
                return payrollDetails;
            }
            catch
            {
                connection.Close();
                return null;
            }
        }

        /// <summary>
        /// UC 3 Updates the salary of employee.
        /// </summary>
        /// <param name="empid">The empid.</param>
        /// <param name="payrollid">The payrollid.</param>
        /// <param name="basepay">The basepay.</param>
        /// <param name="deduction">The deduction.</param>
        public bool UpdateSalaryOfEmployee(int empid,int payrollid,int basepay,int deduction)
        {
            // open connection and create transaction
            connection.Open();
            int result = 0;
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                // create a new command in transaction
                SqlCommand command = new SqlCommand();
                command.Transaction = transaction;
                command.Connection = connection;

                // Execute command
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.addNewPayroll";
                command.Parameters.AddWithValue("@basepay", basepay);
                command.Parameters.AddWithValue("@deductions", deduction);
                SqlParameter returnvalue = new SqlParameter();
                returnvalue.Direction = System.Data.ParameterDirection.InputOutput;
                returnvalue.DbType = System.Data.DbType.Int32;
                returnvalue.ParameterName = "@payrollid";
                returnvalue.Value = payrollid;
                command.Parameters.Add(returnvalue);
                command.ExecuteScalar();

                // Execute second command
                command.CommandText = "dbo.UpdateEmployeeColumn";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@empid", empid);
                command.Parameters.AddWithValue("@payrollid", returnvalue.Value);
                result = command.ExecuteNonQuery();
                transaction.Commit();
                connection.Close();
                return true;
            }
            catch
            {
                if (CheckConnection())
                    transaction.Rollback();
                return false;
            }
            finally
            {
                if (CheckConnection())
                connection.Close();
            }
        }

        /// <summary>
        /// UC 5 Retrieves the with start date.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public List<EmployeeDetails> RetrieveWithStartDate(DateTime x, DateTime y)
        {
            try
            {
                // Open connection
                connection.Open();

                // Create a new command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // Give the query
                command.CommandText = "select * from employeedetails where start_date between '"+ x + "' and '" + y+"'";
                SqlDataReader reader = command.ExecuteReader();
                EmployeeDetails employeeDetails = EmployeeDetails.GetInstance();
                List<EmployeeDetails> detailList = new List<EmployeeDetails>();
                while(reader.HasRows)
                {
                    // Read the output
                    if (reader.Read())
                    {
                        employeeDetails.empId = Convert.ToInt32(reader[0]);
                        employeeDetails.empName = Convert.ToString(reader[1]);
                        employeeDetails.gender = Convert.ToChar(reader[2]);
                        employeeDetails.phoneNumber = Convert.ToString(reader[3]);
                        employeeDetails.payrollId = Convert.ToInt32(reader[4]);
                        employeeDetails.startDate = Convert.ToDateTime(reader[5]);
                        Console.WriteLine(employeeDetails.empId + "       " + employeeDetails.empName + "        " +
                                           employeeDetails.gender + "         " + employeeDetails.phoneNumber + "         " +
                                           employeeDetails.payrollId + "          " + employeeDetails.startDate);
                        detailList.Add(employeeDetails);
                    }
                    else
                        break;
                }
                return detailList;
        }
            catch
            {
                // Close the connection
                if (CheckConnection())
                    connection.Close();
                return null;
            }
            finally
            {
                // Close the connection
                if (CheckConnection())
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 6 Performs the sum average minimum maximum.
        /// </summary>
        public void PerformSumAvgMinMax()
        {
            try
            {
                // Open connection
                connection.Open();

                // Perform Command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // Find Sum,Avg,Max,Min by male and female
                command.CommandText = "Select gender,Sum(basepay),Avg(basepay),max(basepay),min(basepay) from (select PD.basepay,ED.gender from payrolldetails PD " +
                                       "inner join employeedetails ED" +
                                      " on ED.payrollid = PD.payrollid) as temp group by gender";
                SqlDataReader reader = command.ExecuteReader();

                // Read the output
                while(reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("sum of salary of Gender {0} is {1}", reader[0], reader[1]);
                        Console.WriteLine("average of salary of Gender {0} is {1}", reader[0], reader[2]);
                        Console.WriteLine("maximum of salary of Gender {0} is {1}", reader[0], reader[3]);
                        Console.WriteLine("mimimum of salary of Gender {0} is {1}", reader[0], reader[4]);
                    }

                    else
                        break;
                }
                reader.Close();
            }
            catch
            {
                if (CheckConnection())
                    connection.Close();
            }
            finally
            {
                if (CheckConnection())
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 7 Adds the employee.
        /// </summary>
        /// <param name="emp">The emp.</param>
        /// <param name="address">The address.</param>
        /// <param name="deptid">The deptid.</param>
        public void AddEmployee(EmployeeDetails emp, EmpAddress address, int deptid = 0)
        {
            // open connection and create transaction
            connection.Open();
            try
            {
                // create a new command in transaction
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // Execute command
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.addEmployee";
                command.Parameters.AddWithValue("@EmpName", emp.empName);
                command.Parameters.AddWithValue("@gender", emp.gender);
                command.Parameters.AddWithValue("@PhoneNumber", emp.phoneNumber);
                command.Parameters.AddWithValue("@PayrollId", emp.payrollId);
                command.Parameters.AddWithValue("@start_date", emp.startDate);
                command.Parameters.AddWithValue("@street", address.street);
                command.Parameters.AddWithValue("@city", address.city);
                command.Parameters.AddWithValue("@state", address.state);
                if (deptid != 0)
                    command.Parameters.AddWithValue("@deptid", deptid);

                command.ExecuteNonQuery();
            }
            catch
            {
                if (CheckConnection())
                    connection.Close();
            }
            finally
            {
                if (CheckConnection())
                    connection.Close();
            }
        }
    }
}
