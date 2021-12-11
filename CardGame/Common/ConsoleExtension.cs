using System;

namespace CardGame.Common
{
    /// <summary>
    /// The Console Extension.
    /// </summary>
    internal static class ConsoleExtension
    {
        /// <summary>
        /// Write to console with foreground color.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="color">
        /// The console color.
        /// </param>
        public static void WriteLine(string message, ConsoleColor color = default(ConsoleColor))
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
