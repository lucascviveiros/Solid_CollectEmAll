using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IInputBeganHandler inputBeganHandler;
    private IInputMovedHandler inputMovedHandler;
    private IInputEndHandler inputEndHandler;
    private BallSelectionManager selectionManager;
    private LineDrawer lineDrawer;

    public GameManager()
    {
        inputBeganHandler = new InputBeganHandler();
        inputMovedHandler = new InputMovedHandler();
        inputEndHandler = new InputEndHandler();
    }

    private void Start()
    {
        lineDrawer = FindObjectOfType<LineDrawer>();
        selectionManager = new BallSelectionManager(lineDrawer);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && !DotController.instance.IsInstantiating)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    inputBeganHandler.HandleInputBegan(touch.position, selectionManager);
                    break;
                case TouchPhase.Moved:
                    inputMovedHandler.HandleInputMoved(touch.position, selectionManager);
                    break;
                case TouchPhase.Ended:
                    inputEndHandler.HandleInputEnd(selectionManager);
                    break;
                default:
                    return;
            }
        }
    }
}