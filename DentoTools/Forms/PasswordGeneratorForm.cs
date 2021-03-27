using System.ComponentModel.DataAnnotations;

namespace DentoTools.Forms
{

    public class PasswordGeneratorForm
    {

        [Range(8, 64, ErrorMessage = "The accepted password length is between 8 to 64.")]
        public int PasswordLength { get; set; } = 16;

        public bool UseUppercaseAlphabets { get; set; } = true;

        public bool UseLowercaseAlphabets { get; set; } = true;

        public bool UseNumberCharacters { get; set; } = true;

        public bool UseCustomCharacters { get; set; } = false;

        public string CustomCharacters { get; set; } = @"!@#$%^&*?";

    }

}