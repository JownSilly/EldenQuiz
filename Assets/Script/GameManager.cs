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
    [SerializeField] private Sprite spritealtIncorreta;
    
    // Start is called before the first frame update
    void Start()
    {
        Timer temporizador = GetComponent<Timer>();
        temporizador.RegistraProximaQuestao();
        textoEnunciado.SetText(perguntaAtual.getEnunciado());
        string[] alternativas = perguntaAtual.getAlternativas();
        for (int i = 0; i < alternativas.Length; i++)
        {
            TextMeshProUGUI alt = alternativaTMP[i].GetComponentInChildren<TextMeshProUGUI>();
            alt.SetText(alternativas[i]);
        }
    }
    //Manuseia as op�oes selecionadas
    public void HandleOption(int alternativaSelecionada)
    {
        if(alternativaSelecionada == perguntaAtual.getRespostaCorreta()){
            ChangeButtonSprite(alternativaTMP[alternativaSelecionada].GetComponent<Image>(), spritealtCorreta);
            DisableOptionButtons(alternativaSelecionada);
            Debug.Log("ganhoooo");
        }else{
            ChangeButtonSprite(alternativaTMP[alternativaSelecionada].GetComponent<Image>(), spritealtIncorreta);
            Debug.Log("perdeuuuuuuu");
            DisableOptionButtons(alternativaSelecionada);
            ChangeButtonSprite(alternativaTMP[perguntaAtual.getRespostaCorreta()].GetComponent<Image>(), spritealtCorreta);
        }
    }
    //Fun�ao para alterar o sprite dos botoes selecionados
    public void ChangeButtonSprite(Image imgBtn, Sprite spriteAlt){
        imgBtn.sprite = spriteAlt;
    }
    //Fun��o para Desabilitar os Botoes desnecess�rios
    public void DisableOptionButtons(int alternativaSelecionada)
    {
        for(int i= 0; i< alternativaTMP.Length; i++)
        {
            Button btn = alternativaTMP[i].GetComponent<Button>();
            if(alternativaSelecionada != i)
            {
                btn.interactable = false;
            }
            else
            {
                btn.enabled = false;
            }
            
        }
    }
    public void 
    // Update is called once per frame
    void Update()
    {
        
    }
}
