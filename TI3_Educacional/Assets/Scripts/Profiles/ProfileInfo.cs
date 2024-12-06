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
        // Paciente
        stringPatientName,
        intAge,
        intDautonismo,
        floatHeight,

        // Geral
        intGeneralVolume,
        intVoiceVolume,
        intSfxVolume,
        floatVrSensibility,

        // Feira
        intFruitAmount,
        intVisitorAmount,
        floatVisitorSpeed,
        floatFruitSize,
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

        // Gincana
        floatAnchorSpeed,
        floatCannonSpeed,
        floatBoatSpeed,
        floatMastSpeed,
        floatBarrelSpeed,
        floatJumpTime,
    }

    private Dictionary<Info, object> info = new()
    {
        // Paciente
        { Info.stringPatientName, "name" },
        { Info.intAge, 99 },
        { Info.intDautonismo, 0 },
        { Info.floatHeight, 1.00f },

        // Geral
        { Info.intGeneralVolume, 50 },
        { Info.intVoiceVolume, 100 },
        { Info.intSfxVolume, 100 },
        { Info.floatVrSensibility, 1f },

        // Feira
        { Info.intFruitAmount, 3 },
        { Info.intVisitorAmount, 2 },
        { Info.floatVisitorSpeed, 4f },
        { Info.floatFruitSize, 2f },
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
        Load(patientName);
    }

    private string GetDestination() => $"{Application.persistentDataPath}/Perfis/{info[Info.stringPatientName]}.json";

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
        info[Info.stringPatientName] = patientName;

        string destination = GetDestination();
        if (!File.Exists(destination))
        {
            Save();
            Debug.Log("Novo perfil criado.");
        }
        else
        {
            string content = File.ReadAllText(destination);
            info = JsonConvert.DeserializeObject<Dictionary<Info, object>>(content);
        }
    }
}
