using System;
using System.ComponentModel.DataAnnotations;

namespace DataTypes
{
    public class BaseEntity
    {
        public Int64 Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Int64 CreateBy { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}