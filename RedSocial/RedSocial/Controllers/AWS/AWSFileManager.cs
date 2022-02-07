using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace RedSocial.Controllers.AWS {
    public class AWSFileManager {
        private static readonly string AccessKey = "AKIA27HTAZSMC5GSG3MF";
        private static readonly string SecretKey = "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";
        private static readonly Amazon.RegionEndpoint Region = Amazon.RegionEndpoint.USEast2;
        private static string bucketName = "spookydevstudio.redsocial";

        private static AmazonS3Client client = new AmazonS3Client(AccessKey, SecretKey, Region);
        private static async void DownloadFile(string fileName, string filePathOut) {
            GetObjectRequest getRequest = new GetObjectRequest {
                BucketName = bucketName,
                Key = fileName
            };
            GetObjectResponse response = await client.GetObjectAsync(getRequest);
            await response.WriteResponseStreamToFileAsync(filePathOut, true, CancellationToken.None);
            
        }
        public static string GetLink(string imageName) => @$"https://s3.us-east-2.amazonaws.com/spookydevstudio.redsocial/{imageName}";

        public static async Task<string> UploadFile(string file) {
            var name = GenerateUUID;
            PutObjectRequest putRequest = new PutObjectRequest {
                BucketName = bucketName,
                Key = name,
                FilePath = file,
                CannedACL = S3CannedACL.PublicRead
            };

            PutObjectResponse response = await client.PutObjectAsync(putRequest);
            return name;
        }
        public static string GenerateUUID => Guid.NewGuid().ToString();
    }
}
