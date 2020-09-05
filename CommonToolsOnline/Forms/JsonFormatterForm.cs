using System.ComponentModel.DataAnnotations;

namespace CommonToolsOnline.Forms
{

    public class JsonFormatterForm
    {

        [Required(ErrorMessage = "You must enter a JSON code to format.")]
        public string Code { get; set; }

    }

}