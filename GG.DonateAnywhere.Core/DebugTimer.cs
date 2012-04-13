using System;
using System.Diagnostics;

namespace GG.DonateAnywhere.Core
{
    public class DebugTimer : Stopwatch, IDisposable
    {
        private readonly string _name;

        public DebugTimer(string name = null)
        {
            _name = name ?? Guid.NewGuid().ToString();
            Debug.WriteLine(string.Format("{0} started.", _name));
            Start();
        }

        public void Dispose()
        {
            Debug.WriteLine(string.Format("{0} took {1} ms", _name, ElapsedMilliseconds));
            Stop();
        }
    }
}