using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject_Team5_Armoire.Models
{
    public class Cloth : BaseEntity
    {
        public Guid UserId { get; set; }
        public string ClothName { get; set; }
        public bool IsClean { get; set; }
        public string PictureUri { get; set; }

        public Cloth (Guid userid, String clothName, bool isClean, String pictureUri)
        {
            UserId = userid;
            ClothName = clothName;
            IsClean = isClean;
            PictureUri = pictureUri;
        }
    }
}
