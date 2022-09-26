using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes.ModelDataTypes.FileUpload
{
    public class FilesInfo : BaseEntityOptional
    {
        public string FileName { get; set; }
        public string OrignalFilename { get; set; }
        public string UserName { get; set; }
        public string FilePath { get; set; }
        public bool Active { get; set; }

    }
}
