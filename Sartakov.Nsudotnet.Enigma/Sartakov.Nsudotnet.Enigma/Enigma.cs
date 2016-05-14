using System;
using System.IO;
using System.Security.Cryptography;

namespace Sartakov.Nsudotnet.Enigma
{
  public class Enigma
  {

    public static void Encrypt(string inName, SymmetricAlgorithm algorithm, string outName)
    {
      try
      {
        using (var fin = new FileStream(inName, FileMode.Open, FileAccess.Read))
        {
          var fout = new FileStream(outName, FileMode.Create, FileAccess.Write);
          using (var cryptoStream = new CryptoStream(fout, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
          {
            CopyStream(fin, cryptoStream);
          }
        }
      }
      catch (IOException e)
      {
        Console.WriteLine(e.Message);
        return;
      }

      string[] base64Repr = {Convert.ToBase64String(algorithm.IV), Convert.ToBase64String(algorithm.Key)};
      var keyName = inName.EndsWith(".txt") ? inName.Insert(outName.Length - 4, ".key") : string.Format("{0}.key.txt", inName);
      Console.WriteLine(keyName);
      using (var keyFile = new StreamWriter(keyName))
      {
        keyFile.WriteLine(base64Repr[0]);
        keyFile.WriteLine(base64Repr[1]);
      }
      Console.WriteLine("Encrypted!");
    }

    public static void Decrypt(string inName, SymmetricAlgorithm algorithm, string outName, string keyName)
    {
      try
      {
        using (var keyFile = new StreamReader(keyName))
        {
          algorithm.IV = Convert.FromBase64String(keyFile.ReadLine());
          algorithm.Key = Convert.FromBase64String(keyFile.ReadLine());
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return;
      }

      try
      {
        using (var fin = new FileStream(inName, FileMode.Open, FileAccess.Read))
        {
          var fout = new FileStream(outName, FileMode.Create, FileAccess.Write);
          using (var cryptoStream = new CryptoStream(fout, algorithm.CreateDecryptor(), CryptoStreamMode.Write))
          {
            CopyStream(fin, cryptoStream);
          }
        }
      }
      catch (IOException e)
      {
        Console.WriteLine(e.Message);
        return;
      }
      Console.WriteLine("Decrypted!");
    }

      private static void CopyStream(Stream fin, Stream fout)
      {
          var buffer = new byte[256];
          long written = 0;
          var total = fin.Length;
          while (written < total)
          {
            var curlen = fin.Read(buffer, 0, 256);
            fout.Write(buffer, 0, curlen);
            written += curlen;
          }
      }
  }
}