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
            //try
            //{
                // If no error return true else false
                connection.Open();
                connection.Close();
                logger.Info("connection established");
                return true;
            //}
            //catch
            //{
            //    logger.Info("connection not established");
            //    return false;
            //}
        }
    }
}
