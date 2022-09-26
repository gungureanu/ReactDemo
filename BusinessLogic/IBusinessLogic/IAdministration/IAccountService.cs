using DataTypes.ModelDataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.IBusinessLogic
{
    public interface IAccountService
    {
        //GET LOGIN INFORMATION
        UserProfile GetLoginInfo(string emailId, string password);
        //GET EMPLOYEE LIST
        List<Employee> GetEmployeeList(int pageNo, int pageSize);
        //UPDATE EMPLOYEE BY ID
        int EditEmployeeByID(CustomUtility objEmpModel);
        //ADD NEW EMPLOYEE
        Employee AddEmployee(Employee objEmpModel);
        //DELETE EMPLOYEE
        int DeleteEmployee(Int64 EmployeeID);
        //CODE TO GET ALL EMPLOYEE
        List<Employee> GetAllEmployeeList();

    }   
}
