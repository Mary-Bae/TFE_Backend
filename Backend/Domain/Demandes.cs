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
            public DateOnly DateBegin { get; set; }
            public DateOnly DateEnd { get; set; }
            public string Comment { get; set; }
        

    }
}
