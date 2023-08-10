using System.ComponentModel.DataAnnotations;

namespace Features.Core
{
    public class Entity
    {
        public Guid Id { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }
    }
}
