using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public double CalculateDistance(float StartX, float StartY, float EndX, float EndY)
    {
        double exit = Mathf.Sqrt(Mathf.Pow((StartX) - (EndX), 2) + Mathf.Pow((StartY) - (EndY), 2));
        return exit;
    }


    public Vector3 nextTick;
    public Rigidbody2D rigidBody;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = MapGenerator.instance.superPowerManager.directionOfBullet;
        Vector3 look = transform.InverseTransformPoint(direction+MapGenerator.instance.activePlayer.transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
        transform.Rotate(0, 0, angle - 90);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyToDestroy = null;
            foreach (Enemy enemy in MapGenerator.instance.listOfEnemies)
            {
                if(enemyToDestroy is null)
                {
                    enemyToDestroy = enemy;
                }
                else if(CalculateDistance(enemyToDestroy.transform.position.x,enemyToDestroy.transform.position.y,transform.position.x,transform.position.y)>CalculateDistance(enemy.transform.position.x, enemy.transform.position.y, transform.position.x, transform.position.y))
                {
                    enemyToDestroy = enemy;
                }
            }
            MapGenerator.instance.listOfEnemies.Remove(enemyToDestroy);
            Destroy(enemyToDestroy.gameObject);
        }
        Destroy(this.gameObject);
    }
    void Update()
    {
        nextTick = transform.position + direction;
        transform.position = Vector3.MoveTowards(transform.position, nextTick,0.1f);
        
    }
}
