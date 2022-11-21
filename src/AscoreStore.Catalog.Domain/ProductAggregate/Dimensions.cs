using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Catalog.Domain.ProductAggregate
{
    public class Dimensions : ValueObject
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimensions(decimal height, decimal width, decimal depth)
        {
            Validations.ValidateLessThan(height, 1, "O campo Altura não pode ser menor ou igual a 0");
            Validations.ValidateLessThan(width, 1, "O campo Largura não pode ser menor ou igual a 0");
            Validations.ValidateLessThan(depth, 1, "O campo Profundidade não pode ser menor ou igual a 0");

            Height = height;
            Width = width;
            Depth = depth;
        }

        public string DescriptionFormatted()
        {
            return $"HxWxP: {Height} x {Width} x {Depth}";
        }

        public override string ToString()
        {
            return DescriptionFormatted();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Height;
            yield return Width;
            yield return Depth;
        }
    }
}