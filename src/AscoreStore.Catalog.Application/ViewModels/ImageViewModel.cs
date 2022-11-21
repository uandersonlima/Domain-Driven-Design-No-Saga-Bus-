using System.ComponentModel.DataAnnotations;

namespace AscoreStore.Catalog.Application.ViewModels
{
    public class ImageViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ContentType { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Size { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public byte[] Data { get; set; }
    }
}