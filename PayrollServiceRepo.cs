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
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "dbo.GetPayRollDetails";
            if (empid != 0)
                command.Parameters.AddWithValue("@empId",1);
            command.Connection = connection;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("Empid    payrollid   basepay     deductions      taxable     tax     netpay");
            while (reader.HasRows)
            {
                while(reader.Read())
                Console.WriteLine(reader[0]+"           " +reader[1] + "       " + reader[2] + "               " + 
                                  reader[3] + "         " + reader[4] + "           " + reader[5] + "            " + reader[6]);
            }
        }
    }
}
