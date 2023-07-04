using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static To3DBuilder;

public class test : MonoBehaviour
{
    [SerializeField] public GameObject Atoms;
    [SerializeField] public GameObject Bonds;

    private void Awake() 
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Undestructible");

        if (objs.Length > 3)
        {
            Destroy(objs[3]);   
            Destroy(objs[4]);   
            Destroy(objs[5]); 
        }
        foreach(GameObject obj in objs)
        {      //this.gameObject
            DontDestroyOnLoad(obj);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "3DScene")
        {
            To3DBuilder.Create3DModels(Atoms, Bonds, gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
