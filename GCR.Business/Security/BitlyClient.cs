using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GCR.Business.Security
{
    internal sealed class BitlyClient : OAuth2Client
    {
        private readonly string appId;
        private readonly string appSecret;
        private static MethodInfo urlEncoder;
        private const string OAuthEndpoint = "https://bitly.com/";
        private const string ApiEndpoint = "https://api-ssl.bitly.com/";
        private const string AuthorizationEndpoint = "oauth/authorize";
        private const string TokenEndpoint = "oauth/access_token";
        private const string UserInfoEndpoint = "v3/user/info";

        static BitlyClient()
        {
            urlEncoder = typeof(MessagingUtilities).GetMethod("EscapeUriDataStringRfc3986", BindingFlags.NonPublic | BindingFlags.Static);
        }
        
        public BitlyClient(string appId, string appSecret)
            : this("bitly", appId, appSecret)
        { }
    
        protected BitlyClient(string providerName, string appId, string appSecret)
            : base(providerName)
        {
            this.appId = appId;
            this.appSecret = appSecret;
        }

        protected override Uri GetServiceLoginUrl(Uri returnUrl)
        {
            UriBuilder builder = new UriBuilder(OAuthEndpoint + AuthorizationEndpoint);
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("client_id", this.appId);
            args.Add("redirect_uri", returnUrl.AbsoluteUri);
            builder.Query = GetQueryString(args);
            return builder.Uri;

        }

        protected override IDictionary<string, string> GetUserData(string accessToken)
        {
            UriBuilder builder = new UriBuilder(ApiEndpoint + UserInfoEndpoint);
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("access_token", accessToken);
            builder.Query = GetQueryString(args);

            dynamic response;
            using (WebResponse wr = WebRequest.Create(builder.Uri).GetResponse())
            {
                using (Stream stream = wr.GetResponseStream())
                {
                    response = DeserializeStream<ExpandoObject>(stream);
                }
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            AddItemIfNotEmpty(dictionary, "id", response.data.apiKey);
            AddItemIfNotEmpty(dictionary, "username", response.data.login);
            AddItemIfNotEmpty(dictionary, "name", response.data.full_name);
            return dictionary;

        }

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
        {
            UriBuilder builder = new UriBuilder();
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("client_id", this.appId);
            args.Add("redirect_uri", returnUrl.AbsoluteUri);
            args.Add("client_secret", this.appSecret);
            args.Add("code", authorizationCode);
            string query = GetQueryString(args);

            WebRequest request = WebRequest.Create(ApiEndpoint + TokenEndpoint);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = query.Length;
            request.Method = "POST";
            using (Stream stream = request.GetRequestStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(query);
                writer.Flush();
            }

            using (WebResponse wr = request.GetResponse())
            {
                using (var stream = wr.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var response = HttpUtility.ParseQueryString(reader.ReadToEnd());
                    if (response != null)
                    {
                        return response["access_token"];
                    }
                }
            }
            return null;
        }

        private static void AddItemIfNotEmpty(IDictionary<string, string> dictionary, string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (!string.IsNullOrEmpty(value))
            {
                dictionary[key] = value;
            }
        }

        private static T DeserializeStream<T>(Stream stream)
        {
            var reader = new StreamReader(stream);
            var JsonReader = new JsonTextReader(reader);

            if (!JsonReader.Read())
            {
                return default(T);
            }

            JsonSerializer JsonSerializer = new JsonSerializer();
            JsonSerializer.Converters.Add(new ExpandoObjectConverter());
            return JsonSerializer.Deserialize<T>(JsonReader);
        }

        private static string GetQueryString(IDictionary<string, string> args)
        {
            if ((args != null) && args.Count > 0)
            {
                StringBuilder builder = new StringBuilder(50 + args.Count * 10);
                foreach (var pair in args)
                {
                    builder.Append(UrlEncode(pair.Key));
                    builder.Append('=');
                    builder.Append(UrlEncode(pair.Value));
                    builder.Append('&');
                }
                builder.Length--;

              return builder.ToString();
            }
            return String.Empty;
        }
        private static string UrlEncode(string value)
        {
            return (string)urlEncoder.Invoke(null, new object[] { value });
        }
    }
}
