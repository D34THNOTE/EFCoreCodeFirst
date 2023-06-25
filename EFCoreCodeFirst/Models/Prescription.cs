using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace EFCoreCodeFirst.Models
{
    public class Prescription
    {
        [Key]
        public int IdPrescription { get; set; }
    
        [Required]
        public DateTime Date { get; set; }
    
        [Required]
        public DateTime DueDate { get; set; }

        // The 2 below create the "one" side of "one to many" association between Prescription and Doctor tables
        public int IdDoctor { get; set; }
        
        //[ForeignKey(nameof(IdDoctor))] don't use this if you are using OnModelCreating
        public virtual Doctor Doctor { get; set; }
        
        
        public int IdPatient { get; set; }
        
        //[ForeignKey(nameof(IdDoctor))] don't use this if you are using OnModelCreating
        public virtual Patient Patient { get; set; }
        
        public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; } = new List<Prescription_Medicament>();
    }
}
