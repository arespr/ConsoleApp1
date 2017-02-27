using System;
using System.Diagnostics;
using System.Numerics;

namespace ConsoleApp1
{
    class Program
    {
        private const byte ByteSpace = (byte)' ';
        private const byte ByteQuestionMark = (byte)'?';
        private const byte BytePercentage = (byte)'%';

        static void Main(string[] args)
        {

            int iterations = 100000000;
            //iterations = 1;



            var sw = new Stopwatch();

            string aaa; KnownMethods.GetKnownMethod(KnownMethods._httpConnectMethodLong, out aaa);

            Console.WriteLine("\r\n\r\nTest GetKnownMethod: started.");
            sw.Start();
            for (int i = 0; i < iterations; i++)
            {

                string str1; KnownMethods.GetKnownMethod(KnownMethods._httpConnectMethodLong, out str1);
                string str2; KnownMethods.GetKnownMethod(KnownMethods._httpDeleteMethodLong, out str2);
                string str3; KnownMethods.GetKnownMethod(KnownMethods._httpGetMethodLong, out str3);
                string str4; KnownMethods.GetKnownMethod(KnownMethods._httpHeadMethodLong, out str4);
                string str5; KnownMethods.GetKnownMethod(KnownMethods._httpPatchMethodLong, out str5);
                string str6; KnownMethods.GetKnownMethod(KnownMethods._httpPostMethodLong, out str6);
                string str7; KnownMethods.GetKnownMethod(KnownMethods._httpPutMethodLong, out str7);
                string str8; KnownMethods.GetKnownMethod(KnownMethods._httpOptionsMethodLong, out str8);
                string str9; KnownMethods.GetKnownMethod(KnownMethods._httpTraceMethodLong, out str9);
            }
            sw.Stop();
            Console.WriteLine("Elapsed time: {0} ms.", sw.Elapsed.TotalMilliseconds);

            sw.Reset();

            Console.WriteLine("\r\n\r\nTest GetKnownMethodNew: started.");
            sw.Start();
            for (int i = 0; i < iterations; i++)
            {
                string str1; KnownMethods.GetKnownMethodNew(KnownMethods._httpConnectMethodLong, out str1);
                string str2; KnownMethods.GetKnownMethodNew(KnownMethods._httpDeleteMethodLong, out str2);
                string str3; KnownMethods.GetKnownMethodNew(KnownMethods._httpGetMethodLong, out str3);
                string str4; KnownMethods.GetKnownMethodNew(KnownMethods._httpHeadMethodLong, out str4);
                string str5; KnownMethods.GetKnownMethodNew(KnownMethods._httpPatchMethodLong, out str5);
                string str6; KnownMethods.GetKnownMethodNew(KnownMethods._httpPostMethodLong, out str6);
                string str7; KnownMethods.GetKnownMethodNew(KnownMethods._httpPutMethodLong, out str7);
                string str8; KnownMethods.GetKnownMethodNew(KnownMethods._httpOptionsMethodLong, out str8);
                string str9; KnownMethods.GetKnownMethodNew(KnownMethods._httpTraceMethodLong, out str9);
            }
            sw.Stop();
            Console.WriteLine("Elapsed time: {0} ms.", sw.Elapsed.TotalMilliseconds);



            Console.ReadKey();
        }
   
    }
}