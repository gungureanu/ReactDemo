using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTypes;
using DataTypes.ModelDataTypes;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using DataAccess;
using System.Text.RegularExpressions;
using System.Web;

using System.Reflection;
using System.ComponentModel;

namespace BusinessLogic.BLImplementation
{
    public class UtilityService : IBusinessLogic.IUtility
    {
        
        public Int32 InsertErrorLogs(Guid userID, string errorPage, string methodName, string errorMessage, string errorDescription, string errorMode, string errorCode, bool active = true)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();                
                parameter.Add("@UserID", userID);
                parameter.Add("@ErrorMode", errorMode.Trim().Replace("'", "''"));
                parameter.Add("@ErrorCode", errorCode.Trim().Replace("'", "''"));
                parameter.Add("@ErrorPage", errorPage.Trim().Replace("'", "''"));
                parameter.Add("@MethodName", methodName.Trim().Replace("'", "''"));
                parameter.Add("@ErrorMessage", errorMessage.Trim().Replace("'", "''"));
                parameter.Add("@Description", errorDescription.Trim().Replace("'", "''"));
                parameter.Add("@Active", active);
                return FactoryServices.dbFactory.InsertCommand_SP("system_ErrorLog_Add", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

    }//CLASS END HERE
}
