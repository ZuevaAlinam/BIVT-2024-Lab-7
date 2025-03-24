using System;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penalty;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties => _penalty?.Clone() as int[];

            public int Total
            {
                get
                {
                    if (_penalty == null) return 0;
                    int total = 0;
                    foreach (int p in _penalty)
                        total += p;
                    return total;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penalty == null) return false;
                    foreach (int p in _penalty)
                        if (p == 10) return true;
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalty = Array.Empty<int>();
            }

            public virtual void PlayMatch(int time)
            {
                if (time < 0) return;

                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[_penalty.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].Total < array[j].Total)
                        {
                            (array[j + 1], array[j]) = (array[j], array[j + 1]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.Write("{0,-20} {1,-20}", Name, Surname);
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5) return;
                base.PlayMatch(fouls);
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penalty == null || _penalty.Length == 0) return false;

                    int fiveFoulsCount = 0;
                    foreach (int fouls in _penalty)
                    {
                        if (fouls == 5) fiveFoulsCount++;
                    }

                    bool condition1 = (double)fiveFoulsCount / _penalty.Length > 0.1;
                    bool condition2 = Total > 2 * _penalty.Length;

                    return condition1 || condition2;
                }
            }
        }

        public class HockeyPlayer : Participant
        {
            private static Participant[] _allHockeyPlayers = Array.Empty<Participant>();

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                Array.Resize(ref _allHockeyPlayers, _allHockeyPlayers.Length + 1);
                _allHockeyPlayers[_allHockeyPlayers.Length - 1] = this;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (base.IsExpelled) return true;

                    if (_allHockeyPlayers.Length == 0) return false;

                    int totalPenalties = 0;
                    foreach (var player in _allHockeyPlayers)
                        totalPenalties += player.Total;

                    double average = (double)totalPenalties / _allHockeyPlayers.Length;
                    return Total > 0.1 * average;
                }
            }
        }
    }
}
