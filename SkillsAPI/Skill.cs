namespace SkillsAPI
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public List<string>? Tools { get; set; } = new List<string>();
        public string Description { get; set; } = string.Empty;
        public string Function { get; set; } = string.Empty;
        public string SubjectField { get; set; } = string.Empty;    

    }
}
