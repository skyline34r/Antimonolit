using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Antimonolith.Services.Models;
using Newtonsoft.Json;

namespace Models
{
    public static class JsonWebToken
    {
        public static string Encode(PayloadModel payload, string key)
        {
            var segments = new List<string>();
            var header = new { alg = "HS256", typ = "JWT", kid = 0 };

            byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
            byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload, Formatting.None));

            segments.Add(Base64UrlEncode(headerBytes));
            segments.Add(Base64UrlEncode(payloadBytes));

            var stringToSign = string.Join(".", segments);

            segments.Add(getSignature(stringToSign, key));

            return string.Join(".", segments);
        }

        public static bool IsCorrectToken(string token, string key)
        {
            var segments = token.Split('.');

            var stringToSign = string.Join(".", new[] { segments[0], segments[1] });

            var signature = getSignature(stringToSign, key);

            var signIsCorrect = signature == segments[2];

            var payload = JsonConvert.DeserializeObject<PayloadModel>(Encoding.UTF8.GetString(Convert.FromBase64String(segments[1])));

            var isExpired = DateTimeOffset.Parse(payload.ExpirationDate) <= DateTimeOffset.Now;

            return signIsCorrect && !isExpired;
        }

        public static object Decode(string token, string key)
        {
            var segments = token.Split('.');

            var payload = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(Convert.FromBase64String(segments[1])));

            return payload;
        }

        private static string getSignature(string stringToSign, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);
            SHA256Managed hash = new SHA256Managed();
            byte[] signingBytes = hash.ComputeHash(keyBytes);

            var sha = new HMACSHA256(signingBytes);
            byte[] signature = sha.ComputeHash(bytesToSign);

            return Base64UrlEncode(signature);
        } 

        // from JWT spec
        private static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            //output = output.Split('=')[0]; // Remove any trailing '='s
            //output = output.Replace('+', '-'); // 62nd char of encoding
            //output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }
    }
}
