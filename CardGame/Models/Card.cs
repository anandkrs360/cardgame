namespace CardGame.Models
{
    /// <summary>
    /// The Card.
    /// </summary>
    internal class Card
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="value">
        /// The Card.
        /// </param>
        /// <param name="suit">
        /// The Suit Name.
        /// </param>
        public Card(int value, string suit)
        {
            Value = value;
            Suit = suit;
            DisplayName = string.IsNullOrEmpty(suit) ? value.ToString() : suit + value;
        }

        /// <summary>
        /// Gets the card value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets the suit name.
        /// </summary>
        public string Suit { get; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; }
    }
}
