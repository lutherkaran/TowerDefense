using System.Runtime.CompilerServices;
using TowerDefense;

namespace TowerDefense
{
    public class Program
    {
        static int Main(string[] args)
        {
            bool gameIsOver = false;
            Game game = new Game(new Vector2Int(8, 8),
                new List<Vector2Int>()
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(1, 1),
                    new Vector2Int(2, 1),
                    new Vector2Int(2, 2),
                    new Vector2Int(3, 2),
                    new Vector2Int(3, 3),
                    new Vector2Int(4, 3),
                    new Vector2Int(4, 4),
                    new Vector2Int(5, 4),
                    new Vector2Int(5, 5),
                    new Vector2Int(6, 5),
                    new Vector2Int(6, 6),
                    new Vector2Int(7, 6),
                    new Vector2Int(7, 7),
                },
                () => gameIsOver = true
                );

            game.SpawnAssaultTower(new Vector2Int(0, 2));
            game.SpawnAssaultTower(new Vector2Int(0, 4));
            game.SpawnAssaultTower(new Vector2Int(1, 2));
            game.SpawnAssaultTower(new Vector2Int(5, 3));
            game.SpawnAssaultTower(new Vector2Int(5, 4));

            game.SpawnBombardTower(new Vector2Int(2, 4));
            game.SpawnBombardTower(new Vector2Int(6, 4));

            game.SpawnCannonTower(new Vector2Int(3, 5));
            game.SpawnCannonTower(new Vector2Int(7, 5));


            while (!gameIsOver)
            {
                if (game.CanSpawnEnemy())
                {
                    Random random = new Random();
                    int randomEnemy = random.Next(0, 3);
                    //int randomLocationX = random.Next(0, 64);
                    //int randomLocationY = random.Next(0, 64);
                    //Vector2Int randomLocation = new Vector2Int(randomLocationX, randomLocationY);

                    switch (randomEnemy)
                    {
                        default:
                        case 0:
                            game.SpawnSlime();
                            break;

                        case 1:
                            game.SpawnZombie();
                            break;

                        case 2:
                            game.SpawnTank();
                            break;
                    }
                }

                game.Update();

                PrintBoard(game);

                Thread.Sleep(500);
            }

            Console.WriteLine("GAME OVER!");

            return 0;
        }
        private static void PrintBoard(Game game)
        {
            Console.Clear();
            Console.WriteLine("BOARD:");

            for (int j = 0; j < game.worldSize.y; j++)
            {
                for (int i = 0; i < game.worldSize.x; i++)
                    Console.Write(' ' + game.GetDisplayChar(new Vector2Int(i, j)).ToString());

                Console.Write("\n");
            }
        }
    }
}


