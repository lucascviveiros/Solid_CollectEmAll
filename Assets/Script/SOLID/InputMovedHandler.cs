using UnityEngine;

public interface IInputMovedHandler
{
    void HandleInputMoved(Vector2 inputPosition, BallSelectionManager selectionManager);
}

public class InputMovedHandler : IInputMovedHandler
{
    public void HandleInputMoved(Vector2 inputPosition, BallSelectionManager selectionManager)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            //Preventing from going "around"
            if (objectHit.name == "Plane" || objectHit.name == "Block")
            {
                selectionManager.HasNotSameColor = true;
                selectionManager.BreakingLine = true;
                selectionManager.BreakLine();
            }
            else if (objectHit.CompareTag("Ball"))
            {
                selectionManager.OneSelection = false;
                Color colorComparable = BallUtilities.GetBallColor(objectHit);
                BallUtilities.EnableOutline(objectHit, Color.magenta);

                if (colorComparable != selectionManager.firstColor)
                {
                    selectionManager.HasNotSameColor = true;
                    selectionManager.BreakingLine = true;
                    selectionManager.BreakLine();
                }
                else if (colorComparable == selectionManager.firstColor)
                {
                    selectionManager.HasNotSameColor = false;
                    if (!selectionManager.GetSelectedBalls().Contains(objectHit))
                        selectionManager.AddSelectedBall(objectHit);

                }                 
            }
        }
    }
}

