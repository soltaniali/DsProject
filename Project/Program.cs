using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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

                var temp = new Hashtable();
                for (var i = 0; i < numberRandomDrug; i++)
                {
                    var index = random.Next(0, drugs.Count);
                    var effectiveTemp = "Eff_" + GenerateRandomString();

                    temp.Add(drugsName[index], effectiveTemp);
                }
                //add effects
                effects.Add(drugName, temp);

                //add to diseases data set
                var numberDiseases = random.Next(1, 5);
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

                SaveDrugs(drugs);
                SaveEffects(effects);
                SaveAllergies(allergies);
            }
            else
                Console.WriteLine("already exist this drug in 'drugs' data set!");

            x.Stop();
            Console.WriteLine("Execute time for create new Drug process: " + x.ElapsedMilliseconds * 1000 + " Micros");
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

                SaveDiseases(disease);
                SaveAllergies(allergies);
            }
            else
                Console.WriteLine("already exist this disease in 'drugs' data set!");

            x.Stop();
            Console.WriteLine("Execute time for create new Disease process: " + x.ElapsedMilliseconds * 1000 + " Micros");
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

                foreach (DictionaryEntry item in effects)
                {
                    Hashtable t = (Hashtable)item.Value;
                    if (t != null && t.ContainsKey(drugName))
                        t.Remove(drugName);

                }

                foreach (DictionaryEntry item in allergies)
                {
                    Hashtable t = (Hashtable)item.Value;
                    if (t != null && t.ContainsKey(drugName))
                        t.Remove(drugName);

                }

                SaveDrugs(drugs);
                SaveEffects(effects);
                SaveAllergies(allergies);
            }
            else
                Console.WriteLine("Not exist this drug in 'drugs' data set!");

            x.Stop();
            Console.WriteLine("Execute time for delete the Drug process: " + x.ElapsedMilliseconds * 1000 + " Micros");
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

                SaveDiseases(disease);
                SaveAllergies(allergies);
            }
            else
                Console.WriteLine("Not exist this disease in 'diseases' data set!");

            x.Stop();
            Console.WriteLine("Execute time for delete the disease process: " + x.ElapsedMilliseconds * 1000 + " Micros");
        }
        public static void SearchByDrugName(string drugName, Hashtable drugs)
        {
            var x = new Stopwatch();
            x.Start();

            if (drugs.ContainsKey(drugName))
                Console.WriteLine("Name of drug: " + drugName +
                                  "\nPrice of drug: " + drugs[drugName]);
            else
                Console.WriteLine("Not found this drug in 'drugs' data set!");

            x.Stop();
            Console.WriteLine("Execute time for search the Drug: " + x.ElapsedMilliseconds * 1000 + " Micros");
        }
        public static void SearchByDiseaseName(string diseaseName, List<string> disease)
        {
            var x = new Stopwatch();
            x.Start();

            if (disease.Contains(diseaseName))
                Console.WriteLine("Name of disease: " + diseaseName);
            else
                Console.WriteLine("Not found this disease in 'diseases' data set!");

            x.Stop();
            Console.WriteLine("Execute time for search the Disease: " + x.ElapsedMilliseconds * 1000 + " Micros");
        }
        public static void SearchByDrugName(string drugName, Hashtable drugs, List<string> disease, Hashtable effects,
            Hashtable allergies)
        {
            var x = new Stopwatch();
            x.Start();
            if (drugs.ContainsKey(drugName))
            {
                Console.WriteLine("Related to the 'effects' data set:");
                foreach (DictionaryEntry item in effects)
                {
                    if (item.Value is Hashtable t && t.ContainsKey(drugName))
                        Console.WriteLine(drugName + " has effect: " + t[drugName] + " on " + item.Key);
                }
                Console.WriteLine("----------------------------------\nRelated to the 'allergies' data set:");
                foreach (DictionaryEntry item in allergies)
                {
                    if (item.Value is Hashtable t && t.ContainsKey(drugName))
                        Console.WriteLine(drugName + " has effect: " + t[drugName] + " on " + item.Key);
                }
                Console.WriteLine("----------------------------------");
            }
            else
                Console.WriteLine("Not found this drug in 'drugs','effects','allergies' data set!");
            x.Stop();
            Console.WriteLine("Execute time for search the drug in 'drugs', 'effects', 'allergies' data set: " +
                              x.ElapsedMilliseconds * 1000 + " Micros");
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
                                Console.WriteLine(item.Key + " has effect: " + item.Value);
                        }
                }
                Console.WriteLine("----------------------------------");
            }
            else
                Console.WriteLine("Not found this drug in 'drugs','effects','allergies' data set!");
            x.Stop();
            Console.WriteLine("Execute time for search the drug in 'drugs', 'effects', 'allergies' data set: " +
                              x.ElapsedMilliseconds * 1000 + " Micros");
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
        public static void CheckEffects(Dictionary<string , int> noskhe , Hashtable effects)
        {
            var y = new Stopwatch();
            y.Start();
            foreach (var item in noskhe)
            {
                if( effects is Hashtable t && t.ContainsKey(item.Key))
                {
                    foreach(var daro in noskhe)
                    {
                        if(daro.Key != item.Key && t[item.Key] is Hashtable x && x.Contains(daro.Key))
                        {
                            Console.WriteLine(item.Key + ":" + daro.Key + " has tadakhol " +  x[daro.Key].ToString());
                        }
                    }
                }
            }
            y.Stop();
            Console.WriteLine("time : " +
                              y.ElapsedMilliseconds * 1000 + " Micros");

        }
        public static bool EnterNoskhe(ref Dictionary<string, int> noskhe) 
        {
            var x = new Stopwatch();
            x.Start();
            Console.WriteLine(
                " first enter the nums of dugs and thenEnter name of the drug and then num of drugs");
            int numberOfDrugs = 0;
            string[] Line = new string[2];
            try
            {
                numberOfDrugs = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("lotfan yek adad vared konid");
                x.Stop();
                Console.WriteLine("time :" +
                                  x.ElapsedMilliseconds * 1000 + " Micros");
                return false;
            }
            for (int i = 0; i < numberOfDrugs; i++)
            {
                Line = Console.ReadLine().Split(' ');
                try
                {
                    noskhe.Add(Line[0], int.Parse(Line[1]));
                }
                catch
                {
                    Console.WriteLine("lotfan noskhe ra dorost vared konid");
                    noskhe.Clear();
                    x.Stop();
                    Console.WriteLine("time"+
                                      x.ElapsedMilliseconds * 1000 + " Micros");
                    return false;
                }
            }
            x.Stop();
            Console.WriteLine("time : " +
                              x.ElapsedMilliseconds * 1000 + " Micros");
            return true;
        }
        public static void UserInterface()
        {
            Hashtable drugs = new Hashtable(), effects = new Hashtable(), allergies = new Hashtable();
            var diseases = new List<string>();
            var noskhe = new Dictionary<string, int>();
            var time = new Stopwatch();

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
                              "\n10- exit" +
                              "\n11- enter the noskhe" +
                              "\n12- chack tadakhol drugs in noskhe" +
                              "\n13- clean noskhe" +
                              "\n**************************");
            var counter = 0;
            while (true)
            {
                var order = Convert.ToInt32(Console.ReadLine());
                if (order != 1 && order != 10 && counter == 0)
                    Console.WriteLine("First should you read data from the data sets!!");
                else if(order == 1 && counter == 0)
                {
                    time.Start();
                    // read from memory
                     diseases = ReadFromDisease();
                     allergies = ReadFromAllergies();
                     effects = ReadFromEffects();
                     drugs = ReadFromDrugs();
                    time.Stop();
                    Console.WriteLine("Execute time for read data :" + time.ElapsedMilliseconds * 1000 + " Micros");

                    counter++;
                }
                else if (order == 1 && counter > 0)
                    Console.WriteLine("data read from data set for once");
                else if (order == 2)
                {
                    Console.WriteLine("Inter name of drug:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        CreateDrug(replace, drugs, diseases, effects, allergies);
                    }
                    counter++;
                }
                else if (order == 3)
                {
                    Console.WriteLine("Inter name of disease:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        CreateDisease(replace, drugs, diseases, effects, allergies);
                    }
                    counter++;
                }
                else if (order == 4)
                {
                    Console.WriteLine("Inter name of the drug that you want remove:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        DeleteDrug(replace, drugs, diseases, effects, allergies);
                    }
                    counter++;
                }
                else if (order == 5)
                {
                    Console.WriteLine("Inter name of the disease that you want remove:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        DeleteDisease(replace, drugs, diseases, effects, allergies);
                    }
                    counter++;
                }
                else if (order == 6)
                {
                    Console.WriteLine("Inter name of the drug that you want to search:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        SearchByDrugName(replace, drugs);
                    }
                    counter++;
                }
                else if (order == 7)
                {
                    Console.WriteLine("Inter name of the disease that you want to search:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        SearchByDiseaseName(replace, diseases);
                    }
                    counter++;
                }
                else if (order == 8)
                {
                    Console.WriteLine("Inter name of the drug that you want to search in 'effects', 'allergies':");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        SearchByDrugName(replace, drugs, diseases, effects, allergies);
                    }
                    counter++;
                }
                else if (order == 9)
                {
                    Console.WriteLine(
                        "Inter name of the disease that you want to search in 'diseases', 'allergies' data set:");
                    var x = Console.ReadLine();
                    if (x != null)
                    {
                        var replace = x.Replace(" ", string.Empty);
                        SearchByDiseaseName(replace, drugs, diseases, effects, allergies);
                    }
                    counter++;
                }
                else if (order == 11)
                {
                    var temp = EnterNoskhe(ref noskhe);
                    if (temp)
                    {
                        Console.WriteLine("noskhe created .");
                    }
                    else
                    {
                        Console.WriteLine("dobare talash konid .");
                    }
                    counter++;
                }
                else if (order == 12)
                {
                    CheckEffects(noskhe, effects);
                }
                else if (order == 13)
                {
                    var y = new Stopwatch();
                    y.Start();
                    noskhe.Clear();
                    Console.WriteLine("cleared");
                    y.Stop();
                    Console.WriteLine("time : " +
                                      y.ElapsedMilliseconds * 1000 + " Micros");
                    counter++;
                }
                if (order == 10)
                    break;
            }
        }
        private static void Main()
        {
            UserInterface();
        }
    }
}