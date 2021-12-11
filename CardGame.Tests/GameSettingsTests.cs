using CardGame.Configuration;
using CardGame.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CardGame.Tests
{
    /// <summary>
    /// Game Settings Tests.
    /// </summary>
    [TestClass]
    public class GameSettingsTests
    {
        private IConfiguration defaultConfiguration;
        private Setting defaultSettings;

        /// <summary>
        /// Mock Setup.
        /// </summary>
        [TestInitialize]
        public void MockSetup()
        {
            defaultSettings = new Setting()
            {
                NumberOfPlayers = 2,
                DeckSize = 40,
                Suit = null
            };

            var inMemorySettings = new Dictionary<string, string> {
                {"DefaultSettings:NumberOfPlayers", defaultSettings.NumberOfPlayers.ToString()},
                {"DefaultSettings:DeckSize", defaultSettings.DeckSize.ToString()},
                {"DefaultSettings:Suit", null},
            };

            defaultConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        /// <summary>
        /// Test Default settings.
        /// </summary>
        [TestMethod]
        public void Default_Settings()
        {
            IGameSettings gameSetting = new GameSettings(defaultConfiguration);

            Assert.AreEqual(defaultSettings.NumberOfPlayers, gameSetting.GetCurrentSetting.NumberOfPlayers);
            Assert.AreEqual(defaultSettings.DeckSize, gameSetting.GetCurrentSetting.DeckSize);
        }

        /// <summary>
        /// Validate configuration.
        /// </summary>
        [TestMethod]
        public void Validate_Configuration()
        {
            Setting setting = new Setting()
            {
                NumberOfPlayers = 4,
                DeckSize = 20,
                Suit = null
            };
            IGameSettings gameSetting = new GameSettings(defaultConfiguration);

            var expected = gameSetting.ValidateConfiguration(setting);

            Assert.IsTrue(expected);
        }

        /// <summary>
        /// Validate invalid configuration.
        /// </summary>
        [TestMethod]
        public void Validate_Invalid_Configuration()
        {
            Setting setting = new Setting()
            {
                NumberOfPlayers = 8,
                DeckSize = 56,
                Suit = null
            };
            IGameSettings gameSetting = new GameSettings(defaultConfiguration);

            var expected = gameSetting.ValidateConfiguration(setting);

            Assert.IsFalse(expected);
        }

        /// <summary>
        /// Update configuration.
        /// </summary>
        [TestMethod]
        public void Update_Configuration()
        {
            Setting setting = new Setting()
            {
                NumberOfPlayers = 4,
                DeckSize = 20,
                Suit = null
            };
            IGameSettings gameSetting = new GameSettings(defaultConfiguration);

            gameSetting.UpdateConfiguration(setting);

            Assert.AreEqual(setting.NumberOfPlayers, gameSetting.GetCurrentSetting.NumberOfPlayers);
            Assert.AreEqual(setting.DeckSize, gameSetting.GetCurrentSetting.DeckSize);
        }
    }
}
