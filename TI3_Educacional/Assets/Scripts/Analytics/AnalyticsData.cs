using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AnalyticsData
{
    public float tempo;
    public string remetente;
    public string evento;
    public string valor;

    public AnalyticsData(float time, string sender, string track, string value)
    {
        this.tempo = time;
        this.remetente = sender;
        this.evento = track;
        this.valor = value;
    }

}
[Serializable]
public class AnalyticsFile
{
    public AnalyticsData[] dados;
}
