using System.Linq;
using System.Data.Entity;
using CountCustomers.Enums;

namespace CountCustomers;

class Program
{
    static void Main(string[] args)
    {
        //InsertData();
        QueryData();
    }

    private static void QueryData()
    {

        var years = new int[] { 2020, 2021, 2022 };
        var months = new int[12];
        for (int i = 0; i < 12; i++) { months[i] = i+1; }
        var purchases = new PurchasesDB();
        var custIdList = new List<int>();

        foreach (var item in purchases.Purchases.OrderBy(x => x.CustomerID))
        {
            if (custIdList.Contains(item.CustomerID))
            {
                continue;
            }
            else
            {
                custIdList.Add(item.CustomerID);
            }
        }

        var dictQuery = new Dictionary<string, double>();
        foreach (var year in years)
        {
            foreach (var month in months)
            {
                var key = $"{year}/{month}";
                dictQuery.Add(key, 0);

            }
        }

        

        foreach (var customerId in custIdList)
        {
            var dictMonths = new Dictionary<string, double>();
            foreach (var year in years)
            {
                foreach(var month in months)
                {
                    var key = $"{year}/{month}";
                    dictMonths.Add(key, 0);
                    
                }
            }
            foreach (var ticket in purchases.Purchases)
            {
                if (ticket.CustomerID == customerId)
                {
                    var key = $"{ticket.PaymentTS.Year}/{ticket.PaymentTS.Month}";
                    dictMonths[key] += ticket.Amount;
                }
            }
            foreach (var item in dictMonths)
            {
                if (item.Value > 20)
                {
                    dictQuery[item.Key]++;
                }
            }
        }

        foreach (var item in dictQuery)
        {
            Console.WriteLine($"{item.Key} : {item.Value}");
        }
        
        var query = 
            from ticket in purchases.Purchases
            group ticket by ticket.CustomerID into g
            orderby g.Key
            select g;

        foreach (var group in query)
        {
            Console.WriteLine(group.Key);
            foreach (var ticket in group.OrderBy(d => d.PaymentTS))
            {
                Console.WriteLine($"\t{ticket.CustomerID} {ticket.Id} {ticket.PaymentTS}: {ticket.Amount}");
            }
        }

    }

    private static void InsertData()
    {
        var customers = ProcessCustomers("Data2.csv");
        var db = new PurchasesDB();

        if (!db.Purchases.Any())
        {
            int i = 0;
            foreach (var customer in customers)
            {
                db.Purchases.Add(customer);
                Console.WriteLine($"Inserido {i}");
                i++;
            }
            db.SaveChanges();
        }
    }

    private static List<Purchase> ProcessCustomers(string path)
    {
        var customer = new Purchase();
        return 
            File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .Select(customer.ParseFromCsv)
                .ToList();

    }
}
