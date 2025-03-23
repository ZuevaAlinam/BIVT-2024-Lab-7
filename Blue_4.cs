using System;


namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _score;

            public string Name=>_name;
            public int[] Scores
            {
                get
                {
                    if(_score==null) return default(int[]);
                    int[] res = new int[_score.Length];
                    for(int i =0; i < _score.Length; i++) res[i] = _score[i];
                    return res;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_score == null) return 0;
                    int sum = 0;
                    for(int i = 0; i < _score.Length; i++) sum += _score[i];
                    return sum;
                }
            }

            public Team(string name)
            {
                _name = name;
                _score = new int[0];
            }

            public void PlayMatch(int result)
            {
                if(_score == null) return;
                int[] newmas = new int[_score.Length+1];
                for(int i = 0;i < newmas.Length; i++)
                {
                    if(i==_score.Length)
                        newmas[i] = result;
                    else
                        newmas[i] = _score[i];

                }
                _score = newmas;
            }
            public void Print()
            {
                Console.WriteLine("{0,-20}", Name);
            }

        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }
        public class Group
        {
            private string _name;
            
            private int _Manindex;
            private int _Womanindex;
            public string Name => _name;
            public Team[] ManTeams { get; private set; }
            public Team[] WomanTeams { get; private set; }
            public Group(string name)
            {
                _name = name;
                ManTeams = new Team[12];
                WomanTeams = new Team[12];
                _Manindex = 0;
                _Womanindex = 0;
            }

            public void Add(Team team)
            {
                if (ManTeams == null || WomanTeams == null) return;
                if (team is ManTeam manTeam && _Manindex < ManTeams.Length)
                    ManTeams[_Manindex++] = manTeam;
                else if (team is WomanTeam womanTeam && _Womanindex < WomanTeams.Length)
                    ManTeams[_Womanindex++] = womanTeam;
            }
            public void Add(Team[] teams)
            {
                if (ManTeams == null|| WomanTeams == null || ManTeams.Length==0 || WomanTeams.Length==0) return;
                
                for (int i = 0; i < teams.Length; i++) { 
                   Add(teams[i]);
                }
               
            }
            public void Sort()
            {
                SortTeam(ManTeams);
                SortTeam(WomanTeams);
            }
            public void SortTeam(Team[] baseTeam) {

                if (baseTeam == null) return;
                for (int i = 0; i < baseTeam.Length - 1; i++)
                {
                    for (int j = 0; j < baseTeam.Length - i - 1; j++)
                    {
                        if (baseTeam[j + 1].TotalScore > baseTeam[j].TotalScore)
                        {
                            (baseTeam[j + 1], baseTeam[j]) = (baseTeam[j], baseTeam[j + 1]);
                        }
                    }
                }
             
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                

                Group final = new Group("Финалисты");
                

                MergeTeams(group1.ManTeams, group2.ManTeams, final.ManTeams, size/2);
                MergeTeams(group1.WomanTeams, group2.WomanTeams, final.WomanTeams, size / 2);

                return final;
            }
            public static void MergeTeams(Team[] team1, Team[] team2, Team[] finalGroup, int size)
            {
                
                
                int index1 = 0, index2 = 0, itog=0;

                while (index1 < size && index2< size)
                {
                    if (team1[index1].TotalScore >= team2[index2].TotalScore)
                    {
                        finalGroup[itog++] = team1[index1++];
                    }
                    else
                    {
                        finalGroup[itog++] = team2[index2++];
                    }
                    
                }

                while (index1<size)
                {
                    finalGroup[itog++] = team1[index1++];

                }

                while (index2<size)
                {
                    finalGroup[itog++] = team2[index2++];

                }

            }

            public void Print()
            {
                Console.WriteLine("{0,-20}", Name);
            }
        }
    }
}
