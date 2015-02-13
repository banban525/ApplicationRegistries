namespace ApplicationRegistries.Generator
{
    class OutputObject
    {
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public Entries Entries { get; set; }
        public string InputXml { get; set; }
    }
}