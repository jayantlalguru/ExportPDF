using ExportPDF;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportToPDFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //PDFCreator.DemoTableSpacing();
            //return;
            //PDFCreator.CreatePdf();
            List<TransactionDetail> transactionDetails = new List<TransactionDetail>();            
            string conString = @"Data Source=JAYANTGURU-PC\SQLSERVER2017;Initial Catalog=Practice;Persist Security Info=True;User ID=sa;Password=guru@000";
            using (SqlConnection sqlConn = new SqlConnection(conString))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(@"SELECT FolioNumber
                                                                    ,ClientCode
                                                                    ,ClientName
                                                                    ,SchemeName
                                                                    ,AMCName
                                                                    ,AssetClass
                                                                    ,Category
                                                                    ,TransactionType
                                                                    ,SubType
                                                                    ,TransactionDate
                                                                    ,TransactionNumber
                                                                    ,Units
                                                                    ,Price
                                                                    ,Amount
                                                                   FROM [Practice].[dbo].[TransactionDetail]", sqlConn))
                {
                    using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                    {
                        // get the data and fill rows 5 onwards
                        while (sqlReader.Read())
                        {
                            TransactionDetail transactionDetail = new TransactionDetail();
                            transactionDetail.FolioNumber = sqlReader["FolioNumber"] != DBNull.Value ? Convert.ToInt64(sqlReader["FolioNumber"]) : 0;
                            transactionDetail.ClientCode = sqlReader["ClientCode"] != DBNull.Value ? sqlReader["ClientCode"].ToString() : string.Empty;
                            transactionDetail.ClientName = sqlReader["ClientName"] != DBNull.Value ? sqlReader["ClientName"].ToString() : string.Empty;
                            transactionDetail.SchemeName = sqlReader["SchemeName"] != DBNull.Value ? sqlReader["SchemeName"].ToString() : string.Empty;
                            transactionDetail.AMCName = sqlReader["AMCName"] != DBNull.Value ? sqlReader["AMCName"].ToString() : string.Empty;
                            transactionDetail.AssetClass = sqlReader["AssetClass"] != DBNull.Value ? sqlReader["AssetClass"].ToString() : string.Empty;
                            transactionDetail.Category = sqlReader["Category"] != DBNull.Value ? sqlReader["Category"].ToString() : string.Empty;
                            transactionDetail.TransactionType = sqlReader["TransactionType"] != DBNull.Value ? sqlReader["TransactionType"].ToString() : string.Empty;
                            transactionDetail.SubType = sqlReader["SubType"] != DBNull.Value ? sqlReader["SubType"].ToString() : string.Empty;
                            transactionDetail.TransactionDate = sqlReader["TransactionDate"] != DBNull.Value ? sqlReader["TransactionDate"].ToString() : string.Empty;
                            transactionDetail.TransactionNumber = sqlReader["TransactionNumber"] != DBNull.Value ? Convert.ToInt64(sqlReader["TransactionNumber"]) : 0;
                            transactionDetail.Units = sqlReader["Units"] != DBNull.Value ? float.Parse(sqlReader["Units"].ToString()) : 0;
                            transactionDetail.Price = sqlReader["Price"] != DBNull.Value ? float.Parse(sqlReader["Price"].ToString()) : 0;
                            transactionDetail.Amount = sqlReader["Amount"] != DBNull.Value ? float.Parse(sqlReader["Amount"].ToString()) : 0;
                            transactionDetails.Add(transactionDetail);
                        }
                        sqlReader.Close();
                    }
                }
                sqlConn.Close();
            }
            DataTable dataTable = Utils.ToDataTable(transactionDetails);            
            PDFCreator.Create(dataTable, @"D:\Work\Projects\DotNet\PracticeProjects\ExportToPDFTest\ExportToPDFTest\Pdfs\TransPDFdocument.pdf");
        }
    }
}
