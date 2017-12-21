using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;
    [SerializeField] private bool isDead = false;

    public bool IsDead
    {
        get { return this.isDead; }
        protected set { this.isDead = value; }
    }

	// Use this for initialization
	void Start ()
	{
        this.health = this.maxHealth;
	}

    public void TakeDamage(int amt)
    {
        if (IsDead)
            return;

        this.health -= amt;

        if (this.health <= 0)
        {
            this.health = 0;
            IsDead = true;
        }
    }
}
