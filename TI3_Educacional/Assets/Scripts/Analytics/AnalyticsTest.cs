using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Net.Mail;
using System;

public class AnalyticsTest : MonoBehaviour
{
    public List<AnalyticsData> data;
    public static AnalyticsTest instance;
    public float sceneTimer;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        AddAnalytics(this.name, "Sessão iniciada", DateTime.Now.ToString("d/M/y hh:mm"));
    }

    public void AddAnalytics(string sender, string track, string value)
    {
        AnalyticsData d = new AnalyticsData(Time.time, sender, track, value);
        Debug.Log("Send: " + d.remetente + ", Track: " + d.evento + ", Value: " + d.valor);
        data.Add(d);
    }

    public void Save()
    {
        AnalyticsFile f = new AnalyticsFile();
        f.dados = data.ToArray();
        string json = JsonUtility.ToJson(f, true);
        try
        {
            SaveFile(json);
        }
        catch
        {

        }
        try
        {
            SendEmail(json);
        }
        catch
        {

        }
    }

    public void SaveFile(string text)
    {
        string path = Application.persistentDataPath + "/analytics.txt";
        Debug.Log("Arquivado salvo em: " + path);
        File.WriteAllText(path, text);
    }

    public void SendEmail(string text)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("fisiovrjogo@gmail.com", "yavokpljshvwqixe"),
            EnableSsl = true
        };
        client.Send("fisiovrjogo@gmail.com", "fisiovrjogo@gmail.com", "Análise do jogo", text);
        Debug.Log("Email enviado");
    }

    //private void OnApplicationFocus(bool pause)
    //{
    //    Save();
    //}
}
