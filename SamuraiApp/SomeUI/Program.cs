using System;
using SamuraiApp.Domain;
using SamuraiApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SomeUI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurai();

            //InsertMultipleDifferentObjects();

            //MoreQuerirs();

            //RetrieveAndUpdateMultipleSamurai();

            //Update and add both
            //MultipleDataBaseOperations();

            //Disconnected or web scenarios, where object changes "context scope"...
            //QueryAndUpdateBattle_Disconnected();

            //Add Battle.
            //AddBattle();

            //Delete is Remove 
            //DeleteWhileTracked();

            DeleteWhileNotTracked();
        }

        private static void AddBattle()
        {
            _context.Battles.Add(new Battle { Name = "Battle1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) });
            _context.SaveChanges();
        }

        private static void DeleteWhileNotTracked()
        {
            var battle = _context.Battles.FirstOrDefault();

            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Remove(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void DeleteWhileTracked()
        {
            var battle = _context.Battles.FirstOrDefault();
            _context.Remove(battle);
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = DateTime.Now.AddDays(1);

            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void MultipleDataBaseOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault(x=> EF.Functions.Like(x.Name, "Babu%"));
            samurai.Name += " Soniupdated";

            _context.Samurais.Add(new Samurai { Name = "Rampal"});

            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurai()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(x => x.Name += "Soni");
            _context.SaveChanges();
        }

        private static void MoreQuerirs()
        {
            // var samurais = _context.Samurais.Where(x => x.Name == "Julie").ToList();

            //Like operator
            var samurais = _context.Samurais.Where(x => EF.Functions.Like(x.Name, "J%")).ToList();

            //contains
            var samurais1 = _context.Samurais.Where(x => x.Name.Contains("J")).ToList();
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
