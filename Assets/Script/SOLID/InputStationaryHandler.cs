
using UnityEngine;

public interface IInputStationaryHandler
{
    void HandleInputStationary(Vector2 inputPosition, BallSelectionManager selectionManager);
}

public class InputStationaryHandler : IInputStationaryHandler
{
    public void HandleInputStationary(Vector2 inputPosition, BallSelectionManager selectionManager)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;

            if (objectHit.CompareTag("Board") || objectHit.name == "Plane")
            {
                selectionManager.HasNotSameColor = true;
                selectionManager.BreakingLine = true;
            }
            else if (objectHit.CompareTag("Ball"))
            {
                selectionManager.OneSelection = false;
                Color colorComparable = BallUtilities.GetBallColor(objectHit);
                BallUtilities.EnableOutline(objectHit, Color.magenta);

                if (colorComparable == selectionManager.firstColor)
                {
                    selectionManager.HasNotSameColor = false;
                    if (!selectionManager.GetSelectedBalls().Contains(objectHit))
                        selectionManager.AddSelectedBall(objectHit);

                }
                else if (colorComparable != selectionManager.firstColor)
                {
                    selectionManager.HasNotSameColor = true;
                    selectionManager.BreakingLine = true;
                }
            }
            //preventing from going "around"
            /*else if (objectHit.name == "Plane")
            {
                //Debug.Log("Obj: " + objectHit.name);
                selectionManager.HasNotSameColor = true;
                selectionManager.BreakingLine = true;
                selectionManager.BreakLine();
            }*/
            else if (objectHit.name == "LimitBoard")
            {
          
            }
        }
    }
}
