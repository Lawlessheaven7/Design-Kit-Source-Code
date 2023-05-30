using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]
    [Tooltip("Player current health value")]
    [SerializeField] public float currentHealth;

    [Tooltip("Player max Health value")]
    [SerializeField] public float maxHealth;

    [Header("Invincibility")]
    [Tooltip("How long player will be invincible after getting hurt")]
    [SerializeField] private float invincibleLength = 2f;

    [Tooltip("What objects on the player will flash when the player gets hurt")]
    [SerializeField] private GameObject[] playerPieces;

    [Header("Health Bar")]
    [Tooltip("Player Health Bar UI")]
    [SerializeField] private Slider HealthBar;

    [Tooltip("Sounds that will play when the player gets hurt")]
    [SerializeField] AudioSource HurtSound;

    private float invincibleCounter;

    [Header("Unity Events")]
    public UnityEvent Respawn;
    public UnityEvent knockBack;

    // Start is called before the first frame update
    void Start()
    {
        resetHealth();
  
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            for(int i = 0; i < playerPieces.Length; i++)
            {
                if(Mathf.Floor(invincibleCounter * 5f)% 2 == 0)
                {
                    playerPieces[i].SetActive(true);
                }
                else
                {
                    playerPieces[i].SetActive(false);
                }

                if(invincibleCounter <= 0)
                {
                  playerPieces[i].SetActive(true);
                }
            }

            if(invincibleCounter < 0)
            {
                invincibleCounter = 0;
            }

        }
    }

    public void Hurt()
    {
        if(invincibleCounter <= 0)
        {
            currentHealth -= 20;
            HurtSound.Play();
            updateUI();
            Debug.Log("Update Ui");

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Respawn.Invoke();
            }
            else
            {
                knockBack.Invoke();
                invincibleCounter = invincibleLength;
            }
        }
    }



    public void addHealth(int amountHeal)
    {
        currentHealth += amountHeal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateUI();
    }

    public void playerKilled()
    {
        currentHealth = 0;
        Debug.Log("Death");
        updateUI();
    }

    public void updateUI()
    {
        HealthBar.value = currentHealth;
    }

    public void resetHealth()
    {
        if (currentHealth >= 0)
        {
            currentHealth = maxHealth;
            Debug.Log("Called");
            updateUI();
        }
    }
}
