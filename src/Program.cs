using SokoFarm.Core.Models;

// Support Emojis
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Begin
GameModel game = new();
game.Render();
while (!game.IsGameOver())
{
    game.Play();
}
// End
