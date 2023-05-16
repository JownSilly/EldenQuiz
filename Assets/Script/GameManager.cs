using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;
public class GameManager : MonoBehaviour
{
    [Header("Questões do Quiz")]
    //Camada Modelo(PerguntasSO)
    [SerializeField] private PerguntasSO perguntaAtual;
    [SerializeField] private PerguntasSO[] perguntasDoQuiz;
    //Camada Visao
    [SerializeField] private TextMeshProUGUI textoEnunciado;
    [SerializeField] private GameObject[] alternativaTMP;
    [Header ("Sprites")]
    [SerializeField] private Sprite spriteDefault;
    [SerializeField] private Sprite spritealtCorreta;
    [SerializeField] private Sprite spritealtIncorreta;
    [Header("Canvas")]
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject QuizCanvas;
    [SerializeField] private GameObject StartGame;
    [SerializeField] private TextMeshProUGUI FeedBackTextTMP;
    //Camada Controller
    private int indiceQuestion;
    private int rightAlternatives;
    [SerializeField] private Timer temporizador;
    void Start()
    {
        
        rightAlternatives = 0;
        indiceQuestion = 0;
        endGame.SetActive(false);
        QuizCanvas.SetActive(true);
        temporizador.RegistrarParada(OnStoppedTimer);
        CallQuestion(indiceQuestion);
        StartAlternativesBtn();
    }
    //Manuseia as op�oes selecionadas
    public void HandleOption(int alternativaSelecionada)
    {
        DisableEnableOptionButtons(false);
        //Verifica qual foi a alternativa selecionada e se esta correta ou não, para alterar os sprite e dar um retorno visual da resposta
        Image imgAlternativaSelecionada = alternativaTMP[alternativaSelecionada].GetComponent<Image>();
        if (alternativaSelecionada == perguntaAtual.getRespostaCorreta()){
            ChangeButtonSprite(imgAlternativaSelecionada, spritealtCorreta);
            rightAlternatives++;
        }else{
            Image imgAlternativaCorreta = alternativaTMP[perguntaAtual.getRespostaCorreta()].GetComponent<Image>();
            ChangeButtonSprite(imgAlternativaSelecionada, spritealtIncorreta);
            ChangeButtonSprite(imgAlternativaCorreta, spritealtCorreta);
        }
        StopTimer();
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
    //Enquanto o Cronometro Estiver parado Executara uma co - rotina para esperar por 0,5 segundos e assim chamar a proxima questao
    public void OnStoppedTimer()
    {
        StartCoroutine(WaitingNextQuestion());   
    }

    IEnumerator WaitingNextQuestion()
    {
        indiceQuestion++;
        if (indiceQuestion < perguntasDoQuiz.Length)
        {
            yield return new WaitForSeconds(0.5f);
            StartAlternativesBtn();
            Debug.Log(indiceQuestion);
            CallQuestion(indiceQuestion);
            temporizador.ResetTimer();
        }
        else {
            
            Debug.Log("Acabou");
            EndGameScreen();
            yield break;
        }
    }
    void EndGameScreen()
    {
        TextMeshProUGUI FeedbackText = FeedBackTextTMP;
        FeedbackText.SetText("Meus Parabéns Guerreiro! \n <size=60%>Você Conseguiu se sobressair e acertou " + rightAlternatives+" questões de "+ perguntasDoQuiz.Length+"</size>");
        QuizCanvas.SetActive(false);
        endGame.SetActive(true);
        

    }
    public void ResetGameOnClick()
    {
        rightAlternatives = 0;
        indiceQuestion = 0;
        endGame.SetActive(false);
        QuizCanvas.SetActive(true);
        CallQuestion(indiceQuestion);
        StartAlternativesBtn();
        temporizador.ResetTimer();
        //StopCoroutine(WaitingNextQuestion());
    }
    void Update()
    {
    }
}
