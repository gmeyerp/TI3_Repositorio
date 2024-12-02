using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSettingGroup : MonoBehaviour
{
    [SerializeField] private ProfileManager.InfoGroup group;

    public void Save()
    {
        Gerenciador_Audio.TocarSFX(Gerenciador_Audio.SFX.buttonClick);
        ProfileManager.SaveGroup(group);
    }

    public void Undo()
    {
        Gerenciador_Audio.TocarSFX(Gerenciador_Audio.SFX.buttonClick);
        ProfileManager.UndoGroup(group);
    }
}
