namespace NancyDemo.Lib.Domain
{
    public class Secret
    {
        public string Value { get; set; }

        public Secret()
        {
        }

        public Secret(string value)
        {
            Value = value;
        }
    }
}