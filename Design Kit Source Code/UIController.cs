using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [Tooltip("This is the screen that will fade")]
    [SerializeField] Image blackScreen;

    [Tooltip("This is the speed of the fade")]
    [SerializeField] float fadeSpeed = 2f;
    [SerializeField] bool fading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fading = false;
            }
        }
    }

    public void fade()
    {
        fading = true;
    }
}
