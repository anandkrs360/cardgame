using CardGame.Models;

namespace CardGame.Configuration
{
    /// <summary>
    /// The Game Settings Interface.
    /// </summary>
    interface IGameSettings
    {
        /// <summary>
        /// Gets the Current Setting.
        /// </summary>
        Setting GetCurrentSetting { get; }

        /// <summary>
        /// Validate Configuration.
        /// </summary>
        /// <param name="setting">
        /// The Setting.
        /// </param>
        bool ValidateConfiguration(Setting setting);

        /// <summary>
        /// Update Configuration.
        /// </summary>
        /// <param name="setting">
        /// The Setting.
        /// </param>
        void UpdateConfiguration(Setting setting);

    }
}
