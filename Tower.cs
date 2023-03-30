using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class Tower
    {
        public enum TowerType
        {
            Assault,
            Bombard,
            Canon
        }

        public TowerType Type { get; private set; }
        public int Health { get; private set; }
        public int FireRate { get; private set; }
        public int AttackRadius { get; private set; }
        public int Damage { get; private set; }
        public Vector2Int VTowerPosition { get; private set; }

        public Tower(TowerType towertype, Vector2Int position)
        {
            this.Type = towertype;
            this.VTowerPosition = position;
            Health = GetMaxHealth(towertype);
            FireRate = GetFireRate(towertype);
            AttackRadius = GetAttackRadius(towertype);
            Damage = GetDamage(towertype);
        }

        public void Attack(List<Tower> towers, TowerType towerType, List<Enemy> enemies) 
        {
            Enemy target = GetNearestEnemy(enemies);
            if (target != null) { target.TakeHit(Damage); }
            foreach (Tower tower in towers)
            {
                if (tower.Type != towerType)
                {
                    tower.TakeDamage(GetDamage(towerType));

                }
            }
        }

        public Enemy GetNearestEnemy(List<Enemy> enemies) // To Get the nearest enemy within the attack radius
        {
            Enemy nearestEnemy = null;
            double nearestDistance = double.MaxValue;
            foreach (Enemy enemy in enemies)
            {
                double distance = GetDistance(enemy.VEnemyPosition);
                if (distance <= AttackRadius && distance < nearestDistance)
                {
                    nearestEnemy = enemy;
                    nearestDistance = distance;
                }
            }

            return nearestEnemy;
        }

        private double GetDistance(Vector2Int _position) // Using Pythogras to calculate the distance between two positions
        {
            return Math.Sqrt(Math.Pow(_position.x - VTowerPosition.x, 2) + Math.Pow(_position.y - VTowerPosition.y, 2));
        }

        public int GetDamage(TowerType towertype) 
        {
            switch (towertype)
            {
                case TowerType.Assault: return 1;
                case TowerType.Bombard: return 2;
                case TowerType.Canon: return 1;
                default: return 0;
            }
        }

        public int GetAttackRadius(TowerType towertype)
        {
            switch (towertype)
            {
                case TowerType.Assault: return 3;
                case TowerType.Bombard: return 4;
                case TowerType.Canon: return 5;
                default: return 0;
            }
        }

        public int GetFireRate(TowerType towertype)
        {
            switch (towertype)
            {
                case TowerType.Assault: return 3;
                case TowerType.Bombard: return 2;
                case TowerType.Canon: return 3;
                default: return 0;
            }
        }

        public int GetMaxHealth(TowerType towertype)
        {
            switch (towertype)
            {
                case TowerType.Assault: return 3;
                case TowerType.Bombard: return 5;
                case TowerType.Canon: return 3;
                default: return 0;
            }
        }

        public void TakeDamage(int damage)
        {
            Health = -damage;
            if (Health < 0)
            {
                Health = 0;
               // Console.WriteLine("Tower Destroyed");
            }
        }

    }
}
