using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Test.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //TestHelper.TestMessageQueue();
                //TestHelper.TestDataConfig();
                //TestHelper.TestSerializeDataConfig();
                //TestHelper.TestWorkflow();
                TestHelper.TestDataReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.Write("Done ...");
                Console.Read();
            }
        }
    }
}
