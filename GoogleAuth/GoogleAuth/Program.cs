using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Google.Apis.Admin.Reports.reports_v1;
using Google.Apis.Admin.Reports.reports_v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Services;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;

namespace GoogleAuth
{
    class Program
    {
        public static string token1;
        public static string token2;
        static string[] Scopes = { ReportsService.Scope.AdminReportsAuditReadonly, DriveService.Scope.DriveReadonly };
        static void Main(string[] args)
        {
            GetTokenAndCall();
            
            var service = new DriveService(new BaseClientService.Initializer()
            {
                ApplicationName = "Report API Demo",
                HttpClientInitializer = GoogleCredential.FromAccessToken(token1)
            });

            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }

            Console.ReadLine();
        }


        public static void GetTokenAndCall()
        {
            token1 = GoogleServiceAccount.GetAccessTokenFromJSONKey("ivydrive-207109-5d7ca92ee3ff.json", Scopes);
            Console.WriteLine("AccessToken: {0}", token1);
            token2 = GoogleServiceAccount.GetAccessTokenFromJSONKey("IvyDemo-b2c78c0660e4.json", Scopes);
            Console.WriteLine("AccessToken: {0}", token2);
            //Console.WriteLine(new HttpClient().GetStringAsync($"https://www.googleapis.com/plus/v1/people/109956788316344882635?access_token={token1}").Result);
            //Console.WriteLine(new HttpClient().GetStringAsync($"https://www.googleapis.com/plus/v1/people/109956788316344882635?access_token={token2}").Result);
        }
    }
}
