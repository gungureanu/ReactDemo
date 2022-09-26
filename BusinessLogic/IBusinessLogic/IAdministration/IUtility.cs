using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTypes;
using DataTypes.ModelDataTypes;
using System.Data;

namespace BusinessLogic.IBusinessLogic
{
    public interface IUtility
    {

        #region Function for Error Logs
        Int32 InsertErrorLogs(Guid userID, string errorPage, string methodName, string errorMessage, string errorDescription, string errorMode, string errorCode, bool active = true);
        #endregion


    }
}
