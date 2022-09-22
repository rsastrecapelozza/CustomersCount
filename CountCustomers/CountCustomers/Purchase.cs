using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountCustomers
{
    public class Purchase
    {
        public int Id { get; set; }
        private int unixTime;
        public int PaymentID { get; set; }
        public int CustomerID { get; set; }
        public int StaffID { get; set; }
        public int RentalID { get; set; }
        public double Amount { get; set; }
        public int Timestamp { get; set; } 
        public int UnixTime { get; set; }
        public DateTimeOffset PaymentTS { get; set; }

        public Purchase ParseFromCsv(string line)
        {
            Random r = new Random();
            var start = new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            long startUnix = new DateTimeOffset(start).ToUnixTimeSeconds();
            var end = new DateTime(2022, 12, 31, 0, 0, 0, DateTimeKind.Utc);
            long endUnix = new DateTimeOffset(end).ToUnixTimeSeconds();
            var columns = line.Split(',');
            var unixTime = r.Next((int)startUnix, (int)endUnix);
            return new Purchase
            {
                PaymentID = int.Parse(columns[0]),
                CustomerID = int.Parse(columns[1]),
                StaffID = int.Parse(columns[2]),
                RentalID = int.Parse(columns[3]),
                Amount = double.Parse(columns[4]),
                UnixTime = unixTime,
                PaymentTS = DateTimeOffset.FromUnixTimeSeconds(unixTime)
            };
        }
    }
}
