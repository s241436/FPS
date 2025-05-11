using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int points = 1;

    private int currentHealth;

    private GameManager gameManager;

    public GameManager GameManager { get { return gameManager; } set { gameManager = value; } }
    private void OnEnable()
    {
        //Set current health to max health
        currentHealth = maxHealth;
    }

    //Function to disable targets
    private void DisableTarget()
    {
        Debug.Log("TargetDown");
        if (gameManager != null)
        {
            gameManager.AddScore(points);
        }
        gameObject.SetActive(false);

        if (gameObject.tag == "Moving_Target")
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    //Public function that will be called by the bullet
    public void Damage(int damage)
    {
        currentHealth -= damage;
        //If our health is 0 or less, call Disable Targets
        if (currentHealth <= 0)
        {
            DisableTarget();
        }
    }
}
