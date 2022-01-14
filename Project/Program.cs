using System;
using System.IO;
using System.Collections;

namespace Project
{
    class Program
    {
        public static void Save (string[] diseases , string[] NewDiseases, string [] alergies, string[] NewAlergies , string[] effects, string[] NewEffects , Hashtable drugs )
        {
            File.Delete(@"../datasets/drugs.txt");
            StreamWriter WriteDrugs = new StreamWriter(@"../datasets/drugs.txt");
            // save changes and add new things
            File.WriteAllLines(@"../datasets/diseases.txt", diseases);
            StreamWriter writeNewDis = new StreamWriter(@"../datasets/diseases.txt");
            //File.AppendAllLines(@"../datasets/diseases.txt", NewDiseases);
            foreach(var item in diseases)
            {
                writeNewDis.WriteLine(item);
            }
            foreach (var item in NewDiseases)
            {
                writeNewDis.WriteLine(item);
            }
            writeNewDis.Close();
            File.WriteAllLines(@"../datasets/effects.txt", effects);
            File.AppendAllLines(@"../datasets/effects.txt", NewEffects);
            foreach(DictionaryEntry item in drugs)
            {
                WriteDrugs.WriteLine("{0} : {1}", item.Key.ToString(), item.Value.ToString() );
            }
            File.WriteAllLines(@"../datasets/alergies.txt", alergies);
            File.AppendAllLines(@"../datasets/alergies.txt", NewAlergies);
            WriteDrugs.Close();
        }
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            string Line = "";
            int counter = 0;
            string[] divider2 = new string[2];
            // read from memory
            string[] diseases = File.ReadAllLines(@"../datasets/diseases.txt");
            string[] alergies = File.ReadAllLines(@"../datasets/alergies.txt");
            string[] effects = File.ReadAllLines(@"../datasets/effects.txt");
            var drugs = new Hashtable();
            StreamReader reader = new StreamReader(@"../datasets/drugs.txt");
            while ((Line = reader.ReadLine()) != null)
            {
                counter++;
                divider2 = Line.Split(' ' , ':');
                drugs.Add(counter, $"{divider2[0]}{divider2[1]}");
            }
            reader.Close();
            // for new data
            string[] NewDiseases = new string[100]; // can stroe 50 values
            string[] NewAlergies = new string[100]; // can store about 25 values 
            string[] NewEffects = new string[100]; // can store about 50 values 
            // making null
            for(int i = 0; i < 100; i++)
            {
                NewAlergies[i] = "";
                NewEffects[i] = "";
                NewEffects[i] = "";
            }

            drugs.Add(112345671, "asd");
            drugs.Add(11234561231, "2sd");
            drugs.Add(11234531, "2123sd");
            drugs.Add(11234532, "2124sd");
            NewDiseases[0] = "asghar";
            Console.WriteLine("Hello World!");


            // save all changes
            Save(diseases, NewDiseases, alergies, NewAlergies, effects, NewEffects, drugs);
            DateTime end = DateTime.Now;
            Console.WriteLine((end - start));
        }
    }
}
