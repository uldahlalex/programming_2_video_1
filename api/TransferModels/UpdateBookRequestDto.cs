using System.ComponentModel.DataAnnotations;

namespace api.TransferModels;

public class UpdateBookRequestDto
{

        public int BookId { get; set; }
        [MinLength(4)]
        public string BookTitle { get; set; }
        public string Author  { get; set; }
        public string CoverImgUrl { get; set; }
        public string Publisher { get; set; }
    
}