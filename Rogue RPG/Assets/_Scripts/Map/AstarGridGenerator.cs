using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AstarGridGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateGrid()
    {
        //AstarPath.active.data
        ///*
        AstarData data = AstarPath.active.data;
        GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;
        int width = 500;
        int depth = 500;
        float nodeSize = 1f;
        gg.SetDimensions(width, depth, nodeSize);
        AstarPath.active.Scan();
        print("Generate Grid");
        //*/        
    }
}
