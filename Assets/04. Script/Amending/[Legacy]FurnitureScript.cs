// //  가구 오브젝트에 부착, type을 지정하면 type에 맞는 Furniture 오브젝트를 자동으로 생성하여 사용

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Legacy_FurnitureScript : MonoBehaviour
// {
//     public FurnitureType furnitureType;
//     [HideInInspector]
//     public FurnitureObject furnitureObject;
//     // Start is called before the first frame update
//     void Awake()
//     {
//         switch (furnitureType)
//         {
//             case FurnitureType.Bed:
//             {
//                 furnitureObject = ScriptableObject.CreateInstance<BedObject>();
//                 break;
//             }
//             // case FurnitureType.Car:
//             // {
//             //     furnitureObject = ScriptableObject.CreateInstance<CarObject>();
//             //     break;
//             // }
//             // case FurnitureType.Radio:
//             // {
//             //     furnitureObject = ScriptableObject.CreateInstance<RadioObject>();
//             //     break;
//             // }
//             // case FurnitureType.Sofa:
//             // {
//             //     furnitureObject = ScriptableObject.CreateInstance<SofaObject>();
//             //     break;
//             // }
//         }
//     }

//     void Start()
//     {
        
//     }

//     private void OnTriggerEnter()
//     {
//         Destroy(furnitureObject);
//     }
// }
