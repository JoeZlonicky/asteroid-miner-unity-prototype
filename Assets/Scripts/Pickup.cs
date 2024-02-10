using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [field: SerializeField] public ItemData Data { get; private set; }
}
