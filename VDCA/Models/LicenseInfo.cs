using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDCA.Models
{
    public class LicenseInfo
    {
        public string PackageName { get; set; }
        public string PackageVersion { get; set; }
        public string PackageUrl { get; set; }
        public string Copyright { get; set; }
        public string[] Authors { get; set; }
        public string Description { get; set; }
        public string LicenseUrl { get; set; }
        public string LicenseType { get; set; }
        // Computed property to get the license file name
        public string LicenseFileName => $"{LicenseType}.txt";
    }
}
