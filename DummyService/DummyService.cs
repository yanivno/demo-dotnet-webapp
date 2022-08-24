

using System;

namespace aspnet.Services
{
    public class DummyService
    {
        public bool IsDummy(int candidate)
        {
            return candidate % 2 == 0;
        }
    }
}
