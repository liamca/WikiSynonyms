using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSQLConverter
{
    class Program
    {
        private static string SQLConnectionString;
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: WikiSQLConverter.exe [File path of wiki SQL file] [SQL Server Connection String]");
                return;
            }

            SQLConnectionString = args[1];
            ExtractDataToTXT(args[0]);

        }

        static void ExtractDataToTXT(string FileName)
        {
            string line;
            int counter = 0;
            bool FoundData = false;
            string InsertString;
            string InsertIntoString = string.Empty;
            string ParsedLine = string.Empty;
            string DataLine = string.Empty;
            // Read the file and display it line by line.
            StreamReader file = new System.IO.StreamReader(FileName);
            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {
                    conn.Open();
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.IndexOf("VALUES (") > -1)
                        {
                            FoundData = true;
                            InsertIntoString = line.Substring(0, line.IndexOf("VALUES (") + 7).Replace("`", "");
                            line = line.Substring(line.IndexOf("VALUES (") + 7);
                        }

                        if ((FoundData) && (line.IndexOf(",") > -1))
                        {
                            InsertString = line.Replace("`", "");
                            InsertString = InsertString.Replace("\\'", "''");
                            while (InsertString.Length > 0)
                            {
                                // Break into a smaller batch of 500 items
                                int currIndexLoc = 0;
                                for (int rowCounter = 0; rowCounter < 500; rowCounter++)
                                {
                                    counter++;
                                    currIndexLoc = InsertString.IndexOf("),(", currIndexLoc + 1);
                                }

                                if (counter == 1963500)
                                {
                                    Console.Write("aa");
                                }

                                string query = InsertIntoString;
                                if (currIndexLoc > -1)
                                {
                                    query += InsertString.Substring(0, currIndexLoc + 1);
                                    InsertString = InsertString.Substring(currIndexLoc + 2);
                                }
                                else
                                {
                                    query += InsertString;
                                    InsertString = string.Empty;
                                }
                                try
                                {
                                    SqlCommand cmd = new SqlCommand(query, conn);
                                    cmd.ExecuteNonQuery();
                                    Console.WriteLine(counter);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }

                        }

                    }
            }
            file.Close();

        }
    }
}
