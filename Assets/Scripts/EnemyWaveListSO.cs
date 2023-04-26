using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyWaveList")]
public class EnemyWaveListSO : ScriptableObject 
{
    public List<EnemyWaveSO> list;
}

