using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//[RequireComponent(typeof(Slider))]

public class Timer : MonoBehaviour
{
    //Referencia ao Metodo GameManager, GameManager Delega função para Time
    public delegate void OnStoppedTimer();
    public OnStoppedTimer onStoppedTimer;
    [SerializeField] private float maxTime;
    private Slider slider;
    private float currentTime;
    public bool isCounting;

    // Start is called before the first frame update
    void Start()
    {
        isCounting = true;
        currentTime = 0f;
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = maxTime;
        slider.value = currentTime;
    }
    // Update is called once per frame
    void Update()
    {
        //Se Contagem for verdadeira, então o temporizador inicia 
        if (isCounting)
        {
            currentTime += 1 * Time.deltaTime;
            slider.value = currentTime;
            //Se o tempo atual for Maior q o Tempo Maximo, então é chamada a função para para o Cronometro; 
            if (currentTime > maxTime)
            {
                if(onStoppedTimer == null)
                {
                    return;
                }
                Stop();
            }
        }
    }
    // Registrada o Metodo dentro de uma variavel q faz referencia a função dentro do script GameManager, via delegate;
    public void RegistrarParada(OnStoppedTimer method)
    {
        onStoppedTimer += method;
    }
    //Parada do Cronometro, geralmente chamado quando o player selecionar um questao ou o tempo acabar
    public void Stop()
    {
        isCounting = false;
            if(onStoppedTimer != null)
            {
                onStoppedTimer();
            }
    }
    //Reinicia o Cronometro do Zero, geralmente chamado ao iniciar um nova Pergunta.
    public void ResetTimer()
    {
        currentTime = 0;
        slider.value = currentTime;
        isCounting = true;
        Debug.Log("Tempo Zerado");
    }
    /*public void RegistraProximaQuestao(NextQuestion metod){
        
        nextQuestion += metod;
    }
    */
}
