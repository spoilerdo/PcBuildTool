namespace KillerApp.Domain
{
    public class AJAXError
    {
        public AJAXError(string result, string className)
        {
            Result = result;
            ClassName = className;
        }

        public string Result { get; set; }
        public string ClassName { get; set; }
    }
}