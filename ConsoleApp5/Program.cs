using System;
using System.IO;
using System.Text;
using System.Net;
using System.Text.Json;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using DAL;
using System.Linq;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start getting data...");
            ParseAndSaveDataToDB();
            Console.ReadLine();
        }

        static void ParseAndSaveDataToDB()
        {
            try
            {
                var dal = new DealsDAL();
                int dealsTotalCount = GetTotalDealsCount();
                Console.WriteLine("Total amount of deals on portal: {0}", dealsTotalCount);
                int dealsCountInDB = dal.GetDealsCount();
                Console.WriteLine("Total amount of deals in DB: {0}", dealsCountInDB);

                //check for new deals
                if (dealsTotalCount > dealsCountInDB)
                {
                    Console.WriteLine("Start getting new data from portal...");
                    var dealNumberFromDB = dal.GetDealNumbers();
                    string data = RunPowerShellScript(@"C:\Users\DOTNET\source\repos\ConsoleApp5\GetDeals.ps1", dealsTotalCount);
                    var json = data.Substring(data.IndexOf("content") + 9, data.Length - data.IndexOf("content") - 48);

                    //parse json and save to db
                    using var doc = JsonDocument.Parse(json);
                    JsonElement root = doc.RootElement;

                    var deals = root.EnumerateArray();
                    while (deals.MoveNext())
                    {
                        var deal = deals.Current;
                        var props = deal.EnumerateObject();

                        while (props.MoveNext())
                        {
                            var prop = props.Current;
                            if (prop.Name == "dealNumber")
                            {
                                decimal dealNumber = Convert.ToDecimal(prop.Value.ToString());
                                if (dealNumberFromDB.Where(n => n == Convert.ToDecimal(prop.Value.ToString())).Count() == 0)
                                {
                                    dal.AddDeal(new Deal() { DealNumber = dealNumber, Data = deal.ToString() });
                                    Console.WriteLine("Deal with number {0} has been added to DB", dealNumber);
                                }
                                else
                                {
                                    Console.WriteLine("Deal with number {0} is already in DB", dealNumber);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
        }

        static string RunPowerShellScript(string scriptPath, int dealsAmount = 20)
        {
            var script = File.ReadAllText(scriptPath);
            if (dealsAmount != 20)
                script = script.Replace(":77", ":" + dealsAmount);

            // Create a runspace.
            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace())
            {
                myRunSpace.Open();
                using (PowerShell powershell = PowerShell.Create())
                {
                    // Create a pipeline with the Get-Command command.
                    powershell.AddScript(script);

                    // execute the script
                    var results = powershell.Invoke();
                    powershell.Streams.ClearStreams();
                    powershell.Commands.Clear();
                    // convert the script result into a single string
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (PSObject obj in results)
                    {
                        stringBuilder.AppendLine(obj.ToString());
                    }

                    return stringBuilder.ToString();
                }
            }
        }

        static int GetTotalDealsCount()
        {
            string data = RunPowerShellScript(@"C:\Users\DOTNET\source\repos\ConsoleApp5\GetTotal.ps1");

            int totalEnd = data.IndexOf("number") - 2;
            int totalStart = data.IndexOf("total") + 7;
            var totalDealsCout = Convert.ToInt32(data.Substring(totalStart, totalEnd - totalStart));
            return totalDealsCout;
        }
    }
}
