using BusinessLogic;
using BusinessLogic.IBusinessLogic;
using DataTypes.ModelDataTypes;
using DataTypes.ModelDataTypes.FileUpload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;

namespace ReactAPI.Controllers.FileUpload
{
    public class FileUploadController : ControllerBase
    {


        private readonly ICryptographyService _cryptographyService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        public FileUploadController(ICryptographyService cryptographyService, IWebHostEnvironment env, IConfiguration config)
        {
            this._cryptographyService = cryptographyService;
            this._env = env;
            _config = config;
        }

        //CODE TO UPDATE UPLOAD EXCEL AND SAVED THE DATA INTO THE DATABASE
        //[Authorize]
        [HttpPost]
        [Route("api/PostImages")]
        public async Task<IActionResult> PostImages(Guid UserID)
        {
            try
            {
                var fileName = string.Empty;
                var newFileName = string.Empty;
                var oldFileName = string.Empty;
                Int64 fileLength = 0;
                byte[] fileByes = null;
                string retval = "";
                List<string> objReturn = new List<string>();
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    for (Int32 i = 0; i < files.Count; i++)
                    {
                        if (files[i].Length > 0)
                        {
                            fileName = ContentDispositionHeaderValue.Parse(files[i].ContentDisposition).FileName.Trim('"');
                            string name = Path.GetFileNameWithoutExtension(fileName);
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var FileExtension = Path.GetExtension(fileName);
                            string projectPath = _env.ContentRootPath;
                            oldFileName = name + FileExtension;
                            newFileName = myUniqueFileName + FileExtension;
                            if (!string.IsNullOrEmpty(fileName.Trim().ToString()))
                            {
                                if (!Directory.Exists(Path.Combine("C:/Temp/")))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(Path.Combine("C:/Temp/"));
                                }
                                //fileName = Path.Combine(projectPath, "/www/UploadedFiles/Temp/") + $@"/{newFileName}";                                
                                fileName = "C:/Temp/" + $@"/{newFileName}";
                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    files[i].CopyTo(fs);
                                    fs.Flush();
                                }
                                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                                {
                                    using (var reader = new BinaryReader(stream))
                                    {
                                        fileByes = reader.ReadBytes((int)stream.Length);
                                    }
                                }
                            }
                        }

                        //CHECK IF THE FILE EXIST
                        if (System.IO.File.Exists(fileName))
                        {
                            //fileLength = new FileInfo(fileName).Length / 1024;
                            //if (fileLength > 1024)
                            //{
                            //return Ok("File size is greater than 1MB, Please reduce the file size and try again!");
                            //}
                            //else
                            //{
                            //SAVE FILE DATA INTO DATABASE
                            retval = BusinessLogicFactory.fileService.SaveExcelData(fileName, oldFileName, newFileName, UserID, fileByes);
                            //}
                        }
                    }

                    objReturn.Add(retval + ":");
                    return Ok(objReturn);
                }
                else
                {
                    return Ok("Excel Image is not saved");
                }
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(UserID, "FileUpload", "Index", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                return BadRequest("Internal Server Error. Contact to Administrator");
            }
        }

        //CHECK UPLOAD FILE SIZE
        [HttpPost]
        [Route("api/CheckFileSize")]
        public async Task<IActionResult> CheckFileSize()
        {
            try
            {
                var fileName = string.Empty;
                var newFileName = string.Empty;
                var oldFileName = string.Empty;
                Int64 fileLength = 0;

                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    for (Int32 i = 0; i < files.Count; i++)
                    {
                        if (files[i].Length > 0)
                        {
                            fileName = ContentDispositionHeaderValue.Parse(files[i].ContentDisposition).FileName.Trim('"');
                            string name = Path.GetFileNameWithoutExtension(fileName);
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var FileExtension = Path.GetExtension(fileName);

                            oldFileName = name + FileExtension;
                            newFileName = myUniqueFileName + FileExtension;
                            if (!string.IsNullOrEmpty(fileName.Trim().ToString()))
                            {
                                if (!Directory.Exists(Path.Combine(_env.ContentRootPath, "/www/UploadedFiles/Temp/")))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, "/www/UploadedFiles/Temp/"));
                                }
                                fileName = _env.ContentRootPath + "/www/UploadedFiles/Temp/" + newFileName;
                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    files[i].CopyTo(fs);
                                    fs.Flush();

                                }


                            }
                        }
                    }
                    if (System.IO.File.Exists(fileName))
                    {
                        fileLength = new FileInfo(fileName).Length / 1024;
                    }
                }
                return Ok(fileLength);
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(Guid.Empty, "FileUpload", "CheckFileSize", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                return BadRequest("Internal Server Error. Contact to Administrator");
            }
        }

        //GET UPLOADED FILE LIST
        //[Authorize]
        [HttpGet]
        [Route("api/GetUploadFileList")]
        public async Task<IActionResult> GetUploadFileList()
        {
            List<FilesInfo> fileList = new List<FilesInfo>();
            try
            {
                fileList = BusinessLogicFactory.fileService.GetUploadFileList();
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(Guid.Empty, "FileUpload", "GetUploadFileList", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                return Ok(ex.StackTrace);
            }
            return Ok(fileList);
        }

    }
}
