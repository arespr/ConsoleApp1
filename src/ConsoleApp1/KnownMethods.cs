using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleApp1
{
    class KnownMethods
    {
        // readonly primitive statics can be Jit'd to consts https://github.com/dotnet/coreclr/issues/1079
        public readonly static ulong _httpConnectMethodLong = GetAsciiStringAsLong("CONNECT ");

        public readonly static ulong _httpDeleteMethodLong = GetAsciiStringAsLong("DELETE \0");
        public readonly static ulong _httpGetMethodLong = GetAsciiStringAsLong("GET \0\0\0\0");
        public readonly static ulong _httpHeadMethodLong = GetAsciiStringAsLong("HEAD \0\0\0");
        public readonly static ulong _httpPatchMethodLong = GetAsciiStringAsLong("PATCH \0\0");
        public readonly static ulong _httpPostMethodLong = GetAsciiStringAsLong("POST \0\0\0");
        public readonly static ulong _httpPutMethodLong = GetAsciiStringAsLong("PUT \0\0\0\0");
        public readonly static ulong _httpOptionsMethodLong = GetAsciiStringAsLong("OPTIONS ");
        public readonly static ulong _httpTraceMethodLong = GetAsciiStringAsLong("TRACE \0\0");

        private readonly static ulong _http10VersionLong = GetAsciiStringAsLong("HTTP/1.0");
        private readonly static ulong _http11VersionLong = GetAsciiStringAsLong("HTTP/1.1");

        private readonly static ulong _mask8Chars = GetMaskAsLong(new byte[]
            {0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff});

        private readonly static ulong _mask7Chars = GetMaskAsLong(new byte[]
            {0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00});

        private readonly static ulong _mask6Chars = GetMaskAsLong(new byte[]
            {0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x00});

        private readonly static ulong _mask5Chars = GetMaskAsLong(new byte[]
            {0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x00, 0x00});

        private readonly static ulong _mask4Chars = GetMaskAsLong(new byte[]
            {0xff, 0xff, 0xff, 0xff, 0x00, 0x00, 0x00, 0x00});

        private readonly static Tuple<ulong, ulong, string>[] _knownMethods = new Tuple<ulong, ulong, string>[8];
        private readonly static Tuple<ulong, ulong, string>[] _knownMethods2 = new Tuple<ulong, ulong, string>[17];


        private unsafe static ulong GetAsciiStringAsLong(string str)
        {
            Debug.Assert(str.Length == 8, "String must be exactly 8 (ASCII) characters long.");

            var bytes = Encoding.ASCII.GetBytes(str);

            fixed (byte* ptr = &bytes[0])
            {
                return *(ulong*)ptr;
            }
        }

        private unsafe static ulong GetMaskAsLong(byte[] bytes)
        {
            Debug.Assert(bytes.Length == 8, "Mask must be exactly 8 bytes long.");

            fixed (byte* ptr = bytes)
            {
                return *(ulong*)ptr;
            }
        }

        static KnownMethods()
        {
            _knownMethods[0] = Tuple.Create(_mask4Chars, _httpPutMethodLong, HttpMethods.Put);
            _knownMethods[1] = Tuple.Create(_mask5Chars, _httpPostMethodLong, HttpMethods.Post);
            _knownMethods[2] = Tuple.Create(_mask5Chars, _httpHeadMethodLong, HttpMethods.Head);
            _knownMethods[3] = Tuple.Create(_mask6Chars, _httpTraceMethodLong, HttpMethods.Trace);
            _knownMethods[4] = Tuple.Create(_mask6Chars, _httpPatchMethodLong, HttpMethods.Patch);
            _knownMethods[5] = Tuple.Create(_mask7Chars, _httpDeleteMethodLong, HttpMethods.Delete);
            _knownMethods[6] = Tuple.Create(_mask8Chars, _httpConnectMethodLong, HttpMethods.Connect);
            _knownMethods[7] = Tuple.Create(_mask8Chars, _httpOptionsMethodLong, HttpMethods.Options);

            Set(_knownMethods2, _mask4Chars, _httpPutMethodLong, HttpMethods.Put);
            Set(_knownMethods2, _mask5Chars, _httpPostMethodLong, HttpMethods.Post);
            Set(_knownMethods2, _mask5Chars, _httpHeadMethodLong, HttpMethods.Head);
            Set(_knownMethods2, _mask6Chars, _httpTraceMethodLong, HttpMethods.Trace);
            Set(_knownMethods2, _mask6Chars, _httpPatchMethodLong, HttpMethods.Patch);
            Set(_knownMethods2, _mask7Chars, _httpDeleteMethodLong, HttpMethods.Delete);
            Set(_knownMethods2, _mask8Chars, _httpConnectMethodLong, HttpMethods.Connect);
            Set(_knownMethods2, _mask8Chars, _httpOptionsMethodLong, HttpMethods.Options);
            Set(_knownMethods2, _mask4Chars, _httpGetMethodLong, HttpMethods.Get);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Set(Tuple<ulong, ulong, string>[] knownMethods, ulong mask, ulong knownMethodUlong, string knownMethod)
        {
            knownMethods[GetKey(knownMethodUlong)] = new Tuple<ulong, ulong, string>(mask, knownMethodUlong, knownMethod);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetKnownMethod(ulong value, out string knownMethod)
        {
            knownMethod = null;

            if ((value & _mask4Chars) == _httpGetMethodLong)
            {
                knownMethod = HttpMethods.Get;
                return true;
            }
            foreach (var x in _knownMethods)
            {
                if ((value & x.Item1) == x.Item2)
                {
                    knownMethod = x.Item3;
                    return true;
                }
            }

            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetKnownMethodNew(ulong value, out string knownMethod)
        {
            knownMethod = null;

            if ((value & _mask4Chars) == _httpGetMethodLong)
            {
                knownMethod = HttpMethods.Get;
                return true;
            }

            var key = GetKey(value);

            Tuple<ulong, ulong, string> tuple = _knownMethods2[key];

            if (tuple != null && (value & tuple.Item1) == tuple.Item2)
            {
                knownMethod = tuple.Item3;
                return true;
            }

            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        private static int GetKey(ulong value)
        {
            var tmp = value & 0x100604;

            return (int)(((tmp >> 2) | (tmp >> 8) | (tmp >> 17)) & 0x0F);
        }

    }
}
