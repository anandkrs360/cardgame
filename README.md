# CARD GAME

Card game is simple game played with 2 players.

Requires .NET 5.0 

To download the game:

    git clone https://github.com/anandkrs360/cardgame.git

To buid game:

    cd CardGame
    dotnet build

To run game:

    dotnet run

To run test casess

    cd ..
    cd CardGame.Tests
    dotnet test

######################CARD GAME RULE####################

Objective of Game: The objective of the game is to win all cards.

Number of players: 2 players (Can be played with 3 or 4 players).

Number of Cards: 40 Cards (Deck Size can be configured)

# How to Play

1. Each card shows a number from 1 to 10. Each number is in the deck four times for a total of 40 cards.
2. At the beginning of the game, the deck is shuffledin random order.
3. Each Player receives a stack of 20 cards from the shuffled deck as their draw pile.
4. The draw pile is kept face-down in front of the player. Player will also keep a discard pile.
5. Each turn, both players draw the top card.
6. The player with the higher value card, takes both cards and add them to their discard pile.
7. If the cards show same value, the winner of the next turn wins these cards as well.
8. If there are no more cards in the draw pile, cards from discard pile will be shuffled and added draw pile.
9. Once the player has no cards in either draw or discard pile will loose the game.
10. The player that have all the cards will be the winner of the game.

#############################################################

# EXAMPLE OUTPUT

```
Player 1 (20 cards): 8
Player 2 (20 cards): 1
Player 1 wins this round

Player 1 (21 cards): 1
Player 2 (19 cards): 10
Player 2 wins this round

[...]

Player 1 (38 cards): 4
Player 2 (2 cards): 4
No winner in this round

Player 1 (37 cards): 7
Player 2 (1 cards): 3
Player 1 wins this round

Player 1 wins the game!
```
