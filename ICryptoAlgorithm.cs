using System;

public interface ICryptoAlgorithm
{
    string Encrypt(string plainText, string passPhrase);
    string Decrypt(string cipherText, string passPhrase);
}
