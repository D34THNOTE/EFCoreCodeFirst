using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirst.Models
{
    public class Prescription_Medicament
    {
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        
        public int? Dose { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Details { get; set; }
        
        // HERE WE DON'T DEFINE IdMedicament like we defined IdDoctor in Prescription because these fields already exist(and are PKs) as the first fields of this class
        public virtual Prescription Prescription { get; set; }
        
        public virtual Medicament Medicament { get; set; }
    }
}