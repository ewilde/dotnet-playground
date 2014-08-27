using System;
// Solutions based on Jon Skeet's article http://csharpindepth.com/Articles/General/Singleton.aspx#lazy
namespace Edward.Wilde.CSharp.Features.Singleton
{
    public sealed class Singleton6
    {
        private static readonly Lazy<Singleton6> instance = new Lazy<Singleton6>(() => new Singleton6());

        public static Singleton6 Instance { get { return instance.Value; } }

        private Singleton6()
        {
        }
    }
}