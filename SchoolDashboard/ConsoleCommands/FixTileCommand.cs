using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.ConsoleCommands
{
    class FixTileCommand : ConsoleCommand
    {
        public override bool IsMatch(string command)
        {
            return command.ToLower().StartsWith("fixtile");
        }

        public override void Process(string command)
        {
            var words = command.ToLower().Split(' ');
            if (words.Length == 1)
            {
                ShowTilesList();
                return;
            }
            else
            {
                var argument = words[1].ToLower();

                if (argument == "reset" || argument == "r")
                {
                    Dashboard.GetInstance().Tiles.FixedTile = null;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Tile unfixed.");
                    Console.ResetColor();
                    return;
                }

                var tiles = Dashboard.GetInstance().Tiles.Tiles;
                var selectedTile = tiles.FirstOrDefault(t => t.GetType().Name.ToLower() == argument);
                if (selectedTile != null)
                {
                    Dashboard.GetInstance().Tiles.FixedTile = selectedTile;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Tile fixed.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid tile name");
                Console.ResetColor();
            }
        }

        private void ShowTilesList()
        {
            var tiles = Dashboard.GetInstance().Tiles.Tiles.Select(t => t.GetType().Name);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("All tiles:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var tile in tiles)
                Console.WriteLine(tile);
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
