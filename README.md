# TowerDefense
The console project in C# for the GitHub repository is a simple tile-based Tower Defense game. 
The game consists of two main entities: enemies and towers. The objective of the game is to defend the path against the enemies, 
who are trying to reach the last position of the path.

The game follows a set of rules that include:

    The game will continue playing until an enemy reaches the last position on its path.
    Towers and enemies occupy one tile at a time, and only one enemy or tower can occupy a position at a time.
    Towers cannot be placed on the enemies' path.
    Towers can attack in any direction at any time if an enemy is within its radius.
    The euclidean distance is used to determine whether an enemy is within the radius of a tower.
    A tower will attack the enemy furthest along the enemy path.
    An enemy can move one tile every X ticks, which is defined per enemy.
    An enemy is always spawned at the start of the enemies' path and can only be spawned if there is no enemy at that position.

The projectiles in the game are not displayed on the board, and a tower will attack the enemy that is furthest along the path. 
The game's objective is to survive for as long as possible by defending the path against the enemies.
