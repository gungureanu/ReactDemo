using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
   public class DBConnection
    {
        public string constr { get; set; }
        public  string constAdditionalAuthenticationKey { get; set; }
        public IConfigurationRoot Configuration { get; set; }
        public string Main(string[] args = null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            constr = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            constAdditionalAuthenticationKey = Configuration.GetSection("AppSettingKeys")["AdditionalAuthentication"];
            return constr;
        }
    }
}
