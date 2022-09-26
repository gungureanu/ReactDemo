using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace BusinessLogic
{
   public static class BusinessLogicFactory
    {
        private static BusinessLogic.IBusinessLogic.ICryptographyService _cryptographyService;
        private static BusinessLogic.IBusinessLogic.IAccountService _accountService;
        private static BusinessLogic.IBusinessLogic.IFileUpload _fileService;
        private static BusinessLogic.IBusinessLogic.IUtility _utility;
        public static BusinessLogic.IBusinessLogic.ICryptographyService cryptographyService { get { return _cryptographyService ?? (_cryptographyService = new BusinessLogic.BLImplementation.CryptographyService()); } }
        public static BusinessLogic.IBusinessLogic.IAccountService accountService { get { return _accountService ?? (_accountService = new BusinessLogic.BLImplementation.AccountService()); } }
        public static BusinessLogic.IBusinessLogic.IFileUpload fileService { get { return _fileService ?? (_fileService = new BusinessLogic.BLImplementation.FileUploadService()); } }
        public static BusinessLogic.IBusinessLogic.IUtility utility { get { return _utility ?? (_utility = new BusinessLogic.BLImplementation.UtilityService()); } }

    }// Class Ends HEre
}
