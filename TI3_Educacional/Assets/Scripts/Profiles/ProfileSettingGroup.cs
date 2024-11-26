using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSettingGroup : MonoBehaviour
{
    [SerializeField] private ProfileManager.InfoGroup group;

    public void Save() => ProfileManager.SaveGroup(group);
    public void Undo() => ProfileManager.UndoGroup(group);
}
