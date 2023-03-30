using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TowerDefense
{
    public class Game
    {
        public Vector2Int worldSize { get; private set; }
        private List<Tower> towers = new List<Tower>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<Vector2Int> enemyPath = new List<Vector2Int>();
        private int ticksSinceLastMove = 0;
        Action onGameOver;
        bool bGameOver;
        public Game(Vector2Int worldSize, List<Vector2Int> enemyPath, Action onGameOver)
        {
            this.enemyPath.Clear();
            this.enemies.Clear();
            this.towers.Clear();

            this.worldSize = worldSize;
            this.enemyPath = enemyPath;
            this.onGameOver = EndGame;
            // ...
        }

        public void Update()
        {
            if (bGameOver)
            {
                return;
            }
            foreach (Enemy enemy in enemies)
            {

                enemy.MoveEnemies(enemies, enemyPath);

                if (enemy.VEnemyPosition.x == enemyPath[enemyPath.Count - 1].x && enemy.VEnemyPosition.y == enemyPath[enemyPath.Count - 1].y)
                {

                    onGameOver?.Invoke();
                    break;
                }

            }

            foreach (var tower in this.towers)
            {
                if (enemies.Count != 0)
                {
                    tower.Attack(towers, tower.Type, enemies);
                    for (int i = 0; i < enemies.Count - 1; i++)
                    {
                        Enemy enemy = enemies[i];
                        if (enemy.Health <= 0)
                        {
                            enemies.RemoveAt(i);
                            Vector2Int position = enemies[i].VEnemyPosition;
                            GetDisplayChar(position);

                        }
                    }

                }

            }
            //if (towers.Count != 0)
            //{
            //    for (int i = 0; i < towers.Count - 1; i++)
            //    {
            //        if (towers[i].Health <= 0)
            //        {
            //            towers.RemoveAt(i);
            //            Vector2Int position = towers[i].VTowerPosition;
            //            GetDisplayChar(position);
            //        }
            //    }
            //}

        }
        public void EndGame() // Invoking this function with OnGameOver 
        {
            bGameOver = true;
            Console.WriteLine("GameOver");
        }

        /* SPAWNING TOWERS AND ENEMIES */
        public void SpawnAssaultTower(Vector2Int position)
        {
            Tower tower = new Tower(Tower.TowerType.Assault, position);
            towers.Add(tower);

        }
        public void SpawnBombardTower(Vector2Int position)
        {
            Tower tower = new Tower(Tower.TowerType.Bombard, position);
            towers.Add(tower);
        }
        public void SpawnCannonTower(Vector2Int position)
        {
            Tower tower = new Tower(Tower.TowerType.Canon, position);
            towers.Add(tower);
        }


        public void SpawnSlime()
        {
            Slime slime = new Slime(enemyPath[0]);
            enemies.Add(slime);
        }
        public void SpawnZombie()
        {
            Zombie zombie = new Zombie(enemyPath[0]);
            enemies.Add(zombie);
        }
        public void SpawnTank()
        {
            Tank tank = new Tank(enemyPath[0]);
            enemies.Add(tank);
        }

        public bool CanSpawnEnemy()
        {
            if (pathIsOccupied())
            {
                return false;
            }
            else { return true; }

        }

        private bool pathIsOccupied() // Checking if the path is occupied by the enemy or tower
        {
            foreach (Tower tower in towers)
            {
                if (enemyPath.Contains(tower.VTowerPosition))

                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            foreach (Enemy enemy in enemies)
            {
                if (enemyPath.Contains(enemy.VEnemyPosition))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }

        public char GetDisplayChar(Vector2Int position) // Displaying the character for the Towers and Enemies
        {

            foreach (Enemy enemy in enemies)
            {
                if (enemy.VEnemyPosition.x == position.x && enemy.VEnemyPosition.y == position.y)
                {
                    if (enemy is Slime) { return 's'; }
                    else if (enemy is Zombie) { return 'z'; }
                    else if (enemy is Tank) { return 't'; }
                    else { return '_'; }
                }
            }
            foreach (Tower tower in towers)
            {
                if (tower.VTowerPosition.x == position.x && tower.VTowerPosition.y == position.y)
                {
                    if (tower.Type == Tower.TowerType.Assault) { return 'A'; }
                    if (tower.Type == Tower.TowerType.Bombard) { return 'B'; }
                    if (tower.Type == Tower.TowerType.Canon) { return 'C'; }
                }

            }
            return '.';
        }
    }
}
