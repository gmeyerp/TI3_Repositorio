using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinCollect : MonoBehaviour
{
    [SerializeField] private Color inactiveColor; // Refer�ncia a cor quando jogador n�o estiver olhando
    [SerializeField] private Color activeColor; // Refer�ncia a cor quando jogador estiver olhando

    private MeshRenderer meshRenderer;
    private Collider coinCollider;
    [SerializeField] GameObject objectCanvas;
    [SerializeField] Image completion;

    private void Start() 
    {
        //Ativando o MeshRenderer e o Collider da moeda
        meshRenderer = GetComponent<MeshRenderer>(); 
        coinCollider = GetComponent<Collider>();

        meshRenderer.material.color = inactiveColor; //Atribui ao material a cor inativa
    }

    public void ChangeColorOnLook(bool isLooking)
    {
        float lerpSpeed = Time.deltaTime / PlayerRayCast.instance.timeToCollect;

        if (isLooking)
        {
            //Quando estiver olhando, alterar� a cor atual at� a cor ativa com o metodo Color.Lerp
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, activeColor, lerpSpeed);
            completion.fillAmount += (Time.deltaTime / PlayerRayCast.instance.timeToCollect);
            completion.color = meshRenderer.material.color;
        }
        else
        {
            if (meshRenderer.material.color != inactiveColor) // Se a cor atual for diferente da cor inativa
            {
                //Quando n�o estiver olhando, alterar� a cor atual at� a cor inativa com o metodo Color.Lerp
                meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, inactiveColor, lerpSpeed);
                completion.fillAmount = 0;
            }
            else
            {
                meshRenderer.material.color = inactiveColor;
                completion.fillAmount = 0;
                completion.color = inactiveColor;
            }
        }
    }

    public void Collect()
    {
        Debug.Log("Moeda coletada!");
        MiniGameManager.instance.coinsAdquired++;
        MiniGameManager.instance.GetoutMiniGame();
        Destroy(gameObject);
    }
}
