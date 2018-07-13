using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportToPDFTest
{
    public class TransactionDetail
    {
        public Int64 FolioNumber { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string SchemeName { get; set; }
        public string AMCName { get; set; }
        public string AssetClass { get; set; }
        public string Category { get; set; }
        public string TransactionType { get; set; }
        public string SubType { get; set; }
        public string TransactionDate { get; set; }
        public Int64 TransactionNumber { get; set; }
        public float Units { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
    }
}
