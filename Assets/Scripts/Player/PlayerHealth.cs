using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public bool dead;
    public bool takeDamage;
    void Start()
    {
        takeDamage = true;
        currentHealth = maxHealth;
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
