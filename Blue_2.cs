using System;
using System.Linq;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _globalI;
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks => _marks?.Clone() as int[,];

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            sum += _marks[i, j];
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _globalI = 0;
            }

            public void Jump(int[] result)
            {
                if (result == null || _globalI > 1 || result.Length != 5) return;

                for (int j = 0; j < 5; j++)
                {
                    _marks[_globalI, j] = result[j];
                }
                _globalI++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore)
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

        public abstract class WaterJump
        {
            private Participant[] _participants;
            private int _count;

            public string Name { get; }
            public int Bank { get; }
            public Participant[] Participants
            {
                get
                {
                    if (_participants == null)
                        return null;
                    return _participants.Take(_count).ToArray();
                }
            }
            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                Name = name;
                Bank = bank;
                _participants = Array.Empty<Participant>();
                _count = 0;
            }

            public void Add(Participant participant)
            {
                if (_count == _participants.Length)
                {
                    Array.Resize(ref _participants, _count + 1);
                }
                _participants[_count++] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null) return;

                foreach (var p in participants)
                {
                    Add(p);
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return null;
                    return new double[]
                    {
                        0.5 * Bank,
                        0.3 * Bank,
                        0.2 * Bank
                    };
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    int count = Participants.Length;
                    if (count < 3) return null;

                    int topCount = count / 2;
                    if (topCount < 3) topCount = 3;
                    if (topCount > 10) topCount = 10;

                    double[] prizes = new double[topCount];
                    double n = 20.0 / topCount;

                    prizes[0] = 0.4 * Bank + n / 100 * Bank;
                    prizes[1] = 0.25 * Bank + n / 100 * Bank;
                    prizes[2] = 0.15 * Bank + n / 100 * Bank;

                    for (int i = 3; i < topCount; i++)
                    {
                        prizes[i] = n / 100 * Bank;
                    }

                    return prizes;
                }
            }
        }
    }
}
