namespace ApplicationRegistries
{
    interface IEntry
    {
        EntryDefine Define { get; }
        string GetValue();
        bool ExistsValue();
    }
}