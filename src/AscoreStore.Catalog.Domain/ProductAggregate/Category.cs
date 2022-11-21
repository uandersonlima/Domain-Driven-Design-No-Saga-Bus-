using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Catalog.Domain.ProductAggregate
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }


        //Ef Relation
        public ICollection<Product> Products { get; set; }


        protected Category() { }
        public Category(string name, int code)
        {
            Name = name;
            Code = code;
            
            Validate();
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            Validations.ValidateForEmpty(Name, "O campo Nome da categoria não pode estar vazio");
            Validations.ValidateIfEqual(Code, 0, "O campo Codigo não pode ser 0");
        }

    }
}