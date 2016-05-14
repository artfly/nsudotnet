using System;
using System.IO;

namespace Sartakov.Nsudotnet.LinesCounter
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length != 1)
      {
        Console.WriteLine("Usage : {0} FORMAT");
        return;
      }
      CountLines(args[0]);
    }

    private static void CountLines(string format)
    {
      var counter = 0;
      int c;
      char prev;
      bool charsAppeared;
      bool singleLineComment;
      bool multipleLinesComment;

      foreach (var fileName in Directory.EnumerateFiles(".", format, SearchOption.AllDirectories))
      {
        using (var sr = new StreamReader(fileName))
        {
          prev = '\n';
          charsAppeared = false;
          singleLineComment = false;
          multipleLinesComment = false;
          while ((c = sr.Read()) > 0)
          {
            switch ((char) c)
            {
              case '/':
                if (prev == '/' && !singleLineComment && !multipleLinesComment)
                {
                  if (charsAppeared)
                  {
                    counter++;
                    charsAppeared = false;
                  }
                  singleLineComment = true;
                }
                else if (prev == '*' && !singleLineComment && multipleLinesComment)
                {
                    multipleLinesComment = false;
                }
                break;
              case '*':
                if (prev == '/' && !multipleLinesComment && !singleLineComment)
                {
                  if (charsAppeared)
                  {
                    counter++;
                    charsAppeared = false;
                  }
                  multipleLinesComment = true;
                }
                break;
              case '\n':
                if (charsAppeared && !singleLineComment && !multipleLinesComment)
                {
                  counter++;
                }
                singleLineComment = false;
                charsAppeared = false;
                break;
              default:
                if (!multipleLinesComment && !singleLineComment && (char)c != ' ' && (char)c != '\t')
                {
                  charsAppeared = true;
                }
                break;
          	}
            prev = (char) c;
          }
          if (charsAppeared && !singleLineComment && !multipleLinesComment)
          {
            counter++;
          }
        }
      }
      Console.WriteLine("Total lines count : {0}", counter);
    }
  }
}


