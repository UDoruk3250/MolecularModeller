using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static ElementManager;
using static AtomPlacer;
using static UnityEngine.Mathf;


public class BondManager : MonoBehaviour
{
    private LineRenderer line;
    private LineRenderer line1;
    private LineRenderer line2;
    private GameObject DBond;
    private Vector3 mousePos;
    private Vector3 pos;
    public Material material;
    private int currLines = 0;
    private GameObject startAtom;
    public float distance = 20f;
    [SerializeField] public GameObject Bonds;
    // Start is called before the first frame update
    private void Awake() 
    { 
        
        Bonds = GameObject.Find("Bonds");
    }
    void Start()
    {
        
    }
    private void FormABond()
    {
        // Abort the bonding process if pressed "esc"
        if (Input.GetKeyDown("escape"))
        {
            if(line != null){Object.DestroyImmediate(line.gameObject);}
            else if(line1 != null){
                Object.DestroyImmediate(line1.gameObject);
                Object.DestroyImmediate(line2.gameObject);
            }
            line = null;
            line1 = null;
            line2 = null;
        }
        //HIGHLIGHT
        // If mouse button is clicked:
        if(Input.GetMouseButtonDown(0))
        {
            // If the mouse is over an element:
            if((ElementManager.isTouching == true))
            {
                // If the selected bond type is Single Bond:

                if (line == null  && (ButtonManager.selection == "BondButton"))
                {
                    createLine();
                    pos = Input.mousePosition;
                    pos.z = 841f; //842f
                    mousePos = Camera.main.ScreenToWorldPoint(pos);
                    mousePos = new Vector3(mousePos.x, mousePos.y, mousePos.z + 15);
                    startAtom = ElementManager.elementObject;
                    line.SetPosition(0, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y, ElementManager.elementObject.transform.position.z + 1f));
                    line.SetPosition(1, mousePos);
                }

                //If the selected bond type is Double Bond:

                else if(line1 == null  && line2 == null && (ButtonManager.selection == "DBondButton"))
                {
                    createDoubleLine();
                    pos = Input.mousePosition;
                    pos.z = 841f; // 842f
                    mousePos = Camera.main.ScreenToWorldPoint(pos);
                    mousePos = new Vector3(mousePos.x, mousePos.y, mousePos.z + 15);
                    startAtom = ElementManager.elementObject;
                    line1.SetPosition(0, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y + 10f, ElementManager.elementObject.transform.position.z + 1f));
                    line2.SetPosition(0, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y - 10f, ElementManager.elementObject.transform.position.z + 1f));
                    line1.SetPosition(1, mousePos);
                    line2.SetPosition(1, mousePos);
                }

                //If the bond is done and the mouse has clicked on the second element:
                else
                {
                    // This if block completes a single bonding task
                    // If the selected bond type is single bond:

                    if(ButtonManager.selection == "BondButton")
                    {
                        pos = Input.mousePosition;
                        pos.z = 841f;
                        mousePos = Camera.main.ScreenToWorldPoint(pos);
                        mousePos = new Vector3(mousePos.x, mousePos.y, mousePos.z + 15);

                        if(startAtom.name == ElementManager.elementObject.name)
                        {
                            Object.DestroyImmediate(line.gameObject);
                        }
                        else{
                            line.SetPosition(1, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y, ElementManager.elementObject.transform.position.z + 1));
                            line.name = "Bond" + "-" + currLines + "-" + startAtom.name + "-" + ElementManager.elementObject.name;
                            currLines++;
                        }
                        line = null;
                        line1 = null;
                        line2 = null;
                    }

                    // This if block completes a double bonding task
                    // If the selected bond type is Double Bond:

                    if(ButtonManager.selection == "DBondButton")
                    {
                        // pos = Input.mousePosition;
                        // pos.z = 841f;
                        // mousePos = Camera.main.ScreenToWorldPoint(pos);
                        // mousePos = new Vector3(mousePos.x, mousePos.y, mousePos.z + 15);

                        if(startAtom.name == ElementManager.elementObject.name)
                        {
                            Object.DestroyImmediate(line1.gameObject);
                            Object.DestroyImmediate(line2.gameObject);
                        }
                        else{
                            // line1.SetPosition(1, mousePos + new Vector3(distance * Mathf.Cos(result), distance * Mathf.Sin(result + (45 * Mathf.PI)), 0f));
                            // line2.SetPosition(1, mousePos + new Vector3(distance * Mathf.Cos(result + (45 * Mathf.PI)), distance * Mathf.Sin(result), 0f));
                            float result = (Mathf.Atan((ElementManager.elementObject.transform.position.x - startAtom.transform.position.x)/((ElementManager.elementObject.transform.position.y - startAtom.transform.position.y))));
                            
                            line1.SetPosition(1, ElementManager.elementObject.transform.position + new Vector3(distance * Mathf.Cos(result), distance * Mathf.Sin(result + (45 * Mathf.PI)), 1f));
                            line2.SetPosition(1, ElementManager.elementObject.transform.position + new Vector3(distance * Mathf.Cos(result + (45 * Mathf.PI)), distance * Mathf.Sin(result), 1f));
                                // line1.SetPosition(1, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y + 15f, ElementManager.elementObject.transform.position.z + 1));
                                // line2.SetPosition(1, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y - 15f, ElementManager.elementObject.transform.position.z + 1));
                            
                            // else
                            // {
                            //     line2.SetPosition(1, ElementManager.elementObject.transform.position + new Vector3(distance * Mathf.Cos(result), distance * Mathf.Sin(result + (45 * Mathf.PI)), 0f));
                            //     line1.SetPosition(1, ElementManager.elementObject.transform.position + new Vector3(distance * Mathf.Cos(result + (45 * Mathf.PI)), distance * Mathf.Sin(result), 0f));
                            
                            //     // line1.SetPosition(1, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y - 15f, ElementManager.elementObject.transform.position.z + 1));
                            //     // line2.SetPosition(1, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y + 15f, ElementManager.elementObject.transform.position.z + 1));
                            
                            // }
                            
                            line1.name = "DBond1." + "-" + currLines + "-" + startAtom.name + "-" + ElementManager.elementObject.name;
                            line2.name = "DBond2." + "-" + currLines + "-" + startAtom.name + "-" + ElementManager.elementObject.name;
                            DBond.name = "DBond" + "-" + currLines + "-" + startAtom.name + "-" + ElementManager.elementObject.name;
                            currLines++;
                        }
                        line = null;
                        line1 = null;
                        line2 = null;
                    }

                }
                
                //Instantiate(lineRend, pos, Quaternion.identity);
                // while (Input.GetMouseButtonDown(0))
                // {
                //     print("")
                // }
                // while (!Input.GetMouseButtonDown(0))
                // {
                //     //DrawLineBetweenObjects(pos, mousePos);
                // }
                

                // Instantiate(SelectedAtom, pos, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
            }
            //HIGHLIGHT
            //TODO creates empty carbon atom if clicked nowhere
            //TODO enable this code block after all the bugs are patched.

            /*
            else
            {
                //TODO
                
                if(line != null || line1 != null || line2 != null)
                {
                    pos = Input.mousePosition;
                    pos.z = 621f;
                    mousePos = Camera.main.ScreenToWorldPoint(pos);
                    mousePos = new Vector3(mousePos.x, mousePos.y, mousePos.z + 10);

                    GameObject tempAtom = Instantiate(Resources.Load("Carbon"), new Vector3(mousePos.x, mousePos.y, mousePos.z), Quaternion.Euler(new Vector3(-90f, 0f, 0f))) as GameObject;
                    tempAtom.name = "Carbon." + AtomPlacer.currAtom.ToString();
                    AtomPlacer.currAtom++;
                    tempAtom.transform.parent = GameObject.Find("Atoms").transform;

                    if(line != null) line.SetPosition(1, mousePos);
                    if(line1 != null) 
                    {
                        line1.name = "DBond1." + "-" + currLines + "-" + startAtom.name + "-" + ElementManager.elementObject.name;
                        line2.name = "DBond2." + "-" + currLines + "-" + startAtom.name + "-" + ElementManager.elementObject.name;
                        DBond.name = "DBond" + "-" + currLines + "-" + startAtom.name + "-" + ElementManager.elementObject.name;
                        currLines++;

                        mousePos = Camera.main.ScreenToWorldPoint(pos);
                        float result = Mathf.Abs(Mathf.Rad2Deg * (Mathf.Atan((mousePos.y - startAtom.transform.position.y)/((mousePos.x - startAtom.transform.position.x)))));
                        
                        
                        line1.SetPosition(1, mousePos + new Vector3(distance * Mathf.Cos(result), distance * Mathf.Sin(result + (45 * Mathf.PI)), 0f));
                        line2.SetPosition(1, mousePos + new Vector3(distance * Mathf.Cos(result + (45 * Mathf.PI)), distance * Mathf.Sin(result), 0f));

                        // line1.SetPosition(1, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y + 20f, ElementManager.elementObject.transform.position.z + 1));
                        // line2.SetPosition(1, new Vector3(ElementManager.elementObject.transform.position.x, ElementManager.elementObject.transform.position.y - 20f, ElementManager.elementObject.transform.position.z + 1));
                                
                    }
                    line = null;
                    line1 = null;
                    line2 = null;
                }
            }
            */
            
        }

        //HIGHLIGHT  HIGHLIGHT  HIGHLIGHT  HIGHLIGHT  HIGHLIGHT  HIGHLIGHT  HIGHLIGHT  HIGHLIGHT

        // If the mouse is not clicked:
        // If the the selected bond type is single bond
        // The mouse-following-bond-thing program 

        else if (!(Input.GetMouseButton(0)) && line != null)
        {
            if(ButtonManager.selection == "BondButton")
            {
                Vector3 pos = Input.mousePosition;
                pos.z = 841f;

                mousePos = Camera.main.ScreenToWorldPoint(pos);
                mousePos = new Vector3(mousePos.x, mousePos.y, mousePos.z + 15);

                line.SetPosition(1, mousePos);
            }
        }

        //HIGHLIGHT
        // If the mouse is not clicked and double bond is selected:
        // The mouse-following-bond-thing program 

        else if (!(Input.GetMouseButton(0)) && line1 != null && line2 != null)
        {
            if(ButtonManager.selection == "DBondButton")
            {
                Vector3 pos = Input.mousePosition;
                pos.z = 841f;

                mousePos = Camera.main.ScreenToWorldPoint(pos);
                float result = (Mathf.Atan((mousePos.x - startAtom.transform.position.x)/((mousePos.y - startAtom.transform.position.y))));
                

                // line1.SetPosition(0, new Vector3(startAtom.transform.position.x + Xpoint ,startAtom.transform.position.y + Ypoint, mousePos.z + 10));
                // line2.SetPosition(0, new Vector3(startAtom.transform.position.x - Xpoint ,startAtom.transform.position.y - Ypoint, mousePos.z + 10));
                
                line1.SetPosition(0, startAtom.transform.position + new Vector3(distance * Mathf.Cos(result), distance * Mathf.Sin(result + (45 * Mathf.PI)), 1f));
                line2.SetPosition(0, startAtom.transform.position + new Vector3(distance * Mathf.Cos(result + (45 * Mathf.PI)), distance * Mathf.Sin(result), 1f));
                //line2.SetPosition(0, new Vector3(-1f * Xpoint , -1f * Ypoint, mousePos.z + 10));

                // line1.SetPosition(1, new Vector3(mousePos.x, mousePos.y + 10f, mousePos.z + 10));
                // line2.SetPosition(1, new Vector3(mousePos.x, mousePos.y - 10f, mousePos.z + 10));
                
                line1.SetPosition(1, mousePos + new Vector3(distance * Mathf.Cos(result), distance * Mathf.Sin(result + (45 * Mathf.PI)), 0f));
                line2.SetPosition(1, mousePos + new Vector3(distance * Mathf.Cos(result + (45 * Mathf.PI)), distance * Mathf.Sin(result), 0f));
            }
            
        }
       
    }

    void Update()
    {
        FormABond();
    }
    //Create a single bond
    void createLine()
    {
        line = new GameObject("Bond" + currLines).AddComponent<LineRenderer>();
        line.transform.parent = Bonds.transform;
        line.material = material;
        line.positionCount = 2;
        line.startWidth = 10f;
        line.endWidth = 10f;
        line.useWorldSpace = false;
        line.numCapVertices = 50;
    }
    //Create a double bond

    void createDoubleLine()
    {
        DBond = new GameObject("DBond" + currLines);
        line1 = new GameObject("DBond1." + currLines).AddComponent<LineRenderer>();
        line2 = new GameObject("DBond2." + currLines).AddComponent<LineRenderer>();
        DBond.transform.parent = Bonds.transform;
        DBond.transform.position = new Vector3(DBond.transform.position.x, DBond.transform.position.y, 1.001f);
        line1.transform.parent = DBond.transform;
        line2.transform.parent = DBond.transform;

        line1.material = material;
        line2.material = material;

        line1.positionCount = 2;
        line2.positionCount = 2;

        line1.startWidth = 10f;
        line2.startWidth = 10f;

        line1.endWidth = 10f;
        line2.endWidth = 10f;

        line1.useWorldSpace = false;
        line2.useWorldSpace = false;

        line1.numCapVertices = 50;
        line2.numCapVertices = 50;
    }
}
