using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private PerguntasSO perguntaAtual;
    [SerializeField] private TextMeshProUGUI textoEnunciado;
    [SerializeField] private GameObject[] alternativaTMP;
    [Header ("sprites")]
    [SerializeField] private Sprite spritealtCorreta;
    [SerializeField] private Sprite spritealtIcorreta;
    
    // Start is called before the first frame update
    void Start()
    {
        textoEnunciado.SetText(perguntaAtual.getEnunciado());
        string[] alternativas = perguntaAtual.getAlternativas();
        for (int i = 0; i < alternativas.Length; i++)
        {
            TextMeshProUGUI alt = alternativaTMP[i].GetComponentInChildren<TextMeshProUGUI>();
            alt.SetText(alternativas[i]);
        }
    }
    public void HandleOption(int alternativaSelecionada)
    {
        if(alternativaSelecionada == perguntaAtual.getRespostaCorreta()){
            Debug.Log("ganhoooo");
        }else{
            Debug.Log("perdeuuuuuuu");
        }
    }
    public void ChangeButtonSprite(Image imgBtn, Sprite spriteAlt){
        imgBtn.sprite = spriteAlt;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
