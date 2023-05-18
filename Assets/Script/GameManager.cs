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
    [SerializeField] private GameObject resultadoFeedBack; 
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject quizCanvas;
    [SerializeField] private GameObject startGame;
    [SerializeField] private TextMeshProUGUI FeedbackTextTMP;
    [SerializeField] private TextMeshProUGUI FeedbackinGameTextTMP;
    //Camada Controller
    private int indiceQuestion;
    private int rightAlternatives;
    [SerializeField] private Timer temporizador;
    private bool isCorrect;

    void Start()
    {
        temporizador.RegistrarParada(OnStoppedTimer);
        ChangeGameScreen(0);
    }

    //Botoes Funcoes Chamadas ao Clicar em Um Botao
        //Manuseia as op�oes selecionadas
            public void HandleOption(int alternativaSelecionada)
            {
                DisableEnableOptionButtons(false);
                //Verifica qual foi a alternativa selecionada e se esta correta ou não, para alterar os sprite e dar um retorno visual da resposta
                Image imgAlternativaSelecionada = alternativaTMP[alternativaSelecionada].GetComponent<Image>();
                if (alternativaSelecionada == perguntaAtual.getRespostaCorreta()){
                    
                    ChangeButtonSprite(imgAlternativaSelecionada, spritealtCorreta);
                    rightAlternatives++;
                    isCorrect = true;
                }else{
                    Image imgAlternativaCorreta = alternativaTMP[perguntaAtual.getRespostaCorreta()].GetComponent<Image>();
                    ChangeButtonSprite(imgAlternativaSelecionada, spritealtIncorreta);
                    ChangeButtonSprite(imgAlternativaCorreta, spritealtCorreta);
                    
                    isCorrect = false;
                }
                
                StopTimer();
                
            }
        //Reiniciar o Quiz dosde a Primeira Questao
            public void StartQuizGame()
            {   
                rightAlternatives = 0;
                indiceQuestion = 0;
                CallQuestion(indiceQuestion);
                StartAlternativesBtn();
                temporizador.ResetTimer();
            }
    //FeedbackQuestion
    public void FeedbackQuestionAnswer()
    {
        resultadoFeedBack.GetComponent<Canvas>().gameObject.SetActive(true);
        if (temporizador.timerIsOver() || !isCorrect)
        {
            resultadoFeedBack.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(0xB7, 0x42, 0x42, 0xFF);
            FeedbackinGameTextTMP.SetText("<color=#B74242>Que pena, errou! Continue tentando \n A resposta correta é:</color> \n\n\n <color=white> " + perguntaAtual.getAlternativas()[perguntaAtual.getRespostaCorreta()] + "  </color>");
        }
        else
        {
            resultadoFeedBack.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(0x40, 0xB0, 0x5F, 0xFF);
            FeedbackinGameTextTMP.SetText("<color=#40B05F>Parabéns, meu Nobre! \n A resposta correta é:</color> \n\n\n <color=white> " + perguntaAtual.getAlternativas()[perguntaAtual.getRespostaCorreta()] + "  </color>");
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
    //Enquanto o Cronometro Estiver parado Executara uma co - rotina para esperar por 0,5 segundos e assim chamar a proxima questao
    public void OnStoppedTimer()
    {
        //StartCoroutine(WaitingNextQuestion());
        FeedbackQuestionAnswer();
    }
    public void MoveToNextQuestion()
    {
        resultadoFeedBack.SetActive(false);
        indiceQuestion++;
        if (indiceQuestion < perguntasDoQuiz.Length)
        {
            StartAlternativesBtn();
            CallQuestion(indiceQuestion);
            temporizador.ResetTimer();
        }
        else
        {
            ChangeGameScreen(2);
        }
    }
    public void ChangeGameScreen(int Scene)
    {
        
        switch (Scene)
        {       //StartGameScreen
            case 0:
                
                    startGame.SetActive(true);
                    quizCanvas.GetComponent<Canvas>().enabled = false;
                    endGame.SetActive(false);
                    resultadoFeedBack.SetActive(false);
                break;
                //QuizCanvas
            case 1:
                    startGame.SetActive(false);
                    quizCanvas.GetComponent<Canvas>().enabled = true;
                    endGame.SetActive(false);
                    StartQuizGame();
                break;
                //EndGameOption
            case 2:
                    
                    FeedbackTextTMP.SetText("Parabéns Meu Nobre Guerreiro! \n <size=60%>Você Conseguiu se sobressair e acertou " + rightAlternatives + " questões de " + perguntasDoQuiz.Length + "</size>");
                    startGame.SetActive(false);
                    quizCanvas.GetComponent<Canvas>().enabled = false;
                    endGame.SetActive(true);
                    
                break;
                //Passa para Proxima pergunta ao aperta o botao continuar
            case 3:
                    MoveToNextQuestion();
                break;
        }
    }
    /*
    IEnumerator WaitingNextQuestion()
    {
        indiceQuestion++;
        if (indiceQuestion < perguntasDoQuiz.Length)
        {
            yield return new WaitForSeconds(0.5f);
            if(!isClicked){
                FeedbackQuestionAnswer(false);
            }
            StartAlternativesBtn();
            CallQuestion(indiceQuestion);
            temporizador.ResetTimer();
        }
        else {
            EndGameScreen();
            yield break;
        }
    }
    void EndGameScreen()
    {
        TextMeshProUGUI FeedbackText = FeedBackTextTMP;
        FeedbackText.SetText("Meus Parabéns Guerreiro! \n <size=60%>Você Conseguiu se sobressair e acertou " + rightAlternatives+" questões de "+ perguntasDoQuiz.Length+"</size>");
        quizCanvas.SetActive(false);
        endGame.SetActive(true);
        

    }
    */

    void Update()
    {
    }
}
