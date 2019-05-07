using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Domain.Models.Settings
{
    public class Swagger
    {
        public string EndpointName { get; set; }
        public string EndpointUrl { get; set; }
        public string XMLFilePath { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactUrl { get; set; }
        public string LicenseName { get; set; }
        public string LicenseUrl { get; set; }
    }
}
