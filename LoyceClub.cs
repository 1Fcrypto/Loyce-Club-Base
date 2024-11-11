using Donate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LoyceClubBase
{
    class LoyceClub
    {
        private static HashSet<string> Address = new HashSet<string>();

        static void Main(string[] args)
        {
            DonateAddress.Show("Start Loyce Club Base!");
            if (args.Length < 1)
            {
                Console.WriteLine("Input or Output file missing");
                Console.WriteLine("End");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("0-BTC_P2PKH (1u, 1c);\n1-BTC_P2SH (3);\n2-BTC_P2WPKH_P2WSH_P2TR (bc1)");
                try
                {
                    string chose = Console.ReadLine();
                    var choses = chose.Split(' ');
                    Console.WriteLine("Mininmum balance:");
                    string rl = Console.ReadLine();
                    double minimum = Convert.ToDouble(string.IsNullOrEmpty(rl) ? "0" : rl.Replace(',', '.'), new NumberFormatInfo { NumberDecimalSeparator = "." });
                    using (StreamReader sr = new StreamReader(args[0]))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] lines = line.Split('\t');
                            if (lines[0].StartsWith("a") || lines[1].StartsWith("b"))
                                continue;

                            if (choses.Contains("0"))
                            {
                                if (lines[0].StartsWith("1"))
                                    AddAddress(lines[0], lines[1], minimum);
                            }

                            if (choses.Contains("1"))
                            {
                                if (lines[0].StartsWith("3"))
                                    AddAddress(lines[0], lines[1], minimum);
                            }

                            if (choses.Contains("2"))
                            {
                                if (lines[0].StartsWith("bc1"))
                                    AddAddress(lines[0], lines[1], minimum);
                            }
                        }
                    }

                    using (StreamWriter sw = new StreamWriter("base.txt", true, System.Text.Encoding.UTF8))
                    {
                        foreach (var item in Address)
                        {
                            sw.WriteLine(item);
                        }
                    }
                    Address.Clear();
                    Console.WriteLine("End.");
                    Console.ReadKey();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }

        private static void AddAddress(string addres, string bal, double minimum)
        {
            double balance = Convert.ToDouble(bal) / 100000000;
            if (balance > minimum)
                Address.Add(addres);
        }
    }
}
