using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public Button[] characterButtons;
    private bool[] charactersSelected;
    private int playersReady = 0;

    void Start()
    {
        charactersSelected = new bool[characterButtons.Length];
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        }
    }

    void OnCharacterSelected(int index)
    {
        if (!charactersSelected[index])
        {
            charactersSelected[index] = true;
            playersReady++;
            characterButtons[index].interactable = false; // Désactive le bouton

            if (playersReady == 3)
            {
                LoadGameScene();
            }
        }
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("Tutoriel1");
    }
}
