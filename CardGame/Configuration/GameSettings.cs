using CardGame.Common;
using CardGame.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace CardGame.Configuration
{
    /// <summary>
    /// The Game Settings.
    /// </summary>
    internal class GameSettings : IGameSettings
    {
        private readonly Setting _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSettings"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The Configuration.
        /// </param>
        public GameSettings(IConfiguration configuration)
        {
            var defaultSettings = configuration.GetSection("DefaultSettings").Get<Setting>();
            if (defaultSettings == null)
            {
                throw new Exception("Failed to load default settings.");
            }

            // Validate Default Configuration
            if (ValidateConfiguration(defaultSettings))
            {
                _settings = defaultSettings;
            }
            else
            {
                throw new InvalidConfigurationException("Invalid Default configuration.");
            }

        }

        /// <summary>
        /// Gets the Current Setting.
        /// </summary>
        public Setting GetCurrentSetting
        {
            get
            {
                return _settings;
            }
        }

        /// <summary>
        /// Validate Configuration.
        /// </summary>
        /// <param name="setting">
        /// The Setting.
        /// </param>
        public bool ValidateConfiguration(Setting setting)
        {
            bool isvalid = true;
            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            if (setting.NumberOfPlayers < 2 || setting.NumberOfPlayers > 4)
            {
                isvalid = false;
                ConsoleExtension.WriteLine("Minimum 2 and maximum of 4 players are allowed to play the game.", ConsoleColor.Red);
            }

            if (setting.DeckSize < 12 || setting.DeckSize > 52)
            {
                isvalid = false;
                ConsoleExtension.WriteLine("Minimum of 12 and maximum of 52 deck size can be configured.", ConsoleColor.Red);
            }

            return isvalid;
        }

        /// <summary>
        /// Update Configuration.
        /// </summary>
        /// <param name="setting">
        /// The Setting.
        /// </param>
        public void UpdateConfiguration(Setting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            _settings.DeckSize = setting.DeckSize;
            _settings.NumberOfPlayers = setting.NumberOfPlayers;
            _settings.Suit = setting.Suit;
        }
    }
}
