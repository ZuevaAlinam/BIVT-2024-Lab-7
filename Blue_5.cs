using System;



namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                if (_place != 0) return;
                else _place = place;
            }

            public void Print()
            {
                Console.Write("{0,-20} {1,-20} {2, -10}", Name, Surname,Place);
            }
        }

        public abstract class Team
        {
            private string _name;
            protected Sportsman[] _sportsmen;
            private int _cnt;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
           

            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _cnt; i++)
                    {
                        switch (_sportsmen[i].Place)
                        {
                            case 1: sum += 5; break;
                            case 2: sum += 4; break;
                            case 3: sum += 3; break;
                            case 4: sum += 2; break;
                            case 5: sum += 1; break;
                            default: sum += 0; break;
                        }
                    }
                    return sum;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int top = 18;
                    for (int i = 0; i < _cnt; i++)
                    {
                        if (_sportsmen[i].Place < top)
                            top = _sportsmen[i].Place;
                    }
                    return top;
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _cnt = 0;
            }
            public void Add(Sportsman sportsman)
            {

                if (_sportsmen == null) return;                
                    
                
                if (_cnt < 6)                
                    _sportsmen[_cnt++] = sportsman;
                                 
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || sportsmen == null) return;
               
                for (int i = 0; i < sportsmen.Length; i++)
                {
                    Add(sportsmen[i]);
                }
              
            }
            public static void Sort(Team[] teams)
            {

                if (teams == null) return;
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j + 1].SummaryScore > teams[j].SummaryScore)
                        {
                            (teams[j + 1], teams[j]) = (teams[j], teams[j + 1]);
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                            {
                                (teams[j+1],  teams[j]) = (teams[j], teams[j+1]);
                            }
                        }
                    }
                }
            }

            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;

                Team champ = teams[0];
                double maxChampStr = champ.GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double nowStr = teams[i].GetTeamStrength();
                    if (nowStr > maxChampStr)
                    {
                        maxChampStr = nowStr;
                        champ = teams[i];
                    }
                }

                return champ;
            }
            public void Print()
            {
                Console.WriteLine("{0,-20}", Name);
            }

        }

        public class ManTeam : Team
        {
          
            public ManTeam(string name) : base(name) {
           
            }
            protected override double GetTeamStrength()
            {
                if (_sportsmen == null) return 0;

                double sumPlaces = 0;
                int count = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    
                        sumPlaces += _sportsmen[i].Place;
                        count++;
                    
                }

                if (sumPlaces == 0) return 0;
                else
                {
                    double sred = sumPlaces / count;
                    return 100 / sred;
                }
            }
        }
        public class WomanTeam : Team
        {
           
            public WomanTeam(string name) : base(name)
            {
             
            }
            protected override double GetTeamStrength()
            {
                if (_sportsmen == null) return 0;

                double sumOfPlace = 0;
                double participants = 1;
                int count = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] != null)
                    {
                        sumOfPlace += _sportsmen[i].Place;
                        participants *= _sportsmen[i].Place;
                        count++;
                    }
                }

                if (participants == 0) return 0;
                else return 100 * count* sumOfPlace/participants;
                
            }
        }
    }
}
