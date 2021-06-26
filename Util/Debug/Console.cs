using Godot;
using System;
using System.Collections.Generic;

public class Console : Control
{
    [Export]
    private NodePath EntityToDebug;

    private IDebuggable EntityDebuggable;
    private RichTextLabel Monitor;
    
    public override void _Ready()
    {
        Monitor = GetNode<RichTextLabel>("Monitor");
        EntityDebuggable = GetNode<IDebuggable>(EntityToDebug);
    }

    public override void _Process(float delta)
    {
        Monitor.Text = "";
        Dictionary<string, object> debugData = EntityDebuggable.GetDebugData();
        foreach(KeyValuePair<string, object> entry in debugData)
        {
            Monitor.Text += $"{entry.Key}: {entry.Value.ToString()}\n";
        }
    }
}
