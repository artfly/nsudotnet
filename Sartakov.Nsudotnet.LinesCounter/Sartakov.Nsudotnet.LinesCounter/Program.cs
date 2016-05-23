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
            bool inComment = false;
            bool hasCode = false;
            string line;

            foreach (var fileName in Directory.EnumerateFiles(".", format, SearchOption.AllDirectories))
            {
                using (var sr = new StreamReader(fileName))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        hasCode = false;
                        line = line.Trim();
                        if (String.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        for (int i = 1; i < line.Length; i++)
                        {
                            if (!inComment)
                            {

                                if (line[i - 1] == '/' && line[i] == '/')
                                {
                                    if (i != 1)
                                    {
                                        hasCode = true;
                                    }
                                    break;
                                }
                                else if (line[i - 1] == '/' && line[i] == '*')
                                {
                                    inComment = true;
                                    if (i != 1)
                                    {
                                        hasCode = true;
                                    }
                                    i++;
                                }
                                else if (line[i] != ' ')
                                {
                                    hasCode = true;
                                }
                            }
                            else
                            {
                                if (line[i - 1] == '*' && line[i] == '/')
                                {
                                    inComment = false;
                                    i++;
                                }
                            }
                        }

                        if (hasCode || line.Length == 1 && line[0] != ' ')
                        {
                            counter++;
                        }
                    }
                }
            }
            Console.WriteLine("Total lines count : {0}", counter);
        }
    }
}