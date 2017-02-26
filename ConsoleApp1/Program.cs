using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        private const byte ByteSpace = (byte) ' ';
        private const byte ByteQuestionMark = (byte) '?';
        private const byte BytePercentage = (byte) '%';

        static void Main(string[] args)
        {

            int iterations = 5000000;

            byte[] data = PrepareTestData();


            int spaceIndex, questionMarkIndex, percentageIndex, pathEnd;

            var sw = new Stopwatch();


            Console.WriteLine("Test FunctionNew: started.");
            sw.Start();
            for (int i = 0; i < iterations; i++)
            {
                FunctionNew(data, out spaceIndex, out questionMarkIndex, out percentageIndex, out pathEnd);
            }
            sw.Stop();
            Console.WriteLine("Elapsed time: {0} ms.", sw.Elapsed.TotalMilliseconds);

            sw.Reset();

            Console.WriteLine("\r\n\r\nTest FunctionOrginal: started.");
            sw.Start();
            for (int i = 0; i < iterations; i++)
            {
                FunctionOrginal(data, out spaceIndex, out questionMarkIndex, out percentageIndex, out pathEnd);
            }
            sw.Stop();
            Console.WriteLine("Elapsed time: {0} ms.", sw.Elapsed.TotalMilliseconds);

           

            Console.ReadKey();
        }


        /// <summary>
        /// Function based on code from  lines 1093-1125
        /// </summary>
        public static void FunctionOrginal(byte[] data, out int spaceIndex, out int questionMarkIndex,
            out int percentageIndex, out int pathEnd)
        {
            int length = data.Length;
            int pathBegin = 0;
            spaceIndex = -1;
            questionMarkIndex = -1;
            percentageIndex = -1;
            pathEnd = -1;


            for (int i = pathBegin; i < length; i++)
            {
                var ch = data[i];
                if (spaceIndex == -1 && ch == ByteSpace)
                {
                    if (pathEnd == -1)
                    {
                        pathEnd = i;
                    }

                    spaceIndex = i;
                }

                if (questionMarkIndex == -1 && ch == ByteQuestionMark)
                {
                    if (pathEnd == -1)
                    {
                        pathEnd = i;
                    }

                    questionMarkIndex = i;
                }

                if (percentageIndex == -1 && ch == BytePercentage)
                {
                    if (pathEnd == -1)
                    {
                        pathEnd = i;
                    }

                    percentageIndex = i;
                }
            }
        }

        public static void FunctionNew(byte[] data, out int spaceIndex, out int questionMarkIndex,
            out int percentageIndex, out int pathEnd)
        {
            int length = data.Length;
            int pathBegin = 0;
            spaceIndex = -1;
            questionMarkIndex = -1;
            percentageIndex = -1;
            pathEnd = -1;

            for (int i = pathBegin; i < length; i++)
            {
                var ch = data[i];

                switch (ch)
                {
                    case ByteSpace:
                        if (spaceIndex == -1)
                        {
                            if (pathEnd == -1)
                            {
                                pathEnd = i;
                            }

                            spaceIndex = i;

                            if (questionMarkIndex != -1 && percentageIndex != -1)
                            {
                                goto loopExit;
                            }
                        }
                        break;
                    case ByteQuestionMark:
                        if (questionMarkIndex == -1)
                        {
                            if (pathEnd == -1)
                            {
                                pathEnd = i;
                            }

                            questionMarkIndex = i;

                            if (spaceIndex != -1 && percentageIndex != -1)
                            {
                                goto loopExit;
                            }
                        }
                        break;
                    case BytePercentage:
                        if (percentageIndex == -1)
                        {
                            if (pathEnd == -1)
                            {
                                pathEnd = i;
                            }

                            percentageIndex = i;

                            if (spaceIndex != -1 && questionMarkIndex != -1)
                            {
                                goto loopExit;
                            }
                        }
                        break;
                }
            }

            loopExit:
            return;
        }

        #region Test data

        private static byte[] PrepareTestData()
        {
            byte[] data = new byte[256];

            for (int i = 0; i < data.Length; i++)
            {

                byte ch = (byte)(i % 256);
                bool valid =  ch != ByteSpace && ch != ByteQuestionMark &&
                             ch != BytePercentage;
                if (valid)
                {
                    data[i] = ch;
                }
                else
                {
                    data[i] = (byte)'A'; // or any other charracter other than ByteSpace ByteQuestionMark BytePercentage
                }
            }

            data[data.Length - 3] = ByteSpace;
            data[data.Length - 2] = ByteQuestionMark;
            data[data.Length - 1] = BytePercentage;
            return data;
        }


        #endregion
    }
}