using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper
{

    /// <summary>
    /// This code has been taken from Cryptography algo.
    /// and Not written by me
    /// </summary>
    public class NoCryptoAlgorithm : ICryptoAlgorithm
    {
        public string Encrypt(string plainText, string passPhrase)
        {
            return plainText;
        }

        public string Decrypt(string cipherText, string passPhrase)
        {
            return cipherText;
        }

    }
}
