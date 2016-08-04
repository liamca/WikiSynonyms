using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace WikiSynonym
{
    class Program
    {
        public static SQLiteConnection sqlite;
    
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                Console.WriteLine("Usage: WikiSynonym [word]");
                return;
            }

            string word = args[0].ToLower();
            sqlite = new SQLiteConnection("Data Source=synonyms.db");
            string query = "with SynonymList as " +
                "( select synonym from synonyms where root = '" + word + "' " +
                "union " +
                "select synonym from synonyms where root in ( " +
                "  select root from synonyms where synonym = '" + word + "' " +
                ")) " +
                "select distinct synonym from SynonymList order by synonym "; 
            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db

                using (SQLiteCommand fmd = sqlite.CreateCommand())
                {
                    fmd.CommandText = query;
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        Console.WriteLine(r["synonym"]);
                    }

                    //    cmd = sqlite.CreateCommand();

                    //SQLiteDataReader r = fmd.ExecuteReader();
                    //while (r.Read())
                    //{
                    //    ImportedFiles.Add(Convert.ToString(r["FileName"]));

                    //}

                    //cmd.CommandText = query;  //set the passed query
                    //ad = new SQLiteDataAdapter(cmd);
                    //ad.Fill(dt); //fill the datasource
                }
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            sqlite.Close();
            return;
        }
    }
}
