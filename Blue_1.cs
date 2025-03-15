using System;
namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {

            private string _name;
            protected int _counter;

            public string Name => _name;
            public int Votes => _counter;

            public Response(string name)
            {
                _name = name;
           
                _counter = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int cnt_votes = 0;
                foreach (var response in responses)
                {
                    if (response.Name == Name ) cnt_votes++;
                }
                return cnt_votes;
            }

            public virtual void Print()
            {
                Console.Write("{0,-20}", Name);


            }
        }
        public class HumanResponse : Response
        {
            private string _surname;
            public string Surname => _surname;
            public HumanResponse(string name, string surnameOfResponse) : base(name) { 
         
                _surname = surnameOfResponse;
            }
            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int cnt_votes = 0;
                foreach (var response in responses)
                {
                    if (response is HumanResponse human && human.Name == Name && human.Surname == Surname) cnt_votes++;
                }
                return cnt_votes;
            }

            public override void Print()
            {
                base.Print();
                Console.Write("{0,-20} ",Surname);


            }
        }
    }
}
