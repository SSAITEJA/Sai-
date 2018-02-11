using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DataAccess
{
    public   class ConnecttoCloudStorage
    {
        public static void UploadFiletoCloud(string _storageConnectionString, string _fileName, string _containerName, string _extension, MemoryStream _stream)
        {
            try
            {

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);


                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference(_containerName);

                if (container != null)
                {
                    if (!string.IsNullOrEmpty(_fileName))
                    {
                        CloudBlockBlob blockBlob = container.GetBlockBlobReference(_fileName + _extension);

                        if (_extension == ".html")
                        {
                            blockBlob.Properties.ContentType = "text/html";

                        }


                        if (blockBlob.Exists())
                        {
                            // Delete the blob.
                            blockBlob.Delete();

                         
                          

                            //Move the pointer to the start of stream.
                            _stream.Position = 0;

                            // Create or overwrite the "myblob" blob with contents from a local file.
                            using (var fileStream = _stream)
                            {

                                blockBlob.UploadFromStream(fileStream);
                            }

                            // Create or overwrite the "myblob" blob with contents from a local file.


                        }
                        else
                        {
                            // Create or overwrite the "myblob" blob with contents from a local file.
                            _stream.Position = 0;

                            // Create or overwrite the "myblob" blob with contents from a local file.
                            using (var fileStream = _stream)
                            {
                                blockBlob.UploadFromStream(fileStream);
                            }

                        }


                        if (_extension == ".xlsx")
                        {
                            CloudBlockBlob xmlAgreementsBlob = container.GetBlockBlobReference(_fileName + "_A" + ".xml");
                            if (xmlAgreementsBlob.Exists())
                            {
                                xmlAgreementsBlob.Delete();
                            }
                            CloudBlockBlob xmlAnswersBlob = container.GetBlockBlobReference(_fileName + ".xml");
                            if (xmlAnswersBlob.Exists())
                            {
                                xmlAnswersBlob.Delete();
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                //  Clients.Caller.sendMessage("Error");
                // DataAccess.ISDAAgreement.MailDocuTrackerErrors(new StringBuilder(ex.Message));

            }

        }

        public static string GenerateBlobUrl(string _storageConnectionString, string _fileName, string _containerName, string _extension)
        {
            try
            {

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);


                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference(_containerName);
               
                if (container != null)
                {
                    if (!string.IsNullOrEmpty(_fileName))
                    {
                        CloudBlockBlob blockBlob = container.GetBlockBlobReference(_fileName + _extension);


                        if (blockBlob.Exists())
                        {
                            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
                            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1);
                            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

                            //Generate the shared access signature on the blob, setting the constraints directly on the signature.
                            string sasBlobToken = blockBlob.GetSharedAccessSignature(sasConstraints);

                            //Return the URI string for the container, including the SAS token.
                            return blockBlob.Uri + sasBlobToken;

                        }
                        else
                        {
                            return "";
                        }

                      

                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "";
                //  Clients.Caller.sendMessage("Error");
                // DataAccess.ISDAAgreement.MailDocuTrackerErrors(new StringBuilder(ex.Message));

            }

        }
          
        public static StreamReader ReadFilefromCloud(string _storageConnectionString, string _fileName, string _containerName)
        {
            try
            {

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference(_containerName);

                if (container != null)
                {
                    if (!string.IsNullOrEmpty(_fileName))
                    {


                        CloudBlockBlob blockBlob = container.GetBlockBlobReference(_fileName);
                        if (blockBlob.Exists())
                        {
                            StreamReader reader = new StreamReader(blockBlob.OpenRead());

                            return reader;

                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static bool CreateAnswerXML(int agrementid, string _fileName, string _storageConnectionString, string _container)
        {

            try
            {
                XmlDocument doc = new XmlDocument();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KoyaConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand(@"usp_GetISDAAgreementAnswers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@AgreementID", SqlDbType.Int, 0, "AgreementID"));

                    cmd.Parameters[0].Value = agrementid;

                    conn.Open();
                    doc.Load(cmd.ExecuteXmlReader());
                    conn.Close();

                    if (doc.HasChildNodes)
                    {
                        MemoryStream xmlStream = new MemoryStream();
                        doc.Save(xmlStream);
                        if (xmlStream != null)
                        {
                            ConnecttoCloudStorage.UploadFiletoCloud(_storageConnectionString, _fileName, _container, ".xml", xmlStream);
                            return true;
                        }
                        else
                        {
                            return false;

                        }

                    }
                    else
                    {
                        return false;
                    
                    }
                 
                  
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }


        //newly create by mani because of Template single report
        public static bool CreateAnswerXML_Template(int agrementid, string _fileName, string _storageConnectionString, string _container)
        {

            try
            {
                XmlDocument doc = new XmlDocument();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KoyaConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand(@"usp_GetISDAAgreementAnswers_Template", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TemplateAgreementID", SqlDbType.Int, 0, "TemplateAgreementID"));

                    cmd.Parameters[0].Value = agrementid;

                    conn.Open();
                    doc.Load(cmd.ExecuteXmlReader());
                    conn.Close();

                    if (doc.HasChildNodes)
                    {
                        MemoryStream xmlStream = new MemoryStream();
                        doc.Save(xmlStream);
                        if (xmlStream != null)
                        {
                            ConnecttoCloudStorage.UploadFiletoCloud(_storageConnectionString, _fileName, _container, ".xml", xmlStream);
                            return true;
                        }
                        else
                        {
                            return false;

                        }

                    }
                    else
                    {
                        return false;

                    }


                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }


        public static bool CreateAnswerXML_Draft(int agrementid, string _fileName, string _storageConnectionString, string _container)
        {

            try
            {
                XmlDocument doc = new XmlDocument();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KoyaConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand(@"usp_GetISDAAgreementAnswers_Draft", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@AgreementID", SqlDbType.Int, 0, "AgreementID"));

                    cmd.Parameters[0].Value = agrementid;

                    conn.Open();
                    doc.Load(cmd.ExecuteXmlReader());
                    conn.Close();

                    if (doc.HasChildNodes)
                    {
                        MemoryStream xmlStream = new MemoryStream();
                        doc.Save(xmlStream);
                        if (xmlStream != null)
                        {
                            ConnecttoCloudStorage.UploadFiletoCloud(_storageConnectionString, _fileName, _container, ".xml", xmlStream);
                            return true;
                        }
                        else
                        {
                            return false;

                        }

                    }
                    else
                    {
                        return false;

                    }


                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}