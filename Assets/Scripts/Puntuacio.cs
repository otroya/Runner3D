using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntuacio : MonoBehaviour
{
    [SerializeField]
    private GameManager m_GameManager;
    private TMPro.TextMeshProUGUI m_Text;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Text.text = "Score: " + m_GameManager.puntos.valorActual.ToString();
    }
}
