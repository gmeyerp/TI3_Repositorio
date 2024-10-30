using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class ProfileInfo
{
    public enum Info
    {
        // Geral
        stringPatientName,
        intAge,
        intDautonismo,
        floatHeight,
        floatGeneralVolume,
        floatBgmVolume,
        floatSfxVolume,
        floatVrSensibility,

        // Feira
        intFruitAmount,
        intVisitorAmount,
        floatVisitorSpeed,
        floatCoinSize,
        boolFruitMemorize,
        boolTutorialFeira,

        // Barra
        intMaxAngle,
        floatGameDuration,
        boolCanUp,
        boolCanDown,
        boolCanRight,
        boolCanLeft,
        boolTutorialBar,
    }

    private Dictionary<Info, object> info = new()
    {
        // Geral
        { Info.stringPatientName, "name" },
        { Info.intAge, 99 },
        { Info.intDautonismo, 0 },
        { Info.floatHeight, 1.00f },
        { Info.floatGeneralVolume, 0.5f },
        { Info.floatBgmVolume, 1f },
        { Info.floatSfxVolume, 1f },
        { Info.floatVrSensibility, 1f },

        // Feira
        { Info.intFruitAmount, 3 },
        { Info.intVisitorAmount, 2 },
        { Info.floatVisitorSpeed, 4f },
        { Info.floatCoinSize, 2f },
        { Info.boolFruitMemorize, false },
        { Info.boolTutorialFeira, true },

        // Barra
        { Info.intMaxAngle, 70 },
        { Info.floatGameDuration, 60f },
        { Info.boolCanUp, true },
        { Info.boolCanDown, true },
        { Info.boolCanRight, true },
        { Info.boolCanLeft, true },
        { Info.boolTutorialBar, true },
    };

    public ProfileInfo(string patientName)
    {
        info[Info.stringPatientName] = patientName;
    }

    private string GetDestination() => $"{Application.dataPath}/Perfis/{info[Info.stringPatientName]}.txt";

    public object Get(Info info)
    {
        return this.info[info];
    }
    public void Set(Info info, object value)
    {
        this.info[info] = value;
    }

    public void Save()
    {
        FileInfo fileInfo = new FileInfo(GetDestination());
        if (!fileInfo.Directory.Exists)
        { Directory.CreateDirectory(fileInfo.Directory.FullName); }

        string content = JsonConvert.SerializeObject(info, Formatting.Indented);
        File.WriteAllText(fileInfo.FullName, content);
    }

    public void Load(string patientName)
    {
        string destination = GetDestination();
        if (!File.Exists(destination))
        {
            Debug.LogError("Houve um erro ao tentar carregar as configurações do perfil.");
            return;
        }

        string content = File.ReadAllText(destination);
        info = JsonConvert.DeserializeObject<Dictionary<Info, object>>(content);
    }
}
