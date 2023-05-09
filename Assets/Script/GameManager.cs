using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Questões do Quiz")]
    [SerializeField] private PerguntasSO perguntaAtual;
    [SerializeField] private PerguntasSO[] perguntasDoQuiz;
    [SerializeField] private TextMeshProUGUI textoEnunciado;
    [SerializeField] private GameObject[] alternativaTMP;
    private int indiceQuestion;
    [Header ("Alternar sprites")]
    [SerializeField] private Sprite spritealtCorreta;
    [SerializeField] private Sprite spritealtIncorreta;
    [SerializeField] private Timer temporizador;
    // Start is called before the first frame update
    void Start()
    {
        indiceQuestion = 0;
        temporizador.RegistrarParada(OnStoppedTimer);
        CallQuestion(indiceQuestion);
        //temporizador.RegistraProximaQuestao(CallQuestion);
    }
    //Manuseia as op�oes selecionadas
    public void HandleOption(int alternativaSelecionada)
    {
        DisableOptionButtons();
        StopTimer();

        if (alternativaSelecionada == perguntaAtual.getRespostaCorreta()){
            ChangeButtonSprite(alternativaTMP[alternativaSelecionada].GetComponent<Image>(), spritealtCorreta);
            
            Debug.Log("ganhoooo");
        }else{
            ChangeButtonSprite(alternativaTMP[alternativaSelecionada].GetComponent<Image>(), spritealtIncorreta);
            Debug.Log("perdeuuuuuuu");
            ChangeButtonSprite(alternativaTMP[perguntaAtual.getRespostaCorreta()].GetComponent<Image>(), spritealtCorreta);
        }
    }
    //Fun�ao para alterar o sprite dos botoes selecionados
    public void ChangeButtonSprite(Image imgBtn, Sprite spriteAlt){
        imgBtn.sprite = spriteAlt;
    }
    //Fun��o para Desabilitar os Botoes desnecess�rios
    public void DisableOptionButtons()
    {
        for(int i= 0; i< alternativaTMP.Length; i++)
        {
            Button btn = alternativaTMP[i].GetComponent<Button>();
                btn.enabled = false;
        }
    }

    public void CallQuestion(int indiceQuestion)
    {
        perguntaAtual = perguntasDoQuiz[indiceQuestion];
        textoEnunciado.SetText(perguntaAtual.getEnunciado());
        string[] alternativas = perguntaAtual.getAlternativas();
        for (int i = 0; i < alternativas.Length; i++)
        {
            TextMeshProUGUI alt = alternativaTMP[i].GetComponentInChildren<TextMeshProUGUI>();
            alt.SetText(alternativas[i]);
        }
    }
    //Faz a chamada do método do script Timer para parar o cronometro
    void StopTimer()
    {
        temporizador.Stop();
    }
    //Enquanto o Cronometro Estiver parado Executara uma co - rotina para esperar por 5 segundos e assim chamar a proxima questao
    public void OnStoppedTimer()
    {
        StartCoroutine(WaitingNextQuestion());
    }
    IEnumerator WaitingNextQuestion()
    {
        indiceQuestion++;
        if (indiceQuestion < perguntasDoQuiz.Length)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log(perguntasDoQuiz.Length);
            CallQuestion(indiceQuestion);
            temporizador.ResetTimer();
        }
        else {
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
