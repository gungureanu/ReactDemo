using BusinessLogic;
using DataTypes.ModelDataTypes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using Document = iTextSharp.text.Document;
using Rectangle = System.Drawing.Rectangle;

namespace ReactAPI.Controllers.Administrator
{

    public class AccountController : ControllerBase
    {

        private readonly IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            this._config = config;
        }

        //EMPLOYEE VERIFICATION CODE
        [HttpPost]
        [Route("api/AppLogin")]
        public async Task<IActionResult> LoginValidateUsers([FromBody] UserProfile objUserModel)
        {
            try
            {
                UserProfile RetvalUser2 = BusinessLogicFactory.accountService.GetLoginInfo(objUserModel.EmailAddress, BusinessLogicFactory.cryptographyService.Encrypt(objUserModel.Password));
                //var tokenString = GenerateJSONWebToken(RetvalUser2);
                //RetvalUser2.UserWebToken = tokenString;
                return Ok(RetvalUser2);
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(Guid.Empty, "Login", "ValidateUsers", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                return Ok(ex.StackTrace);
            }
        }

        //GET EMPLOYEE LIST CODE
        //[Authorize]
        [HttpGet]
        [Route("api/GetEmployeeList")]
        public async Task<IActionResult> GetEmployeeList(int pageNo, int pageSize)
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                employeeList = BusinessLogicFactory.accountService.GetEmployeeList(pageNo, pageSize);
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(Guid.Empty, "Account", "GetEmployeeList", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                return Ok(ex.StackTrace);
            }
            return Ok(employeeList);
        }

        //EDIT EMPLOYEE BY ID
        //[Authorize]
        [HttpPost]
        [Route("api/EditEmployeeByID")]
        public async Task<IActionResult> EditEmployeeByID([FromBody] CustomUtility objUtilityModel)
        {
            int retval = 0;
            try
            {
                retval = BusinessLogicFactory.accountService.EditEmployeeByID(objUtilityModel);
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(Guid.Empty, "Account", "objUtilityModel", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                return Ok(ex.StackTrace);
            }
            return Ok(retval);
        }

        //GENERATE THE TOKEN
        [NonAction]
        private string GenerateJSONWebToken(UserProfile userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddDays(3),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //ADD NEW EMPLOYEE
        [HttpPost]
        [Route("api/AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee objEmployeeModel)
        {
            try
            {
                Employee RetvalUser2 = BusinessLogicFactory.accountService.AddEmployee(objEmployeeModel);
                return Ok(RetvalUser2);
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(objEmployeeModel.UserID, "Login", "AddEmployee", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                return Ok(ex.StackTrace);
            }
        }
        //DELETE EMPLOYEE
        [HttpGet]
        [Route("api/DeleteEmployee/{EmployeeID}")]
        public async Task<IActionResult> DeleteEmployee(Int64 EmployeeID)
        {
            int retval = 0;
            List<string> objDelete = new List<string>();
            try
            {
                retval = BusinessLogicFactory.accountService.DeleteEmployee(EmployeeID);
                if (retval > 0)
                {
                    objDelete.Add("delete:" + EmployeeID.ToString());
                }
                else
                {
                    objDelete.Add("delete:0");
                }

                return Ok(objDelete);
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(Guid.Empty, "Login", "DeleteEmployee", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                return Ok(ex.StackTrace);
            }
        }

        // CREATE EMPLOYEE PDF
        [Route("api/CreateEmployeeDataPDF")]
        [HttpGet]
        public IActionResult CreateAllEmployeeDataPDF()
        {
            string PDFName = "";
            try
            {
                List<Employee> employeeList = BusinessLogicFactory.accountService.GetAllEmployeeList().ToList();

                if (employeeList != null)
                {

                    string ApplicationDirectoryPath = "";
                    string appRootDir = "";
                    PDFName = "EmployeeData-" + System.DateTime.Now.ToString("MM-dd-yyyy-hh-MM-sss") + ".pdf";


                    appRootDir = "C:/Temp/PDF/";
                    //appRootDir = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot/" + ApplicationDirectoryPath);

                    if (!Directory.Exists(appRootDir))
                    {
                        Directory.CreateDirectory(appRootDir);
                    }

                    using (FileStream fs = new FileStream(appRootDir + "/" + $@"\{PDFName}", FileMode.Create, FileAccess.Write, FileShare.None))
                    using (Document doc = new Document(PageSize.A4.Rotate()))
                    using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                    {
                        doc.Open();

                        PdfContentByte cbHeading = writer.DirectContent;
                        BaseFont bfcbHeading = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        BaseColor FontColor = new BaseColor(99, 110, 123);
                        var MyFont = FontFactory.GetFont("Arial", 11, FontColor);

                        var ItalicBold = FontFactory.GetFont(BaseFont.TIMES_BOLDITALIC, 11, FontColor);
                        var InfoFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColor);
                        BaseColor Color = new BaseColor(118, 118, 118);

                        var MyHeadFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD, Color);
                        var MyHeadFont2 = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD, Color);
                        var MyFontDetail = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, Color);


                        //CODE FOR SET THE HEADER
                        BaseColor Color3 = new BaseColor(78, 155, 234);
                        var MyHeadFont4 = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, Color3);
                        BaseColor Color1 = new BaseColor(210, 225, 249);


                        PdfPTable tblHeader = new PdfPTable(new float[] { 30f, 35f, 35f });
                        tblHeader.WidthPercentage = 100;

                        PdfPTable table = new PdfPTable(new float[] { 100f });
                        table.WidthPercentage = 100;
                        //table.DefaultCell.Border = Rectangle.NO_BORDER;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("https://www.reactdemo.net/image/reactjs-logo.jpg");
                        jpg.ScaleToFit(166f, 25f);
                        PdfPCell cell1 = new PdfPCell(jpg);
                        cell1.Border = 0;                       
                        table.AddCell(cell1);                        
                        table.CompleteRow();
                        doc.Add(table);

                        doc.Add(new Paragraph(5, "\u00a0"));

                        PdfPCell Header = new PdfPCell(new Phrase("Employee Number", MyHeadFont4));
                        Header.FixedHeight = 10f;
                        Header.VerticalAlignment = Element.ALIGN_CENTER;
                        Header.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        Header.Border = 0;
                        Header.BackgroundColor = Color1;
                        tblHeader.AddCell(Header);


                        Header = new PdfPCell(new Phrase("Employee Name", MyHeadFont4));
                        Header.FixedHeight = 15f;
                        Header.VerticalAlignment = Element.ALIGN_CENTER;
                        Header.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        Header.Border = 0;
                        Header.BackgroundColor = Color1;
                        tblHeader.AddCell(Header);


                        Header = new PdfPCell(new Phrase("Employee Salary", MyHeadFont4));
                        Header.FixedHeight = 15f;
                        Header.VerticalAlignment = Element.ALIGN_CENTER;
                        Header.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        Header.Border = 0;
                        Header.BackgroundColor = Color1;
                        tblHeader.AddCell(Header);
                        doc.Add(tblHeader);

                        doc.Add(new Paragraph(5, "\u00a0"));

                        PdfPTable tableline = new PdfPTable(new float[] { 100f });
                        tableline.WidthPercentage = 100;
                        PdfPCell line = new PdfPCell(new Phrase(""));
                        line.Colspan = 4;
                        line.Border = 1;
                        tableline.AddCell(line);
                        doc.Add(tableline);


                        for (var j = 0; j < employeeList.Count; j++)
                        {
                            PdfPTable tbltaskDetail = new PdfPTable(new float[] { 30f, 35f, 35f });
                            tbltaskDetail.WidthPercentage = 100;

                            //string imagePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot/images/Icon-TaskType/" + projectTaskDetailProfiles[j].FileName);
                            //ID column
                            PdfPCell Record = new PdfPCell();
                            Record = new PdfPCell(new Phrase("" + employeeList[j].EmployeeID, MyFontDetail));
                            //Record.PaddingLeft = Convert.ToInt32(projectTaskDetailProfiles[j].IndentLevel) * 3;
                            Record.PaddingTop = 3f;
                            Record.PaddingBottom = 3f;
                            //Record.FixedHeight = 15f;
                            Record.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            Record.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                            Record.BorderWidthTop = 0;
                            Record.BorderWidthLeft = 0;
                            Record.BorderWidthRight = 0;
                            Record.BorderWidthBottom = 0.25f;
                            tbltaskDetail.AddCell(Record);


                            //Task Key
                            Record = new PdfPCell(new Phrase("" + employeeList[j].EmployeeName, MyFontDetail));
                            //Record.PaddingLeft = Convert.ToInt32(projectTaskDetailProfiles[j].IndentLevel) * 3;
                            Record.PaddingTop = 3f;
                            Record.PaddingBottom = 3f;
                            //Record.FixedHeight = 15f;
                            Record.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            Record.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                            Record.BorderWidthTop = 0;
                            Record.BorderWidthLeft = 0;
                            Record.BorderWidthRight = 0;
                            Record.BorderWidthBottom = 0.25f;
                            tbltaskDetail.AddCell(Record);

                            //Task % Complete column
                            Record = new PdfPCell(new Phrase("" + employeeList[j].EmployeeSalary.ToString() + "%", MyFontDetail));
                            Record.PaddingTop = 3f;
                            Record.PaddingBottom = 3f;
                            Record.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            Record.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                            Record.BorderWidthTop = 0;
                            Record.BorderWidthLeft = 0;
                            Record.BorderWidthRight = 0;
                            Record.BorderWidthBottom = 0.25f;
                            tbltaskDetail.AddCell(Record);

                            doc.Add(tbltaskDetail);

                            //doc.Add(new Paragraph(10, "\u00a0"));
                        }
                        doc.Add(new Paragraph(7, "\u00a0"));

                        doc.Close();
                    }

                    string FilePath = appRootDir + "/" + $@"\{PDFName}" + "";
                    byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
                    File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, PDFName);
                    return Ok(PDFName);

                }

            }
            catch (Exception ex)
            {                
                BusinessLogicFactory.utility.InsertErrorLogs(Guid.Empty, "Login", "CreateAllEmployeeDataPDF", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);

            }
            return Ok(PDFName);
        }

        [Route("api/DownloadEmployeeDataPDF")]
        [HttpGet]
        public FileResult DownloadEmployeeDataPDF(string originalFileName)
        {

            try
            {
                string sWebRootFolder = "C:/Temp/PDF/";
                string sFileName = @"" + originalFileName + "";
                //string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, originalFileName));
                var path = Path.Combine(sWebRootFolder, sFileName);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
                file.Delete();
                return File(memory, GetContentType(path), Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                BusinessLogicFactory.utility.InsertErrorLogs(Guid.Empty, "Login", "DownloadEmployeeDataPDF", ex.Message, Convert.ToString(ex.InnerException), "", Convert.ToString(ex.HResult), true);
                throw ex;
            }
        }
        [NonAction]
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        [NonAction]
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
