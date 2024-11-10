using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI purchaseCountText; // New text for displaying the purchase count
    public Button purchaseButton;
    private Upgrade upgrade;

    public void Initialize(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        upgradeNameText.text = upgrade.upgradeName;
        costText.text = "Cost: " + FormatCost(upgrade.cost); // Use centralized formatting
        purchaseButton.onClick.AddListener(OnPurchaseClicked);
        UpdateUI(); // Initial check
    }

    private void Update()
    {
        UpdateUI();
    }

    private void OnPurchaseClicked()
    {
        if (UpgradeManager.Instance.AttemptPurchase(upgrade))
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        // Enable/disable button and set color based on player's points
        purchaseButton.interactable = GameManager.Instance.points >= upgrade.cost;
        purchaseButton.image.color = purchaseButton.interactable ? Color.white : Color.gray;

        // Consistently formatted cost and purchase count
        costText.text = "Cost: " + FormatCost(upgrade.cost);
        purchaseCountText.text = upgrade.purchaseCount.ToString();
    }

    // Format cost using the centralized method from UIManager
    private string FormatCost(float cost)
    {
        (string formattedAmount, string formatLabel) = UIManager.Instance.FormatAmount(cost);
        return $"{formattedAmount} {formatLabel}";
    }
}
