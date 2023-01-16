using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameController gameController;
    private int currentHealth;

    public bool dead;
    public bool takeDamage;
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        takeDamage = true;
        currentHealth = gameController.GetMaxHealthValue();
        dead = false;
    }

    void Update()
    {
        if (currentHealth <= 0) { dead = true; }
    }

    public void TakeDamage(int damage) { if (takeDamage) currentHealth -= damage; }

    public void HealthUP (int heal) { currentHealth += heal; }

    public int GetCurrentHP() { return currentHealth; }
}
