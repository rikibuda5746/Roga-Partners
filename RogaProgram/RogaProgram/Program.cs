using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace RogaProgram
{
    internal class Program
    {
        public static List<Person> GenerateDataset(int count)
        {
            Random random = new Random();
            string[] mensFirstNames = { "Gal", "Dan", "Noam", "David", "Elad" };
            string[] womensFirstNames = { "Yael", "Shira", "Michael", "Tamar" };
            string[] lastNames = { "Shteren", "Star", "Levi", "Brown", "Filip", "Miller" };
           
            //nens or womens suitable to the gender
            int firstNameGender;

            List <Person> records = new List<Person>();

            for (int i = 0; i < count; i++)
            {
                firstNameGender=random.Next(2);
                Person person = new Person();

                person.FirstName = firstNameGender == 0 ? mensFirstNames[random.Next(mensFirstNames.Length)]
                : womensFirstNames[random.Next(womensFirstNames.Length)];
                person.LastName = lastNames[random.Next(lastNames.Length)];
                person.Age = random.Next(18, 71);
                person.Weight = random.Next(120,191);
                person.Gender = firstNameGender == 0 ? "Male" : "Female";

                records.Add(person);
            }
            return records;
        }
        public static void WriteToCSV(List<Person> records, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

        public static List<Person> ReadFromCSV(string filePath)
        {
            List<Person> records = new List<Person>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                 records = csv.GetRecords<Person>().ToList();
            }
            return records;
        }

        //Average age of all people
        public static double CalculateAverageAge(List<Person> people)
        {
            int count=0;
            double sum=0;
            foreach (Person person in people)
            {
                sum += person.Age;   
                count++;    
            }
            return count != 0 ? sum/count : 0;
        }

        //The total number of people weighing between 120lbs and 140lbs
        public static int CalculatePeopleCount(List<Person> people)
        {
            int count = 0;
            foreach (Person person in people)
            {
                if (person.Weight >= 120 && person.Weight <= 140)
                {
                    count++;
                }   
            }
            return count;
        }

        // The average age of the people  weighing between 120lbs and 140lbs 
        public static double CalculateAverageAgeInRange(List<Person> people)
        {
            int count = 0;
            double sum = 0;
            foreach (Person person in people)
            {
                if (person.Weight >= 120 && person.Weight <= 140)
                {
                    sum += person.Age;
                    count++;
                }
            }
            return count != 0 ? sum / count : 0;
        }

        static void Main(string[] args)
        {
            List<Person> people = GenerateDataset(5);
            WriteToCSV(people, "dataset.csv");

            List<Person> readPeople = ReadFromCSV("dataset.csv");

            double averageAge = CalculateAverageAge(readPeople);
            int peopleCount = CalculatePeopleCount(readPeople);
            double averageAgeInRange = CalculateAverageAgeInRange(readPeople);

            Console.WriteLine($"Average age of all people: {averageAge}");
            Console.WriteLine($"Total number of people weighing between 120lbs and 140lbs: {peopleCount}");
            Console.WriteLine($"Average age of people weighing between 120lbs and 140lbs: {averageAgeInRange}");
        }
    }
}
