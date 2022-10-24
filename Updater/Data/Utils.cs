using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CrystalUpdater.Data
{
    public class Utils
    {
        public static string checkMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }

        public static class FileSizeFormatter
        {
            // Load all suffixes in an array  
            static readonly string[] suffixes =
            { "Bytes", "KB", "MB", "GB", "TB", "PB" };
            public static string FormatSize(Int64 bytes)
            {
                int counter = 0;
                decimal number = (decimal)bytes;
                while (Math.Round(number / 1024) >= 1)
                {
                    number = number / 1024;
                    counter++;
                }
                return string.Format("{0:n1}{1}", number, suffixes[counter]);
            }
        }
    }
}
