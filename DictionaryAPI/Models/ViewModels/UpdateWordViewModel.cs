namespace DictionaryAPI.Models.ViewModels
{
    public class UpdateWordViewModel : IUpdateWordViewModel
    {
        public long Id { get;set; }
        public string Name { get;set; }
        public List<DefinitionViewModel> Definitions { get;set; } = new List<DefinitionViewModel>();
    }
}
