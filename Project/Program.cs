﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Project
{
    internal class Program
    {
        public static void SaveDrugs(Hashtable drugs)
        {
            File.Delete(@"../datasets/drugs.txt");
            StreamWriter WriteDrugs = new StreamWriter(@"../datasets/drugs.txt");
            foreach (DictionaryEntry item in drugs)
            {
                if (item.Value != null) WriteDrugs.WriteLine("{0} : {1}", item.Key.ToString(), item.Value.ToString());
            }

            WriteDrugs.Close();
        }
        public static void SaveDiseases(List<string> diseases)
        {
            File.Delete(@"../datasets/diseases.txt");
            StreamWriter writeNewDis = new StreamWriter(@"../datasets/diseases.txt");
            foreach (var item in diseases)
            {
                writeNewDis.WriteLine(item);
            }

            writeNewDis.Close();
        }
        public static void SaveEffects(Hashtable effects)
        {
            File.Delete(@"../datasets/effects.txt");
            StreamWriter writerEffects = new StreamWriter(@"../datasets/effects.txt");
            int counter = 0;
            string form = "";
            foreach (DictionaryEntry item in effects)
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
        public static void SaveAllergies(Hashtable allergies)
        {
            var form = "";
            var counter = 0;
            File.Delete(@"../datasets/alergies.txt");
            var writerAlergies = new StreamWriter(@"../datasets/alergies.txt");
            foreach (DictionaryEntry item in allergies)
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
        public static List<string> ReadFromDisease()
        {
            var diseases = new List<string>();
            diseases.AddRange(File.ReadAllLines(@"../datasets/diseases.txt"));

            return diseases;
        }
        public static Hashtable ReadFromAllergies()
        {
            var Line = "";
            var alergies = new Hashtable();
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
                    alergies.Add(tempstring[0], temp);
                }
            }
            readerAlergies.Close();
            return alergies;
        }
        public static Hashtable ReadFromEffects()
        {
            var Line = "";
            var effects = new Hashtable();
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
                    effects.Add(tempstring[0], temp);
                }
            }
            readerEffects.Close();

            return effects;
        }
        public static Hashtable ReadFromDrugs()
        {
            var Line = "";
            var counter = 0;
            var divider2 = new string[2];
            var drugs = new Hashtable();
            var reader = new StreamReader(@"../datasets/drugs.txt");
            while ((Line = reader.ReadLine()) != null)
            {
                counter++;
                divider2 = Line.Split(' ', ':');
                drugs.Add(divider2[0], divider2[3]);
            }
            reader.Close();

            return drugs;
        }
        public static void CreateDrug(string drugName, Hashtable drugs, List<string> disease, Hashtable effects,
            Hashtable allergies)
        {
            var x = new Stopwatch();
            x.Start();

            if (!drugs.ContainsKey(drugName))
            {
                //convert drug name in hash table to array to access with index
                var drugsName = new string[drugs.Count];
                drugs.Keys.CopyTo(drugsName, 0);

                var random = new Random();
                var numberRandomDrug = random.Next(1, 4);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Number of random drugs: " + numberRandomDrug +
                                  "\n-------------------------------------");
                Console.WriteLine("random effects that generated:");
                Console.ResetColor();
                var temp = new Hashtable();
                for (var i = 0; i < numberRandomDrug; i++)
                {
                    var index = random.Next(0, drugs.Count);
                    var effectiveTemp = "Eff_" + GenerateRandomString();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(effectiveTemp);
                    Console.ResetColor();

                    temp.Add(drugsName[index], effectiveTemp);
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("-------------------------------------");
                Console.ResetColor();
                //add effects
                effects.Add(drugName, temp);

                //add to diseases data set
                var numberDiseases = random.Next(1, 5);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Number of random diseases: " + numberDiseases);
                Console.ResetColor();
                for (var i = 0; i < numberDiseases; i++)
                {
                    var index = random.Next(0, disease.Count);
                    const string effectMark = "+-";

                    if (allergies[disease[index]] is Hashtable tempDisease) tempDisease.Add(drugName, effectMark[random.Next(effectMark.Length)]);
                    else
                    {
                        allergies.Add(disease[index],
                            new Hashtable {{drugName, effectMark[random.Next(effectMark.Length)]}});
                    }
                }

                //add the drug to drugs data set
                var drugPrice = random.Next(1000, 100000);
                drugs.Add(drugName, Convert.ToString(drugPrice));

                //SaveDrugs(drugs);
                //SaveEffects(effects);
                //SaveAllergies(allergies);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("already exist this drug in 'drugs' data set!");
                Console.ResetColor();
            }
            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute time for create new Drug process: " + x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
        }
        public static void CreateDisease(string diseaseName, Hashtable drugs, List<string> disease, Hashtable effects,
            Hashtable allergies)
        {
            var x = new Stopwatch();
            x.Start();

            if (!disease.Contains(diseaseName))
            {
                //convert drug name in hash table to array to access with index
                var drugsName = new string[drugs.Count];
                drugs.Keys.CopyTo(drugsName, 0);

                var random = new Random();
                var numberRandomDrug = random.Next(1, 5);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Number of random drugs that generated: " + numberRandomDrug +
                                  "\n-----------------------------------");
                Console.ResetColor();
                var temp = new Hashtable();
                for (var i = 0; i < numberRandomDrug; i++)
                {
                    var index = random.Next(0, drugs.Count);
                    const string effectMark = "+-";
                    temp.Add(drugsName[index], effectMark[random.Next(effectMark.Length)]);
                }

                //add the disease to diseases data set
                disease.Add(diseaseName);
                allergies.Add(diseaseName, temp);

                //SaveDiseases(disease);
                //SaveAllergies(allergies);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("already exist this disease in 'drugs' data set!");
                Console.ResetColor();
            }
            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute time for create new Disease process: " + x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
        }
        public static void DeleteDrug(string drugName, Hashtable drugs, List<string> disease, Hashtable effects,
            Hashtable allergies)
        {
            var x = new Stopwatch();
            x.Start();

            if (drugs.ContainsKey(drugName))
            {
                drugs.Remove(drugName);
                if (effects.ContainsKey(drugName))
                    effects.Remove(drugName);

                //var tempEffects = new List<string>();
                foreach (DictionaryEntry item in effects)
                {
                    Hashtable t = (Hashtable)item.Value;
                    if (t != null && t.ContainsKey(drugName))
                        t.Remove(drugName);
                    //if (t is { Count: 0 })
                    //    tempEffects.Add(item.Key.ToString());

                }
                //foreach (var item in tempEffects)
                //    effects.Remove(item);

                //var tempDiseases = new List<string>();
                foreach (DictionaryEntry item in allergies)
                {
                    var t = (Hashtable)item.Value;
                    if (t != null && t.ContainsKey(drugName))
                        t.Remove(drugName);
                    //if (t is { Count: 0 })
                    //    tempDiseases.Add(item.Key.ToString());
                }

                //foreach (var item in tempDiseases)
                //    allergies.Remove(item);
                //SaveDrugs(drugs);
                //SaveEffects(effects);
                //SaveAllergies(allergies);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Not exist this drug in 'drugs' data set!");
                Console.ResetColor();
            }
                

            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute time for delete the Drug process: " + x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
        }
        public static void DeleteDisease(string diseaseName, Hashtable drugs, List<string> disease, Hashtable effects,
            Hashtable allergies)
        {
            var x = new Stopwatch();
            x.Start();

            if (disease.Contains(diseaseName))
            {
                disease.Remove(diseaseName);

                if (allergies.ContainsKey(diseaseName))
                    allergies.Remove(diseaseName);

                //SaveDiseases(disease);
                //SaveAllergies(allergies);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Not exist this disease in 'diseases' data set!");
                Console.ResetColor();
            }
            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute time for delete the disease process: " + x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
        }
        public static void SearchByDrugName(string drugName, Hashtable drugs)
        {
            var x = new Stopwatch();
            x.Start();

            if (drugs.ContainsKey(drugName))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Name of drug: " + drugName +
                                  "\nPrice of drug: " + drugs[drugName]);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Not found this drug in 'drugs' data set!");
                Console.ResetColor();
            }

            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute time for search the Drug: " + x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
        }
        public static void SearchByDiseaseName(string diseaseName, List<string> disease)
        {
            var x = new Stopwatch();
            x.Start();

            if (disease.Contains(diseaseName))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Name of disease: " + diseaseName);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Not found this disease in 'diseases' data set!");
                Console.ResetColor();
            }

            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute time for search the Disease: " + x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
        }
        public static void SearchByDrugName(string drugName, Hashtable drugs, List<string> disease, Hashtable effects,
            Hashtable allergies)
        {
            var x = new Stopwatch();
            x.Start();
            if (drugs.ContainsKey(drugName))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Related to the 'effects' data set:");
                Console.ResetColor();
                foreach (DictionaryEntry item in effects)
                {
                    if (item.Value is Hashtable t && t.ContainsKey(drugName))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(drugName + " has effect: " + t[drugName] + " on " + item.Key);
                        Console.ResetColor();
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("----------------------------------\nRelated to the 'allergies' data set:");
                Console.ResetColor();
                foreach (DictionaryEntry item in allergies)
                {
                    if (item.Value is Hashtable t && t.ContainsKey(drugName))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(drugName + " has effect: " + t[drugName] + " on " + item.Key);
                    }
                }

                Console.WriteLine("----------------------------------");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Not found this drug in 'drugs','effects','allergies' data set!");
                Console.ResetColor();
            }
            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute time for search the drug in 'drugs', 'effects', 'allergies' data set: " +
                              x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
        }
        public static void SearchByDiseaseName(string diseaseName, Hashtable drugs, List<string> disease, Hashtable effects,
            Hashtable allergies)
        {
            var x = new Stopwatch();
            x.Start();
            if (disease.Contains(diseaseName))
            {
                if (allergies.ContainsKey(diseaseName))
                {
                    if (allergies[diseaseName] is Hashtable t)
                        foreach (DictionaryEntry item in t)
                        {
                            if (item.Value != null && (string) item.Value == "+")
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine(item.Key + " has effect: " + item.Value);
                            }
                        }
                }
                Console.WriteLine("----------------------------------");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Not found this drug in 'drugs','effects','allergies' data set!");
                Console.ResetColor();
            }
            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute time for search the drug in 'drugs', 'effects', 'allergies' data set: " +
                              x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
        }
        public static void CheckEffects(Dictionary<string, int> noskhe, Hashtable effects)
        {
            var checkInterference = false;
            if (noskhe.Count > 0)
            {
                var y = new Stopwatch();
                y.Start();
                foreach (var item in noskhe)
                {
                    if (effects.ContainsKey(item.Key))
                    {
                        foreach (var drug in noskhe)
                        {
                            if (drug.Key != item.Key && effects[item.Key] is Hashtable x && x.Contains(drug.Key))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine(item.Key + ":" + drug.Key + " has tadakhol " + x[drug.Key]?.ToString());
                                checkInterference = true;
                                Console.ResetColor();
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
            if(!checkInterference)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Not found the any Interference");
                Console.ResetColor();
            }
        }
        public static void CheckDisease(Dictionary<string, int> prescription, List<string> referralDiseases,
            Hashtable allergies)
        {
            if (referralDiseases.Count > 0 && prescription.Count > 0)
            {
                var checkInterference = false;
                var y = new Stopwatch();
                y.Start();
                foreach (var item in referralDiseases)
                {
                    if (allergies.ContainsKey(item))
                    {
                        foreach (var drug in prescription)
                        {
                            if (allergies[item] is Hashtable x && x.Contains(drug.Key) &&
                                x[drug.Key]?.ToString() == "-")
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine(item + ":" + drug.Key + " has tadakhol " + x[drug.Key]?.ToString());
                                checkInterference = true;
                                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Not found the any Interference");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The prescription or referral diseases is(are) empty");
                Console.ResetColor();
            }
        }
        public static bool EnterDisease(ref List<string> referralDiseases)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("First enter number of diseases then enter name of them:");
            Console.ResetColor();
            var numDisease = 0;
            try
            {
                numDisease = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please correct form of number diseases (inter only integer numbers)");
                Console.ResetColor();
                return false;
            }

            for (var i = 0; i < numDisease; i++)
            {
                referralDiseases.Add(Console.ReadLine());
            }

            return true;
        }
        public static bool EnterNoskhe(ref Dictionary<string, int> noskhe)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                "first enter the numbers of drugs and thenEnter name of the drug and then num of each drugs");
            Console.ResetColor();
            var numberOfDrugs = 0;
            var Line = new string[2];
            try
            {
                numberOfDrugs = int.Parse(Console.ReadLine() ?? string.Empty);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please enter integer number!");
                Console.ResetColor();
                return false;
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
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("lotfan noskhe ra dorost vared konid");
                    Console.ResetColor();
                    noskhe.Clear();
                    return false;
                }
            }
            return true;
        }
        public static void CalculatePrice(Dictionary<string, int> prescription, Hashtable drugs)
        {
            if (prescription.Count > 0)
            {
                var x = new Stopwatch();
                x.Start();
                var finalPrice = 0;
                foreach (var item in prescription)
                    finalPrice += item.Value * Convert.ToInt32(drugs[item.Key.ToString()]);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("The price of prescription is= " + finalPrice);
                Console.ResetColor();
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
        public static void PriceIncrease(Hashtable drugs)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter the Percent of increase prices:");
            Console.ResetColor();
            double percent = 0.0f;
            try
            {
                percent = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The format of input is not correct!");
                Console.ResetColor();
                return;
            }

            if (percent < -100)
                percent = -100;
            percent += 100;
            var x = new Stopwatch();
            x.Start();
            var tempDrugs = drugs;
            drugs = new Hashtable();
            foreach (DictionaryEntry item in tempDrugs)
            {
                if (item.Value != null)
                {
                    var perPrice = Convert.ToDouble(item.Value.ToString());
                    drugs.Add(item.Key, Convert.ToString((perPrice * percent) / 100));
                }
            }
            
            //SaveDrugs(drugs);
            x.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("time : " +
                              x.ElapsedMilliseconds * 1000 + " Micros");
            Console.ResetColor();
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
        public static void UserInterface()
        {
            Hashtable drugs = new Hashtable(), effects = new Hashtable(), allergies = new Hashtable();
            var diseases = new List<string>();
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
                    time.Start();
                    // read from memory
                    diseases = ReadFromDisease();
                    allergies = ReadFromAllergies();
                    effects = ReadFromEffects();
                    drugs = ReadFromDrugs();
                    time.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Execute time for read data :" + time.ElapsedMilliseconds * 1000 + " Micros");
                    Console.ResetColor();

                    counter++;
                }
                else if (order == 1 && counter > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("data read from data set for once");
                    Console.ResetColor();
                }
                else if (order == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Inter name of drug:");
                    Console.ResetColor();
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        CreateDrug(replace, drugs, diseases, effects, allergies);
                    }
                }
                else if (order == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Inter name of disease:");
                    Console.ResetColor();
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        CreateDisease(replace, drugs, diseases, effects, allergies);
                    }
                }
                else if (order == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Inter name of the drug that you want remove:");
                    Console.ResetColor();
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        DeleteDrug(replace, drugs, diseases, effects, allergies);
                    }
                }
                else if (order == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Inter name of the disease that you want remove:");
                    Console.ResetColor();
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        DeleteDisease(replace, drugs, diseases, effects, allergies);
                    }
                }
                else if (order == 6)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Inter name of the drug that you want to search:");
                    Console.ResetColor();
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        SearchByDrugName(replace, drugs);
                    }
                }
                else if (order == 7)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Inter name of the disease that you want to search:");
                    Console.ResetColor();
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        SearchByDiseaseName(replace, diseases);
                    }
                }
                else if (order == 8)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Inter name of the drug that you want to search in 'effects', 'allergies':");
                    Console.ResetColor();
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        SearchByDrugName(replace, drugs, diseases, effects, allergies);
                    }
                }
                else if (order == 9)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(
                        "Inter name of the disease that you want to search in 'diseases', 'allergies' data set:");
                    Console.ResetColor();
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        SearchByDiseaseName(replace, drugs, diseases, effects, allergies);
                    }
                }
                else if (order == 10)
                {
                    var temp = EnterNoskhe(ref noskhe);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(temp ? "noskhe created." : "dobare talash konid.");
                    Console.ResetColor();
                }
                else if (order == 11)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    var temp = EnterDisease(ref referralDiseases);
                    Console.WriteLine(temp ? "referralDiseases created." : "dobare talash konid.");
                    Console.ResetColor();
                }
                else if (order == 12)
                {
                    CheckDisease(noskhe, referralDiseases, allergies);
                }
                else if (order == 13)
                {
                    CheckEffects(noskhe, effects);
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
                    CalculatePrice(noskhe, drugs);
                }
                else if (order == 17)
                {
                    PriceIncrease(drugs);
                }
                else if (order == 18)
                {
                    var x = new Stopwatch();
                    x.Start();
                    SaveDiseases(diseases);
                    SaveAllergies(allergies);
                    SaveDrugs(drugs);
                    SaveEffects(effects);
                    x.Stop();
                    Console.WriteLine("time : " +
                                      x.ElapsedMilliseconds * 1000 + " Micros");
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
        private static void Main()
        {
            UserInterface();
        }
    }
}