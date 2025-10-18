using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected string enemyName;

    private void Update()
    {
        moveAround();

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

}
