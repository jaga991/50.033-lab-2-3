using System;
using UnityEngine;

public class TrashCounter : BaseCounter 
{

    public static event EventHandler OnAnyObjectTrashed;
    public override void Interact(Player player)
    {
        if(player.HasFoodObject())
        {
            EventManager.Instance.TriggerEvent("ObjectTrashed", this);
            player.GetFoodObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
