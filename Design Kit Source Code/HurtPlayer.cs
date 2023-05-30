using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtPlayer : MonoBehaviour
{
    [Tooltip("Triggers the Player Knock Back and damage the player")]
    public UnityEvent hurt;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        hurt.Invoke();
    }
}
