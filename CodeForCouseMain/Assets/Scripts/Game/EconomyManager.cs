using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [SerializeField] TMP_Text currencyText;
    public int currencyOwned = 10;

    private void Start()
    {
        currencyText.text = currencyOwned.ToString();
    }
    public void AddCurrency(int currency)
    {
        currencyOwned += currency;
        currencyText.text = currencyOwned.ToString();
    }
}
