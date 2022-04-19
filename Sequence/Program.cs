using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static int linesLocation = 0;

        public static bool isParsing = true;

        static void Main(string[] args)
        {
            //Console.WriteLine(Convert.ToString(args));
            strings.Clear();
            integers.Clear();
            booleans.Clear();

            for (int i = 0; i < args.Length; i++)
            {
                //Console.WriteLine(args[i]);
                pathToFile = args[i];
                if (pathToFile.EndsWith(".seq"))
                {
                    //Console.WriteLine("VALID");
                } else
                {
                    Console.WriteLine("Incorrect file extension!");
                    isParsing = false;
                }


            }

            if (Convert.ToString(args[0]) == "System.String[]")
            {
                //Like Python cmd line.
                //consoleCommand();
            }
            else
            {


                for (int i = 0; i < args.Length; i++)
                {
                    //Console.WriteLine(args[i]);
                    pathToFile = args[i];

                    if(pathToFile == "")
                    {
                        Console.WriteLine("Sequence: Compiler!: No valid path to parse.");
                        isParsing = false;
                    }


                }


                IEnumerable<string> lines = File.ReadLines(pathToFile);
                //Console.WriteLine(String.Join(Environment.NewLine, lines));

                foreach (var line in lines)
                {
                    //Console.WriteLine(line);
                    if (isParsing)
                    {
                        ParseLine(line);
                        linesLocation = linesLocation + 1;
                    }
                    else
                    {

                    }

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
            }
            else
            {
                consoleCommand();
            }

        }

        public static void ParseLine(string line)
        {
            //Console.WriteLine("Parsing...");
            if (line.StartsWith("print\"") || line.StartsWith("print \"") || line.StartsWith("print "))
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
                        try
                        {
                            Console.WriteLine(strings[stage1].ToString().Trim());

                        }
                        catch (Exception ex)
                        {

                            try
                            {
                                Console.WriteLine(integers[stage1].ToString().Trim());

                            }
                            catch (Exception ex2)
                            {

                                try
                                {
                                    Console.WriteLine(booleans[stage1].ToString().Trim());

                                }
                                catch (Exception ex3)
                                {

                                    Console.WriteLine(Constants.vbNewLine + "Sequence: Error: Couldn't parse 'print' variable " + stage1 + " because variable was non-existant in current context.");
                                    isParsing = false;
                                    //output.AppendText(Constants.vbNewLine + "Snowy: Error! Couldn't parse 'print' " + stage1 + ". Maybe the string variable dosen't exist?");
                                }
                            }
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
                    isParsing = false;

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
                        isParsing = false;

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
                    Console.WriteLine(Constants.vbNewLine + "Sequence: Error! While parsing 'string' block. You must assign a value to a variable!");
                    isParsing = false;

                }
            }

            else if (line.StartsWith("int "))
            {

                string stage1 = line.Replace("int", "");

                string varname;
                int equalsIndex = stage1.IndexOf("=");
                int varValue;
                try
                {
                    varname = stage1.Remove(equalsIndex).Trim();
                    // Dim stage2 As String = stage1.Remove(0, 5).Trim
                    varValue = int.Parse(stage1.Remove(0, equalsIndex).Replace("=", ""));
                    integers.Add(varname, varValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(Constants.vbNewLine + "Sequence: Error! While parsing 'int' block. You can't assign a 'int' a string value!");
                    isParsing = false;

                }
            }

            else if (line.StartsWith("bool "))
            {
                // Example: bool isShylaCute = true
                string stage1 = line.Replace("bool", "");
                // isShylaCute = true
                string varname;
                string booleanValue;
                bool booleanValueForArray;
                int equalsPosititon = stage1.IndexOf("=");
                varname = stage1.Remove(equalsPosititon).Trim();
                // MsgBox("varname: " + varname)
                booleanValue = stage1.Remove(0, equalsPosititon).Replace("=", "").Trim();
                // MsgBox("booleanValue: " + booleanValue)

                if (booleanValue == "true" | booleanValue == "false")
                {
                    try
                    {
                        if (booleanValue == "true")
                            booleans.Add(varname.Trim(), true);
                        else if (booleanValue == "false")
                            booleans.Add(varname.Trim(), false);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    Console.WriteLine("Sequence: Error! Booleans can only be true or false.");
                    isParsing = false;


                }
            }
            else if (line.StartsWith("!!"))
            {
                //Ignore. Its a comment
            }
            else if (line.StartsWith("if "))
            {

                // Example: if [1=1] {

                string arguments;
                if (line.Contains("[") & line.Contains("]") & line.Contains("{") & line.Contains("=") == true)
                {
                    arguments = line.Replace("[", "").Replace("]", "").Replace("{", "").Replace("if", "").Trim();
                    // MsgBox("Arguments for 'if' block are: " + arguments)
                    string part1;
                    string part2;
                    int equalsPosition = arguments.IndexOf("=");
                    part1 = arguments.Remove(equalsPosition).Trim();
                    // MsgBox("Part1: " + part1)
                    part2 = arguments.Remove(0, equalsPosition).Replace("=", "").Trim();
                    // MsgBox("Part2: " + part2)
                    if (part1 == part2)
                    {
                        IEnumerable<string> ifLines = File.ReadLines(pathToFile);

                        foreach (var ifline in ifLines)
                        {


                        }

                    }
                }


                else

                {
                    //Console.Write("in final else block");

                    try
                    {
                        //testvar = ""
                        string varname = "";
                        string varContents = "";
                        int equalsPosTopLevel = line.IndexOf("=");
                        varname = line.Remove(equalsPosTopLevel).Trim();
                        //varname = testvar
                        varContents = line.Remove(0, equalsPosTopLevel);
                        //varContents is: = ""
                        varContents = varContents.Replace("=", "").Trim();
                        //varContents is: ""
                        //int varContentsInt = 0;

                        if (varContents.StartsWith("\"") && varContents.EndsWith("\""))
                        {
                            //String

                            for (int i = 0; i <= strings.Count - 1; i++)
                            {
                                string val = strings.ElementAt(i).ToString();
                                string stage1 = val.Replace("[", "").Replace("]", "").Replace("\"", "").Replace("str", "").Trim();
                                // MsgBox(stage1)
                                int commaLocation = stage1.IndexOf(",");
                                string key = stage1.Remove(commaLocation).Replace(",", "");
                                // MsgBox(key)
                                int equalsPos = line.IndexOf("=");
                                string keyValue = line.Remove(0, equalsPos).Replace("\"", "").Trim();
                                string finalKeyValue = keyValue.Replace("=", "");
                                // MsgBox("Final key value: " + finalKeyValue)
                                // Dim intKeyValue As Integer
                                // If Integer.TryParse(finalKeyValue, intKeyValue) = True Then
                                // MsgBox("The finalKeyValue is an int.")
                                // End If
                                if (strings.Remove(key) == true)
                                    strings.Add(key, finalKeyValue.Trim());
                            }


                        }
                        else if (int.TryParse(varContents, out int varContentsInt))
                        {
                            //Integer
                            for (int i = 0; i <= integers.Count - 1; i++)
                            {
                                string val = integers.ElementAt(i).ToString();
                                string stage1 = val.Replace("[", "").Replace("]", "").Replace("\"", "").Replace("str", "").Trim();
                                // MsgBox(stage1)
                                int commaLocation = stage1.IndexOf(",");
                                string key = stage1.Remove(commaLocation).Replace(",", "");
                                // MsgBox(key)
                                int equalsPos = line.IndexOf("=");
                                string keyValue = line.Remove(0, equalsPos).Trim();
                                string finalKeyValue = keyValue.Replace("=", "");
                                // MsgBox("Final key value: " + finalKeyValue)
                                // Dim intKeyValue As Integer
                                // If Integer.TryParse(finalKeyValue, intKeyValue) = True Then
                                // MsgBox("The finalKeyValue is an int.")
                                // End If
                                if (integers.Remove(key) == true)
                                    integers.Add(key, int.Parse(finalKeyValue.Trim()));
                            }




                        }
                        else
                        {
                            //Boolean

                            for (int i = 0; i <= booleans.Count - 1; i++)
                            {
                                string val = booleans.ElementAt(i).ToString();
                                string stage1 = val.Replace("[", "").Replace("]", "").Replace("\"", "").Replace("str", "").Trim();
                                // MsgBox(stage1)
                                int commaLocation = stage1.IndexOf(",");
                                string key = stage1.Remove(commaLocation).Replace(",", "");
                                // MsgBox(key)
                                int equalsPos = line.IndexOf("=");
                                string keyValue = line.Remove(0, equalsPos).Trim();
                                string finalKeyValue = keyValue.Replace("=", "").Trim();
                                // MsgBox("Final key value for boolset:  " + finalKeyValue)
                                // Dim intKeyValue As Integer
                                // If Integer.TryParse(finalKeyValue, intKeyValue) = True Then
                                // MsgBox("The finalKeyValue is an int.")
                                // End If
                                if (booleans.Remove(key) == true)
                                {
                                    if (finalKeyValue == "true" | finalKeyValue == "false")
                                    {
                                        if (finalKeyValue == "true")
                                            booleans.Add(key, true);
                                        else if (finalKeyValue == "false")
                                            booleans.Add(key, false);
                                        else
                                        {
                                        }
                                    }
                                }
                            }





                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Sequence: Error! The command " + line + " is unrecognized syntax.");
                        isParsing = false;
                    }


                }
            }

        }
    }
}
                

    


    


