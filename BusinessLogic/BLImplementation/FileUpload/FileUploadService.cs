
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
using System.Reflection.Metadata.Ecma335;
using ExcelDataReader;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Data.Common;
using DataTypes.ModelDataTypes.FileUpload;

namespace BusinessLogic.BLImplementation
{
    public class FileUploadService : IFileUpload
    {

        //CODE FOR IMPORT EXCEL DATA IN SQL SERVER
        public string SaveExcelData(string filePath, string oldFileName, string newFileName, Guid UplodedBy, byte[] fileByes)
        {
            string retVal = string.Empty;
            try
            {
                DataSet excelfileDS = Read_ExcelFileAsDataset(filePath, false);
                if (excelfileDS != null && excelfileDS != null && excelfileDS.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(excelfileDS.Tables[0].Rows[0][0].ToString()) &&
                        !string.IsNullOrEmpty(excelfileDS.Tables[0].Rows[0][1].ToString())
                        && !string.IsNullOrEmpty(excelfileDS.Tables[0].Rows[0][2].ToString()))
                    {
                        if (excelfileDS.Tables[0].Rows[0][0].ToString() == "Employee Number")
                        {
                            if (excelfileDS.Tables[0].Rows[0][1].ToString() == "Employee Name")
                            {
                                if (excelfileDS.Tables[0].Rows[0][2].ToString() == "Employee Salary")
                                {
                                    //DO NOTHING
                                }
                                else
                                {
                                    return "formatnotvalid";
                                }                               

                            }
                            else
                            {
                                return "formatnotvalid";
                            }
                        }
                        else
                        {
                            return "formatnotvalid";
                        }
                    }
                    else
                    {
                        return "formatnotvalid";
                    }
                    
                    excelfileDS = Read_ExcelFileAsDataset(filePath, true);

                    foreach (DataRow row in excelfileDS.Tables[0].Rows)
                    {

                        try
                        {
                            DynamicParameters parameter = new DynamicParameters();
                            if (!string.IsNullOrEmpty(row["column0"].ToString()) && !string.IsNullOrEmpty(row["column1"].ToString()) && !string.IsNullOrEmpty(row["column2"].ToString()))
                            {
                                parameter.Add("@EmployeeID", row["column0"].ToString());
                                parameter.Add("@EmployeeName", row["column1"].ToString());
                                parameter.Add("@EmployeeSalary", row["column2"].ToString());
                                parameter.Add("@UplodedBy", UplodedBy);

                                retVal = FactoryServices.dbFactory.SelectCommand_SP(retVal, "system_Employee_Import_Add", parameter);
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }

                    }
                    //ADD FILE INFORMATION INTO DATABASE
                    DynamicParameters fileparameter = new DynamicParameters();
                    fileparameter.Add("@FileName", newFileName);
                    fileparameter.Add("@OrignalFileName", oldFileName);
                    fileparameter.Add("@FilePath", "/Temp");
                    fileparameter.Add("@UplodedBy", UplodedBy);
                    fileparameter.Add("@FileByte", fileByes);

                    retVal = FactoryServices.dbFactory.SelectCommand_SP(retVal, "system_FileInfo_Add", fileparameter);


                    //CODE FOR MOVE THE FILE FROm ONE FOLDER TO ANOTHER FOLDER                    
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retVal;
        }

        //CODE FOR READING THE EXCEL FILE
        private DataSet Read_ExcelFileAsDataset(string filePath, bool header)
        {
            DataSet dataSet = null;
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        if (header)
                        {
                            dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                            {
                                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                                {
                                    FilterRow = rowReader => rowReader.Depth > 0
                                }
                            });
                        }
                        else
                        {
                            dataSet = reader.AsDataSet();
                        }
                    }
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                return dataSet;
            }
        }

        //GET UPLOADED FILE INFORMATION
        public List<FilesInfo> GetUploadFileList()
        {
            List<FilesInfo> fileList = new List<FilesInfo>();
            try
            {

                fileList = FactoryServices.dbFactory.SelectCommand_SP(fileList, "system_FileInfo_Get");
                return fileList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
