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
            public int[] Penalties
            {
                get
                {
                    if (_penalty == null) return default(int[]);
                    else
                    {
                        int[] penaltyTimes = new int[_penalty.Length];
                        for (int i = 0; i < penaltyTimes.Length; i++)
                            penaltyTimes[i] = _penalty[i];
                        return penaltyTimes;
                    }
                }
            }

            public int Total
            {
                get
                {
                    if (_penalty == null) return 0;
                    else
                    {
                        int totalTime = 0;
                        for (int i = 0; i < _penalty.Length; i++)
                            totalTime += _penalty[i];
                        return totalTime;
                    }
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penalty == null) return false;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        if (_penalty[i] == 10)
                            return true;
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalty = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                if (_penalty == null) _penalty = new int[0];
                if (time < 0) return;

                int[] newar = new int[_penalty.Length + 1];
                for (int i = 0; i < _penalty.Length; i++)
                    newar[i] = _penalty[i];
                newar[_penalty.Length] = time;
                _penalty = newar;
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

            public override void PlayMatch(int fall)
            {
                if (fall < 0 || fall > 5) return;
                base.PlayMatch(fall);
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penalty == null || _penalty.Length == 0) return false;

                    int cntOfFalls = 0;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        if (_penalty[i] == 5) cntOfFalls++;
                    }

                    return (double)cntOfFalls / _penalty.Length > 0.1;
                }
            }
        }

        public class HockeyPlayer : Participant
        {
            private Participant[] _hockeyPlayers;
            public Participant[] HockeyPlayers => _hockeyPlayers;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _hockeyPlayers = new Participant[0];
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_hockeyPlayers == null || _hockeyPlayers.Length == 0) return false;

                    bool firstPart = base.IsExpelled;
                    int sumOfHockeyPlayers = 0;
                    foreach (var player in _hockeyPlayers)
                    {
                        sumOfHockeyPlayers += player.Total;
                    }

                    int totalHockeys = sumOfHockeyPlayers / _hockeyPlayers.Length;
                    bool secondPart = Total > 0.1 * totalHockeys;

                    return firstPart || secondPart;
                }
            }
        }
    }
}
