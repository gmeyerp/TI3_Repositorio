using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorial/New Tutorial", order = 0)]
public class TutorialSO : ScriptableObject
{
    public float duration = -1;
    [TextArea(5, 10)] public string text;
    public Sprite image;
    public TutorialSO nextTutorial;

    public void ShowText(TextMeshProUGUI textMesh)
    {
        Debug.Log(textMesh);
        textMesh.gameObject.SetActive(true);
        textMesh.text = text;        
    }

    public IEnumerator HideText(TextMeshProUGUI textMesh)
    {
        if (duration > 0)
        {
            yield return new WaitForSeconds(duration);
            textMesh.gameObject.SetActive(false);
        }
    }

    public IEnumerator NextTutorial(TextMeshProUGUI textMesh)
    {
        if (duration > 0 && nextTutorial != null)
        {
            Debug.Log("corotina comeca");
            yield return new WaitForSeconds(duration);
            nextTutorial.ShowText(textMesh);
        }
        else Debug.Log("Duracao incorreta");
    }
}
