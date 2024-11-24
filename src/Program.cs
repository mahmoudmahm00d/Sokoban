using Sokoban.Core.Models;

// Support Emojis
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Begin
GameModel game = new();
game.Render();
while (!GameModel.IsGameOver())
{
    game.Play();
}

// End
