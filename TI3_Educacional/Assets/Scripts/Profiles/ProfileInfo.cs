using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
[Serializable]
public class ProfileInfo
{
    //[Header("General")]
    public string patientName = "name";
    public int age = 99;
    public float height = 1.00f;
    public int dautonismo = 0;
    public float generalVolume = 0.5f;
    public float bgmVolume = 1f;
    public float sfxVolume = 1f;
    public float vrSensibility = 1f;

    //[Header("Feira") 
    public int fruitAmount = 3;
    public bool fruitMemorize = false;
    public int visitorAmount = 2;
    public float visitorSpeed = 4f;
    public float coinSize = 2f;
    public bool tutorialFeira = true; 

    //[Header("Bar") {get; set;}
    public float gameDuration = 60f;
    public int maxAngle = 70;
    public bool canUp = true;
    public bool canDown = true;
    public bool canRight = true;
    public bool canLeft = true;
    public bool tutorialBar = true;

    string destination;

    public ProfileInfo(string patientName)
    {
        this.patientName = patientName;
    }

    public void Save(string patientName)
    {
        destination = Application.dataPath + "/" + patientName;
        var content = JsonUtility.ToJson(this, true);
        File.WriteAllText(destination, content);
    }

    public void Load(string patientName)
    {
        string destination = Application.persistentDataPath + "/" + patientName;
        var content = File.ReadAllText(destination);
        var p = JsonUtility.FromJson<ProfileInfo>(content);

        this.patientName = p.patientName;
        this.age = p.age;
        this.height = p.height;
        this.dautonismo = p.dautonismo;
        this.generalVolume = p.generalVolume;
        this.bgmVolume = p.bgmVolume;
        this.sfxVolume = p.sfxVolume;
        this.vrSensibility = p.vrSensibility;

        this.fruitAmount = p.fruitAmount;
        this.fruitMemorize = p.fruitMemorize;
        this.visitorAmount = p.visitorAmount;
        this.visitorSpeed = p.visitorSpeed;
        this.coinSize = p.coinSize;
        this.tutorialFeira = p.tutorialFeira;

        this.gameDuration = p.gameDuration;
        this.maxAngle = p.maxAngle;
        this.canUp = p.canUp;
        this.canDown = p.canDown;
        this.canRight = p.canRight;
        this.canLeft = p.canLeft;
        this.tutorialBar = p.tutorialBar;
    }

}
