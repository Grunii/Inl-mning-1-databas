using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using System.Linq;

namespace DB3
{
    //class Person  // Förklarar för C# att här kommer en ny datatyp: Person
    //              // Install-Package LiteDB
    //{
    //    public string max { get; set; }
    //    public int wage { get; set; }
    //    public string prof { get; set; } // null
    //    public string food { get; set; } // null

    //}
    class user
    {
        public string maxcv { get; set; }
        public int wagecv { get; set; }
        public string profcv { get; set; }
        public string foodcv { get; set; }


    }

    class Program3
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Närvarokontroll! Tryck Q för att avsluta"); // TODO fyll i
                string command = Console.ReadLine();
                if (command == "Q")
                {
                    break;
                }

                using StreamReader reader = new StreamReader("formulär.csv");

                string[] alla_rader = File.ReadAllLines("formulär.csv");

                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();
                    string[] columns = line.Split(",");
                    string max = columns[0];
                    int wage = int.Parse(columns[1]);
                    string prof = columns[2];
                    string food = columns[3];
                    Console.WriteLine(max);
                    Console.WriteLine(wage);
                    Console.WriteLine(prof);
                    Console.WriteLine(food);
                    user x = new user
                    {
                        maxcv = max,
                        wagecv = wage,
                        profcv = prof,
                        foodcv = food
                    };
                    //using (var db = new LiteDatabase("formulär.db"))
                    //{
                    //    var personer = db.GetCollection<user>("formulär");
                    //    personer.Insert(x);
                    //}
                }

            }
            using (var db = new LiteDatabase("formulär.db"))
            {
                var personer = db.GetCollection<user>("formulär");

                //-Från hur många olika städer närvarade människor ?
                var diff_prof = personer.FindAll().GroupBy(x => x.profcv).Count();
                Console.WriteLine("Antal olika jobb: " + diff_prof);

                //-Vad var genomsnittsåldern?
                var totalWage = personer.FindAll().Select(x => x.wagecv).Sum();
                var averageWage = totalWage / personer.FindAll().Count();
                Console.WriteLine("Average expected salary: " + averageWage);

                //klassens favorit Max 

                var dictionary = new Dictionary<string, int>();

                    foreach (var person in personer.FindAll())
                    {
                        if (dictionary.ContainsKey(person.maxcv))
                        {
                            dictionary[person.maxcv]++;
                        }
                        else
                        {
                            dictionary.Add(person.maxcv, 1);
                        }

                    }
                   Console.WriteLine("Vilken max är populärast?");
                    foreach (KeyValuePair<string, int> entry in dictionary)
                    {
                        Console.WriteLine(entry.Key + " : " + entry.Value);


                    }
                    
                var max = dictionary.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                Console.WriteLine("klassens favorit max var: " + max);


                //int[] numbers = { mats, Max1, Max2, Max3 };
                //int biggestNumber = numbers.Max();
                //Console.WriteLine(biggestNumber);

                {

                    }
                }
            }
        }
    }
