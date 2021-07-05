using SQLite;
using System;
using System.IO;
using SQLiteNetExtensions.Extensions;
using Common.Models;

namespace SQLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Init
            // Connection
            DirectoryInfo workDir = new DirectoryInfo(Environment.CurrentDirectory);
            string path = workDir.Parent.Parent.Parent.FullName;

            SQLiteConnection con = new SQLiteConnection(Path.Combine(path, "databasedemo.db"));

            con.DropTable<ParentObject>();
            con.DropTable<ChildObject>();

            con.CreateTable<ParentObject>();
            con.CreateTable<ChildObject>();
            #endregion

            // Models
            ParentObject parent1 = new ParentObject() { Name = "PARENT_1" };
            ParentObject parent2 = new ParentObject() { Name = "PARENT_2" };

            ChildObject child1 = new ChildObject() { Name = "CHILD_1" };
            ChildObject child2 = new ChildObject() { Name = "CHILD_2" };
            ChildObject child3 = new ChildObject() { Name = "CHILD_3" };
            ChildObject child4 = new ChildObject() { Name = "CHILD_4" };

            parent1.Children.Add(child1);
            parent1.Children.Add(child2);

            parent2.Children.Add(child3);
            parent2.Children.Add(child4);

            Console.WriteLine("INSERT: Press any key to write PARENT_1 with children CHILD_1 & CHILD_2 to database");
            Console.ReadKey();
            con.InsertWithChildren(parent1, recursive: true);

            Console.WriteLine("INSERT: Press any key to write PARENT_2 with children CHILD_2 & CHILD_3 to database");
            Console.ReadKey();
            con.InsertWithChildren(parent2, recursive: true);

            Console.WriteLine("DELETE: Press any key to delete PARENT_1 with children CHILD_1 & CHILD_2 from database");
            Console.ReadKey();
            con.Delete(parent1, recursive: true);

            Console.WriteLine("DELETE: Press any key to delete PARENT_2 with children CHILD_2 & CHILD_3 from database");
            Console.ReadKey();
            con.Delete(parent2, recursive: true);
        }
    }
}
