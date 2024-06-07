using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public Button[] characterButtons; // Les boutons pour chaque personnage
    private bool[] charactersSelected; // Suivi des personnages sélectionnés
    private int playersReady = 0; // Nombre de joueurs prêts

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

            if (playersReady == 3) // Si tous les joueurs ont choisi
            {
                LoadGameScene();
            }
        }
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
