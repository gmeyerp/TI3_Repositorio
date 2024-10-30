using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ProfileInfo;

public class ProfileManager : MonoBehaviour
{
    //static private ProfileManager instance;

    public enum InfoGroup
    {
        general,
        fair,
        bar,
    }

    private readonly Dictionary<InfoGroup, HashSet<Info>> infoGroups = new()
    {
        { InfoGroup.general, new() {
            Info.stringPatientName,
            Info.intAge,
            Info.intDautonismo,
            Info.floatHeight,
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

    /*
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    */

    private readonly Dictionary<Info, UnityAction<object>> listeners = new();

    private void Awake()
    {
        savedProfile = new ProfileInfo("Saved");
        currentProfile = new ProfileInfo("Teste");
    }

    public void AddListener(Info info, UnityAction<object> listener)
    {
        if (!listeners.ContainsKey(info))
        { listeners[info] = listener; }
        else
        { listeners[info] += listener; }

        listener.Invoke(currentProfile.Get(info));
    }

    public void SetCurrent(Info info, object value)
    {
        currentProfile.Set(info, value);
    }

    public void UndoGroup(InfoGroup infoGroup)
    {
        foreach (Info info in infoGroups[infoGroup])
        {
            object value = savedProfile.Get(info);
            currentProfile.Set(info, value);

            if (listeners.ContainsKey(info))
            { listeners[info].Invoke(value); }
        }
    }
    public void SaveGroup(InfoGroup infoGroup)
    {
        foreach (Info info in infoGroups[infoGroup])
        { savedProfile.Set(info, currentProfile.Get(info)); }

        savedProfile.Save();
    }

    public void UndoGeneral() => UndoGroup(InfoGroup.general);
    public void UndoFair() => UndoGroup(InfoGroup.fair);
    public void UndoBar() => UndoGroup(InfoGroup.bar);

    public void SaveGeneral() => SaveGroup(InfoGroup.general);
    public void SaveFair() => SaveGroup(InfoGroup.fair);
    public void SaveBar() => SaveGroup(InfoGroup.bar);

}
