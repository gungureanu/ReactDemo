
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using DataTypes.ModelDataTypes;
using Dapper;
using DataAccess;
using System.Data.SqlClient;
using System.Data;
using BusinessLogic.IBusinessLogic;

namespace BusinessLogic.BLImplementation
{
    public class AccountService : IAccountService
    {

        //CODE TO GET LOGIN INFORMATION
        public UserProfile GetLoginInfo(string emailId, string password)
        {
            UserProfile objProfile = new UserProfile();
            try
            {

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Email", emailId);
                parameter.Add("@Password", password);                
                objProfile = FactoryServices.dbFactory.SelectCommand_SP(objProfile, "system_Users_ProfileInfo_Verify", parameter);                
                return objProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //CODE TO GET EMPLOYEE
        public List<Employee> GetEmployeeList(int pageNo, int pageSize)
        {
            List<Employee> empList = null;
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@PageNo", pageNo);
                parameter.Add("@PageSize", pageSize);
                empList = FactoryServices.dbFactory.SelectCommand_SP(empList, "system_Employee_Get", parameter);

                return empList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //CODE TO UPDATE EMPLOYEE
        public int EditEmployeeByID(CustomUtility objEmpModel)
        {            
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@ColmunId", objEmpModel.id);
                parameter.Add("@TableName", objEmpModel.tableName);
                parameter.Add("@ColumnName", objEmpModel.columnName);
                parameter.Add("@ColumnValue", objEmpModel.columnValue);
                parameter.Add("@Condition", objEmpModel.condition);
                parameter.Add("@UserId", objEmpModel.UserID);
                
                return (int)FactoryServices.dbFactory.SelectScalarValue_SP("system_Generic_Update", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //CODE TO ADD NEW EMPLOYEE
        public Employee AddEmployee(Employee objEmpModel)
        {
            Employee objEmployee = new Employee();
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@EmployeeID", objEmpModel.EmployeeID);
                parameter.Add("@EmployeeName", objEmpModel.EmployeeName);
                parameter.Add("@EmployeeSalary", objEmpModel.EmployeeSalary);                              
                parameter.Add("@UserId", objEmpModel.UserID);

                objEmployee = FactoryServices.dbFactory.SelectCommand_SP(objEmployee, "system_Employee_Add", parameter);
                return objEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //CODE TO DELETE EMPLOYEE
        public int DeleteEmployee(Int64 EmployeeID)
        {
            int retval = 0;
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@EmployeeID", EmployeeID);

                retval = FactoryServices.dbFactory.SelectCommand_SP(retval, "system_Employee_Delete", parameter);
                return retval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //CODE TO GET ALL EMPLOYEE
        public List<Employee> GetAllEmployeeList()
        {
            List<Employee> empList = null;
            try
            {
                DynamicParameters parameter = new DynamicParameters();                
                empList = FactoryServices.dbFactory.SelectCommand_SP(empList, "system_AllEmployee_Get");

                return empList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
