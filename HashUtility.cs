using System;
using System.IO;
using System.Security.Cryptography;

public static class HashUtility
{
    public static string ComputeFileMd5Hash(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentNullException(nameof(path));
        }
        
        using (var md5 = MD5.Create())
        using (var stream = File.OpenRead(path))
        {
            return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
        }
    }
}