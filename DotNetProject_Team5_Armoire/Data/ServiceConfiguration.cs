using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace a00893112s3clothesimages_S3_bucket.Data
{
    public class ServiceConfiguration
    {
        public AWSS3Configuration AWSS3 { get; set; }
    }

    public class AWSS3Configuration
    {
        public string BucketName { get; set; }
        public string BucketRegion { get; set; }
        public string Key { get; set; }
        public string SecretKey { get; set; }

    }
}
