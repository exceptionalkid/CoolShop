using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.Script
{
    internal class Program
    {
        const string CommandMessage = "Please Send Me FilePath<SPACE>ColumnNumber<SPACE>SearchKey";
        static void Main(string[] args)
        {
            try
            {
                //Do we have file path , column number & search key?
                if (args.Length == 3)
                {
                    //Do we have valid file path?
                    if(string.IsNullOrEmpty(args[0].Trim()))
                    {
                        Console.WriteLine(@"Send me a valid file path! Ex:- C:\Documents\File1.csv");
                        return;
                    }

                    //Do we have valid search key?
                    if (string.IsNullOrEmpty(args[2].Trim()))
                    {
                        Console.WriteLine(@"Send me a search key! Ex:- Alice");
                        return;
                    }

                    //Do we have valid column number ?
                    if (int.TryParse(args[1], out int ColumnNumber))
                    {
                        //We have valid commands
                        //Do we have valid file in our path?
                        string[] RowDatas = GetDataFromFile(filePath: args[0]);
                        if(RowDatas != null)
                        {

                            //We have data in our file
                            //Lets take all the column values in each rows
                            List<string[]> Rows = new List<string[]>();
                            foreach (string Row in RowDatas)
                            {
                                Rows.Add(Row.Split(','));
                            }

                            //Go for search
                            Search(Rows, ColumnNumber, searchKey: args[2]);

                            Console.WriteLine("Thats it! Press any key to exit.");
                            

                        }

                    }
                    else
                        Console.WriteLine("Send me a valid Column Number! Ex:- 3");
                }
                else
                {
                    Console.WriteLine(CommandMessage);
                }
                Console.ReadKey();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Something went wrong!");
            }
        }

        static string[] GetDataFromFile(string filePath)
        {
            try
            {
                return File.ReadAllLines(filePath);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(string.Format("Something wrong! I cannot read data from the file! {0}", Ex.Message));
                return null;
            }
        }
        static void Search(List<string[]> rowsCollection, int columnNumber, string searchKey)
        {
            try
            {
                bool AnyMatches = false;
                rowsCollection.ForEach(eachRow =>
               {
                   //Do we have element in this column number?
                   if (eachRow.Length > columnNumber)
                   {
                       //Yes we have element, Lets compare with our key
                       if (eachRow[columnNumber].Equals(searchKey))
                       {
                           Console.WriteLine(" ---- Match found ---- ");
                           Console.WriteLine(String.Join(",", eachRow)); //We found a result, print it.
                           AnyMatches = true;
                           //check for other matches.
                       }
                   }
               });
                if (!AnyMatches)
                    Console.WriteLine("No matches");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(string.Format("Something wrong! I cannot search data! {0}", Ex.Message));
            }
        }
    }
}
