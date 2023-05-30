using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    private _PlayerMovement _PlayerMovement;

    private Vector3 respawnPosition;

    [Tooltip("This is the death effect player instantiate")]
    [SerializeField] private GameObject deathEffect;

    [Tooltip("Current values of your points")]
    [SerializeField] private int currentCoins;

    [Tooltip("The music that will play when you clear the level")]
    [SerializeField] private AudioSource levelEndMusic;

    [Tooltip("This number controls the level to load into when the player reachs the end")]
    [SerializeField] private int levelToLoad;

    [Tooltip("The UI components that updates the point text")]
    [SerializeField] private TMP_Text pointsText;

    [Header("Unity Events")]
    public UnityEvent resetHealth;
    public UnityEvent playerkilled;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _PlayerMovement = FindObjectOfType<_PlayerMovement>();

        respawnPosition = _PlayerMovement.transform.position;

        AddCoins(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCoins >= 100)
        {
            resetHealth.Invoke();
            currentCoins = 0;
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn Point Set");
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());

        playerkilled.Invoke();
    }

    public IEnumerator RespawnCo()
    {
       _PlayerMovement.gameObject.SetActive(false);

        Instantiate(deathEffect, _PlayerMovement.transform.position + new Vector3(0f, 1f, 0f), _PlayerMovement.transform.rotation);

        yield return new WaitForSeconds(2f);

        resetHealth.Invoke();

       _PlayerMovement.transform.position = respawnPosition;

        _PlayerMovement.gameObject.SetActive(true);
    }



    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        pointsText.text = "POINTS: " + currentCoins;
    }

    public void levelEnd()
    {
        StartCoroutine(LevelEndCo());
    }

    public IEnumerator LevelEndCo()
    {
        levelEndMusic.Play();
        
        yield return new WaitForSeconds(3f);

        Debug.Log("Level Ended");

        SceneManager.LoadScene(levelToLoad);
    }
}


