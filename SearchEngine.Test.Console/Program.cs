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
                //Cmd<MyContext> cmd = new Cmd<MyContext>();

                //ICmd<Context> foo = cmd as ICmd<Context>;
                //Console.WriteLine(foo.GetName());
                TestHelper.CreateDataReader();
                //TestHelper.TestMessageQueue();
                //TestHelper.TestDataConfig();
                //TestHelper.TestSerializeDataConfig();
                //TestHelper.TestWorkflow();
                //TestHelper.ReadFromQueue();

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
