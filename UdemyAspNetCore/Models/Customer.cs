using System.ComponentModel.DataAnnotations;

namespace UdemyAspNetCore.Models
{
    public class Customer
    {
        [Required(ErrorMessage ="id alanı boş geçilemez!")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad alanı boş geçilemez!")]
        [MaxLength(30,ErrorMessage ="Ad alanı en fazla 30 karakterden oluşur")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad alanı boş geçilemez!")]
        [MinLength(3, ErrorMessage = "Soyad alanı en az 3 karakterden oluşur")]
        public string LastName { get; set; }
        [Range(18,40,ErrorMessage ="Yaş en az 18 en fazla 40 olabilir")]
        public int Age { get; set; }
    }
}
