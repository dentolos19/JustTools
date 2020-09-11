using System.ComponentModel.DataAnnotations;

namespace CommonToolsOnline.Forms
{

    public class TextFormatterForm
    {

        public string Format { get; set; } = "Reverse";

        [Required(ErrorMessage = "You must specify text to format.")]
        public string Text { get; set; }

    }

}