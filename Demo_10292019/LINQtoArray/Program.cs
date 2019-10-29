using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQtoArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Fun with LINQ to Objects ****\n");
            // define an array of strings
            string[] currentVideoGames = {"Morrowind", "Uncharted 2", "Fallout 3",
                                            "Dexter", "System Shock 2"};

            // desired query
            // games that have a space in the title

            #region old-fashioned way
            string[] result = QueryOverStringsLonghand(currentVideoGames);
            Console.WriteLine("Returned results from longhand version");
            foreach(string s in result)
            {
                Console.WriteLine("Item: {0}", s);
            }
            #endregion

            #region let's try the same thing using LINQ
            List<string>  listResult = QueryOverStrings(currentVideoGames);
            Console.WriteLine("Returned results from query method");
            foreach(string s in listResult)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();
            #endregion
        }

        #region old fashioned way
        static string[] QueryOverStringsLonghand(string[] s)
        {
            string[] resultsWithSpaces = new string[s.Length];

            // find results
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(" "))
                    resultsWithSpaces[i] = s[i];
            }

            // sort results
            Array.Sort(resultsWithSpaces);

            // print results
            Console.WriteLine("Immediate results from longhand version:");
            foreach(string s1 in resultsWithSpaces)
            {
                // if an if/for etc. has only one line of body then 
                // curly braces aren't required - compiler approves
                if(s1 != null)
                    Console.WriteLine("Item: {0}",s1);
                // Console.WriteLine("blah"); - this is outside of the if statement
            }
            Console.WriteLine();

            // generate a return array
            // figure out size 
            int count = 0;
            foreach(string s2 in resultsWithSpaces)
            {
                if (s2 != null) count++;
            }

            // create an array
            string[] outputArray = new string[count];

            // populate output
            count = 0;
            foreach(string s3  in resultsWithSpaces)
            {
                if (s3 != null)
                {
                    outputArray[count] = s3;
                    count++;
                }
            }

        }
        #endregion

        #region LINQ
        static List<string> QueryOverStrings(string[] inputArray)
        {
            // example of defferred execution
            // build query
            // from textbook:
            // IEnumerable<string> subset = from ...
            var subset = from game in inputArray
                         where game.Contains(" ")
                         orderby game
                         select game;

            // print results
            ReflectOverQueryResults(subset, "Query Expression");

            // print results
            Console.WriteLine("Immediate results using LINQ query");
            foreach(var s in subset)
            {
                Console.WriteLine("Item: {0}",s);
            }
            Console.WriteLine();

            // demo reuse of query
            // data change:
            inputArray[0] = "some string";
            // same for loop as lines 103-108
            Console.WriteLine("Immediate results using LINQ query after change to data");
            foreach (var s in subset)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();

            // demo returning results - immediate execution
            List<string> outputList = (from game in inputArray
                                       where game.Contains(" ")
                                       orderby game // is this casting? (below)
                                       select game).ToList<string>();

            return outPutList;

            static void ReflectOverQueryResults(object resultSet, string queryType)
            {
                Console.WriteLine("*** query type: {0}", queryType);
                Console.WriteLine("resultSet is of type: {0}", resultSet.GetType().Name);
                Console.WriteLine("resultSet location: {0}", resultSet.GetType().Assembly.GetName().Name);
            }
        }
        #endregion
    }
}
