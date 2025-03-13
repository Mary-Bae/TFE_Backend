namespace Domain
{
    public class Demandes
    {
        //Provisoire
        public Demandes(string Type)
        {
            this.Type = Type;
        }
       
            public int Id { get; set; }
            public string Type { get; set; }
            public DateTime DateBegin { get; set; }
            public DateTime DateEnd { get; set; }
            public string Comment { get; set; }
        

    }
}
