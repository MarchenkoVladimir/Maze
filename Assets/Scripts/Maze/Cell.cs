using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _rightPart;
    [SerializeField] private GameObject _leftPart;
    [SerializeField] private GameObject _upPart;
    [SerializeField] private GameObject _downPart;
    [SerializeField] private GameObject _finishObject;

    public void SetWall(bool reght, bool left, bool up, bool down)
    {
        _rightPart.SetActive(reght);
        _leftPart.SetActive(left);
        _upPart.SetActive(up);
        _downPart.SetActive(down);
    }

    public void SetFinish()
    {
        _finishObject.SetActive(true);
    }
}
