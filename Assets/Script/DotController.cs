using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : Singleton<DotController>
{
    public GameObject dotPrefab; // Prefab representing the dot sphere
    public ScriptableDots[] dotConfigurations; // Array of ScriptableDots configurations (e.g., red and blue)
    public bool IsInstantiating { get; set; } = true;
    const int gridSize = 6; // Size of the grid
    const float spacing = .63f; // Spacing between each dot
    Outline outline;
    private Dictionary<GameObject, ScriptableDots> instantiatedSpheres;
    private List<GameObject> remainingSpheres = new List<GameObject>();
    float yPos = 0.9f; //fixed Y height in the board
    private void Start() => InstantiateDots();

    public void InstantiateDots()
    {
        instantiatedSpheres = new Dictionary<GameObject, ScriptableDots>();
        // Shuffle the dot configurations to get random order
        ShuffleArray(dotConfigurations);
        int dotCount = 0;

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                GameObject dot = Instantiate(dotPrefab, GetPosition(x, z), Quaternion.identity);
                Transform sphere = dot.transform.Find("Sphere");
                ScriptableDots dotConfiguration = dotConfigurations[Random.Range(0, dotConfigurations.Length)];
                Renderer dotRenderer = sphere.GetComponent<Renderer>();
                dotRenderer.material.color = dotConfiguration.materialColor;
                outline = dotPrefab.GetComponent<Outline>();
                outline.enabled = false;
                dotCount++;
                instantiatedSpheres.Add(dot, dotConfiguration); // Add the instantiated sphere and its configuration to the dictionary
                // Attach the DotData information to the dot object
                DotData dotData = dot.AddComponent<DotData>();
                dotData.dotType = dotConfiguration.dotType;
                dotData.materialColor = dotConfiguration.materialColor;
            }
        }

        IsInstantiating = false;
    }

    public int GetBallCountByColor(DotTypes dotType)
    {
        int count = 0;

        foreach (KeyValuePair<GameObject, ScriptableDots> kvp in instantiatedSpheres)
        {
            ScriptableDots dotConfiguration = kvp.Value;

            if (dotConfiguration != null && dotConfiguration.dotType == dotType)
            {
                count++;
            }
        }

        return count;
    }

    public void ShuffleButtonDebug()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject obj in list)
            Destroy(obj);

        InstantiateDots();
    }

    private Vector3 GetPosition(int x, int z)
    {
        float offsetX = (gridSize - 1) * spacing * 0.5f;
        float offsetZ = (gridSize - 1) * spacing * 0.5f;

        float xPos = x * spacing - offsetX;
        float zPos = z * spacing - offsetZ;
        return new Vector3(xPos, yPos, zPos);
    }

    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n);
            n--;
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public void FindRemainingSpheres()
    {
        remainingSpheres.Clear();

        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject sphere in spheres)
        {
            remainingSpheres.Add(sphere);
        }
    }

    public void InstantiateNewDotsOnTop()
    {
        FindRemainingSpheres();
        float zOffset = 1.0f; // Adjust this value based on the desired spacing between the new dots
        Dictionary<float, float> highestPositions = new Dictionary<float, float>();

        foreach (GameObject sphere in remainingSpheres)
        {
            Vector3 position = sphere.transform.position;
            float column = position.x; // Assuming the dots are aligned with integer X positions

            if (!highestPositions.ContainsKey(column) || position.z > highestPositions[column])
            {
                highestPositions[column] = position.z;
            }
        }

        // Instantiating new line and destroying dots above destroyThreshold
        foreach (KeyValuePair<float, float> kvp in highestPositions)
        {
            float column = kvp.Key;
            float highestPosition = kvp.Value;

            int amountToInstantiate = 9;

            for (int i = 1; i <= amountToInstantiate; i++)
            {
                float newPositionZ = highestPosition + i * zOffset;
                Vector3 newPosition = new Vector3(column, yPos, newPositionZ);
                GameObject newDot = Instantiate(dotPrefab, newPosition, Quaternion.identity);
                ScriptableDots dotConfiguration = dotConfigurations[Random.Range(0, dotConfigurations.Length)];
                Renderer dotRenderer = newDot.transform.Find("Sphere").GetComponent<Renderer>();
                dotRenderer.material.color = dotConfiguration.materialColor;
                dotRenderer.enabled = false;
            }
        }

        IsInstantiating = true;
        StartCoroutine(DestroyDotWithDelay());
    }

    private IEnumerator DestroyDotWithDelay()
    {
        yield return new WaitForSecondsRealtime(3f);
        //Debug.Log("Destroy delayed");

        float destroyThreshold = -0.361f;

        // Find dots above the destroyThreshold position and destroy them
        Collider[] colliders = Physics.OverlapSphere(Vector3.zero, 10f); // Replace Vector3.zero with the desired center position
        foreach (Collider collider in colliders)
        {
            GameObject dot = collider.gameObject;
            if (dot.CompareTag("Ball"))
            {
                float dotPositionZ = dot.transform.position.z;
                if (dotPositionZ > destroyThreshold)
                {
                    Destroy(dot);
                }
                else
                {
                    Renderer dotRenderer = dot.transform.Find("Sphere").GetComponent<Renderer>();
                    dotRenderer.enabled = true;
                }
            }
        }

        IsInstantiating = false;
    }
}


