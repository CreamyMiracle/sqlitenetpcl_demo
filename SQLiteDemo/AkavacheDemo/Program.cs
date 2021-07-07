using Akavache;
using Common.Models;
using System;
using System.Reactive.Linq;   // IMPORTANT - this makes await work!
using System.Threading;
using System.Threading.Tasks;

namespace AkavacheDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Akavache.Registrations.Start("AkavacheDemo");
            Demo().Wait();
        }

        private static async Task Demo()
        {
            #region Create cached objects
            ParentObject parent1 = new ParentObject() { Name = "PARENT_1_LOCAL" };

            ChildObject child1 = new ChildObject() { Name = "CHILD_1_LOCAL" };
            ChildObject child2 = new ChildObject() { Name = "CHILD_2_LOCAL" };

            parent1.Children.Add(child1);
            parent1.Children.Add(child2);
            #endregion

            Console.WriteLine("INSERT: Press any key to write objects to local database with 5 seconds expiration");
            Console.ReadKey();

            DateTimeOffset insertExpiration = DateTimeOffset.UtcNow.AddSeconds(5);
            await BlobCache.LocalMachine.InsertObject("parent_1", parent1, insertExpiration);
            Console.WriteLine("Inserted: " + parent1.Name + " with children " + parent1.Children[0].Name + " and " + parent1.Children[1].Name);

            Console.WriteLine("INSERT: Press any key to get objects from local database (<5 seconds) or from server (>=5 seconds)");
            Console.ReadKey();
            DateTimeOffset fetchExpiration = DateTimeOffset.UtcNow.AddSeconds(5);
            ParentObject fetched = await BlobCache.LocalMachine.GetOrFetchObject<ParentObject>("parent_1", SimulateAPICall, fetchExpiration);
            Console.WriteLine("Inserted: " + fetched.Name + " with children " + fetched.Children[0].Name + " and " + fetched.Children[1].Name);
        }

        private static async Task<ParentObject> SimulateAPICall()
        {
            // This method may fetch the data from e.g. REST API or a local database
            // For now this method is just simulating such scenario with 1000 ms delay

            await Task.Run(() => Thread.Sleep(1000));
            ParentObject parent1 = new ParentObject() { Name = "PARENT_1_FROM_SERVER" };

            ChildObject child1 = new ChildObject() { Name = "CHILD_1_FROM_SERVER" };
            ChildObject child2 = new ChildObject() { Name = "CHILD_2_FROM_SERVER" };

            parent1.Children.Add(child1);
            parent1.Children.Add(child2);

            return parent1;
        }
    }
}
