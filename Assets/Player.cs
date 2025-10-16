using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "Bob the hero"; // This box (variable) called "playerName" holds the text "Bob". 
    public int age = 25; // This box (variable) called "age" holds the number 25.
    public float moveSpeed = 2.5f; // This box (variable) called "moveSpeed" holds the decimal number 2.5.
    public bool gameOver = true; // This box (variable) called "gameOver" holds the value true.

    public Rigidbody2D rb;

    private void Start()
    {

    }

}
