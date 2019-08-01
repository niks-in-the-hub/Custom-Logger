using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureStorageLogger;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            connString = AzureStorageLogger.BlobLogger.ConnectToBlob();
            string localFolder = ConfigurationManager.AppSettings["testing"];
            string destContainer = ConfigurationManager.AppSettings["functiontest"];


            //Console.WriteLine(connString);
            //Console.WriteLine(localFolder);
            //Console.WriteLine(destContainer);

            //Console.ReadLine();

            //Get a reference to the storage account
            Console.WriteLine(@"Connecting to storage account");
            CloudStorageAccount sa = CloudStorageAccount.Parse(connString);
            CloudBlobClient bc = sa.CreateCloudBlobClient();

            //Get a reference to the container (creating it if necessary)
            Console.WriteLine(@"Getting reference to container");
            CloudBlobContainer container = bc.GetContainerReference(destContainer);

            //Create this container if it doesn't exist
            container.CreateIfNotExists();

            //Upload files
            string[] fileEntries = Directory.GetFiles(localFolder);


            foreach (string filePath in fileEntries)
            {
                //Get the date to use with the key
                string key = DateTime.UtcNow.ToString("yyy-MM-dd-HH:mm:ss") + "-" + Path.GetFileName(filePath);

                UploadBlob(container, key, filePath, true);
            }
            Console.WriteLine(@"Upload processing complete. Press any key to exit.");
            Console.ReadKey();
        }

        static void UploadBlob(CloudBlobContainer container, string key, string fileName, bool deleteAfter)
        {
            Console.WriteLine(@"Uploading file to container: key=" + key + " source file = " + fileName);

            //Get a blob reference to write this file to
            CloudBlockBlob b = container.GetBlockBlobReference(key);

            //Write the file
            using (var fs = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                b.UploadFromStream(fs);
            }

            //If delete of file is requested, then do that
            if (deleteAfter)
                File.Delete(fileName);
        }
    }
    }
}
