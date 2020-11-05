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

    class Departments
    {
        public int deptid { get; set; }
        public string deptName { get; set; }

        private static Departments instance = null;

        static Departments GetInstance()
        {
            if (instance == null)
                instance = new Departments();
            return instance;
        }
    }
}
