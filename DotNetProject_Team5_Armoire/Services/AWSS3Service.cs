using a00893112s3clothesimages_S3_bucket.Data;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace a00893112s3clothesimages_S3_bucket.Services
{
    public interface IAWSS3Service
    {
        Task<string> UploadFile(IFormFile fileUpload);
        void DeleteFile(string pictureUri);
    }
    public class AWSS3Service : IAWSS3Service
    {
        private readonly ServiceConfiguration settings;
        public AWSS3Service(IOptions<ServiceConfiguration> settings)
        {
            this.settings = settings.Value;
        }
        public async Task<string> UploadFile(IFormFile fileUpload)
        {
            try
            {
                // Create an aws client
                // install package
                var region = Amazon.RegionEndpoint.GetBySystemName(settings.AWSS3.BucketRegion);
                var s3Client = new AmazonS3Client(settings.AWSS3.Key, settings.AWSS3.SecretKey, region);

                // Create a request
                //Storing a file in S3
                string key = $"{DateTime.Now.Ticks}{fileUpload.FileName}";
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = fileUpload.OpenReadStream(),
                    BucketName = settings.AWSS3.BucketName,
                    Key = key
                };

                // Send the request
                PutObjectResponse response = await s3Client.PutObjectAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return $"https://{settings.AWSS3.BucketName}.s3.{settings.AWSS3.BucketRegion}.amazonaws.com/{key}";
                }
                else { throw new Exception(); }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async void DeleteFile(string pictureUri)
        {
            if (pictureUri != null && pictureUri != "")
            {

                string[] separatingStrings = { "https://armoirevirtualcloset.s3.us-west-2.amazonaws.com/" };

                string[] words = pictureUri.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                string key = "";

                foreach (var word in words)
                {
                    key = word;
                }

                try
                {
                    var region = Amazon.RegionEndpoint.GetBySystemName(settings.AWSS3.BucketRegion);
                    var s3Client = new AmazonS3Client(settings.AWSS3.Key, settings.AWSS3.SecretKey, region);


                    DeleteObjectRequest request = new DeleteObjectRequest
                    {
                        BucketName = settings.AWSS3.BucketName,
                        Key = key
                    };

                    DeleteObjectResponse response = await s3Client.DeleteObjectAsync(request);
                    if (response != null)
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
