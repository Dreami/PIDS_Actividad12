﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileOutputs;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;

namespace Actividad12
{
    class Program
    {
        private static int wordAmount = 0;
        private static string output_path12 = @"C:\Users\maple\Documents\9° Semester\CS13309_Archivos_HTML\a12_matricula.txt";
        private static Dictionary<String, Repetitions> repetitions;
        static void Main(string[] args)
        {
            DirectoryInfo d = new DirectoryInfo(Outputs.getAllFiles());
            FileInfo[] Files = d.GetFiles("*.txt");

            string output_path12 = @"C:\Users\maple\Documents\9° Semester\CS13309_Archivos_HTML\a12_matricula.txt";
            string output;

            List<string> sortedWords = new List<string>();
            repetitions = new Dictionary<string, Repetitions>();

            Console.WriteLine("Processing all tokens, please wait a moment...");
            var processingTime = System.Diagnostics.Stopwatch.StartNew();
            foreach (FileInfo file in Files)
            {

                string htmlContent = File.ReadAllText(file.FullName);
                htmlContent.Trim();

                string[] eachWord = htmlContent.Split(' ');

                try
                {
                    foreach (string w in eachWord)
                    {
                        if (!string.IsNullOrEmpty(w))
                        {
                            if (!w.Equals(" "))
                            {
                                string word = w.ToLower();
                                word.Replace(",", "")
                                    .Replace(".", "")
                                    .Replace("\r", "")
                                    .Replace("\t", "")
                                    .Replace("\n", "")
                                    .Replace("(", "")
                                    .Replace(")", "");

                                if (repetitions.ContainsKey(word))
                                {
                                    repetitions[word].addWord(file.Name);
                                }
                                else
                                {
                                    repetitions.Add(word, new Repetitions(word, file.Name));
                                }
                            }
                        }
                    }

                }
                catch (ArgumentNullException argExc)
                {
                    Console.WriteLine(argExc.StackTrace);
                }
                catch (KeyNotFoundException keyNotFoundExc)
                {
                    Console.WriteLine(keyNotFoundExc.StackTrace);
                }
            }
            processingTime.Stop();
            Console.WriteLine("Finished processing in " + processingTime.ElapsedMilliseconds +
                "\nYou may search tokens with 'retrieve' followed by the word you want, e.g. 'retrieve gato'" +
                "\nY 'stop' to stop the program.");
            string[] input = Console.ReadLine().Split(' ');

            while (!input[0].Equals("stop"))
            {
                if (input[0].Equals("retrieve")) {
                   

                    for (int i = 1; i < input.Length; i++)
                    {
                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        runSearch(input[i]);
                        watch.Stop();
                        output = "All '" + input[i] + "'(" + wordAmount + ") found in " + watch.Elapsed.TotalMilliseconds.ToString() + " ms";
                        Console.WriteLine(output);
                        Outputs.output_print(output_path12, output);
                    }
                } else
                {
                    Console.WriteLine("'{0}' unknown command", input[0]);
                }
                Console.Write("Search: ");
                input = Console.ReadLine().Split(' ');
            }
            Console.Read();
        }

        private static void runSearch(string input)
        {
            if (repetitions.Keys.ToList().Contains(input))
            {
                string output;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                
                wordAmount = repetitions[input].getRepeatsCount();
                HashSet<string> files = repetitions[input].getFilesFoundIn();
                output = "Found in: \n";
                Console.WriteLine(output);
                Outputs.output_print(output_path12, output);
                foreach (string filename in files)
                {
                    output = filename;
                    Console.WriteLine(output);
                    Outputs.output_print(output_path12, output);
                }
                watch.Stop();
            } else
            {
                wordAmount = 0;
            }
        }
    }
}
