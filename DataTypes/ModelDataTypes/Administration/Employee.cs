using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DataTypes.ModelDataTypes;

namespace DataTypes.ModelDataTypes
{
    public class Employee: BaseEntityOptional
    {
        public Int64 EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal EmployeeSalary { get; set; }
        public bool Active { get; set; }       
        public string Password { get; set; }       
        public string UserStatus { get; set; }
        public Guid UserID { get; set; }

    }
}
