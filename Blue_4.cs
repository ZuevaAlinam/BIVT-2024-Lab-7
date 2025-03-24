using System;
using System.Linq;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;

            public string Name => _name;
            public int[] Scores => _scores?.Clone() as int[];

            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int sum = 0;
                    foreach (int score in _scores)
                        sum += score;
                    return sum;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = Array.Empty<int>();
            }

            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length-1] = result;
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
            private Team[] _manTeams;
            private Team[] _womanTeams;
            private int _manCount;
            private int _womanCount;

            public string Name => _name;
            public Team[] ManTeams
            {
                get
                {
                    if (_manTeams == null)
                        return null;
                    return _manTeams.Take(_manCount).ToArray();
                }
            }
            public Team[] WomanTeams
            {
                get
                {
                    if (_womanTeams == null)
                        return null;
                    return _womanTeams.Take(_womanCount).ToArray();
                }
            }

            public Group(string name)
            {
                _name = name;
                _manTeams = new Team[12];
                _womanTeams = new Team[12];
                _manCount = 0;
                _womanCount = 0;
            }

            public void Add(Team team)
            {
                if (team is ManTeam manTeam && _manCount < _manTeams.Length)
                {
                    _manTeams[_manCount++] = manTeam;
                }
                else if (team is WomanTeam womanTeam && _womanCount < _womanTeams.Length)
                {
                    _womanTeams[_womanCount++] = womanTeam;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;

                foreach (Team team in teams)
                {
                    Add(team);
                }
            }

            public void Sort()
            {
                SortTeam(_manTeams, _manCount);
                SortTeam(_womanTeams, _womanCount);
            }

            private void SortTeam(Team[] teams, int count)
            {
                if (teams == null) return;

                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - i - 1; j++)
                    {
                        if (teams[j + 1]?.TotalScore > teams[j]?.TotalScore)
                        {
                            (teams[j + 1], teams[j]) = (teams[j], teams[j + 1]);
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group final = new Group("Финалисты");

                MergeTeams(group1.ManTeams, group2.ManTeams, final._manTeams, size / 2, ref final._manCount);
                MergeTeams(group1.WomanTeams, group2.WomanTeams, final._womanTeams, size / 2, ref final._womanCount);

                return final;
            }

            private static void MergeTeams(Team[] team1, Team[] team2, Team[] finalGroup, int size, ref int count)
            {
                int index1 = 0, index2 = 0;
                count = 0;

                while (index1 < size && index2 < size && count < finalGroup.Length)
                {
                    if (team1[index1]?.TotalScore >= team2[index2]?.TotalScore)
                    {
                        finalGroup[count++] = team1[index1++];
                    }
                    else
                    {
                        finalGroup[count++] = team2[index2++];
                    }
                }

                while (index1 < size && count < finalGroup.Length)
                {
                    finalGroup[count++] = team1[index1++];
                }

                while (index2 < size && count < finalGroup.Length)
                {
                    finalGroup[count++] = team2[index2++];
                }
            }

            public void Print()
            {
                Console.WriteLine("{0,-20}", Name);
            }
        }
    }
}
