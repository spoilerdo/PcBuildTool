using System;
using System.Collections.Generic;
using System.Text;

namespace KillerApp.Domain
{
    public class File
    {
        public string ID { get; set; }
        public string _Name { get; set; }
        public string ContentType { get; set; }
        public string _Content { get; set; }
        public FileType _Type { get; set; }

        public File(string name, string contentType, FileType type)
        {
            _Name = name;
            ContentType = contentType;
            _Type = type;
        }

        public enum FileType
        {
            PcImage = 1
        }
    }
}
