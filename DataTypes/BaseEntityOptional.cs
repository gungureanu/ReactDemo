using System;
namespace DataTypes
{
    public class BaseEntityOptional
    {
        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }        
        public Guid CreatedBy { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
