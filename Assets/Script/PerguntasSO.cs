using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
       menuName = "EldenQuiz/Nova Pergunta",
       fileName = "pergunta - ")]
public class PerguntasSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] private string enunciado;
    [SerializeField] private string[] alternativas;
    [SerializeField] private int respostaCorreta;
    [SerializeField] private string id;

    public int getRespostaCorreta()
    {
        return respostaCorreta;
    }
    public string[] getAlternativas()
    {
        return alternativas;
    }
    public string getEnunciado()
    {
        return enunciado;
    }
}
