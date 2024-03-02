using FluentValidation.Results;

namespace Bitinvest.Domain.Entities
{
    public abstract class Entity
    {

        public Guid Id { get; set; }
        public ValidationResult ValidationResult { get; protected set; }
        public bool Ativo { get; private set; }
        protected Entity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            ValidationResult = new ValidationResult();
            Ativo = true;
        }
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            return compareTo is not null && Id.Equals(compareTo.Id);
        }
        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;

            if (a is null || b is null) return false;

            return a.Equals(b);
        }
        public static bool operator !=(Entity a, Entity b) => !(a == b);
        public override int GetHashCode() => (GetType().GetHashCode() * 2021) + Id.GetHashCode();
        public override string ToString() => $"{GetType().Name} [Id={Id}]";
        public void AdicionarErro(string message) => ValidationResult.Errors.Add(new ValidationFailure("", message));
        public void AlterarStatus() => Ativo = !Ativo;
        public abstract ValidationResult Validar();
        public DateTime? CriadoEm { get; set; }
        public Guid? CriadoPorId { get; set; }
        public DateTime? AlteradoEm { get; set; }
        public Guid? AlteradoPorId { get; set; }
    }
}
