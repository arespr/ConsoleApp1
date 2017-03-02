using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class PerformanceMeasurement : IDisposable
    {
        private readonly string _label;
        private readonly Stopwatch _sw;
        private readonly Action<string> _output;

        /// <summary>
        /// Default output to Console
        /// </summary>
        /// <param name="label"></param>
        public PerformanceMeasurement(string label)
            : this(label, Console.WriteLine)
        {

        }

        public PerformanceMeasurement(string label, Action<string> output)
        {
            _output = output;
            _label = label;
            _sw = new Stopwatch();
            _sw.Start();
        }

        public void Dispose()
        {
            _sw.Stop();
            _output(string.Format("{0}: {1} ms", _label, _sw.Elapsed.TotalMilliseconds));
        }
    }
}
