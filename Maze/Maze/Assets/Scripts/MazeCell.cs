using System;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    //stores the coordinates of the current MazeCell
    public IntVector2 coordinates;

    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    private int initializedEdgeCount = 0;

    public MazeRoom room;

    public void Initialize(MazeRoom room)
    {
        room.Add(this);
        //Debug.Log("Setting Room Material to " + room.settings.floorMaterial.name);
        transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
    }

    public void OnPlayerEntered()
    {
        room.Show();
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i].OnPlayerEntered();
        }
    }

    public void OnPlayerExited()
    {
        room.Hide();
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i].OnPlayerExited();
        }
    }

    public bool isFullyInitialized {
        get
        {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    public MazeCellEdge GetEdge(MazeDirection direction)
    {
        return edges[(int)direction];
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount++;
    }

    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = UnityEngine.Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.Exception("Maze has no uninitialized edges!");
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
