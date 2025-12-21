namespace YourProject.Models
{
	public class Macro
	{
        public int Id { get; set; }              
        public string KeyCombination { get; set; } 
        public string Description { get; set; }   
        public string KeyName { get; set; }       
    }

    public class Porf
    {
        public int Id { get; set; }
        public string Profile { get; set; }
        public string IdMakros { get; set; }
        
    }
}