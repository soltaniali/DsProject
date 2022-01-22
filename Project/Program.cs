using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Project
{
    internal class Program
    {
        public static Hashtable Diseases = new Hashtable();
        public static Hashtable Allergies = new Hashtable();
        public static Hashtable Drugs = new Hashtable();
        public static Hashtable Effects = new Hashtable();

        public static void SaveDrugs()
        {
            File.Delete(@"../datasets/drugs.txt");
            StreamWriter WriteDrugs = new StreamWriter(@"../datasets/drugs.txt");
            foreach (DictionaryEntry item in Program.Drugs)
            {
                if (item.Value != null) WriteDrugs.WriteLine("{0} : {1}", item.Key.ToString(), item.Value.ToString());
            }

            WriteDrugs.Close();
        }
        public static void SaveDiseases()
        {
            File.Delete(@"../datasets/diseases.txt");
            StreamWriter writeNewDis = new StreamWriter(@"../datasets/diseases.txt");
            foreach (DictionaryEntry item in Program.Diseases)
            {
                writeNewDis.WriteLine(item.Key);
            }

            writeNewDis.Close();
        }
        public static void SaveEffects()
        {
            File.Delete(@"../datasets/effects.txt");
            StreamWriter writerEffects = new StreamWriter(@"../datasets/effects.txt");
            int counter = 0;
            string form = "";
            foreach (DictionaryEntry item in Program.Effects)
            {
                form = item.Key.ToString();
                form += " :";
                foreach (DictionaryEntry obj in (Hashtable)item.Value)
                {
                    counter++;
                    form += " (";
                    form += obj.Key.ToString();
                    form += ",";
                    form += obj.Value.ToString();
                    form += ") ";
                    if (counter < ((Hashtable)item.Value).Count)
                    {
                        form += ";";
                    }
                }

                writerEffects.WriteLine(form);
                form = "";
                counter = 0;
            }

            writerEffects.Close();
        }
        public static void SaveAllergies()
        {
            var form = "";
            var counter = 0;
            File.Delete(@"../datasets/alergies.txt");
            var writerAlergies = new StreamWriter(@"../datasets/alergies.txt");
            foreach (DictionaryEntry item in Program.Allergies)
            {
                form = item.Key.ToString();
                form += " :";
                foreach (DictionaryEntry obj in (Hashtable) item.Value)
                {
                    counter++;
                    form += " (";
                    form += obj.Key.ToString();
                    form += ",";
                    form += obj.Value.ToString();
                    form += ") ";
                    if (counter < ((Hashtable) item.Value).Count)
                    {
                        form += ";";
                    }
                }

                writerAlergies.WriteLine(form);
                form = "";
                counter = 0;
            }

            writerAlergies.Close();
            
        }
        public static void ReadFromDisease()
        {
            var Line = "";
            var reader = new StreamReader(@"../datasets/diseases.txt");
            while ((Line = reader.ReadLine()) != null)
            {
                Program.Diseases.Add(Line, "");
            }
            reader.Close();
        }
        public static void ReadFromAllergies()
        {
            var Line = "";
            StreamReader readerAlergies = new StreamReader(@"../datasets/alergies.txt");
            while ((Line = readerAlergies.ReadLine()) != null)
            {
                string[] tempstring = Line.Split(' ', ':', ';', ')', '(');
                var temp = new Hashtable();
                for (int i = 1; i < tempstring.Length; i++)
                {
                    if (tempstring[i] != "")
                    {
                        string[] doubleHashString = tempstring[i].Split(',');
                        temp.Add(doubleHashString[0], doubleHashString[1]);
                    }
                }
                if (tempstring[0] != "")
                {
                    Program.Allergies.Add(tempstring[0], temp);
                }
            }
            readerAlergies.Close();
        }
        public static void ReadFromEffects()
        {
            var Line = "";
            StreamReader readerEffects = new StreamReader(@"../datasets/effects.txt");
            while ((Line = readerEffects.ReadLine()) != null)
            {
                string[] tempstring = Line.Split(' ', ':', ';', ')', '(');
                var temp = new Hashtable();
                for (int i = 1; i < tempstring.Length; i++)
                {
                    if (tempstring[i] != "")
                    {
                        string[] doubleHashString = tempstring[i].Split(',');
                        temp.Add(doubleHashString[0], doubleHashString[1]);
                    }
                }
                if (tempstring[0] != "")
                {
                    Program.Effects.Add(tempstring[0], temp);
                }
            }
            readerEffects.Close();
        }
        public static void ReadFromDrugs()
        {
            var Line = "";
            var counter = 0;
            var divider2 = new string[2];
            var reader = new StreamReader(@"../datasets/drugs.txt");
            while ((Line = reader.ReadLine()) != null)
            {
                counter++;
                divider2 = Line.Split(' ', ':');
                Program.Drugs.Add(divider2[0], divider2[3]);
            }
            reader.Close();
        }
        public static string GenerateRandomString()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[10];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        private static void Main()
        {
            var noskhe = new Dictionary<string, int>();
            var referralDiseases = new List<string>();

            var time = new Stopwatch();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("********** Menu **********" +
                              "\nPlease inter the number of Order:" +
                              "\n1- Read from data sets and store in data structure" +
                              "\n2- Inter the new drug that you store in data set" +
                              "\n3- Inter the new disease that you store in data set" +
                              "\n4- Inter the drug name that you remove from data set" +
                              "\n5- Inter the disease name that you remove from data set" +
                              "\n6- search in 'drugs' data set by name" +
                              "\n7- search in 'disease' data set by name" +
                              "\n8- search in 'drugs', 'effects', 'allergies' data set by drugName" +
                              "\n9- search in 'diseases', 'allergies' data set by diseaseName" +
                              "\n10- enter the prescription" +
                              "\n11- enter the referral Diseases" +
                              "\n12- check the allergies of referral Diseases" +
                              "\n13- check the Interference drugs in prescription" +
                              "\n14- clean prescription" +
                              "\n15- clean referral Diseases" +
                              "\n16- Calculate price" +
                              "\n17- Increase price of drugs" +
                              "\n18- exit and save files" +
                              "\n19- exit" +
                              "\n**************************");
            Console.ResetColor();
            var counter = 0;
            while (true)
            {
                var check = false;
                var order = 0;
                while (!check)
                {
                    try
                    {
                        order = Convert.ToInt32(Console.ReadLine());
                        check = true;
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Please correct form of orders (inter only integer numbers)");
                        Console.ResetColor();
                    }
                }

                if (order != 1 && order != 18 && counter == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("First should you read data from the data sets!!");
                    Console.ResetColor();
                }
                else if (order == 1 && counter == 0)
                {
                    var a = new Thread(Program.ReadFromDisease);
                    var b = new Thread(Program.ReadFromAllergies);
                    var c = new Thread(Program.ReadFromDrugs);
                    var d = new Thread(Program.ReadFromEffects);

                    time.Start();
                    a.Start();
                    b.Start();
                    c.Start();
                    d.Start();
                    time.Stop();

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Execute time for read data :" +
                                      time.ElapsedMilliseconds * 1000 + " Micros");
                    Console.ResetColor();

                    counter++;
                }
                else if (order == 1 && counter > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("data read from data set for once");
                    Console.ResetColor();
                }
                else if (order == 2)
                {
                    Console.WriteLine("Inter name of drug:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);

                        var diseaseTemp = new string[20];
                        var drugsName = new string[20];
                        var count = 0;

                        var y = new Stopwatch();
                        y.Start();

                        foreach (var item in Program.Diseases.Keys)
                        {
                            if (count == 20)
                                break;
                            diseaseTemp[count] = item as string;
                            count++;
                        }

                        count = 0;
                        foreach (var item in Program.Drugs.Keys)
                        {
                            if (count == 20)
                                break;
                            drugsName[count] = item as string;
                            count++;
                        }

                        if (!Program.Drugs.ContainsKey(replace))
                        {
                            //convert drug name in hash table to array to access with index
                            var random = new Random();
                            var numberRandomDrug = random.Next(1, 4);
                            Console.WriteLine("Number of random drugs: " + numberRandomDrug +
                                              "\n-------------------------------------");
                            Console.WriteLine("random effects that generated:");
                            var temp = new Hashtable();
                            for (var i = 0; i < numberRandomDrug; i++)
                            {
                                var index = random.Next(0, 20);
                                var effectiveTemp = "Eff_" + GenerateRandomString();
                                Console.WriteLine(effectiveTemp);

                                temp.Add(drugsName[index], effectiveTemp);
                            }

                            Console.WriteLine("-------------------------------------");
                            //add effects
                            Program.Effects.Add(replace, temp);

                            //add to diseases data set
                            var numberDiseases = random.Next(1, 5);
                            Console.WriteLine("Number of random diseases: " + numberDiseases);
                            for (var i = 0; i < numberDiseases; i++)
                            {
                                var index = random.Next(0, 20);
                                const string effectMark = "+-";

                                if (Program.Allergies[diseaseTemp[index]] is Hashtable tempDisease) tempDisease.Add(replace, effectMark[random.Next(effectMark.Length)]);
                                else
                                {
                                    Program.Allergies.Add(diseaseTemp[index],
                                        new Hashtable {{replace, effectMark[random.Next(effectMark.Length)]}});
                                }
                            }

                            //add the drug to drugs data set
                            var drugPrice = random.Next(1000, 100000);
                            Program.Drugs.Add(replace, Convert.ToString(drugPrice));
                        }
                        else
                        {
                            Console.WriteLine("already exist this drug in 'drugs' data set!");
                        }
                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Execute time for create new Drug process: " +
                                          y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                }
                else if (order == 3)
                {
                    Console.WriteLine("Inter name of disease:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        var y = new Stopwatch();
                        y.Start();

                        if (!Program.Diseases.Contains(replace))
                        {
                            //convert drug name in hash table to array to access with index
                            var drugsName = new string[Program.Drugs.Count];
                            Program.Drugs.Keys.CopyTo(drugsName, 0);

                            var random = new Random();
                            var numberRandomDrug = random.Next(1, 5);
                            Console.WriteLine("Number of random drugs that generated: " + numberRandomDrug +
                                              "\n-----------------------------------");
                            var temp = new Hashtable();
                            for (var i = 0; i < numberRandomDrug; i++)
                            {
                                var index = random.Next(0, Program.Drugs.Count);
                                const string effectMark = "+-";
                                temp.Add(drugsName[index], effectMark[random.Next(effectMark.Length)]);
                            }

                            //add the disease to diseases data set
                            Program.Diseases.Add(replace, "");
                            Program.Allergies.Add(replace, temp);
                        }
                        else
                        {
                            Console.WriteLine("already exist this disease in 'drugs' data set!");
                        }
                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Execute time for create new Disease process: " +
                                          y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                }
                else if (order == 4)
                {
                    Console.WriteLine("Inter name of the drug that you want remove:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        var y = new Stopwatch();

                        if (Program.Drugs.ContainsKey(replace))
                        {
                            y.Start();
                            Program.Drugs.Remove(replace);
                            if (Program.Effects.ContainsKey(replace))
                                Program.Effects.Remove(replace);

                            var tempEffects = new List<string>();
                            foreach (DictionaryEntry item in Program.Effects)
                            {
                                Hashtable t = (Hashtable) item.Value;
                                if (t != null && t.ContainsKey(replace))
                                    t.Remove(replace);
                                if (t is {Count: 0})
                                    tempEffects.Add(item.Key.ToString());

                            }

                            foreach (var item in tempEffects)
                                Program.Effects.Remove(item);

                            var tempDiseases = new List<string>();
                            foreach (DictionaryEntry item in Program.Allergies)
                            {
                                var t = (Hashtable) item.Value;
                                if (t != null && t.ContainsKey(replace))
                                    t.Remove(replace);
                                if (t is {Count: 0})
                                    tempDiseases.Add(item.Key.ToString());
                            }

                            foreach (var item in tempDiseases)
                                Program.Allergies.Remove(item);
                        }
                        else
                        {
                            y.Start();
                            Console.WriteLine("Not exist this drug in 'drugs' data set!");
                        }

                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Execute time for delete the Drug process: " +
                                          y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                }
                else if (order == 5)
                {
                    Console.WriteLine("Inter name of the disease that you want remove:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        var y = new Stopwatch();
                        try
                        {
                            y.Start();
                            Program.Diseases.Remove(replace);

                            if (Program.Allergies.ContainsKey(replace))
                                Program.Allergies.Remove(replace);
                        }
                        catch
                        {
                            Console.WriteLine("Not exist this disease in 'diseases' data set!");
                        }
                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Execute time for delete the disease process: " +
                                          y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                }
                else if (order == 6)
                {
                    Console.WriteLine("Inter name of the drug that you want to search:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        var y = new Stopwatch();
                        y.Start();

                        if(Program.Drugs.ContainsKey(replace))
                        {
                            Console.WriteLine("Name of drug: " + replace +
                                              "\nPrice of drug: " + Program.Drugs[replace]);
                        }
                        else
                        {
                            Console.WriteLine("Not found this drug in 'drugs' data set!");
                        }

                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Execute time for search the Drug: " +
                                          y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                }
                else if (order == 7)
                {
                    Console.WriteLine("Inter name of the disease that you want to search:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        var y = new Stopwatch();
                        y.Start();

                        if (Program.Diseases.ContainsKey(replace))
                        {
                            Console.WriteLine("Name of disease: " + replace);
                        }
                        else
                        {
                            Console.WriteLine("Not found this disease in 'diseases' data set!");
                        }

                        y.Stop();
                        Console.WriteLine("Execute time for search the Disease: " + y.Elapsed + "( " +
                                          y.ElapsedMilliseconds * 1000 + " Micros)");
                    }
                }
                else if (order == 8)
                {
                    Console.WriteLine("Inter name of the drug that you want to search in 'effects', 'allergies':");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        var y = new Stopwatch();
                        
                        if (Program.Drugs.ContainsKey(replace))
                        {
                            y.Start();
                            Console.WriteLine("Related to the 'effects' data set:");
                            foreach (DictionaryEntry item in Program.Effects)
                            {
                                if (item.Value is Hashtable t && t.ContainsKey(replace))
                                {
                                    Console.WriteLine(replace + " has effect: " + t[replace] + " on " + item.Key);
                                }
                            }

                            Console.WriteLine(
                                "----------------------------------\nRelated to the 'allergies' data set:");
                            foreach (DictionaryEntry item in Program.Allergies)
                            {
                                if (item.Value is Hashtable t && t.ContainsKey(replace))
                                {
                                    Console.WriteLine(replace + " has effect: " + t[replace] + " on " + item.Key);
                                }
                            }

                            Console.WriteLine("----------------------------------");
                        }
                        else
                        {
                            y.Start();
                            Console.WriteLine("Not found this drug in 'drugs','effects','allergies' data set!");
                        }

                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(
                            "Execute time for search the drug in 'drugs', 'effects', 'allergies' data set: "
                            + y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                }
                else if (order == 9)
                {
                    Console.WriteLine(
                        "Inter name of the disease that you want to search in 'diseases', 'allergies' data set:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        var y = new Stopwatch();
                        y.Start();
                        if (Program.Diseases.ContainsKey(replace))
                        {
                            if (Program.Allergies.ContainsKey(replace))
                            {
                                if (Program.Allergies[replace] is Hashtable t)
                                    foreach (DictionaryEntry item in t)
                                    {
                                        if (item.Value != null && (string)item.Value == "+")
                                        {
                                            Console.WriteLine(item.Key + " has effect: " + item.Value);
                                        }
                                    }
                            }
                            Console.WriteLine("----------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Not found this drug in 'drugs','effects','allergies' data set!");
                        }
                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Execute time for search the drug in 'drugs', 'effects', 'allergies' data set: " +
                                          y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                }
                else if (order == 10)
                {
                    var temp = true;
                    Console.WriteLine(
                        "first enter the numbers of drugs and thenEnter name of the drug and then num of each drugs");
                    var numberOfDrugs = 0;
                    var Line = new string[2];
                    try
                    {
                        numberOfDrugs = int.Parse(Console.ReadLine() ?? string.Empty);
                    }
                    catch
                    {
                        Console.WriteLine("Please enter integer number!");
                        temp = false;
                    }
                    for (int i = 0; i < numberOfDrugs; i++)
                    {
                        Line = Console.ReadLine()?.Split(' ');
                        try
                        {
                            if (Line != null) noskhe.Add(Line[0], int.Parse(Line[1]));
                        }
                        catch
                        {
                            Console.WriteLine("lotfan noskhe ra dorost vared konid");
                            noskhe.Clear();
                            temp = false;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(temp ? "noskhe created." : "dobare talash konid.");
                    Console.ResetColor();
                }
                else if (order == 11)
                {
                    var temp = true;
                    Console.WriteLine("First enter number of diseases then enter name of them:");
                    var numDisease = 0;
                    try
                    {
                        numDisease = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Please correct form of number diseases (inter only integer numbers)");
                        temp = false;
                    }

                    for (var i = 0; i < numDisease; i++)
                    {
                        referralDiseases.Add(Console.ReadLine());
                    }
                    Console.WriteLine(temp ? "referralDiseases created." : "dobare talash konid.");
                }
                else if (order == 12)
                {
                    if (referralDiseases.Count > 0 && noskhe.Count > 0)
                    {
                        var checkInterference = false;
                        var y = new Stopwatch();
                        y.Start();
                        foreach (var item in referralDiseases)
                        {
                            if (Program.Allergies.ContainsKey(item))
                            {
                                foreach (var drug in noskhe)
                                {
                                    if (Program.Allergies[item] is Hashtable x && x.Contains(drug.Key) &&
                                        x[drug.Key]?.ToString() == "-")
                                    {
                                        Console.WriteLine(item + ":" + drug.Key + " has tadakhol " + x[drug.Key]?.ToString());
                                        checkInterference = true;
                                    }
                                }
                            }
                        }

                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("time : " +
                                          y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                        if (!checkInterference)
                        {
                            Console.WriteLine("Not found the any Interference");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The prescription or referral diseases is(are) empty");
                    }
                }
                else if (order == 13)
                {
                    var checkInterference = false;
                    if (noskhe.Count > 0)
                    {
                        var y = new Stopwatch();
                        y.Start();
                        foreach (var item in noskhe)
                        {
                            if (Program.Effects.ContainsKey(item.Key))
                            {
                                foreach (var drug in noskhe)
                                {
                                    if (drug.Key != item.Key && Program.Effects[item.Key] is Hashtable x && x.Contains(drug.Key))
                                    {
                                        Console.WriteLine(item.Key + ":" + drug.Key + " has tadakhol " + x[drug.Key]?.ToString());
                                        checkInterference = true;
                                    }
                                }
                            }
                        }
                        y.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("time : " +
                                          y.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The prescription is empty");
                        Console.ResetColor();
                    }
                    if (!checkInterference)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Not found the any Interference");
                        Console.ResetColor();
                    }
                }
                else if (order == 14)
                {
                    noskhe.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("cleared");
                    Console.ResetColor();
                }
                else if (order == 15)
                {
                    referralDiseases.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("cleared");
                    Console.ResetColor();
                }
                else if (order == 16)
                {
                    if (noskhe.Count > 0)
                    {
                        var x = new Stopwatch();
                        x.Start();
                        var finalPrice = 0;
                        foreach (var item in noskhe)
                            finalPrice += item.Value * Convert.ToInt32(Program.Drugs[item.Key.ToString()]);
                        Console.WriteLine("The price of prescription is= " + finalPrice);
                        x.Stop();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("time : " +
                                          x.ElapsedMilliseconds * 1000 + " Micros");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The prescription is empty!");
                        Console.ResetColor();
                    }
                }
                else if (order == 17)
                {
                    Console.WriteLine("Enter the Percent of increase prices:");
                    double percent = 0.0f;
                    try
                    {
                        percent = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("The format of input is not correct!");
                        return;
                    }

                    if (percent < -100)
                        percent = -100;
                    percent += 100;
                    var x = new Stopwatch();
                    var tempDrugs = Program.Drugs;
                    Program.Drugs = new Hashtable();
                    x.Start();
                    foreach (DictionaryEntry item in tempDrugs)
                    {
                        if (item.Value != null)
                        {
                            var perPrice = Convert.ToDouble(item.Value.ToString());
                            Program.Drugs.Add(item.Key, Convert.ToString((perPrice * percent) / 100));
                        }
                    }
                    x.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("time : " +
                                      x.ElapsedMilliseconds * 1000 + " Micros");
                    Console.ResetColor();
                }
                else if (order == 18)
                {
                    var a = new Thread(Program.SaveDiseases);
                    var b = new Thread(Program.SaveDrugs);
                    var c = new Thread(Program.SaveAllergies);
                    var d = new Thread(Program.SaveEffects);

                    var x = new Stopwatch();
                    x.Start();

                    a.Start();
                    b.Start();
                    c.Start();
                    d.Start();

                    x.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("time : "+
                                      x.ElapsedMilliseconds * 1000 + " Micros");
                    Console.ResetColor();
                    break;
                }
                else if (order == 19)
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Out range of the orders!");
                    Console.ResetColor();
                }
            }
        }
    }
}