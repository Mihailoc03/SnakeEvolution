using UnityEngine;

public enum FoodType
{
    Normal,
    Bonus,
    SlowDown
}

public class Food : MonoBehaviour
{
    public FoodType foodType = FoodType.Normal;
}
