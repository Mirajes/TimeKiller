using System.Collections.Generic;
using UnityEngine;

public abstract class A_Stage : MonoBehaviour
{
    [SerializeField] protected List<A_Enemy> _Enemies = new();

    
}

public class Stage_LVL1 : A_Stage
{

}

public class Stage_LVL2 : A_Stage
{

}

public class Stage_BossLVL1 : A_Stage
{

}