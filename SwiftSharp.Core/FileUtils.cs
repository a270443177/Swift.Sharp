// ---------------------------------------------------------------------------
// <copyright file="FileUtils.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 9-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Core
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Various utilities for on file operation
    /// </summary>
    internal class FileUtils
    {
        /// <summary>
        /// Generate MD5 hash of file content
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>String representation of MD5 hash of file content</returns>
        internal static string GenerateMD5Hash(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    //return md5.ComputeHash(stream);
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

        /// <summary>
        /// Normalizes the name of the file so it could be safety uploaded
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File name converted so it could be safety uploaded </returns>
        internal static string NormalizeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName", "File name could not be empty");
            }

            string normalFileName = Path.GetFileName(fileName);

            while (normalFileName.Contains("."))
            {
                normalFileName = normalFileName.Replace(".", "_");
            }

            return normalFileName;
        }

        /// <summary>
        /// Convert file content to base64 string
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Content of file as base64 string</returns>
        internal static string FileContent(string fileName)
        {
            using (var content = File.OpenRead(fileName))
            {
                using (BinaryReader reader = new BinaryReader(content))
                {
                    byte[] binaryContent = new byte[(int)content.Length];
                    reader.Read(binaryContent, 0, (int)content.Length);

                    return Convert.ToBase64String(binaryContent);
                }
            }
        }
    }
}
