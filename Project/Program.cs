using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

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
        public static void SaveAlergies(Hashtable alergies)
        {
            var form = "";
            var counter = 0;
            File.Delete(@"../datasets/alergies.txt");
            var writerAlergies = new StreamWriter(@"../datasets/alergies.txt");
            foreach (DictionaryEntry item in alergies)
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
            // drugs
            
        }

        public static List<string> ReadFromDisease()
        {
            var diseases = new List<string>();
            diseases.AddRange(File.ReadAllLines(@"../datasets/diseases.txt"));

            return diseases;
        }
        public static Hashtable ReadFromAlergies()
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
                //drugs.Add(counter, $"{divider2[0]}{divider2[1]}");
                drugs.Add(divider2[0], divider2[3]);
            }
            reader.Close();

            return drugs;
        }
        private static void Main()
        {
            DateTime start = DateTime.Now;
            // read from memory
            var diseases = ReadFromDisease();
            var alergies = ReadFromAlergies();
            var effects = ReadFromEffects();
            var drugs = ReadFromDrugs();
            // reading Ends

            effects["Drug_pxouyeenru"] = new Hashtable {{"adultCold", "exelent"}};
            foreach (DictionaryEntry item in effects)
            {
                Hashtable t = (Hashtable) item.Value;
                if (t.ContainsKey("Drug_pxouyeenru"))
                    t.Remove("Drug_pxouyeenru");

            }

            // save all changes
            SaveDrugs(drugs);
            SaveDiseases(diseases);
            SaveEffects(effects);
            SaveAlergies(alergies);
            

            DateTime end = DateTime.Now;
            Console.WriteLine((end - start));
        }
    }
}