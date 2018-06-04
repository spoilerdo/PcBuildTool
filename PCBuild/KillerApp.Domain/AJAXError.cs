namespace KillerApp.Domain
{
    public class AJAXError
    {
        public string Result { get; set; }
        public string ClassName { get; set; }

        public AJAXError(string result, string className)
        {
            Result = result;
            ClassName = className;
        }
    }
}