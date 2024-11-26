using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ProfileInfo;

public class ProfileManager : MonoBehaviour
{
    private static ProfileManager instance;

    public enum InfoGroup
    {
        patient,
        general,
        fair,
        bar,
    }

    private readonly Dictionary<InfoGroup, HashSet<Info>> infoGroups = new()
    {
        { InfoGroup.patient, new() {
            Info.stringPatientName,
            Info.intAge,
            Info.intDautonismo,
            Info.floatHeight,
        } },

        { InfoGroup.general, new() {
            Info.floatGeneralVolume,
            Info.floatBgmVolume,
            Info.floatSfxVolume,
            Info.floatVrSensibility,
        } },

        { InfoGroup.fair, new() {
            Info.intFruitAmount,
            Info.intVisitorAmount,
            Info.floatVisitorSpeed,
            Info.floatCoinSize,
            Info.boolFruitMemorize,
            Info.boolTutorialFeira,
        } },

        { InfoGroup.bar, new() {
            Info.intMaxAngle,
            Info.floatGameDuration,
            Info.boolCanUp,
            Info.boolCanDown,
            Info.boolCanRight,
            Info.boolCanLeft,
            Info.boolTutorialBar,
        } },
    };

    private ProfileInfo currentProfile;
    private ProfileInfo savedProfile;

    private readonly Dictionary<Info, UnityAction<object>> listeners = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            savedProfile = new ProfileInfo("Paciente Anônimo");
            currentProfile = new ProfileInfo("Paciente Anônimo");
        }
        else { Destroy(gameObject); }
    }

    static public void AddListener(Info info, UnityAction<object> listener) => instance?.Instance_AddListener(info, listener);
    private void Instance_AddListener(Info info, UnityAction<object> listener)
    {
        if (!listeners.ContainsKey(info))
        { listeners[info] = listener; }
        else
        { listeners[info] += listener; }

        listener.Invoke(currentProfile.Get(info));
    }

    static public object GetCurrent(Info info) => instance?.Instance_GetCurrent(info);
    private object Instance_GetCurrent(Info info)
    {
        return currentProfile.Get(info);
    }
    static public void SetCurrent(Info info, object value) => instance?.Instance_SetCurrent(info, value);
    private void Instance_SetCurrent(Info info, object value)
    {
        currentProfile.Set(info, value);
    }

    static public void UndoGroup(InfoGroup infoGroup) => instance?.Instance_UndoGroup(infoGroup);
    private void Instance_UndoGroup(InfoGroup infoGroup)
    {
        if (infoGroup != InfoGroup.patient)
        { UndoGroup(InfoGroup.patient); }

        foreach (Info info in infoGroups[infoGroup])
        {
            object value = savedProfile.Get(info);
            currentProfile.Set(info, value);

            if (listeners.ContainsKey(info))
            { listeners[info].Invoke(value); }
        }
    }
    static public void SaveGroup(InfoGroup infoGroup) => instance?.Instance_SaveGroup(infoGroup);
    private void Instance_SaveGroup(InfoGroup infoGroup)
    {
        if (infoGroup != InfoGroup.patient)
        { SaveGroup(InfoGroup.patient); }

        foreach (Info info in infoGroups[infoGroup])
        { savedProfile.Set(info, currentProfile.Get(info)); }

        savedProfile.Save();
    }

    static public void ChangePatient(string name) => instance.Instance_ChangePatient(name);
    private void Instance_ChangePatient(string name)
    {
        savedProfile.Load(name);
        foreach (KeyValuePair<InfoGroup, HashSet<Info>> infoGroup in infoGroups)
        { UndoGroup(infoGroup.Key); }
    }
}
