using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Catalog.Domain.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActivated { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public int StockQuantity { get; private set; }
        public Dimensions Dimensions { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid ImageId { get; private set; }


        public Category Category { get; private set; }
        public Image Image { get; private set; }

        protected Product() { }

        public Product(string name, string description, bool isActivated, decimal value, DateTime createdDate, Dimensions dimensions, Guid categoryId, Image image)
        {
            Name = name;
            Description = description;
            IsActivated = isActivated;
            Value = value;
            CreatedDate = createdDate;
            Dimensions = dimensions;
            CategoryId = categoryId;
            ImageId = image.Id;
            Image = image;

            Validate();
        }

        public void Enable() => IsActivated = true;

        public void Disable() => IsActivated = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            Validations.ValidateForEmpty(description, "O campo Descricao do produto não pode estar vazio");
            Description = description;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity < 0)
                quantity *= -1;
            if (!HaveStock(quantity))
                throw new DomainException("Estoque insuficiente");
            StockQuantity -= quantity;
        }

        public void IncreaseStock(int quantity)
        {
            StockQuantity += quantity;
        }

        public bool HaveStock(int quantity)
        {
            return StockQuantity >= quantity;
        }

        public void Validate()
        {
            Validations.ValidateForEmpty(Name, "O campo Nome do produto não pode estar vazio");
            Validations.ValidateForEmpty(Description, "O campo Descricao do produto não pode estar vazio");
            Validations.ValidateIfEqual(CategoryId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            Validations.ValidateIfEqual(ImageId, Guid.Empty, "O campo ImageId do produto não pode estar vazio");
            Validations.ValidateLessThan(Value, 1, "O campo Valor do produto não pode se menor ou igual a 0");
        }
    }
}