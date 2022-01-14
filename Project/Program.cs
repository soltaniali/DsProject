using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Project
{
    class Program
    {
        public static void Save (List<string> diseases , Hashtable alergies , Hashtable effects , Hashtable drugs )
        {
            // save changes and add new things
            //diseases
            StreamWriter writeNewDis = new StreamWriter(@"../datasets/diseases.txt");
            foreach(var item in diseases)
            {
                writeNewDis.WriteLine(item);
            }
            writeNewDis.Close();
            // effects
            File.Delete(@"../datasets/effects.txt");
            StreamWriter writerEffects = new StreamWriter(@"../datasets/effects.txt");
            int counter = 0;
            string form = "";
            foreach (DictionaryEntry item in effects)
            {
                form = item.Key.ToString();
                form += " :";
                foreach(DictionaryEntry obj in (Hashtable)item.Value)
                {
                    counter++;
                    form += " (";
                    form += obj.Key.ToString();
                    form += ",";
                    form += obj.Value.ToString();
                    form += ") ";
                    if(counter < ((Hashtable)item.Value).Count)
                    {
                        form += ";";
                    }
                }
                writerEffects.WriteLine(form);
                form = "";
            }
            writerEffects.Close();
            //alergies
            File.Delete(@"../datasets/alergies.txt");
            StreamWriter writerAlergies = new StreamWriter(@"../datasets/alergies.txt");
            foreach (DictionaryEntry item in alergies)
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
                writerAlergies.WriteLine(form);
                form = "";
            }
            writerAlergies.Close();
            // drugs
            File.Delete(@"../datasets/drugs.txt");
            StreamWriter WriteDrugs = new StreamWriter(@"../datasets/drugs.txt");
            foreach (DictionaryEntry item in drugs)
            {
                WriteDrugs.WriteLine("{0} : {1}", item.Key.ToString(), item.Value.ToString() );
            }
            WriteDrugs.Close();
        }
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            string Line = "";
            int counter = 0;
            string[] divider2 = new string[2];
            // read from memory
            List<string> diseases = new List<string>();
            diseases.AddRange( File.ReadAllLines(@"../datasets/diseases.txt"));

            var alergies = new Hashtable();
            StreamReader readerAlergies = new StreamReader(@"../datasets/alergies.txt");
            while ((Line = readerAlergies.ReadLine()) != null)
            {
                string[] tempstring = Line.Split(' ', ':', ';' , ')' , '(');
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
            //string[] alergies = File.ReadAllLines(@"../datasets/alergies.txt");

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
            //string[] effects = File.ReadAllLines(@"../datasets/effects.txt");

            var drugs = new Hashtable();
            StreamReader reader = new StreamReader(@"../datasets/drugs.txt");
            while ((Line = reader.ReadLine()) != null)
            {
                counter++;
                divider2 = Line.Split(' ' , ':');
                drugs.Add(counter, $"{divider2[0]}{divider2[1]}");
            }
            reader.Close();
            // reading Ends



            drugs.Add(112345671, "asd");
            drugs.Add(11234561231, "2sd");
            drugs.Add(11234531, "2123sd");
            drugs.Add(11234532, "2124sd");
           // NewDiseases[0] = "asghar";
            Console.WriteLine("Hello World!");


            // save all changes
            //Save(diseases, NewDiseases, alergies, NewAlergies, effects, NewEffects, drugs);
            DateTime end = DateTime.Now;
            Console.WriteLine((end - start));
        }
    }
}
