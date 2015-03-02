namespace Edward.Wilde.CSharp.Features.Utilities
{
    public static class FloatExtensions
    {

        public static float Megabytes(this float bytes)
        {
            return bytes * 1024 * 1024;
        }

        public static float ToMegabytes(this float value)
        {
            return value / 1024.0f / 1024.0f;
        }

        public static float Kilobytes(this float value)
        {
            return value * 1024;
        }

        public static float Bytes(this float value)
        {
            return value;
        } 
    }
}