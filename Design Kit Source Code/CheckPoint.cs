using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    [Header("Check Points States")]
    [Tooltip("These are the states of the check points")]
    [SerializeField] private GameObject checkPointOn, CheckPointOff;

    [Header("Check Points Sound")]
    [Tooltip("The sounds that will play when check point is triggered")]
    [SerializeField] AudioSource checkPointSound;

    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.SetSpawnPoint(transform.position);
            checkPointSound.Play();

            CheckPoint[] allCheckPoint = FindObjectsOfType<CheckPoint>();
            for(int i = 0; i < allCheckPoint.Length; i++)
            {
                allCheckPoint[i].CheckPointOff.SetActive(true);
                allCheckPoint[i].checkPointOn.SetActive(false);
            }
            CheckPointOff.SetActive(false);
            checkPointOn.SetActive(true);

        }
    }
}
