using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public static bool isTouching = false;
    public static GameObject elementObject;
    GameObject glow;
    // Start is called before the first frame update
    void Start()
    {
        glow = transform.GetChild(1).gameObject;
    }
    void OnMouseOver(){
        isTouching = true;
        elementObject = gameObject;
        glow.GetComponent<MeshRenderer>().enabled = true;

    }
    void OnMouseExit(){
        
        isTouching = false;

        glow.GetComponent<MeshRenderer>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
