using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    [Tooltip("Scriptable Objects")]
    [SerializeField] PickUpScriptableObject MSO;
    private int value;
    private int healAmount;

    [Tooltip("Sounds that will play")]
    [SerializeField] AudioSource PickUpSound;
   
    private GameManager GameManager;
    private HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        healthManager = FindObjectOfType<HealthManager>();
        value = MSO.value;
        healAmount = MSO.healAmount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !MSO.heal)
        {
            GameManager.AddCoins(value);

            PickUpSound.Play();

            Destroy(gameObject);
        }

        if(other.tag == "Player" && MSO.heal)
        {
            PickUpSound.Play();
            Destroy(gameObject);
            healthManager.addHealth(healAmount);
            
        }
    }
}
