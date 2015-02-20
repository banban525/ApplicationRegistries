namespace ApplicationRegistries
{
    interface IEntry
    {
        EntryDefine Define { get; }
        string GetValue();
        bool ExistsValue();
        IEntry Repace(string @from, string to);
    }
}