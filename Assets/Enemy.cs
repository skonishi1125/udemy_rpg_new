using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected string enemyName;
    [SerializeField] protected float moveSpeed;

    private void Update()
    {
        //moveAround();

        if (Input.GetKeyDown(KeyCode.F))
            Attack();
    }

    // health, armor, etc...

    private void moveAround()
    {
        Debug.Log(enemyName + " moves at speed " + moveSpeed);
    }

    protected virtual void Attack()
    {
        Debug.Log(enemyName + " attacks!");
    }

    public void TakeDamage()
    {

    }

    public string GetEnemyName()
    {
        return enemyName;
    }

}
