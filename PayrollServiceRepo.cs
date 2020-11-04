// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fileName.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Your name"/>
// --------------------------------------------------------------------------------------------------------------------
namespace EmployeePayroll
{
    using System;
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
        /// </summary>
        public void GetPayrollDetails(int empid = 0)
        {
            try
            {
                // Create a command with text type and parameters
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.GetPayRollDetails";
                if (empid != 0)
                    command.Parameters.AddWithValue("@empId", 1);
                command.Connection = connection;
                connection.Open() ;

                //Read the output of command execution
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Empid    payrollid   basepay     deductions      taxable     tax     netpay");
                while (reader.HasRows)
                {
                    if (reader.Read())
                        Console.WriteLine(reader[0] + "           " + reader[1] + "       " + reader[2] + "               " +
                                          reader[3] + "         " + reader[4] + "           " + reader[5] + "            " + reader[6]);
                    else
                        break;
                }
                logger.Info("User accesed payroll records");
                connection.Close();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// UC 3 Updates the salary of employee.
        /// </summary>
        /// <param name="empid">The empid.</param>
        /// <param name="payrollid">The payrollid.</param>
        /// <param name="basepay">The basepay.</param>
        /// <param name="deduction">The deduction.</param>
        public void UpdateSalaryOfEmployee(int empid,int payrollid,int basepay,int deduction)
        {
            // open connection and create transaction
            connection.Open();
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
                command.ExecuteNonQuery();
                transaction.Commit();
                connection.Close();
            }
            catch
            {
                if (CheckConnection())
                    transaction.Rollback();
            }
            finally
            {
                if (CheckConnection())
                connection.Close();
            }
        }
    }
}
