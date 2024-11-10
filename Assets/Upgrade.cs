using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public string upgradeName;            // Name of the upgrade
    public int cost;                      // Initial cost of the upgrade
    public float costMultiplier = 1.25f;  // Unique multiplier to increase cost per purchase
    public float incrementRate;           // Points per second added by this upgrade
    public int purchaseCount = 0;         // Track the number of purchases
    public bool isPurchased = false;      // Track if the upgrade has been purchased at least once

    // Purchase method to handle buying the upgrade
    public bool Purchase()
    {
        // Check if the player has enough points to purchase the upgrade
        if (GameManager.Instance.points >= cost)
        {
            // Deduct the cost from the player's points
            GameManager.Instance.points -= cost;

            // Increase the game's points per second by this upgrade's increment rate
            GameManager.Instance.AddToOverallIncrementRate(incrementRate);

            // Increment the purchase count and mark the upgrade as purchased
            purchaseCount++;
            isPurchased = true;

            // Increase the cost for the next purchase using the unique costMultiplier
            cost = Mathf.CeilToInt(cost * costMultiplier);

            return true; // Indicate that the purchase was successful
        }
        else
        {
            // Debug message if the player does not have enough points
            Debug.Log("Not enough points to purchase this upgrade.");
            return false; // Indicate that the purchase failed
        }
    }
}
