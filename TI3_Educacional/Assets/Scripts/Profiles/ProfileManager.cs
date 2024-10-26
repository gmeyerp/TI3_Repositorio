using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    ProfileInfo currentProfile;
    ProfileInfo lastProfile;

    private void Start()
    {
        currentProfile = new ProfileInfo("Teste");
    }
    public void ReverterGeral()
    {
        currentProfile.age = lastProfile.age;
        currentProfile.height = lastProfile.height;
        currentProfile.dautonismo = lastProfile.dautonismo;
        currentProfile.generalVolume = lastProfile.generalVolume;
        currentProfile.bgmVolume = lastProfile.bgmVolume;
        currentProfile.sfxVolume = lastProfile.sfxVolume;
        currentProfile.vrSensibility = lastProfile.vrSensibility;
    }

    public void SetPatientName (string value) {currentProfile.patientName = value;}
    public void SetAge (int value) {currentProfile.age = value;}
    public void SetHeight (float value) {currentProfile.height = value;}
    public void SetDautonismo (int value) {currentProfile.dautonismo = value;}
    public void SetGeneralVolume (float value) {currentProfile.generalVolume = value;}
    public void SetBgmVolume (float value) {currentProfile.bgmVolume = value;}
    public void SetSfxVolume (float value) {currentProfile.sfxVolume = value;}
    public void SetVrSensibility (float value) {currentProfile.vrSensibility = value;}
    public void SetFruitAmount (int value) {currentProfile.fruitAmount = value;}
    public void SetFruitMemorize (bool value) {currentProfile.fruitMemorize = value;}
    public void SetVisitorAmount (int value) {currentProfile.visitorAmount = value;}
    public void SetVisitorSpeed (float value) {currentProfile.visitorSpeed = value;}
    public void SetCoinSize (float value) {currentProfile.coinSize = value;}
    public void SetTutorialFeira (bool value) {currentProfile.tutorialFeira = value;}
    public void SetGameDuration (float value) {currentProfile.gameDuration = value;}
    public void SetMaxAngle (int value) {currentProfile.maxAngle = value;}
    public void SetCanUp (bool value) {currentProfile.canUp = value;}
    public void SetCanDown (bool value) {currentProfile.canDown = value;}
    public void SetCanRight (bool value) {currentProfile.canRight = value;}
    public void SetCanLeft (bool value) {currentProfile.canLeft = value;}
    public void SetTutorialBar(bool value)  {currentProfile.tutorialBar = value;}




}
