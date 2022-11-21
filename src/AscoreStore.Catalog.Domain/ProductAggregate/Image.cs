using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Catalog.Domain.ProductAggregate
{
    public class Image : Entity
    {
        public string Name { get; private set; }
        public string ContentType { get; private set; }
        public int Size { get; private set; }
        public byte[] Data { get; private set; }


        public Image(string name, string contentType, int size, byte[] data)
        {
            Name = name;
            ContentType = contentType;
            Size = size;
            Data = data;

            Validate();
        }

        public void Validate()
        {
            Validations.ValidateForEmpty(Name, "O campo Nome da imagem não pode estar vazio");
            Validations.ValidateForEmpty(ContentType, "O campo ContentType da imagem não pode estar vazio");
            Validations.ValidateLessThan(Size, 1, "O campo Size da imagem não pode ser menor ou igual a 0");
            Validations.ValidateLessThan(Data.Length, 1, "O campo data da imagem não pode estar vazio");
        }

    }
}