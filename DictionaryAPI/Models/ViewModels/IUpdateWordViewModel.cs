namespace DictionaryAPI.Models.ViewModels
{
    public interface IUpdateWordViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<DefinitionViewModel> Definitions { get; set; }}
}
