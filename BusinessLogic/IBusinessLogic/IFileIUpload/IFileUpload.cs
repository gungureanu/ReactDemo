using DataTypes.ModelDataTypes;
using DataTypes.ModelDataTypes.FileUpload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLogic.IBusinessLogic
{
    public interface IFileUpload
    {
        //CODE FOR IMPORT EXCEL DATA IN SQL SERVER
        string SaveExcelData(string filePath, string oldFileName, string newFileName, Guid UplodedBy, byte[] fileByes);
        //GET UPLOADED FILE INFORMATION
        List<FilesInfo> GetUploadFileList();
    }
}
