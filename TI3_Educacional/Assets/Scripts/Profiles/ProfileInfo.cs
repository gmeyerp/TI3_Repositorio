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
        intDautonismo,
        floatHeight,

        // Terapeuta
        stringEmail,

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
        floatGameDurationMinutes,
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
        { Info.intDautonismo, 0 },
        { Info.floatHeight, 1.00f },

        // Terapeuta
        { Info.stringEmail, null },

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
        { Info.intMaxAngle, 60 },
        { Info.floatGameDurationMinutes, 5f },
        { Info.boolCanUp, true },
        { Info.boolCanDown, true },
        { Info.boolCanRight, true },
        { Info.boolCanLeft, true },
        { Info.boolTutorialBar, true },

        // Gincana
        { Info.floatAnchorSpeed, 1f },
        { Info.floatCannonSpeed, 1f },
        { Info.floatBoatSpeed, 1f },
        { Info.floatMastSpeed, 1f },
        { Info.floatBarrelSpeed, 1f },
        { Info.floatJumpTime, 1f },
    };

    public ProfileInfo(string patientName)
    {
        info[Info.stringPatientName] = patientName;
        Save();
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
        {
            Directory.CreateDirectory(fileInfo.Directory.FullName);
            Debug.Log("Novo perfil criado.");
        }

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
        }
        else
        {
            string content = File.ReadAllText(destination);
            info = JsonConvert.DeserializeObject<Dictionary<Info, object>>(content);
        }
    }
}
