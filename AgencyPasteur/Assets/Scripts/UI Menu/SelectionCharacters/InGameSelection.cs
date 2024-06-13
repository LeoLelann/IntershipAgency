using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSelection : MonoBehaviour
{
    [SerializeField] CharacterSelectionSO _CharaSelectionSO;
    [SerializeField] int _idPlayer;

    [SerializeField] GameObject _p1;
    [SerializeField] GameObject _p2;
    [SerializeField] GameObject _p3;

    private void Awake()
    {
        //if (_CharaSelectionSO.Selection[_idPlayer] == CharacterSelectionSO.Character.Oliv)
        //{
        //    _p1.transform.SetParent(this.transform);
        //    _p1.SetActive(true);
        //    _p1.transform.position = this.transform.position;
        //}
        //else if (_CharaSelectionSO.Selection[_idPlayer] == CharacterSelectionSO.Character.Lesha)
        //{
        //    _p2.transform.SetParent(this.transform);
        //    _p2.SetActive(true);
        //    _p2.transform.position = this.transform.position;
        //}
        //else if (_CharaSelectionSO.Selection[_idPlayer] == CharacterSelectionSO.Character.Britney)
        //{
        //    _p3.transform.SetParent(this.transform);
        //    _p3.SetActive(true);
        //    _p3.transform.position = this.transform.position;
        //}
    }
}
