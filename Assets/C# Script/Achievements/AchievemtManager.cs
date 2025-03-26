using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievemtManager : MonoBehaviour
{
    int count = 0;

    private void Update()
    {
        Debug.Log(count);
    }
    public void Achieved()
    {
        count++;
    }


    
   
}
