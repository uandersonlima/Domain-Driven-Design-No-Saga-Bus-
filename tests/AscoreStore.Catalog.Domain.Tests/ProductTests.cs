using AscoreStore.Catalog.Domain.ProductAggregate;
using AscoreStore.Core.DomainObjects;
using Xunit;

namespace AscoreStore.Catalog.Domain.Tests;


public class ProductTests
{
    [Fact]
    public void Product_Validate_ValidationsMustReturnExceptions()
    {
        // Arrange & Act & Assert
        var ex = Assert.Throws<DomainException>(() =>
            new Product(string.Empty, "Descricao", false, 100, DateTime.Now, new Dimensions(1, 1, 1), Guid.NewGuid(), new Image("nova imagem", "novo conteudo", 10, new byte[10]))
        );

        Assert.Equal("O campo Nome do produto não pode estar vazio", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Nome", string.Empty, false, 100, DateTime.Now, new Dimensions(1, 1, 1), Guid.NewGuid(), new Image("nova imagem", "novo conteudo", 10, new byte[10]))
        );

        Assert.Equal("O campo Descricao do produto não pode estar vazio", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Nome", "Descricao", false, 0, DateTime.Now, new Dimensions(1, 1, 1), Guid.NewGuid(), new Image("nova imagem", "novo conteudo", 10, new byte[10]))
        );

        Assert.Equal("O campo Valor do produto não pode se menor igual a 0", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Nome", "Descricao", false, 0, DateTime.Now, new Dimensions(1, 1, 1), Guid.Empty, new Image("nova imagem", "novo conteudo", 10, new byte[10]))
        );

        Assert.Equal("O campo CategoriaId do produto não pode estar vazio", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Nome", "Descricao", false, 0, DateTime.Now, new Dimensions(1, 1, 1), Guid.NewGuid(), null)
        );

        Assert.Equal("O campo Imagem do produto não pode estar vazio", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Nome", "Descricao", false, 0, DateTime.Now, new Dimensions(0, 1, 1), Guid.NewGuid(), new Image("nova imagem", "novo conteudo", 10, new byte[10]))
        );

        Assert.Equal("O campo Altura não pode ser menor ou igual a 0", ex.Message);
    }
}