using System.ComponentModel.DataAnnotations;

namespace AspNetMvcEFSobes.Models
{
    public class Account
    {
        [Display(Name = "ID счёта")]
        public int Id { get; set; }
        [Display(Name = "ID владельца счёта")]
        public int PersonId { get; set; }
        [Display(Name = "Текущий баланс счёта")]
        public decimal MoneyRUB { get; set; }
        [Display(Name = "Счёт принадлежит юр. лицу")]
        public bool isJuridicalPerson { get; set; }
        
        public List<Operation> Operations { get; set; } = new();
    }
}
