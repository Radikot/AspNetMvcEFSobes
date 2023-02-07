using System.ComponentModel.DataAnnotations;

namespace AspNetMvcEFSobes.Models
{
    public class Operation
    {
        public int Id { get; set; }
        [Display(Name = "Операция - доход")]
        public bool isIncome { get; set; }
        [Display(Name = "Изменение счёта, руб.")]
        public Decimal BalanceChange { get; set; }
        [Display(Name = "Дата и время операции")]
        public DateTime DateTime { get; set; }
        
        public Account? Account { get; set; }
        [Display(Name = "ID банковского счёта")]
        public int AccountId { get; set; }
    }
}
