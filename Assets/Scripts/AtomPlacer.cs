using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using static ElementManager;
using static ButtonManager;
    
public class AtomPlacer : MonoBehaviour
{
    public GameObject SelectedAtom;
    [SerializeField] private GameObject dropdown;
    [SerializeField] public GameObject Atoms;
    [SerializeField] public GameObject Bonds;
    [SerializeField] public GameObject Destroylanmayan;
    [SerializeField] public GameObject popup;
    [SerializeField] public TextMeshProUGUI tmpro;
    public static int currAtom = 0;

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
        Atoms = GameObject.Find("Atoms");
        Bonds = GameObject.Find("Bonds");
        foreach(Transform asad in Atoms.transform)
        {
            asad.gameObject.SetActive(true);
        }
        foreach(Transform asad in Bonds.transform)
        {
            asad.gameObject.SetActive(true);
        }
        Destroylanmayan = GameObject.Find("Destroylanmayan");
        foreach(Transform atoms in Destroylanmayan.transform)
        {
            atoms.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SelectedAtom = Resources.Load("Carbon") as GameObject;

    }
    
    private void SpawnAtMousePos()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject() && (ButtonManager.selection == "ElementPlacerButton"))
            {
                if((ElementManager.isTouching == false))
                {
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 800f;
                    Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
                
                    GameObject ins = Instantiate(SelectedAtom, pos, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
                    ins.name = SelectedAtom.name + "." + currAtom.ToString();
                    ins.transform.parent = Atoms.transform;
                    currAtom++;

                }
                else if((ElementManager.isTouching) && (SelectedAtom.name != ElementManager.elementObject.name.Split('.')[0]))
                {
                    GameObject ins = Instantiate(SelectedAtom, ElementManager.elementObject.transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
                    ins.name = SelectedAtom.name + "." + (ElementManager.elementObject.name.Split('.')[1]).ToString();
                    DestroyImmediate(ElementManager.elementObject);
                    ins.transform.parent = Atoms.transform;
                    currAtom++;
                }
            }
            
        }
    }
    public void OnItemSelected(int input)
    {
        TMPro.TMP_Dropdown drop = dropdown.GetComponent<TMPro.TMP_Dropdown>();
        // ButtonManager.OnSelectionButtonPressed();
        ButtonManager.selection = "ElementPlacerButton";

        string elementName = drop.options[input].text;
        if(elementName == "Other...")
        {
            popup.SetActive(true);
        }
        else
        {
            SelectedAtom = Resources.Load(elementName) as GameObject;
        }
    }
    // Update is called once per frame
    void Update()
    {
        SpawnAtMousePos();

    }
}

