using System.Runtime.InteropServices;
using System;

class Program()
{
    public static void Main(string[] arg)
    {
        // Enter your MySQL Info
        string server = "localhost";
        string user = "user";   
        string password = "pass";
        string database = "name";


        MySqlDB.EnsureDatabaseAndTable(server, user, password, database);


        string connectionString = $"server={server};user={user};password={password};database={database};";

        ProductManager productManager = new ProductManager(connectionString);
        bool TF  = true;
        while (TF)
        {
            Console.WriteLine("Welcome to Product Manager.");
            Console.WriteLine("Options,");
            Console.WriteLine("1- Adding a Product for Press 1");
            Console.WriteLine("2- Updating a Product for Press 2");
            Console.WriteLine("3- Deleting a Product for Press 3");
            Console.WriteLine("4- See Products for Press 4");
            Console.WriteLine("5- Exit the Program for Press 5");
            Console.Write("Girdiniz: ");
            string input = Console.ReadLine();
            int SwitchValue = int.Parse(input);

            switch (SwitchValue)
            {
                case 1: productManager.Add(); break;
                case 2: productManager.Update(); break;
                case 3: productManager.Remove(); break;
                case 4: productManager.Show(); break;
                case 5: Console.WriteLine("Program is closed."); TF = false; break;
                default: Console.WriteLine("Please enter only the specified numbers."); break;
            }
        }



    }
    
}