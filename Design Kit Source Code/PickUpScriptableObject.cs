using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickUpScriptableObject",
 menuName = "ScriptableObjects/PickUpScriptableObject")]

public class PickUpScriptableObject : ScriptableObject
{
    public int value;
    public int healAmount;
    public bool heal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
