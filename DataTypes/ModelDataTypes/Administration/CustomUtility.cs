using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes.ModelDataTypes
{
    public class CustomUtility : BaseEntityOptional
    {     
        public Guid UserID { get; set; }
        public string id { get; set; }
        public string tableName { get; set; }
        public string columnName { get; set; }
        public string columnValue { get; set; }
        public string condition { get; set; }

    }
}
