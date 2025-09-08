using System.Text;

namespace GAC.Integration.Service.Utils
{
    public class EncryptionService
    {
        public string EncryptPswd(string sPassword)
        {
            using var x = System.Security.Cryptography.SHA256.Create(); 
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
            
        }
    }
}
