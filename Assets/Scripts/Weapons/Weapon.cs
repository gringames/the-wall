using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void Shoot(Vector2 direction);
    }
}