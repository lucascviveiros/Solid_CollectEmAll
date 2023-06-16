using System.Collections.Generic;
using UnityEngine;

public class BallSelectionManager
{
    private List<GameObject> selectedBalls;
    public GameObject SelectedBall { get; set; }
    public bool OneSelection { get; set; }
    public bool HasNotSameColor { get; set; }
    public bool BreakingLine { get; set; }
    public Color firstColor { get; set; }
    LineDrawer lineDrawer;
    private Dictionary<DotTypes, int> destroyedBallColors;

    public BallSelectionManager(LineDrawer lineDrawer)
    {
        selectedBalls = new List<GameObject>();
        destroyedBallColors = new Dictionary<DotTypes, int>();
        SelectedBall = null;
        OneSelection = false;
        this.lineDrawer = lineDrawer;
    }

    public void AddSelectedBall(GameObject ball)
    {
        selectedBalls.Add(ball);
        SelectedBall = ball;

        if (selectedBalls.Count > 1)
        {
            GameObject lastBall = selectedBalls[selectedBalls.Count - 1];
            GameObject penultimateBall = selectedBalls[selectedBalls.Count - 2];
            DrawLine(lastBall, penultimateBall);
        }
    }

    private void DrawLine(GameObject ball1, GameObject ball2)
    {
        Vector3 startPoint = ball1.transform.position;
        Vector3 endPoint = ball2.transform.position;
        if (!HasNotSameColor)
            lineDrawer.DrawLine(startPoint, endPoint);
    }

    public void ClearSelectedBalls()
    {
        selectedBalls.Clear();
        SelectedBall = null;
        OneSelection = false;
        BreakingLine = false;
        lineDrawer.ClearLines();
    }

    public List<GameObject> GetSelectedBalls()
    {
        return selectedBalls;
    }

    public bool HasOneSelection()
    {
        return OneSelection;
    }

    public bool BreakLine()
    {
        AudioPlayer.instance.PlaySound2();
        return BreakingLine;
    }

    public Dictionary<DotTypes, int> DestroySelectedBalls()
    {           
        destroyedBallColors.Clear();

        foreach (GameObject obj in selectedBalls)
        {
            // Get the color of the destroyed ball from the DotData component
            DotData dotData = obj.GetComponent<DotData>();
            if (dotData != null)
            {
                DotTypes ballColor = dotData.dotType;
                // Increment the quantity of destroyed balls for the corresponding color
                if (destroyedBallColors.ContainsKey(ballColor))
                {
                    destroyedBallColors[ballColor]++;
                    //Debug.Log("Incrementing quantity for Ball Color: " + ballColor);
                }
                else
                {
                    destroyedBallColors.Add(ballColor, 1);
                }
            }

            GameObject.Destroy(obj);
        }

        ClearSelectedBalls();
        AudioPlayer.instance.PlaySound1();
        DotController.instance.InstantiateNewDotsOnTop();
        return destroyedBallColors;
    }
}

  
