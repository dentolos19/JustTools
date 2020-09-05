using System.ComponentModel.DataAnnotations;

namespace CommonToolsOnline.Forms
{

    public class CodeFormatterForm
    {

        [Required(ErrorMessage = "You must select a language to format.")]
        public string Language { get; set; } = "JSON";

        [Required(ErrorMessage = "You must specify JSON code to format.")]
        public string Code { get; set; }

    }

}