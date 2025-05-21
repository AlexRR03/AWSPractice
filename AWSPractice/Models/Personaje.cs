using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWSPractice.Models
{

    [Table("PERSONAJES")]
    public class Personaje
    {
        [Key]
        [Column("IDPERSONAJE")]
        public int IdPersonaje { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("POSICION")]
        public string Posicion { get; set; }
        [Column("FECHA")]
        public int Fecha { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }

        [NotMapped]
        public string ImagenUrl => "https://s3.amazonaws.com/bucket-test-arr/" + Imagen;


    }
}
