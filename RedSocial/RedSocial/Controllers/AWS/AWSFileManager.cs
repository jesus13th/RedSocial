using System;
using System.Threading;
using System.Threading.Tasks;

using Amazon.S3;
using Amazon.S3.Model;

namespace RedSocial.Controllers.AWS {
    public class AWSFileManager {
        private static string AccessKey = "";
        private static string SecretKey = "";
        private static string BucketName = "";
        private static Amazon.RegionEndpoint Region = Amazon.RegionEndpoint.USEast2;

        private static AmazonS3Client client;

        public AWSFileManager() {
            AccessKey = App.Credentials.AWS.AccessKey;
            SecretKey = App.Credentials.AWS.SecretKey;
            BucketName = App.Credentials.AWS.BucketName;

            client = new AmazonS3Client(AccessKey, SecretKey, Region);
        }

        private static async void DownloadFile(string fileName, string filePathOut) {
            GetObjectRequest getRequest = new GetObjectRequest {
                BucketName = BucketName,
                Key = fileName
            };
            GetObjectResponse response = await client.GetObjectAsync(getRequest);
            await response.WriteResponseStreamToFileAsync(filePathOut, true, CancellationToken.None);
        }

        public static async Task<string> UploadFile(string file) {
            var name = GenerateUUID;
            PutObjectRequest putRequest = new PutObjectRequest {
                BucketName = BucketName,
                Key = name,
                FilePath = file,
                CannedACL = S3CannedACL.PublicRead
            };

            PutObjectResponse response = await client.PutObjectAsync(putRequest);
            return name;
        }
        public static string GenerateUUID => Guid.NewGuid().ToString();
        public static string GetLink(string imageName) => @$"https://s3.us-east-2.amazonaws.com/spookydevstudio.redsocial/{imageName}";
    }
}
