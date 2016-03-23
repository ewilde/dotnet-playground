using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public static class IntegerExtensions
    {
        public static void Times(this int value,
                                 Action<int> action)
        {
            for (int i = 0; i < value; i++)
            {
                action(i);
            }
        }

        public static List<Task<T>> TimesAsync<T>(this int value, Func<int, T> func)
        {
            var tasks = new List<Task<T>>();

            for (var i = 0; i < value; i++)
            {
                var j = i;
                var task = new Task<T>(() => func(j), TaskCreationOptions.LongRunning);
                task.Start();
                tasks.Add(task);
            }

            return tasks;
        } 

        public static bool Odd(this int value)
        {
            return value % 2 == 1;
        }

        public static int Megabytes(this int value)
        {
            return value * 1024 * 1024;
        }

        public static int Kilobytes(this int value)
        {
            return value * 1024;
        }

        public static int Bytes(this int value)
        {
            return value;
        }

        public static string Ordinalize(this int value)
        {

            string ordinalized;

            if ((value % 100).Between(11, 13))
            {
                ordinalized = "th";
            }
            else
            {
                switch (value % 10)
                {
                case 1:
                    ordinalized = "st"; break;
                case 2:
                    ordinalized = "nd"; break;
                case 3:
                    ordinalized = "rd"; break;
                default:
                    ordinalized = "th"; break;
                }
            }

            return value + ordinalized;
        }

        public static bool Between(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static TimeSpan Hour(this int value)
        {
            if (value != 1)
            {
                throw new ArgumentOutOfRangeException("value");
            }
            return Hours(value);
        }

        public static TimeSpan Hours(this int value)
        {
            return TimeSpan.FromHours(value);
        }

        public static TimeSpan Minutes(this int value)
        {
            return TimeSpan.FromMinutes(value);
        }

        public static TimeSpan Seconds(this int value)
        {
            return TimeSpan.FromSeconds(value);
        } 
    }
}