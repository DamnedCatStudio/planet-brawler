using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    public List<Stage> stages = new List<Stage>();

    [Header("Public References")]
    public GameObject stagePointPrefab;
    public Transform planetTransform;
    private void Start() {
        foreach(Stage stage in stages)
        {
            SpawnStagePoint(stage);
        }   
    }

    void SpawnStagePoint(Stage stage)
    {
        GameObject level = Instantiate(stagePointPrefab, planetTransform);
        level.transform.localEulerAngles = new Vector3(stage.x, stage.y, 8);
    }
}

[System.Serializable]
public class Stage
{
    public string name;
    [Range(-180,180)]
    public float x;
    [Range (-89,89)]
    public float y;
}
