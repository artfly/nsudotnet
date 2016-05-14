using System;
using System.Security.Cryptography;

namespace Sartakov.Nsudotnet.Enigma
{
  class Program
  {
    private static void ShowHelp()
    {
      Console.WriteLine(
        @"Usage :
		{0} encrypt FILE ALGORITHM OUTPUT_FILE
		{0} decrypt FILE ALGORITHM KEY_FILE OUTPUT_FILE", System.AppDomain.CurrentDomain.FriendlyName
      );
    }

    static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        ShowHelp();
        return;
      }
      if ((args.Length != 4 || args[0] != "encrypt") && (args.Length != 5 || args[0] != "decrypt"))
      {
        ShowHelp();
        return;
      }

      SymmetricAlgorithm algorithm;
      switch (args[2])
      {
        case "aes":
          algorithm = new AesCryptoServiceProvider();
          break;
        case "des":
          algorithm = new DESCryptoServiceProvider();
          break;
        case "rc2":
          algorithm = new RC2CryptoServiceProvider();
          break;
        case "rijndael":
          algorithm = new RijndaelManaged();
          break;
        default:
          ShowHelp();
          return;
      }

      switch (args[0])
      {
        case "encrypt":
          Enigma.Encrypt(args[1], algorithm, args[3]);
          break;
        case "decrypt":
          Enigma.Decrypt(args[1], algorithm, args[4], args[3]);
          break;
      }
    }
  }
}


