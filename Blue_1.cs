using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            protected int _votes;

            public string Name { get { return _name; } }
            public int Votes
            {
                get
                {
                    return _votes;
                }
            }

            public Response(string name)
            {
                _name = name;
                _votes = 0;

            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == _name)
                    {
                        count++;
                    }
                }
                _votes = count;
                return _votes;
            }
            public virtual void Print()
            {
                Console.WriteLine($"{Name} {Votes}");
            }
        }
        // класс-наследник
        public class HumanResponse : Response
        {

            private string _surname;
            public string Surname { get { return _surname; } }

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    HumanResponse humanresponse = responses[i] as HumanResponse; 
                    if ((humanresponse.Name == Name) && (humanresponse.Surname == Surname))
                    {
                        count++;
                    }
                }
                _votes = count;
                return _votes;
            }
            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname} {Votes}");
            }
        }
    }
}
