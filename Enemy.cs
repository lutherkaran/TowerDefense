using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class Enemy
    {
        public int Health { get; protected set; }
        public int MoveRate { get; protected set; }
        public Vector2Int VEnemyPosition { get; set; }
        private int ticksSinceLastMove = 0;

        public void TakeHit(int damage) // Reduces Enemy's Health.
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;

            }
        }
        public void MoveEnemies(List<Enemy> enemies, List<Vector2Int> enemyPath) // To Move Enemies on the provided EnemyPath
        {

            foreach (Enemy enemy in enemies)
            {
                if (enemy.ticksSinceLastMove >= enemy.MoveRate)
                {
                    enemy.ticksSinceLastMove = 0;

                    int currentIndex = enemyPath.IndexOf(enemy.VEnemyPosition);
                    Vector2Int nextPosition = new Vector2Int(0, 0);
                    if (currentIndex < enemyPath.Count - 1)
                    {
                        nextPosition = enemyPath[currentIndex + 1];
                    }
                    bool isOccupied = false;
                    foreach (Enemy otherEnemy in enemies)
                    {
                        if (otherEnemy.VEnemyPosition.x == nextPosition.x && otherEnemy.VEnemyPosition.y == nextPosition.y)
                        {
                            isOccupied = true;
                            break;
                        }
                    }

                    if (!isOccupied && nextPosition.x <= enemyPath[enemyPath.Count - 1].x)
                    {
                        enemy.VEnemyPosition = nextPosition;
                    }
                }

                enemy.ticksSinceLastMove++;
            }

        }
    }
    public class Slime : Enemy
    {
        public Slime(Vector2Int _SlimeSpawnPosition)
        {
            VEnemyPosition = _SlimeSpawnPosition;
            Health = 3;
            MoveRate = 1;
        }

    }
    public class Zombie : Enemy
    {
        public Zombie(Vector2Int _ZombieSpawnPosition)
        {
            VEnemyPosition = _ZombieSpawnPosition;
            Health = 10;
            MoveRate = 2;
        }

    }
    public class Tank : Enemy
    {
        public Tank(Vector2Int _TankSpawnPosition)
        {
            VEnemyPosition = _TankSpawnPosition;
            Health = 20;
            MoveRate = 3;
        }
    }

}
