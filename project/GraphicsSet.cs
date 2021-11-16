using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSet : MonoBehaviour
{
    public void SetQuiality(int quialityIndex)
    {
        QualitySettings.SetQualityLevel(quialityIndex);
    }
}
