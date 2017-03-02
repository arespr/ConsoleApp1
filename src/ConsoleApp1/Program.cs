using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    class Program
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int GetIndexOld(ulong value)
        {
            var tmp = (int)value & 0x100604;

            return ((tmp >> 2) | (tmp >> 8) | (tmp >> 17)) & 0x0F;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int GetIndexOld1(ulong value)
        {
            var tmp = (int)value & 0x100604;

            return (((tmp >> 2) | (tmp >> 8) | (tmp >> 17)) & 0x0F);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int GetIndex1(ulong value)
        {
            var tmp = value & 0x0600000C;

            
            return (byte)((tmp >> 2) | (tmp >> 23));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int GetIndex2(ulong value)
        {
            var tmp = (int) value & 0x0600000C;

            return (byte) ((tmp >> 2) | (tmp >> 23));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int GetIndex2_1(ulong value)
        {

            var tmp = (int) value & 0x0600000C;

            return ((tmp >> 2) | (tmp >> 23)) & 0x0F;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int GetIndex3(ulong value)
        {
            var tmp = value & 0x0600000C;


            return (byte)((tmp >> 2) | (tmp >> 23));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int GetIndex4(ulong value)
        {
            var tmp = value & 0x0600000C;


            return (byte)((tmp >> 2) | (tmp >> 23));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int GetIndex5(ulong value)
        {
            var tmp = value & 0x0600000C;


            return (int)(((tmp >> 2) | (tmp >> 23)) & 0x0F);
        }

        static void Main(string[] args)
        {

            //MASK SEARCH:
            //var methods = new ulong[9]
            //{
            //    KnownMethods._httpPutMethodLong,
            //    KnownMethods._httpPostMethodLong,
            //    KnownMethods._httpHeadMethodLong,
            //    KnownMethods._httpTraceMethodLong,
            //    KnownMethods._httpPatchMethodLong,
            //    KnownMethods._httpDeleteMethodLong,
            //    KnownMethods._httpConnectMethodLong,
            //    KnownMethods._httpOptionsMethodLong,
            //    KnownMethods._httpGetMethodLong,
            //};


            //ulong? bestMask = Utils.SearchMask(methods, 15, uint.MaxValue, 4);


            Console.WriteLine(string.Format("0x{0:X8}", GetIndexOld(0xFFFFFFFFFFFFFFFF)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndexOld1(0xFFFFFFFFFFFFFFFF)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex1(0xFFFFFFFFFFFFFFFF)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex2(0xFFFFFFFFFFFFFFFF)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex3(0xFFFFFFFFFFFFFFFF)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex4(0xFFFFFFFFFFFFFFFF)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex5(0xFFFFFFFFFFFFFFFF)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex2_1(0xFFFFFFFFFFFFFFFF)));

            Console.WriteLine(string.Format("0x{0:X8}", GetIndex1(0x0600000C)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex2(0x0600000C)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex3(0x0600000C)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex4(0x0600000C)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex5(0x0600000C)));
            Console.WriteLine(string.Format("0x{0:X8}", GetIndex2_1(0x0600000C)));

            uint iterations = uint.MaxValue;

            using (new PerformanceMeasurement("GetIndexOld"))
            {
                for (uint i = 0; i < iterations; i++)
                {
                    GetIndexOld(i);
                }
            }

            using (new PerformanceMeasurement("GetIndexOld1"))
            {
                for (uint i = 0; i < iterations; i++)
                {
                    GetIndexOld1(i);
                }
            }
            using (new PerformanceMeasurement("GetIndex1"))
            {
                for (uint i = 0; i < iterations; i++)
                {
                    GetIndex1(i);
                }
            }

            using (new PerformanceMeasurement("GetIndex2"))
            {

                for (uint i = 0; i < iterations; i++)
                {
                    GetIndex2(i);
                }
            }


            using (new PerformanceMeasurement("GetIndex3"))
            {
                for (uint i = 0; i < iterations; i++)
                {
                    GetIndex3(i);
                }
            }


            using (new PerformanceMeasurement("GetIndex4"))
            {
                for (uint i = 0; i < iterations; i++)
                {
                    GetIndex4(i);
                }
            }

            using (new PerformanceMeasurement("GetIndex5"))
            {
                for (uint i = 0; i < iterations; i++)
                {
                    GetIndex5(i);
                }
            }

            using (new PerformanceMeasurement("GetIndex2_1"))
            {
                for (uint i = 0; i < iterations; i++)
                {
                    GetIndex2_1(i);
                }
            }


            Console.ReadKey();
        }
   
    }
}