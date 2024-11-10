using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    public List<Upgrade> upgrades;
    public GameObject upgradeTemplatePrefab;
    public Transform upgradeContainer;
    public int pointsPerClick = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GenerateUpgradeUI();
    }

    public bool AttemptPurchase(Upgrade upgrade)
    {
        if (upgrade.Purchase())
        {
            ApplyUpgrade(upgrade);
            UIManager.Instance.UpdatePointCount(GameManager.Instance.points);
            return true;
        }
        return false;
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
        if (upgrade.isPurchased)
        {
            GameManager.Instance.AddToOverallIncrementRate(upgrade.incrementRate); // Add incrementRate instead
        }
    }


    public int GetPointsPerClick()
    {
        return pointsPerClick;
    }

    private void GenerateUpgradeUI()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            Debug.Log("Instantiating upgrade: " + upgrade.upgradeName);

            GameObject newUpgradeUI = Instantiate(upgradeTemplatePrefab, upgradeContainer);
            UpgradeUI upgradeUI = newUpgradeUI.GetComponent<UpgradeUI>();

            if (upgradeUI == null)
            {
                Debug.LogError("UpgradeUI component is missing on upgrade template prefab.");
                continue;
            }

            upgradeUI.Initialize(upgrade);
        }
    }

}
