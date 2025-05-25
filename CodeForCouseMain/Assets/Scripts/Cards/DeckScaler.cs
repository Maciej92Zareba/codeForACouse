using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckScaler : MonoBehaviour
{
    [SerializeField] DeckManager deckManager;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] float oneCardThickness = 0.05f;

    [SerializeField] TMP_Text deckAmountText;
    [SerializeField] string onHoverTextString;

    public void ScaleDeck()
    {
        if (deckManager.drawPile.Count > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, deckManager.drawPile.Count * oneCardThickness, transform.localScale.z);
            
            if (meshRenderer.enabled == false)
            {
                meshRenderer.enabled = true;
            }
        }
        else 
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnMouseEnter()
    {
        deckAmountText.text = onHoverTextString + deckManager.drawPile.Count.ToString();
        deckAmountText.enabled = true;
    }

    private void OnMouseExit()
    {
        deckAmountText.enabled = false;
    }
}
