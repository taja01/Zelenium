using System;

namespace Zelenium.Core.Config
{
    public static class TimeConfig
    {
        /// <summary>
        /// 2s
        /// </summary>
        public static readonly TimeSpan TinyTimeout = TimeSpan.FromSeconds(2);

        /// <summary>
        /// 5s
        /// </summary>
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        /// <summary>
        /// 10s
        /// </summary>
        public static readonly TimeSpan MediumTimeout = TimeSpan.FromSeconds(10);

        /// <summary>
        /// 30s
        /// </summary>
        public static readonly TimeSpan LongTimeout = TimeSpan.FromSeconds(30);

        /// <summary>
        /// 60s
        /// </summary>
        public static readonly TimeSpan ExtraLongTimeout = TimeSpan.FromSeconds(60);

        /// <summary>
        /// 120s
        /// </summary>
        public static readonly TimeSpan DoubleExtraLongTimeout = TimeSpan.FromSeconds(120);
    }
}
