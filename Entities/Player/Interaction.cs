using Godot;
using System;

public class Interaction: RayCast
{
    [Signal]
    public delegate void InteractingObject(Node object_);

    public override void _Process(float delta)
    {
        Godot.Object collider = GetCollider();

        if (IsColliding() && collider is IInteractable interactable) 
        {
            GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("InteractionLabelVisibility", true, interactable.GetInteractText());
            if (Input.IsActionJustPressed("interact"))
            {
                EmitSignal("InteractingObject", collider);
            }
        }
        else
        {
            GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("InteractionLabelVisibility", false, null);
        }
    }
}