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
    [SerializeField] private Sprite spriteDefault;
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
        DisableEnableOptionButtons(false);
        StopTimer();
        //Verifica qual foi a alternativa selecionada e se esta correta ou não, para alterar os sprite e dar um retorno visual da resposta
        Image imgAlternativaSelecionada = alternativaTMP[alternativaSelecionada].GetComponent<Image>();
        if (alternativaSelecionada == perguntaAtual.getRespostaCorreta()){
            ChangeButtonSprite(imgAlternativaSelecionada, spritealtCorreta);
        }else{
            Image imgAlternativaCorreta = alternativaTMP[perguntaAtual.getRespostaCorreta()].GetComponent<Image>();
            ChangeButtonSprite(imgAlternativaSelecionada, spritealtIncorreta);
            ChangeButtonSprite(imgAlternativaCorreta, spritealtCorreta);
        }
    }
    //Funcao para alterar o sprite dos botoes selecionados
    public void ChangeButtonSprite(Image imgBtn, Sprite spriteAlt){
        imgBtn.sprite = spriteAlt;
    }
    //Funcao para Desabilitar os Botoes desnecess�rios
    public void DisableEnableOptionButtons(bool isEnable)
    {
        for(int i= 0; i< alternativaTMP.Length; i++)
        {
            Button btn = alternativaTMP[i].GetComponent<Button>();
                btn.enabled = isEnable;
        }
    }
    //Funcao para Chamar Enunciado e as Questoes do Inicio ao Fim do Quiz
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
    // Habilita os Botoes Novamente e altera os sprites dos botoes para o spriteDefault
    public void StartAlternativesBtn()
    {
        DisableEnableOptionButtons(true);
        for (int i = 0; i < alternativaTMP.Length; i++)
        {
            Image alternatives = alternativaTMP[i].GetComponentInChildren<Image>();
            ChangeButtonSprite(alternatives, spriteDefault);
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
            StartAlternativesBtn();
            Debug.Log(indiceQuestion);
            CallQuestion(indiceQuestion);
            temporizador.ResetTimer();
        }
        else {
            yield return null;
        }
    }

    void Update()
    {
    }
}
