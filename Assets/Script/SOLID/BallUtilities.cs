using UnityEngine;

public static class BallUtilities
{
    public static Color GetBallColor(GameObject ball)
    {
        Transform sphere = ball.transform.Find("Sphere");
        return sphere.GetComponent<Renderer>().material.GetColor("_Color");
    }

    public static void EnableOutline(GameObject ball)
    {
        Outline outline = ball.GetComponent<Outline>();
        outline.enabled = true;
    }

    public static void EnableOutline(GameObject ball, Color outlineColor)
    {
        Outline outline = ball.GetComponent<Outline>();
        outline.enabled = true;
        outline.OutlineColor = outlineColor;
    }

    public static void DisableOutline(GameObject ball)
    {
        Outline outline = ball.GetComponent<Outline>();
        outline.enabled = false;
    }

    public static void DisableAllOutlines()
    {
        Outline[] outlines = Object.FindObjectsOfType<Outline>();
        foreach (Outline outline in outlines)
        {
            outline.enabled = false;
        }
    }
}
