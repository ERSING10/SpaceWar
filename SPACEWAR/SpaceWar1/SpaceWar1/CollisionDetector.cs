using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Drawing;

namespace SpaceWar1
{
    public static class CollisionDetector
    {

        // Oyuncu ile düşmanın çarpışması
        public static void CheckCollision(Spaceship player, Enemy enemy)
        {
            
            if (enemy.getHealth() <= 0)
                return;

            
            if (player.GetSpaceshipImg().Bounds.IntersectsWith(enemy.enemyImg.Bounds))
            {
                
                player.TakeDamage(enemy.getDamage());
                enemy.puan = 0;
                enemy.Destroy();
                return;
                
            }
        }


        // Mermiler X düşman veya oyuncu
        public static void CheckBulletCollision(List<Bullet> bullets, List<Enemy> enemies)
        {
            foreach (var bullet in bullets.ToList()) // Her bir mermi için
            {
                foreach (var enemy in enemies.ToList()) 
                {
                    
                    if (enemy.getHealth() <= 0)
                        continue;

                    
                    if (bullet.bulletImg.Bounds.IntersectsWith(enemy.enemyImg.Bounds))
                    {
                        bullet.OnHit(enemy); 
                        bullet.Destroy(); 
                        

                       
                        if (enemy.getHealth() <= 0)
                        {
                            enemies.Remove(enemy); // Düşmanı listeden kaldır
                        }
                    }
                }
            }
        }

    }
}