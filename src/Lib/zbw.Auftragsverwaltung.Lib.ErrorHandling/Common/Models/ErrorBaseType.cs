using System;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models
{
    public class ErrorBaseType
    {
        public ErrorBaseType(int id, string name, ErrorBaseType linkedError)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            Id = id;
            Name = name;
            LinkedError = linkedError;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ErrorBaseType LinkedError { get; set; }

        public ErrorBaseType GetBaseError()
        {
            var baseError = this;
            while (HasLinkedErrors())
            {
                baseError = baseError.LinkedError;
            }

            return baseError;
        }

        public bool HasLinkedErrors() => LinkedError != null;
    }
}
