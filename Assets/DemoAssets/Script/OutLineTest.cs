using System;
using UnityEngine;
using UnityEngine.Serialization;

public class OutLineTest : MonoBehaviour
{
    public Material outLineMat;
    private Material[] _outLineArray;
    private Material[] _originalArray;
    private MeshRenderer _meshRenderer;
    private bool _isApply;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalArray = _meshRenderer.materials;
        _outLineArray = new Material[_originalArray.Length+1];
        Array.Copy(_originalArray, _outLineArray, _originalArray.Length);
        _outLineArray[^1] = outLineMat;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (_isApply) _meshRenderer.materials = _originalArray;
            else _meshRenderer.materials = _outLineArray;
            _isApply = !_isApply;
        }    
    }
}
