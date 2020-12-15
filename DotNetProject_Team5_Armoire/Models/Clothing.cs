using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject_Team5_Armoire.Models
{
    public class Clothing : BaseEntity
    {
        public string OwnerId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string ClothName { get; set; }
        public bool IsClean { get; set; }
        public string PictureUri { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Clothing(string ownerId, string clothName, bool isClean, string pictureUri, int categoryId)
        {
            OwnerId = ownerId;
            ClothName = clothName;
            IsClean = isClean;
            PictureUri = pictureUri;
            CategoryId = categoryId;
        }

        public Clothing() { }
    }
}
