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

    public class EmpAddress
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public static EmpAddress instance = null;
        public static EmpAddress GetInstance()
        {
            if (instance == null)
                instance = new EmpAddress();
            return instance;
        }
    }
}
