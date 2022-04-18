using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace Sequence
{
    class Program
    {

        private static Dictionary<string, string> strings = new Dictionary<string, string>();
        private static Dictionary<string, int> integers = new Dictionary<string, int>();
        private static Dictionary<string, bool> booleans = new Dictionary<string, bool>();
        private static Dictionary<string, string> tuples = new Dictionary<string, string>();
        private static Dictionary<string, List<string>> lists = new Dictionary<string, List<string>>();

        public static string pathToFile = "";

        public static string varname;
        public static string varValue;

        static void Main(string[] args)
        {
            Console.WriteLine(Convert.ToString(args));

            for (int i = 0; i < args.Length; i++)
            {
                //Console.WriteLine(args[i]);
                pathToFile = args[i];


            }

            if (Convert.ToString(args[0]) == "System.String[]")
            {
                //Like Python cmd line.
                consoleCommand();
            } else
            {
               

                for (int i = 0; i < args.Length; i++)
                {
                    //Console.WriteLine(args[i]);
                    pathToFile = args[i];
                    

                }

                IEnumerable<string> lines = File.ReadLines(pathToFile);
                //Console.WriteLine(String.Join(Environment.NewLine, lines));

                foreach (var line in lines)
                {
                    //Console.WriteLine(line);
                    Parse(line);
                }

                //foreach (var line in text)
                //{
                //    Console.WriteLine(Convert.ToString(line));
                //}
            }

        }

        public static void consoleCommand()
        {
            Console.WriteLine("Sequence C-B-L (Compile-By-Line) v0.1 >>>>>>>>>>");
            string line = Console.ReadLine();

            if (line.StartsWith("print\"") || line.StartsWith("print \""))
            {
                string stage1 = line.Replace("print", "").Trim();
                // Where it is now: ex: Started with: write "hello!" now is: "hello!"
                try
                {
                    int lastIndexOfStage1 = stage1.Length;
                    lastIndexOfStage1 = lastIndexOfStage1 - 1;
                    //Console.WriteLine("Length of statement: " + lastIndexOfStage1);
                    //Console.WriteLine("Last index of \" is: " + stage1.LastIndexOf("\""));


                    if (stage1.LastIndexOf("\"") != lastIndexOfStage1)
                    {
                        Console.WriteLine(Constants.vbNewLine + "Sequence: Error: Couldn't parse 'print' " + stage1 + " because missing \".");
                        consoleCommand();
                    }
                    else
                    {
                        if (stage1.Contains("\""))
                        {
                            string stage2 = stage1.Replace("\"", "");
                            Console.WriteLine(stage2.Trim());
                            consoleCommand();

                        }
                        else
                            try
                            {
                                Console.WriteLine(strings[stage1].ToString().Trim());
                                consoleCommand();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(Constants.vbNewLine + "Sequence: Error: Couldn't parse 'print' " + stage1);
                                consoleCommand();
                                //output.AppendText(Constants.vbNewLine + "Snowy: Error! Couldn't parse 'print' " + stage1 + ". Maybe the string variable dosen't exist?");
                            }
                    }
                }


                catch (Exception ex)
                {
                    Console.WriteLine(Constants.vbNewLine + "Sequence: Error: Couldn't parse 'print' " + stage1);

                }
            } else {
                consoleCommand();
            }

        }

        public static void Parse(string line)
        {
            //Console.WriteLine("Parsing...");
            if(line.StartsWith("print\"") || line.StartsWith("print \"") || line.StartsWith("print "))
            {
                string stage1 = line.Replace("print", "").Trim();
                // Where it is now: ex: Started with: write "hello!" now is: "hello!"
                try
                {
                    int lastIndexOfStage1 = stage1.Length;
                    lastIndexOfStage1 = lastIndexOfStage1 - 1;
                    //Console.WriteLine("Length of statement: " + lastIndexOfStage1);
                    //Console.WriteLine("Last index of \" is: " + stage1.LastIndexOf("\""));
                    

                    if(stage1.LastIndexOf("\"") != lastIndexOfStage1)
                    {
                        try
                        {
                            Console.WriteLine(strings[stage1].ToString().Trim());

                        }
                        catch (Exception ex)
                        {
                            
                            Console.WriteLine(Constants.vbNewLine + "Sequence: Error: Couldn't parse 'print' " + stage1 + " because missing \".");
                            //output.AppendText(Constants.vbNewLine + "Snowy: Error! Couldn't parse 'print' " + stage1 + ". Maybe the string variable dosen't exist?");
                        }
                        
                    }
                    else
                    {
                        if (stage1.Contains("\""))
                        {
                            string stage2 = stage1.Replace("\"", "");
                            Console.WriteLine(stage2.Trim());

                        }
                      
                            
                    }
                }
                    

                catch (Exception ex)
                {
                    Console.WriteLine(Constants.vbNewLine + "Sequence: Error: Couldn't parse 'print' " + stage1);

                }
            }

            else if (line.StartsWith("string "))
            {
                //Console.Write("Adding string...");
                string stage1 = line.Replace("string", "");
                int commaIndex = stage1.IndexOf("=");
                

                try
                {
                    varname = stage1.Remove(commaIndex).Trim();
                    if (stage1.Contains("\""))
                        varValue = stage1.Remove(0, commaIndex).Replace("=", "").Trim().Replace("\"", "");
                    else
                    {
                        Console.WriteLine(Constants.vbNewLine + "Sequence: Error! You must assign a value to a string before usage. (Can only print string variables)");
                        
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    strings.Add(varname, varValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(Constants.vbNewLine + "Sequence: Error! While parsing 'str.var' block. You must assign a value to a variable!");
                    
                }
            }

        }
    }
    }


