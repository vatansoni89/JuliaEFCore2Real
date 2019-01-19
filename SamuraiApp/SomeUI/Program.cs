using System;
using SamuraiApp.Domain;
using SamuraiApp.Data;
using Microsoft.EntityFrameworkCore;

namespace SomeUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurai();

            InsertMultipleDifferentObjects();
        }

        private static void InsertMultipleDifferentObjects()
        {
            var samurai = new Samurai { Name = "Dhruv" };

            var battle = new Battle
            {
                Name = "Battle of Nagashino",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            using (var context = new SamuraiContext())
            {
                //Different data types can be added at once.
                //Even we can directly add to context object.
                context.AddRange(samurai, battle);
                context.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {  
            var samurai = new Samurai { Name = "Julie" };
            var samu1 = new Samurai { Name = "Dardar" };
            using (var context = new SamuraiContext())
            {
                //context.Samurais.Add(samurai);
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }
        private static void InsertMultipleSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            var samu1 = new Samurai { Name = "Dardar" };
            using (var context = new SamuraiContext())
            {
                //context.Samurais.Add(samurai);
                context.Samurais.AddRange(samurai, samu1);
                context.SaveChanges();
            }
        }

    }
}
