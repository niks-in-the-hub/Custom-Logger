using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Azure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.RetryPolicies;
using Microsoft.Azure.Storage.Shared.Protocol;
using System.Threading;
using NLog;


namespace AzureStorageLogger
{
    public class BlobLogger
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static void ConnectToBlob()
        {
            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse("connectionString");
            }

            
            catch (StorageException e)
            {
                Logger.Error(e, "could not establish connection with blob storage");
                throw;
            }

            finally
            {
                LogManager.Shutdown();
            }

        }
    }
}
