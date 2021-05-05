using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atlas : MonoBehaviour
{
    [SerializeField] Atlas _knightAtlas;

    SpriteRenderer _spriteRederer;

    private void Awake()
    {
        _spriteRederer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
