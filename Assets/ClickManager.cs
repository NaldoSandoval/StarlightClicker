using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public void OnPointClicked()
    {
        GameManager.Instance.AddPoints(UpgradeManager.Instance.GetPointsPerClick());
    }
}
