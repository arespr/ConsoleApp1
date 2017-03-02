using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public static class Utils
    {
        public static ulong? SearchMask(ulong[] values, ulong maskMinValue, ulong maskMaxValue, int bitsCount)
        {
            int maskAlignment = 0;
            ulong? maskFound = null;
            ulong maskAlignedToRight = 0;
            int bit0HolesFoundMask = 0;

            int arrayLength = values.Length;
           

            var mashHash = new HashSet<ulong>();

            for (ulong currentMask = maskMinValue; currentMask <= maskMaxValue; currentMask++)
            {
                //if (currentMask % 400000000 == 0)
                //{
                //    Console.WriteLine(currentMask);
                //}

                if (CountBits(currentMask) != bitsCount) continue;
                mashHash.Clear();
                bool duplicatesExists = false;
                for (int j = 0; j < arrayLength; j++)
                {
                    var tmp = values[j] & currentMask;

                    bool alreadyExists = mashHash.Add(tmp) == false;
                    if (alreadyExists)
                    {
                        duplicatesExists = true;
                        break;
                    }
                }

                if (duplicatesExists == false)
                {
                    ////try to find smallest mask
                    //var currentMaskAlignment = GetMaskRightAlignment(currentMask);

                    //var currentMaskAlignedToRight = currentMask >> currentMaskAlignment;

                    //if (maskFound.HasValue)
                    //{
                    //    if (maskAlignedToRight > currentMaskAlignedToRight)
                    //    {
                    //        maskFound = currentMask;
                    //        maskAlignedToRight = currentMaskAlignedToRight;
                    //        maskAlignment = currentMaskAlignment;
                    //        Console.WriteLine("{0:X8}", maskFound);
                    //    }
                    //}
                    //else
                    //{
                    //    maskFound = currentMask;
                    //    maskAlignedToRight = currentMaskAlignedToRight;
                    //    maskAlignment = currentMaskAlignment;
                    //    Console.WriteLine("{0:X8}", maskFound);

                    //}


                    //try to find perfect mask
                    var bit0HolesCurrentMask = CountBit0Holes(currentMask);


                    if (maskFound.HasValue)
                    {
                        if (bit0HolesFoundMask > bit0HolesCurrentMask)
                        {
                            maskFound = currentMask;
                            bit0HolesFoundMask = bit0HolesCurrentMask;

                            Console.WriteLine("{0:X8} holes: {1}", maskFound, bit0HolesFoundMask);

                            if (bit0HolesFoundMask == 0)
                                return maskFound;
                        }
                    }
                    else
                    {
                        maskFound = currentMask;
                        bit0HolesFoundMask = bit0HolesCurrentMask;
                        Console.WriteLine("{0:X8} holes: {1}", maskFound, bit0HolesFoundMask);

                        if (bit0HolesFoundMask == 0)
                            return maskFound;
                    }
                }
            }

            return maskFound;
        }


        internal static int CountBit0Holes(ulong mask)
        {
            int holesCount = 0;
            ulong one = 0x01;
            bool isLastBit0 = false;
            for (int i = 0; i < 64; i++)
            {
                var tmp = one << i;

                bool isBit0 = (tmp & mask) == 0;

                if (isLastBit0 == false && isLastBit0 != isBit0)
                {
                    holesCount++;
                }
                isLastBit0 = isBit0;
            }

            return holesCount > 0? holesCount -1 : 0;
        }

        internal static int GetMaskRightAlignment(ulong mask)
        {
            ulong one = 0x01;
            for (int i = 0; i < 64; i++)
            {
                var tmp = one << i;
                if ((tmp & mask) > 0)
                {
                    return i;
                }
            }

            return 0;
        }

        internal static int CountBits(ulong v)
        {
            const ulong Mask01010101 = 0x5555555555555555UL;
            const ulong Mask00110011 = 0x3333333333333333UL;
            const ulong Mask00001111 = 0x0F0F0F0F0F0F0F0FUL;
            const ulong Mask00000001 = 0x0101010101010101UL;
            v = v - ((v >> 1) & Mask01010101);
            v = (v & Mask00110011) + ((v >> 2) & Mask00110011);
            return (int)(unchecked(((v + (v >> 4)) & Mask00001111) * Mask00000001) >> 56);
        }
    }
}
