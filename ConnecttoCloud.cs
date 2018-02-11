using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Data.Sql;
using System.Configuration;
namespace CloudDocumentsMoving
{
  public  class ConnecttoCloud
    {
     
   public  static void Main(string[] args)
        {
           string ContainerName = ConfigurationManager.AppSettings["documentsContainer"].ToString();
           string ContainerNamepb = ConfigurationManager.AppSettings["pbdocumentsContainer"].ToString();
           string continerDraftisda = ConfigurationManager.AppSettings["isdareportstorageContainer"].ToString();
           string continerdraftpb = ConfigurationManager.AppSettings["pbreportsstorageContainer"].ToString();
         string AgreementCode = string.Empty;
      

         //int clientid = 0; 
       try{
           string  a;
           CloudStorageAccount account = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());


           CloudBlobClient blobClient = account.CreateCloudBlobClient();
           Console.WriteLine("Enter '1' to move  Executed Isda Documets");
           Console.WriteLine("\n");
           Console.WriteLine("Enter '2' to move  Executed PB   Documets");
           Console.WriteLine("\n");
           Console.WriteLine("Enter '3' to move  Draft    Isda Documets");
           Console.WriteLine("\n");
           Console.WriteLine("Enter '4' to move  Draft    PB   Documets");

           Console.WriteLine("\n");
           Console.WriteLine("Enter Number");

          a = Console.ReadLine();

        switch (a)
           {
               case "1":
                   
            DataTable dt = DataAccess.ISDAAgreement.usp_GetDocumentForCloudStroages();

           Isdamovestroage(blobClient, ContainerName, dt, new string[] { "executed/", "isda/" });
                   break;
               case "2":
                 DataTable dt1 = DataAccess.PBAgreements.Get_PBAllcloudstroagemovedagreements();
           Isdamovestroage(blobClient, ContainerNamepb, dt1, new string[] { "executed/", "pb/" });
                   break;
               case "3":
                   
         DataTable draftisda = DataAccess.Draft_AgreementsISDA.Draft_ISDAGetAgreementscloudstroage();
          draftIsdamovestroage(blobClient, continerDraftisda, draftisda, new string[] { "draft/", "isda/" });
                   break;
               case "4":
                 DataTable dratpb = DataAccess.Draft_AgreementsPB.Draft_PBGetAgreementscloudstroage();
            draftIsdamovestroage(blobClient, continerdraftpb, dratpb, new string[] { "draft/", "pb/" });
                   break;
            default:
                   Console.WriteLine("Invalid Selection");
                   break;

           }

        
           
       }
       catch(Exception ex)
       {
           Console.WriteLine("Error While Running Code");
           Console.WriteLine(ex.StackTrace.ToString());
       }
      

       }

   public static void Isdamovestroage(CloudBlobClient blobClient, string ContainerName,DataTable dt,string[] array)
   {
       Console.WriteLine("Documents Moving Status");
       Console.WriteLine("-----------------------");

       CloudBlobContainer Isdacontainer = blobClient.GetContainerReference(ContainerName);


      
       string cloud = string.Empty;

       if (dt != null && dt.Rows.Count > 0)
       {
           int count = 0;
           Console.WriteLine("\n Processing started................................ please wait.");
           foreach (DataRow item in dt.Rows)
           {
               string Message = string.Empty;

               if (!string.IsNullOrEmpty(item["ClientID"].ToString()))
               {
                   string documentName = string.Empty;
                   string documentst = string.Empty;
                   if (!string.IsNullOrEmpty(item["UniqueID"].ToString()))
                   {
                       documentName = item["ISDADocumentID"].ToString() + "_" + item["UniqueID"].ToString();

                   }
                   else
                   {
                       documentName = item["ISDADocumentID"].ToString();
                   }


                   if (!string.IsNullOrEmpty(documentName))
                   {
                       if (!string.IsNullOrEmpty(item["ClientGuiID"].ToString()))
                       {
                           CloudBlockBlob oldDoc = Isdacontainer.GetBlockBlobReference(documentName);

                           if (oldDoc.Exists())
                           {
                               // client container is exist
                               CloudBlobContainer Clientcontainer = blobClient.GetContainerReference(item["ClientGuiID"].ToString());
                               if (Clientcontainer.Exists())
                               {
                                   // executed container is exist

                                   CreateDirectory(Clientcontainer, documentName, oldDoc, array);

                               }
                               else
                               {
                                   // client conatinere doesnot exist
                                   if (Clientcontainer.CreateIfNotExists())
                                   {
                                       CreateDirectory(Clientcontainer, documentName, oldDoc, array);

                                   }

                               }
                           }
                       }
                   }
               }
               count++;
               
               Message = "\n"+"ID :" + count + " --- ClientID: " + item["ClientID"].ToString()+ " --- AgreementID :" + item["ISDAAgreementID"].ToString() + " --- DocumentID:" + item["ISDADocumentID"].ToString() ;
               Console.WriteLine(Message);

               
           }
           Console.WriteLine("Documents Moving Completed................................");
           Console.ReadLine();
       }
       else
       {
           Console.WriteLine("Documents Not Found");
       }
   }


 
   public static bool moveblob(CloudBlobDirectory _cbd, string _documentname, CloudBlockBlob _oldDoc)
   {

       CloudBlockBlob newBlob = _cbd.GetBlockBlobReference(_documentname);

       if (!newBlob.Exists())
       {
           //moving blob                                               
           newBlob.StartCopyFromBlob(_oldDoc);

       }
       return true;
   }

   public static bool CreateDirectory(CloudBlobContainer Clientcontainer, string documentName, CloudBlockBlob oldDoc, string[] _args)
   {

       CloudBlobDirectory executedirectory = Clientcontainer.GetDirectoryReference(_args[0]);
       if (executedirectory.Container.Exists())
       {

           ChecDirectoryExistandMove(Clientcontainer, documentName, oldDoc, _args[1]);
       }
       else
       {
           //isdadirectory does not exists
           if (executedirectory.Container.CreateIfNotExists())
           {
               ChecDirectoryExistandMove(Clientcontainer, documentName, oldDoc, _args[1]);

           }
       }
       return true;
   
   }

   public static bool ChecDirectoryExistandMove(CloudBlobContainer Clientcontainer, string documentName, CloudBlockBlob oldDoc,string directoryName)
   {
       //Get Refers of Isda Directory
       CloudBlobDirectory IsdaDriectory = Clientcontainer.GetDirectoryReference(directoryName);

       //Move to Existing ISDA Directroy

       if (IsdaDriectory.Container.Exists())
       {
           moveblob(IsdaDriectory, documentName, oldDoc);
       }
       else
       {
           //Create Isda Directory and Move

           if (IsdaDriectory.Container.CreateIfNotExists())
           {
               moveblob(IsdaDriectory, documentName, oldDoc);
           }
       }
       return true;
   
   }

   public static void draftIsdamovestroage(CloudBlobClient blobClient, string ContainerName, DataTable dt, string[] array)
   {
       Console.WriteLine("Draft_Documents Moving Status");
       Console.WriteLine("-----------------------");

       CloudBlobContainer Isdacontainer = blobClient.GetContainerReference(ContainerName);



       string cloud = string.Empty;

       if (dt != null && dt.Rows.Count > 0)
       {
           int count = 0;
           Console.WriteLine("\n Processing started................................ please wait.");
           foreach (DataRow item in dt.Rows)
           {
               string Message = string.Empty;

               if (!string.IsNullOrEmpty(item["ClientID"].ToString()))
               {
                   string documentName = string.Empty;
                   string documentst = string.Empty;
                   if (!string.IsNullOrEmpty(item["UniqueID"].ToString()))
                   {
                       documentName = item["ISDADFDocumentID"].ToString() + "_" + item["UniqueID"].ToString();

                   }
                   else
                   {
                       documentName = item["ISDADFDocumentID"].ToString();
                   }


                   if (!string.IsNullOrEmpty(documentName))
                   {
                       if (!string.IsNullOrEmpty(item["ClientGuiID"].ToString()))
                       {
                           CloudBlockBlob oldDoc = Isdacontainer.GetBlockBlobReference(documentName);

                           if (oldDoc.Exists())
                           {
                               // client container is exist
                               CloudBlobContainer Clientcontainer = blobClient.GetContainerReference(item["ClientGuiID"].ToString());
                               if (Clientcontainer.Exists())
                               {
                                   // executed container is exist

                                   CreateDirectory(Clientcontainer, documentName, oldDoc, array);

                               }
                               else
                               {
                                   // client conatinere doesnot exist
                                   if (Clientcontainer.CreateIfNotExists())
                                   {
                                       CreateDirectory(Clientcontainer, documentName, oldDoc, array);

                                   }

                               }
                           }
                       }
                   }
               }
               count++;

               Message = "\n" + "ID :" + count + " --- ClientID: " + item["ClientID"].ToString() + " --- VersionAgreementID :" + item["VersionAgreementID"].ToString() + " --- ISDADFDocumentID:" + item["ISDADFDocumentID"].ToString();
               Console.WriteLine(Message);


           }
           Console.WriteLine("Draft_Documents Moving Completed................................");
           Console.ReadLine();
       }
       else
       {
           Console.WriteLine("Draft_Documents Not Found");
       }
   }


     
    }
}
