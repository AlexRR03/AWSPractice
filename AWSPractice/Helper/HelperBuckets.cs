using Amazon.S3;
using Amazon.S3.Model;
using System.Net;

namespace AWSPractice.Helper
{
    public  class HelperBuckets
    {
        private string BucketName;
        private IAmazonS3 S3Client;
        public HelperBuckets(IConfiguration configuration, IAmazonS3 S3Client)
        {
            this.BucketName = configuration.GetValue<string>("Aws:BucketName");
            this.S3Client = S3Client;
        }

        public async Task<bool> UploadFileAsync(string filename, Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest {
                Key = filename,
                BucketName = this.BucketName,
                InputStream = stream,
            };
            PutObjectResponse response = await this.S3Client.PutObjectAsync(request);
            if(response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteFileAsync(string filename)
        {
            DeleteObjectResponse response = await this.S3Client.DeleteObjectAsync(this.BucketName, filename);
            if (response.HttpStatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<string>> GetVersionsFileAsync()
        {
            ListVersionsResponse response = await this.S3Client.ListVersionsAsync(this.BucketName);
            List<string> fileNames = response.Versions
                .Select(v => v.Key)
                .ToList();
            return fileNames;
        }
    }
}
