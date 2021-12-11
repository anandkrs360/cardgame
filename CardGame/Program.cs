using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CardGame.Configuration;
using CardGame.Models;
using CardGame.Common;

namespace CardGame
{
    /// <summary>
    /// The program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        static void Main()
        {
            // Registering Services
            ServiceProvider serviceProvider = RegisterServices();

            Start(serviceProvider);
        }

        /// <summary>
        /// Setup Configuration.
        /// </summary>
        static IConfiguration SetupConfiguration()
        {
            return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json", false)
            .Build();
        }

        /// <summary>
        /// Register Services.
        /// </summary>
        static ServiceProvider RegisterServices()
        {
            IConfiguration configuration = SetupConfiguration();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IGameSettings, GameSettings>()
                .AddScoped<IGame, Game>()
                .AddSingleton(configuration);

            return serviceProvider.BuildServiceProvider();
        }

        /// <summary>
        /// Start Game.
        /// </summary>
        /// <param name="serviceProvider">
        /// The Service Provider.
        /// </param>
        static void Start(ServiceProvider serviceProvider)
        {
            // Game Setting Service
            IGameSettings gameSettings = serviceProvider.GetRequiredService<IGameSettings>();
            Console.WriteLine("Hello and Welcome to Card Game");
            Console.WriteLine();
            try
            {
                while (true)
                {
                    Console.WriteLine("1. Start Game");
                    Console.WriteLine("2. Settings");
                    Console.WriteLine("3. Rules");
                    Console.WriteLine("4. Exit");
                    Console.WriteLine();
                    Console.WriteLine("Select an option to continue.");

                    string option = Console.ReadLine();

                    if (int.TryParse(option, out int opt))
                    {
                        switch (opt)
                        {
                            case 1:
                                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                                {
                                    IGame game = scope.ServiceProvider.GetRequiredService<IGame>();

                                    List<Player> players = new List<Player>();

                                    int noOfPlayers = gameSettings.GetCurrentSetting.NumberOfPlayers;

                                    for (int i = 1; i <= noOfPlayers; i++)
                                    {
                                        Console.WriteLine($"Enter name of Player {i}");

                                        // Read Player Name (OPTIONAL)
                                        string playerName = Console.ReadLine();

                                        if (string.IsNullOrEmpty(playerName))
                                        {
                                            playerName = $"Player {i}";
                                        }

                                        players.Add(new Player(playerName));
                                    }

                                    // Initialize Game
                                    if (game.Initialize(players))
                                    {
                                        // Check Game Status
                                        while (!game.GameStatus())
                                        {
                                            // Playing Turn
                                            game.PlayingTurn();
                                        }

                                        // Reset Game
                                        game.Reset();
                                    }
                                    else
                                    {
                                        throw new ApplicationException("Unable to Initialize the game. Please try after sometime.");
                                    }
                                }
                                Console.WriteLine();
                                ConsoleExtension.WriteLine("Game Over.", ConsoleColor.DarkBlue);
                                Console.ReadLine();
                                break;

                            case 2:
                                Console.WriteLine();
                                ConsoleExtension.WriteLine("Current Game Settings", ConsoleColor.DarkBlue);
                                Console.WriteLine();
                                Console.WriteLine($"Number of Players: {gameSettings.GetCurrentSetting.NumberOfPlayers}");
                                Console.WriteLine($"Deck Size: {gameSettings.GetCurrentSetting.DeckSize}");
                                if (gameSettings.GetCurrentSetting.Suit != null && gameSettings.GetCurrentSetting.Suit.Length > 0)
                                {
                                    Console.WriteLine($"Suits: ");
                                    foreach (string suit in gameSettings.GetCurrentSetting.Suit)
                                    {
                                        Console.WriteLine(suit);
                                    }
                                }

                                // Manual Configuration
                                ConsoleExtension.WriteLine("Do you want to configure settings? (Yes/No)", ConsoleColor.DarkGray);
                                string input = Console.ReadLine();
                                if(!string.IsNullOrEmpty(input) && (input.ToLower() == "y" || input.ToLower() == "yes"))
                                {
                                    Setting newSetting = new Setting();
                                    ConsoleExtension.WriteLine("Update Settings", ConsoleColor.DarkBlue);
                                    Console.WriteLine();
                                    Console.Write($"Number of Players ({gameSettings.GetCurrentSetting.NumberOfPlayers}): ");
                                    string noOfPlayers = Console.ReadLine();
                                    if (int.TryParse(noOfPlayers, out int players))
                                    {
                                        newSetting.NumberOfPlayers = players;
                                    }
                                    Console.WriteLine();
                                    Console.Write($"Deck Size ({gameSettings.GetCurrentSetting.DeckSize}): ");
                                    string deckSize = Console.ReadLine();
                                    if (int.TryParse(deckSize, out int decks))
                                    {
                                        newSetting.DeckSize = decks;
                                    }
                                    Console.WriteLine();

                                    ConsoleExtension.WriteLine("(OPTIONAL) Do you want to Configure Suit names? Yes/No", ConsoleColor.DarkGray);
                                    string response = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(response) && (response.ToLower() == "y" || response.ToLower() == "yes"))
                                    {
                                        string[] suit = new string[4];
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Console.Write($"Suit {i + 1} name: ");
                                            suit[i] = Console.ReadLine();
                                        }

                                        newSetting.Suit = suit;
                                    }

                                    if (gameSettings.ValidateConfiguration(newSetting))
                                    {
                                        gameSettings.UpdateConfiguration(newSetting);
                                        Console.WriteLine();
                                        ConsoleExtension.WriteLine("Configuration Updated!", ConsoleColor.Green);
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        ConsoleExtension.WriteLine("Validation failed. Configuration restored to default.", ConsoleColor.Red);
                                        Console.WriteLine();
                                    }
                                }
                                
                                break;

                            case 3:
                                // Rules 
                                string rule = Utilities.GetResourceString("Content.Rule.md");
                                Console.WriteLine(rule);
                                Console.WriteLine();
                                break;

                            case 4:
                                // Exist the Game
                                Environment.Exit(0);
                                break;

                            default:
                                ConsoleExtension.WriteLine("Invalid Choice. Please Try Again", ConsoleColor.Red);
                                break;

                        }
                    }
                    else
                    {
                        ConsoleExtension.WriteLine("Invalid Choice. Please Try Again", ConsoleColor.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleExtension.WriteLine($"Exception Occured - {ex.Message}", ConsoleColor.Red);
                Environment.Exit(0);
            }
        }
    }
}
