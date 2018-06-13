using System;
using System.Net.Http;

namespace GoogleAuth
{
    class Program
    {
        static void Main(string[] args)
        {
            GetTokenAndCall();
            Console.ReadLine();
        }


        public static void GetTokenAndCall()
        {
            var token = GoogleServiceAccount.GetAccessTokenFromJSONKey(
                "ivydrive-207109-5d7ca92ee3ff.json",
                "https://www.googleapis.com/auth/userinfo.profile");
            Console.WriteLine("AccessToken: {0}",token);
            Console.WriteLine(new HttpClient().GetStringAsync($"https://www.googleapis.com/plus/v1/people/109956788316344882635?access_token={token}").Result);
        }
    }
}
