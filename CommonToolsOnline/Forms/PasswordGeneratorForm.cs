namespace CommonToolsOnline.Forms
{

    public class PasswordGeneratorForm
    {

        public int PasswordLength { get; set; } = 16;

        public bool UseUppercaseAlphabets { get; set; } = true;

        public bool UseLowercaseAlphabets { get; set; } = true;

        public bool UseNumberCharacters { get; set; } = true;

        public bool UseCustomCharacters { get; set; } = false;

        public string CustomCharacters { get; set; } = @"!@#$%^&*?";

    }

}