﻿using System.ComponentModel.DataAnnotations;

namespace CommonToolsOnline.Forms
{

    public class CodeFormatterForm
    {

        public string Language { get; set; } = "JSON";

        [Required(ErrorMessage = "You must specify code to format.")]
        public string Code { get; set; }

    }

}