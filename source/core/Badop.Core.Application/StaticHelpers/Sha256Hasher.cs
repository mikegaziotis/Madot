using System.Security.Cryptography;
using System.Text;

namespace Badop.Core.Application.StaticHelpers;

public static class Sha256Hasher
{
    public static String Hash(String value) 
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256.Create()) {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }
        return Sb.ToString();
    }
}