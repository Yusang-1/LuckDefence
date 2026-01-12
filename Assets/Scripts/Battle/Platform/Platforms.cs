using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private Platform[] platformList;

    public Platform[] PlatformList => platformList;
}
