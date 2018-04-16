using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkOutline : MonoBehaviour {
    [Header("Border")]
    public Vector3 borderSize;
    public Color borderColour;

    [Header("Lanes")]
    public Vector3 laneSize;
    public List<Vector3> lanePositions;
    public Color laneColour;
    public bool showLanes;

    [Header("Obstacles")]
    public Vector3 obstacleArea;
    public Color obstacleColour;
    public bool showObstacleArea;

    private void OnDrawGizmos() {
        Gizmos.color = borderColour;
        Gizmos.DrawWireCube(transform.position, borderSize);
    }

    private void OnDrawGizmosSelected() {
        if (showLanes) {
            Gizmos.color = laneColour;

            for (int i = 0; i < lanePositions.Count; i++) {
                Vector3 position = transform.position + lanePositions[i];
                Gizmos.DrawWireCube(position, laneSize);
            }
        }

        if (showObstacleArea) {
            Gizmos.color = obstacleColour;
            Gizmos.DrawWireCube(transform.position, obstacleArea);
        }
    }
}
