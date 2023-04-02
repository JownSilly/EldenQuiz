using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PerguntasSO perguntaAtual;
    [SerializeField] private TextMeshProUGUI textoEnunciado;
    [SerializeField] private GameObject[] alternativaTMP;
    // Start is called before the first frame update
    void Start()
    {
        textoEnunciado.SetText(perguntaAtual.getEnunciado());
        string[] alternativas = perguntaAtual.getAlternativas();
        for (int i = 0; i < alternativas.Length; i++)
        {
            TextMeshProUGUI alternativa = alternativaTMP[i].GetComponentInChildren<TextMeshProUGUI>();
            alternativa.SetText(alternativas[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
