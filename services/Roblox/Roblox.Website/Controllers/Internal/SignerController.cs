using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Roblox.Website.Controllers.Internal
{
    public class SignatureController : ControllerBase
    {
        private static RSACryptoServiceProvider? _rsaCsp;
        private static SHA1? _shaCsp;
        private static readonly string format = "--rbxsig%{0}%{1}";
        private static readonly string newLine = "\r\n";
        
        public static void Setup()
        {
            try
            {
                byte[] privateKeyBlob = Convert.FromBase64String(System.IO.File.ReadAllText("PrivateKeyBlob.txt"));
                
                _shaCsp = SHA1.Create();
                _rsaCsp = new RSACryptoServiceProvider();
                
                _rsaCsp.ImportCspBlob(privateKeyBlob);
            }
            catch (Exception ex)
            {
                throw new Exception("Error setting up SignatureController: " + ex.Message);
            }
        }

        public static string SignJsonResponseForClientFromPrivateKey(dynamic JSONToSign)
        {
            string json = JsonConvert.SerializeObject(JSONToSign);
            string script = Environment.NewLine + json;
            byte[] signature = _rsaCsp!.SignData(Encoding.Default.GetBytes(script), _shaCsp!);

            return String.Format(format, Convert.ToBase64String(signature), script);
        }

        public static string SignStringResponseForClientFromPrivateKey(string stringToSign, bool bUseRbxSig = false)
        {
            if (bUseRbxSig)
            {
                string script = newLine + stringToSign;
                byte[] signature = _rsaCsp!.SignData(Encoding.Default.GetBytes(script), SHA1.Create());

                return string.Format(format, Convert.ToBase64String(signature), script);
            }
            else
            {
                byte[] signature = _rsaCsp!.SignData(Encoding.Default.GetBytes(stringToSign), SHA1.Create());
                return Convert.ToBase64String(signature);
            }
        }
    }
}