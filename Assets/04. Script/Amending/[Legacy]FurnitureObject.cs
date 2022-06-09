// /* 가구들의 최상위 클래스, Amend scriptable object를 보유하고 있어 수리 가능하며
// 사용 가능한 기능들을 보유하고 있다.
// */

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public enum Legacy_FurnitureType
// {
//     Bed,
//     Car,
//     Radio,
//     Sofa,
// }

// public abstract class Legacy_FurnitureObject : ScriptableObject
// {
//     public FurnitureType type;
//     [TextArea(15,20)]
//     public string description;

//     public virtual void Awake()
//     {
//         //
//     }

//     // public void Amend()
//     // {
//     //     // 가구는 수리 가능
//     //     // amendObject.Amend();
//     // }

//     public virtual void Use()
//     {
//         // 가구는 사용 가능
//     }
// }