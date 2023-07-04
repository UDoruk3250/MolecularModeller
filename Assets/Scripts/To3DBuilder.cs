using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class To3DBuilder : MonoBehaviour
{
    
    void Start()
    {
        
    }
    public static void Create3DModels(GameObject Atoms, GameObject Bonds, GameObject Everything)
    {
        foreach(Transform element in Atoms.transform)
        {
            element.gameObject.SetActive(false);
            print("Instantiated a " + element.name + ".");
            try
            {
                GameObject ins = Instantiate(Resources.Load("Models/" + element.name.Split('.')[0]), Camera.main.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(element.position)), Quaternion.identity) as GameObject;
                ins.name = element.name;
                ins.transform.parent = Everything.transform;
            }
            catch(Exception)
            {
                GameObject ins = Instantiate(Resources.Load("Models/defaultAtom"), Camera.main.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(element.position)), Quaternion.identity) as GameObject;
                ins.name = element.name;
                ins.transform.parent = Everything.transform;
            }
        }


        //Bond Builder
        foreach(Transform bond in Bonds.transform)
        {
            bond.gameObject.SetActive(false);
            string[] bondArgs = bond.name.Split('-');
            Transform firstAtom = Everything.transform.Find(bondArgs[2]);
            Transform secondAtom = Everything.transform.Find(bondArgs[3]);
            
            float distance = (float) Math.Sqrt(Math.Pow(secondAtom.position.x - firstAtom.position.x, 2) + Math.Pow(secondAtom.position.y - firstAtom.position.y, 2)) / 2f;
            // Vector3.Distance(firstAtom.position, secondAtom.position);
            
            float resultYRotate = (float)( Math.Atan((secondAtom.position.y - firstAtom.position.y)/(secondAtom.position.x - firstAtom.position.x)) * 180 / Math.PI);
            //float resultZRotate = (float)( Math.Atan((secondAtom.position.y - firstAtom.position.y)/(secondAtom.position.z - firstAtom.position.z)) * 180 / Math.PI);
            resultYRotate = resultYRotate + 90f;
            GameObject inst = Instantiate(Resources.Load("Bonds/" + bondArgs[0]), (firstAtom.position + secondAtom.position) / 2f,  Quaternion.Euler(new Vector3(0f, 0f, resultYRotate))) as GameObject;
            inst.transform.localScale = new Vector3(60f / (bondArgs[0].Length + 1f), distance, 60f / (bondArgs[0].Length + 1f));
            inst.transform.parent = Everything.transform;

            print("Distance: " + distance);
            print("Length: " + inst.transform.localScale.y);
        }
    }
    // Update is called once per frame

}
