using System;

namespace ZeleniumFramework.Config
{
    public static class TimeConfig
    {
        public static readonly TimeSpan TinyTimeout = TimeSpan.FromSeconds(2);
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);
        public static readonly TimeSpan MediumTimeout = TimeSpan.FromSeconds(10);
        public static readonly TimeSpan LongTimeout = TimeSpan.FromSeconds(30);
        public static readonly TimeSpan ExtraLongTimeout = TimeSpan.FromSeconds(60);
        public static readonly TimeSpan DoubleExtraLongTimeout = TimeSpan.FromSeconds(120);
    }
}
