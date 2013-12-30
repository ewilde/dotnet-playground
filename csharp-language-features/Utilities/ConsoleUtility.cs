// -----------------------------------------------------------------------
// <copyright file="ConsoleUtility.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public class ConsoleUtility
    {
        public static void PrintSuccess(string message)
        {
            PrintColor(message, ConsoleColor.DarkGreen);
        }

        public static void PrintError(string message)
        {
            PrintColor(message, ConsoleColor.Red);
        }

        public static void PrintInfo(string message)
        {
            PrintColor(message, ConsoleColor.White);
        }

        public static void PrintFatal(string message)
        {
            PrintColor(message, ConsoleColor.Red, ConsoleColor.White);
        }

        public static void PrintColor(string message, ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.BackgroundColor = backgroundColor;

                Console.WriteLine(message);
            }
            finally
            {
                Console.ForegroundColor = originalForegroundColor;
                Console.BackgroundColor = originalBackgroundColor;

            }
        }
    }
}