using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/RecipeCompletedEvent")]
public class RecipeCompletedEvent : GameEvent<bool> { } // `true` for success, `false` for failure


