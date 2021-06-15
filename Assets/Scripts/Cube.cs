using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    private static int _staticID = 0;

    [SerializeField] private TMP_Text[] _numbersText;

    [HideInInspector] public int cubeID;
    [HideInInspector] public int cubeNumber;
    [HideInInspector] public bool isMainCube;

    [HideInInspector] public Rigidbody cubeRigidbody;
    [HideInInspector] public Color cubeColor;

    private MeshRenderer _cubeMeshRenderer;

    private void Awake()
    {
        cubeID = _staticID++;
        _cubeMeshRenderer = GetComponent<MeshRenderer>();
        cubeRigidbody = GetComponent<Rigidbody>();
    }

    public void SetColor(Color color)
    {
        cubeColor = color;
        _cubeMeshRenderer.material.color = color;
    }

    public void SetNumber(int number)
    {
        cubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            _numbersText[i].text = number.ToString();
        }
    }

}
