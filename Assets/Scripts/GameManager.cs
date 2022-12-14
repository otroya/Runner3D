using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public ScriptableInteger puntos;
    [SerializeField] public CharacterController m_character;
    void Start()
    {
        ResetPuntos();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GanarPuntos()
    {
        puntos.valorActual += 1;
    }
    public void ResetPuntos()
    {
        if(m_character.spawn==0)
            puntos.valorActual = 0;
        if (m_character.spawn == 1)
            puntos.valorActual = 8;
    }
}
