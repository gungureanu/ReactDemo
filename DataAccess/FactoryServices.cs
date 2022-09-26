using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public static class FactoryServices
    {
        #region Private
     public static DbFactory _dbFactory;
        #endregion

        #region Public properties
    public static DbFactory dbFactory
        {
            //set { _iConfiguration = IConfiguration(value) }
            get{ return _dbFactory ?? (_dbFactory = new DbFactory()); }
        }
        #endregion       
    }//==Class Ends Here
}
