using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace zbw.Auftragsverwaltung.Domain.Common
{
    public class FileContainer
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Stream Content { get; set; }

    }
}
