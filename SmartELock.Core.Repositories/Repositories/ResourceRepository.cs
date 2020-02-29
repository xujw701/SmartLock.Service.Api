using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Enums;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly string PortraitsContainer = "portraits";
        private readonly string PropertyContainer = "properties";

        private readonly IDbRetryHandler _dbRetryHandler;

        public ResourceRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        #region Potraits

        public async Task<int> AddPortrait(string url)
        {
            var portraitId = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("ResPortrait_Add", new
                {
                    url
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return portraitId;
        }

        public async Task<bool> UpdatePortrait(int resPortraitId, string url)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("ResPortrait_Update", new
                {
                    resPortraitId,
                    url
                });
            });

            return result > 0;
        }

        public async Task<string> GetPortrait(int resPortraitId)
        {
            var url = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("ResPortrait_Get", new
                {
                    resPortraitId
                }))
                {
                    var portraitSnapshot = reader.Read<ResPortraitSnapshot>().ToList();

                    return portraitSnapshot.Select(snapshot => snapshot.Url).FirstOrDefault();
                }
            });

            return url;
        }

        #endregion

        #region Property

        public async Task<int> AddPropertyResource(int propertyId, string url)
        {
            var resPropertyId = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("ResProperty_Add", new
                {
                    propertyId,
                    url
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return resPropertyId;
        }

        public async Task<List<ResProperty>> GetResPropertyList(int propertyId)
        {
            var resPropertyList = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("ResProperty_GetByPropertyId", new
                {
                    propertyId
                }))
                {
                    var snapshots = reader.Read<ResPropertySnapshot>().ToList();

                    return snapshots.Select(snapshot => ResProperty.CreateFrom(snapshot)).ToList();
                }
            });

            return resPropertyList;
        }

        public async Task<string> GetResProperty(int resPropertyId)
        {
            var url = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("ResProperty_Get", new
                {
                    resPropertyId
                }))
                {
                    var snapshots = reader.Read<ResPropertySnapshot>().ToList();

                    return snapshots.Select(snapshot => snapshot.Url).FirstOrDefault();
                }
            });

            return url;
        }

        public async Task<bool> UpdateResProperty(int resPropertyId, string url)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("ResProperty_Update", new
                {
                    resPropertyId,
                    url
                });
            });

            return result > 0;
        }

        public async Task<bool> DeleteResProperty(int resPropertyId)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("ResProperty_Delete", new
                {
                    resPropertyId
                });
            });

            return result > 0;
        }

        #endregion


        #region Azure Storage

        public async Task<string> SaveBlob(byte[] bytes, FileType fileType, ResourceType resourceType)
        {
            var cloudBlobContainer = GetCloudBlobContainer(resourceType);
            var filePath = GenerateFilePath(fileType);
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filePath);
            await cloudBlockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
            return cloudBlockBlob.Uri.ToString();
        }

        public async Task<byte[]> LoadBlob(string url, ResourceType resourceType)
        {
            var blobName = url.Split('/').LastOrDefault();
            var normalizedBlobName = blobName.Replace("%5C", "/");

            var cloudBlobContainer = GetCloudBlobContainer(resourceType);
            var cloudBlockBlob = cloudBlobContainer.GetBlobReference(normalizedBlobName);

            using (var memoryStream = new MemoryStream())
            {
                await cloudBlockBlob.DownloadToStreamAsync(memoryStream, null, null, null);
                byte[] array = memoryStream.ToArray();

                return array;
            }
        }

        public async Task<bool> DeleteBlob(string url, ResourceType resourceType)
        {
            var blobName = url.Split('/').LastOrDefault();
            var normalizedBlobName = blobName.Replace("%5C", "/");

            var cloudBlobContainer = GetCloudBlobContainer(resourceType);
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(normalizedBlobName);
            return await cloudBlockBlob.DeleteIfExistsAsync();
        }

        private CloudBlobContainer GetCloudBlobContainer(ResourceType resourceType)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SmartELockStorage"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            return GetContainer(cloudBlobClient, resourceType);
        }

        private string GenerateFilePath(FileType type)
        {
            var year = DateTime.UtcNow.Year.ToString();
            var month = DateTime.UtcNow.Month.ToString();

            var fileName = Guid.NewGuid().ToString();

            switch (type)
            {
                case FileType.Jpeg:
                    fileName += ".jpg";
                    break;
                case FileType.Png:
                    fileName += ".png";
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Path.Combine(year, month, fileName);
        }

        private CloudBlobContainer GetContainer(CloudBlobClient client, ResourceType resourceType)
        {
            var cloudBlobContainer = client.GetContainerReference(GetContainerName(resourceType));

            if (cloudBlobContainer.Exists())
            {
                return cloudBlobContainer;
            }

            cloudBlobContainer.Create();

            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            };

            cloudBlobContainer.SetPermissions(permissions);

            return cloudBlobContainer;
        }

        private string GetContainerName(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Portrait:
                    return PortraitsContainer;
                case ResourceType.Property:
                    return PropertyContainer;
                default:
                    return "";
            }
        }

        #endregion
    }
}