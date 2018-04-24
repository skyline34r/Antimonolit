using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Antimonolith.Services
{
    public static class RSAService
    {
        static RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024);
        public static void rsa()
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            //Create byte arrays to hold original, encrypted, and decrypted data.
            byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");
            byte[] encryptedData;
            byte[] decryptedData;

            //Create a new instance of RSACryptoServiceProvider to generate
            //public and private key data.
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024))
            {

                var r = RSA.ToXmlString();
                var rrr = RSA.ToXmlString();
            }
                //Pass the data to ENCRYPT, the public key information 
                //(using RSACryptoServiceProvider.ExportParameters(false),
                //and a boolean flag specifying no OA padding.

            var s1 = RSA.ExportParameters(false);
            var s = JsonConvert.SerializeObject(s1);
            var ep = JsonConvert.DeserializeObject<RSAParameters>(s);
            //RSA.ExportParameters(false)
            encryptedData = RSAEncrypt(dataToEncrypt, ep, false);

            //Pass the data to DECRYPT, the private key information 
            //(using RSACryptoServiceProvider.ExportParameters(true),
            //and a boolean flag specifying no OAEP padding.
            //decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

            //var x = Convert.ToBase64String(RSA.ExportParameters(true).Modulus);

            //var e = ByteConverter.GetString(encryptedData);
            //var d = ByteConverter.GetString(decryptedData);
            //Display the decrypted plaintext to the console. 
            //Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
            //}

            //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024))
            //{

            //Pass the data to ENCRYPT, the public key information 
            //(using RSACryptoServiceProvider.ExportParameters(false),
            //and a boolean flag specifying no OAEP padding.
            //encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

            //Pass the data to DECRYPT, the private key information 
            //(using RSACryptoServiceProvider.ExportParameters(true),
            //and a boolean flag specifying no OAEP padding.

            var j1 = RSA.ExportParameters(true);
            var j = JsonConvert.SerializeObject(j1);
            var ep2 = JsonConvert.DeserializeObject<RSAParameters>(j);
            decryptedData = RSADecrypt(encryptedData, ep2, false);

            //var x = Convert.ToBase64String(RSA.ExportParameters(true).Modulus);

            var e = ByteConverter.GetString(encryptedData);
                var d = ByteConverter.GetString(decryptedData);
                //Display the decrypted plaintext to the console. 
                Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
            //}
        }


        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
    }
}
