using CardGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CardGame.Common
{
    /// <summary>
    /// The Utilities Class.
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// Shuffle Cards.
        /// </summary>
        /// <param name="cards">
        /// The Card.
        /// </param>
        public static void ShuffleCards(List<Card> cards)
        {
            int len = cards.Count;

            Random rand = new Random();

            for (int i = 0; i < len; i++)
            {
                int index = i + rand.Next(len - i);

                var temp = cards[index];
                cards[index] = cards[i];
                cards[i] = temp;
            }
        }

        public static string GetResourceString(string fileName)
        {
            string value;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = string.Format("CardGame.{0}", fileName);

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    value = reader.ReadToEnd();
                }
            }
            return value;
        }
    }
}
