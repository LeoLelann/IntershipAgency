using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static CharacterSelectionSO;
using UnityEngine.InputSystem;

public class CharacterSelectionManager : MonoBehaviour
{
    public PlayerToken[] characterButtons;
    [SerializeField] CharacterSelectionSO characterSelectionSO;
    [SerializeField] string _scene;

    private void Start() //LoadScene 1P
    {
        Debug.Log(characterButtons.Length);
        foreach (var button in characterButtons)
        {
            button.OnSelected.AddListener(CheckSelection);
        }
    }

    private void CheckSelection()
    {        foreach (var el in characterButtons)
        {
            if (el._isChoosed)
            {
                (Character, string)[] selection = new (Character, string)[characterButtons.Length];
                selection[0] = (el.CurrentSelection.PlayerRepresented,
                        el.GetComponent<PlayerInput>().actions.devices.Value[0].name);
                characterSelectionSO.SendSelection(selection);

                LoadGameScene();
            }
        }
    }

    /*private void Update()  //// LoadScene 3P
    {
        bool isReady = true;
        foreach(var el in characterButtons)
        {
            if(el._isChoosed == false) isReady = false;
        }

        if(isReady)
        {

            (Character, string)[] selection = new (Character, string)[3];
            for (int i = 0; i < characterButtons.Length; i++)
            {
                selection[i] = (characterButtons[i].CurrentSelection.PlayerRepresented, 
                    characterButtons[i].GetComponent<PlayerInput>().actions.devices.Value[0].name);
            }
            characterSelectionSO.SendSelection(selection);


            LoadGameScene();
        }
    }*/

    void LoadGameScene()
    {
        SceneManager.LoadScene(_scene);
    }
}
