using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int points = 0;                       // Total points the player has
    public float overallIncrementRate = 0;       // Combined rate from all upgrades for points per second
    private float accumulatedPoints = 0f;        // Track fractional points for smooth updates

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Calculate the incremental points based on overallIncrementRate and deltaTime
        float increment = overallIncrementRate * Time.deltaTime;
        
        // Accumulate the fractional points
        accumulatedPoints += increment;

        // Convert accumulated points to integer for actual points increment
        int pointsToAdd = Mathf.FloorToInt(accumulatedPoints);
        if (pointsToAdd > 0)
        {
            points += pointsToAdd;
            accumulatedPoints -= pointsToAdd; // Remove the added integer part from accumulated points

            // Update the UI to display the new points total
            UIManager.Instance.UpdatePointCount(points);
        }

        // Update points per second display in UI
        UIManager.Instance.UpdatePointsPerSecond(overallIncrementRate);
    }

    // Method to add points manually, e.g., via clicks
    public void AddPoints(int amount)
    {
        points += amount;
        UIManager.Instance.UpdatePointCount(points);
    }

    // Method to increase overallIncrementRate by a given amount when upgrades are purchased
    public void AddToOverallIncrementRate(float amount)
    {
        overallIncrementRate += amount;
        UIManager.Instance.UpdatePointsPerSecond(overallIncrementRate);
    }
}