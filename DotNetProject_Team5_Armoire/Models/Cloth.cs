using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject_Team5_Armoire.Models
{
    public class Cloth : BaseEntity
    {
        public int UserId { get; set; }
        public string ClothName { get; set; }
        public bool IsClean { get; set; }
        public string PictureUri { get; set; }
        public int CategoryId { get; set; }

        public Cloth (int userid, String clothName, bool isClean, String pictureUri, int categoryId)
        {
            UserId = userid;
            ClothName = clothName;
            IsClean = isClean;
            PictureUri = pictureUri;
            CategoryId = categoryId;
        }
    }
}
