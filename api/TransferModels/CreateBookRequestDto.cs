using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class CreateBookRequestDto
{
    [MinLength(5)]
    public string BookTitle { get; set; }
    
    public string Author  { get; set; }
    public string CoverImgUrl { get; set; }   
    
    [ValueIsOneOf(new string[] {"publisher_1", "publisher_2"}, "Must be one one ...")]

    public string Publisher { get; set; }
    
}