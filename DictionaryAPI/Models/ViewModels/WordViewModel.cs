namespace DictionaryAPI.Models.ViewModels
{
    public class WordViewModel
    {
        public string Name { get; set; } = string.Empty;
        public List<DefinitionViewModel> Definitions { get; set; } = new List<DefinitionViewModel>();
    }
}
