using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static To3DBuilder;

public class ButtonManager : MonoBehaviour 
{
    // C8C8C8 - Pressed
    // 
    public static string selection;
    [SerializeField] public Transform[] Buttons;
    public static GameObject clickedButton;
    void Start()
    {
        selection = "ElementPlacerButton";
    }
    public void OnSelectionButtonPressed()
    {
         foreach (Transform buttonChild in Buttons)
         {
            var Colorasd = buttonChild.GetComponent<Button>().colors;
            Colorasd.normalColor = new Color(1f, 1f, 1f);
            buttonChild.GetComponent<Button>().colors = Colorasd; 

        }
        clickedButton = EventSystem.current.currentSelectedGameObject;

        var Colors = clickedButton.GetComponent<Button>().colors;
        Colors.normalColor = new Color(0.44f, 0.43f, 0.43f);
        clickedButton.GetComponent<Button>().colors = Colors; 

        selection = EventSystem.current.currentSelectedGameObject.name;

    }
    public void On3DButtonPressed()
    {
        // To3DBuilder.Create3DModels();
        SceneManager.LoadScene("3DScene");
    }
    public void On2DButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    void Update()
    {
        
    }
     
}
