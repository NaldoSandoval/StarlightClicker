using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI pointsText;          // Text for displaying the points amount
    public TextMeshProUGUI pointsFormatText;    // Text for displaying the format (Units, Kilounits, etc.)
    public TextMeshProUGUI pointsPerSecondText; // Text for displaying points per second

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update points with consistent format
    public void UpdatePointCount(int count)
    {
        (string formattedAmount, string formatLabel) = FormatAmount(count);
        pointsText.text = formattedAmount;
        pointsFormatText.text = formatLabel;
    }

    // Update points per second with consistent format
    public void UpdatePointsPerSecond(float pointsPerSecond)
    {
        // Ensure pointsPerSecond is always shown with one decimal place
        (string formattedAmount, string formatLabel) = FormatPointsPerSecond(pointsPerSecond);
        pointsPerSecondText.text = $"{formattedAmount} {formatLabel} per Second";
    }

    // Method specifically for formatting points per second with one decimal place
    private (string, string) FormatPointsPerSecond(float amount)
    {
        if (amount == 0)
            return ("0.0", "Units");  // Display "0.0 Units per Second" initially

        if (amount >= 1_000_000_000_000_000_000f)
            return ((amount / 1_000_000_000_000_000_000f).ToString("F3"), "Exunits");
        if (amount >= 1_000_000_000_000_000f)
            return ((amount / 1_000_000_000_000_000f).ToString("F3"), "Petunits");
        if (amount >= 1_000_000_000_000f)
            return ((amount / 1_000_000_000_000f).ToString("F3"), "Terunits");
        if (amount >= 1_000_000_000f)
            return ((amount / 1_000_000_000f).ToString("F3"), "Gigunits");
        if (amount >= 1_000_000f)
            return ((amount / 1_000_000f).ToString("F3"), "Megaunits");
        if (amount >= 1_000f)
            return ((amount / 1_000f).ToString("F3"), "Kilounits");

        return (amount.ToString("F1"), "Units"); // Display with one decimal place for smaller values
    }

    // Centralized formatting method for any amount in the game
    public (string, string) FormatAmount(float amount)
    {
        if (amount == 0)
            return ("0", "Units");  // Ensures "0 Units per Second" initially

        if (amount >= 1_000_000_000_000_000_000f)
            return ((amount / 1_000_000_000_000_000_000f).ToString("F3"), "Exunits");
        if (amount >= 1_000_000_000_000_000f)
            return ((amount / 1_000_000_000_000_000f).ToString("F3"), "Petunits");
        if (amount >= 1_000_000_000_000f)
            return ((amount / 1_000_000_000_000f).ToString("F3"), "Terunits");
        if (amount >= 1_000_000_000f)
            return ((amount / 1_000_000_000f).ToString("F3"), "Gigunits");
        if (amount >= 1_000_000f)
            return ((amount / 1_000_000f).ToString("F3"), "Megaunits");
        if (amount >= 1_000f)
            return ((amount / 1_000f).ToString("F3"), "Kilounits");

        // For values below 1,000, display as whole numbers
        return (Mathf.FloorToInt(amount).ToString("N0"), "Units");
    }

}
