using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyWave")]
public class EnemyWaveSO : ScriptableObject 
{  
    public int enemyMax;
    public Enemy enemy;
    
}
