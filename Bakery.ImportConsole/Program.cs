using System;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Persistence;

namespace Bakery.ImportConsole
{
    class Program
    {

        static async Task Main()
        {
            await InitDataAsync();

            Console.WriteLine();
            Console.Write("Beenden mit Eingabetaste ...");
            Console.ReadLine();
        }

        private static async Task InitDataAsync()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("          Import");
            Console.WriteLine("***************************");
            Console.WriteLine("Import der Bäckerei-Daten in die Datenbank");
            await using var unitOfWork = new UnitOfWork();
            Console.WriteLine("Datenbank löschen");
            await unitOfWork.DeleteDatabaseAsync();
            Console.WriteLine("Datenbank migrieren");
            await unitOfWork.MigrateDatabaseAsync();

            Console.WriteLine("Daten werden von csv-Dateien eingelesen");
            var products = ImportController.ReadFromCsv();
            Console.WriteLine($"  {products.Count()} Produkte eingelesen");

            await unitOfWork.Products.AddRangeAsync(products);

            Console.WriteLine("Produkte werden in Datenbank gespeichert (persistiert)");
            await unitOfWork.SaveChangesAsync();


            var cntProducts = await unitOfWork.Products.GetCountAsync();
            var cntCustomers = await unitOfWork.Customers.GetCountAsync();
            var cntOrders = await unitOfWork.Orders.GetCountAsync();
            var cntOrderItems = await unitOfWork.OrderItems.GetCountAsync();
            Console.WriteLine($"  Es wurden {cntProducts} Produkte gespeichert!");
            Console.WriteLine($"  Es wurden {cntCustomers} Kunden gespeichert!");
            Console.WriteLine($"  Es wurden {cntOrders} Bestellungen gespeichert!");
            Console.WriteLine($"  Es wurden {cntOrderItems} Bestellpositionen gespeichert!");

        }
    }
}
