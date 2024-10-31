using System;

public class UpgradeEventArgs : EventArgs
{
    public ItemDisplay Item { get; }

    public UpgradeEventArgs(ItemDisplay item)
    {
        Item = item;
    }
}
